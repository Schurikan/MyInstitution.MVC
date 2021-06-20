using Microsoft.EntityFrameworkCore;
using MyInstitution.MVC.Models;

namespace MyInstitution.MVC.Data
{
    public class InstitutionContext : DbContext
    {
        //Entitätenmenge = Tabelle
        //Entität = Zeile

        public InstitutionContext(DbContextOptions<InstitutionContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Entitätsmenge von Movies, quasi Tabelle mit Movies.
        /// </summary>
       // public DbSet<Employee> Employees { get; set; }

        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Entitätsmenge von Movies, quasi Tabelle mit Movies.
        /// </summary>
       // public DbSet<Employee> Employees { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventDetails> EventDetails { get; set; }

        public DbSet<EventMember> EventMembers { get; set; }

        public DbSet<DutyRoster> DutyRosters { get; set; }

        public DbSet<MyInstitution.MVC.Models.Movie> Movie { get; set; }

        public DbSet<MyInstitution.MVC.Models.Test> Test { get; set; }

    }
}