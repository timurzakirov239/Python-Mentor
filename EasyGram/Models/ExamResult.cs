using System;

namespace EasyGram.Models
{
    public class ExamResult
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string UserId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int Score { get; set; }
        public bool IsPassed { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual Users User { get; set; }
    }
}
