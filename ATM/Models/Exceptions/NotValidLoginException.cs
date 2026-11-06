namespace ATM.Models.Exceptions;

public class NonValidLoginException
    :Exception
{
    public NonValidLoginException(string message) : base(message)
    {

    }
}

    