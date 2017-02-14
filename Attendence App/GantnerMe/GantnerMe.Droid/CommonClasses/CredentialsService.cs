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
using Xamarin.Auth;
using GantnerMe.ViewModel;
using GantnerMe.Droid.CommonClasses;

[assembly: Dependency(typeof(CredentialsService))]
namespace GantnerMe.Droid.CommonClasses
{
    public class CredentialsService : ICredentialsService
    {

        public CredentialsService()
        {

            var account = AccountStore.Create(Forms.Context).FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {
                GlobalUserDetail.Username = account.Username;
                GlobalUserDetail.UserID = account.Properties["Userid"].ToString();
            }
        }

        public string Password
        {
            get
            {
                var account = AccountStore.Create(Forms.Context).FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Password"] : null;
            }
        }

        public string UserID
        {
            get
            {
                var account = AccountStore.Create(Forms.Context).FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Userid"] : null;
            }
        }

        public string UserName
        {
            get
            {
                var account = AccountStore.Create(Forms.Context).FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Username : null;
            }
        }

        public void DeleteCredentials()
        {
            var account = AccountStore.Create(Forms.Context).FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {
                AccountStore.Create(Forms.Context).Delete(account, App.AppName);
            }
        }

        public bool DoCredentialsExist()
        {
            return AccountStore.Create(Forms.Context).FindAccountsForService(App.AppName).Any() ? true : false;
        }

        public void SaveCredentials(string userName, string password, string UserID)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                Account account = new Account
                {
                    Username = userName
                };
                account.Properties.Add("Password", password);
                account.Properties.Add("Userid", UserID);
                AccountStore.Create(Forms.Context).Save(account, App.AppName);
            }
        }
    }
}