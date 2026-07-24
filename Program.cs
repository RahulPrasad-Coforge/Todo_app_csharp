using System;
using TodoApp.Services;
using TodoApp.Repositories;
using TodoApp.Models;

namespace TodoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== TODO Application Backend ===");
            
            var repository = new TodoRepository();
            var service = new TodoService(repository);
            var controller = new TodoController(service);
            
            // Demo usage
            RunDemo(controller);
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        
        static void RunDemo(TodoController controller)
        {
            // Create some todos
            controller.CreateTodo("Learn .NET Framework", "High");
            controller.CreateTodo("Build TODO app", "Medium");
            controller.CreateTodo("Write documentation", "Low");
            
            // Display all todos
            controller.GetAllTodos();
            
            // Update a todo
            controller.UpdateTodo(1, "Learn .NET Framework - Completed!", "High", true);
            
            // Display updated list
            Console.WriteLine("\n--- After Update ---");
            controller.GetAllTodos();
            
            // Delete a todo
            controller.DeleteTodo(2);
            
            Console.WriteLine("\n--- After Delete ---");
            controller.GetAllTodos();
        }
    }
}