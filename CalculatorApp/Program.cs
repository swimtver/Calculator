using System;
using CalculatorApp.Engine;
using CalculatorApp.Engine.Parsers;

namespace CalculatorApp
{
    class Program
    {
        static void Main()
        {
            var calculator = new Calculator(new DefaultParser(), x => x.Add(Operator.Create<ModOperation>("%", Priority.Middle)));
            for (; ; )
            {
                Console.Write(">");
                var expression = Console.ReadLine();
                if ("q" == expression) break;
                string result = String.Empty;
                try
                {
                    result = calculator.Calculate(expression).ToString();
                }
                catch (ArgumentNullException ex)
                {
                    result = ex.Message;
                }
                catch (ArgumentException ex)
                {
                    result = ex.Message;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
                Console.WriteLine("> {0}", result);
            }
        }
    }
}
