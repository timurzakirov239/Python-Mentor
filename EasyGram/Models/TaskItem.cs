using System;
using System.Collections.Generic;

namespace EasyGram.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Title { get; set; }               // Название задачи
        public string Description { get; set; }         // Описание/условие
        public int MaxScore { get; set; }               // Максимум баллов
        public int? Order { get; set; }                 // Порядок задач в уроке (опционально)

        // var deadline = taskItem.Lesson.Deadline;  дедлайн урока не задания
        public virtual Lesson Lesson { get; set; }      // Связь с уроком

        // 🔽 Новое: Связь с тестами для автопроверки
        public virtual ICollection<TaskTest> Tests { get; set; } = new List<TaskTest>();
    }
}
