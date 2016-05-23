using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a1 = new StaticClass()
            {
                Liczba = 5,
                Name = "Test 1"
            };

            var a2 = a1.Clone();
            Console.WriteLine(a2.IsNew);

        }
    }
}
