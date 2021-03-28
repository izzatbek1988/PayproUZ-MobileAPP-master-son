using Paypro_Mobile.Data;
using Paypro_Mobile.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Paypro_Mobile.Models
{
    public class StaticVariables
    {
        public static string Title = LangRS.MainTitle;
        public static Color BackgroundColor = Color.White;
        public static Color HomePageBackgroundColor = Color.White;
        public static Color MainTextColor = Color.Black;
        public static Color NavBarBackgroundColor = Color.DodgerBlue;
        public static Color NavBarTextColor = Color.White;
        public static Color ButtonDangerColor = Color.FromArgb(232, 102, 102);
        public static Color ButtonSuccessColor = Color.LightGreen;
        public static Color LineColor = Color.LightBlue;
        public static Color WhatsAppBtnColor = Color.FromArgb(63, 182, 24);
        public static Color TelegramBtnColor = Color.FromArgb(39, 128, 227);
        public static Color PaidColor = Color.FromArgb(184, 255, 184);
        public static Color NotPaidColor = Color.FromArgb(233, 255, 160);
        public static double LineOpacity = 0.5;
        public static int LineHeight = 3;
        public static int LineWidth = 500;
        public static int LogoHeight = 75;
        public static int LogoWidth = 75;
        public static int MainTextSize = 16;
        public static int HeadingTextSize = 20;
        public static decimal CurrencyRate = APIService.GetCurrencyRate();
        public static int MinMoney = APIService.GetMinMoney();
        public static List<ISSProviders> ISSProviders = APIService.GetISSProviders();
        public static User loggedUser;
        public static bool isLogged = false;

        public static string ErrorMessageEmpty = LangRS.ErrorMessageEmpty;
        public static string RegisterURL = "https://paypro.uz/register?src=mobile";

        public static string TabPageHomeTitle = LangRS.HomePageTitle;
        public static string TabPageProfileTitle = LangRS.TabPageProfileTitle;
        public static string TabPagePastOrdersTitle = LangRS.TabPagePastOrdersTitle;
        public static string TabPageContactTitle = LangRS.TabPageContactTitle;

        public static string AlertYesText = LangRS.AlertYesText;
        public static string AlertNoText = LangRS.AlertNoText;
        public static string AlertOKText = LangRS.AlertOKText;

        public static string LoginNeededMessage = LangRS.LoginNeededMessage;

        public static string ErrorMessageMinMoney = LangRS.ErrorMessageMinMoney;
        public static string AlertSaveSuccessMessage = LangRS.AlertSaveSuccessMessage;
        public static string AlertSaveFailMessage = LangRS.AlertSaveFailMessage;


    }
}
