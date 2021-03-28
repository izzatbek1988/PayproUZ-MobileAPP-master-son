using Paypro_Mobile.Models;
using Paypro_Mobile.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Paypro_Mobile
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "MediaElement_Experimental", "RadioButton_Experimental", "Shell_UWP_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental" });
            InitializeComponent();

            if(Preferences.Get("AutoLogin",false))
            {
                User user = new User
                {
                    Username = Preferences.Get("username", String.Empty),
                    PasswordSha1 = Preferences.Get("password", string.Empty)
                };

                LoginService loginService = new LoginService();

                if(!loginService.Login(user))
                {
                    Preferences.Set("AutoLogin", false);
                }
            }

            if(Preferences.ContainsKey("lang"))
            {
                LanguageManager.SetLanguage(Preferences.Get("lang","en"));
                Current.MainPage = new NavigationPage(new HomePage())
                {
                    BarBackgroundColor = StaticVariables.NavBarBackgroundColor,
                    BarTextColor = StaticVariables.NavBarTextColor,
                };
            }
            else
            {
                Current.MainPage = new NavigationPage(new SelectLanguagePage())
                {
                    BarBackgroundColor = StaticVariables.NavBarBackgroundColor,
                    BarTextColor = StaticVariables.NavBarTextColor,
                };
            }
           


         
        }

        protected override void OnStart()
        {
          
        }   

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
          
        }
        


    }
}
