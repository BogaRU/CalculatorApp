using CalculatorApp.Interfaces;
using System;
using System.Collections.Generic;

namespace CalculatorApp.Services
{
    public class Calculator
    {
        private readonly IExpressionParser _parser;

        public Calculator(IExpressionParser parser)
        {
            _parser = parser;
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("Введите выражение (или 'exit' для выхода):");
                var input = Console.ReadLine();

                if (input.ToLower() == "exit")
                    break;

                try
                {
                    var result = _parser.ParseAndCalculate(input);
                    Console.WriteLine($"Результат: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}