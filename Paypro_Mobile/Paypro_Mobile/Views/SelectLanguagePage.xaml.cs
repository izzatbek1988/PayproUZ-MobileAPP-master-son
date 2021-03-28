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
    public partial class SelectLanguagePage : ContentPage
    {
        public SelectLanguagePage()
        {
            InitializeComponent();
            Init();
        }


        public void Init()
        {
            lblTitle.TextColor = Color.White;
            lblTitle.FontSize = 20;
            lblTitle.FontAttributes = FontAttributes.Bold;
            btnContinue.BackgroundColor = StaticVariables.ButtonSuccessColor;
            if (LanguageManager.GetCurrentLanguage().Equals("ru"))
                rbtnKiril.IsChecked = true;
            else
                rbtnLatin.IsChecked = true;
        }


     

        private void btnContinue_Clicked_1(object sender, EventArgs e)
        {
            if (rbtnKiril.IsChecked)
                LanguageManager.SetLanguage("ru");
            else
                LanguageManager.SetLanguage("en");

            (Application.Current).MainPage = new NavigationPage(new HomePage())
            {
                BarBackgroundColor = StaticVariables.NavBarBackgroundColor,
                BarTextColor = StaticVariables.NavBarTextColor,
            };

        }
    }
}