namespace ATM.Models.Exceptions;

    public class NonValidCurrencyException
        :Exception
    {
        public NonValidCurrencyException(string message) : base(message) 
        { 

        }

    }

