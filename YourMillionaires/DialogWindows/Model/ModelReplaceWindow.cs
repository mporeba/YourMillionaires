using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMillionaires.DialogWindows.ViewModel;
using YourMillionaires.Model;

namespace YourMillionaires.DialogWindows.Model
{
    public class ModelReplaceWindow
    {
        string newQuestionOrAnswer;
        string idHelper;
        string statement;

        public ModelReplaceWindow(string questionOrAnswer, string id, string message)
        {
            this.newQuestionOrAnswer = questionOrAnswer;
            this.idHelper = id;
            this.statement = message;
        }

        public void GetNewQuestionOrAnswer()
        {
            if (!string.IsNullOrEmpty(newQuestionOrAnswer))
            {
                int questionId = int.Parse(
                    idHelper.Split(';').FirstOrDefault()
                    );

                int answerId = int.Parse(
                    idHelper.Split(';').LastOrDefault()
                    );

                if (!string.IsNullOrEmpty(statement))
                {
                    bool isQuestion = false;

                    if (answerId == 0)
                        isQuestion = true;

                    Messenger.Default.Send<WindowMessage>(new WindowMessage
                    {
                        Message = newQuestionOrAnswer,
                        AnswerId = answerId,
                        QuestionId = questionId,
                        IsQuestion = isQuestion
                    });
                }
            }
            else
            {
                ViewModelMessageWindow message = new ViewModelMessageWindow();
                message.SendMessage("\n\nNie napisałeś nowej odpowiedzi!");
                message.OpenWindow(300, 200);
            }
        }
    }
}
