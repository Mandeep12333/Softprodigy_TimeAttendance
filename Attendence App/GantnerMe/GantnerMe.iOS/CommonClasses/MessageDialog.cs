using GantnerMe.Interface;
using GantnerMe.iOS.CommonClasses;
using GCDiscreetNotification;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MessageDialog))]
namespace GantnerMe.iOS.CommonClasses
{
    public class MessageDialog : IMessageDialog
    {
        public void AskForString(string message, string title, Action<string> returnString)
        {
            throw new NotImplementedException();
        }

        public void SendConfirmation(string message, string title, Action<bool> confirmationAction)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message, string title = null)
        {
            Helpers.EnsureInvokedOnMainThread(() =>
            {
                var alertView = new UIAlertView(title ?? string.Empty, message, null, "OK");
                alertView.Show();
            });
        }

        public void SendToast(string message)
        {
            var notificationView = new GCDiscreetNotificationView(
                text: message,
                activity: false,
                presentationMode: GCDNPresentationMode.Bottom,
                view: UIApplication.SharedApplication.KeyWindow
            );
            notificationView.ShowAndDismissAfter(4);
        }
    }
}
