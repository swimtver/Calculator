using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using CalculatorApp.Engine.Operations;
using CalculatorApp.Engine.Operations.Abstractions;
using CalculatorApp.Engine.Parsers.Abstractions;

namespace CalculatorApp.Engine.Tests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void WhenCreateCalculatorAndAddSomeOperatorWecanUseIt()
        {
            var parser = new Mock<Parser>();
            parser.Protected().Setup<OperatorCollection>("DefaultConfiguration").Returns(new OperatorCollection());
            var calculator = new Calculator(parser.Object, x => x.Add(Operator.Create<FakeOperation>("+", Priority.Low)));//var calculator = new Calculator(_parser).Configure(x => x.Add(op));
            Assert.IsTrue(calculator.Operators.Count() > 0);
        }

        [TestMethod]
        public void IfAddOperatorWithRepeatingSymbolOldOperatorUpdatesByNew()
        {
            var parser = new Mock<Parser>();
            parser.Protected().Setup<OperatorCollection>("DefaultConfiguration").Returns(new OperatorCollection());
            var calculator = new Calculator(parser.Object, x => x.Add(Operator.Create<FakeOperation>("+", Priority.Low))
                                                                 .Add(Operator.Create<FakeOperation>("+", Priority.Middle)));
            Assert.IsTrue(calculator.Operators.Count() == 1);
            Assert.IsTrue(calculator.Operators.First().Priority == Priority.Middle);
        }

        [TestMethod]
        public void OperatorCanCreateBinaryOperationInstance()
        {
            var left = new Mock<IOperation>().Object;
            var right = new Mock<IOperation>().Object;
            var op = Operator.Create<FakeOperation>("+", Priority.Low);
            var operation = op.Compute(left, right);
            Assert.IsInstanceOfType(operation, typeof(FakeOperation));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IfExpressionIsNullOrEmptyThrowException()
        {
            var parser = new Mock<IParser>().Object;
            var expression = String.Empty;
            var calculator = new Calculator(parser);
            calculator.Calculate(expression);
        }

        [TestMethod]
        public void SumOfTwoNumbersTest()
        {
            var expression = "2+1";
            var parser = new Mock<IParser>();
            parser.Setup(m => m.Parse(It.IsAny<string>())).Returns(new AddOperation(new AtomOperation(2), new AtomOperation(1)));
            var calculator = new Calculator(parser.Object);
            double result = calculator.Calculate(expression);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void ThreeNumbersExpressionWithDifferentPriorityOperationsTest()
        {
            var expression = "5-2*2";
            var parser = new Mock<IParser>();
            parser.Setup(m => m.Parse(It.IsAny<string>()))
                  .Returns(new SubtractOperation(new AtomOperation(5), new MultiplicateOperation(new AtomOperation(2), new AtomOperation(2))));
            var calculator = new Calculator(parser.Object);
            double result = calculator.Calculate(expression);
            Assert.AreEqual(1, result);
        }
    }
}