using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.Models
{
    public class CommentAnswer
    {
        public int CommentID { get; set; }
        public int AnswerID { get; set; }
        public Answer Answer { get; set; }
        public Comment Comment { get; set; }
    }
}
