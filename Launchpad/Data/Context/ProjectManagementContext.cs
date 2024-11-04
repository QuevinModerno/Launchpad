using Microsoft.EntityFrameworkCore;



namespace Data.Context
{
    public class ProjectManagementContext : DbContext
    {

        public ProjectManagementContext(DbContextOptions options) : base(options)
        {

        }

        //public DbSet<ERC20Standard> Erc20Standards {  get; set; }
       
    }
}

