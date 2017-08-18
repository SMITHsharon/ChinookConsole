using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChinookConsoleApp
{
    public class DeleteEmployee
    {
        public void Delete()
        {
            {
                Console.WriteLine();
                Console.Write("Enter ID of Employee to delete: ");
                var empID = Convert.ToInt32(Console.ReadLine());

                var empNewLastName = ListEmployees.ListSelectEmployee("delete", empID);

                if (empNewLastName != "null")
                {
                    using (var connection = new SqlConnection("Server = (local)\\SqlExpress; Database=chinook;Trusted_Connection=True;"))
                    {
                        var deleteEmployee = connection.CreateCommand();
                        deleteEmployee.CommandText = "delete Employee " +
                                                     "where EmployeeId = @selectedID ";

                        var employeeIDParameter = deleteEmployee.Parameters.Add("@selectedID", SqlDbType.Int);
                        employeeIDParameter.Value = empID;

                        try
                        {
                            connection.Open();

                            var rowsAffected = deleteEmployee.ExecuteNonQuery();
                            Console.WriteLine(rowsAffected != 1 ? "Delete Failed" : "Success!");

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
}