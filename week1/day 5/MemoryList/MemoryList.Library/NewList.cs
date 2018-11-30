using System;
using System.Collections;
using System.Collections.Generic;

namespace MemoryList.Library
{
    public class NewList : List<string>
    {
        Tracker tracker = new Tracker();

        public void Add(string s)
        {
            base.Add(s); //Calls the parent class's implementations
            tracker.logging($"Adding {s}");
        }
    }
}
