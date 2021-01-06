using ContactsWebAPI.DBContext;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsWebAPI.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _Context;

        public ContactRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            var result = await _Context.Contacts.AddAsync(contact);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Contact> DeleteContact(int ID)
        {
            var result = await _Context.Contacts.Where(s => s.ID == ID).FirstOrDefaultAsync();
            if(result!=null)
            {
                _Context.Contacts.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Contact> EditContact(Contact contact)
        {
            var result = await _Context.Contacts.FirstOrDefaultAsync(s => s.ID == contact.ID);

            if(result!=null)
            {
                result.FirstName = contact.FirstName;
                result.LastName = contact.LastName;
                result.PhoneNumber = contact.PhoneNumber;
                result.Status = contact.Status;

                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Contact> GetContact(int ID)
        {
            return await _Context.Contacts.FirstOrDefaultAsync(s => s.ID == ID);
        }

        public async Task<IEnumerable<Contact>> GetContactsList()
        {
           //return await _Context.Contacts.ToListAsync();
            return await _Context.Contacts.Where(s=>s.Status ==true).ToListAsync();
        }
    }
}
