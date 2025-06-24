using System;
using System.Collections.Generic;

namespace EasyGram.ViewModels
{
    public class LessonTasksViewModel
    {
        public int LessonId { get; set; }
        public string LessonTitle { get; set; }
        public string LessonDescription { get; set; }
        public DateTime? Deadline { get; set; }
        public List<TaskStatusViewModel> Tasks { get; set; }
        public List<MaterialViewModel> Materials { get; set; }
    }

    public class TaskStatusViewModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int MaxScore { get; set; }

        // Добавлено для отображения зелёной галочки
        public bool IsCompleted { get; set; }
    }

    public class MaterialViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
