namespace CalculatorApp.Interfaces
{
    public interface IExpressionParser
    {
        double ParseAndCalculate(string expression);
    }
}