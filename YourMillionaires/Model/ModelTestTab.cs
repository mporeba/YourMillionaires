using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMillionaires.DialogWindows.ViewModel;

namespace YourMillionaires.Model
{
    class ModelTestTab
    {
        public void GenerateRaport(List<XML.Question> questions)
        {
            string report = string.Empty;

            for (int i = 0; i < questions.Count; i++)
            {
                int questionNumber = i + 1;

                var correctAnswers = questions[i].Items
                    .Where(q => q.IsOk == true && q.Marked == true)
                    .ToArray();

                var wrongAnswers = questions[i].Items
                    .Where(q => q.IsOk == false && q.Marked == true)
                    .ToArray();

                var allCorrectAnswers = questions[i].Items
                    .Where(q => q.IsOk == true)
                    .ToArray();

                report = string.Concat(
                    report,
                    ResultReport(
                        correctAnswers,
                        wrongAnswers,
                        allCorrectAnswers,
                        questionNumber,
                        questions[i]),
                    "\n\n"
                    );
            }

            ViewModelMessageWindow message = new ViewModelMessageWindow();
            message.SendMessage(report);
            message.OpenWindow(500, 500);
        }

        string ResultReport(Answer[] correctAnswers, Answer[] wrongAnswers, Answer[] allCorrectAnswers, int questionNumber, XML.Question item)
        {
            string message = string.Empty;
            string answerHeading = "Odp: ";
            string question = string.Concat(questionNumber.ToString(), ". ", item.Values);

            message = question;

            foreach (var answer in correctAnswers)
            {
                string correct = string.Concat(
                    answerHeading,
                    answer.Name,
                    "\t",
                    "CORRECT"
                    );

                message = string.Concat(message, "\n", correct);
            }

            foreach (var answer in wrongAnswers)
            {
                string wrong = string.Concat(
                    answerHeading,
                    answer.Name,
                    "\t",
                    "WRONG"
                    );

                message = string.Concat(message, "\n", wrong);
            }

            Answer[] isAllCorrectAnswer = allCorrectAnswers.Except(correctAnswers, new Comparer()).ToArray();

            if (isAllCorrectAnswer.Any())
            {
                message = string.Concat(message, "\n\nCorrect answers:");
                foreach (var answer in allCorrectAnswers)
                {
                    string correct = string.Concat(
                        answerHeading,
                        answer.Name
                        );

                    message = string.Concat(message, "\n", correct);
                }
            }

            return message;
        }
    }

    public class Comparer : IEqualityComparer<Answer>
    {
        public bool Equals(Answer x, Answer y)
        {
            return x == y;
        }

        public int GetHashCode(Answer obj)
        {
            return obj.GetHashCode();
        }
    }
}
