using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MYCGenerator4OurDailyBread
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;

        public string stMsg
        {
            get { return (string)GetValue(stMsgProperty); }
            set { SetValue(stMsgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for stMsg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty stMsgProperty =
            DependencyProperty.Register("stMsg", typeof(string), typeof(MainPage), new PropertyMetadata(""));



        public Visibility MsgVisibility
        {
            get { return (Visibility)GetValue(MsgVisibilityProperty); }
            set { SetValue(MsgVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MsgVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MsgVisibilityProperty =
            DependencyProperty.Register("MsgVisibility", typeof(Visibility), typeof(MainPage), new PropertyMetadata(Visibility.Collapsed));



        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            fmMain.Navigate(typeof(MYCGenerator.Pages.OurDailyBreadPage));
        }

        private void abMsgClose_Click(object sender, RoutedEventArgs e)
        {
            MsgVisibility = Visibility.Collapsed;
        }
    }
}
