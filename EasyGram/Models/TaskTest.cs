using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGram.Models
{
    public class TaskTest
    {
        public int Id { get; set; }

        [Required]
        public string Input { get; set; }

        [Required]
        public string ExpectedOutput { get; set; }

        public int TaskItemId { get; set; }

        [ForeignKey("TaskItemId")]
        public virtual TaskItem TaskItem { get; set; }
    }
}
