using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AdvancedSearch.Util.Filter;

namespace AdvancedSearch.Util
{
    /// <summary>
    /// 表达式构造类
    /// </summary>
    public class ExpressionCreator<T> where T:class 
    {
        #region 创建单个表达式树
        public static Expression CreateExpression(ParameterExpression param, string field, object value, OperatorType method)
        {
            Expression left = CreatePropertyExpression(param, field);
            Expression right = CreateConstantExpression(left, value);
            //以判断符或方法连接
            return ExpressionOperatorDict[method](left, right);
        } 
        #endregion

        #region 创建参数表达式
        /// <summary>
        /// 创建参数表达式
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns></returns>
        public static ParameterExpression CreateParameterExpression(string paramName)
        {
            return Expression.Parameter(typeof(T), paramName);
        } 
        #endregion

        #region 创建属性表达式
        /// <summary>
        /// 创建属性表达式,例如c.Name
        /// </summary>
        /// <param name="param">参数表达式</param>
        /// <param name="propertyName">属性名称,例如Name，支持多级名称c.Person.Name</param>
        /// <returns></returns>
        public static Expression CreatePropertyExpression(ParameterExpression param, string propertyName)
        {
            //获取每级属性如c.Users.Proiles.UserId
            var props = propertyName.Split('.');
            Expression propertyAccess = param;
            var typeOfProp = typeof(T);
            int i = 0;
            do
            {
                PropertyInfo property = typeOfProp.GetProperty(props[i]);
                if (property == null) return null;
                typeOfProp = property.PropertyType;
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                i++;
            } while (i < props.Length);
            return propertyAccess;
        } 
        #endregion

        #region Constant(创建常量表达式)

        /// <summary>
        /// 获取常量表达式，自动转换值的类型
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="value">值</param>
        public static Expression CreateConstantExpression(Expression expression, object value)
        {
            #region 数组

            if (value.GetType() == typeof (string[]))
            {
                var arr = ((Array)value);
                var expList = new List<Expression>();
                //确保可用
                for (var i = 0; i < arr.Length; i++)
                {
                    var changeValue = TypeHelper.ChangeType(arr.GetValue(i), expression.Type);
                    expList.Add(Expression.Constant(changeValue, expression.Type));
                }
                //构造inType类型的数组表达式树，并为数组赋初值
                return Expression.NewArrayInit(expression.Type, expList);
            }
            #endregion
            var elementType = TypeHelper.GetUnNullableType(expression.Type);
            var newValue = Convert.ChangeType(value, elementType);
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null)
                return Expression.Constant(newValue);
            return Expression.Constant(newValue, memberExpression.Type);
        }
        #endregion

        #region OperatorType 操作方法

        public static readonly Dictionary<OperatorType, Func<Expression, Expression, Expression>> ExpressionOperatorDict =
            new Dictionary<OperatorType, Func<Expression, Expression, Expression>>
                {
                    {
                        OperatorType.Equal,
                        (left, right) => { return Expression.Equal(left, right); }
                        },
                    {
                        OperatorType.Greater,
                        (left, right) => { return Expression.GreaterThan(left, right); }
                        },
                    {
                        OperatorType.GreaterEqual,
                        (left, right) => { return Expression.GreaterThanOrEqual(left, right); }
                        },
                    {
                        OperatorType.Less,
                        (left, right) => { return Expression.LessThan(left, right); }
                        },
                    {
                        OperatorType.LessEqual,
                        (left, right) => { return Expression.LessThanOrEqual(left, right); }
                        },
                    {
                        OperatorType.Contains,
                        (left, right) =>
                            {
                                if (left.Type != typeof (string)) return null;
                                return Expression.Call(left, typeof (string).GetMethod("Contains"), right);
                            }
                        },
                    {
                        OperatorType.In,
                        (left, right) =>
                            {
                                if (!right.Type.IsArray) return null;
                                //调用Enumerable.Contains扩展方法
                                MethodCallExpression resultExp =
                                    Expression.Call(
                                        typeof (Enumerable),
                                        "Contains",
                                        new[] {left.Type},
                                        right,
                                        left);

                                return resultExp;
                            }
                        },
                    {
                        OperatorType.NotEqual,
                        (left, right) => { return Expression.NotEqual(left, right); }
                        },
                    {
                        OperatorType.Starts,
                        (left, right) =>
                            {
                                if (left.Type != typeof (string)) return null;
                                return Expression.Call(left, typeof (string).GetMethod("StartsWith", new[] {typeof (string)}), right);

                            }
                        },
                    {
                        OperatorType.Ends,
                        (left, right) =>
                            {
                                if (left.Type != typeof (string)) return null;
                                return Expression.Call(left, typeof (string).GetMethod("EndsWith", new[] {typeof (string)}), right);
                            }
                        }
                };

        #endregion
    }
}