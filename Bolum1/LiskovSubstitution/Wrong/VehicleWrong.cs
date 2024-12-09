using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.LiskovSubstitution.Wrong
{
    public class VehicleWrong
    {
        public virtual void StartEngine()
        {
            Console.WriteLine("Motor basladi");
        }
    }
}
