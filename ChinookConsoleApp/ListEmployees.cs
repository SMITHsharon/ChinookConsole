using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChinookConsoleApp
{
    public class ListEmployees
    {
        public void ListAll()
        {
            Console.Clear();
            Console.WriteLine();

            using (var connection = new SqlConnection("Server = (local)\\SqlExpress; Database=chinook;Trusted_Connection=True;"))
            {
                var employeeListCommand = connection.CreateCommand();

                employeeListCommand.CommandText = "select employeeid as Id, " +
                                                  "firstname + ' ' + lastname as fullname " +
                                                  "from Employee";

                try
                {
                    connection.Open();
                    var reader = employeeListCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"]}.) {reader["FullName"]}");
                    }

                    Console.WriteLine("Press <enter> to return to the menu.");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        public static string ListSelectEmployee (string promptMessage, int empID)
        {
            var empNewLastName = "null";
            using (var connection = new SqlConnection("Server = (local)\\SqlExpress; Database=chinook;Trusted_Connection=True;"))
            {
                var listThisEmpCommand = connection.CreateCommand();
                listThisEmpCommand.CommandText = "select FirstName + ' ' + LastName as fullname " +
                                                  "from Employee " +
                                                  "where EmployeeId = @selectedID ";

                var employeeIDParameter = listThisEmpCommand.Parameters.Add("@selectedID", SqlDbType.Int);
                employeeIDParameter.Value = empID;

                try
                {
                    connection.Open();
                    var reader = listThisEmpCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        if (promptMessage == "update")
                        {
                            Console.Write($"Change {reader["fullname"]}'s last name to: ");
                        }
                        else if (promptMessage == "delete")
                        {
                            Console.Write($"Type <Y> or <y> to delete {reader["fullname"]}'s record: ");
                        }
                    }
                    empNewLastName = Console.ReadLine();

                    Console.WriteLine("Press <enter> to return to the menu.");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ex.Message");
                    Console.WriteLine(ex.StackTrace);
                }

                return empNewLastName;
            }
        }
    }
}