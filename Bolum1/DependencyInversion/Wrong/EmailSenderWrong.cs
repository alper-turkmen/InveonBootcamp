using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.DependencyInversion.Wrong
{
    public class EmailSenderWrongUse
    {
        public void SendEmail(string message)
        {
            Console.WriteLine("Email gonderildi");
        }
    }

}
