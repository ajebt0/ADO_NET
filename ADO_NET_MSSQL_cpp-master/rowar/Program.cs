using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOBA
{
    class Program
    {
        static void Main(string[] args)
        {

            string conn_str =
               "Server=COMP7A2\\SQLEXPRESS;" +
               "Database=TestIndex;" +
               "Trusted_Connection=yes;" +
               "Encrypt=yes;" + 
               "TrustServerCertificate=yes;";

            SqlConnection connection = new SqlConnection(conn_str);
            connection.Open();
            string sql_command = "SELECT TOP 10 * FROM orders";
            SqlCommand command = new SqlCommand(sql_command, connection);
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read()){
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                double balance = reader.GetDouble(2);
                double creadit = reader.GetDouble(3);
                double diff_balance = reader.GetDouble(4);

                Console.WriteLine(
                    $"Идентификатор: {id}\n" +
                    $"Имя: {name}\n" +
                    $"Баланс: {balance}\n" +
                    $"Задолжность: {creadit}\n" +
                    $"Итого: {diff_balance}\n");
            }

            Console.ReadKey();

        }
    }
}
