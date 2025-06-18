using System;
using System.Collections.Generic;

namespace EasyGram.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Deadline { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TaskItem> Tasks { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
