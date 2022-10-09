using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rentid.Entities;

namespace rentid.Contexts
{
    public class appDbContext:IdentityDbContext<appUser,appRole,string>
    {
        public appDbContext(DbContextOptions<appDbContext> options):base(options)
        {
            
        }
    }
}