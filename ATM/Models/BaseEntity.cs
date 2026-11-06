namespace ATM.Models;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset DateOfCreation { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }

    protected BaseEntity(
        Guid id,
        DateTimeOffset dateOfCreation,
        DateTimeOffset updatedDate)
    {
        Id = id;
        DateOfCreation = dateOfCreation;
        UpdatedDate = updatedDate;
    }

    public void Test()
    {
        Console.WriteLine("Test");
    }
}
