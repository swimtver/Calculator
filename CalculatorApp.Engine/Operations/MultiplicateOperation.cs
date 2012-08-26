using CalculatorApp.Engine.Operations.Abstractions;

namespace CalculatorApp.Engine.Operations
{
    public class MultiplicateOperation : BinaryOperation
    {
        public MultiplicateOperation(IOperation left, IOperation right) : base(left, right) { }

        public override double Evaluate()
        {
            return LeftArgument.Evaluate() * RightArgument.Evaluate();
        }
    }
}
