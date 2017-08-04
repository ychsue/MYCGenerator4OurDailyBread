using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MYCGenerator4OurDailyBread.Helpers
{
    public class LocalSettingsHelper
    {
        public const string AnsLangKey = "AnsLang";
        public const string ContLangKey = "ContLang";

        public static string GetLSAnsLang()
        {
            if (ApplicationData.Current.LocalSettings.Values.Keys.Contains(AnsLangKey))
                return ApplicationData.Current.LocalSettings.Values[AnsLangKey] as string;
            else
                return "";
        }
        /// <summary>
        /// Set Answer's language code into LocalSettings
        /// </summary>
        /// <param name="stLang">such as zh-TW</param>
        public static void SetLSAnsLang(string stLang)
        {
            ApplicationData.Current.LocalSettings.Values[AnsLangKey] = stLang;
        }

        public static string GetLSContLang()
        {
            if (ApplicationData.Current.LocalSettings.Values.Keys.Contains(ContLangKey))
                return ApplicationData.Current.LocalSettings.Values[ContLangKey] as string;
            else
                return "";
        }
        /// <summary>
        /// Set Content's language code into LocalSettings
        /// </summary>
        /// <param name="stLang">such as zh-TW</param>
        public static void SetLSContLang(string stLang)
        {
            ApplicationData.Current.LocalSettings.Values[ContLangKey] = stLang;
        }

    }
}
