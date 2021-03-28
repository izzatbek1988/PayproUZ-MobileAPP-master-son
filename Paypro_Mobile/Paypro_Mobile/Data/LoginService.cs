using Paypro_Mobile.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Paypro_Mobile.Models
{
    class LoginService
    {

        public bool Login(User user)
        {

            if (APIService.Login(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool LogOut()
        {
            if (!StaticVariables.isLogged)
                return false;
            StaticVariables.isLogged = false;
            StaticVariables.loggedUser = null;
            Preferences.Set("username", String.Empty);
            Preferences.Set("password", String.Empty);
            Preferences.Set("AutoLogin", false);
            return true;
        }
    }
}
