using System;

namespace EasyGram.Models
{
    public class Material
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } // "video" или "text"
        public string Url { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
