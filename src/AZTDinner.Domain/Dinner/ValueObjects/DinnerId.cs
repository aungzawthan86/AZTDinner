using AZTDinner.Domain.Common.Models;

namespace AZTDinner.Domain.Dinner.ValueObjects;

public sealed class DinnerId : ValueObject
{
    public Guid Value { get; protected set; }
    public DinnerId(Guid value)
    {
        Value = value;
    }
    public static DinnerId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static DinnerId Create(Guid value)
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    private DinnerId()
    { }
}