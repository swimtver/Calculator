namespace CalculatorApp.Engine.Operations.Abstractions
{
    public abstract class BinaryOperation : IOperation
    {
        protected IOperation LeftArgument { get; set; }
        protected IOperation RightArgument { get; set; }

        protected BinaryOperation(IOperation left, IOperation right)
        {
            LeftArgument = left;
            RightArgument = right;
        }

        public abstract double Evaluate();
    }
}
