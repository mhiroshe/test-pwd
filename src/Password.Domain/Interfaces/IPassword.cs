using System;

namespace Password.Domain.Interfaces
{
    public interface IPassword
    {
        bool IsValid(string passwordString);
    }
}