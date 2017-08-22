using System;
using System.Configuration;
//using System.Data;
using System.Data.SqlClient;
using Dapper;
//using System.Linq;
//using System.Collections.Generic;
//using System.Threading.Tasks;


namespace ChinookConsoleApp
{
    //public class SalesYearsResult
    //{
    //    public DateTime Year { get; set; }
    //    public float TotalSales { get; set; }
    //}

    public class EmployeeSalesListResult
    {
        public string FullName { get; set; }
        public DateTime Year { get; set; }
        public float TotalSales { get; set; }
    }

    public class ListEmployeeSales
    {

        public void GetSalesYears()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("There were sales in the following years: ");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["chinook"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    var result = connection.Query<int>
                                ("select distinct " +
                                 "year(i.InvoiceDate) " +
                                 "from Invoice as i");

                    foreach (int year in result)
                    {
                        Console.WriteLine($"{year}");
                    }

                    Console.WriteLine();
                    Console.Write("Enter the year for which you want to list sales: ");
                    var selectYear = true;
                    while (selectYear)
                    {
                        int userChoice = int.Parse(Console.ReadLine());
                        foreach (int year in result)
                        {
                            if (userChoice == year)
                            {
                                Console.WriteLine();
                                ListSales(userChoice);
                                selectYear = false;
                            }
                            else
                            {
                                Console.WriteLine("There are not sales for the indicated year.");
                                Console.Write("Enter the year for which you want to list sales: ");
                                // add a way for user to quit
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    Console.ReadLine();
                }

                //Console.WriteLine();
                //Console.WriteLine("Press <enter> to return to the menu");
                //Console.ReadLine();
            }
        }
    

        public void ListSales(int yearX)
        {
            //Console.Clear();
            Console.WriteLine();

            //Console.Write("List employee sales for which year: ");
            //var yearX = Console.ReadLine();

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
                                     "join Customer as c on e.EmployeeId = c.SupportRepId " +
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

                Console.WriteLine();
                Console.WriteLine("Press <enter> to return to the menu");
                Console.ReadLine();
            }
        }
    }
}
    
