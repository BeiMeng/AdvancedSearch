using System;

namespace AdvancedSearch.Util.Filter
{
    public class FilterRule
    {
        public FilterRule()
        {
        }

        public FilterRule(string field, object data, string op)
        {
            this.Field = field;
            this.Data = data;
            this.Op = op;
        }
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Data { get; set; }


        public object Value
        {
            get
            {
                string[] strval = ((string)Data).Split(',');
                if (strval.Length > 0 && OperatorType == OperatorType.In)
                {
                    return strval;
                }
                return Data;
            }
        }
        public string Op { get; set; }

        /// <summary>
        /// 比较符号
        /// </summary>
        public OperatorType OperatorType
        {
            get { return EnumHelper.GetInstance<OperatorType>(Op); }
        }

    }
}
