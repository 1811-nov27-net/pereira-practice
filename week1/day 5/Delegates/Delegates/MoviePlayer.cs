using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Delegates
{
    public class MoviePlayer
    {
        /*
         * C# supports some things called delegates and events.
         * The idea here is, i can write a class that expects it's consumer
         * to actualli "inject" behavior into it for it to use
         * 
         * This enables some polymorphism, you can wrtie classes that suppoer a lot
         * of differnt behavior to be decided by other code.
         */
        public string CurrentMovie { get; set; }

        /*
         * Thi delegate type can hold any function with zero parameters and void return
         */
        //public delegate void MovieFinishedHandler();
        //Return type /^                       ^\ Zero arguments

        /*
         * Now, any variable of type "MovieFinishedHandler" can hold zero-arg functions with void return
         */

        /*
         * Delegate is 
         */
        public delegate void MovieFinishedHandler(string title);


        /*
         * An event is something you can subscribe to with any number of functions.
         * When event is "called" as if it itself were a functionm
         * all subscribing functions are called
         */

        /*
         * events delegates always should be return type void
         */

        /*
         * You need a delegate type 
        */
        //public event MovieFinishedHandler MovieFinished;
        public event MovieFinishedHandler MovieFinished;

        public void PlayMovie()
        {
            Thread.Sleep(3000); //Wait 3 seconts
            Console.WriteLine($"Finished movie {CurrentMovie}");

            //We'll fire an event when the movies is finished
            //and any code usign thi movie player
            //can subscribe to that event with whatever function/code they want

            //Have to check the events are not null before firing them. Events withous subscriber are == null
            if (MovieFinished != null)
            {
                //MovieFinished();
                MovieFinished(CurrentMovie);
            }
            //or use null condition operator
            //?. does a null check on the left hand side first, and if the left hand side is null, it'll do nothing
            //just like syntax sugar
            //      X?.Y?.Z?.A
            //MovieFinished?.Invoke();
        }
    }
}
