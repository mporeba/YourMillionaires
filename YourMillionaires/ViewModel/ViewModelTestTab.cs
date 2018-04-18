using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using YourMillionaires.DialogWindows.ViewModel;
using YourMillionaires.Model;

namespace YourMillionaires.ViewModel
{
    public class ViewModelTestTab : PropertyChangedHelper
    {
        private int questionsMax;
        public int QuestionsMax
        {
            get
            {
                return questionsMax;
            }
            set
            {
                questionsMax = value;
                this.IsDisabledAfterStart = true;
            }
        }

        public DelegateCommand StartButton { get; set; }
        public DelegateCommand NextButton { get; set; }
        public DelegateCommand StartAgainTestButton { get; set; }

        private string questionsCounter;
        public string QuestionsCounter
        {
            get
            {
                return questionsCounter;
            }
            set
            {
                questionsCounter = value;
                OnPropertyChanged("QuestionsCounter");
            }
        }

        private string question;
        public string Question
        {
            get
            {
                return question;
            }
            set
            {
                question = value;
                OnPropertyChanged("Question");
            }
        }

        private string nextButtonName = "Dalej";
        public string NextButtonName
        {
            get
            {
                return nextButtonName;
            }
            set
            {
                nextButtonName = value;
                OnPropertyChanged("NextButtonName");
            }
        }

        public ObservableCollection<Answer> answers;
        public ObservableCollection<Answer> Answers
        {
            get
            {
                return answers;
            }
            set
            {
                answers = value;
                OnPropertyChanged("Answers");
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

                if (answer != null)
                {
                    answer.Marked = true;
                }
            }
        }

        private bool isEnabledTrueAfterStart = false;
        public bool IsEnabledAfterStart
        {
            get
            {
                return isEnabledTrueAfterStart;
            }
            set
            {
                isEnabledTrueAfterStart = value;
                OnPropertyChanged("IsEnabledTrueAfterStart");
            }
        }

        private bool autoReplies;
        public bool AutoReplies
        {
            get
            {
                return autoReplies;
            }
            set
            {
                autoReplies = value;
                OnPropertyChanged("AutoReplies");
            }
        }

        private bool isEnabledFalseAfterStart = true;
        public bool IsDisabledAfterStart
        {
            get
            {
                return isEnabledFalseAfterStart;
            }
            set
            {
                isEnabledFalseAfterStart = value;
                OnPropertyChanged("IsEnabledFalseAfterStart");
            }
        }

        List<object> startAgainProperties;
        List<XML.Question> questionsForOneTest;
        int counter = 0;
        bool clickStartAgain = false;

        public ViewModelTestTab()
        {
            StartButton = new DelegateCommand(Start);
            NextButton = new DelegateCommand(NextQuestion);
            StartAgainTestButton = new DelegateCommand(StartAgain);
        }

        void Start()
        {
            if (QuestionsMax > 0)
            {
                if (clickStartAgain)
                {
                    counter = 0;
                    NextButtonName = "Next";
                }

                clickStartAgain = true;
                IsEnabledAfterStart = true;
                IsDisabledAfterStart = false;
                
                var allQuestions = new XML().Deserialize().QuestionsList;

                if (QuestionsMax > allQuestions.Count)
                {
                    string msg = string.Concat("\n\nNie ma tyle pytań w bazie, ilość pytań w bazie: ", allQuestions.Count, ".");
                    ViewModelMessageWindow message = new ViewModelMessageWindow();
                    message.SendMessage(msg);
                    message.OpenWindow(300, 200);
                    return;
                }

                Random randomQuestions = new Random();
                List<XML.Question> mixedQuestions = allQuestions
                    .OrderBy(e => randomQuestions.Next())
                    .ToList();

                questionsForOneTest = mixedQuestions
                    .Take(QuestionsMax)
                    .ToList();

                startAgainProperties = new List<object>
                {
                    QuestionsMax,
                    AutoReplies,
                    questionsForOneTest
                };

                NextQuestion();

                Messenger.Default.Send<AutoReplyMessage>(new AutoReplyMessage
                {
                    Message = AutoReplies
                });
            }
            else
            {
                ViewModelMessageWindow message = new ViewModelMessageWindow();
                message.SendMessage("\n\nNiepoprawna ilość pytań!");
                message.OpenWindow(300, 200);
            }
        }

        void NextQuestion()
        {
            //jeśli ktoś ma auto odpowiedzi i mozliwa jest więcej niz jedna odp
            //i ktośnie zaznaczył wszystkich to po przycisnieciu dalej
            //powinno sie podświetlić odp na jakiś kolor
            //i po ponownym przycisnieciu next dalsze pytanie

            if (counter < QuestionsMax)
            {
                QuestionsCounter = string.Concat("Pytanie ", counter + 1, "/", QuestionsMax.ToString());
                Question = questionsForOneTest[counter].Values;

                //mix answers
                Random randomAnswers = new Random();
                var mixAnswers = questionsForOneTest[counter]
                    .Items
                    .OrderBy(q => randomAnswers.Next())
                    .ToList();
                Answers = new ObservableCollection<Answer>(mixAnswers);

                counter++;

                if (counter == QuestionsMax)
                {
                    NextButtonName = "Koniec";
                }
            }
            else
            {
                Results();
                IsDisabledAfterStart = true;
            }
        }

        void StartAgain()
        {
            if (isEnabledTrueAfterStart)
            {
                QuestionsMax = (int)startAgainProperties[0];
                AutoReplies = (bool)startAgainProperties[1];
                questionsForOneTest = (List<XML.Question>)startAgainProperties[2];
                counter = 0;
                NextButtonName = "Next";

                NextQuestion();
            }
        }

        void Results()
        {
            new ModelTestTab()
                .GenerateRaport(questionsForOneTest);
        }
    }
}
