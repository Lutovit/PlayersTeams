using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_6_1
{
    class ContextInitializer : CreateDatabaseIfNotExists<SocerContext>                                //создает базу и инициализирует ее , если база не существует
    {
        protected override void Seed(SocerContext sc)
        {
            Team t = new Team { TeamName = "Not defined", Coach = "Not defined" };
            sc.Teams.Add(t);
            sc.SaveChanges();
        }

    }

}
