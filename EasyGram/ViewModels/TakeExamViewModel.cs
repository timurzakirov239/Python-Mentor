using System.Collections.Generic;

namespace EasyGram.ViewModels
{
    public class TakeExamViewModel
    {
        public int ExamId { get; set; }
        public int DurationMinutes { get; set; }
        public List<TaskStatusViewModel> Tasks { get; set; }
    }
}
