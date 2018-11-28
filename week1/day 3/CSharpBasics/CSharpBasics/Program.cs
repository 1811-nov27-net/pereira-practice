using System;
using System.Collections.Generic;

namespace CSharpBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //local variables and types
            int x = 0;
            double y = 3.14; //Allows decimal 64b float)
            decimal z = 5.0010m; //Even more precision - for financial etc 
            string s = "string";
            bool b = true;

            //Base class of everything is - object
            object o = true;

            // var (Compiler type inference) 
            var xyz = "hello";
            var b1 = true;
            //xyz = false; //error! no dynamic typing
            //Use only when the type is clear to the person reading the code
            // Dont use whe it obscures useful context

            //controle structures
            //loop
            for(var i=0; i<10; i++)
            {
                Console.WriteLine(i);
            }

            //needs using, like import in other languages
            List<string> list = new List<string>();
            list.Add("asdf");
            list.Add("ewqew");

            //snippet foreach
            foreach(var item in list)
            {
                //snippet cw
                Console.WriteLine(item);
            }


            //Conditionals
            do
            {
                //Do while loop
            } while (false);

            if(true)
            {
                //if
            }
            else if(false)
            {
                //else if
            }
            else
            {
                //else
            }


            //object-oriented
            //  Have objects that associate data and related behavior to represent "entities"/nouns
            //  Create thoso objects from templates called classes which define a contract for those objects at runtime

            //Part of .NET eosystem/platform

            //Strong typed (more precisely, statically types)
            //Statically typed means, variables are locked to a certain type at compile time and cannot change

            //Unified type systems
            //  "primatives" (types with value semantics instead of reference semantics)
            //  *also* inherit from object

            //Garbage collection
            //"managed" language (memory is menaged for you)
            //the runtime is responsible for freeing unused objects from memory
            //save developer time, fewer bugs, some performance panalty

            //functions are not quite first-class but in practice more or less
            // C# is somewhat functional, especially in practice with LINQ
            // LINQ (one of the best parts of C#)
            //  (language-Integrated Query Language)

            //Asynchronous programming support with TPL (Task Processing Library)
            //parallel programming support

            //exception handling
        }
    }
}
