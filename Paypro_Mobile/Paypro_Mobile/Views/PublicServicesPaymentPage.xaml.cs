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
    public partial class PublicServicesPaymentPage : ContentPage
    {
        private readonly string pageName = LangRS.PublicServicesPageTitle;
        private readonly string offset = " | ";
        private List<City> cities;
        private List<County> counties;
        private int operationID = 3;
        public PublicServicesPaymentPage()
        {
            InitializeComponent();
            Init();
        }


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
            line.Color = StaticVariables.LineColor;
            line.HeightRequest = StaticVariables.LineHeight;
            line.Opacity = StaticVariables.LineOpacity;

            line2.Color = StaticVariables.LineColor;
            line2.HeightRequest = StaticVariables.LineHeight;
            line2.Opacity = StaticVariables.LineOpacity;

            lblHeading.TextColor = StaticVariables.MainTextColor;
            lblHeading.FontSize = StaticVariables.HeadingTextSize;

            lblCity.FontSize = StaticVariables.MainTextSize;
            lblCity.TextColor = StaticVariables.MainTextColor;

            lblCounty.FontSize = StaticVariables.MainTextSize;
            lblCounty.TextColor = StaticVariables.MainTextColor;

            lblServiceType.FontSize = StaticVariables.MainTextSize;
            lblServiceType.TextColor = StaticVariables.MainTextColor;



            lblMoney.Text = String.Format("{1} (Min {0} UZS):", StaticVariables.MinMoney.ToString(), LangRS.GlobalInputMoneyText);
            tboxMoney.Placeholder = lblMoney.Text;
            InitPickers();
        }


        private void InitPickers()
        {
            pickerServiceType.Items.Add("Elektr Energiyasi");
            pickerServiceType.Items.Add("Sovuq Suv");
            pickerServiceType.Items.Add("Tabiiy Gaz");
            pickerServiceType.Items.Add("Chiqindi");
        }


        private void UpdateCityPickers()
        {
            pickerCity.Items.Clear();
            pickerCounty.Items.Clear();
            cities = APIService.GetCities(operationID);
            if(cities == null)
            {
                DisplayAlert("Error", "Connection error! Check your network connection or try again!", "OK");
                return;
            }

            foreach (var item in cities)
            {
                pickerCity.Items.Add(item.Text);
            }
        }

        private void UpdateCountyPickers(int cityID)
        {
            pickerCounty.Items.Clear();
            counties = APIService.GetCounties(operationID,cityID);
            if(counties == null)
            {
                DisplayAlert("Error", "Connection error! Check your network connection or try again!", "OK");
                return;
            }

            foreach (var item in counties)
            {
                pickerCounty.Items.Add(item.Text);
            }
        }


        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnContinue_Clicked(object sender, EventArgs e)
        {
           if (tboxMoney.Text == String.Empty || tboxMoney.Text == null || pickerCity.SelectedIndex<0 || pickerServiceType.SelectedIndex<0 || tboxAccountNumber.Text == string.Empty || tboxAccountNumber.Text == null)
            {
                DisplayAlert("Error", StaticVariables.ErrorMessageEmpty, StaticVariables.AlertOKText);
                return;
            }

           if(pickerCounty.IsEnabled)
            {
                if (pickerCounty.SelectedIndex < 0)
                {
                    DisplayAlert("Error", StaticVariables.ErrorMessageEmpty, StaticVariables.AlertOKText);
                    return;
                }
            }

            if (Convert.ToInt32(tboxMoney.Text) < StaticVariables.MinMoney)
            {
                DisplayAlert("Error", StaticVariables.ErrorMessageMinMoney, StaticVariables.AlertOKText);
                return;
            }

            string money = tboxMoney.Text.Replace(" ", string.Empty);
            Payment payment = new Payment();
            payment.AccountNumber = tboxAccountNumber.Text;
            payment.Money = Convert.ToDecimal(money);
            payment.OpID = 5;
            payment.Region = pickerCounty.IsEnabled ? pickerCity.SelectedItem.ToString() + " " + pickerCounty.SelectedItem.ToString() : payment.Region = pickerCity.SelectedItem.ToString();
            payment.cityID = cities.Where(c => c.Text.ToLower() == pickerCity.SelectedItem.ToString().ToLower()).FirstOrDefault().Value.ToString();
            payment.Type = pickerServiceType.SelectedItem.ToString();
            
            if(pickerCounty.IsEnabled)
                payment.countyID = counties.Where(c => c.Text.ToLower() == pickerCounty.SelectedItem.ToString().ToLower()).FirstOrDefault().Value.ToString();

            Navigation.PushAsync(new PaymentPage(payment));

        }

        private void pickerServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
 
            switch(pickerServiceType.SelectedIndex)
            {
                case 0:
                    pickerCounty.IsEnabled = true;
                    lblCounty.Text = "Xududni Tanlang:";
                    operationID = 1;
                    UpdateCityPickers();
                    break;
                case 1:
                    pickerCounty.IsEnabled = false;
                    lblCounty.Text = "Xududni Tanlang (Xudud tanlash shart emas):";
                    operationID = 3;
                    UpdateCityPickers();
                    break;
                case 2:
                    pickerCounty.IsEnabled = true;
                    lblCounty.Text = "Xududni Tanlang:";
                    operationID = 2;
                    UpdateCityPickers();
                    break;
                case 3:
                    pickerCounty.IsEnabled = false;
                    lblCounty.Text = "Xududni Tanlang (Xudud tanlash shart emas):";
                    operationID = 3;
                    UpdateCityPickers();
                    break;
            }
        }

        private void pickerCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!pickerCounty.IsEnabled || pickerCity.Items.Count<=0)
                return;
            string cityName = pickerCity.SelectedItem.ToString();
            var city = cities.Where(t => t.Text.ToLower() == cityName.ToLower()).FirstOrDefault();
           
            int cityID = city.Value;
            UpdateCountyPickers(cityID);

        }

        private void pickerCounty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}