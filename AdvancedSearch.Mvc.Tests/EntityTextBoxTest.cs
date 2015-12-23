using System;
using AdvancedSearch.Mvc.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedSearch.Mvc.Tests
{
    [TestClass]
    public class EntityTextBoxTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var c=new EntityTextBox<Student, string>(t=>t.StuId);
            c.Contains();
            Console.WriteLine(c.ToHtmlString());
        }
    }
    public class Student
    {
        public string StuId { get; set; }

        public string StuName { get; set; }

        public int? Nullint { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? Birthday { get; set; }

        public string LoveGril { get; set; }

    }
}