using System;
using TodoApp.Interfaces;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoController
    {
        private readonly ITodoService _todoService;
        
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        
        public void GetAllTodos()
        {
            Console.WriteLine("\n=== All Todos ===");
            var response = _todoService.GetAllTodos();
            
            if (response.Success)
            {
                if (response.Data.Count == 0)
                {
                    Console.WriteLine("No todos found.");
                }
                else
                {
                    foreach (var todo in response.Data)
                    {
                        Console.WriteLine(todo.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.Message}");
            }
        }
        
        public void GetTodoById(int id)
        {
            Console.WriteLine($"\n=== Todo #{id} ===");
            var response = _todoService.GetTodoById(id);
            
            if (response.Success)
            {
                Console.WriteLine(response.Data.ToString());
            }
            else
            {
                Console.WriteLine($"Error: {response.Message}");
            }
        }
        
        public void CreateTodo(string title, string priority = "Medium", string description = "")
        {
            Console.WriteLine($"\n=== Creating Todo: {title} ===");
                        var request = new CreateTodoRequest
            {
                Title = title,
                Description = description,
                Priority = priority
            };
            
            var response = _todoService.CreateTodo(request);
            
            if (response.Success)
            {
                Console.WriteLine($"✓ Todo created successfully: {response.Data.ToString()}");
            }
            else
            {
                Console.WriteLine($"✗ Error creating todo: {response.Message}");
                if (response.Errors.Count > 0)
                {
                    foreach (var error in response.Errors)
                    {
                        Console.WriteLine($"  - {error}");
                    }
                }
            }
        }
        
        public void UpdateTodo(int id, string title = null, string priority = null, bool? isCompleted = null, string description = null)
        {
            Console.WriteLine($"\n=== Updating Todo #{id} ===");
            
            var request = new UpdateTodoRequest
            {
                Title = title,
                Description = description,
                Priority = priority,
                IsCompleted = isCompleted
            };
            
            var response = _todoService.UpdateTodo(id, request);
            
            if (response.Success)
            {
                Console.WriteLine($"✓ Todo updated successfully: {response.Data.ToString()}");
            }
            else
            {
                Console.WriteLine($"✗ Error updating todo: {response.Message}");
                if (response.Errors.Count > 0)
                {
                    foreach (var error in response.Errors)
                    {
                        Console.WriteLine($"  - {error}");
                    }
                }
            }
        }
        
        public void DeleteTodo(int id)
        {
            Console.WriteLine($"\n=== Deleting Todo #{id} ===");
            
            var response = _todoService.DeleteTodo(id);
            
            if (response.Success)
            {
                Console.WriteLine("✓ Todo deleted successfully");
            }
            else
            {
                Console.WriteLine($"✗ Error deleting todo: {response.Message}");
            }
        }
        
        public void GetCompletedTodos()
        {
            Console.WriteLine("\n=== Completed Todos ===");
            var response = _todoService.GetCompletedTodos();
            
            if (response.Success)
            {
                if (response.Data.Count == 0)
                {
                    Console.WriteLine("No completed todos found.");
                }
                else
                {
                    foreach (var todo in response.Data)
                    {
                        Console.WriteLine(todo.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.Message}");
            }
        }
        
        public void GetPendingTodos()
        {
            Console.WriteLine("\n=== Pending Todos ===");
            var response = _todoService.GetPendingTodos();
            
            if (response.Success)
            {
                if (response.Data.Count == 0)
                {
                    Console.WriteLine("No pending todos found.");
                }
                else
                {
                    foreach (var todo in response.Data)
                    {
                        Console.WriteLine(todo.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.Message}");
            }
        }
        
        public void GetTodosByPriority(string priority)
        {
            Console.WriteLine($"\n=== {priority} Priority Todos ===");
            var response = _todoService.GetTodosByPriority(priority);
            
            if (response.Success)
            {
                if (response.Data.Count == 0)
                {
                    Console.WriteLine($"No {priority} priority todos found.");
                }
                else
                {
                    foreach (var todo in response.Data)
                    {
                        Console.WriteLine(todo.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.Message}");
            }
        }
    }
}