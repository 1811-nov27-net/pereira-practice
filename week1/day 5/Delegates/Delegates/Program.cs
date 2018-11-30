using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Linq();


            //Object initialization syntax
            //If no parens after MoviePLayer, zero arg constructor "()" is assumed
            var player = new MoviePlayer
            {
                CurrentMovie = "Lord of the Rings: The Followship of Rings Extended Edition"
            };

            /*
             * The function must have a compatible signature with the delegate of the handler
             */
            MoviePlayer.MovieFinishedHandler handler = EjectDisc;

            //Subscribe to events with +=
            player.MovieFinished += handler;
            //Ubsubscribe with -=
            //player.MovieFinished -= handler;


            /*
             * When C# got generics, they added Func and Actions generic classes. and We can use these instead of delegate types
             * Action is for void returns
             * Func is for non-void returns
             */
            Action<string> handler2 = EjectDisc;

            //player.MovieFinished += handler2;

            //Labda expressions
            player.MovieFinished += s => Console.WriteLine("lamba subscribe");
            /*
             * This lambda takes 
             */

            player.PlayMovie();

            //function taking int and string, returning bool
            Func<bool> func2 = () => false;
            //The last type parameters is the return type, and the ones before it are the arguments

            //Function taking three arguments, returning void
            Action<int, string, bool> func3 = (num, str, b) => 
            {
                if(b)
                {
                    Console.Write(num);
                    Console.Write(str);
                }
            };

            Action<bool> func4 = b => { return; };
        }

        static void EjectDisc()
        {
            Console.WriteLine("Ejecting disc.");
        }

        static void EjectDisc(string title)
        {
            Console.WriteLine($"Ejecting disc {title}.");
        }

        /*
         * Having two methods with the same name, but different arguments
         * is allowed, this is called method overloading.
         * It's not a problema becaus c# knows what method you are calling
         */


        static void Linq()
        {
            var x = new List<string>();
            //i want to know the max lenght of those strings
            int longest = x.Max(s => s.Length);

            /*
             * LINQ methods we should know
             *      Select (mapping opperatinos
             */
            var listOfFirstCharacters = x.Select(s => s[0]);
            //AVERAGE, MIN, MAX
            //ALL (Expects a bool returning labda - checks that all elements meet some condition)
            bool allShortThen5Chars = x.All(s => s.Length < 5);
            //ANY (work like All, but return true if there's ANY match, not ALL matches
            //Where (filters the sequence for only the elements that return true)
            var onlyTheLongElements = x.Where(s => s.Length > 20);

            //Should also know that you can chains theses together
            bool b = x.Where(s => s.Length > 20)
                        .Select(s => s[0])
                        .All(c => c == 'a' || c == 'b');
            //b will be true if every long element starts with a or b

            //Remember, LINQ used "deferred execution" meaning it does'nt actually "run the loop" until will need the result7
            List<char> listOfChars = listOfFirstCharacters.ToList();
            // ToList - is a LINQ method that will actually run the loop (if necessary) and return you a proper list
            // All IEnumerables can do is get put in foreach loops and have LINQ methods called on it
        }

        static void Finally()
        {
            try
            {
                //this code run always
                Console.WriteLine("try");
                //until an exception in here

                //If i'm opening resources that need to be cleaned up
                
                //don't put cleanup code here beause exception beforehand might skip it
            }
            catch(ArgumentException e)
            {
                //this code runs when there is a matching exception from inside the try block

                //only put ArgumentException-specific cleanup here
            }
            finally
            {
                //This code runs always, period. Even if there was a uncaught exception in the try block

                //put general cleanup of "try" resources here
            }
               //Don't put cleanup here, because uncaught exceptions will skip it
      

            //We can even have try and finally without any catch
            //if you are using resources that you must clean up
            //but any erros really still needs to propagate up
            //because you can't actually handle it

            //There is a "using statement" syntax that can replace try-finally sometimes.
        }
    }
}
