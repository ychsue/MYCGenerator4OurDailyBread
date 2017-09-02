using MemorizeYC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace MYCGenerator4OurDailyBread.Helpers
{
    public static class LocalSettingsHelper
    {
        public const string AnsLangKey = "AnsLang";
        public const string ContLangKey = "ContLang";
        #region     View Settings
        public const string TitleFontSizeKey = "TitleFontSize";
        public const double DefaultTitleFontSize = 32;
        public const string CommonFontSizeKey = "CommonFontSize";
        public const double DefaultCommonFontSize = 15;
        public const string IsHorizontalPairKey = "IsHorizontalPair";
        public const bool DefaultIsHorizontalPair = true;
        public const string PairMarginKey = "PairMargin";
        public static Thickness DefaultPairMargin = new Thickness(0,4,0,4);
        #endregion  View Settings

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

        internal static void SetKeyValue(string key, object token)
        {
            if (token == null || ((token is string) && (string)token== "") )
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

        #region     Language
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
        #endregion  Language

        #region For View Settings
        public static double GetTitleFontSize()
        {
            double value = (GetValueOfAKey(TitleFontSizeKey) is double)?(double)GetValueOfAKey(TitleFontSizeKey): DefaultTitleFontSize;
            return value;
        }
        public static double GetCommonFontSize()
        {
            double value = (GetValueOfAKey(CommonFontSizeKey) is double) ? (double)GetValueOfAKey(CommonFontSizeKey) : DefaultCommonFontSize;
            return value;
        }
        public static bool GetIsHorizontalPair()
        {
            bool value = (GetValueOfAKey(IsHorizontalPairKey) is bool) ? (bool)GetValueOfAKey(IsHorizontalPairKey) : DefaultIsHorizontalPair;
            return value;
        }
        public static Thickness GetPairMargin()
        {
            var stValue = GetValueOfAKey(PairMarginKey);
            if (stValue == null||(stValue is string) == false)
                return DefaultPairMargin;
            else
                return ThicknessToStringConverter.GetThicknessFromString((string)stValue,isDefault: true);
        }
        #endregion For View Settings
    }
}
