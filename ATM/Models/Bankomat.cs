using ATM.Models.Exceptions;
using System.Security.Cryptography.X509Certificates;
namespace ATM.Models;

public class Bankomat : BaseEntity
{
    public const string WITHDRAWAL = nameof(WITHDRAWAL);

    public const string REPLENISHMENT = nameof(REPLENISHMENT);

    public Bankomat(Guid id, DateTimeOffset dateOfCreation, DateTimeOffset updatedDate, string location, string typedCurrency) : base(id, dateOfCreation, updatedDate)
    {
        Validate(location,typedCurrency);
        Location = location;
        Cur = typedCurrency;

    }
    public string Location { get; private set; }

    public string Cur { get; private set; }

    public decimal TotalCash { get; private set; }


    public void ChangeTotalCash(decimal value, string operation)
    {
        if (operation == WITHDRAWAL)
        {
            if (TotalCash < value)
            {
                throw new NotEnoughMoneyException("Недостаточно денег в банкомате");
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

    public void Validate(string location,string currency)
    {
        ValidateCountries(location);
        ValidateCurrency(currency);
        
    }
    public void ValidateCurrency(string currency)
    {
        if (!Enum.TryParse(currency, true, out Currency CurrencyEnum))
            {
            throw new NonValidCurrencyException("Неверно указана валюта");
        }
    }

    private void ValidateCountries(string country)
    {
        if(Locations.HostileCountries.Contains(country))
        {
            throw new NonValidCountryException("Страна не прошла валидацию");
        }
    
      }
    public override string ToString()
    {
        return $"{Id} - {Location} - {TotalCash}";
    }
    }        
