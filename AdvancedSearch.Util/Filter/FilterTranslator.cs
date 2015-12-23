using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AdvancedSearch.Util.Filter
{
    public static class FilterTranslator
    {
        #region 转换Lambda表达式
        public static Expression<Func<T, bool>> ToLambdaExpression<T>(FilterGroup filterGroup)where T:class 
        {
            if (filterGroup == null)
                return arg => true;
            if (filterGroup.Groups == null && filterGroup.Rules == null)
                return arg => true;
            if (filterGroup.Groups != null && (filterGroup.Groups.Count == 0 && filterGroup.Rules.Count == 0))
                return arg => true;
            //构建 c=>Body中的c
            ParameterExpression param = ExpressionCreator<T>.CreateParameterExpression("c");
            //构建c=>Body中的Body
            var body = CreateExpressions<T>(param, filterGroup);
            //将二者拼为c=>Body
            var expression = Expression.Lambda<Func<T, bool>>(body, param);
            return expression;
        }

        #region 创建表达式树集合
        private static Expression CreateExpressions<T>(ParameterExpression param, FilterGroup filterGroup) where T : class
        {
            var list = new List<Expression>();
            if (filterGroup.Rules != null)
            {
                foreach (var rule in filterGroup.Rules)
                {
                    list.Add(CreateExpression<T>(param, rule));
                }
            }
            if (filterGroup.Groups != null)
            {
                foreach (var subgroup in filterGroup.Groups)
                {
                    list.Add(CreateExpressions<T>(param, subgroup));
                }
            }
            if (filterGroup.GroupOp == GropuOp.And)
                return list.Aggregate(Expression.AndAlso);
            return list.Aggregate(Expression.OrElse);
        }
        #endregion

        #region 创建单个表达式树
        private static Expression CreateExpression<T>(ParameterExpression param, FilterRule filterRule) where T : class
        {
            return ExpressionCreator<T>.CreateExpression(param, filterRule.Field, filterRule.Value, filterRule.OperatorType);
        }
        #endregion

        #endregion

        #region 转换Sql语句
        public static string ToSql(FilterGroup filterGroup)
        {
            StringBuilder sb = new StringBuilder();

            if (filterGroup == null)
                return " 1=1 ";

            sb.Append("(");
            bool flag = false;
            if (filterGroup.Rules != null)
            {
                foreach (var rule in filterGroup.Rules)
                {
                    if (flag)
                        sb.Append(" " + filterGroup.GroupOp.ToString() + " ");
                    sb.Append(TranslateRule(rule));
                    flag = true;
                }
            }

            if (filterGroup.Groups != null)
            {
                foreach (var subgroup in filterGroup.Groups)
                {
                    if (flag)
                        sb.Append(" " + filterGroup.GroupOp.ToString() + " ");
                    sb.Append(ToSql(subgroup));
                    flag = true;
                }
            }

            sb.Append(")");
            return sb.ToString();
        }

        private static string TranslateRule(FilterRule rule)
        {
            StringBuilder sb = new StringBuilder();
            if (rule == null) return " 1=1 ";

            if (!string.IsNullOrEmpty(rule.Op))
            {
                string _op = GetOperatorQueryText(rule.Op);
                switch (rule.OperatorType)
                {
                    case OperatorType.Starts:
                        sb.Append(rule.Field + _op + "'" + rule.Value + "%'");
                        break;
                    case OperatorType.Ends:
                        sb.Append(rule.Field + _op + "'%" + rule.Value + "'");
                        break;
                    case OperatorType.Contains:
                        sb.Append(rule.Field + _op + "'%" + rule.Value + "%'");
                        break;
                    case OperatorType.In:
                        sb.Append(rule.Field + _op + "(" + rule.Value + ")");
                        break;
                    default:
                        sb.Append(rule.Field + _op + "'" + rule.Value + "'");
                        break;
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 将操作符代码转换为SQL的操作符号
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        private static string GetOperatorQueryText(string op)
        {
            switch (op)
            {
                case "Equal": return " = ";
                case "NotEqual": return " <> ";
                case "Greater": return " > ";
                case "GreaterEqual": return " >= ";
                case "Less": return " < ";
                case "LessEqual": return " <= ";
                case "Contains": return " like ";
                case "Starts": return " like ";
                case "Ends": return " like ";
                case "In": return " IN ";


                case "nu": return " IS NULL ";
                case "nn": return " IS NOT NULL ";
                case "Not In": return " NOT IN ";
                default: return " = ";
            }
        } 
        #endregion
    }
}
