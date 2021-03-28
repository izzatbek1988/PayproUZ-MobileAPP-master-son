using Paypro_Mobile.Data;
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
    public partial class CreditCardPaymentPage : ContentPage
    {
        private readonly string pageName = LangRS.CardPageTitle;
        private readonly string offset = " | ";

        public CreditCardPaymentPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            HideCardInfo();
            this.Title = StaticVariables.Title + offset + pageName;

            BackgroundColor = StaticVariables.BackgroundColor;
            payproLogo.HeightRequest = StaticVariables.LogoHeight;
            payproLogo.WidthRequest = StaticVariables.LogoWidth;
            lblCardNumber.TextColor = StaticVariables.MainTextColor;
            lblMoney.TextColor = StaticVariables.MainTextColor;
            lblCardNumber.FontSize = StaticVariables.MainTextSize;
            lblMoney.FontSize = StaticVariables.MainTextSize;
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

            lblMoney.Text = String.Format("Summani yozing (Min {0} UZS):", StaticVariables.MinMoney.ToString());
            tboxMoney.Placeholder = lblMoney.Text;

        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnContinue_Clicked(object sender, EventArgs e)
        {
            if (tboxMoney.Text == null || tboxCardNumber.Text == null || tboxCardNumber.Text == String.Empty || tboxMoney.Text == String.Empty || tboxCardNumber.Text.Length<19)
            {
                DisplayAlert("Hata", StaticVariables.ErrorMessageEmpty, StaticVariables.AlertOKText);
                return;
            }

            if(Convert.ToInt32(tboxMoney.Text)<StaticVariables.MinMoney)
            {
                DisplayAlert("Hata", StaticVariables.ErrorMessageMinMoney, StaticVariables.AlertOKText);
                return;
            }

            string cardNumber = tboxCardNumber.Text.Replace("-", string.Empty).Replace(" ", string.Empty);
            string money = tboxMoney.Text.Replace(" ", string.Empty);
            Payment payment = new Payment();
            payment.AccountNumber =cardNumber;
            payment.Money = Convert.ToDecimal(money);
            payment.OpID = 2;

            Navigation.PushAsync(new PaymentPage(payment));
        }

        private void tboxCardNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tboxCardNumber.Text.Length == 19)
            {
                tboxMoney.Focus();
                string cardNumber = tboxCardNumber.Text.Replace("-", string.Empty);
                var cardObject = APIService.GetCreditCardInfo(cardNumber);
                if(cardObject != null)
                {
                    ShowCardInfo(cardObject.Owner, cardObject.BankName);
                    SwitchCardOKMode();
                }
                else
                {
                    SwitchErrorMode();
                    HideCardInfo();
                }
            }
            else
            {
                HideCardInfo();
            }
            
        }


        private void ShowCardInfo(string cardOwner, string bankName)
        {
            lblCardInfo.Text = cardOwner;
            lblBankInfo.Text = bankName;
            layoutCard.IsEnabled = true;
            layoutCard.IsVisible = true;
        }

        private void HideCardInfo()
        {
            layoutCard.IsEnabled = false;
            layoutCard.IsVisible = false;
        }

        private void SwitchErrorMode()
        {
            btnContinue.IsEnabled = false;
            layoutError.IsVisible = true;
        }
        
        private void SwitchCardOKMode()
        {
            btnContinue.IsEnabled = true;
            layoutError.IsVisible = false;
        }
    }
}