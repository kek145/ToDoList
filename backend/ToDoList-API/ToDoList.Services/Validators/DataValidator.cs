﻿using System.Text.RegularExpressions;

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

        public bool CheckWhitespace(string username, string email, string password)
        {
            if (username.Contains(" ") || email.Contains(" ") || password.Contains(" "))
                return false;
            else return true;
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
            else return true;
        }

        public bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
