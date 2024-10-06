using CalculatorApp.Interfaces;
using CalculatorApp.Operations;
using CalculatorApp.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace CalculatorApp.Tests
{
    public class CalculatorTests
    {
        private IExpressionParser _parser;

        [SetUp]
        public void Setup()
        {
            var operations = new List<IOperation>
            {
                new Addition(),
                new Subtraction(),
                new Multiplication(),
                new Division()
            };
            _parser = new ExpressionParser(operations);
        }

        [Test]
        public void Test_Addition()
        {
            Assert.That(3, Is.EqualTo(_parser.ParseAndCalculate("1+2")));
        }

        [Test]
        public void Test_Subtraction()
        {
            Assert.That(-1, Is.EqualTo(_parser.ParseAndCalculate("1-2")));
        }

        [Test]
        public void Test_Multiplication()
        {
            Assert.That(6, Is.EqualTo(_parser.ParseAndCalculate("2*3")));
        }

        [Test]
        public void Test_Division()
        {
            Assert.That(2, Is.EqualTo(_parser.ParseAndCalculate("4/2")));
        }

        [Test]
        public void Test_ComplexExpression()
        {
            Assert.That(0, Is.EqualTo(_parser.ParseAndCalculate("1+2-3")));
        }

        [Test]
        public void Test_DivisionByZero()
        {
            Assert.Throws<DivideByZeroException>(() => _parser.ParseAndCalculate("1/0"));
        }

        [Test]
        public void Test_AdditionWithZero()
        {
            Assert.That(5, Is.EqualTo(_parser.ParseAndCalculate("5+0")));
        }

        [Test]
        public void Test_SubtractionWithZero()
        {
            Assert.That(5, Is.EqualTo(_parser.ParseAndCalculate("5-0")));
        }

        [Test]
        public void Test_MultiplicationWithZero()
        {
            Assert.That(0, Is.EqualTo(_parser.ParseAndCalculate("5*0")));
        }

        [Test]
        public void Test_NegativeNumbers()
        {
            Assert.That(-3, Is.EqualTo(_parser.ParseAndCalculate("-1-2")));
        }

        [Test]
        public void Test_NegativeAndPositiveMultiplication()
        {
            Assert.That(-10, Is.EqualTo(_parser.ParseAndCalculate("5*-2")));
        }

        [Test]
        public void Test_OrderOfOperations_AdditionAndMultiplication()
        {
            Assert.That(7, Is.EqualTo(_parser.ParseAndCalculate("1+2*3")));
        }

        [Test]
        public void Test_OrderOfOperations_SubtractionAndDivision()
        {
            Assert.That(8, Is.EqualTo(_parser.ParseAndCalculate("10-8/4")));
        }

        [Test]
        public void Test_OrderOfOperations_MultiplicationAndParentheses()
        {
            Assert.That(9, Is.EqualTo(_parser.ParseAndCalculate("(1+2)*3")));
        }

        [Test]
        public void Test_DecimalNumbers()
        {
            Assert.That(7.5, Is.EqualTo(_parser.ParseAndCalculate("5+2.5")));
        }

        [Test]
        public void Test_ComplexExpressionWithDecimals()
        {
            Assert.That(12.5, Is.EqualTo(_parser.ParseAndCalculate("10+2.5*1")));
        }

        [Test]
        public void Test_MultipleParentheses()
        {
            Assert.That(20, Is.EqualTo(_parser.ParseAndCalculate("((2+3)*4)")));
        }

        [Test]
        public void Test_LongExpression()
        {
            Assert.That(24, Is.EqualTo(_parser.ParseAndCalculate("1+2*3+4*5-6/2")));
        }

        [Test]
        public void Test_MultipleOperationsWithNegatives()
        {
            Assert.That(-9, Is.EqualTo(_parser.ParseAndCalculate("1-2*3-4")));
        }

        [Test]
        public void Test_LargeNumbers()
        {
            Assert.That(1000000000000, Is.EqualTo(_parser.ParseAndCalculate("1000000000*1000")));
        }

        [Test]
        public void Test_VeryLongExpression()
        {
            Assert.That(109, Is.EqualTo(_parser.ParseAndCalculate("10+20*3-5+2+4/2+1*0-10+50")));
        }
    }
}
