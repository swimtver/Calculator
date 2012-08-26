using CalculatorApp.Engine.Operations.Abstractions;

namespace CalculatorApp.Engine.Operations
{
    public class AtomOperation : IOperation
    {
        public double Argument { get; set; }

        public AtomOperation(double argument)
        {
            Argument = argument;
        }

        public double Evaluate()
        {
            return Argument;
        }
    }
}
