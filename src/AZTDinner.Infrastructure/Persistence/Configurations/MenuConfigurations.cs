using System.Data.Common;
using AZTDinner.Domain.Host.ValueObjects;
using AZTDinner.Domain.Menu;
using AZTDinner.Domain.Menu.Entities;
using AZTDinner.Domain.Menu.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AZTDinner.Infrastructure.Persistence.Configurations;
public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
  public void Configure(EntityTypeBuilder<Menu> builder)
  {
    ConfigureMenuTable(builder);
    ConfigureMenuSectionTable(builder);
    ConfigureMenuDinnerIdsTable(builder);
    ConfigureMenuReviewIdsTable(builder);
  }



  private void ConfigureMenuTable(EntityTypeBuilder<Menu> builder)
  {
    builder.ToTable("Menus");
    builder.HasKey(m => m.Id);
    builder.Property(m => m.Id)
           .ValueGeneratedNever()
           .HasConversion(id => id.Value,
                          value => MenuId.Create(value));
    builder.Property(m => m.Name)
           .HasMaxLength(100);
    builder.Property(m => m.Description)
           .HasMaxLength(100);
    builder.OwnsOne(m => m.AverageRating);

    builder.Property(m => m.HostId)
           .HasConversion(id => id.Value,
                          value => HostId.Create(value));




  }
  private void ConfigureMenuSectionTable(EntityTypeBuilder<Menu> builder)
  {
    builder.OwnsMany(m => m.Sections, sb =>
    {
      sb.ToTable("MenuSections");
      sb.WithOwner().HasForeignKey("MenuId");

      sb.HasKey("Id", "MenuId");


      sb.Property(s => s.Id)
          .HasColumnName("MenuSectionId")
          .ValueGeneratedNever()
          .HasConversion(id => id.Value, value => MenuSectionId.Create(value));

      sb.Property(p => p.Name)
            .HasMaxLength(100);
      sb.Property(p => p.Description)
            .HasMaxLength(100);

      sb.OwnsMany(s => s.Items, ib =>
          {
            ib.ToTable("MenuItems");
            ib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");
            ib.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");
            ib.Property(p => p.Id)
                  .HasColumnName("MenuItemId")
                  .ValueGeneratedNever()
                  .HasConversion(id => id.Value, value => MenuItemId.Create(value));

            ib.Property(p => p.Name)
                  .HasMaxLength(100);

            ib.Property(p => p.Description)
                  .HasMaxLength(100);
          });

      sb.Navigation(s => s.Items).Metadata.SetField("_Items");
      sb.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
    });
    builder.Metadata.FindNavigation(nameof(Menu.Sections))!.SetPropertyAccessMode(PropertyAccessMode.Field);
  }
  private void ConfigureMenuDinnerIdsTable(EntityTypeBuilder<Menu> builder)
  {
    builder.OwnsMany(m => m.DinnerIds, dib =>
    {
      dib.ToTable("MenuDinnerIds");
      dib.WithOwner().HasForeignKey("MenuId");
      dib.HasKey("Id");
      dib.Property(d => d.Value)
              .HasColumnName("DinnerId").ValueGeneratedNever();

    });
    builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
  }
  private void ConfigureMenuReviewIdsTable(EntityTypeBuilder<Menu> builder)
  {
    builder.OwnsMany(m => m.MenuReviewIds, dib =>
    {
      dib.ToTable("MenuReviewIds");
      dib.WithOwner().HasForeignKey("MenuId");
      dib.HasKey("Id");
      dib.Property(d => d.Value)
              .HasColumnName("MenuReviewId").ValueGeneratedNever();

    });
    builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
  }




}