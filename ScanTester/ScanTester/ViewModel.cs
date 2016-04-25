using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanTester
{
    public class ViewModel : INotifyPropertyChanged
    {
        private IList<ImageDatas> _myList;
        private double _maxWidError;
        private double _maxHeiError;
        private double _aveWidError;
        private double _aveHeiError;
        Model model = new Model();
        public IList<ImageDatas> MyList
        {
            get
            {
                return _myList;
            }
            set
            {
                _myList = value;
                OnPropertyChanged("MyList");
            }
        }
        public double MaxWidError {
            get
            {
                return _maxWidError;
            }
            set
            {
                _maxWidError = value;
                OnPropertyChanged("MaxWidError");
            }
        }
        public double MaxHeiError
        {
            get
            {
                return _maxHeiError;
            }
            set
            {
                _maxHeiError = value;
                OnPropertyChanged("MaxHeiError");
            }
        }
        public double AveWidError
        {
            get
            {
                return _aveWidError;
            }
            set
            {
                _maxHeiError = value;
                OnPropertyChanged("AveHeiError");
            }
        }
        public double AveHeiError
        {
            get
            {
                return _aveHeiError;
            }
            set
            {
                _maxHeiError = value;
                OnPropertyChanged("AveHeiError");
            }
        }
        public void Start() {
            MyList=model.Start();
            model.ErrorCheck(MyList,ref _maxWidError,ref _maxHeiError);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
