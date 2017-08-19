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
                var employeeList = new ListEmployees();
                var firedEmployee = employeeList.ListAll("Choose an employee to transition: ");

                
                //Console.WriteLine();
                //Console.Write("Enter ID of Employee to delete: ");
                //var empID = Convert.ToInt32(Console.ReadLine());

                //var confirmedDelete = ListEmployees.ListSelectEmployee("delete", empID);

                //if (confirmedDelete == "Y" || confirmedDelete == "y")
                //{
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["chinook"].ConnectionString))
                    {
                        connection.Open();
                        var cmd = connection.CreateCommand();
                        cmd.CommandText = "delete from Employee where EmployeeId = @employeeID";

                        var employeeIDParameter = cmd.Parameters.Add("@employeeID", SqlDbType.Int);
                        employeeIDParameter.Value = firedEmployee;

                        try
                        {
                            //connection.Open();

                            var recordsDeleted = cmd.ExecuteNonQuery();

                        if (recordsDeleted == 1)
                        {
                            Console.WriteLine("Success");
                        }
                        else if (recordsDeleted > 1)
                        {
                            Console.WriteLine("AAAAHHHHHHHH!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to find a matching ID");
                        }

                            Console.WriteLine("Press <enter> to return to the menu.");
                            Console.ReadLine();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("ex.Message");
                            Console.WriteLine(ex.StackTrace);
                        }
                    //}
                }
            }
        }
    }
}