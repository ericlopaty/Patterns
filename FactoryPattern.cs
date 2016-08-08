using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.FactoryPattern
{
    public enum EmployeeType
    {
        Hourly, Salaried, Contract
    }

    class DemoFactoryPattern : Demo
    {
        public void Run()
        {
            Console.WriteLine("-----------------------------------------------");
            List<IEmployee> employees = new List<IEmployee>();
            IEmployee e;
            e = EmployeeFactory1.GetEmployee(EmployeeType.Hourly);
            e.Name = "John";
            employees.Add(e);
            e = EmployeeFactory1.GetEmployee(EmployeeType.Salaried);
            e.Name = "Paul";
            employees.Add(e);
            e = EmployeeFactory1.GetEmployee(EmployeeType.Contract);
            e.Name = "George";
            employees.Add(e);
            foreach (IEmployee employee in employees)
                employee.Pay();

            Console.WriteLine("-----------------------------------------------");
            EmployeeFactory2.RegisterType(EmployeeType.Hourly, typeof(HourlyEmployee));
            EmployeeFactory2.RegisterType(EmployeeType.Salaried, typeof(SalariedEmployee));
            EmployeeFactory2.RegisterType(EmployeeType.Contract, typeof(ContractEmployee));
            employees = new List<IEmployee>();
            e = EmployeeFactory2.GetEmployee(EmployeeType.Hourly);
            e.Name = "Fred";
            employees.Add(e);
            e = EmployeeFactory2.GetEmployee(EmployeeType.Salaried);
            e.Name = "Barney";
            employees.Add(e);
            e = EmployeeFactory2.GetEmployee(EmployeeType.Contract);
            e.Name = "Wilma";
            employees.Add(e);
            foreach (IEmployee employee in employees)
                employee.Pay();



        }
    }

    public static class EmployeeFactory1
    {
        public static IEmployee GetEmployee(EmployeeType type)
        {
            IEmployee employee = null;
            switch (type)
            {
                case EmployeeType.Hourly:
                    employee = new HourlyEmployee();
                    break;
                case EmployeeType.Salaried:
                    employee = new SalariedEmployee();
                    break;
                case EmployeeType.Contract:
                    employee = new ContractEmployee();
                    break;
                default:
                    employee = null;
                    break;
            }
            return employee;
        }
    }

    public static class EmployeeFactory2
    {
        private static Hashtable _registeredTypes = new Hashtable();

        public static void RegisterType(EmployeeType id, Type type)
        {
            if (!_registeredTypes.ContainsKey(id))
                _registeredTypes.Add(id, type);
        }

        public static IEmployee GetEmployee(EmployeeType id)
        {
            IEmployee employee = null;
            if (_registeredTypes.ContainsKey(id))
            {
                Type className = (Type)_registeredTypes[id];
                employee = (IEmployee)Activator.CreateInstance(className);
            }
            return employee;
        }
    }

    public interface IEmployee
    {
        string Name { get; set; }
        IEmployee CreateProduct();
        void Pay();
    }

    public class HourlyEmployee : IEmployee
    {
        public string Name { get; set; }
        public double HourlyRate { get; set; }

        public IEmployee CreateProduct()
        {
            return new HourlyEmployee();
        }

        public void Pay()
        {
            Console.WriteLine("Hourly: {0} - paid", Name);
        }
    }

    public class SalariedEmployee : IEmployee
    {
        public string Name { get; set; }
        public double AnnualSalary { get; set; }

        public IEmployee CreateProduct()
        {
            return new SalariedEmployee();
        }

        public void Pay()
        {
            Console.WriteLine("Salaried: {0} - paid", Name);
        }
    }

    public class ContractEmployee : IEmployee
    {
        public string Name { get; set; }
        public double ContractRate { get; set; }

        public IEmployee CreateProduct()
        {
            return new ContractEmployee();
        }

        public void Pay()
        {
            Console.WriteLine("Contract: {0} - paid", Name);
        }
    }
}
