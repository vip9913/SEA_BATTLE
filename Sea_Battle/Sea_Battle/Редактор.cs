﻿using System;

namespace Sea_Battle
{
    class Редактор: Море  //наследуем все свойства от Моря
    {
        static int[] длина_кораблей = { 4,3,3,2,2,2,1,1,1,1};
        static Random rnd = new Random();

        public Редактор(): base()            
        {

        }

        public bool ПоставитьРовно()
        {
            Сброс();
            ПоставитьКорабль(0,
                new Точка[]{
                new Точка(1,1),
                new Точка(2,1),
                new Точка(3,1),
                new Точка(4,1) });

            ПоставитьКорабль(1,
                new Точка[]{
                new Точка(1,3),
                new Точка(2,3),               
                new Точка(3,3) });

            ПоставитьКорабль(2,
                new Точка[]{
                new Точка(5,3),
                new Точка(6,3),
                new Точка(7,3) });

            ПоставитьКорабль(3,
                new Точка[]{
                new Точка(1,5),                
                new Точка(2,5) });

            ПоставитьКорабль(4,
                new Точка[]{
                new Точка(4,5),
                new Точка(5,5) });

            ПоставитьКорабль(5,
                new Точка[]{
                new Точка(7,5),
                new Точка(8,5) });

            for (int номер = 6; номер < всего_кораблей; номер++)
            {
                ПоставитьКорабль(номер,
                new Точка[]{                
                new Точка((номер-5)*2-1, 7)});
            }
         
            return true;
        }

        public bool ПоставитьСлучайно(int номер) //расставить случайно один корабль
        {
            int длина = длина_кораблей[номер];
            Точка нос;//левый верхний что бы определиться горизонтально или вертикально ставим корабль
            Точка шаг;
            if (rnd.Next(2) == 0)
            {
                нос = new Точка(rnd.Next(0, размер_моря.x - длина + 1), rnd.Next(0, размер_моря.y));
                шаг = new Точка(1,0);
            }
            else
            {
                нос = new Точка(rnd.Next(0, размер_моря.x), rnd.Next(0, размер_моря.y-длина+1));
                шаг = new Точка(0, 1);
            }
            Точка[] палуба = new Точка[длина];
            for (int i = 0; i < длина; i++)
            {
                палуба[i] = new Точка(нос.x + i * шаг.x, нос.y + i * шаг.y);
                //ОчиститьТочку(палуба[i]);
                ОчиститьОбласть(палуба[i]);
            }
            //ОчиститьОбласть(палуба[i]);
            ПоставитьКорабль(номер, палуба);
            return true;
        }

        public void ПоставитьСлучайно()
        {
            Сброс();
            int loop = 100;//клапан для блокировки зацикливания размещения
            while (--loop > 0 && создано < Море.всего_кораблей)
            {
                for (int nr = 0; nr < Море.всего_кораблей; nr++)
                    if (НетКорабля(nr))
                        ПоставитьСлучайно(nr);
            }
            if (создано < Море.всего_кораблей) Сброс();
        }



        public void ОчиститьТочку(Точка t)
        {
            if (!НаМоре(t)) return;
            if (карта_кораблей[t.x, t.y] == -1) return;
            УбратьКорабль(карта_кораблей[t.x, t.y]);
        }

        protected void ОчиститьОбласть(Точка t)
        {
            Точка p;
            for (p.x = t.x - 1; p.x <= t.x + 1; p.x++)
                for (p.y = t.y - 1; p.y <= t.y + 1; p.y++)
                    ОчиститьТочку(p);
        }

        public void Сброс()
        {
            for (int x = 0; x < размер_моря.x; x++)
                for (int y = 0; y < размер_моря.y; y++)
                {
                    карта_кораблей[x, y] = -1;
                    ShowShip(new Точка(x, y), -1);
                    карта_попаданий[x, y] = Статус.неизвестно;
                    ShowFight(new Точка(x, y), Статус.неизвестно);
                }
            for (int k = 0; k < всего_кораблей; k++) корабль[k] = null;
            создано = 0; убито = 0;
        }

        //рисуем ручками корабль
        public bool ПоставитьПоТочкам(Точка[] палуба)
        {
            int длина = палуба.Length;
            int номер = НайтиНомер(длина);
            if (номер < 0) return false;
            Точка лв = палуба[0]; //установка начальных параметров
            Точка пн = палуба[0];
            for (int i = 1; i < длина; i++)
                {
                лв.x = Math.Min(лв.x, палуба[i].x);
                лв.y = Math.Min(лв.y, палуба[i].y);
                пн.x = Math.Max(пн.x, палуба[i].x);
                пн.y = Math.Max(пн.y, палуба[i].y);
            }
            if (лв.x == пн.x) //вертикально
            {
                if (пн.y - лв.y + 1 != длина) return false;                
            } else
            if (лв.y == пн.y)  //горизонтально
            {
                if (пн.x - лв.x + 1 != длина) return false;
            }
            else return false;
            //for (int i = 0; i < длина; i++)
            //ОчиститьОбласть(палуба[i]);
            ПоставитьКорабль(номер, палуба);
             return true;                        
        }


        /// <summary>
        /// находит место для нового корабля указанной длины
        /// </summary>
        /// <param name="длина"></param>
        /// <returns></returns>
        protected int НайтиНомер(int длина)
        {
            for (int i = 0; i < длина_кораблей.Length; i++)            
                if (длина == длина_кораблей[i])
                    if (НетКорабля(i)) return i;            
        return -1;
        }

        public void ПоставитьКорабль(int номер, Точка[] палуба)
        {
            if (корабль[номер] != null) УбратьКорабль(номер);
            корабль[номер] = new Корабль(палуба);
            foreach (Точка t in палуба)
            {
                карта_кораблей[t.x, t.y] = номер;
                ShowShip(t, номер);
            }
            создано++;
        }

        public void УбратьКорабль(int номер)
        {
            foreach (Точка t in корабль[номер].палуба)
            {
                карта_кораблей[t.x, t.y] = -1;
                ShowShip(t, -1);
            }
            корабль[номер] = null;
            создано--;
        }

        public bool НетКорабля(int номер)
        {
            return корабль[номер] == null;
        }

        public int КартаКораблей(Точка t)
        {
            if (НаМоре(t)) return карта_кораблей[t.x, t.y];
            return -1;
        }
    }
}
