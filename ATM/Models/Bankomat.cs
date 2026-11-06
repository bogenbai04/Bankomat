namespace ATM.Models;

public class Bankomat : BaseEntity
{
    public Bankomat(Guid id, DateTimeOffset dateOfCreation, DateTimeOffset updatedDate) : base(id, dateOfCreation, updatedDate)
    {
    }
}
