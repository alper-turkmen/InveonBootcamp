using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.InterfaceSegregation.Wrong
{
    public class EmailNotifierWrong : INotifierWrong
    {
        public void SendNotification(string message)
        {
            Console.WriteLine("Email gonderildi");

        }



        public void AddEmailSubject(string subject)
        {
            Console.WriteLine("Email basligi eklendi");
        }

        public void AddEmailAttachment(string attachment)
        {
            Console.WriteLine("Email dosyasi eklendi");
            
        }

        public void AddSmsPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException("Email'lerde telefon numarasi eklenmez");
        }

    }
}
