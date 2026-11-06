namespace ATM.Models.Exceptions;
    public class NonValidCountryException
        :Exception
    {
        public NonValidCountryException(string message) : base(message) 
        { 

        }
    }

