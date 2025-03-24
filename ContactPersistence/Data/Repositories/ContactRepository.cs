using ContactPersistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactPersistence.Data.Repositories;

public class ContactRepository(ApplicationDbContext dbContext) : IContactRepository
{
    public async Task AddAsync(Contact contact)
    {
        await dbContext.Contacts.AddAsync(contact);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Contact contact)
    {
        dbContext.Contacts.Remove(contact);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Contact?> GetByIdAsync(Guid id)
    {
        return await dbContext.Contacts.FindAsync(id);
    }

    public async Task UpdateAsync(Contact contact)
    {
        dbContext.Entry(contact).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }
}
