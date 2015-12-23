using System;
using AdvancedSearch.Mvc.Controls;
using AdvancedSearch.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedSearch.Mvc.Tests
{
    [TestClass]
    public class TestBoxTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            TextBox t=new TextBox();
            t.Name("Code").Value("AA").Equal();
            Console.WriteLine(t.ToHtmlString());
        }
        [TestMethod]
        public void TestMethod2()
        {

            TextBox t = new TextBox();
            t.Name("Code").Value("AA").SearchWay(OperatorType.Starts);
            Console.WriteLine(t.ToHtmlString());
        }
    }
}
