using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ChinookConsoleApp
{ 
    public class EmployeeListResult
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class ListEmployees
    {
        public int ListAll(string  prompt)
        {
            Console.Clear();
            Console.WriteLine();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["chinook"].ConnectionString))
            {
                
                //var employeeListCommand = connection.CreateCommand();

                //employeeListCommand.CommandText = "select employeeid as Id, " +
                //                                  "firstname + ' ' + lastname as fullname " +
                //                                  "from Employee";

                try
                {
                    connection.Open();
                //var reader = employeeListCommand.ExecuteReader();

                //while (reader.Read())
                //{
                //    Console.WriteLine($"{reader["Id"]}.) {reader["FullName"]}");
                //}

                    var result = connection.Query<EmployeeListResult>("select employeeid as Id, " +
                                                  "firstname + ' ' + lastname as fullname " +
                                                  "from Employee");

                    foreach (var employee in result)
                    {
                        Console.WriteLine($"{employee.Id}.) {employee.FullName}");
                    }

                    Console.WriteLine();
                    Console.Write(prompt);
                    return int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                return 0;
            }
        }

        public static string ListSelectEmployee (string promptMessage, int empID)
        {
            var userResponse = "null";
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
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
                    userResponse = Console.ReadLine();

                    Console.WriteLine("Press <enter> to return to the menu.");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ex.Message");
                    Console.WriteLine(ex.StackTrace);
                }
                connection.Close();
                return userResponse;
            }
        }
    }
}