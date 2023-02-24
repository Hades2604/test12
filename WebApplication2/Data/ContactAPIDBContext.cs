using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ContactAPIDBContext : DbContext
    {
        public ContactAPIDBContext(DbContextOptions options):base(options) 
        {
        }
        public DbSet<Contact> contacts { get; set; }
    }
}
