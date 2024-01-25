using AZTDinner.Domain.Entities;

namespace AZTDinner.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
   User? GetUserByEmail(string email);
   void Add(User user);
}