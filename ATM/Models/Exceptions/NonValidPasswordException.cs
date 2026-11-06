namespace ATM.Models.Exceptions;

    public class NonValidPasswordException
    :Exception
    {
     public NonValidPasswordException(string message) : base(message) 
    {

    }
}
