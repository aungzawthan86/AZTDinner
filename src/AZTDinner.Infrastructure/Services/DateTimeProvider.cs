using AZTDinner.Application.Common.Interfaces.Services;

namespace AZTDinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}