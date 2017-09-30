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
