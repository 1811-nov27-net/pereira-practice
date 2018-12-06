using System;
using System.Data.SqlClient;

namespace AdoNetDisconected
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = SecretConfiguration.ConnectionString;
            
            Console.WriteLine("Enter name of movie:");
            var input = Console.ReadLine();
            var commandString = $"select * from Movies.Movie where Name = '{input}';";
            //This allows "SQL injection"

            //disconnected architecture - we're going to wait to get the whole result,
            //load into a DataSet (in memory-collection), close our connection and THEN process the results
            //better aproach in real world

            //this have ore overhead on the c# side, but keep the connection open for less time
            //(which is really good because the DB is usually the bottleneck)

            //dataset 
            var dataSet = new System.Data.DataSet();

            using (var connection = new SqlConnection(connectionString))
            {
                //disconnected architecthure 
                //step 1: open the connection
                connection.Open();
                using (var command = new SqlCommand(commandString, connection)) //This using block is stacked
                using (var adapter = new SqlDataAdapter(command))
                {
                    //step 2: execute query, filling dataset
                    adapter.Fill(dataSet);

                    //(still uses DataReader object internally)
                }

                //step 4: close connection
                connection.Close();
                
                //step 3: process results

                var firstTable = dataSet.Tables[0];
                //watch out - foreach withou generics does a cast when you assign the
                //type right here (DataRow)
                foreach (System.Data.DataRow row in firstTable.Rows)
                {
                    //DataSet contains DataTable, DataColumn, DataRow, etc

                    object id = row["ID"]; //Access values by column name
                    object name = row["Name"];

                    Console.WriteLine($"ID: {id}, Name: {name}");
                }
            }
        }
    }
}
