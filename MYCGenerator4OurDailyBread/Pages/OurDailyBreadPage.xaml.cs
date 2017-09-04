using HtmlAgilityPack;
using MYCGenerator.ViewModels;
using MYCGenerator4OurDailyBread.GlobalVariables;
using MYCGenerator4OurDailyBread.Helpers;
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
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.UI.Popups;
using System.Net;
using System.Threading.Tasks;
using MemorizeYC.Helpers;

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
        private string _version="";
        public string version
        {
            get { return _version; }
            set { _version = value; NotifyPropertyChanged(); }
        }
        
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
            set { _contentLangCode = value;
                LocalSettingsHelper.SetLSContLang(value.LangCode);
                abContentRefresh_Click(null, null);
                NotifyPropertyChanged(); }
        }
        private VMODBLangCode _answerLangCode;
        public VMODBLangCode answerLangCode
        {
            get { return _answerLangCode; }
            set { _answerLangCode = value;
                LocalSettingsHelper.SetLSAnsLang(value.LangCode);
                abAnswerRefresh_Click(null, null);
                NotifyPropertyChanged(); }
        }

        private double _idealPairWidth =100;
        public double idealPairWidth
        {
            get { return _idealPairWidth; }
            set { _idealPairWidth = value; NotifyPropertyChanged(); }
        }

        private bool _isAnsGet = false;
        public bool isAnsGet
        {
            get { return _isAnsGet; }
            set { _isAnsGet = value; NotifyPropertyChanged(); }
        }

        private bool _isContentGet = false;
        public bool isContentGet
        {
            get { return _isContentGet; }
            set { _isContentGet = value; NotifyPropertyChanged(); }
        }

        private bool _isMYCCanCreate =false;
        public bool isMYCCanCreate
        {
            get { return _isMYCCanCreate; }
            set { _isMYCCanCreate = value;
                if (value == true)
                    abGenMYC.Visibility = Visibility.Visible;
                else
                    abGenMYC.Visibility = Visibility.Collapsed;
                NotifyPropertyChanged(); }
        }

        private string _bookshelfPath ="";
        public string bookshelfPath
        {
            get { return _bookshelfPath; }
            set { _bookshelfPath = value; NotifyPropertyChanged(); }
        }
        
        public VM1OurDailyBread ourDailyBread
        {
            get { return (VM1OurDailyBread)GetValue(ourDailyBreadProperty); }
            set { SetValue(ourDailyBreadProperty, value); }
        }

        public StorageFolder folder { get; private set; }
        

        // Using a DependencyProperty as the backing store for ourDailyBread.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ourDailyBreadProperty =
            DependencyProperty.Register("ourDailyBread", typeof(VM1OurDailyBread), typeof(OurDailyBreadPage), new PropertyMetadata(new VM1OurDailyBread()));

        #endregion      Binding Properties

        public OurDailyBreadPage Current = null;

        public OurDailyBreadPage()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Current = this;
            //* [2017-08-17 16:32] Declare the version
            var ver = Windows.ApplicationModel.Package.Current.Id.Version;
            version = ver.Major + "." + ver.Minor + "." + ver.Build + "." + ver.Revision;
            //* [2017-07-25 15:12] Set the language
            IniAllLangs();
            IniBookshelfPath();
        }

        private void IniAllLangs()
        {
            langCodes = new VMODBLangCodes();
            //* [2017-08-02 11:24] Get current language
            var stContLang = LocalSettingsHelper.GetLSContLang();
            var stAnsLang = LocalSettingsHelper.GetLSAnsLang();
            if (stContLang != null && stContLang != "" && stAnsLang != null && stAnsLang != "")
            {
                contentLangCode = langCodes.Where(x => x.LangCode == stContLang).FirstOrDefault();
                answerLangCode = langCodes.Where(x => x.LangCode == stAnsLang).FirstOrDefault();
            }
            else
            {
                string currentStLang = "en-US";
                try
                {
                    currentStLang = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
                    if (currentStLang.IndexOf("zh") == 0 && currentStLang != "zh-CN")
                        currentStLang = "zh-TW";
                    if (currentStLang.IndexOf("en") == 0 && currentStLang != "en-US")
                        currentStLang = "en-US";
                }
                catch (Exception)
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.CannotGetYourSystemLang);
                }

                var currentLangCode = langCodes.Where(x => currentStLang.ToLower().Contains(x.LangCode.ToLower())).FirstOrDefault();
                //* [2017-08-02 11:25] For not supported one & for zh-TW
                if (currentLangCode == null || currentStLang.ToLower() == "zh-TW".ToLower())
                {
                    contentLangCode = langCodes.Where(x => x.LangCode == "zh-TW").FirstOrDefault();
                    answerLangCode = langCodes.Where(x => x.LangCode == "en-US").FirstOrDefault();
                }
                else if (currentStLang.IndexOf("en") == 0)
                {
                    contentLangCode = langCodes.Where(x => x.LangCode == "en-US").FirstOrDefault();
                    answerLangCode = langCodes.Where(x => x.LangCode == "es").FirstOrDefault();
                }
                else
                {
                    contentLangCode = currentLangCode;
                    answerLangCode = langCodes.Where(x => x.LangCode == "en-US").FirstOrDefault();
                }
            }
        }

        private void gdPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateIdealPairWidth(e.NewSize.Width);
        }

        public void UpdateIdealPairWidth(double width)
        {
            var bufWidth = width - PairMargin.Left - PairMargin.Right - 48;
            if (IsHorizontalPair)
                idealPairWidth = bufWidth / 2;
            else
                idealPairWidth = bufWidth;
        }

        private void abContentRefresh_Click(object sender, RoutedEventArgs e)
        {
            isContentGet = false;
            isMYCCanCreate = false;
            ourDailyBread.initialize(contentLangCode.Address, VMContentAnswerPair.GetPContent(), () => {
                isContentGet = true;
                if (isAnsGet)
                    isMYCCanCreate = true;
                return null; },
                contentLangCode.LangCode);
        }

        private void abAnswerRefresh_Click(object sender, RoutedEventArgs e)
        {
            isAnsGet = false;
            isMYCCanCreate = false;
            ourDailyBread.initialize(answerLangCode.Address, VMContentAnswerPair.GetPAnswer(), () => {
                isAnsGet = true;
                if (isContentGet)
                    isMYCCanCreate = true;
                return null; },
                answerLangCode.LangCode);
        }

        private async void imgAnswer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Uri uri;
            if (ourDailyBread.pageURL.Count != 0)
            {
                var stUri = ourDailyBread.pageURL[0].Answer;
                if(stUri!="" && Uri.TryCreate(stUri,UriKind.Absolute,out uri))
                    await Launcher.LaunchUriAsync(uri);
            }
        }

        private async void imgContent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Uri uri;
            if (ourDailyBread.pageURL.Count != 0)
            {
                var stUri = ourDailyBread.pageURL[0].Content;
                if (stUri != "" && Uri.TryCreate(stUri, UriKind.Absolute, out uri))
                    await Launcher.LaunchUriAsync(uri);
            }
        }

        private void abContPlay_Click(object sender, RoutedEventArgs e)
        {
            meAns.Pause();
            meCont.Play();
        }

        private void lvContMP3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ourDailyBread.selContentMP3!=null)
                meCont.Source = new Uri(ourDailyBread.selContentMP3.Answer);
        }

        private void abAnsPlay_Click(object sender, RoutedEventArgs e)
        {
            meCont.Pause();
            meAns.Play();
        }

        private void lvAnsMP3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ourDailyBread.selAnswerMP3!=null)
                meAns.Source = new Uri(ourDailyBread.selAnswerMP3.Answer);
        }

        private void abContPause_Click(object sender, RoutedEventArgs e)
        {
            meCont.Pause();
        }

        private void abAnsPause_Click(object sender, RoutedEventArgs e)
        {
            meAns.Pause();
        }

        private async void abGenMYC_Click(object sender, RoutedEventArgs e)
        {
            prMain.IsActive = true;
            //* [2017-08-17 14:19] To force the TextBox upadtes its binding data
            TextBoxHelper.UpdateTextBinding();
            //* [2017-08-04 10:04] Check whether MRU has a folder for it, then get it.
            if (LocalSettingsHelper.CheckExistenceOfKey(GlobalVariables.MainFolderTokenKey) == false)
            {
                await PickAFolderAsABookshelfAsync();
            }
            else
            {
                folder = await MRUHelper.GetAFolderBackAsync((string)LocalSettingsHelper.GetValueOfAKey(GlobalVariables.MainFolderTokenKey));
            }

            if(folder==null)
            {
                bookshelfPath = "";
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.CannotGetTheFolder, "From OurDailyBreadPage:abGenMYC_Click:: ");
                if (LocalSettingsHelper.CheckExistenceOfKey(GlobalVariables.MainFolderTokenKey) == true)
                {
                    MRUHelper.RemoveAFolderFromMRU(LocalSettingsHelper.GetValueOfAKey(GlobalVariables.MainFolderTokenKey) as string);
                    LocalSettingsHelper.RemoveAKey(GlobalVariables.MainFolderTokenKey);
                }
            }
            else
            {
                bookshelfPath = folder.Path;
                //** [2017-08-04 11:22] Since I can get the folder for containing MYContainers, let me make the whole container
                await MYContainerHelper.CreateAContainer(folder, ourDailyBread);
            }

            prMain.IsActive = false;
        }

        private async Task PickAFolderAsABookshelfAsync()
        {
            //** [2017-08-04 10:14] If not, get one and store it into MRU
            folder = await FolderPickerHelper.GetAFolderAsync("Choose a folder as a bookshelf");
            if (folder != null)
            {
                string token = MRUHelper.AddAFolderIntoMRU(folder);
                LocalSettingsHelper.SetKeyValue(GlobalVariables.MainFolderTokenKey, token);
            }
        }

        private async void IniBookshelfPath()
        {
            if (LocalSettingsHelper.CheckExistenceOfKey(GlobalVariables.MainFolderTokenKey) == true)
            {
                var folder = await MRUHelper.GetAFolderBackAsync((string)LocalSettingsHelper.GetValueOfAKey(GlobalVariables.MainFolderTokenKey));
                if (folder == null)
                {
                    bookshelfPath = "";
                    MRUHelper.RemoveAFolderFromMRU(LocalSettingsHelper.GetValueOfAKey(GlobalVariables.MainFolderTokenKey) as string);
                    LocalSettingsHelper.RemoveAKey(GlobalVariables.MainFolderTokenKey);
                }
                else
                    bookshelfPath = folder.Path;
            }
        }

        private void ToggleSettings_Click(object sender, RoutedEventArgs e)
        {
            gdSettings.Visibility = (gdSettings.Visibility==Visibility.Collapsed)?Visibility.Visible:Visibility.Collapsed;
        }

        private async void btSetChange_Click(object sender, RoutedEventArgs e)
        {
            prMain.IsActive = true;
            StorageFolder oldFolder = null;
            //* [2017-08-07 15:42] Keep the original one in tmpFolder
            if (LocalSettingsHelper.CheckExistenceOfKey(GlobalVariables.MainFolderTokenKey) == true)
            {
                oldFolder = await MRUHelper.GetAFolderBackAsync((string)LocalSettingsHelper.GetValueOfAKey(GlobalVariables.MainFolderTokenKey));
                MRUHelper.RemoveAFolderFromMRU(LocalSettingsHelper.GetValueOfAKey(GlobalVariables.MainFolderTokenKey) as string);
                LocalSettingsHelper.RemoveAKey(GlobalVariables.MainFolderTokenKey);
                bookshelfPath = "";
            }

            //* [2017-08-07 15:43] Get the folder from folder picker
            await PickAFolderAsABookshelfAsync();
            if (folder == null && oldFolder != null)
            {
                string token = MRUHelper.AddAFolderIntoMRU(oldFolder);
                LocalSettingsHelper.SetKeyValue(GlobalVariables.MainFolderTokenKey, token);
                bookshelfPath = oldFolder.Path;
            }
            else if (folder != null)
                bookshelfPath = folder.Path;
            else
                bookshelfPath = "";

            prMain.IsActive = false;
        }

        #region     Add And Remove Items from a listview
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var uiSender = sender as ListView;
            if (uiSender == null)
                return;
            if (uiSender.SelectedIndex >= 0)
            {
                ucAddRemoveItem.SelectedListView = uiSender;
                ucAddRemoveItem.Visibility = Visibility.Visible;
            }
        }

        private VMCollection4Comparison ForWhichCollection(ListView lvSelected)
        {
            VMCollection4Comparison whichCollection;
            if (lvSelected == lvThought)
                whichCollection = ourDailyBread.thought;
            else if (lvSelected == lvPoem)
                whichCollection = ourDailyBread.poem;
            else if (lvSelected == lvContent)
                whichCollection = ourDailyBread.Content;
            else if (lvSelected == lvBible)
                whichCollection = ourDailyBread.BibleContent;
            else
            {
                whichCollection = null;
            }

            return whichCollection;
        }


        private void ucAddRemoveItem_InsertAns_Click(object sender, RoutedEventArgs e)
        {
            prMain.IsActive = true;

            ListView lvSelected = ucAddRemoveItem.SelectedListView;
            int selIndex = lvSelected.SelectedIndex;
            VMCollection4Comparison whichCollection = ForWhichCollection(lvSelected);
            if (whichCollection != null && ucAddRemoveItem.NumPlusItems > 0)
            {
                //* [2017-08-28 17:03] Determine adding how many new pairs
                ucAddRemoveItem.NumPlusItems = (ucAddRemoveItem.NumPlusItems < 0) ? 0 :
                    (ucAddRemoveItem.NumPlusItems > GlobalVariables.MaxPlusNum) ? GlobalVariables.MaxPlusNum : ucAddRemoveItem.NumPlusItems;
                int nadd = ucAddRemoveItem.NumPlusItems;
                int ith;
                for ( ith = whichCollection.Count-1; ith >= whichCollection.Count-ucAddRemoveItem.NumPlusItems && ith > selIndex; ith--)
                {
                    if (whichCollection[ith].Answer != "")
                        break;
                    nadd--;
                }
                //* [2017-08-29 20:21] Add nadd pairs
                for (int i0 = 0; i0 < nadd; i0++)
                {
                    whichCollection.Add(new VMContentAnswerPair());
                }
                //* [2017-08-29 20:27] Begin to move
                do
                {
                    whichCollection[ith + ucAddRemoveItem.NumPlusItems].Answer = whichCollection[ith].Answer;
                    whichCollection[ith].Answer = "";
                    ith--;
                } while (ith >=selIndex);
                //* [2017-08-29 20:32] Notify Collection Changed
                whichCollection.NoticeCollectionChanged(this, new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
            }
            prMain.IsActive = false;
        }

        private void ucAddRemoveItem_InsertCont_Click(object sender, RoutedEventArgs e)
        {
            prMain.IsActive = true;

            ListView lvSelected = ucAddRemoveItem.SelectedListView;
            int selIndex = lvSelected.SelectedIndex;
            VMCollection4Comparison whichCollection = ForWhichCollection(lvSelected);
            if (whichCollection != null && ucAddRemoveItem.NumPlusItems > 0)
            {
                //* [2017-08-28 17:03] Determine adding how many new pairs
                ucAddRemoveItem.NumPlusItems = (ucAddRemoveItem.NumPlusItems < 0) ? 0 :
                    (ucAddRemoveItem.NumPlusItems > GlobalVariables.MaxPlusNum) ? GlobalVariables.MaxPlusNum : ucAddRemoveItem.NumPlusItems;
                int nadd = ucAddRemoveItem.NumPlusItems;
                int ith;
                for (ith = whichCollection.Count - 1; ith >= whichCollection.Count - ucAddRemoveItem.NumPlusItems && ith > selIndex; ith--)
                {
                    if (whichCollection[ith].Content != "")
                        break;
                    nadd--;
                }
                //* [2017-08-29 20:21] Add nadd pairs
                for (int i0 = 0; i0 < nadd; i0++)
                {
                    whichCollection.Add(new VMContentAnswerPair());
                }
                //* [2017-08-29 20:27] Begin to move
                do
                {
                    whichCollection[ith + ucAddRemoveItem.NumPlusItems].Content = whichCollection[ith].Content;
                    whichCollection[ith].Content = "";
                    ith--;
                } while (ith >= selIndex);
                //* [2017-08-29 20:32] Notify Collection Changed
                whichCollection.NoticeCollectionChanged(this, new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
            }
            prMain.IsActive = false;

        }

        private void ucAddRemoveItem_InsertPair_Click(object sender, RoutedEventArgs e)
        {
            prMain.IsActive = true;

            ListView lvSelected = ucAddRemoveItem.SelectedListView;
            int selIndex = lvSelected.SelectedIndex;
            VMCollection4Comparison whichCollection = ForWhichCollection(lvSelected);
            if (whichCollection != null && ucAddRemoveItem.NumPlusItems > 0)
            {
                //* [2017-08-28 17:03] Determine adding how many new pairs
                ucAddRemoveItem.NumPlusItems = (ucAddRemoveItem.NumPlusItems < 0) ? 0 :
                    (ucAddRemoveItem.NumPlusItems > GlobalVariables.MaxPlusNum) ? GlobalVariables.MaxPlusNum : ucAddRemoveItem.NumPlusItems;
                int nadd = ucAddRemoveItem.NumPlusItems;
                int ith;
                for (ith = whichCollection.Count - 1; ith >= whichCollection.Count - ucAddRemoveItem.NumPlusItems && ith > selIndex; ith--)
                {
                    if ((whichCollection[ith].Content != "") || (whichCollection[ith].Answer != ""))
                        break;
                    nadd--;
                }
                //* [2017-08-29 20:21] Add nadd pairs
                for (int i0 = 0; i0 < nadd; i0++)
                {
                    whichCollection.Add(new VMContentAnswerPair());
                }
                //* [2017-08-29 20:27] Begin to move
                do
                {
                    whichCollection[ith + ucAddRemoveItem.NumPlusItems].Content = whichCollection[ith].Content;
                    whichCollection[ith].Content = "";
                    whichCollection[ith + ucAddRemoveItem.NumPlusItems].Answer = whichCollection[ith].Answer;
                    whichCollection[ith].Answer = "";
                    ith--;
                } while (ith >= selIndex);
                //* [2017-08-29 20:32] Notify Collection Changed
                whichCollection.NoticeCollectionChanged(this, new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
            }
            prMain.IsActive = false;

        }

        private void ucAddRemoveItem_DeleteAns_Click(object sender, RoutedEventArgs e)
        {
            DeleteAction(sender, e);
        }

        private void ucAddRemoveItem_DeleteCont_Click(object sender, RoutedEventArgs e)
        {
            DeleteAction(sender, e);
        }

        private void ucAddRemoveItem_DeletePair_Click(object sender, RoutedEventArgs e)
        {
            DeleteAction(sender, e);
        }

        private void DeleteAction(object sender, RoutedEventArgs e)
        {
            prMain.IsActive = true;

            var btn = sender as Button;
            ListView lvSelected = ucAddRemoveItem.SelectedListView;
            int selIndex = lvSelected.SelectedIndex;
            VMCollection4Comparison whichCollection = ForWhichCollection(lvSelected);
            //* [2017-8-30 15:04] Get a reasonable NumMinusItems value
            int maxDelNum = whichCollection.Count - 1 - selIndex;
            ucAddRemoveItem.NumMinusItems = (ucAddRemoveItem.NumMinusItems < 0) ? 0 :
                (ucAddRemoveItem.NumMinusItems > maxDelNum) ? maxDelNum : ucAddRemoveItem.NumMinusItems;
            if (ucAddRemoveItem.NumMinusItems > 0)
            {
                //* [2017-08-30 15:09] Begin To Move
                int nDel = ucAddRemoveItem.NumMinusItems;
                for (int i0 = selIndex; i0 < whichCollection.Count; i0++)
                {
                    if (btn.Name.ToLower().Contains("del"))
                    {
                        if (btn.Name.ToLower().Contains("cont") || btn.Name.ToLower().Contains("pair"))
                        {
                            var buf = ((i0 + nDel) < whichCollection.Count) ? whichCollection[i0 + nDel].Content : "";
                            whichCollection[i0].Content = buf;
                            //whichCollection[i0 + nDel].Content = "";
                        }
                        if (btn.Name.ToLower().Contains("ans") || btn.Name.ToLower().Contains("pair"))
                        {
                            var buf = ((i0 + nDel) < whichCollection.Count) ? whichCollection[i0 + nDel].Answer : "";
                            whichCollection[i0].Answer = buf;
                            //whichCollection[i0 + nDel].Answer = "";
                        }
                    }
                }
                //* [2017-08-30 15:13] Remove dummy pairs
                var lastPair = whichCollection.LastOrDefault();
                while (whichCollection.Count > (selIndex+1) && lastPair != null && lastPair.Answer == "" && lastPair.Content == "")
                {
                    whichCollection.Remove(lastPair);
                    lastPair = whichCollection.LastOrDefault();
                }
                //* [2017-08-30 15:28] Notify Collection Changed
                whichCollection.NoticeCollectionChanged(this, new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
            }

            prMain.IsActive = false;
        }
        #endregion  Add And Remove Items from a listview

        #region     Settings.View

        private double _TitleFontSize = LocalSettingsHelper.GetTitleFontSize();
        public double TitleFontSize
        {
            get {
                if (_TitleFontSize == double.NaN || _TitleFontSize < 4 || _TitleFontSize > 50)
                    _TitleFontSize = LocalSettingsHelper.DefaultTitleFontSize;
                return _TitleFontSize; }
            set
            {
                if (value < 4)
                {
                    ShowInvalidValueMsg((a) => { TitleFontSize = LocalSettingsHelper.DefaultTitleFontSize; });
                }
                else if (value > 50)
                {
                    ShowInvalidValueMsg((a) => { TitleFontSize = LocalSettingsHelper.DefaultTitleFontSize; });
                }
                else
                {
                    _TitleFontSize = value;
                    LocalSettingsHelper.SetKeyValue(LocalSettingsHelper.TitleFontSizeKey, value);
                    NotifyPropertyChanged();
                }
            }
        }

        private double _CommonFontSize = LocalSettingsHelper.GetCommonFontSize();
        public double CommonFontSize
        {
            get {
                if (_CommonFontSize == double.NaN || _CommonFontSize < 4 || _CommonFontSize > 50)
                    _CommonFontSize = LocalSettingsHelper.DefaultCommonFontSize;
                return _CommonFontSize; }
            set
            {
                if (value < 4)
                {
                    ShowInvalidValueMsg((a) => { CommonFontSize = LocalSettingsHelper.DefaultCommonFontSize; });
                }
                else if (value > 50)
                {
                    ShowInvalidValueMsg((a) => { CommonFontSize = LocalSettingsHelper.DefaultCommonFontSize; });
                }
                else
                {
                    _CommonFontSize = value;
                    LocalSettingsHelper.SetKeyValue(LocalSettingsHelper.CommonFontSizeKey, value);
                    NotifyPropertyChanged();
                }
            }
        }

        private async void ShowInvalidValueMsg(UICommandInvokedHandler action)
        {
            var dialog = new MessageDialog("Invalid input");
            dialog.Commands.Add(
                new UICommand("OK", action));
            await dialog.ShowAsync();
        }

        private bool _IsHorizontalPair = LocalSettingsHelper.GetIsHorizontalPair();
        public bool IsHorizontalPair
        {
            get { return _IsHorizontalPair; }
            set {
                _IsHorizontalPair = value;
                LocalSettingsHelper.SetKeyValue(LocalSettingsHelper.IsHorizontalPairKey, value);
                UpdateIdealPairWidth(gdPage.ActualWidth);
                NotifyPropertyChanged(); }
        }

        private Thickness _PairMargin = LocalSettingsHelper.GetPairMargin();
        public Thickness PairMargin
        {
            get { return _PairMargin; }
            set
            {
                if (((value.Left + value.Right) > Window.Current.Bounds.Width * 0.8) || value.Top > Window.Current.Bounds.Height / 2 || value.Bottom > Window.Current.Bounds.Height / 2)
                {
                    ShowInvalidValueMsg((a) => { PairMargin = LocalSettingsHelper.DefaultPairMargin; });
                }
                else
                {
                    _PairMargin = value;
                    LocalSettingsHelper.SetKeyValue(LocalSettingsHelper.PairMarginKey, ThicknessToStringConverter.GetStringFromThickness(value));
                    UpdateIdealPairWidth(gdPage.ActualWidth);
                    NotifyPropertyChanged();
                }
            }
        }



        #endregion  Settings.View

        private void btSetViewDefault_Click(object sender, RoutedEventArgs e)
        {
            TitleFontSize = LocalSettingsHelper.DefaultTitleFontSize;
            CommonFontSize = LocalSettingsHelper.DefaultCommonFontSize;
            IsHorizontalPair = LocalSettingsHelper.DefaultIsHorizontalPair;
            PairMargin = LocalSettingsHelper.DefaultPairMargin;
        }
    }
}
