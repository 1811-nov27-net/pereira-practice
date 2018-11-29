using System;
using System.Collections.Generic;
using System.Text;

namespace LinqAndTestingLibrary
{
    //In c#, there's regular parameters - maybe a method Add can accept 2 and 2, 5 and 3, 1 and 0, it can accept many values
    //There's also "typed parameters" which means, a class or a mthod can work with different types withous being a whole new class/methods

    //type parameters aka generics

    //The way we do type parameters is with angle brackets <type>
    //Some, like Dictionary, take more than one e.g. Dictionary<string, int>

    //readonly just means I can't reassign "_list" to a different object later. it doesn't mean i can't modify the object with its methods

    // Make class generic with angle brackets in its definition this defines a type parameters in that class. by convention, - call it "T" if there's
    // only one. (stands for type)

    //generic / type-parameter constraints
    //    you can required that it is derived from some type (class, interface)
    //          MyGenerictCollection<T> where T : Sometime
    //    you can required that it be a struct
    //          MyGenerictCollection<T> where T : struct
    //    you can required that it hava a default constructor
    //          MyGenerictCollection<T> where T : new()
    //  can have more 
    public class MyGenerictCollection<T> where T : new()
    {
        protected List<T> _list = new List<T>();

        //we don't know what T is, it could be anything, so this member will have a different type for every instance of MyGenericCollection


        publuc function addDefaultValue()
        {
            _list.Add(new T());
        }

        public virtual void Add(T item)
        {
            _list.Add(item);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void Sort()
        {
            _list.Sort();
        }
    }
