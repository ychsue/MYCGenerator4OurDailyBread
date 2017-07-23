using MYCGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MYCGenerator.Controllers
{
    public class MYCGenerator4ODB: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private VM1OurDailyBread _Ans4ODB;
        public VM1OurDailyBread Ans4ODB
        {
            get { return _Ans4ODB; }
            set { _Ans4ODB = value; NotifyPropertyChanged(); }
        }

        private VM1OurDailyBread _Content4ODB;
        public VM1OurDailyBread Content4ODB
        {
            get { return _Content4ODB; }
            set { _Content4ODB = value; NotifyPropertyChanged(); }
        }

        public MYCGenerator4ODB()
        {
            Ans4ODB = new VM1OurDailyBread();
            Content4ODB = new VM1OurDailyBread();
        }
    }
}
