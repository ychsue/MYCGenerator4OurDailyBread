using HtmlAgilityPack;
using MYCGenerator.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MYCGenerator.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OurDailyBreadPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName]string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region     Binding Properties
        private string _todayTitle;
        public string todayTitle
        {
            get { return _todayTitle; }
            set { _todayTitle = value; NotifyPropertyChanged(); }
        }
        private string _todayImageURL;
        public string todayImageURL
        {
            get { return _todayImageURL; }
            set { _todayImageURL = value; NotifyPropertyChanged(); }
        }
        

        public MYCGenerator4ODB todayMYCGen4ODB
        {
            get { return (MYCGenerator4ODB)GetValue(todayMYCGen4ODBProperty); }
            set { SetValue(todayMYCGen4ODBProperty, value); }
        }

        // Using a DependencyProperty as the backing store for todayMYCGen4ODB.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty todayMYCGen4ODBProperty =
            DependencyProperty.Register("todayMYCGen4ODB", typeof(MYCGenerator4ODB), typeof(OurDailyBreadPage), new PropertyMetadata(new MYCGenerator4ODB()));
        
        #endregion

        public OurDailyBreadPage()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

#if DEBUG
//            todayMYCGen4ODB.Ans4ODB.initialize("https://odb.org/2017/06/22/silence/", isTodayURL: true, callbackObj: this);
//            todayMYCGen4ODB.Content4ODB.initialize("https://traditional-odb.org/2017/06/22/%e9%9d%9c%e9%bb%98/", isTodayURL: true);
//#else
            todayMYCGen4ODB.Ans4ODB.initialize("https://ourdailybread.org",callbackObj: this);
            todayMYCGen4ODB.Content4ODB.initialize("https://traditional-odb.org/");
#endif
        }

        private void btCreateMYC_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
