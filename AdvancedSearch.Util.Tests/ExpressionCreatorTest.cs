using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedSearch.Util.Tests
{
    /// <summary>
    /// 表达式构造测试类
    /// </summary>
    [TestClass]
    public class ExpressionCreatorTest
    {
        /// <summary>
        /// 生成参数表达式方法测试
        /// </summary>
        [TestMethod]
        public void CreateParameterExpressionTest()
        {
            Assert.AreEqual("c", Util.ExpressionCreator<Human>.CreateParameterExpression("c").ToString());
        }
        /// <summary>
        /// 生成属性表达式方法测试
        /// </summary>
        [TestMethod]
        public void CreatePropertyExpressionTest()
        {
            Assert.AreEqual("c.Name", Util.ExpressionCreator<Human>.CreatePropertyExpression(ExpressionCreator<Human>.CreateParameterExpression("c"), "Name").ToString());
        }
        [TestMethod]
        public void CreateConstantExpressionTest()
        {
            var cc =
                Util.ExpressionCreator<Human>.CreatePropertyExpression(
                    ExpressionCreator<Human>.CreateParameterExpression("c"), "Name");
            //Assert.AreEqual("AA",ExpressionCreator<Human>.CreateConstantExpression(cc,"AA,BB") .ToString());
            Console.WriteLine(ExpressionCreator<Human>.CreateConstantExpression(cc, "AA,BB").ToString());
        }
        [TestMethod]
        public void ExpressionOperatorDictTest()
        {
            var cc =
                Util.ExpressionCreator<Human>.CreatePropertyExpression(
                    ExpressionCreator<Human>.CreateParameterExpression("c"), "Name");
            //Assert.AreEqual("AA",ExpressionCreator<Human>.CreateConstantExpression(cc,"AA,BB") .ToString());
            Console.WriteLine(ExpressionCreator<Human>.CreateConstantExpression(cc, "AA,BB").ToString());
        }
    }

    public class Human
    {
        public string Name { get; set; }
    }
}
