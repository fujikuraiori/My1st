using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScanTester
{
    public class ViewModel : INotifyPropertyChanged
    {
        Model model = new Model();
        private IList<ImageDatas> _myList;
       
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
        private ImageErrors _errors = new ImageErrors();
        public ImageErrors Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                OnPropertyChanged("Errors");
            }
        }
        public void Start() {
            Mouse.OverrideCursor = Cursors.Wait;
            MyList = model.Start();
            Errors = model.ErrorCheck(MyList , Errors);
            Mouse.OverrideCursor = null;
        }
        public void SaveStart()
        {
            model.ResultSave(MyList);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
