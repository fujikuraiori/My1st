using System.ComponentModel;

namespace ProcessTest
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary> 
        /// プロパティの変更があったときに発行されます。 
        /// </summary> 
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
