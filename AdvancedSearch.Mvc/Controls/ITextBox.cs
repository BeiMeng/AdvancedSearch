using System.Web;
using AdvancedSearch.Util;

namespace AdvancedSearch.Mvc.Controls
{
    /// <summary>
    /// 文本框
    /// </summary>
    public interface ITextBox : ITextBox<ITextBox>
    {
    }
    public interface ITextBox<out T>:IHtmlString where T : ITextBox<T>
    {
        /// <summary>
        /// 设置name属性
        /// </summary>
        /// <param name="name">名称</param>
        T Name(string name);
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        T Value(string value);
        /// <summary>
        /// 设置查询方式
        /// </summary>
        /// <param name="operatorType">查询方式枚举</param>
        /// <returns></returns>
        T SearchWay(OperatorType operatorType);
        /// <summary>
        /// 相等
        /// </summary>
        /// <returns></returns>
        T Equal();
        /// <summary>
        /// 不相等
        /// </summary>
        /// <returns></returns>
        T NotEqual();
        /// <summary>
        /// 大于
        /// </summary>
        /// <returns></returns>
        T Greater();
        /// <summary>
        /// 大于等于
        /// </summary>
        /// <returns></returns>
        T GreaterEqual();
        /// <summary>
        /// 小于
        /// </summary>
        /// <returns></returns>
        T Less();
        /// <summary>
        /// 小于等于
        /// </summary>
        /// <returns></returns>
        T LessEqual();
        /// <summary>
        /// 头尾匹配.like(包含)
        /// </summary>
        /// <returns></returns>
        T Contains();
        /// <summary>
        /// 包括，例如：in(a,b)
        /// </summary>
        /// <returns></returns>
        T In();
        /// <summary>
        /// 头匹配
        /// </summary>
        /// <returns></returns>
        T Starts();
        /// <summary>
        /// 尾匹配
        /// </summary>
        /// <returns></returns>
        T Ends();
    }
}