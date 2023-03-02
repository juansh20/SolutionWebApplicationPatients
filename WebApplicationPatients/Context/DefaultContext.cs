using Microsoft.EntityFrameworkCore;
using WebApplicationPatient.Models;

namespace WebApplicationPatients.Context
{
    public class DefaultContext : DbContext
    {

        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {

        }

    }
}
