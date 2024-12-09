using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.DependencyInversion.Wrong
{
    public class SmsSenderWrongUse
    {
        public void SendSms(string message)
        {
            Console.WriteLine($"SMS gonderildi");
        }
    }
}
