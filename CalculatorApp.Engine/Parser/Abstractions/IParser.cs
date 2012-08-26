using CalculatorApp.Engine.Operations.Abstractions;

namespace CalculatorApp.Engine.Parsers.Abstractions
{
    public interface IParser
    {
        OperatorCollection Operators { get; set; }
        IOperation Parse(string expression);
    }
}
