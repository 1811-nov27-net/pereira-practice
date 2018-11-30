using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializationAndAsync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var list = GetPeople();
            var fileName = @"C:\Users\Rogerio\Desktop\data.xml";
            Task<List<Person>> listTask = DeserializeFromFileAsync(fileName);

            //At this point int time, i have not yet started reading the file

            //Synchronously wait on the task to get the return value
            List<Person> list = listTask.Result;


            list[0].Id *= 2; //Make some change
            Console.WriteLine(list[0].Name.FirstName);
            //Backslash are and "escape character" in strings like this to thread them literally, use an @-string
            SerializeToFile(fileName, list);
        }

        public static void SerializeToFile(string fileName, List<Person> people)
        {
            /*
             * First, we need to convert the data in memory (the list of person)
             * into some byte representation (aka serial representation)
             * We can use manu formats for this
             *      We can make up our own
             *      JSON
             *      xml
             *      or some other format
             * We will use XML
             */

            //Reflection is when c# "looks at itself", it let's you for example iterate throught all the properties of a class in code
            var serializer = new XmlSerializer(typeof(List<Person>));
            FileStream fileStream = null;

            //Second we need to write that representation to a file
            try
            {
                fileStream = new FileStream(fileName, FileMode.Create);
                serializer.Serialize(fileStream, people);
            }
            catch(IOException e)
            {
                Console.WriteLine($"Some error in file I/O: {e.Message}");
            }
            finally
            {
                
                fileStream?.Dispose();
            }
        }

        /*
         * As soon as we start doing something ascyn
         * 1 - Await some async method
         * 2 - Make yout method async, returning Task, and name ...Async
         * 3 - Repeat with all method that call  method
         */

        //async on a method is just a flag to people that this returns
        //Task and it needs to be awaited to actually get the result
        public static async Task<List<Person>> DeserializeFromFileAsync(string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<Person>));
            List<Person> result;

            using (var memoryStream = new MemoryStream())
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    //Copy the fileStream asychronously into memoryStream
                    await fileStream.CopyToAsync(memoryStream);
                    /*
                     * When we await the task, other code can run in the meantime
                     * like on another thread - our web server can receive
                     * other requests
                     */
                }//will automatically call .Dispose as Throught it was in "finally"

                //Doesn't support generics, returns "object", have to explicitly
                result = (List<Person>)serializer.Deserialize(memoryStream);
            }

            return result;
        }

        public static List<Person> GetPeople()
        {
            return new List<Person>
            {
                new Person
                {
                    Id = 20,
                    Name = new Name
                    {
                        FirstName = "Rogerio",
                        MiddleName = "Eduardo",
                        LastName = "Pereira"
                    },
                    Address = new Address
                    {
                        Street = "Stret",
                        City = "Arlington",
                        State = "TX"
                    },
                    Age = 26,
                    Nicknames = new List<string>(){"farofa"}
                },
                new Person
                {
                    Name = new Name
                    {
                        FirstName = "Fred",
                        LastName = "Teste"
                    }
                }
            };
        }
    }
}
