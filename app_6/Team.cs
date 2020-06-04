using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_6_1
{
    public class Team
    {
        public int Id { set; get; }
        public string TeamName { set; get; }
        public string Coach { set; get; }

        public virtual ICollection<Player> Players { set; get; }    // навигационное свойство

        public Team() 
        {
            Players = new List<Player>();
        }

        public override string ToString()
        {
            return TeamName;
        }
    }
}
