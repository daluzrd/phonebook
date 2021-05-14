using System;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Phonebook> Phonebooks { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
