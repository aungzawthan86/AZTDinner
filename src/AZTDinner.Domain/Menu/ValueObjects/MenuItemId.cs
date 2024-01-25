using AZTDinner.Domain.Common.Models;

namespace AZTDinner.Domain.Menu.ValueObjects;

public sealed class MenuItemId : ValueObject
{
    public Guid Value { get; }
    public MenuItemId(Guid value)
    {
        Value = value;
    }
    public static MenuItemId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static MenuItemId Create(Guid value)
    {
        return new(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    private MenuItemId()
    { }
}