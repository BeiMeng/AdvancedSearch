using System.Linq.Expressions;
using System.Reflection;

namespace AdvancedSearch.Util
{
    public class LambdaHelper
    {
        #region GetMember(获取成员)

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name</param>
        public static MemberInfo GetMember(Expression expression)
        {
            var memberExpression = GetMemberExpression(expression);
            if (memberExpression == null)
                return null;
            return memberExpression.Member;
        }

        /// <summary>
        /// 获取成员表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        public static MemberExpression GetMemberExpression(Expression expression)
        {
            if (expression == null)
                return null;
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetMemberExpression(((LambdaExpression)expression).Body);
                case ExpressionType.Convert:
                    return GetMemberExpression(((UnaryExpression)expression).Operand);
                case ExpressionType.MemberAccess:
                    return (MemberExpression)expression;
            }
            return null;
        }

        #endregion

        #region GetName(获取成员名称)

        /// <summary>
        /// 获取成员名称，范例：t => t.Name,返回 Name
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name</param>
        public static string GetName(Expression expression)
        {
            var memberExpression = GetMemberExpression(expression);
            return GetMemberName(memberExpression);
        }

        /// <summary>
        /// 获取成员名称
        /// </summary>
        private static string GetMemberName(MemberExpression memberExpression)
        {
            if (memberExpression == null)
                return string.Empty;
            string result = memberExpression.ToString();
            return result.Substring(result.IndexOf(".") + 1);
        }

        #endregion 
    }
}