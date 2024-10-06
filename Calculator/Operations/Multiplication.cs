using CalculatorApp.Interfaces;

namespace CalculatorApp.Operations
{
    public class Multiplication : IOperation
    {
        public char Operator => '*';

        public double Execute(double left, double right)
        {
            return left * right;
        }
    }
}