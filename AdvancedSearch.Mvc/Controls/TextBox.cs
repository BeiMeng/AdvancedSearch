using System.Text;
using AdvancedSearch.Mvc.Builders;
using AdvancedSearch.Mvc.Builders.Tags;
using AdvancedSearch.Util;

namespace AdvancedSearch.Mvc.Controls
{
    /// <summary>
    /// 文本框
    /// </summary>
    public class TextBox : TextBox<ITextBox>, ITextBox
    {
    }
    /// <summary>
    /// 文本框
    /// </summary>
    /// <typeparam name="T">文本框类型</typeparam>
    public abstract class TextBox<T> : ITextBox<T> where T : ITextBox<T>
    {
        /// <summary>
        /// 属性生成器
        /// </summary>
        private readonly InputBuilder _builder;

        protected TextBox()
        {
            _builder = new InputBuilder();
            _builder.SetText();
        }

        #region Textbox基础属性设置
        /// <summary>
        /// 设置name属性
        /// </summary>
        /// <param name="name">名称</param>
        public T Name(string name)
        {
            _builder.UpdateAttribute("name", name);
            return This();
        }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        public T Value(string value)
        {
            _builder.UpdateAttribute("value", value);
            return This();
        } 
        #endregion

        #region 设置查询方式操作
        /// <summary>
        /// 设置查询方式
        /// </summary>
        /// <param name="operatorType">查询方式枚举</param>
        /// <returns></returns>
        public T SearchWay(OperatorType operatorType)
        {
            string value = _builder.Get("name");
            string newValue = string.Format("[{0}]" + value, operatorType.ToString());
            _builder.UpdateAttribute("name", newValue);
            return This();
        }
        /// <summary>
        /// 相等
        /// </summary>
        /// <returns></returns>
        public T Equal()
        {
            return SearchWay((OperatorType.Equal));
        }
        /// <summary>
        /// 不相等
        /// </summary>
        /// <returns></returns>
        public T NotEqual()
        {
            return SearchWay((OperatorType.NotEqual));
        }
        /// <summary>
        /// 大于(仅限于,数值类型和时间类型)
        /// </summary>
        /// <returns></returns>
        public T Greater()
        {
            return SearchWay((OperatorType.Greater));
        }
        /// <summary>
        /// 大于等于(仅限于,数值类型和时间类型)
        /// </summary>
        /// <returns></returns>
        public T GreaterEqual()
        {
            return SearchWay((OperatorType.GreaterEqual));
        }
        /// <summary>
        /// 小于(仅限于,数值类型和时间类型)
        /// </summary>
        /// <returns></returns>
        public T Less()
        {
            return SearchWay((OperatorType.Less));
        }
        /// <summary>
        /// 小于等于(仅限于,数值类型和时间类型)
        /// </summary>
        /// <returns></returns>
        public T LessEqual()
        {
            return SearchWay((OperatorType.LessEqual));
        }
        /// <summary>
        /// 头尾匹配.like(包含)
        /// </summary>
        /// <returns></returns>
        public T Contains()
        {
            return SearchWay((OperatorType.Contains));
        }
        /// <summary>
        /// 包括，例如：in(a,b)
        /// </summary>
        /// <returns></returns>
        public T In()
        {
            return SearchWay((OperatorType.In));
        }
        /// <summary>
        /// 头匹配
        /// </summary>
        /// <returns></returns>
        public T Starts()
        {
            return SearchWay((OperatorType.Starts));
        }
        /// <summary>
        /// 尾匹配
        /// </summary>
        /// <returns></returns>
        public T Ends()
        {
            return SearchWay((OperatorType.Ends));
        } 
        #endregion

        /// <summary>
        /// 生成Html
        /// </summary>
        /// <returns></returns>
        public string ToHtmlString()
        {
            return _builder.ToString();
        }
        /// <summary>
        /// 返回组件
        /// </summary>
        protected T This()
        {
            return (T)((object)this);
        }
    }
}