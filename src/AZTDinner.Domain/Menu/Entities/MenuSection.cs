using AZTDinner.Domain.Common.Models;
using AZTDinner.Domain.Entities;
using AZTDinner.Domain.Menu.Entities;
using AZTDinner.Domain.Menu.ValueObjects;

namespace AZTDinner.Domain.Menu.Entities;

public sealed class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _Items = new();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<MenuItem> Items => _Items.AsReadOnly();
    public MenuSection(MenuSectionId menuSectionId, string name, string description, List<MenuItem> items) : base(menuSectionId)
    {
        Name = name;
        Description = description;
        _Items = items;
    }
    public static MenuSection Create(string name, string description, List<MenuItem> items)
    {
        return new(MenuSectionId.CreateUnique(), name, description, items);
    }
    private MenuSection()
    { }
}