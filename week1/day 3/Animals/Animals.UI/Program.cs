using System;
using Animals.Library;

namespace Animals.UI
{
    class Program
    {
        //Program.cs and Program class name are just convetion

        /*
            Naming convention in c#
                PascalCase aks TitleCase for
                    classes
                    methods
                    properties
                    namespace
                camelCase (first letter lowercase) for local variables
         */
        static void Main(string[] args)
        {
            Dog dog = new Dog();
            dog.Bark();

            //
            dog.SetWeight(6);
            Console.WriteLine(dog.GetWeight());

            dog.Name = "Fido";
            Console.WriteLine(dog.Name);

            dog.Breed = "Golden retriever";

            dog.GoTo("the Park");

            Console.WriteLine("Hello World!");



            IAnimal animal = new Dog();
            animal = new Eagle();

            /*
                This is ok but because both classes are within/under the IAnimal type.
                BUT - you're not allowed to do dog-specifig or eagle-specifigc things via this variable
                    error: animal.Fly();
             */
             Eagle e = (Eagle) animal;
             /*
                you can cast objects to mor especific types
                It'll fail at runtime if the object is not actually within/under that type

                These terms are interchangable
                    Superclass, base class, parent class
                    subclass, derived class, child class

                Good design (separation of concerns)
                means you shouldn't write code needlessly tied to one specific implementation

                Then you use the same code with multiple implementations of the classses you're using
              */
              DisplayData(new Dog());
              DisplayData(new Eagle());
        }

        /*
        
         */
        public static void DisplayData(IAnimal animal)
        {
            Console.WriteLine(animal.Name);
        }
    }
}
