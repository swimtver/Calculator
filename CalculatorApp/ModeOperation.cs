using CalculatorApp.Engine.Operations.Abstractions;

namespace CalculatorApp
{
    class ModOperation : BinaryOperation
    {
        public ModOperation(IOperation left, IOperation right) : base(left, right) { }
        public override double Evaluate()
        {
            return LeftArgument.Evaluate() % RightArgument.Evaluate();
        }
    }
}
