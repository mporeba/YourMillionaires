using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMillionaires.DialogWindows.View;
using YourMillionaires.Model;
using YourMillionaires.ViewModel;

namespace YourMillionaires.DialogWindows.ViewModel
{
    public class ViewModelMessageWindow : PropertyChangedHelper
    {
        public DelegateCommand OkButton { get; set; }

        private string statement = staticMessage;
        public string Statement
        {
            get
            {
                return statement;
            }
            set
            {
                statement = value;
                OnPropertyChanged("Statement");
            }
        }

        static MessageWindow messageWindow;
        static string staticMessage = string.Empty;

        public ViewModelMessageWindow()
        {
            OkButton = new DelegateCommand(Ok);
        }        

        public void OpenWindow(int width, int height)
        {
            messageWindow = new MessageWindow
            {
                Width = width,
                Height = height
            };        

            messageWindow.Show();
        }

        private void Ok()
        {
            messageWindow.Close();
        }

        public void SendMessage(string msg)
        {
            staticMessage = msg;
        }        
    }
}
