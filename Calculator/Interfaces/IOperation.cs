namespace CalculatorApp.Interfaces
{
    public interface IOperation
    {
        char Operator { get; }
        double Execute(double left, double right);
    }
}