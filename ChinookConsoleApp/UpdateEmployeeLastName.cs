using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ChinookConsoleApp
{
    public class UpdateEmployeeLastName
    {
        public void Update()
        {
            //Console.WriteLine();
            //Console.Write("Enter ID of Employee for name change: ");
            //var empID = Convert.ToInt32(Console.ReadLine());

            var employeeList = new ListEmployees();
            Console.WriteLine();
            var updateEmployeeID = employeeList.ListAll("Choose an employee for name change: ");
            Console.Write("Change this employee's last name to: ");
            var newLastName = Console.ReadLine();

            //var empNewLastName = ListEmployees.ListSelectEmployee ("update", empID);

            //if (empNewLastName != "null")
            //{
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["chinook"].ConnectionString))
            {
               // var updateEmployeeName = connection.CreateCommand();
               // updateEmployeeName.CommandText = "update Employee " +
               //                                  "set LastName = @changedLastName " +
               //                                  "where EmployeeId = @selectedID";

               // var employeeIDParameter = updateEmployeeName.Parameters.Add("@selectedID", SqlDbType.Int);
               // employeeIDParameter.Value = empID;

               //var employeeNameParameter = updateEmployeeName.Parameters.Add("@changedLastName", SqlDbType.VarChar);
               //employeeNameParameter.Value = empNewLastName;

                try
                {
                   connection.Open();

                   var rowsAffected = connection.Execute("update Employee " +
                                                         "set LastName = @changedLastName " +
                                                         "where EmployeeId = @selectedID",
                                      new { changedLastName = newLastName, selectedID = updateEmployeeID });

                    //var rowsAffected = updateEmployeeName.ExecuteNonQuery();
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
