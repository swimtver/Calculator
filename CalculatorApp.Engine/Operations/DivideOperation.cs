using System;
using CalculatorApp.Engine.Operations.Abstractions;

namespace CalculatorApp.Engine.Operations
{
    public class DivideOperation : BinaryOperation
    {
        public DivideOperation(IOperation left, IOperation right) : base(left, right) { }

        public override double Evaluate()
        {
            var right = RightArgument.Evaluate();
            if (right == 0.0) throw new DivideByZeroException("В выражении происходит деление на ноль");
            return LeftArgument.Evaluate() / right;
        }
    }
}
