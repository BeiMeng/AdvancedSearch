namespace AdvancedSearch.Mvc.Builders.Tags {
    /// <summary>
    /// 输入标签生成器
    /// </summary>
    public class InputBuilder : TagBuilder {
        /// <summary>
        /// 初始化输入标签生成器
        /// </summary>
        public InputBuilder() : base( "input" ) {
        }

        /// <summary>
        /// 设置为文本框
        /// </summary>
        public virtual void SetText() {
            AddAttribute( "type","text" );
        }

        /// <summary>
        /// 设置为复选框
        /// </summary>
        public virtual void SetCheckBox() {
            AddAttribute( "type", "checkbox" );
        }

        /// <summary>
        /// 设置为单选框
        /// </summary>
        public virtual void SetRadio() {
            AddAttribute( "type", "radio" );
        }

        /// <summary>
        /// 获取标签Html
        /// </summary>
        public override string GetTagHtml() {
            return string.Format( "<{0}/>", GetBeginTagOptions() );
        }
    }
}
