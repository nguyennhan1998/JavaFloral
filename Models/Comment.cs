using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string Message { get; set; }
        public int rating { get; set; }
        public string UserID { get; set; }
        public DateTime CommentTime { get; set; }
        public int Status { get; set; }
        public ICollection<CommentProduct> CommentProducts { get; set; }
        public ICollection<CommentAnswer> CommentAnswers { get; set; }
    }
}
