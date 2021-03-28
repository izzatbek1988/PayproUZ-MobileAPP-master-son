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
    public partial class IPPhonePage : ContentPage
    {
        public IPPhonePage()
        {
            InitializeComponent();
            Init();
        }

        private readonly string pageName = LangRS.IPPhonePageTitle;
        private readonly string offset = " | ";
       
        private void Init()
        {
            this.Title = StaticVariables.Title + offset + pageName;

            BackgroundColor = StaticVariables.BackgroundColor;
            payproLogo.HeightRequest = StaticVariables.LogoHeight;
            payproLogo.WidthRequest = StaticVariables.LogoWidth;
            lblAccountNumber.TextColor = StaticVariables.MainTextColor;
            lblMoney.TextColor = StaticVariables.MainTextColor;
            lblAccountNumber.FontSize = StaticVariables.MainTextSize;
            lblMoney.FontSize = StaticVariables.MainTextSize;
            btnContinue.BackgroundColor = StaticVariables.ButtonSuccessColor;
            btnBack.BackgroundColor = StaticVariables.ButtonDangerColor;
            lbliptvPicker.TextColor = StaticVariables.MainTextColor;
            lbliptvPicker.FontSize = StaticVariables.MainTextSize;
            line.Color = StaticVariables.LineColor;
            line.HeightRequest = StaticVariables.LineHeight;
            line.Opacity = StaticVariables.LineOpacity;

            line2.Color = StaticVariables.LineColor;
            line2.HeightRequest = StaticVariables.LineHeight;
            line2.Opacity = StaticVariables.LineOpacity;

            lblHeading.TextColor = StaticVariables.MainTextColor;
            lblHeading.FontSize = StaticVariables.HeadingTextSize;

            lblMoney.Text = String.Format("{1} (Min {0} UZS):", StaticVariables.MinMoney.ToString(), LangRS.GlobalInputMoneyText);
            tboxMoney.Placeholder = lblMoney.Text;

            LoadIPTVProviders();
        }


        private void LoadIPTVProviders()
        {
            iptvPicker.Items.Add("Beeline");
            iptvPicker.Items.Add("TeleKarta");
            iptvPicker.Items.Add("Esat telecom");
            iptvPicker.Items.Add("Platinum connect");
            iptvPicker.Items.Add("Oxygen");
            iptvPicker.Items.Add("UzTelecom");
            /*iptvPicker.SelectedIndex = 0;*/
        }
        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnContinue_Clicked(object sender, EventArgs e)
        {
            if (iptvPicker.SelectedIndex < 0 || tboxAccountNumber.Text == String.Empty || tboxMoney.Text == String.Empty || tboxMoney.Text == null || tboxAccountNumber.Text == null)
            {
                DisplayAlert("Hata", StaticVariables.ErrorMessageEmpty, StaticVariables.AlertOKText);
                return;
            }

            if (Convert.ToInt32(tboxMoney.Text) < StaticVariables.MinMoney)
            {
                DisplayAlert("Error", StaticVariables.ErrorMessageMinMoney, StaticVariables.AlertOKText);
                return;
            }

            string provider = iptvPicker.SelectedItem.ToString();
            string iptvNumber = tboxAccountNumber.Text.Replace(" ", string.Empty);
            string money = tboxMoney.Text.Replace(" ", string.Empty);
            Payment payment = new Payment();
            payment.AccountNumber = iptvNumber;
            payment.Money = Convert.ToDecimal(money);
            payment.Provider = provider;
            payment.OpID = 6;
            Navigation.PushAsync(new PaymentPage(payment));

        }
    }
}
