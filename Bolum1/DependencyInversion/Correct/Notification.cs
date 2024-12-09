using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.DependencyInversion.Correct
{
    public class NotificationCorrect
    {
        private readonly INotificationSenderCorrect _notificationSender;

        public NotificationCorrect(INotificationSenderCorrect notificationSender)
        {
            _notificationSender = notificationSender;
        }

        public void Notify(string message)
        {
            _notificationSender.Send(message);
        }
    }
}
