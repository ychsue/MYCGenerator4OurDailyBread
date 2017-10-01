using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYCGenerator4OurDailyBread.ViewModels
{
    public class ListOfReservedChars : List<ReserveSubstitutePair>
    {
        public ListOfReservedChars()
        {
            this.Add(new ReserveSubstitutePair('<', '＜'));
            this.Add(new ReserveSubstitutePair('>', '＞'));
            this.Add(new ReserveSubstitutePair(':', '：'));
            this.Add(new ReserveSubstitutePair('\"', '＂'));
            this.Add(new ReserveSubstitutePair('/', '／'));
            this.Add(new ReserveSubstitutePair('\\', '＼'));
            this.Add(new ReserveSubstitutePair('|', '｜'));
            this.Add(new ReserveSubstitutePair('?', '？'));
            this.Add(new ReserveSubstitutePair('*', '＊'));
            this.Add(new ReserveSubstitutePair('#', '＃'));
            this.Add(new ReserveSubstitutePair('&', '＆'));
            //* [2017-10-01 20:08] For SharePoint https://support.microsoft.com/en-us/help/905231/information-about-the-characters-that-you-cannot-use-in-site-names--fo
            this.Add(new ReserveSubstitutePair('~', '～'));
            this.Add(new ReserveSubstitutePair('%', '％'));
            this.Add(new ReserveSubstitutePair('{', '｛'));
            this.Add(new ReserveSubstitutePair('}', '｝'));
            this.Add(new ReserveSubstitutePair('+', '＋'));
        }
    }

    public class ReserveSubstitutePair
    {
        public ReserveSubstitutePair(char res, char sub)
        {
            this.reserved = res;
            this.substitute = sub;
        }
        public char reserved;
        public char substitute;
    }
}
