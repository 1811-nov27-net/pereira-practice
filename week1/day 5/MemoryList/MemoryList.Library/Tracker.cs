using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryList.Library
{
    class Tracker
    {
        private List<string> log = new List<string>() { };

        public void logging(String l)
        {
            log.Add(l);
        }

        public List<string> getLog()
        {
            return log;
        }

        public string getLog(int position)
        {
            return log[position];
        }
    }
}
