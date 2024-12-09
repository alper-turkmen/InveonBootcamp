using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.LiskovSubstitution.Correct
{
    public class Car : MotorVehicle
    {
        public override void StartEngine()
        {
            Console.WriteLine("Araba motoru basladi");
        }

        public override void Move()
        {
            Console.WriteLine("Araba hareket ediyor");
        }
    }
}
