using AZTDinner.Application.Common.Interfaces;
using AZTDinner.Domain.Menu;

namespace AZTDinner.Infrastructure.Persistence.Repositories;
public class MenuRepository : IMenuRepository
{
    private readonly AZTDinnerDbContext _dbContext;

    public MenuRepository(AZTDinnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Menu menu)
    {
        _dbContext.Add(menu);
        _dbContext.SaveChanges();
    }
}