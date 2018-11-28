using System;

namespace Animals.Library
{
    public class Dog : IAnimal //: means implements the interface
    {
        //not Fields - properties
        //auto-field
        public int ID{ get; set;}

        //only auto properties have implicit backing fields.
        // as siib as you give a body to the get or the set,
        // you need to add a private field yourself

        //weird example
        private string _name;
        public string Name{ 
            get { return "Bob"; }
            set { Console.WriteLine("inside property setter"); }
        }

        //Property with validation
        private string _breed;
        public string Breed { 
            get
            {
                return _breed;
            } 
            set
            {
                if(value != null && value != "")
                {
                    _breed = value;
                }
            }
        }

        //propertie with explicit backing field
        private int _age;

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                //keyword value in a settertakes in the assigned value
            }
        }

        //Classic setter and getters
        private int Weight;

        public int GetWeight()
        {
            return Weight;
        }

        public void SetWeight(int weight) {
            Weight = weight;
        }

        public void Bark()
        {
            Console.WriteLine("Woof");
        }

        public void MakeSound()
        {
            Bark();
        }

        public void GoTo(string location)
        {
            //string interpolation syntax
            // prefix with $
            // inside {} you should give an expression
            //      Other string or any object that toString will be called on
            string output = $"Walking to {location.ToLower()}.";
            Console.WriteLine(output);
        }

        //snippets: pop, propfull
    }
}
