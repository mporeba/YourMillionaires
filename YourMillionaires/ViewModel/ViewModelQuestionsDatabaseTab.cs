using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using YourMillionaires.Model;

namespace YourMillionaires.ViewModel
{
    public class ViewModelQuestionsDatabaseTab : PropertyChangedHelper
    {
        public DelegateCommand RemoveQuestionButton { get; set; }
        public DelegateCommand ChangeQuestionButton { get; set; }
        public DelegateCommand RemoveAnswerButton { get; set; }
        public DelegateCommand ChangeAnswerButton { get; set; }

        private ObservableCollection<XML.Question> questions;
        public ObservableCollection<XML.Question> Questions
        {
            get
            {
                return questions;
            }
            set
            {
                if (questions != value)
                {
                    questions = value;
                    OnPropertyChanged("Questions");
                }
            }
        }

        private XML.Question question;
        public XML.Question Question
        {
            get
            {
                return question;
            }
            set
            {
                if (question != value)
                {
                    question = value;

                    if (Question != null)
                    {
                        Answers = new ObservableCollection<Answer>(Question.Items);
                    }

                    OnPropertyChanged("Question");
                }

            }
        }

        public ObservableCollection<Answer> answers { get; set; }
        public ObservableCollection<Answer> Answers
        {
            get
            {
                return answers;
            }
            set
            {
                if (answers != value)
                {
                    answers = value;
                    OnPropertyChanged("Answers");
                }
            }
        }

        private Answer answer;
        public Answer Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }

        public static string id = string.Empty;
        ModelQuestionDatabaseTab modelQuestionDatabaseTab;

        public ViewModelQuestionsDatabaseTab()
        {
            RemoveQuestionButton = new DelegateCommand(RemoveQuestion);
            ChangeQuestionButton = new DelegateCommand(ChangeQuestion);
            RemoveAnswerButton = new DelegateCommand(RemoveAnswer);
            ChangeAnswerButton = new DelegateCommand(ChangeAnswer);

            Messenger.Default.Register<WindowMessage>(this, this.GetMvvmMessageChangeQuestionOrAnswer);
            Messenger.Default.Register<RefreshQuestions>(this, this.GetMvvmRefreshQuestions);
            LoadData();
        }

        void RemoveQuestion()
        {
            modelQuestionDatabaseTab = new ModelQuestionDatabaseTab(Questions, Question, Answers, Answer);
            this.Questions = modelQuestionDatabaseTab.RemoveQuestion();
            this.Answers = new ObservableCollection<Answer>();
        }

        void ChangeQuestion()
        {
            modelQuestionDatabaseTab = new ModelQuestionDatabaseTab(Questions, Question, Answers, Answer);
            modelQuestionDatabaseTab.ChangeQuestion();
        }

        void RemoveAnswer()
        {

        }

        void ChangeAnswer()
        {
            modelQuestionDatabaseTab = new ModelQuestionDatabaseTab(Questions, Question, Answers, Answer);
            modelQuestionDatabaseTab.ChangeAnswer();
        }

        void GetMvvmMessageChangeQuestionOrAnswer(WindowMessage message)
        {
            modelQuestionDatabaseTab = new ModelQuestionDatabaseTab(Questions, Question, Answers, Answer);

            if (message.IsQuestion)
            {
                modelQuestionDatabaseTab.MvvmMessageChangeQuestion(message.QuestionId, message.Message);
            }
            else
            {
                this.Answers = modelQuestionDatabaseTab.MvvmMessageChangeAnswer(message.QuestionId, message.AnswerId, message.Message);
            }

            LoadData();
        }

        void GetMvvmRefreshQuestions(RefreshQuestions message)
        {
            if (message.Refresh)
            {
                LoadData();
            }
        }

        void LoadData()
        {
            modelQuestionDatabaseTab = new ModelQuestionDatabaseTab();
            this.Questions = modelQuestionDatabaseTab.LoadData();
        }
    }
}
