using System;

namespace EasyGram.ViewModels
{
    public class TaskDetailsViewModel
    {
        public int Id { get; set; }                 // Id задачи
        public string Title { get; set; }           // Название
        public string Description { get; set; }     // Текст условия (HTML можно)
        public int MaxScore { get; set; }           // Максимум баллов

        public string UserCode { get; set; }        // Последний введённый код пользователя
        public string JudgeResult { get; set; }     // Результат проверки (или вывод компилятора)
        public DateTime? SubmissionTime { get; set; } // Дата последней отправки (опционально)

        // Если позже потребуется "История отправок" — можно добавить сюда List<SubmissionViewModel>
    }
}
