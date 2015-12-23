using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using AdvancedSearch.Mvc.Controls;

namespace AdvancedSearch.Mvc.Services
{
    public class UiFactory<TEntity>
    {
        /// <summary>
        /// 创建Ui服务
        /// </summary>
        public static IUiService<TEntity> CreateUiService(HtmlHelper<TEntity> helper)
        {
            return new UiService<TEntity>(helper);
        }
        /// <summary>
        /// 创建文本框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        public static ITextBox CreateTextBox<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, HtmlHelper<TEntity> helper)
        {
            return new EntityTextBox<TEntity, TProperty>(propertyExpression, helper);
        }
    }
}