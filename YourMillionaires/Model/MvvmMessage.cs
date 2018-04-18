using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMillionaires.Model
{
    class WindowMessage
    {
        public string Message { get; set; }
        public int AnswerId { get; set; }
        public int  QuestionId { get; set; }
        public bool IsQuestion { get; set; }
    }

    class AutoReplyMessage
    {
        public bool Message { get; set; }
    }

    class RefreshQuestions
    {
        public bool Refresh { get; set; }
    }
}
