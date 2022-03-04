using Microsoft.EntityFrameworkCore;
using RepositoryLayer.entities;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext ///dbcontext-for data accessibility
    {
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> UserTables { get; set; } //connecting entity w Database
        public DbSet<Note> NotesTable { get; set; }
        public DbSet<Collaborator> CollabTable { get; set; }
    }
}