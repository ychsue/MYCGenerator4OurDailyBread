using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MYCGenerator.ViewModels
{
    public class VMCollection4Comparison : ObservableCollection<VMContentAnswerPair> //, INotifyCollectionChanged
    {

        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void InsertItem(int index, VMContentAnswerPair item)
        {
            base.InsertItem(index, item); 
            item.PropertyChanged += Item_PropertyChanged;
        }

        protected override void RemoveItem(int index)
        {
            this[index].PropertyChanged -= Item_PropertyChanged;
            base.RemoveItem(index);
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }

    public class VMContentAnswerPair : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _Content = "";
        public string Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(); }
        }

        private string _Answer = "";
        public string Answer
        {
            get { return _Answer; }
            set { _Answer = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// I don't know how to get its name "Answer" by code. Therefore, once you change the propertyName "Answer", you need to change this word here, too.
        /// </summary>
        /// <returns></returns>
        public static PropertyInfo GetPAnswer()
        {
            return typeof(VMContentAnswerPair).GetProperty("Answer"); // TODO: 
        }

        /// <summary>
        /// I don't know how to get its name "Content" by code. Therefore, once you change the propertyName "Content", you need to change this word here, too.
        /// </summary>
        /// <returns></returns>
        public static PropertyInfo GetPContent()
        {
            return typeof(VMContentAnswerPair).GetProperty("Content"); // TODO: 
        }
    }
}
