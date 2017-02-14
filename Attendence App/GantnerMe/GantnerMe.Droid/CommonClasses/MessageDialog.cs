using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GantnerMe.Interface;
using Xamarin.Forms;
using GantnerMe.Droid.CommonClasses;

[assembly: Dependency(typeof(MessageDialog))]
namespace GantnerMe.Droid.CommonClasses
{
    public class MessageDialog : IMessageDialog
    {
        public static void SendMessage(Activity activity, string message, string title = null)
        {
            var builder = new AlertDialog.Builder(activity);
            builder
            .SetTitle(title ?? string.Empty)
            .SetMessage(message)
            .SetPositiveButton("OK", delegate
            {

            });

            AlertDialog alert = builder.Create();
            alert.Show();
        }

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
            var activity = Xamarin.Forms.Forms.Context as Activity;
            var builder = new AlertDialog.Builder(activity);
            builder
                .SetTitle(title ?? string.Empty)
                .SetMessage(message)
                .SetPositiveButton("OK", delegate { });
            AlertDialog alert = builder.Create();
            Device.BeginInvokeOnMainThread(() =>
            {
                alert.Show();
                BrandAlertDialog(alert);
            });
        }

        public void SendToast(string message)
        {
            var activity = Xamarin.Forms.Forms.Context as Activity;
            activity.RunOnUiThread(() =>
            {
                Toast toast = Toast.MakeText(activity, message, ToastLength.Short);
                toast.SetGravity(GravityFlags.Bottom, 0, 0);
                toast.Show();

            });
        }

        private static void BrandAlertDialog(Dialog dialog)
        {
            try
            {
                var resources = dialog.Context.Resources;
                //var color = dialog.Context.Resources.GetColor(Android.Graphics.Color.White);
                // var background = dialog.Context.Resources.GetColor(Android.Graphics.Color.Red);

                var alertTitleId = resources.GetIdentifier("alertTitle", "id", "android");
                var alertTitle = (TextView)dialog.Window.DecorView.FindViewById(alertTitleId);
                alertTitle.SetTextColor(Android.Graphics.Color.Wheat);

                var titleDividerId = resources.GetIdentifier("titleDivider", "id", "android");
                var titleDivider = dialog.Window.DecorView.FindViewById(titleDividerId);
                titleDivider.SetBackgroundColor(Android.Graphics.Color.LawnGreen); // change divider color
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                //Can't change dialog brand color
            }
        }
    }
}