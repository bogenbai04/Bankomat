using System.Text.RegularExpressions;

namespace ATM.Models;

public class Account : BaseEntity
{
    private const string PASSWORD_REGEX = @"/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*+=])(?:\\S+$).{8,}$/";

    public string Login { get; private set; }
    public string Password { get; private set; }

    public Account(Guid id, DateTimeOffset dateOfCreation, DateTimeOffset updatedDate, string login, string password) : base(id, dateOfCreation, updatedDate)
    {
        ValidatePassword(password);

        Login = login;
        Password = password;
    }

    public void SetLogin(string newLogin)
    {
        if (newLogin.Length > 10)
            throw new Exception();

        Login = newLogin;
    }

    public void ValidatePassword(string password)
    {
        if (!Regex.IsMatch(password, PASSWORD_REGEX))
            throw new Exception("Не соответсвует требованиям!");
    }
}
