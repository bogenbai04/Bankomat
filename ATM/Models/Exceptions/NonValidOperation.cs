namespace ATM.Models.Exceptions;

public class NonValidOperationException
    : Exception
{
    public NonValidOperationException(string message) : base(message)
    {

    }
}

