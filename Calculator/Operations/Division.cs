using CalculatorApp.Interfaces;

namespace CalculatorApp.Operations
{
    public class Division : IOperation
    {
        public char Operator => '/';

        public double Execute(double left, double right)
        {
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return left / right;
        }
    }
}