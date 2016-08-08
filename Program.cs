using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    class Program
    {
        static void Main(string[] args)
        {
            FactoryPattern.DemoFactoryPattern f = new FactoryPattern.DemoFactoryPattern();
            f.Run();


            Console.ReadLine();
        }
    }
}
