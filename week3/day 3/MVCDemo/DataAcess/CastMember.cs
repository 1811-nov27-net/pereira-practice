using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAcess
{
    public class CastMember
    {
        public int Id {get; set;}
        public string Name { get; set; }
        
        //Navigation Property
        public virtual Movie Movie { get; set; }
    }
}
