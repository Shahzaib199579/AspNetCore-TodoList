using System.ComponentModel.DataAnnotations;

namespace Todo_List_Project.Dtos
{
    public class TodoDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string State { get; set; }
    }
}