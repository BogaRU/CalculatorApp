using CalculatorApp.Interfaces;

namespace CalculatorApp.Operations
{
    public class Subtraction : IOperation
    {
        public char Operator => '-';

        public double Execute(double left, double right)
        {
            return left - right;
        }
    }
}