using System;

namespace Shop.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException()
        : base("This user never been registered in this app") { }
}