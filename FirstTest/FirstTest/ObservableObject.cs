using System;
using System.ComponentModel;
namespace FirstTest
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertychanged(string name){
            if(PropertyChanged != null){
                PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }
    }
}
