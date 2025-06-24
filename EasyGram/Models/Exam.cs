using System;
using System.Collections.Generic;

namespace EasyGram.Models
{
    public class Exam
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        // Продолжительность экзамена (например, 01:30:00 = 1.5 часа)
        public TimeSpan Duration { get; set; }

        // Список задач, привязанных к экзамену
        public virtual ICollection<ExamTask> ExamTasks { get; set; } = new List<ExamTask>();
    }
}
