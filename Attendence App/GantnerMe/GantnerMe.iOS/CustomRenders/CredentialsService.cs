using GantnerMe.Interface;
using GantnerMe.iOS.CustomRenders;
using GantnerMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;
using Xamarin.Forms;
using System.Linq;

[assembly: Dependency(typeof(CredentialsService))]
namespace GantnerMe.iOS.CustomRenders
{
    public class CredentialsService : ICredentialsService
    {
        public CredentialsService()
        {
            var accountStore = AccountStore.Create();
            var account = accountStore.FindAccountsForService(App.AppName).FirstOrDefault();
            //var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {
                GlobalUserDetail.Email = account.Username;
                GlobalUserDetail.UserID = account.Properties["Userid"].ToString();
            }
        }

        public string Password
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Password"] : null;
            }
        }

        public string UserID
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Userid"] : null;
            }
        }

        public string UserName
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Username : null;
            }
        }

        public void DeleteCredentials()
        {
            var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {
                AccountStore.Create().Delete(account, App.AppName);
            }
        }

        public bool DoCredentialsExist()
        {
            return AccountStore.Create().FindAccountsForService(App.AppName).Any() ? true : false;

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
                AccountStore.Create().Save(account, App.AppName);
            }
        }
    }
}
