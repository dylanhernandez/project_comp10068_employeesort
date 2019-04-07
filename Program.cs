/*
 * AUTHOR: DYLAN HERNANDEZ - 000307857
 * DATE: 2016/03/25
 * PURPOSE: This program will employee data from a csv file and sort it according to user input. This is the employee class which will hold employee data.
 * NOTE: Updated employee class with properties, iComparable and generic list
 * STATEMENT OF AUTHORSHIP: 
 * I, Dylan Hernandez, 000307857 certify that this material is my original work. 
 * No other person's work has been used without due acknowledgement.
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace Lab4
{
    class Program
    {
        /// <summary>
        /// Main:
        /// - Reads a text file of employees
        /// - Displays the data
        /// - Gives the user the option to sort the data
        /// - Will cancel if no data is found
        /// </summary>
        static void Main(string[] args)
        {
            string menuSelection; //Holds the selection choice
            bool programLoop = true; //The loop that the program operates on
            
            List<Employee> empList;
            empList = ReadIntoList(); //Populate array

            if (empList != null) //Will not continue if no file is found
            {
                menuSelection = PrintMenu(false); //Start menu
                while (programLoop) 
                {
                    Employee.EmployeeComparer currentComparer = Employee.GetComparer();
                    switch (menuSelection) //Selection determines what comparison to use for the sort, will sort and then print
                    {
                        case "1": //NAME
                            currentComparer.WhichComp = Employee.EmployeeComparer.ComparisonType.Name; //Set comparer
                            empList.Sort(currentComparer); //Sort by comparer
                            PrintFromEmployee(empList); //Print sorted array
                            menuSelection = PrintMenu(false); //Print menu
                            break;
                        case "2": //NUMBER
                            currentComparer.WhichComp = Employee.EmployeeComparer.ComparisonType.Number;
                            empList.Sort(currentComparer);
                            PrintFromEmployee(empList);
                            menuSelection = PrintMenu(false);
                            break;
                        case "3": //RATE
                            currentComparer.WhichComp = Employee.EmployeeComparer.ComparisonType.Rate;
                            empList.Sort(currentComparer);
                            PrintFromEmployee(empList);
                            menuSelection = PrintMenu(false);
                            break;
                        case "4": //HOURS
                            currentComparer.WhichComp = Employee.EmployeeComparer.ComparisonType.Hours;
                            empList.Sort(currentComparer);
                            PrintFromEmployee(empList);
                            menuSelection = PrintMenu(false);
                            break;

                        case "5": //GROSS
                            currentComparer.WhichComp = Employee.EmployeeComparer.ComparisonType.Gross;
                            empList.Sort(currentComparer);
                            PrintFromEmployee(empList);
                            menuSelection = PrintMenu(false);
                            break;
                        case "6": //Exit application, break loop
                            programLoop = false;
                            break;
                        default: //Assumes error if no option can return, reprint with error
                            menuSelection = PrintMenu(true);
                            break;
                    }
                }
            }
            else 
            {
                Console.Read(); //The program cannot operate; this is so the error message can be reviewed
            }
        }

        /// <summary>
        /// ReadIntoList:
        /// - Opens a text file and collects the data in a string array
        /// - Goes through the string lines, breaks the lines and creates employees
        /// - Returns a list of employees in a list to use for sorting
        /// </summary>
        /// <returns>Employee collection</returns>
        public static List<Employee> ReadIntoList() 
        {
            List<Employee> readCollection = new List<Employee>(); //List to be returned

            try 
            {
                string[] fileRawData = File.ReadAllLines("employees.txt");
                foreach (string rawEmployee in fileRawData) //Run through and populate list
                {
                    Employee sampleEmployee = StringToEmployee(rawEmployee);
                    readCollection.Add(sampleEmployee);
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null; //Returns null if there is an error in the file
            }

            return readCollection;
        }

        /// <summary>
        /// StringToEmployee:
        /// - Converts the data in a string into employee data
        /// </summary>
        /// <param name="RawData">The string that will be used</param>
        /// <returns>The new employee</returns>
        private static Employee StringToEmployee(string RawData)
        {
            //Default variables - these will be put into the employee
            string name;
            int number;
            decimal rate;
            double hours;

            //Split the raw data and parse it into values
            string[] splitRawData = RawData.Split(',');
            name = splitRawData[0].Trim();
            number = int.Parse(splitRawData[1].Trim());
            rate = decimal.Parse(splitRawData[2].Trim());
            hours = double.Parse(splitRawData[3].Trim());

            //Place values into employee
            Employee returnEmployee = new Employee(name, number, rate, hours);
            return returnEmployee;
        }

        /// <summary>
        /// PrintFromEmployee:
        /// - Goes through the provided employee array and prints the data
        /// - Clears all content on the console first
        /// - Prints the headers and data following the console being cleared
        /// </summary>
        private static void PrintFromEmployee(List<Employee> eCollection)
        {
            Console.Clear();
            Console.WriteLine("Employee\t\tNumber\tRate\tHours\tGross Pay");
            Console.WriteLine("===========\t\t======\t======\t=====\t=========");
            foreach (Employee E in eCollection)
            {
                if (E != null)
                {
                    Console.WriteLine(E.ToString());
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// PrintMenu:
        /// - Prints the main menu for this application
        /// - Menu options and choice prompt all appear
        /// </summary>
        /// <param name="errorDetected">If the menu is reprinted because of an error, a message will show</param>
        private static string PrintMenu(bool errorDetected)
        {
            if (errorDetected)
            {
                Console.WriteLine("*** Invalid Choice - Try Again ***");
                Console.WriteLine();
            }
            Console.WriteLine("1. Sort by Employee Name");
            Console.WriteLine("2. Sort by Employee Number");
            Console.WriteLine("3. Sort by Employee Pay Rate");
            Console.WriteLine("4. Sort by Employee Hours");
            Console.WriteLine("5. Sort by Employee Gross Pay");
            Console.WriteLine();
            Console.WriteLine("6. Exit");
            Console.WriteLine();
            Console.Write("Enter choice: ");
            string returnSelection = Console.ReadLine();
            Console.WriteLine();
            return returnSelection;
        }
    }
}
