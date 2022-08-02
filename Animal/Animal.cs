using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    public abstract class Animal : IAnimal{
        public string Name { get; set; }
        public int NumberOfLegs{ get; set; }

        public abstract void Eat();
        public abstract void Sleep();
        public abstract void Reproduce();

    }
}
