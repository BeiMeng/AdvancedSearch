using System.Collections.Generic;
using System.Text;

namespace AdvancedSearch.Mvc.Builders.Nodes {
    /// <summary>
    /// "名-值列表"属性节点，范例：name="1,2,3"
    /// </summary>
    public class NameValuesAttributeNode : IAttributeNode {
        /// <summary>
        /// 初始化属性节点
        /// </summary>
        /// <param name="name">属性名</param>
        public NameValuesAttributeNode( string name ) {
            _values = new List<string>();
            Name = name;
            AttributeSeparator = "=";
            ValueSeparator = ";";
            ValueQuotes = "\"";
        }

        /// <summary>
        /// 属性值集合
        /// </summary>
        private readonly List<string> _values;

        /// <summary>
        /// 属性名称
        /// </summary>
        private string Name { get; set; }

        /// <summary>
        /// 属性分隔符
        /// </summary>
        public string AttributeSeparator { get; set; }

        /// <summary>
        /// 值分隔符
        /// </summary>
        public string ValueSeparator { get; set; }

        /// <summary>
        /// 值两边的引号字符串
        /// </summary>
        public string ValueQuotes { get; set; }

        /// <summary>
        /// 添加属性值
        /// </summary>
        /// <param name="value">属性值</param>
        public void Add( string value ) {
            _values.Add( value );
        }

        /// <summary>
        /// 移除属性值
        /// </summary>
        /// <param name="value">属性值</param>
        public void Remove( string value ) {
            _values.Remove( value );
        }

        /// <summary>
        /// 清空属性值
        /// </summary>
        public void Clear() {
            _values.Clear();
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() {
            if ( _values.Count == 0 )
                return string.Empty;
            var result = new StringBuilder();
            result.AppendFormat( "{0}{1}", Name, AttributeSeparator );
            result.AppendFormat( "{0}{1}{0}",ValueQuotes, GetValue() );
            return result.ToString();
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        public string GetValue() {
            return _values.Splice( "", ValueSeparator );
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            return GetResult();
        }
    }
}
