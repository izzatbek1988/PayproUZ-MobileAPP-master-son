using Paypro_Mobile.Models;
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
    public partial class WebViewPage : ContentPage
    {
        private string money = String.Empty;
        private string number = String.Empty;
        private string opID = String.Empty;
        private string provider = String.Empty;
        private string region = String.Empty;
        private string type = String.Empty;
        private string userID = String.Empty;
        private string passwordSha1 = String.Empty;
        private string username = String.Empty;
        private string cityID = String.Empty;
        private string countyID = String.Empty;
        public WebViewPage(string money, string number, string opID, string provider, string region, string type,string userID, string passwordSha1, string username, string cityID, string countyID)
        {
            this.money = money ?? String.Empty;
            this.number = number ?? String.Empty;
            this.opID = opID ?? String.Empty;
            this.provider = provider ?? String.Empty;
            this.region = region ?? String.Empty;
            this.type = type ?? String.Empty;
            this.userID = userID ?? String.Empty;
            this.passwordSha1 = passwordSha1 ?? String.Empty;
            this.username = username ?? String.Empty;
            this.cityID = cityID ?? String.Empty;
            this.countyID = countyID ?? String.Empty;
            InitializeComponent();
            Init();
        }

        void Init()
        {
            var htmlSource = new HtmlWebViewSource();
            string html = "<html><body>" +
                           "<form id = \"paymentForm\" name = \"paymentForm\" method = \"POST\" action = \"https://paypro.uz/payment/sendpayment\">" +
                           String.Format("<input type = \"hidden\" name = \"money\" value = \"{0}\" >",money) +
                           String.Format("<input type = \"hidden\" name = \"number\" value = \"{0}\" >",number) +
                           String.Format("<input type = \"hidden\" name = \"opID\" value = \"{0}\" >",opID) +
                           String.Format("<input type = \"hidden\" name = \"provider\" value = \"{0}\" >",provider) +
                           String.Format("<input type = \"hidden\" name = \"region\" value = \"{0}\" >",region) +
                           String.Format("<input type = \"hidden\" name = \"type\" value = \"{0}\" >",type) +
                           String.Format("<input type = \"hidden\" name = \"userID\" value = \"{0}\" >", userID) +
                           String.Format("<input type = \"hidden\" name = \"passwordSha1\" value = \"{0}\" >", passwordSha1) +
                           String.Format("<input type = \"hidden\" name = \"username\" value = \"{0}\" >", username) +
                           String.Format("<input type = \"hidden\" name = \"cityID\" value = \"{0}\" >", cityID) +
                           String.Format("<input type = \"hidden\" name = \"countyID\" value = \"{0}\" >", countyID) +
                           "</form >" +
                           "<div style = \"font-size:xx-large\" > Loading...</div>"+
                            "<h3> If its not redirecting, then <button type = \"submit\" > click here </button> to continue your payment.. </h3 >"+
                            "</body ></html >" +
                          "<script>document.getElementById(\"paymentForm\").submit();</script>";
            htmlSource.Html = html;
            webView.Source = htmlSource;
        }

   
        private void webView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if(e.Url.Contains("https://paypro.uz/success") || e.Url.Contains("paypro.uz/success"))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Success!!", "Tolovingiz muvofoqiyatli amalga oshirildi!! To’lov 10 daqiqa ichida hisobingizga otkaziladi!", "OK"); ;
                });

                if (Device.RuntimePlatform == Device.Android)
                {
                    Application.Current.MainPage = new NavigationPage(new HomePage())
                    {
                        BarBackgroundColor = StaticVariables.NavBarBackgroundColor,
                        BarTextColor = StaticVariables.NavBarTextColor
                    };
                    Navigation.RemovePage(this);

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
        }
    }
}