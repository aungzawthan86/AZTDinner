using AZTDinner.Domain.Menu;

namespace AZTDinner.Application.Common.Interfaces;

public interface IMenuRepository
{
    void Add(Menu menu);
}