using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using YourMillionaires.DialogWindows.Model;
using YourMillionaires.DialogWindows.View;
using YourMillionaires.Model;
using YourMillionaires.ViewModel;

namespace YourMillionaires.DialogWindows.ViewModel
{
    public class ViewModelReplaceWindow : PropertyChangedHelper
    {
        public DelegateCommand OkButton { get; set; }
        public DelegateCommand CancelButton { get; set; }

        string statement = staticMessage;
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

        string newQuestionOrAnswer;
        public string NewQuestionOrAnswer
        {
            get
            {
                return newQuestionOrAnswer;
            }
            set
            {
                newQuestionOrAnswer = value;
                OnPropertyChanged("NewQuestionOrAnswer");
            }
        }

        public ViewModelReplaceWindow()
        {
            OkButton = new DelegateCommand(Ok);
            CancelButton = new DelegateCommand(Cancel);
        }

        static string staticMessage;
        static List<XML.Question> questionsHelper;
        static string idHelper;
        static ReplaceWindow replaceWindow;

        public void OpenWindow(int width, int height)
        {
            replaceWindow = new ReplaceWindow
            {
                Width = width,
                Height = height
            };

            replaceWindow.Show();
        }

        public void SendData(string msg, List<XML.Question> questions, string id)
        {
            staticMessage = msg;
            questionsHelper = questions;
            idHelper = id;
        }

        void Ok()
        {           
            new ModelReplaceWindow(NewQuestionOrAnswer, idHelper, statement)
                .GetNewQuestionOrAnswer();

            replaceWindow.Close();
        }

        void Cancel()
        {
            replaceWindow.Close();
        }
    }
}
