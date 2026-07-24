using System;
using TodoApp.Models;

namespace TodoApp.Utils
{
    public static class ConsoleHelper
    {
        public static void PrintHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', title.Length + 8));
            Console.WriteLine($"=== {title} ===");
            Console.WriteLine(new string('=', title.Length + 8));
        }
        
        public static void PrintSuccess(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ {message}");
            Console.ForegroundColor = originalColor;
        }
        
        public static void PrintError(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ {message}");
            Console.ForegroundColor = originalColor;
        }
        
        public static void PrintWarning(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠ {message}");
            Console.ForegroundColor = originalColor;
        }
        
        public static void PrintTodo(Todo todo)
        {
            var statusColor = todo.IsCompleted ? ConsoleColor.Green : ConsoleColor.Yellow;
            var originalColor = Console.ForegroundColor;
            
            Console.ForegroundColor = statusColor;
            var status = todo.IsCompleted ? "✓" : "○";
            Console.Write($"[{todo.Id}] {status} ");
            
            Console.ForegroundColor = originalColor;
            Console.Write($"{todo.Title} ");
            
            // Priority color coding
            var priorityColor = GetPriorityColor(todo.Priority);
            Console.ForegroundColor = priorityColor;
            Console.Write($"({todo.Priority}) ");
            
            Console.ForegroundColor = originalColor;
            Console.WriteLine($"- {(todo.IsCompleted ? "Completed" : "Pending")}");
            
            if (!string.IsNullOrEmpty(todo.Description))
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"    Description: {todo.Description}");
                Console.ForegroundColor = originalColor;
            }
        }
        
        private static ConsoleColor GetPriorityColor(string priority)
        {
            switch (priority?.ToLower())
            {
                case "high":
                    return ConsoleColor.Red;
                case "medium":
                    return ConsoleColor.Yellow;
                case "low":
                    return ConsoleColor.Green;
                default:
                    return ConsoleColor.White;
            }
        }
        
        public static void PrintSeparator()
        {
            Console.WriteLine(new string('-', 50));
        }
        
        public static void WaitForKeyPress(string message = "Press any key to continue...")
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}