using Paypro_Mobile.Models;
using Paypro_Mobile.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Paypro_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : ContentPage
    {
        private Payment Payment;

        private readonly string pageName = LangRS.PaymentPageTitle;
        private readonly string offset = " | ";

        public PaymentPage(Payment payment)
        {
            this.Payment  = payment;
            InitializeComponent();
            Init();
            InitPayment();
        }
       
        void Init()
        {
            this.Title = StaticVariables.Title + offset + pageName;

            BackgroundColor = StaticVariables.BackgroundColor;
            payproLogo.HeightRequest = StaticVariables.LogoHeight;
            payproLogo.WidthRequest = StaticVariables.LogoWidth;
            lblAccNumber.TextColor = StaticVariables.MainTextColor;
            lblAccNumber.FontSize = StaticVariables.MainTextSize;
            lblMoney.TextColor = StaticVariables.MainTextColor;
            lblMoney.FontSize = StaticVariables.MainTextSize;

            lblUZS.TextColor = StaticVariables.MainTextColor;
            lblUZS.FontSize = StaticVariables.MainTextSize;

            lblWarning.FontAttributes = FontAttributes.Bold;
            lblMoney.FontAttributes = FontAttributes.Bold;
            tboxMoney.FontAttributes = FontAttributes.Bold;

            btnContinue.BackgroundColor = StaticVariables.ButtonSuccessColor;
            btnBack.BackgroundColor = StaticVariables.ButtonDangerColor;

            line.Color = StaticVariables.LineColor;
            line.HeightRequest = StaticVariables.LineHeight;
            line.Opacity = StaticVariables.LineOpacity;

            line2.Color = StaticVariables.LineColor;
            line2.HeightRequest = StaticVariables.LineHeight;
            line2.Opacity = StaticVariables.LineOpacity;

            lblHeading.TextColor = StaticVariables.MainTextColor;
            lblHeading.FontSize = StaticVariables.HeadingTextSize;

            lblWarning.TextColor = Color.DarkRed;

          
        }

        void InitPayment()
        {
            switch(Payment.OpID)
            {
                case 1:
                    lblAccNumber.Text = LangRS.MobilePageInputPhone;
                    break;
                case 2:
                    lblAccNumber.Text = LangRS.CardPageCardNumberInput;
                    break;
                case 3:
                    lblAccNumber.Text = LangRS.GlobalAccountNumberText;
                    break;
                case 4:
                    lblAccNumber.Text = LangRS.GlobalAccountNumberText;
                    break;
                case 5:
                    lblAccNumber.Text = LangRS.GlobalAccountNumberText;
                    break;


            }

            tboxAccNumber.Text = Payment.AccountNumber.ToString() + " " + Payment.Provider + " " + Payment.Region + " " + Payment.Type;
            tboxUZS.Text = Payment.Money.ToString() + " UZS";
            decimal RUB = Payment.Money / StaticVariables.CurrencyRate;
            tboxMoney.Text = LangRS.PaymentPagePaid + " " + RUB.ToString("0.00") + " Rubl.";
        }

        void OpenWebView()
        {
            string userID = StaticVariables.isLogged ? StaticVariables.loggedUser.UserID.ToString() : "-1";
            string passwordSha1 = StaticVariables.isLogged ? StaticVariables.loggedUser.PasswordSha1 : String.Empty;
            string username = StaticVariables.isLogged ? StaticVariables.loggedUser.Username.ToLower() : String.Empty;

            Navigation.PushModalAsync(new WebViewPage(Payment.Money.ToString(),Payment.AccountNumber.ToString(),Payment.OpID.ToString(),Payment.Provider,Payment.Region,Payment.Type,userID,passwordSha1,username,Payment.cityID,Payment.countyID));
        }

        private void btnContinue_Clicked(object sender, EventArgs e)
        {
            OpenWebView();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}