using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.SingleResponsibility.CorrectUse
{
    public class EmployeeReportGenerator
    {
        public void GenerateReport(Employee employee)
        {
            Console.WriteLine($"{employee.Name} ({employee.Position}) icin rapor olusturuldu");
        }
    }
}
