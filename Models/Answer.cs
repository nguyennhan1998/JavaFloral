using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public string UserID { get; set; }
        public DateTime AnswerTime { get; set; }
        public ICollection<CommentAnswer> CommentAnswers { get; set; }
    }
}
