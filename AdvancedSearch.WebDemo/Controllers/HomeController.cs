using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using AdvancedSearch.Util.Filter;
using AdvancedSearch.WebDemo.Model;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AdvancedSearch.WebDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string Query(string filter, FilterGroup fg)
        {
            FilterGroup filterGroup = ConvertToObject<FilterGroup>(filter ?? "");
            string whereSql = FilterTranslator.ToSql(filterGroup);
            Expression<Func<Student, bool>> whereLambda = FilterTranslator.ToLambdaExpression<Student>(filterGroup??fg);
            return ToJson(new { rows = MockStudents().Where(whereLambda), total = 1 });
        }

        private IQueryable<Student> MockStudents()
        {
            IList<Student> stulist = Builder<Student>.CreateListOfSize(321).TheFirst(44)
                .With(x => x.StuName = "hy").And(x => x.Nullint = 1)
                .And(x => x.LoveGril = "LILI").And(x => x.CreateTime = new DateTime(2012, 02, 03))
                .And(x => x.Birthday = new DateTime(2012, 09, 01))
                .And(x => x.Stuclass = new StuClass() { ClassId = "2", ClassName = "二班" })
                .TheNext(33).With(x => x.StuName = "wlf").And(x => x.Nullint = 2).And(x => x.LoveGril = "MM")
                .And(x => x.CreateTime = new DateTime(2012, 06, 06)).And(x => x.Birthday = new DateTime(2012, 09, 010))
                .And(x => x.Stuclass = new StuClass() { ClassId = "1", ClassName = "一班" }).TheNext(244).And(x => x
                .Stuclass = new StuClass() { ClassId = "3", ClassName = "三班" }).Build();

            return stulist.AsQueryable(); //模拟EF context 假设数据库里原数据为200条 
        }
        #region 序列化和反序列化
        public static T ConvertToObject<T>(string json)
        {
            var jsetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.DeserializeObject<T>(json, jsetting);
        }
        /// <summary>
        /// 将object对象进行序列化
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToJson(object t)
        {
            var jsetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateFormatString = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(t, Formatting.Indented, jsetting);
        } 
        #endregion
    }
    public class Cat
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime CreateTime { get; set; }
    }
}