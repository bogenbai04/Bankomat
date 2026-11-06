using ATM.Models.Exceptions;
using System.Text.RegularExpressions;

namespace ATM.Models;

public class Account : BaseEntity
{
    private const string PASSWORD_REGEX = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#\$%^&*+=])\S{8,}$";
    public const string WITHDRAWAL = nameof(WITHDRAWAL);

    public const string REPLENISHMENT = nameof(REPLENISHMENT);

    public string Login { get; private set; }
    public string Password { get; private set; }
    
    public decimal TotalCash { get; private set; }


    public Account(Guid id, DateTimeOffset dateOfCreation, DateTimeOffset updatedDate, string login, string password) : base(id, dateOfCreation, updatedDate)
    {
        Validate(password, login);

        Login = login;
        Password = password;
        TotalCash = 0;
    }

    public void SetLogin(string newLogin)
    {
        if (newLogin.Length > 10)
            throw new Exception();

        Login = newLogin;
    }
    private void Validate(string password, string login)
    {
        ValidatePassword(password);
        ValidateLogin(login);
    }
    public void ValidatePassword(string password)
    {
        if (!Regex.IsMatch(password, PASSWORD_REGEX))
            throw new Exception("Не соответсвует требованиям!");
    }
    private void ValidateLogin(string login)
    {
        if (login.Length > 10) 
        {  
            throw new NonValidLoginException("Неверный логин");
        } 
    }
    public void ChangeTotalCash(decimal value, string operation)
    {
        if (operation == WITHDRAWAL)
        {
            if (TotalCash < value)
            {
                throw new NotEnoughMoneyException("Недостаточно денег на аккаунте");
            }
            TotalCash -= value;
            return;
        }
        if (operation == REPLENISHMENT)
        {
            TotalCash += value;
            return;
        }
        throw new NonValidOperationException("Недопустимая операция");
    }
    public override string ToString()
    {
        return $"{Id} - {Login} - {TotalCash}";
    }
}
