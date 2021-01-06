using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsWebAPI.Repositories
{
   public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetContactsList();

        Task<Contact> GetContact(int ID);

        Task<Contact> AddContact(Contact contact);

        Task<Contact> EditContact(Contact contact);

        Task<Contact> DeleteContact(int ID);
    }
}
