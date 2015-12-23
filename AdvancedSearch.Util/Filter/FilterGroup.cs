using System.Collections.Generic;

namespace AdvancedSearch.Util.Filter
{
    public class FilterGroup
    {
        public FilterGroup()
        {
            Rules=new List<FilterRule>();
            Groups=new List<FilterGroup>();
        }

        /// <summary>
        /// 筛选条件组合方式 and or
        /// </summary>
        public GropuOp GroupOp { get; set; }
        public IList<FilterRule> Rules { get; set; }
        public IList<FilterGroup> Groups { get; set; }
    }

    public enum GropuOp
    { 
        And,
        Or
    }
}
