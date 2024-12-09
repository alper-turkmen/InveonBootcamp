using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.InterfaceSegregation.Wrong
{
        public interface INotifierWrong
        {
            void SendNotification(string message);

            
            void AddEmailSubject(string subject);
            void AddEmailAttachment(string attachment);

            void AddSmsPhoneNumber(string phoneNumber);

         
        }
}
