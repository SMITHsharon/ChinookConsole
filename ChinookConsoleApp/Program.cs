using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = 4;
            var selection = -1;
            while (selection <= options)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("----------------------Welcome to Chinook---------------------");
                Console.WriteLine("------------Please select from the following options---------");
                Console.WriteLine("1. List Employees");
                Console.WriteLine("2. Add an Employee");
                Console.WriteLine("3. Update Employee Last Name");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("9. Exit");
                Console.WriteLine("");
                Console.Write(">");
                selection = Convert.ToInt32(Console.ReadLine());


                if (selection == 1) new ListEmployees().ListAll("Press <enter> to return to the main menu ");
                if (selection == 2) new AddEmployee().Add();
                if (selection == 3) new UpdateEmployeeLastName().Update();
                if (selection == 4) new DeleteEmployee().Delete();
                //if (selection == 9) break;

            }
        }
    }
}
