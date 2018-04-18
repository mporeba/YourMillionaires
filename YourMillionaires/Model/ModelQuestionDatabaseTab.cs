using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMillionaires.DialogWindows.ViewModel;

namespace YourMillionaires.Model
{
    public class ModelQuestionDatabaseTab
    {
        ObservableCollection<XML.Question> questions;
        XML.Question question;
        ObservableCollection<Answer> answers;
        Answer answer;

        public static string id = string.Empty;

        public ModelQuestionDatabaseTab(
            ObservableCollection<XML.Question> questions,
            XML.Question question,
            ObservableCollection<Answer> answers,
            Answer answer
            )
        {
            this.questions = questions;
            this.question = question;
            this.answers = answers;
            this.answer = answer;
        }

        public ModelQuestionDatabaseTab()
        { }

        public ObservableCollection<XML.Question> RemoveQuestion()
        {
            if (question != null)
            {
                var questionsList = ConverQuestionsToList();

                int id = question.Id - 1;
                questionsList.RemoveAt(id);

                //update question id
                for (int i = 0; i < questionsList.Count; i++)
                {
                    questionsList[i].Id = i + 1;
                }

                XML newXmlData = new XML();
                newXmlData.QuestionsList = questionsList;

                XML.Serialize(newXmlData);
                LoadData();
            }
            else
            {
                ViewModelMessageWindow message = new ViewModelMessageWindow();
                message.SendMessage("\n\nNie wybrałeś pytania!");
                message.OpenWindow(300, 200);
            }

            return questions;
        }

        public void ChangeQuestion()
        {
            if (question != null)
            {
                id = string.Concat(question.Id, ";", 0);

                ViewModelReplaceWindow replaceWindow = new ViewModelReplaceWindow();
                replaceWindow.SendData(question.Values, ConverQuestionsToList(), id);
                replaceWindow.OpenWindow(300, 200);
            }
            else
            {
                ViewModelMessageWindow message = new ViewModelMessageWindow();
                message.SendMessage("\n\nNie wybrałeś pytania!");
                message.OpenWindow(300, 200);
            }
        }

        public void ChangeAnswer()
        {
            if (answer != null)
            {
                if (question != null)
                {
                    id = string.Concat(question.Id, ";", answer.Id);
                }
                else
                {
                    id = string.Concat(
                        id.Split(';').FirstOrDefault(),
                        ";",
                        answer.Id
                        );
                }

                ViewModelReplaceWindow replaceWindow = new ViewModelReplaceWindow();
                replaceWindow.SendData(answer.Name, ConverQuestionsToList(), id);
                replaceWindow.OpenWindow(300, 200);
            }
            else
            {
                ViewModelMessageWindow message = new ViewModelMessageWindow();
                message.SendMessage("\n\nNie wybrałeś odpowiedzi!");
                message.OpenWindow(300, 200);
            }
        }

        public void MvvmMessageChangeQuestion(int questionId, string message)
        {
            int id = questionId;
            if (questions != null)
            {
                foreach (var item in questions)
                {
                    if (item.Id == id && question != null)
                    {
                        item.Values = message;

                        SaveData();
                        break;
                    }
                }
            }
        }

        public ObservableCollection<Answer> MvvmMessageChangeAnswer(int questionId, int answerId, string message)
        {
            var selectedQuestion = questions
                    .Where(q => q.Id == questionId)
                    .FirstOrDefault();

            foreach (var item in selectedQuestion.Items)
            {
                if (item.Id == answerId)
                {
                    item.Name = message;

                    answers = ConverAnswersToObservableColection(selectedQuestion.Items);

                    SaveData();
                    break;
                }
            }

            return answers;
        }

        public ObservableCollection<XML.Question> LoadData()
        {
            var questionFromXML = new XML().Deserialize().QuestionsList;
            questions = new ObservableCollection<XML.Question>(questionFromXML);

            return questions;
        }

        void SaveData()
        {
            XML newXmlData = new XML();
            var questionsList = ConverQuestionsToList();
            newXmlData.QuestionsList = questionsList;

            XML.Serialize(newXmlData);
        }

        List<XML.Question> ConverQuestionsToList()
        {
            IEnumerable<XML.Question> collection = questions;
            List<XML.Question> questionsList = new List<XML.Question>(collection);
            return questionsList;
        }

        ObservableCollection<Answer> ConverAnswersToObservableColection(List<Answer> answers)
        {
            var oc = new ObservableCollection<Answer>();
            foreach (var item in answers)
                oc.Add(item);

            return oc;
        }
    }
}
