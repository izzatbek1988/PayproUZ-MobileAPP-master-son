using Paypro_Mobile.Data;
using Paypro_Mobile.Models;
using Paypro_Mobile.Resources;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Paypro_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        private readonly string pageName = "Home";
        private readonly string offset = " | ";
        public HomePage()
        {
            Resources.Add("titleHome", LangRS.HomePageTitle);
            Resources.Add("titleProfile", LangRS.TabPageProfileTitle);
            Resources.Add("titlePastOrders", LangRS.TabPagePastOrdersTitle);
            Resources.Add("titleContact", LangRS.TabPageContactTitle);
            InitializeComponent();
            Init();
            InitPastOrders();
            InitProfile();
        }
        
        void Init()
        {
            if(StaticVariables.CurrencyRate == 0)
            {
                DisplayAlert("Error", "Connection error! Check your network connection or try again!", "OK");
                return;
            }

              if(StaticVariables.isLogged)
              {
                  lblWelcomeMsg.IsVisible = true;
                  lblWelcomeMsg.Text = String.Format("{1} {0}!", StaticVariables.loggedUser.Username[0].ToString().ToUpper() + StaticVariables.loggedUser.Username.Substring(1), LangRS.HomePageWelcomeMsg);
              }
            else
            {
                lblWelcomeMsg.IsVisible = true;
                lblWelcomeMsg.Text = LangRS.HomePageWelcomeMsg;
            }


            this.lblTitle.Text = StaticVariables.Title + " | " + this.CurrentPage.Title;
            lblTitle.TextColor = Color.White;
            lblTitle.FontSize = 20;
            lblTitle.FontAttributes = FontAttributes.Bold;

            lblCurrencyRate.Text = String.Format("{0}", StaticVariables.CurrencyRate);
            lblCurrencyRate.TextColor = Color.White;
            lblCurrencyRate100.Text = String.Format("{0}", StaticVariables.CurrencyRate*100);
            lblCurrencyRate100.TextColor = Color.White;
            BackgroundColor = StaticVariables.HomePageBackgroundColor;
        }

        

        void InitPastOrders()
        {
            if (!StaticVariables.isLogged)
                return;

            var data = APIService.GetPastOrders();
            if (data == null || data.Count <= 0)
            {
                DisplayAlert("Error", "Connection error! Check your network connection or try again!", "OK");
                return;
            }

            listViewPastOrders.ItemsSource = data;
        }

        void InitProfile()
        {
            BackgroundColor = StaticVariables.BackgroundColor;
            payproLogo.HeightRequest = StaticVariables.LogoHeight;
            payproLogo.WidthRequest = StaticVariables.LogoWidth;


            line.Color = StaticVariables.LineColor;
            line.HeightRequest = StaticVariables.LineHeight;
            line.Opacity = StaticVariables.LineOpacity;

            line2.Color = StaticVariables.LineColor;
            line2.HeightRequest = StaticVariables.LineHeight;
            line2.Opacity = StaticVariables.LineOpacity;

            lblHeading.TextColor = StaticVariables.MainTextColor;
            lblHeading.FontSize = StaticVariables.HeadingTextSize;

            lblProfileUsername.FontSize = StaticVariables.MainTextSize;
            lblProfileUsername.TextColor = StaticVariables.MainTextColor;

            lblProfilePhoneNumber.FontSize = StaticVariables.MainTextSize;
            lblProfilePhoneNumber.TextColor = StaticVariables.MainTextColor;

            lblProfileEMail.FontSize = StaticVariables.MainTextSize;
            lblProfileEMail.TextColor = StaticVariables.MainTextColor;

            btnProfileSave.BackgroundColor = StaticVariables.ButtonSuccessColor;
            btnLogout.BackgroundColor = StaticVariables.ButtonDangerColor;
            FillProfileWidthUserData();
        }

        private void FillProfileWidthUserData()
        {
            if (!StaticVariables.isLogged)
                return;

           tboxProfileUsername.Text = StaticVariables.loggedUser.Username[0].ToString().ToUpper() + StaticVariables.loggedUser.Username.Substring(1);
           tboxProfileEMail.Text = StaticVariables.loggedUser.EMail;
           tboxProfilePhoneNumber.Text = StaticVariables.loggedUser.PhoneNumber;
        }   

        async void MobileButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new MobilePaymentPage());
            }
            catch (Exception)
            {
                return;
            }
        }

        

        async void ISSButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ISSPaymentPage());
            }
            catch (Exception)
            {
                return;
            }
        }

        async void CreditCardButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new CreditCardPaymentPage());
            }
            catch (Exception )
            {
                return;
            }
        }

        async void IPTV_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new IPTVPaymentPage());
            }
            catch (Exception )
            {
                return;
            }
        }

        async void PublicServicesButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new PublicServicesPaymentPage());
            }
            catch (Exception )
            {
                return;
            }
        }

        async void PhoneIP_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new IPPhonePage());
            }
            catch (Exception)
            {
                return;
            }
        }

        private void btnTelegram_Clicked(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://t.me/PayPro_Uz");
            OpenBrowser(uri);
        }

        private void btnWhatsApp_Clicked(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://wa.me/79917439956");
            OpenBrowser(uri);
        }

        private void btnTelegram2_Clicked(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://t.me/PayProUz");
            OpenBrowser(uri);
        }

        public async Task OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        

        private async void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            this.lblTitle.Text = StaticVariables.Title + " | " + this.CurrentPage.Title;
            if((this.CurrentPage.Title.Equals(LangRS.TabPageProfileTitle) || this.CurrentPage.Title.Equals(LangRS.TabPagePastOrdersTitle)) && !StaticVariables.isLogged) // Profile TAB
            {
               
                var action = await DisplayAlert("Tizimga kirish zarur", StaticVariables.LoginNeededMessage, StaticVariables.AlertYesText, StaticVariables.AlertNoText);
                if(action)
                {
                    await Navigation.PushAsync(new LoginScreen());
                }
                else
                {
                    tabbedPage.CurrentPage = tabbedPage.Children[0];
                }

            }
           
        }

        private void btnProfileSave_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tboxProfileEMail.Text) || string.IsNullOrEmpty(tboxProfilePhoneNumber.Text))
            {
                DisplayAlert("Error", StaticVariables.ErrorMessageEmpty, StaticVariables.AlertOKText);
                return;
            }

            if (!StaticVariables.isLogged)
                return;

            var data = APIService.UpdateProfile(tboxProfileEMail.Text, tboxProfilePhoneNumber.Text);
            if (data.Successful)
            {
                DisplayAlert(StaticVariables.AlertOKText, StaticVariables.AlertSaveSuccessMessage, StaticVariables.AlertOKText);
                StaticVariables.loggedUser.PhoneNumber = tboxProfilePhoneNumber.Text;
                StaticVariables.loggedUser.EMail = tboxProfileEMail.Text;
            }
            else
            {
                DisplayAlert(StaticVariables.AlertNoText, StaticVariables.AlertSaveFailMessage + " Error: " + data.Information, StaticVariables.AlertOKText);
            }
        }

        private void btnProfileReset_Clicked(object sender, EventArgs e)
        {
            tboxProfileEMail.Text = StaticVariables.loggedUser.EMail;
            tboxProfilePhoneNumber.Text = StaticVariables.loggedUser.PhoneNumber;
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            if (!LoginService.LogOut())
                return;
            tabbedPage.CurrentPage = tabbedPage.Children[0];
            Navigation.PushAsync(new LoginScreen());
        }

        private void btnWebsite_Clicked(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://paypro.uz");
            OpenBrowser(uri);
        }

        private void lblLanguage_Clicked(object sender, EventArgs e)
        {
            if (Preferences.Get("lang", "en").Equals("ru"))
                LanguageManager.SetLanguage("en");
            else
                LanguageManager.SetLanguage("ru");

            (Application.Current).MainPage = new NavigationPage(new HomePage());
        }

        private void ToolbarItemChangeLanguage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectLanguagePage());
        }
    }
}