using System;
using CalculatorApp.Engine.Parsers.Abstractions;

namespace CalculatorApp.Engine
{
    public class Calculator
    {
        private readonly IParser _parser;
        public OperatorCollection Operators { get { return _parser.Operators; } }

        public Calculator(IParser parser)
        {
            _parser = parser;
        }

        public Calculator(IParser parser, Func<OperatorCollection, OperatorCollection> func)
            : this(parser)
        {
            _parser.Operators = func(_parser.Operators);
        }

        public double Calculate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression)) throw new ArgumentNullException("строка выражения пустая", "expression");
            var operation = _parser.Parse(expression);
            return operation.Evaluate();
        }
    }
}
