using System.Text.RegularExpressions;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Services.Models.Dto;

namespace ToDoList.Services.Validators
{
    public class DataValidator
    {
        public bool ValidateFieldsNotEmpty(string username, string email, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;

            return true;
        }

        public bool CheckWhitespace(string username, string email, string password, string confirmPassword)
        {
            if (username.Contains(" ") || email.Contains(" ") || password.Contains(" ") || confirmPassword.Contains(" "))
                return false;
            return true;
        }

        public bool ContainsRussian(string username, string email, string password)
        {
            Regex russianAlphabet = new Regex("[а-яёА-ЯЁ]");

            if (russianAlphabet.IsMatch(username))
                return false;
            else if (russianAlphabet.IsMatch(email))
                return false;
            else if(russianAlphabet.IsMatch(password))
                return false;
            return true;
        }

        public bool IsEmailValid(string email)
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        public bool TaskValidation(TaskDto taskDto)
        {
            if (string.IsNullOrEmpty(taskDto.Title) || string.IsNullOrEmpty(taskDto.Description))
                return false;
            if (taskDto.Priority != Priority.Easy && 
                taskDto.Priority != Priority.Medium &&
                taskDto.Priority != Priority.Hard)
                return false;
            return true;
        }
    }
}