using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsWebAPI.Repositories;
using DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _ContactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _ContactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetContacts()
        {
            try
            {
                return Ok(await _ContactRepository.GetContactsList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }

        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Contact>> GetContact(int Id)
        {
            try
            {
                var result = await _ContactRepository.GetContact(Id);
                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }

        }

        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            try
            {               
                if (contact == null)
                {
                    return BadRequest();
                }
                var CreateContact =await _ContactRepository.AddContact(contact);
                return CreatedAtAction(nameof(GetContact),new { Id = CreateContact.ID}, CreateContact);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
           
        }

        [HttpPost("{Id:int}")]
        public async Task<ActionResult<Contact>> UpdateContact(int Id,Contact contact)
        {
            try
            {

                if (Id != contact.ID)
                {
                    return BadRequest("Id MisMatch");
                }
                var updatedContact = await _ContactRepository.GetContact(Id);
                if(updatedContact==null)
                {
                    return NotFound($"Conact Id= {Id} not Found in records");
                }
               
                return await _ContactRepository.EditContact(contact);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }

        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Contact>> DeleteContact(int Id)
        {
            try
            {               
                var contactDeleted = await _ContactRepository.GetContact(Id);
                if (contactDeleted == null)
                {
                    return NotFound($"Conact Id= {Id} not Found in records");
                }

               return await _ContactRepository.DeleteContact(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }

        }
    }
}
