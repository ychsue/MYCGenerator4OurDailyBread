using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYCGenerator4OurDailyBread.ViewModels
{
    public class VMODBLangCodes : ObservableCollection<VMODBLangCode>
    {
        public VMODBLangCodes()
        {
            this.Add(new VMODBLangCode()
            {
                View = "‏العربية (Arabic)‏",                        //Different
                Address = "https://arabic-odb.org",
                LangCode = "ar"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Afrikaans",
                Address = "https://afrikaans-odb.org/",
                LangCode = "af"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Bahasa Indonesia",
                Address = "https://santapanrohani.org/",
                LangCode = "id"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Bahasa Malaysia",
                Address = "https://pedomanharian.org/",
                LangCode = "ms"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Burmese",
                Address = "https://myanmar-odb.org/",
                LangCode = "my"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Deutsch (German)",
                Address = "https://unsertaglichbrot.org/",
                LangCode = "de"
            });

            this.Add(new VMODBLangCode()
            {
                View = "English",
                Address = "https://odb.org",
                LangCode = "en-US"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Español (Spanish)",
                Address = "https://nuestropandiario.org/",
                LangCode = "es"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Français (French)",
                Address = "https://www.ministeresnpq.org/",
                LangCode = "fr"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Italiano (Italian)",
                Address = "https://ilnostropanequotidiano.org/",
                LangCode = "it"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Kayin",                             //The same as Khmer one
                Address = "https://kayin-odb.org/",
                LangCode = "kar"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Khmer (Camodian)",                //Different
                Address = "https://khmer-odb.org/",
                LangCode = "km"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Nederlands (Dutch)",
                Address = "https://onsdagelijksbrood.org/",
                LangCode = "nl"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Polski (Polish)",
                Address = "https://codziennychleb.org/",
                LangCode = "pl"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Português (Portuguese)",        //Different one
                Address = "https://paodiario.org/",
                LangCode = "pt"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Русский (Russian)",
                Address = "https://russian-odb.org/",
                LangCode = "ru"
            });


            this.Add(new VMODBLangCode()
            {
                View = "Українська (Ukrainian)",
                Address = "https://ukrainian-odb.org",
                LangCode = "uk"
            });

            this.Add(new VMODBLangCode()
            {
                View = "Tiếng Việt (Vietnamese)",
                Address = "https://vietnamese-odb.org/",
                LangCode = "vi"
            });

            this.Add(new VMODBLangCode()
            {
                View = "हिन्दी (Hindi)",
                Address = "https://hindi-odb.org",
                LangCode = "hi"
            });

            this.Add(new VMODBLangCode()
            {
                View = "தமிழ் (Tamil)",
                Address = "https://tamil-odb.org",
                LangCode = "ta"
            });

            this.Add(new VMODBLangCode()
            {
                View = "සිංහල (Sinhala)",
                Address = "https://sinhala-odb.org",
                LangCode = "si"
            });

            this.Add(new VMODBLangCode()
            {
                View = "ภาษาไทย (Thai)",
                Address = "https://thaiodb.org/",
                LangCode = "th"
            });

            this.Add(new VMODBLangCode()
            {
                View = "简体中文 (Chinese Simplified)",
                Address = "https://simplified-odb.org/",
                LangCode = "zh-CN"
            });

            this.Add(new VMODBLangCode()
            {
                View = "繁體中文 (Chinese Traditional)",
                Address = "https://traditional-odb.org/",
                LangCode = "zh-TW"
            });

            this.Add(new VMODBLangCode()
            {
                View = "日本語 (Japanese)",
                Address = "https://japanese-odb.org/",
                LangCode = "jp"
            });

        }
    }

    public class VMODBLangCode
    {
        public string LangCode { get; set; }
        public string Address { get; set; }
        public string View { get; set; }
    }
}
