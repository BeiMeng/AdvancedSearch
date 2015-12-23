using System;
using System.Linq.Expressions;
using AdvancedSearch.Mvc.Controls;

namespace AdvancedSearch.Mvc.Services
{
    public interface IUiService<TEntity> 
    {
        /// <summary>
        /// 文本框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        ITextBox TextBox<TProperty>(Expression<Func<TEntity, TProperty>> expression);

        ITextBox TextBox(string name);
    }
}