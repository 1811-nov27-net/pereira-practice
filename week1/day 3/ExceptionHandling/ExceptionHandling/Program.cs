using System;

namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Exceptions are runtime errors that we can potencially handle
             * exceptions are obects and defined by classes like anything else
             */

            try
            {
                BadCode();


                //trye goes around code that is expected to thorw an exception
                var x = 4;
                var y = x / 0;

                Console.WriteLine("Never prints because excpetion is thrown first");

                throw new ArgumentNullException();
            }
            catch(DivideByZeroException e)
            {
                //handle the exception in catch block
                Console.WriteLine("divided by zero, moving on");
                //at the end of catch, we move on with business
            }
            catch(InvalidCastException e)
            {
                Console.WriteLine("handled bad cast");

                throw; //Re-throws the current exception
                //only works inside catch
            }

            Console.WriteLine("the program continues");
        }

        static void BadCode()
        {
            try
            {
                object o = true;
                string s = (string)o;
            }
            catch(DivideByZeroException e)
            {
                Console.WriteLine("won't print beacause this isn't a fiv by 0");
            }

            Console.WriteLine("won't print");
        }
    }
}
