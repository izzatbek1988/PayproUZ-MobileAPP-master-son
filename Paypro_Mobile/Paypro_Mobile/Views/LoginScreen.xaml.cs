using Paypro_Mobile.Data;
using Paypro_Mobile.Models;
using Paypro_Mobile.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Paypro_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginScreen : ContentPage
    {
        private readonly string pageName = LangRS.LoginTitle;
        private readonly string offset = " | ";

        public LoginScreen()
        {
            InitializeComponent();
            InitAsync();
        }

        public bool AutoLogin
        {
            get => Preferences.Get(nameof(AutoLogin), true);
            set
            {
                Preferences.Set(nameof(AutoLogin), value);
                OnPropertyChanged(nameof(AutoLogin));
            }
        }
        private void InitAsync()
        {
            this.Title = StaticVariables.Title +offset+ pageName;
            autoLoginSwitch.IsToggled = AutoLogin;

            BackgroundColor = StaticVariables.BackgroundColor;
            payproLogo.HeightRequest = StaticVariables.LogoHeight;
            payproLogo.WidthRequest = StaticVariables.LogoWidth;
           
            btnLogin.BackgroundColor = StaticVariables.ButtonSuccessColor;

            line.Color = StaticVariables.LineColor;
            line.HeightRequest = StaticVariables.LineHeight;
            line.Opacity = StaticVariables.LineOpacity;

            line2.Color = StaticVariables.LineColor;
            line2.HeightRequest = StaticVariables.LineHeight;
            line2.Opacity = StaticVariables.LineOpacity;

            lblHeading.TextColor = StaticVariables.MainTextColor;
            lblHeading.FontSize = StaticVariables.HeadingTextSize;

            lblUserName.TextColor = StaticVariables.MainTextColor;
            lblPassword.TextColor = StaticVariables.MainTextColor;

            lblUserName.FontSize = StaticVariables.MainTextSize;
            lblPassword.FontSize = StaticVariables.MainTextSize;

            lblAutoLogin.TextColor = StaticVariables.MainTextColor;
            lblAutoLogin.FontSize = StaticVariables.MainTextSize;


            /*  var contacts = await Plugin.ContactService.CrossContactService.Current.GetContactListAsync();
              Console.WriteLine(contacts);*/


        }
        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (tboxUserName.Text == null || tboxPassword.Text == null || tboxUserName.Text.Equals(string.Empty) || tboxPassword.Text.Equals(string.Empty))
            {
                DisplayAlert("Hata", StaticVariables.ErrorMessageEmpty, "OK");
                return;
            }


            User user = new User 
            { 
                Username = tboxUserName.Text.ToLower(), 
                PasswordSha1 = SHA1.GenerateSHA1(tboxPassword.Text) 
            };


            if (APIService.Login(user))
            {

                if(AutoLogin)
                {
                    Preferences.Set("username", user.Username);
                    Preferences.Set("password", user.PasswordSha1);
                }

                DisplayAlert(StaticVariables.AlertOKText, LangRS.LoginSuccessText, StaticVariables.AlertOKText);

                if (Device.RuntimePlatform == Device.Android)
                {
                    Application.Current.MainPage = new NavigationPage(new HomePage())
                    {
                        BarBackgroundColor = StaticVariables.NavBarBackgroundColor,
                        BarTextColor = StaticVariables.NavBarTextColor
                    };

                }
                else if (Device.RuntimePlatform == Device.iOS)
                {

                    Navigation.PushModalAsync(new NavigationPage(new HomePage())
                    {
                        BarBackgroundColor = StaticVariables.NavBarBackgroundColor,
                        BarTextColor = StaticVariables.NavBarTextColor
                    }); 
                }
            }
            else
            {
                DisplayAlert("Login Failed", LangRS.LoginFailText, "OK");
            }
        }

        private void btnContinueWithoutLogin_Clicked(object sender, EventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new NavigationPage(new HomePage())
                {
                    BarBackgroundColor = StaticVariables.NavBarBackgroundColor,
                    BarTextColor = StaticVariables.NavBarTextColor
                };

            }
            else if (Device.RuntimePlatform == Device.iOS)
            {

                Navigation.PushModalAsync(new NavigationPage(new HomePage())
                {
                    BarBackgroundColor = StaticVariables.NavBarBackgroundColor,
                    BarTextColor = StaticVariables.NavBarTextColor
                });
            }
        }

        async private void Register_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri(StaticVariables.RegisterURL), BrowserLaunchMode.SystemPreferred);
        }

        private void autoLoginSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            AutoLogin = autoLoginSwitch.IsToggled;
        }
    }
}