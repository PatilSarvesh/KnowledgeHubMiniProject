using KnowledgeHub.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.Web.Models.Data
{
    public class KnowledgeHubDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=KnowledgeHubDb2022;Integrated Security=True");
        }
        
       public DbSet<Catagory> Catagories { get; set; }
    }
}
