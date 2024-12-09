using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.LiskovSubstitution.Wrong
{
    public class CarWrong : VehicleWrong
    {
        public override void StartEngine()
        {
            Console.WriteLine("Araba motoru basladi");
        }
    }
}
