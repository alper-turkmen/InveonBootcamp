using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.DependencyInversion.Wrong
{
    public class NotificationWrongUse
    {
        private readonly EmailSenderWrongUse _emailSender;
        private readonly SmsSenderWrongUse _smsSender;

        public NotificationWrongUse()
        {
            _emailSender = new EmailSenderWrongUse();
            _smsSender = new SmsSenderWrongUse();  
        }

        public void SendEmail(string message)
        {
            _emailSender.SendEmail(message);
        }

        public void SendSms(string message)
        {
            _smsSender.SendSms(message);
        }
    }
}
