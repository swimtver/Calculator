using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorApp.Engine.Operations;
using CalculatorApp.Engine.Parsers;
using CalculatorApp.Engine.Parsers.Abstractions;

namespace CalculatorApp.Engine.Tests
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ParseAtomOperation()
        {
            const string expression = "63";
            IParser parser = new DefaultParser();

            var operation = parser.Parse(expression);
            Assert.IsInstanceOfType(operation, typeof(AtomOperation));
            Assert.AreEqual(63, operation.Evaluate());
        }        

        [TestMethod]
        public void GetAddOperationTypeAndRightResultWhenParseAddExpressions()
        {
            const string expression = "6+3";
            const string expression2 = "6+3+2";
            IParser parser = new DefaultParser();

            var operation = parser.Parse(expression);
            Assert.IsInstanceOfType(operation, typeof(AddOperation));
            Assert.AreEqual(9, operation.Evaluate());

            var operation2 = parser.Parse(expression2);
            Assert.IsInstanceOfType(operation2, typeof(AddOperation));
            Assert.AreEqual(11, operation2.Evaluate());
        }

        [TestMethod]
        public void GetSubtractOperationTypeAndRightResultWhenParseSubtractExpression()
        {
            const string expression = "6-3";
            const string expression2 = "6-3-2";
            IParser parser = new DefaultParser();

            var operation = parser.Parse(expression);
            Assert.IsInstanceOfType(operation, typeof(SubtractOperation));
            Assert.AreEqual(3, operation.Evaluate());

            var operation2 = parser.Parse(expression2);
            Assert.IsInstanceOfType(operation2, typeof(SubtractOperation));
            Assert.AreEqual(1, operation2.Evaluate());
        }

        [TestMethod]
        public void AddAndSubtructOperationsCalulate()
        {
            const string expression = "(6-3-5)";
            const string expression2 = "6-(3-2)+5";
            const string expression3 = "(6+5)-(3-2)";
            const string expression4 = "6-(2*(3-2))";
            IParser parser = new DefaultParser();

            var operation = parser.Parse(expression);
            var operation2 = parser.Parse(expression2);
            var operation3 = parser.Parse(expression3);
            var operation4 = parser.Parse(expression4);

            Assert.AreEqual(-2, operation.Evaluate());
            Assert.AreEqual(10, operation2.Evaluate());
            Assert.AreEqual(10, operation3.Evaluate());
            Assert.AreEqual(4, operation4.Evaluate());
        }

        [TestMethod]
        public void GetMultipleOperationTypeAndRightResultWhenParseMultipleExpression()
        {
            const string expression = "6*3";
            IParser parser = new DefaultParser();
            var operation = parser.Parse(expression);
            Assert.IsInstanceOfType(operation, typeof(MultiplicateOperation));
            Assert.AreEqual(18, operation.Evaluate());
        }

        [TestMethod]
        public void GetAddOperationTypeAndRightResultWhenParseExpressionWithAddAndMultipleOperations()
        {
            const string expression = "2+6*3";
            const string expression2 = "6*3+2";
            IParser parser = new DefaultParser();

            var operation = parser.Parse(expression);
            Assert.IsInstanceOfType(operation, typeof(AddOperation));
            Assert.AreEqual(20, operation.Evaluate());

            var operation2 = parser.Parse(expression2);
            Assert.IsInstanceOfType(operation2, typeof(AddOperation));
            Assert.AreEqual(20, operation2.Evaluate());
        }

        [TestMethod]
        public void BracesInExpressionHaveHighestPriority()
        {
            const string expression = "(2+6)*3";
            const string expression2 = "6*(3+2)";
            IParser parser = new DefaultParser();

            var operation = parser.Parse(expression);
            Assert.IsInstanceOfType(operation, typeof(MultiplicateOperation));
            Assert.AreEqual(24, operation.Evaluate());

            var operation2 = parser.Parse(expression2);
            Assert.IsInstanceOfType(operation2, typeof(MultiplicateOperation));
            Assert.AreEqual(30, operation2.Evaluate());
        }

        [TestMethod]
        public void MultipleAndDivideOperationsCalulate()
        {
            const string expression = "6*5/3";
            const string expression2 = "50/5/5";
            const string expression3 = "(4*4)-(3*3)";
            const string expression4 = "50/(5/5)";
            IParser parser = new DefaultParser();

            var operation = parser.Parse(expression);
            var operation2 = parser.Parse(expression2);
            var operation3 = parser.Parse(expression3);
            var operation4 = parser.Parse(expression4);

            Assert.AreEqual(10, operation.Evaluate());
            Assert.AreEqual(2, operation2.Evaluate());
            Assert.AreEqual(7, operation3.Evaluate());
            Assert.AreEqual(50, operation4.Evaluate());
        }
    }
}
