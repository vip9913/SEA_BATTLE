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
        int[,] shape =    //матрица порядка нанесения ударов по алгоритму бьем через клетку по диагонали
        {
            { 1,2,1,3,1,2,1,3,1,2},
            { 2,1,3,1,2,1,3,1,2,1},
            { 1,3,1,2,1,3,1,2,1,3},
            { 3,1,2,1,3,1,2,1,3,1},
            { 1,2,1,3,1,2,1,3,1,2},
            { 2,1,3,1,2,1,3,1,2,1},
            { 1,3,1,2,1,3,1,2,1,3},
            { 3,1,2,1,3,1,2,1,3,1},
            { 1,2,1,3,1,2,1,3,1,2},
            { 2,1,3,1,2,1,3,1,2,1}
        };

        bool modeDanger;//ранен корабль или нет
        int[] shipLength= new int[5]; //сколько кораблей какой длины осталось
        int[,] map; //в какие клетки мы стреляли 0-неизвестно 1- мимо 2 - ранен 3 -убит
        int[,] put; //для

            public Mission(Море sea)
            {
                this.sea = sea;
                rand = new Random();
            map = new int[Море.размер_моря.x, Море.размер_моря.y];
            put = new int[Море.размер_моря.x, Море.размер_моря.y];
            Reset();//подготовка массива
        }


        /// <summary>
        /// функция инициализации поля выстрелов
        /// </summary>
        private void Reset()
        {
            shipLength[1] = 4;
            shipLength[2] = 3;
            shipLength[3] = 2;
            shipLength[4] = 1;
            for (int x = 0; x < Море.размер_моря.x; x++)
                for (int y = 0; y < Море.размер_моря.y; y++)
                    map[x, y] = 0;
            modeDanger = false;
        }

        public Статус Fight(out Точка target)
            {
            //do
            //{
            //    target = new Точка(
            //        rand.Next(0, Море.размер_моря.x),
            //        rand.Next(0, Море.размер_моря.y));
            //} while (sea.КартаПопаданий(target) != Статус.неизвестно);
            if (modeDanger) target = FightDanger();
            else target = FightShapes();
            Статус status = sea.Выстрел(target);
            switch (status)
            {
                case Статус.мимо: map[target.x, target.y] = 1; break;
                case Статус.ранил: map[target.x, target.y] = 2;
                    modeDanger = true;   break;
                case Статус.убил: 
                case Статус.победил: map[target.x, target.y] = 2;
                    int len = markKilledShip(targer); //если попали в корабль
                    shipLength[len]--;//уменьшаем его длину
                    modeDanger = false;  break;

            }
            //return sea.Выстрел(target);
            return status;
        }

        private int markKilledShip(Точка place)
        {
            return 0;
        }

        private Точка FightShapes() //стрелять по шаблону
        {
            InitPut();
            for (int x = 0; x < Море.размер_моря.x; x++)
                for (int y = 0; y < Море.размер_моря.y; y++)
                    if (map[x, y] == 0) put[x, y] = shape[x, y]; //если в эту точку еще не шмальнили значит берем её на вооружение
                        return RandomPut();
        }

        private Точка RandomPut()  //выбрать случайное значение
        {
            int max = -1; //максимальное значение инициализация
            int qty = 0; //количество максимальных значений
            for (int x = 0; x < Море.размер_моря.x; x++)
                for (int y = 0; y < Море.размер_моря.y; y++)
                    if (put[x, y] > max)
                    {
                        max = put[x, y];
                        qty = 1;
                    }
                    else
                     if (put[x, y] == max) qty++;
            int nr = rand.Next(0, qty); //выбираем случайное занчение
            for (int x = 0; x < Море.размер_моря.x; x++)
                for (int y = 0; y < Море.размер_моря.y; y++)
                    if (put[x, y] == max)
                        if (nr-- == 0)
                            return new Точка(x,y); //мы нашли точку куда шмальнуть
            return new Точка(0,0);
        }

        private void InitPut()
        {
            for (int x = 0; x < Море.размер_моря.x; x++)
                for (int y = 0; y < Море.размер_моря.y; y++)
                    put[x, y] = 0;
        }

        private Точка FightDanger()
        {
            return new Точка(0,0);
        }


        /// <summary>
        /// вспомогательная функция для отображения как работает алгоритм матрицы
        /// </summary>
        private void ShowPutArray()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            for (int x = 0; x < Море.размер_моря.x; x++)
                for (int y = 0; y < Море.размер_моря.y; y++)
                {
                    Console.SetCursorPosition(x+1, y + 12);
                    Console.WriteLine(put[x, y]);
                }
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}


