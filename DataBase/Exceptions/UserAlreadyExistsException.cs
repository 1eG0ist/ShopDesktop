using System;

namespace Shop.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException()
        : base("This user already registered in this app") { }
}