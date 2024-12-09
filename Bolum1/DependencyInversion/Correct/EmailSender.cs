using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.DependencyInversion.Correct
{
    public class EmailSenderCorrect : INotificationSenderCorrect
    {
        public void Send(string message)
        {
            Console.WriteLine("Email gonderildi");
        }
    }
}
