using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repository;

namespace MyProject
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<DecisoesContext>
    {
        public DecisoesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DecisoesContext>();
            optionsBuilder.UseMySql("Server=localhost;DataBase=decisoes;Uid=root;Pwd=");

            return new DecisoesContext(optionsBuilder.Options);
        }
    }
}