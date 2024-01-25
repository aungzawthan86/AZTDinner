using AZTDinner.Application.Menus.Commands.CreateMenu;
using AZTDinner.Contracts.Menus;
using AZTDinner.Domain.Menu;
using Mapster;
using MenuSection = AZTDinner.Domain.Menu.Entities.MenuSection;
using MenuItem = AZTDinner.Domain.Menu.Entities.MenuItem;
namespace AZTDinner.Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateMenuRequest request, Guid HostId), CreateMenuCommand>()
        .Map(dest => dest.HostId, src => src.HostId)
        .Map(dest => dest, src => src.request);


        config.NewConfig<Menu, MenuResponse>()
             .Map(dest => dest.Id, src => src.Id.Value.ToString())
             .Map(dest => dest.AverageRating, src => src.AverageRating.Value)
             .Map(dest => dest.HostId, src => src.HostId.Value.ToString())
             .Map(dest => dest.DinnerIds, src => src.DinnerIds.Select(dinnerId => dinnerId.Value.ToString()))
             .Map(dest => dest.MenuReviewIds, src => src.MenuReviewIds.Select(menuReviewId => menuReviewId.Value.ToString()));

        config.NewConfig<MenuSection, MenuSectionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString());

        config.NewConfig<MenuItem, MenuItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString());

    }
}