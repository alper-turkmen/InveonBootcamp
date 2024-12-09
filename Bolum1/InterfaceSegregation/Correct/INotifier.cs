using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum1.InterfaceSegregation.Correct
{
    public interface INotifier
    {
        void SendNotification(string message);
    }
}
