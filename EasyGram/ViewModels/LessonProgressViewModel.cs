namespace EasyGram.ViewModels
{
    public class LessonProgressViewModel
    {
        public int LessonId { get; set; }
        public string LessonTitle { get; set; }
        public int TotalTasks { get; set; }
        public int SolvedTasks { get; set; }
        public int ProgressPercent => TotalTasks > 0 ? (int)Math.Round(SolvedTasks * 100.0 / TotalTasks) : 0;
    }
}
