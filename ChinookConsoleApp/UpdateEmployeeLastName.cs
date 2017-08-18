using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChinookConsoleApp
{
    public class UpdateEmployeeLastName
    {
        public void Update()
        {
            Console.Write("Enter ID of Employee for name change: ");
            var empID = Convert.ToInt32(Console.ReadLine());
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
                        Console.Write($"Change {reader["fullname"]}'s last name to: ");
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
            }

            if (empNewLastName != "null")
            {
                using (var connection = new SqlConnection("Server = (local)\\SqlExpress; Database=chinook;Trusted_Connection=True;"))
                {
                    var updateEmployeeName = connection.CreateCommand();
                    updateEmployeeName.CommandText = "update Employee " +
                                                     "set LastName = @changedLastName " +
                                                     "where EmployeeId = @selectedID ";

                    var employeeIDParameter = updateEmployeeName.Parameters.Add("@selectedID", SqlDbType.Int);
                    employeeIDParameter.Value = empID;

                    var employeeNameParameter = updateEmployeeName.Parameters.Add("@changedLastName", SqlDbType.VarChar);
                    employeeNameParameter.Value = empNewLastName;

                    try
                    {
                        connection.Open();

                        var rowsAffected = updateEmployeeName.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected != 1 ? "Update Failed" : "Success!");

                        Console.WriteLine("Press <enter> to return to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ex.Message");
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
        }
    }
}
