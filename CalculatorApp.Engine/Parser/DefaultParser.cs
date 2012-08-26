using System;
using System.Linq;
using CalculatorApp.Engine.Operations;
using CalculatorApp.Engine.Operations.Abstractions;
using CalculatorApp.Engine.Parsers.Abstractions;

namespace CalculatorApp.Engine.Parsers
{
    public sealed class DefaultParser : Parser
    {
        private readonly string NUMBER_SEPARATOR = System.Globalization.NumberFormatInfo.CurrentInfo.PercentDecimalSeparator;
        private string RESERVED_DELIMETERS = "()";

        public DefaultParser() : base() { }

        protected override OperatorCollection DefaultConfiguration()
        {
            return new OperatorCollection()
                .Add(Operator.Create<AddOperation>("+", Priority.Low))
                .Add(Operator.Create<SubtractOperation>("-", Priority.Low))
                .Add(Operator.Create<MultiplicateOperation>("*", Priority.Middle))
                .Add(Operator.Create<DivideOperation>("/", Priority.Middle));
        }

        public override IOperation Parse(string expression)
        {
            if (String.IsNullOrWhiteSpace(expression)) throw new ArgumentNullException("expression");
            var operation = Parse(ref expression, Priority.Low);
            if (!String.IsNullOrWhiteSpace(expression)) throw new ArgumentException("Ошибка синтаксиса");
            return operation;
        }

        private IOperation Parse(ref string expression, Priority priority)
        {
            if (priority == Priority.Highest)
            {
                return GetOperationUnit(ref expression);
            }
            else
            {
                var nextPriority = priority + 1;
                var result = Parse(ref expression, nextPriority);
                var token = PeekToken(expression);
                var op = Operators.GetOperator(token);
                while (op != null && op.Priority == priority)
                {
                    token = TakeToken(ref expression);
                    var operation = Parse(ref expression, nextPriority);
                    result = op.Compute(result, operation);
                    op = Operators.GetOperator(PeekToken(expression));
                }
                return result;
            }
        }

        private IOperation GetOperationUnit(ref string expression)
        {
            var token = TakeToken(ref expression);
            if (token == "(")
            {
                var expressionPart = GetExpressionInBraces(ref expression);
                return Parse(expressionPart);
            }
            double number;
            if (double.TryParse(token, out number))
                return new AtomOperation(number);
            throw new ArgumentException("Ошибка синтаксиса.");
        }

        private string GetExpressionInBraces(ref string expression)
        {
            if (String.IsNullOrEmpty(expression)) throw new ArgumentException("Не хватает скобки.");
            var bracesCount = 1;
            var index = 0;
            foreach (var c in expression)
            {
                if (c == ')') bracesCount--;
                if (c == '(') bracesCount++;
                if (bracesCount == 0 || index == expression.Length - 1) break;
                index++;
            }
            if (expression[index] != ')') throw new ArgumentException("Не хватает скобки.");
            var expressionPart = expression.Substring(0, index);
            expression = expression.Remove(0, index + 1);
            return expressionPart;
        }

        private string PeekToken(string expression)
        {
            return GetNextToken(expression);
        }

        private string TakeToken(ref string expression)
        {
            var token = GetNextToken(expression);
            expression = expression.Remove(0, token.Length);
            return token;
        }

        private string GetNextToken(string expression)
        {
            var token = string.Empty;
            var index = 0;
            if (expression.Length == 0) return token;
            while (index < expression.Length && Char.IsWhiteSpace(expression[index])) ++index;
            if (IsDelimiter(expression[index]))
            {
                token += expression[index];
            }
            else if (Char.IsDigit(expression[index]))
            {
                while (IsNumberPart(expression[index]))
                {
                    token += expression[index];
                    index++;
                    if (index >= expression.Length) break;
                }
            }
            else
            {
                if (NUMBER_SEPARATOR.Equals(expression[index].ToString()))
                    throw new ArgumentException("Разделитель числа стоит не на месте");
                else
                    throw new ArgumentException(string.Format("Неизвестный символ:{0}", expression[index].ToString()));
            }
            return token;
        }

        private bool IsNumberPart(char c)
        {
            return Char.IsDigit(c) || NUMBER_SEPARATOR.Equals(c.ToString());
        }

        private bool IsDelimiter(char c)
        {
            return RESERVED_DELIMETERS.IndexOf(c) != -1 ||
                   Operators.Any(x => x.Symbol.Equals(c.ToString()));
        }
    }
}
