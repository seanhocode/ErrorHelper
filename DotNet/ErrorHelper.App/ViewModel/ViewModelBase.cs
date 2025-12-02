using ErrorHelper.App.Core.Model;
using System.ComponentModel;

namespace ErrorHelper.App.ViewModel
{
    public class ViewModelBase : ModelEditor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
