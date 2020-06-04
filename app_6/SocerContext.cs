using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace app_6_1
{
    public class SocerContext : DbContext
    {
        static SocerContext() 
        {
            Database.SetInitializer<SocerContext>(new ContextInitializer());               
        }

        public SocerContext() : base("DefaultConnection")
        {

        }

        public DbSet<Player> Players { set; get; }
        public DbSet<Team> Teams { set; get; }

    }
}
