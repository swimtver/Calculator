using System;
using CalculatorApp.Engine.Operations.Abstractions;

namespace CalculatorApp.Engine.Tests
{
    class FakeOperation : BinaryOperation
    {
        public FakeOperation(IOperation left, IOperation right) : base(left, right) { }

        public override double Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
