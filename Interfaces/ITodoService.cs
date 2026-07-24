using System.Collections.Generic;
using TodoApp.Models;

namespace TodoApp.Interfaces
{
    public interface ITodoService
    {
        ApiResponse<List<Todo>> GetAllTodos();
        ApiResponse<Todo> GetTodoById(int id);
        ApiResponse<Todo> CreateTodo(CreateTodoRequest request);
        ApiResponse<Todo> UpdateTodo(int id, UpdateTodoRequest request);
        ApiResponse<bool> DeleteTodo(int id);
        ApiResponse<List<Todo>> GetCompletedTodos();
        ApiResponse<List<Todo>> GetPendingTodos();
        ApiResponse<List<Todo>> GetTodosByPriority(string priority);
    }
}