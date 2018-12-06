using System;
using System.Data.SqlClient;

namespace AdoNotConnected
{
    class Program
    {
        static void Main(string[] args)
        {
            //ADO.NEt is technically MS's umbrella name for all data-related libraries
            //Including Entity Framework
            //but when we say "ADO.NET" we typically are talking about the older 
            //ways of doing things with DataReader, DataAdapter objects

            //In various GUIs, you need the server URL, login, password.
            //In code, we have developed a convention to use what we call a
            //"connection string" wich will jam all that data into
            //one string to connect to some kind of data source, in our case, SQL Server

            //never commit your connection string to source control like git
            //they're basically passwords
            var connectionString = SecretConfiguration.ConnectionString;

            //var commandString = "select * from Movies.Movie";
            Console.WriteLine("Enter name of movie:");
            var input = Console.ReadLine();
            var commandString = $"select * from Movies.Movie where Name = '{input}';";
            //This allows "SQL injection"
            //un-sanitized user input must not be used to construct SQL queries
            //directly, or else hackers can access or destroy things

            //connection architecture - we're going to receive the whole result
            //have it waiting in the network buffer
            //and use a "cursor/iterator" to read it in row by row

            using (var connection = new SqlConnection(connectionString))
            {
                //connected architecthure 
                //step 1: open the connection
                connection.Open();
                //step 2: execute query
                using (var command = new SqlCommand(commandString, connection)) //This using block is stacked
                using (var reader = command.ExecuteReader())
                {
                    //command.Execute Reader for Select queries that returns things
                    //      return a DataReader
                    //command.Execute non-query for all other commands (that don't return things)
                    //      returns an int for number of rows affected


                    //step 3: process results
                    if (reader.HasRows)
                    {
                        //for each row
                        while (reader.Read())
                        {
                            object id = reader["ID"]; //Access values by column name
                            object name = reader["Name"];

                            Console.WriteLine($"ID: {id}, Name: {name}");
                        }
                    }
                }

                //step 4: close connection
                connection.Close();
            }
        }
    }
}
