using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AdvancedSearch.Util;
using AdvancedSearch.Util.Filter;

namespace AdvancedSearch.Mvc
{
    /// <summary>
    /// 智能查询数据绑定模型
    /// </summary>
    public class SearchModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = (FilterGroup)(bindingContext.Model ?? new FilterGroup());
            var dict = controllerContext.HttpContext.Request.Params;
            var keys = dict.AllKeys.Where(c => c.StartsWith("[")).ToList();
            if (keys.Count() != 0)
            {
                foreach (var key in keys)
                {
                    if (!key.StartsWith("[")) continue;
                    var val = dict[key];

                    if (string.IsNullOrEmpty(val))
                    {
                        continue;
                    }
                    AddSearchItem(model, key, val);
                }
            }
            return model;
        }

        private void AddSearchItem(FilterGroup model, string key, string val)
        {
            string field = "", orGroup = "", method = "";
            var regmethod = Regex.Match(key, @"(?<=\[).*(?=])");
            method = regmethod.Success ? regmethod.Value : "";
            var regorGroup = Regex.Match(key, @"(?<={).*(?=})");
            orGroup = regorGroup.Success ? regorGroup.Value : "AND";
            var regfield = Regex.Matches(key, @"(\[.*(}|]))(?<field>.*)");
            field = regfield.Count > 0 ? regfield[0].Groups["field"].Value : "";
            if (string.IsNullOrEmpty(method) || string.IsNullOrEmpty(field)) return;
            object value = val.Trim();
            var item = new FilterRule
            {
                Field = field,
                Data = value,
                Op =method
            };
            model.GroupOp = EnumHelper.GetInstance<GropuOp>(orGroup);
            model.Rules.Add(item);
        }
    }
}