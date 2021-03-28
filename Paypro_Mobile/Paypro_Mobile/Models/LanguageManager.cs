using Paypro_Mobile.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Xamarin.Essentials;

namespace Paypro_Mobile.Models
{
    class LanguageManager
    {
        public static void SetLanguage(string languageCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
            LangRS.Culture = new CultureInfo(languageCode);
            Preferences.Set("lang", languageCode);
        }

        public static string GetCurrentLanguage()
        {
            return Preferences.Get("lang", "en");
        }
    }
}
