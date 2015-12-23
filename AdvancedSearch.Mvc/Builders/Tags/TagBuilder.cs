using System.Text;

namespace AdvancedSearch.Mvc.Builders.Tags {
    /// <summary>
    /// 标签生成器
    /// </summary>
    public class TagBuilder {
        /// <summary>
        /// 初始化标签生成器
        /// </summary>
        /// <param name="tagName">标签名称，范例：div</param>
        public TagBuilder( string tagName ) {
            TagName = tagName;
        }

        private AttributeBuilder _attributeBuilder;
        /// <summary>
        /// 属性生成器
        /// </summary>
        private AttributeBuilder AttributeBuilder {
            get { return _attributeBuilder ?? ( _attributeBuilder = new AttributeBuilder() ); }
        }

        /// <summary>
        /// 标签名称
        /// </summary>
        protected string TagName { get; private set; }

        /// <summary>
        /// Html内容
        /// </summary>
        protected string InnerHtml { get;private set; }

        /// <summary>
        /// 标签前Html
        /// </summary>
        protected string BeforeHtml { get; private set; }

        /// <summary>
        /// 待移除的标签前Html
        /// </summary>
        private string _removeBeforeHtml;

        /// <summary>
        /// 标签后Html
        /// </summary>
        protected string AfterHtml { get; private set; }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        public void AddAttribute( string name, string value ) {
            AttributeBuilder.Add( name, value );
        }

        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        public void UpdateAttribute( string name, string value ) {
            AttributeBuilder.Update( name, value );
        }

        /// <summary>
        /// 移除属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        public void RemoveAttribute( string name ) {
            AttributeBuilder.Remove( name );
        }

        /// <summary>
        /// 添加style属性
        /// </summary>
        /// <param name="name">style属性名</param>
        /// <param name="value">style属性值</param>
        public void AddStyle( string name, string value ) {
            AttributeBuilder.AddStyle( name,value );
        }

        /// <summary>
        /// 添加class属性
        /// </summary>
        /// <param name="class">class属性值</param>
        public void AddClass( string @class ) {
            AttributeBuilder.AddClass( @class );
        }

        /// <summary>
        /// 更新class属性
        /// </summary>
        /// <param name="class">class属性</param>
        public void UpdateClass( string @class ) {
            AttributeBuilder.UpdateClass( @class );
        }

        /// <summary>
        /// 添加data-属性
        /// </summary>
        /// <param name="name">data属性名，范例toggle,结果为data-toggle</param>
        /// <param name="value">属性值</param>
        public void AddDataAttribute( string name, string value ) {
            AttributeBuilder.AddDataAttribute( name, value );
        }
        /// <summary>
        /// 设置标签内部Html
        /// </summary>
        /// <param name="html">Html</param>
        public void SetInnerHtml( string html ) {
            InnerHtml = html;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="name">属性名</param>
        public string Get( string name ) {
            return AttributeBuilder.Get( name );
        }

        /// <summary>
        /// 在组件标签前添加html
        /// </summary>
        /// <param name="html">附加的html</param>
        public void AddBeforeHtml( string html ) {
            BeforeHtml += html;
        }

        /// <summary>
        /// 更新组件标签前html
        /// </summary>
        /// <param name="html">附加的html</param>
        public void UpdateBeforeHtml( string html ) {
            BeforeHtml = html;
        }

        /// <summary>
        /// 移除组件标签前html
        /// </summary>
        /// <param name="html">要移除的html</param>
        public void RemoveBeforeHtml( string html ) {
            _removeBeforeHtml = html;
        }

        /// <summary>
        /// 在组件标签后添加html
        /// </summary>
        /// <param name="html">附加的html</param>
        public void AddAfterHtml( string html ) {
            AfterHtml += html;
        }

        /// <summary>
        /// 更新组件标签后html
        /// </summary>
        /// <param name="html">附加的html</param>
        public void UpdateAfterHtml( string html ) {
            AfterHtml = html;
        }

        /// <summary>
        /// 获取Html结果
        /// </summary>
        public override string ToString() {
            return GetResult();
        }

        /// <summary>
        /// 获取Html结果
        /// </summary>
        public virtual string GetResult() {
            var result = new StringBuilder();
            result.Append( GetBeforeHtml() );
            result.Append( GetTagHtml() );
            result.Append( GetAfterHtml() );
            return GetResult( result );
        }

        /// <summary>
        /// 获取标签前Html
        /// </summary>
        public virtual string GetBeforeHtml() {
            return BeforeHtml;
        }

        /// <summary>
        /// 获取标签Html
        /// </summary>
        public virtual string GetTagHtml() {
            return string.Format( "{0}{1}{2}", GetBeginTag(), GetInnerHtml(), GetEndTag() );
        }

        /// <summary>
        /// 获取开始标签
        /// </summary>
        public virtual string GetBeginTag() {
            return string.Format( "<{0}>", GetBeginTagOptions() );
        }

        /// <summary>
        /// 获取开始标签及配置项
        /// </summary>
        protected string GetBeginTagOptions() {
            var options = GetOptions();
            return string.Format( "{0}{1}", TagName, string.IsNullOrWhiteSpace(options) ? "" : " " + options );
        }

        /// <summary>
        /// 获取配置项
        /// </summary>
        public virtual string GetOptions() {
            return AttributeBuilder.ToString();
        }

        /// <summary>
        /// 获取标签内部Html
        /// </summary>
        public virtual string GetInnerHtml() {
            return InnerHtml;
        }

        /// <summary>
        /// 获取结束标签
        /// </summary>
        public virtual string GetEndTag() {
            return string.Format( "</{0}>", TagName );
        }

        /// <summary>
        /// 获取标签后Html
        /// </summary>
        public virtual string GetAfterHtml() {
            return AfterHtml;
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        private string GetResult( StringBuilder html ) {
            var result = html.ToString();
            if (string.IsNullOrWhiteSpace(_removeBeforeHtml))
                return result;
            if ( !result.StartsWith( _removeBeforeHtml ) )
                return result;
            return result.Substring( _removeBeforeHtml.Length, result.Length - _removeBeforeHtml.Length );
        }
    }
}
