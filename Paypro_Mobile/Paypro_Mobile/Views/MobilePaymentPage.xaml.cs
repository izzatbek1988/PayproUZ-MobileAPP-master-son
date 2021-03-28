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
    public partial class MobilePaymentPage : ContentPage
    {
        private readonly string pageName = LangRS.MobilePageTitle;
        private readonly string offset = " | ";
        public MobilePaymentPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            this.Title = StaticVariables.Title + offset + pageName;

            BackgroundColor = StaticVariables.BackgroundColor;
            payproLogo.HeightRequest = StaticVariables.LogoHeight;
            payproLogo.WidthRequest = StaticVariables.LogoWidth;
            lblPhoneNumber.TextColor = StaticVariables.MainTextColor;
            lblMoney.TextColor = StaticVariables.MainTextColor;
            lblPhoneNumber.FontSize = StaticVariables.MainTextSize;
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

            lblMoney.Text = String.Format("{1} (Min {0} UZS):", StaticVariables.MinMoney.ToString(),LangRS.GlobalInputMoneyText);
            tboxMoney.Placeholder = lblMoney.Text;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnContinue_Clicked(object sender, EventArgs e)
        {
            if(tboxPhoneNumber.Text == String.Empty || tboxMoney.Text == String.Empty || tboxMoney.Text == null || tboxPhoneNumber.Text == null  || tboxPhoneNumber.Text.Length<19)
            {
                DisplayAlert("Error", StaticVariables.ErrorMessageEmpty, StaticVariables.AlertOKText);
                return;
            }

            if (Convert.ToInt32(tboxMoney.Text) < StaticVariables.MinMoney)
            {
                DisplayAlert("Error", StaticVariables.ErrorMessageMinMoney, StaticVariables.AlertOKText);
                return;
            }

            string phoneNumber = tboxPhoneNumber.Text.Replace("+", string.Empty).Replace(" ", string.Empty).Replace("-",String.Empty).Replace("(",String.Empty).Replace(")", String.Empty);
            string money = tboxMoney.Text.Replace(" ", string.Empty);
            Payment payment = new Payment();
            payment.AccountNumber =phoneNumber;
            payment.Money = Convert.ToDecimal(money);
            payment.OpID = 1;

            Navigation.PushAsync(new PaymentPage(payment));
           
        }
    }
}