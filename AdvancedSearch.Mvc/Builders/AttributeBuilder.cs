using System;
using System.Collections.Generic;
using System.Text;
using AdvancedSearch.Mvc.Builders.Nodes;

namespace AdvancedSearch.Mvc.Builders {
    /// <summary>
    /// 属性生成器
    /// </summary>
    public class AttributeBuilder {
        /// <summary>
        /// 初始化属性生成器
        /// </summary>
        /// <param name="attributeSeparator">属性分隔符,范例a="1"中的等号</param>
        /// <param name="nodeSeparator">属性节点分隔符，范例a="1",b="2"中的逗号</param>
        public AttributeBuilder( string attributeSeparator = "=", string nodeSeparator = " " ) {
            _nodes = new Dictionary<string, IAttributeNode>();
            _attributeSeparator = attributeSeparator;
            _nodeSeparator = nodeSeparator;
        }

        /// <summary>
        /// 属性节点集合
        /// </summary>
        private readonly Dictionary<string, IAttributeNode> _nodes;
        /// <summary>
        /// 属性分隔符
        /// </summary>
        private readonly string _attributeSeparator;
        /// <summary>
        /// 属性节点分隔符
        /// </summary>
        private readonly string _nodeSeparator;
        /// <summary>
        /// 是否包含指定项
        /// </summary>
        /// <param name="name">属性名</param>
        public bool Contains( string name ) {
            if ( string.IsNullOrWhiteSpace(name))
                return false;
            return _nodes.ContainsKey( name );
        }

        /// <summary>
        /// 参数个数
        /// </summary>
        public int Count {
            get { return _nodes.Count; }
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="name">属性名</param>
        public string Get( string name ) {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;
            if ( !_nodes.ContainsKey( name ) )
                return string.Empty;
            return _nodes[name].GetValue();
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="value">属性值</param>
        public void Add( string value ) {
            _nodes.Add( Guid.NewGuid().ToString(), new AttributeListNode( value ) );
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        /// <param name="separator">属性值分隔符，默认为分号</param>
        /// <param name="quotes">属性值两边的引号,默认为双引号</param>
        public void Add( string name, string value, string separator = ";", string quotes = "\"" ) {
            if (string.IsNullOrWhiteSpace(name))
                return;
            if ( _nodes.ContainsKey( name ) ) {
                MergeNode( name, value, separator );
                return;
            }
            AddNode( name, value, separator, quotes );
        }

        /// <summary>
        /// 合并节点
        /// </summary>
        private void MergeNode( string name, string value, string separator ) {
            var node = _nodes[name];
            node.ValueSeparator = separator;
            node.Add( value );
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        private void AddNode( string name, string value, string separator, string quotes ) {
            var node = new NameValuesAttributeNode( name ) { ValueSeparator = separator, AttributeSeparator = _attributeSeparator, ValueQuotes = quotes };
            node.Add( value );
            _nodes.Add( name, node );
        }

        /// <summary>
        /// 更新属性,不存在则添加
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        /// <param name="separator">属性值分隔符，默认为分号</param>
        /// <param name="quotes">属性值两边的引号,默认为双引号</param>
        public void Update( string name, string value, string separator = ";", string quotes = "\"" ) {
            if (string.IsNullOrWhiteSpace(name))
                return;
            if ( _nodes.ContainsKey( name ) )
                UpdateNode( name, value, separator, quotes );
            else
                AddNode( name, value, separator, quotes );
        }

        /// <summary>
        /// 更新节点
        /// </summary>
        private void UpdateNode( string name, string value, string separator, string quotes ) {
            Remove( name );
            AddNode( name, value, separator, quotes );
        }

        /// <summary>
        /// 移除属性
        /// </summary>
        /// <param name="name">属性名</param>
        public void Remove( string name ) {
            if ( !_nodes.ContainsKey( name ) )
                return;
            _nodes.Remove( name );
        }

        /// <summary>
        /// 添加class属性
        /// </summary>
        /// <param name="class">class属性值</param>
        public void AddClass( string @class ) {
            Add( "class", @class, " " );
        }

        /// <summary>
        /// 更新class属性
        /// </summary>
        /// <param name="class">class属性值</param>
        public void UpdateClass( string @class ) {
            Update( "class", @class, " " );
        }

        /// <summary>
        /// 移除指定class
        /// </summary>
        /// <param name="class">class值</param>
        public void RemoveClass( string @class ) {
            if ( !_nodes.ContainsKey( "class" ) )
                return;
            var node = _nodes["class"];
            node.Remove( @class );
        }

        /// <summary>
        /// 添加style属性
        /// </summary>
        /// <param name="name">style属性名</param>
        /// <param name="value">style属性值</param>
        public void AddStyle( string name, string value ) {
            Add( "style", string.Format( "{0}:{1}", name, value ) );
        }

        /// <summary>
        /// 添加data-属性
        /// </summary>
        /// <param name="name">data属性名，范例toggle,结果为data-toggle</param>
        /// <param name="value">属性值</param>
        public void AddDataAttribute( string name, string value ) {
            Add( string.Format( "data-{0}", name ), value );
        }



        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() {
            var result = new StringBuilder();
            foreach ( var node in _nodes )
                result.Append( GetValue( node.Value ) );
            return result.ToString().TrimEnd( _nodeSeparator.ToCharArray() );
        }

        /// <summary>
        /// 获取值
        /// </summary>
        private string GetValue( IAttributeNode node ) {
            var value = node.GetResult();
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return string.Format( "{0}{1}", value, _nodeSeparator );
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            return GetResult();
        }
    }
}
