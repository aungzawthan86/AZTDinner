using AZTDinner.Domain.Common.Models;
using AZTDinner.Domain.Common.ValueObjects;
using AZTDinner.Domain.Dinner.ValueObjects;
using AZTDinner.Domain.Host.ValueObjects;
using AZTDinner.Domain.Menu.Entities;
using AZTDinner.Domain.Menu.Events;
using AZTDinner.Domain.Menu.ValueObjects;
using AZTDinner.Domain.MenuReview.ValueObjects;

namespace AZTDinner.Domain.Menu;

public sealed class Menu : AggregateRoot<MenuId, Guid>
{
    private readonly List<MenuSection> _sections = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewId = new();
    public string Name { get; private set; }
    public string Description { get; private set; }
    public AverageRating AverageRating { get; }
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    public HostId HostId { get; private set; }
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewId.AsReadOnly();

    public DateTime UpdatedDateTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public Menu(MenuId menuId,
                HostId hostId,
                string name,
                string description,
                AverageRating averageRating,
                List<MenuSection>? sections
                ) : base(menuId)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        _sections = sections;
        AverageRating = averageRating;

    }
    public static Menu Create(HostId hostId,
                              string name,
                              string description,
                              List<MenuSection>? sections = null)
    {
        var menu = new Menu(
            MenuId.CreateUnique(),
            hostId,
            name,
            description,
            AverageRating.CreateNew(0),
            sections ?? new()
        );
        menu.AddDomainEvent(new MenuCreated(menu));
        return menu;
    }

    private Menu()
    {

    }
}