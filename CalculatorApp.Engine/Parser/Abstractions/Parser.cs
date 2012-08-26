using CalculatorApp.Engine.Operations.Abstractions;

namespace CalculatorApp.Engine.Parsers.Abstractions
{
    public abstract class Parser : IParser
    {
        public OperatorCollection Operators { get; set; }

        protected Parser()
        {
            Operators = DefaultConfiguration();
        }

        protected abstract OperatorCollection DefaultConfiguration();

        public abstract IOperation Parse(string expression);
    }

}
