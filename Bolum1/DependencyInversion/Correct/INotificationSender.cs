using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.DependencyInversion.Correct
{
    public interface INotificationSenderCorrect
    {
        void Send(string message);
    }
}
