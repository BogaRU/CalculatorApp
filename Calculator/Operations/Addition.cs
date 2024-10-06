using CalculatorApp.Interfaces;

namespace CalculatorApp.Operations
{
    public class Addition : IOperation
    {
        public char Operator => '+';

        public double Execute(double left, double right)
        {
            return left + right;
        }
    }
}