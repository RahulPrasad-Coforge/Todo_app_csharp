using System;
using System.Collections.Generic;
using System.Linq;
using TodoApp.Interfaces;
using TodoApp.Models;

namespace TodoApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private List<Todo> _todos;
        private int _nextId;
        
        public TodoRepository()
        {
            _todos = new List<Todo>();
            _nextId = 1;
        }
        
        public List<Todo> GetAll()
        {
            return _todos.ToList();
        }
        
        public Todo GetById(int id)
        {
            return _todos.FirstOrDefault(t => t.Id == id);
        }
        
        public Todo Create(Todo todo)
        {
            todo.Id = _nextId++;
            _todos.Add(todo);
            return todo;
        }
        
        public Todo Update(Todo todo)
        {
            var existingTodo = GetById(todo.Id);
            if (existingTodo != null)
            {
                existingTodo.Title = todo.Title;
                existingTodo.Description = todo.Description;
                existingTodo.Priority = todo.Priority;
                existingTodo.IsCompleted = todo.IsCompleted;
                existingTodo.CompletedAt = todo.CompletedAt;
                return existingTodo;
            }
            return null;
        }
        
        public bool Delete(int id)
        {
            var todo = GetById(id);
            if (todo != null)
            {
                _todos.Remove(todo);
                return true;
            }
            return false;
        }
        
        public List<Todo> GetByStatus(bool isCompleted)
        {
            return _todos.Where(t => t.IsCompleted == isCompleted).ToList();
        }
        
        public List<Todo> GetByPriority(string priority)
        {
            return _todos.Where(t => t.Priority.Equals(priority, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}