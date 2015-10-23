using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "john";
            Console.WriteLine($"my name is {{{name}}}");

            //var mine = new MyClass();

            //var x = mine?.NestedAgain?.NestedAgain?.NestedAgain;

            //Console.WriteLine(nameof(mine.NestedAgain.NestedAgain));


            Console.WriteLine("1");
            try
            {
                Console.WriteLine("2");
                try
                {
                    Console.WriteLine("3");

                    throw new InvalidOperationException("You failed");
                }
                catch (Exception e) when (FailedMessage(e))
                {
                    Console.WriteLine("Inner catch");
                }
                finally
                {
                    Console.WriteLine("FINALLY");
                }
            }
            catch (Exception ex) when (FailedMessage(ex))
            {
                Console.WriteLine("Don't get here");
            }
            catch
            {
                Console.WriteLine("Caught again");
            }

            Console.ReadLine();
        }

        private static bool FailedMessage(Exception ex)
        {
            Console.WriteLine("FAILED");
            throw new ArgumentNullException();
            return ex.Message.Contains("failed");
        }
    }

    public class MyClass
    {
        public MyClass NestedAgain { get; set; }

        public override string ToString() => $"#{NestedAgain.GetHashCode()}";
    }
}
