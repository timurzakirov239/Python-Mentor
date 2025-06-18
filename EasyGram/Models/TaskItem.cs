using System;

namespace EasyGram.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxScore { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
