namespace EasyGram.ViewModels.Exam
{
    public class ExamViewModel
    {
        public string ExamTitle { get; set; }
        public string ExamDescription { get; set; }
        public TimeSpan Duration { get; set; }
        public List<ExamTaskViewModel> Tasks { get; set; }
    }

    public class ExamTaskTestViewModel
    {
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }
        


    }

    public class ExamTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxScore { get; set; }
        public string UserCode { get; set; }
        public List<ExamTaskTestViewModel> Tests { get; set; }

        public bool IsCompleted { get; set; }  // <-- добавь сюда!
    }

}
