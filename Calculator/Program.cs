using CalculatorApp.Interfaces;
using CalculatorApp.Operations;
using CalculatorApp.Services;
using System.Collections.Generic;

namespace CalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var operations = new List<IOperation>
            {
                new Addition(),
                new Subtraction(),
                new Multiplication(),
                new Division()
            };

            IExpressionParser parser = new ExpressionParser(operations);
            var calculator = new Calculator(parser);

            calculator.Start();
        }
    }
}