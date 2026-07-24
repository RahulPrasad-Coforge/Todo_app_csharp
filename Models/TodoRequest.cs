namespace TodoApp.Models
{
    public class CreateTodoRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } = "Medium";
    }
    
    public class UpdateTodoRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public bool? IsCompleted { get; set; }
    }
}