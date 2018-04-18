using Microsoft.Practices.Prism.Commands;

namespace YourMillionaires.ViewModel
{
    public class ViewModelAddQuestionTab : PropertyChangedHelper
    {
        #region Properties
        public DelegateCommand AddQuestionButton { get; set; }
        public DelegateCommand ClearAddQuestionTabButton { get; set; }

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

        private int answersCount;
        public int AnswersCount
        {
            get
            {
                return answersCount;
            }
            set
            {
                answersCount = value;
                OnPropertyChanged("AnswersCount");
            }
        }

        private bool answer1IsTrue;
        public bool Answer1IsTrue
        {
            get
            {
                return answer1IsTrue;
            }
            set
            {
                answer1IsTrue = value;
                OnPropertyChanged("Answer1IsTrue");
            }
        }

        private bool answer2IsTrue;
        public bool Answer2IsTrue
        {
            get
            {
                return answer2IsTrue;
            }
            set
            {
                answer2IsTrue = value;
                OnPropertyChanged("Answer2IsTrue");
            }
        }

        private bool answer3IsTrue;
        public bool Answer3IsTrue
        {
            get
            {
                return answer3IsTrue;
            }
            set
            {
                answer3IsTrue = value;
                OnPropertyChanged("Answer3IsTrue");
            }
        }

        private bool answer4IsTrue;
        public bool Answer4IsTrue
        {
            get
            {
                return answer4IsTrue;
            }
            set
            {
                answer4IsTrue = value;
                OnPropertyChanged("Answer4IsTrue");
            }
        }

        private string answer1;
        public string Answer1
        {
            get
            {
                return answer1;
            }
            set
            {
                answer1 = value;
                OnPropertyChanged("Answer1");
            }
        }

        private string answer2;
        public string Answer2
        {
            get
            {
                return answer2;
            }
            set
            {
                answer2 = value;
                OnPropertyChanged("Answer2");
            }
        }

        private string answer3;
        public string Answer3
        {
            get
            {
                return answer3;
            }
            set
            {
                answer3 = value;
                OnPropertyChanged("Answer3");
            }
        }

        private string answer4;
        public string Answer4
        {
            get
            {
                return answer4;
            }
            set
            {
                answer4 = value;
                OnPropertyChanged("Answer4");
            }
        }

        private bool cleanAutomatically;
        public bool CleanAutomatically
        {
            get
            {
                return cleanAutomatically;
            }
            set
            {
                cleanAutomatically = value;
                OnPropertyChanged("CleanAutomatically");
            }
        }
        #endregion

        public ViewModelAddQuestionTab()
        {
            AddQuestionButton = new DelegateCommand(AddQuestionToXML);
            ClearAddQuestionTabButton = new DelegateCommand(ClearDataFromAddQuestionTab);
        }
                
        void AddQuestionToXML()
        {
            new ModelAddQuestionTab().AddQuestion(
                Question,
                Answer1,
                Answer1IsTrue,
                Answer2,
                Answer2IsTrue,
                Answer3,
                Answer3IsTrue,
                Answer4,
                Answer4IsTrue
                );

            if (CleanAutomatically)
            {
                ClearDataFromAddQuestionTab();
            }
        }

        void ClearDataFromAddQuestionTab()
        {
            Question = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            Answer4 = string.Empty;
            Answer1IsTrue = false;
            Answer2IsTrue = false;
            Answer3IsTrue = false;
            Answer4IsTrue = false;
        }
    }
}
