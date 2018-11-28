namespace Animals.Library
{
    public interface IAnimal
    {
        /*
            An interface is a contract that a class has to follow specifying some 
            methods it needs to have, with their arguments types and return type

            No implementation possible in interfaces
            No data either
            Just method names, arguments and return types

            No backing field or auto-implementation
            this is just "any IAnimal class  must have a Name Property
         */
        string Name { get; set;}

        void MakeSound();

        void GoTo(string location); //there is no void type or void class, it just means returns nothing

        /*
            Every interface member must have the access modifier o the whole interface
            Becaus if you really think about it, nothing else would ever be useful
         */
    }
}