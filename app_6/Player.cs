using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_6_1
{
    public class Player
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Age { set; get; }
        public Posicion pos{ set; get; }

        public virtual Team Team { set; get; }    // навигационное свойство
        public int? TeamId { set; get; }         // первичный ключ

        public override string ToString()      //переопределенный метод ToString() для правильного отображения связанных данных в элементах управления
        {
            return Name;
        }

    }
}
