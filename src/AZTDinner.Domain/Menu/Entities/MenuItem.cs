using AZTDinner.Domain.Common.Models;
using AZTDinner.Domain.Menu.ValueObjects;

namespace AZTDinner.Domain.Menu.Entities;
public sealed class MenuItem : Entity<MenuItemId>
{
    private readonly List<MenuItem> items = new();

    public MenuItem(MenuItemId menuItemId, string name, string description) : base(menuItemId)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public static MenuItem Create(string name,
                                  string description)
    {
        return new(MenuItemId.CreateUnique(), name, description);
    }
    private MenuItem()
    { }
}