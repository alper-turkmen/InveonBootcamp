using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.InterfaceSegregation.Correct
{
    public class EmailNotifier : INotifier, IEmailNotifier
    {
        public void SendNotification(string message)
        {
            Console.WriteLine("Email gonderildi");
        }

        public void AddEmailSubject(string subject)
        {
            Console.WriteLine("Email basligi eklendi");
        }
    }
}
