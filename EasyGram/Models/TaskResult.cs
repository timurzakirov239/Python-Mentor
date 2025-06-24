using System;

namespace EasyGram.Models
{
    public class TaskResult
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual Users User { get; set; }

        public int TaskItemId { get; set; }
        public virtual TaskItem TaskItem { get; set; }

        public bool IsCorrect { get; set; }
        public int Score { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
