using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.OpenClosed.CorrectUse
{
    public class AreaCalculator
    {
        public double CalculateArea(Shape shape)
        {
            return shape.CalculateArea();
        }
    }
}
