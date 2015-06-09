using System.ComponentModel.DataAnnotations;

namespace farmApi.Models
{
    public class TodoItem : FarmEntity
    {
        [Required]
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}
