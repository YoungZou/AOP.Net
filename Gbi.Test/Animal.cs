using Gbi.DemoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbi.Test
{
    [LogAop]
    public class Animal : ContextBoundObject
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Animal() { }

        public Animal(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public void Wang()
        {
            // do nothing
        }

        public void Fight()
        {
            // do nothing
        }
    }
}
