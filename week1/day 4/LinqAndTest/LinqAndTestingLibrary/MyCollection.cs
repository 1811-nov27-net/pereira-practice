using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqAndTestingLibrary
{
    /// <summary>
    /// List with some extra helpers methods
    /// </summary>
    /// <remarks>
    /// Two strategies we could use to leverage the built-in collection classes:
    /// - Inheritance (MyCollection is a List)
    /// - composition (MyCollection has a List)
    /// 
    /// Make class generic with angle brackets in its definition this defines a type parameters in that class. by convention, - call it "T" if there's
    /// only one. (stands for type)
    /// </remarks>
    public class MyCollection : MyGenerictCollection<string>
    {
        //Now that we derive from Mygeneric Collect we dont need this
        //private List<T> _list = new List<T>();

        /*
         * Every class have at least one default constructor
         * if you don't define one, it has a default constructor without any parameters
         * 
         *      public MyCollection(){}
         * 
         * but , as soon as you define any constructor, that default one will not be added
         */

        //Virtual and override example whithou changing the real method
        public override void Add(string item)
        {
            _list.Add(item);
        }

        //property withou a "set"
        //calling code can say coll.Length instead of coll.getLength()
        public int Length {
            get
            {
                return _list.Count;
            }
        }

        public string Get(int index)
        {
            return _list[index];
        }

        public string Longest()
        {
            int longestLenght = -1;
            string longest = null;

            foreach(var item in _list)
            {
                if(item != null && item.Length > longestLenght)
                {
                    longestLenght = item.Length;
                    longest = item;
                }
            }

            return longest;
        }

        public double averageLength()
        {
            return _list.Average(x => x.Length);
        }

        //Return list of all lenghts of member
        public IEnumerable<int> Lengths()
        {
            return _list.Select(x => x.Length);
        }

        //Return number of elements that starts with an "a"
        public int NumberOfA()
        {
            //Count the number of elements matching some conditions
            return _list.Count(x => (x != null && x.Length > 0 && x[0] == 'a'));

            //we are using lambda expressions
            //wich are like methods but you can pass them as parameters and assign then to varaibles
        }

        private static bool ContainsVowel(string s)
        {
            //Lambda expressions are convertible to "Func" or "Action" types, so they can be assigned as variables like this
            Func<char, bool> isVowel = (c => "AEIOUaeiou".Contains(c));

            //true if, for any element, it is true that...
            return s.Any(
                //this string of vowels contains it
                isVowel
            );
        }

        public int NumerWithVowels()
        {
            return _list.Count(ContainsVowel);
        }

        //returns first member in sorted order
        //LINQ  (and IEnumerables) uses "deferred" execution]
        public string FirsAlphabetical()
        {
            //Orderby will sort the sequence by some "key"
            //and "x => x" means, sort the strings using regular string sort
            IEnumerable<string> sorted = _list.OrderBy(x => x);
            //We haven't actually sorted the list in any way
            //or iterated over it yet
            // --- only set up how we Well iterate, when we need the values
            var first = sorted.First();
            //that method call actually ran the sort, and then discarded everything but the first entry;
            return first;
        }
    }
}
