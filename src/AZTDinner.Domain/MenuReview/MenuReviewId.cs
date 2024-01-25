using AZTDinner.Domain.Common.Models;

namespace AZTDinner.Domain.MenuReview.ValueObjects;

public sealed class MenuReviewId : ValueObject
{
    public Guid Value { get; protected set; }
    public MenuReviewId(Guid value)
    {
        Value = value;
    }
    public static MenuReviewId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static MenuReviewId Create(Guid value)
    {
        return new(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    private MenuReviewId()
    { }
}