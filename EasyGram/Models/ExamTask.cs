using EasyGram.Models;

public class ExamTask
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public int MaxScore { get; set; }

    // связь с экзаменом
    public int ExamId { get; set; }
    public Exam Exam { get; set; }

    // список тестов
    public ICollection<ExamTaskTest> Tests { get; set; }
}
