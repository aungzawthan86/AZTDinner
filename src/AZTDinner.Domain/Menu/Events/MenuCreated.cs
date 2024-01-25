using AZTDinner.Domain.Common.Models;

namespace AZTDinner.Domain.Menu.Events;
public record MenuCreated(Menu Menu) : IDomainEvent;