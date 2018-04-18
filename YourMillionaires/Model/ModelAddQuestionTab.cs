using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;
using YourMillionaires.DialogWindows.ViewModel;
using YourMillionaires.Model;

namespace YourMillionaires.ViewModel
{
    public class ModelAddQuestionTab
    {
        public void AddQuestion(
            string question, 
            string answer1, 
            bool answer1IsTrue, 
            string answer2, 
            bool answer2IsTrue, 
            string answer3, 
            bool answer3IsTrue, 
            string answer4, 
            bool answer4IsTrue)
        {
            List<Answer> answers = new List<Answer>();
            XML questions = new XML().Deserialize();

            bool questionExistInDB = questions.QuestionsList.Any(q => q.Values == question);
            if (questionExistInDB)
            {
                ViewModelMessageWindow messageWindow = new ViewModelMessageWindow();
                messageWindow.SendMessage("\n\nTakie pytanie już istenieje!");
                messageWindow.OpenWindow(300, 200);
                return;
            }

            Answer answerFirst = new Answer
            {
                Id = 1,
                IsOk = answer1IsTrue,
                Name = answer1
            };

            Answer answerSecond = new Answer
            {
                Id = 2,
                IsOk = answer2IsTrue,
                Name = answer2
            };

            Answer answerThird = new Answer
            {
                Id = 3,
                IsOk = answer3IsTrue,
                Name = answer3
            };

            Answer answerFourth = new Answer
            {
                Id = 4,
                IsOk = answer4IsTrue,
                Name = answer4
            };

            answers.Add(answerFirst);
            answers.Add(answerSecond);
            answers.Add(answerThird);
            answers.Add(answerFourth);

            questions.QuestionsList.Add(
                new XML.Question()
                {
                    Id = questions.QuestionsList.Count + 1,
                    Values = question,
                    Items = answers
                });


            XML.Serialize(questions);

            Messenger.Default.Send<RefreshQuestions>(new RefreshQuestions
            {
                Refresh = true
            });

            ViewModelMessageWindow message = new ViewModelMessageWindow();
            message.SendMessage("\n\nPytanie zostało dodane.");
            message.OpenWindow(300, 200);
        }
    }
}
