using CalculatorApp.Engine.Operations.Abstractions;

namespace CalculatorApp.Engine.Operations
{
    public class AddOperation : BinaryOperation
    {
        public AddOperation(IOperation left, IOperation right)
            : base(left, right)
        {
            LeftArgument = left;
            RightArgument = right;
        }

        public override double Evaluate()
        {
            return LeftArgument.Evaluate() + RightArgument.Evaluate();
        }
    }
}
