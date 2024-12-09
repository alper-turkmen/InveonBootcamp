using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.SingleResponsibility.CorrectUse
{
    public class EmployeeRepository
    {
        public void Save(Employee employee)
        {
            Console.WriteLine($"{employee.Name} ({employee.Position}) kaydedildi");
        }
    }
}
