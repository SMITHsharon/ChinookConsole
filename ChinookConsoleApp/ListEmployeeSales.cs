using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChinookConsoleApp
{
    public class EmployeeSalesListResult
    {
        //public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Year { get; set; }
        public float TotalSales { get; set; }
    }

    public class ListEmployeeSales
    {
        public void ListSales()
        {
            Console.Clear();
            Console.WriteLine();
            Console.Write("List employee sales for which year: ");
            var yearX = Console.ReadLine();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["chinook"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    var result = connection.Query<EmployeeSalesListResult>
                                    ("select " +
                                     "e.FirstName + ' ' + e.LastName as FullName, " +
                                     "sum(i.Total) as TotalSales " +
                                     "from Employee as e " +
                                     "join Customer as c on e.EmployeeId = c.CustomerId " +
                                     "join Invoice as i on i.CustomerId = c.CustomerId " +
                                     "where year(i.InvoiceDate) = @selectedYear " +
                                     "group by e.LastName, e.FirstName",
                                      new { selectedYear = yearX });

                    foreach (var employee in result)
                    {
                        Console.WriteLine($"{employee.FullName}: {employee.TotalSales}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
    
