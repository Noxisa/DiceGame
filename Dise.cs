using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace $safeprojectname$
{
    internal class Dise
    {
        public List<int> Sides { get; private set; }

        public Dise(List<int> sides)
        {
            if (sides == null || sides.Count < 2)
                throw new ArgumentNullException("A die must have at least 2 sides.");
            Sides = sides;
        }

        public int Roll()
        {
            System.Random random = new System.Random();
            return Sides[random.Next(Sides.Count)];
        }
    }
}