using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace MemorizeYC.ViewModels
{
    public class ForPlayPageSettings : INotifyPropertyChanged
    {
        public enum PlayTypeEnum
        {
            hint, syn, rec
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isHelpMode = false;
        public bool isHelpMode
        {
            get { return _isHelpMode; }
            set { _isHelpMode = value; NotifyPropertyChanged(); }
        }

        private bool _isAllCardsMoveable = false;
        public bool isAllCardsMoveable
        {
            get { return _isAllCardsMoveable; }
            set { _isAllCardsMoveable = value; NotifyPropertyChanged(); }
        }

        private bool _isArrangeAfterMove = true;
        public bool isArrangeAfterMove
        {
            get { return _isArrangeAfterMove; }
            set { _isArrangeAfterMove = value; NotifyPropertyChanged(); }
        }

        private double _speechRatio = 1;
        public double speechRatio
        {
            get { return _speechRatio; }
            set { _speechRatio = value; NotifyPropertyChanged(); }
        }

        private bool _isIgnoreNewLinesWhenSynthesizing = false;
        public bool isIgnoreNewLinesWhenSynthesizing
        {
            get { return _isIgnoreNewLinesWhenSynthesizing; }
            set { _isIgnoreNewLinesWhenSynthesizing = value; NotifyPropertyChanged(); }
        }

        private string _Link="";
        public string Link
        {
            get { return _Link; }
            set { _Link = value; NotifyPropertyChanged(); }
        }

        private StorageFolder _categoryFolder=null;
        public StorageFolder categoryFolder
        {
            get { return _categoryFolder; }
            set { _categoryFolder = value; NotifyPropertyChanged(); }
        }

        private Image _ImgBackground;
        public Image ImgBackground
        {
            get { return _ImgBackground; }
            set { _ImgBackground = value; NotifyPropertyChanged(); }
        }



        private PlayTypeEnum _playType = PlayTypeEnum.syn;
        public PlayTypeEnum playType
        {
            get { return _playType; }
            set { _playType = value; NotifyPropertyChanged(); }
        }

        private bool _isPickWCardsRandomly = true;
        public bool isPickWCardsRandomly
        {
            get { return _isPickWCardsRandomly; }
            set { _isPickWCardsRandomly = value; NotifyPropertyChanged(); }
        }

        private bool _isBGAlsoChange = true;
        public bool isBGAlsoChange
        {
            get { return _isBGAlsoChange; }
            set { _isBGAlsoChange = value; NotifyPropertyChanged(); }
        }

        private int _numWCardShown = 8;
        public int numWCardShown
        {
            get { return _numWCardShown; }
            set { _numWCardShown = value; NotifyPropertyChanged(); }
        }

        private int _numOfRestCards = 0;
        public int numOfRestCards
        {
            get { return _numOfRestCards; }
            set { _numOfRestCards = value; NotifyPropertyChanged(); }
        }

        private int _numOfAllCards = 0;
        public int numOfAllCards
        {
            get { return _numOfAllCards; }
            set { _numOfAllCards = value; NotifyPropertyChanged(); }
        }

        private string _SynLang = "en-US";
        public string SynLang
        {
            get { return _SynLang; }
            set { _SynLang = value; NotifyPropertyChanged(); }
        }

        private string _BackgroundPath="";
        public string BackgroundPath
        {
            get { return _BackgroundPath; }
            set { _BackgroundPath = value; NotifyPropertyChanged(); }
        }

        #region     [2016-10-27 10:50] IsShownAsList & IsDictateTextContentInHint
        private bool _IsShownAsList = false;
        public bool IsShownAsList
        {
            get { return _IsShownAsList; }
            set { _IsShownAsList = value; NotifyPropertyChanged(); }
        }

        private double _GlobalFontSize = 12;
        public double GlobalFontSize
        {
            get { return _GlobalFontSize; }
            set { _GlobalFontSize = value; NotifyPropertyChanged(); }
        }

        private bool _IsDictateTextContentInHint = false;
        public bool IsDictateTextContentInHint
        {
            get { return _IsDictateTextContentInHint; }
            set {
                if (value == false && IsDictateAnsInHint == false)
                    IsDictateAnsInHint = true;
                _IsDictateTextContentInHint = value; NotifyPropertyChanged(); }
        }

        private bool _IsDictateAnsInHint = true;
        public bool IsDictateAnsInHint
        {
            get { return _IsDictateAnsInHint; }
            set {
                _IsDictateAnsInHint = value;
                if (value == false && IsDictateTextContentInHint == false)
                {
                    IsDictateTextContentInHint = true; //By this way, its binding xaml might know that I change its value.
                }
                NotifyPropertyChanged();
            }
        }

        #endregion  IsShownAsList & IsDictateTextContentInHint

        #region    [2017-06-15 14:53] IsAnsFirst & ContentSynLang
        private bool _isAnsFirst = false;
        public bool isAnsFirst
        {
            get { return _isAnsFirst; }
            set { _isAnsFirst = value; NotifyPropertyChanged(); }
        }

        private string _ContentSynLang ="";
        public string ContentSynLang
        {
            get { return _ContentSynLang; }
            set { _ContentSynLang = value; NotifyPropertyChanged(); }
        }


        #endregion [2017-06-15 14:53] IsAnsFirst & ContentSynLang
    }
}
