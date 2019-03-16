using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    class Mission
    {
            Море sea;
            Random rand;

            public Mission(Море sea)
            {
                this.sea = sea;
                rand = new Random();
            }

            public Статус Fight(out Точка target)
            {
                do
                {
                    target = new Точка(
                        rand.Next(0, Море.размер_моря.x),
                        rand.Next(0, Море.размер_моря.y));
                } while (sea.КартаПопаданий(target) != Статус.неизвестно);
                return sea.Выстрел(target);
            }
        }
}


