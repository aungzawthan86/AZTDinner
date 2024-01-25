using AZTDinner.Domain.Common.Models;
using AZTDinner.Domain.Menu;
using AZTDinner.Infrastructure.Persistence.Inteceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AZTDinner.Infrastructure.Persistence;
public class AZTDinnerDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public AZTDinnerDbContext(DbContextOptions<AZTDinnerDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }
    public DbSet<Menu> Menus { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<List<IDomainEvent>>()
                    .ApplyConfigurationsFromAssembly(typeof(AZTDinnerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

}