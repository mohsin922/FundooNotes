namespace RepositoryLayer.Context
{
    using Microsoft.EntityFrameworkCore;
    using RepositoryLayer.entities;
    using RepositoryLayer.Entities;


    public class FundooContext : DbContext ///dbcontext-for data accessibility
    {
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> UserTables { get; set; } //connecting entity w Database,query and save instances of user
        public DbSet<Note> NotesTable { get; set; }
        public DbSet<Collaborator> CollabTable { get; set; }
        public DbSet<Label> LabelTable { get; set; }
    }
}