using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYCGenerator.ViewModels
{
    public class VMCollection4Comparison : INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;


    }

    public class VMContentAnswerPair : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
