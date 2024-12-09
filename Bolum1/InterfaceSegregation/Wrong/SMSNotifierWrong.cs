using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.InterfaceSegregation.Wrong
{
    public class SMSNotifierWrong : INotifierWrong
    {
        public void SendNotification(string message)
        {
            Console.WriteLine("SMS gonderildi");
        }

        public void AddEmailSubject(string subject)
        {
            throw new NotImplementedException("SMS'lerde baslik eklenmez");
        }

        public void AddEmailAttachment(string attachment)
        {
            throw new NotImplementedException("SMS'lerde ek dosya eklenmez");
        }

        public void AddSmsPhoneNumber(string phoneNumber)
        {
            Console.WriteLine("SMS numarasi eklendi");
        }
    }
}
