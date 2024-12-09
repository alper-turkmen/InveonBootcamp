using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.OpenClosed.WrongUse
{
    public class AreaCalculatorWrong
    {
        public double CalculateArea(string shapeType, double dimension1, double dimension2 = 0)
        {
            if (shapeType == "Circle")
            {
                return Math.PI * dimension1 * dimension1;
            }
            else if (shapeType == "Rectangle")
            {
                return dimension1 * dimension2;
            }
            else
            {
                throw new ArgumentException("Invalid shape type");
            }
        }
    }
}
