using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Web.Mvc;
using AdvancedSearch.Mvc.Controls;
using AdvancedSearch.Mvc.Services;

namespace AdvancedSearch.Mvc
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class HtmlHelperExtentions
    {
        /// <summary>
        /// 创建Ui服务
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public static IUiService<TEntity> Ui<TEntity>(this HtmlHelper<TEntity> helper)
        {
            return UiFactory<TEntity>.CreateUiService(helper);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="expression">属性表达式</param>
        public static object Value<TEntity, TProperty>(this HtmlHelper<TEntity> helper, Expression<Func<TEntity, TProperty>> expression)
        {
            if (expression == null)
                return string.Empty;
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            return metadata.Model;
        }
    }
}