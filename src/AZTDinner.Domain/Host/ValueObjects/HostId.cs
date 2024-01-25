using AZTDinner.Domain.Common.Models;

namespace AZTDinner.Domain.Host.ValueObjects;

public sealed class HostId : AggregateRootId<Guid>
{

    public override Guid Value { get; protected set; }

    public HostId(Guid value)
    {
        Value = value;
    }
    public static HostId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static HostId Create(Guid value)
    {
        return new HostId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}