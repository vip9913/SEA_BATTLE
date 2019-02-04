using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    class Корабль
    {
        int попаданий;
        public Точка[] палуба { get; private set; } //описание корабля

        public Корабль(Точка[] палуба)
        {
            this.палуба = палуба;
            попаданий = 0;
        }       

        public Статус Выстрел(Точка t)
        {
            for (int i = 0; i < палуба.Length; i++)
            {
                if (палуба[i].x == t.x && палуба[i].y == t.y)
                {
                    попаданий++;
                    if (попаданий == палуба.Length) return Статус.убил;
                    else return Статус.ранил;
                }               
            }
            return Статус.мимо;
        }
    }
}
