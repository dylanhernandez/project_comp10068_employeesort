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

namespace Lab4
{
    /// <summary>
    /// Employee:
    /// - Holds information for an individual employee
    /// - Contains an interface that allows lists of employees to be sorted against each other
    /// - Contains five values: Name, Number, Hourly Rate, Hours, and Gross Pay
    /// </summary>
    class Employee : IComparable<Employee>
    {
        //PRIVATE VARIABLES
        //============================================

        private string _name; //Employee name - example: Clark Kent
        private int _number; //Employee number - example: 123456
        private decimal _rate; //Employee payrate - example: 11.25
        private double _hours; //Employee hours worked - example: 8.5
        private decimal _gross; //Employee grosspay - hours * rate (overtime also calculated for time + half)

        /// <summary>
        /// Constructor 1:
        /// - No default values are set
        /// </summary>
        public Employee()
        {
        }

        /// <summary>
        /// Construtor 2:
        /// - Default values are set
        /// </summary>
        /// <param name="name">Employee name</param>
        /// <param name="number">Employee number</param>
        /// <param name="rate">Employee hourly pay rate</param>
        /// <param name="hours">Employee hours worked</param>
        public Employee(string name, int number, decimal rate, double hours)
        {
            _name = name;
            _number = number;
            _rate = rate;
            _hours = hours;
        }

        //PUBLIC PROPERTIES
        //============================================

        /// <summary>
        /// Name:
        /// This property handles the value of employee name
        /// example: Clark Kent
        /// </summary>
        public string Name 
        {
            get { return this._name; }
            set { this._name = value; }
        }
        /// <summary>
        /// Number:
        /// This property handles the value of employee number
        /// example: 123456
        /// </summary>
        public int Number 
        {
            get { return this._number; }
            set { this._number = value; }
        }
        /// <summary>
        /// Rate:
        /// This property handles the value of employee rate of pay
        /// </summary>
        public decimal Rate
        {
            get { return this._rate; }
            set { this._rate = value; }
        }
        /// <summary>
        /// Hours:
        /// This property handles the value of employee hours worked
        /// </summary>
        public double Hours
        {
            get { return this._hours; }
            set { this._hours = value; }
        }
        /// <summary>
        /// Gross:
        /// This property handles the value of the gross payment for employee
        /// </summary>
        public decimal Gross 
        {
            get 
            {
                this._gross = GetGross();
                return _gross;
            }
            set { this._gross = value; }
        }

        //METHODS
        //============================================

        /// <summary>
        /// GetGross:
        /// - Returns the private variable that holds the gross of the employee
        /// - NOTE: This has been converted to private since the previous lab, this is used to modularize the Gross property
        /// </summary>
        /// <returns>Gross of the employee</returns>
        private decimal GetGross()
        {
            if (_hours > 40)
            {
                double overtimeHours;
                overtimeHours = (_hours - 40) * 1.5; //Remaining hours calculated for time and a half
                _gross = Convert.ToDecimal(40 + overtimeHours) * _rate;
            }
            else
            {
                _gross = Convert.ToDecimal(_hours) * _rate;
            }
            return Math.Round(_gross, 2);
        }

        /// <summary>
        /// toString Override
        /// - Displays all stored data for the employee
        /// - Overrides the toString function in the class
        /// - Usage example: Console.WriteLine(sampleEmployee.toString());
        /// </summary>
        public override string ToString()
        {
            string printString; //The string to go in the print statement, formatted to hold all employee variables
            printString = string.Format("{0}\t\t{1}\t{2:c}\t{3:0.00}\t{4:c}", Name, Number, Rate, Hours, Gross);
            return printString;
        }

        #region IComparable

        /// <summary>
        /// GetComparer:
        /// - Initializes the class that implements IComparer
        /// - This allows a comparison class to be set up and assign sorting based on types
        /// </summary>
        /// <returns>New EmployeeComparer object</returns>
        public static EmployeeComparer GetComparer() 
        {
            return new Employee.EmployeeComparer();
        }

        /// <summary>
        /// CompareTo:
        /// - Uses the generic counterpart to the CompareTo funciton in IComparable
        /// - Evaluates two employee classes based on their properties
        /// </summary>
        /// <param name="other">The employee being valuated</param>
        /// <returns>-1 if the previous is greater, 0 if they match, 1 if the latter is greater</returns>
        public int CompareTo(Employee other)
        {
            return this.Name.CompareTo(other.Name);
        }

        /// <summary>
        /// CompareTo:
        /// Uses the generic counterpart to the CompareTo funciton in IComparable
        /// Evaluates two employee classes based on their properties
        /// NOTE: This is the custom version of the same CompareTo method however:
        ///     -This version uses a nested called EmployeeComparer which implements IComparer
        ///     -The internal class allows a custom comparison to be selected when sorting
        ///     -This makes sorting much cleaner
        /// </summary>
        /// <param name="other">The employee being valuated</param>
        /// <returns>-1 if the previous is greater, 0 if they match, 1 if the latter is greater</returns>
        public int CompareTo(Employee other, Employee.EmployeeComparer.ComparisonType whichOne) 
        {
            switch (whichOne) //Compare the employee based on the selected property
            {
                case Employee.EmployeeComparer.ComparisonType.Name:
                    return this.Name.CompareTo(other.Name);
                case Employee.EmployeeComparer.ComparisonType.Number:
                    return this.Number.CompareTo(other.Number);
                case Employee.EmployeeComparer.ComparisonType.Rate:
                    return this.Rate.CompareTo(other.Rate);
                case Employee.EmployeeComparer.ComparisonType.Hours:
                    return this.Hours.CompareTo(other.Hours);
                case Employee.EmployeeComparer.ComparisonType.Gross:
                    return this.Gross.CompareTo(other.Gross);
            }
            return 0;
        }

        #endregion

        #region EmployeeComparer & IComparer

        /// <summary>
        /// - This is a nested class which enabled the use of a comparisson interface
        /// - The inferface allows the class to select a specific measure to comapre the employees by
        /// - The implementation of this custom comparisson is in the CompareTo method of IComparable
        /// </summary>
        public class EmployeeComparer : IComparer<Employee> 
        {
            /// <summary>
            /// WhichComp:
            /// - This is the property which holds the enumeration that holds specified property to compare
            /// - This property is used as the parameter for the CompareTo method
            /// - The CompareTo method will use this enumeration to determine how the Employees are sorted
            /// </summary>
            public Employee.EmployeeComparer.ComparisonType WhichComp { get; set; }

            /// <summary>
            /// ComparisonType:
            /// - Enumeration based on all employee properties
            /// </summary>
            public enum ComparisonType 
            {
                Name,
                Number,
                Rate,
                Hours,
                Gross
            }

            /// <summary>
            /// Compare:
            /// - Comparison function when using CompareTo
            /// - Allows an employee to be compared using a specific enum
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(Employee x, Employee y)
            {
                return x.CompareTo(y, WhichComp);
            }
        }

        #endregion
    }
}
