using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    internal class RollDise : Dise
    {
        public RollDise(List<int> sides) : base(sides)
        {
        }

        public new int Roll()
        {
            int result = base.Roll();
            Console.WriteLine($"Rolled: {result}");
            return result;
        }
    }
}
    
