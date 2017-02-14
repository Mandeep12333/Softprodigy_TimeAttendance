using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Interface
{
    public interface IMessageDialog
    {
        void SendMessage(string message, string title = null);
        void SendToast(string message);
        void SendConfirmation(string message, string title, Action<bool> confirmationAction);
        void AskForString(string message, string title, Action<string> returnString);
    }
}
