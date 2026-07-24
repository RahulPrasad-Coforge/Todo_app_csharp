using System;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        
        public Todo()
        {
            CreatedAt = DateTime.Now;
            IsCompleted = false;
        }
        
        public Todo(string title, string description = "", string priority = "Medium") : this()
        {
            Title = title;
            Description = description;
            Priority = priority;
        }
        
        public void MarkAsCompleted()
        {
            IsCompleted = true;
            CompletedAt = DateTime.Now;
        }
        
        public void MarkAsIncomplete()
        {
            IsCompleted = false;
            CompletedAt = null;
        }
        
        public override string ToString()
        {
            var status = IsCompleted ? "✓" : "○";
            return $"[{Id}] {status} {Title} ({Priority}) - {(IsCompleted ? "Completed" : "Pending")}";
        }
    }
}