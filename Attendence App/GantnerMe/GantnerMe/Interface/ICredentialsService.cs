using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Interface
{
    public interface ICredentialsService
    {
        string UserName { get; }

        string Password { get; }

        string UserID { get; }

        void SaveCredentials(string userName, string password, string UserID);

        void DeleteCredentials();

        bool DoCredentialsExist();
    }
}
