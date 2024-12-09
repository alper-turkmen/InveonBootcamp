using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.LiskovSubstitution.Correct
{
    public class Bicycle : Vehicle
    {
        public override void Move()
        {
            Console.WriteLine("Bisiklet hareket ediyor");
        }
    }
}
