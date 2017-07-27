using HtmlAgilityPack;
using MYCGenerator.ViewModels;
using MYCGenerator4OurDailyBread.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private VMODBLangCodes _langCodes;
        public VMODBLangCodes langCodes
        {
            get { return _langCodes; }
            set { _langCodes = value; NotifyPropertyChanged(); }
        }
        private VMODBLangCode _contentLangCode;
        public VMODBLangCode contentLangCode
        {
            get { return _contentLangCode; }
            set { _contentLangCode = value; NotifyPropertyChanged(); }
        }
        private VMODBLangCode _answerLangCode;
        public VMODBLangCode answerLangCode
        {
            get { return _answerLangCode; }
            set { _answerLangCode = value; NotifyPropertyChanged(); }
        }

        private double _idealPairWidth =100;
        public double idealPairWidth
        {
            get { return _idealPairWidth; }
            set { _idealPairWidth = value; NotifyPropertyChanged(); }
        }



        public VM1OurDailyBread ourDailyBread
        {
            get { return (VM1OurDailyBread)GetValue(ourDailyBreadProperty); }
            set { SetValue(ourDailyBreadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ourDailyBread.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ourDailyBreadProperty =
            DependencyProperty.Register("ourDailyBread", typeof(VM1OurDailyBread), typeof(OurDailyBreadPage), new PropertyMetadata(new VM1OurDailyBread()));

        #endregion

        public bool isAnsGet = false;
        public bool isContentGet = false;

        public OurDailyBreadPage()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //* [2017-07-25 15:12] Set the language
            langCodes = new VMODBLangCodes();
            contentLangCode = langCodes.Where(x => x.LangCode == "zh-TW").FirstOrDefault();
            answerLangCode = langCodes.Where(x => x.LangCode == "en-US").FirstOrDefault();

            ourDailyBread.initialize(contentLangCode.Address, VMContentAnswerPair.GetPContent(), () => { isContentGet = true; return null; });
            ourDailyBread.initialize(answerLangCode.Address, VMContentAnswerPair.GetPAnswer(), () => { isAnsGet = true; return null; });
        }

        private void btCreateMYC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            idealPairWidth = e.NewSize.Width / 2 - 24;
        }
    }
}
