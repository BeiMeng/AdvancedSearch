using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using AdvancedSearch.Mvc.Controls;

namespace AdvancedSearch.Mvc.Services
{
    public class UiService<TEntity>:IUiService<TEntity> 
    {
                /// <summary>
        /// 初始化EasyUi服务
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public UiService( HtmlHelper<TEntity> helper ) {
            _helper = helper;
        }

        /// <summary>
        /// HtmlHelper
        /// </summary>
        private readonly HtmlHelper<TEntity> _helper;
        public ITextBox TextBox<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            return UiFactory<TEntity>.CreateTextBox(expression, _helper);
        }

        public ITextBox TextBox(string name)
        {
            return new TextBox().Name(name);
        }
    }
}