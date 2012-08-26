using System;
using CalculatorApp.Engine.Operations.Abstractions;

namespace CalculatorApp.Engine
{
    public sealed class Operator
    {
        private Type _operationType;
        public string Symbol { get; private set; }
        public Priority Priority { get; private set; }

        private Operator(Type operationType, string symbol, Priority priority)
        {
            _operationType = operationType;
            Symbol = symbol;
            Priority = priority;
        }

        public static Operator Create<T>(string symbol, Priority priority) where T : BinaryOperation
        {
            return new Operator(typeof(T), symbol, priority);
        }

        public IOperation Compute(IOperation left, IOperation right)
        {
            var constructorInfo = _operationType.GetConstructor(new Type[] { left.GetType(), right.GetType() });
            var operation = constructorInfo.Invoke(new object[] { left, right });
            return (IOperation)operation;
        }
    }
}
