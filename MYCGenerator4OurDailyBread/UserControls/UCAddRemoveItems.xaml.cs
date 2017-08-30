using MYCGenerator4OurDailyBread.Helpers;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MYCGenerator4OurDailyBread.UserControls
{
    public sealed partial class UCAddRemoveItems : UserControl
    {
        public event RoutedEventHandler InsertCont_Click;
        public event RoutedEventHandler InsertPair_Click;
        public event RoutedEventHandler InsertAns_Click;
        public event RoutedEventHandler DeleteCont_Click;
        public event RoutedEventHandler DeletePair_Click;
        public event RoutedEventHandler DeleteAns_Click;

        public ListView SelectedListView { get; set; }

        #region     DependencyProperties
        public int NumPlusItems
        {
            get { return (int)GetValue(NumPlusItemsProperty); }
            set { SetValue(NumPlusItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumPlusItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumPlusItemsProperty =
            DependencyProperty.Register("NumPlusItems", typeof(int), typeof(UCAddRemoveItems), new PropertyMetadata(1));
        

        public int NumMinusItems
        {
            get { return (int)GetValue(NumMinusItemsProperty); }
            set { SetValue(NumMinusItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumMinusItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumMinusItemsProperty =
            DependencyProperty.Register("NumMinusItems", typeof(int), typeof(UCAddRemoveItems), new PropertyMetadata(1));


        #endregion  DependencyProperties

        public UCAddRemoveItems()
        {
            this.InitializeComponent();
        }

        private void btInCont_Click(object sender, RoutedEventArgs e)
        {
            TextBoxHelper.UpdateTextBinding();
            InsertCont_Click?.Invoke(sender, e);
        }

        private void btInPair_Click(object sender, RoutedEventArgs e)
        {
            TextBoxHelper.UpdateTextBinding();
            InsertPair_Click?.Invoke(sender, e);
        }

        private void btInAns_Click(object sender, RoutedEventArgs e)
        {
            TextBoxHelper.UpdateTextBinding();
            InsertAns_Click?.Invoke(sender, e);
        }

        private void btDelCont_Click(object sender, RoutedEventArgs e)
        {
            TextBoxHelper.UpdateTextBinding();
            DeleteCont_Click?.Invoke(sender, e);
        }

        private void btDelPair_Click(object sender, RoutedEventArgs e)
        {
            TextBoxHelper.UpdateTextBinding();
            DeletePair_Click?.Invoke(sender, e);
        }

        private void btDelAns_Click(object sender, RoutedEventArgs e)
        {
            TextBoxHelper.UpdateTextBinding();
            DeleteAns_Click?.Invoke(sender, e);
        }

        private void Hidden_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            if(SelectedListView!=null)
                SelectedListView.SelectedIndex = -1;
        }
    }
}
