using System.Collections.Generic;
using TodoApp.Models;

namespace TodoApp.Interfaces
{
    public interface ITodoRepository
    {
        List<Todo> GetAll();
        Todo GetById(int id);
        Todo Create(Todo todo);
        Todo Update(Todo todo);
        bool Delete(int id);
        List<Todo> GetByStatus(bool isCompleted);
        List<Todo> GetByPriority(string priority);
    }
}