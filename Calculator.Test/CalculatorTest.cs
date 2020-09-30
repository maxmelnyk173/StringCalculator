using NUnit.Framework;

namespace Calculator.Test
{
    public class Tests
    {
        [TestCase(7, "1+2*3")]
        [TestCase(11, "1+2*(3+2)")]
        [TestCase(15, "2+15/3+4*2")]

        public void CalculatePostfixExpression_AreEqual_Test(int expected, string actual)
        {
           
            CalculatePostfix calc = new CalculatePostfix();
            var strToPostfix = calc.GetExpression(actual);

            Assert.AreEqual(expected, calc.Counting(strToPostfix));
        }
    }
}