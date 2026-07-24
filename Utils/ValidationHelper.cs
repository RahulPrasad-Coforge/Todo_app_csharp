using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp.Utils
{
    public static class ValidationHelper
    {
        private static readonly string[] ValidPriorities = { "High", "Medium", "Low" };
        
        public static bool IsValidPriority(string priority)
        {
            if (string.IsNullOrWhiteSpace(priority))
                return false;
                
            return ValidPriorities.Contains(priority, StringComparer.OrdinalIgnoreCase);
        }
        
        public static bool IsValidTitle(string title)
        {
            return !string.IsNullOrWhiteSpace(title) && title.Length <= 200;
        }
        
        public static bool IsValidDescription(string description)
        {
            return description == null || description.Length <= 1000;
        }
        
        public static List<string> ValidateCreateTodo(string title, string description, string priority)
        {
            var errors = new List<string>();
            
            if (!IsValidTitle(title))
            {
                errors.Add("Title is required and must be less than 200 characters");
            }
            
            if (!IsValidDescription(description))
            {
                errors.Add("Description must be less than 1000 characters");
            }
            
            if (!IsValidPriority(priority))
            {
                errors.Add("Priority must be High, Medium, or Low");
            }
            
            return errors;
        }
        
        public static string NormalizePriority(string priority)
        {
            if (string.IsNullOrWhiteSpace(priority))
                return "Medium";
                
            return ValidPriorities.FirstOrDefault(p => 
                p.Equals(priority, StringComparison.OrdinalIgnoreCase)) ?? "Medium";
        }
    }
}