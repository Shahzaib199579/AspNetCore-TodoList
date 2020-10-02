namespace Todo_List_Project.Models
{
    public class Todo
    {
        public int Id { get; set;}
        public string Title { get; set; }
        public string State { get; set; }
        public bool isCompleted { get; set; }
    }
}