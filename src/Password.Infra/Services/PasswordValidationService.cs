using System;
using Password.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Password.Infra.Services
{
    public class PasswordValidationService:IPassword
    {
        public bool IsValid(string passwordString)
        {
            Regex rx = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{9,}$");
            return rx.IsMatch(passwordString);            
        }

    }
}