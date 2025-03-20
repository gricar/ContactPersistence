using ContactPersistence.Models;

namespace ContactPersistence.Data.Repositories;

public interface IContactRepository
{
    Task AddAsync(Contact contact);
}