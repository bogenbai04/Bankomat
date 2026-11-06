namespace ATM.Models;

public class Wallet : BaseEntity
{
    private const int MAX_NOMINAL = 20_000;

    public int Nominal { get; private set; }
    public Currency Currency { get; private set; }

    public Wallet(
        Guid id,
        DateTimeOffset dateOfCreation,
        DateTimeOffset updatedDate,
        int nominal,
        Currency currency)
        : base(id, dateOfCreation, updatedDate)
    {
        Validate(nominal);

        Nominal = nominal;
        Currency = currency;
    }

    private void Validate(int nominal)
    {
        if (nominal > MAX_NOMINAL)
            throw new Exception($"{nominal} слишком большой!");
    }

    public void ChangeNominal(int newNominal)
    {
        if(newNominal > MAX_NOMINAL)
            throw new Exception($"{newNominal} слишком большой!");

        if (newNominal == Nominal)
            throw new Exception($"{newNominal} совпадает со старым номиналом!");

        Nominal = newNominal;
    }
}
