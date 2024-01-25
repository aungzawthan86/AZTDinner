using AZTDinner.Application.Common.Interfaces;
using AZTDinner.Domain.Entities;
using AZTDinner.Domain.Host.ValueObjects;
using AZTDinner.Domain.Menu;
using AZTDinner.Domain.Menu.Entities;
using ErrorOr;
using MediatR;

namespace AZTDinner.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Create Menu
        var menu = Menu.Create(
            hostId: HostId.Create(request.HostId),
            name: request.Name,
            description: request.Description,
            sections: request.Sections.ConvertAll(section => MenuSection.Create(
                section.Name,
                section.Description,
                section.Items.ConvertAll(item => MenuItem.Create(
                    item.Name,
                    item.Description
                )))));
        // Persist Menu
        // Return Menu
        _menuRepository.Add(menu);
        return menu;
    }
}