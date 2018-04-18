using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMillionaires.DialogWindows.ViewModel;
using YourMillionaires.Model;

namespace YourMillionaires.ViewModel
{
    /// <summary>
    /// Pomysły na rozwoj:
    /// -dorobić testy!!!
    /// -podczytywanie z worda albo txt pytan i odp
    /// -zapisywanie wyników do pliku/bazy
    /// -tworzenie z plikow wykresów
    /// </summary>
    public class ViewModelBase : PropertyChangedHelper
    {
        public ObservableCollection<object> Children { get { return children; } }

        ObservableCollection<object> children;

        public ViewModelBase()
        {
            children = new ObservableCollection<object>();
            children.Add(new ViewModelTestTab());
            children.Add(new ViewModelQuestionsDatabaseTab());
            children.Add(new ViewModelAddQuestionTab());
        }
    }
}
