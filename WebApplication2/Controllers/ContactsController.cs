using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactAPIDBContext dbContext;

        public ContactsController(ContactAPIDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.contacts.ToListAsync());
            
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContacts addContacts)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContacts.Address,
                Fullname = addContacts.Fullname,
                Email = addContacts.Email,
                Phone = addContacts.Phone
            };
            await dbContext.contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContact updateContact)
        {
            var contact = await dbContext.contacts.FindAsync(id);
            if(contact != null)
            {
                contact.Fullname = updateContact.Fullname;
                contact.Email = updateContact.Email;
                contact.Phone = updateContact.Phone;
                contact.Address = updateContact.Address;

                await dbContext.SaveChangesAsync();
                return Ok(contact); 
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact =await dbContext.contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();

        }
    }
}
