using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CalculatorApp.Engine
{
    public class OperatorCollection : IEnumerable<Operator>
    {
        private ICollection<Operator> _operators;
        public OperatorCollection() : this(new Collection<Operator>()) { }
        public OperatorCollection(ICollection<Operator> operators)
        {
            _operators = operators;
        }

        public OperatorCollection Add(Operator newOperator)
        {
            if (newOperator != null)
            {
                var op = GetOperator(newOperator.Symbol);
                if (op != null)
                    _operators.Remove(op);
                _operators.Add(newOperator);
            }
            return this;
        }

        public OperatorCollection Clear()
        {
            _operators.Clear();
            return this;
        }

        public int Count()
        {
            return _operators.Count;
        }

        public Operator GetOperator(string symbol)
        {
            return _operators.FirstOrDefault(x => x.Symbol.Equals(symbol));
        }

        public IEnumerator<Operator> GetEnumerator()
        {
            return _operators.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _operators.GetEnumerator();
        }        
    }
}
