using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.LiskovSubstitution.Wrong
{
    public class BicycleWrong : VehicleWrong
    {
        public override void StartEngine()
        {
            throw new NotImplementedException("Bisikletlerde motor olmaz");
        }
    }
}
