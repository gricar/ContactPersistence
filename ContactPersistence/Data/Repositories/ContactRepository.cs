using ContactPersistence.Models;

namespace ContactPersistence.Data.Repositories;

public class ContactRepository(ApplicationDbContext dbContext) : IContactRepository
{
    public async Task AddAsync(Contact contact)
    {
        await dbContext.Contacts.AddAsync(contact);
        await dbContext.SaveChangesAsync();
    }
}
