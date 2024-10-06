using CalculatorApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CalculatorApp.Services
{
    public class ExpressionParser : IExpressionParser
    {
        private readonly IEnumerable<IOperation> _operations;
        private readonly Dictionary<char, int> _precedence;

        public ExpressionParser(IEnumerable<IOperation> operations)
        {
            _operations = operations;

            // приоритет операций
            _precedence = new Dictionary<char, int>
            {
                { '+', 1 },
                { '-', 1 },
                { '*', 2 },
                { '/', 2 },
                { '(', 0 },
                { ')', 0 }
            };
        }

        public double ParseAndCalculate(string expression)
        {
            var postfix = ConvertToPostfix(expression);
            return EvaluatePostfix(postfix);
        }

        // Конвертация в постфиксную запись (обратная польская нотация)
        private Queue<string> ConvertToPostfix(string expression)
        {
            var output = new Queue<string>();
            var operators = new Stack<char>();
            string number = "";
            bool expectUnary = true;

            foreach (var ch in expression)
            {
                if (char.IsDigit(ch) || ch == '.')
                {
                    number += ch;
                    expectUnary = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(number))
                    {
                        output.Enqueue(number);
                        number = "";
                    }

                    if (ch == '(')
                    {
                        operators.Push(ch);
                        expectUnary = true;
                    }
                    else if (ch == ')')
                    {
                        while (operators.Count > 0 && operators.Peek() != '(')
                        {
                            output.Enqueue(operators.Pop().ToString());
                        }
                        operators.Pop();
                        expectUnary = false;
                    }
                    else if (_operations.Any(op => op.Operator == ch))
                    {
                        if (ch == '-' && expectUnary)
                        {
                            // Унарный минус
                            number += '-';
                            expectUnary = false;
                            continue;
                        }

                        while (operators.Count > 0 && _precedence[ch] <= _precedence[operators.Peek()])
                        {
                            output.Enqueue(operators.Pop().ToString());
                        }
                        operators.Push(ch);
                        expectUnary = true;
                    }
                }
            }

            if (!string.IsNullOrEmpty(number))
                output.Enqueue(number);

            while (operators.Count > 0)
            {
                output.Enqueue(operators.Pop().ToString());
            }

            return output;
        }

        private double EvaluatePostfix(Queue<string> tokens)
        {
            var stack = new Stack<double>();

            while (tokens.Count > 0)
            {
                var token = tokens.Dequeue();

                if (double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out double number))
                {
                    stack.Push(number);
                }
                else
                {
                    if (stack.Count < 2)
                        throw new InvalidOperationException("Not enough values in the stack for the operation.");

                    var right = stack.Pop();
                    var left = stack.Pop();
                    var operation = _operations.First(op => op.Operator == token[0]);

                    stack.Push(operation.Execute(left, right));
                }
            }

            if (stack.Count != 1)
                throw new InvalidOperationException("The stack should contain exactly one value at the end of evaluation.");

            return stack.Pop();
        }


    }
}