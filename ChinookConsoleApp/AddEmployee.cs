using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChinookConsoleApp
{
    public class AddEmployee
    {
        public void Add()
        {
            Console.Clear();
            Console.Write("Enter first name:  ");
            var x = Console.ReadLine();
            Console.Write("Enter last name:  ");
            var y = Console.ReadLine();

            using (var connection = new SqlConnection("Server = (local)\\SqlExpress; Database=chinook;Trusted_Connection=True;"))
            {
                var employeeAdd = connection.CreateCommand();
                employeeAdd.CommandText = "Insert into Employee(FirstName, LastName) " +
                                          "Values(@firstName, @lastName)";

                var firstNameParameter = employeeAdd.Parameters.Add("@firstName", SqlDbType.VarChar);
                firstNameParameter.Value = x;

                var lastNameParameter = employeeAdd.Parameters.Add("@lastName", SqlDbType.VarChar);
                lastNameParameter.Value = y;

                try
                {
                    connection.Open();
                    var rowsAffected = employeeAdd.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected != 1 ? "Add Failed" : "Success!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }


                Console.WriteLine("Press <enter> to return to the menu.");
                Console.ReadLine();
            }
        }
    }
}