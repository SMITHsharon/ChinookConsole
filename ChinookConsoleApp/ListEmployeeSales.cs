using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Collections.Generic.List;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChinookConsoleApp
{
    class ListEmployeeSales
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

                    var result = connection.Query<EmployeeListResult>("select e.LastName, sum(i.Total) " +
                                                                     "from Invoice as i " +
                                                                     "join Customer as c on i.CustomerID = c.CustomerId " +
                                                                     "join Employee as e on e.EmployeeId = c.SupportRepId " +
                                                                     "where Year(i.InvoiceDate) = 2012 " +
                                                                     "group by e.LastName");

                    foreach (var employee in result)
                    {
                        Console.WriteLine($"{employee.Id}.) {employee.FullName}: ${employee.Sales}");
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
