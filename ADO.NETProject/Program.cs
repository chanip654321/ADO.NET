using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ADO.NETProject
{
    public class Program
    {



        private static void ReadData(string RqueryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
                connectionString))
            {
                SqlCommand comand = new SqlCommand(RqueryString, connection);
                comand.Connection.Open();
                SqlDataReader reader = comand.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0),reader.GetString(1), reader.GetInt32(2));
                    }
                }
                else
                {
                    Console.WriteLine("No data found");
                }
                reader.Close();
               
            }
        }

        public static void InsertProduct(string connectionString)
        {
            string ProductName, CategoryId, next ="y";

            while (next == "y")
            { 
            Console.WriteLine("Insert product name:");
            ProductName = Console.ReadLine();
            Console.WriteLine("Insert Category Id:");
            CategoryId = Console.ReadLine();

            Console.WriteLine("do you want to insert another product? (y/n)");
            next = Console.ReadLine();


                string query = "insert into Products_tbl (ProductName, CategoryId) values (@ProductName, @CategoryId)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ProductName", SqlDbType.VarChar, 20).Value = ProductName;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.VarChar, 10).Value = CategoryId;


                    connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();

                    connection.Close();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rowEffected);
                    Console.WriteLine("\n\n");

                }
            }
        }


        static void Main(string[] args)
        {
            string connectionString = "Data Source = SRV2\\PUPILS; Initial Catalog = Store_214358897; Integrated Security = True";
            string RqueryString = "select * from Products_tbl";
            Console.WriteLine("Hello");

            InsertProduct(connectionString);

            
            ReadData(RqueryString, connectionString);






        }
    }
}
