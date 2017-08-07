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

        internal static bool CheckExistenceOfKey(string key)
        {
            return ApplicationData.Current.LocalSettings.Values.Keys.Contains(key);
        }

        /// <summary>
        /// If the token is "", it will not set its key value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        internal static void SetKeyValue(string key, string token)
        {
            if (token == "")
                return;
            if (ApplicationData.Current.LocalSettings.Values.Keys.Contains(key))
                ApplicationData.Current.LocalSettings.Values[key] = token;
            else
                ApplicationData.Current.LocalSettings.Values.Add(key, token);
        }

        internal static object GetValueOfAKey(string key)
        {
            if (ApplicationData.Current.LocalSettings.Values.Keys.Contains(key))
                return ApplicationData.Current.LocalSettings.Values[key];
            else
                return null;
        }

        internal static void RemoveAKey(string key)
        {
            ApplicationData.Current.LocalSettings.Values.Remove(key);
        }
    }
}
