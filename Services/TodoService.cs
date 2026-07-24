using System;
using System.Collections.Generic;
using System.Linq;
using TodoApp.Interfaces;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        
        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }
        
        public ApiResponse<List<Todo>> GetAllTodos()
        {
            try
            {
                var todos = _repository.GetAll();
                return ApiResponse<List<Todo>>.SuccessResponse(todos);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<Todo>>.ErrorResponse($"Error retrieving todos: {ex.Message}");
            }
        }
        
        public ApiResponse<Todo> GetTodoById(int id)
        {
            try
            {
                var todo = _repository.GetById(id);
                if (todo == null)
                {
                    return ApiResponse<Todo>.ErrorResponse("Todo not found");
                }
                return ApiResponse<Todo>.SuccessResponse(todo);
            }
            catch (Exception ex)
            {
                return ApiResponse<Todo>.ErrorResponse($"Error retrieving todo: {ex.Message}");
            }
        }
        
        public ApiResponse<Todo> CreateTodo(CreateTodoRequest request)
        {
            try
            {
                var validationErrors = ValidateCreateRequest(request);
                if (validationErrors.Any())
                {
                    return ApiResponse<Todo>.ErrorResponse("Validation failed", validationErrors);
                }
                
                var todo = new Todo(request.Title, request.Description, request.Priority);
                var createdTodo = _repository.Create(todo);
                return ApiResponse<Todo>.SuccessResponse(createdTodo, "Todo created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<Todo>.ErrorResponse($"Error creating todo: {ex.Message}");
            }
        }
        
        public ApiResponse<Todo> UpdateTodo(int id, UpdateTodoRequest request)
        {
            try
            {
                var existingTodo = _repository.GetById(id);
                if (existingTodo == null)
                {
                    return ApiResponse<Todo>.ErrorResponse("Todo not found");
                }
                
                var validationErrors = ValidateUpdateRequest(request);
                if (validationErrors.Any())
                {
                    return ApiResponse<Todo>.ErrorResponse("Validation failed", validationErrors);
                }
                
                // Update properties
                if (!string.IsNullOrEmpty(request.Title))
                    existingTodo.Title = request.Title;
                if (!string.IsNullOrEmpty(request.Description))
                    existingTodo.Description = request.Description;
                if (!string.IsNullOrEmpty(request.Priority))
                    existingTodo.Priority = request.Priority;
                if (request.IsCompleted.HasValue)
                {
                    if (request.IsCompleted.Value && !existingTodo.IsCompleted)
                        existingTodo.MarkAsCompleted();
                    else if (!request.IsCompleted.Value && existingTodo.IsCompleted)
                        existingTodo.MarkAsIncomplete();
                }
                
                var updatedTodo = _repository.Update(existingTodo);
                return ApiResponse<Todo>.SuccessResponse(updatedTodo, "Todo updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<Todo>.ErrorResponse($"Error updating todo: {ex.Message}");
            }
        }
        
        public ApiResponse<bool> DeleteTodo(int id)
        {
            try
            {
                var deleted = _repository.Delete(id);
                if (!deleted)
                {
                    return ApiResponse<bool>.ErrorResponse("Todo not found");
                }
                return ApiResponse<bool>.SuccessResponse(true, "Todo deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse($"Error deleting todo: {ex.Message}");
            }
        }
        
        public ApiResponse<List<Todo>> GetCompletedTodos()
        {
            try
            {
                var todos = _repository.GetByStatus(true);
                return ApiResponse<List<Todo>>.SuccessResponse(todos);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<Todo>>.ErrorResponse($"Error retrieving completed todos: {ex.Message}");
            }
        }
        
        public ApiResponse<List<Todo>> GetPendingTodos()
        {
            try
            {
                var todos = _repository.GetByStatus(false);
                return ApiResponse<List<Todo>>.SuccessResponse(todos);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<Todo>>.ErrorResponse($"Error retrieving pending todos: {ex.Message}");
            }
        }
        
        public ApiResponse<List<Todo>> GetTodosByPriority(string priority)
        {
            try
            {
                var todos = _repository.GetByPriority(priority);
                return ApiResponse<List<Todo>>.SuccessResponse(todos);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<Todo>>.ErrorResponse($"Error retrieving todos by priority: {ex.Message}");
            }
        }
        
        private List<string> ValidateCreateRequest(CreateTodoRequest request)
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(request.Title))
                errors.Add("Title is required");
            
            if (!IsValidPriority(request.Priority))
                errors.Add("Priority must be High, Medium, or Low");
                
            return errors;
        }
        
        private List<string> ValidateUpdateRequest(UpdateTodoRequest request)
        {
            var errors = new List<string>();
            
            if (!string.IsNullOrEmpty(request.Priority) && !IsValidPriority(request.Priority))
                errors.Add("Priority must be High, Medium, or Low");
                
            return errors;
        }
        
        private bool IsValidPriority(string priority)
        {
            if (string.IsNullOrEmpty(priority)) return true;
            var validPriorities = new[] { "High", "Medium", "Low" };
            return validPriorities.Contains(priority, StringComparer.OrdinalIgnoreCase);
        }
    }
}