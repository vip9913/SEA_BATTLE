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
                    int len = markKilledShip(target); //если попали в корабль
                    shipLength[len]--;//уменьшаем его длину
                    modeDanger = false;                   
                    break;

            }
            //return sea.Выстрел(target);
            return status;
        }

        /// <summary>
        /// помечает убитые корабли
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        private int markKilledShip(Точка place)
        {
            if (!sea.НаМоре(place)) return 0;
            if (map[place.x, place.y] == 2)
            {
                map[place.x, place.y] = 3;//убитая
                int x, y;
                for (x = place.x - 1; x <= place.x + 1; x++)
                    for (y = place.y - 1; y <= place.x + 1; y++)
                        if (Map(x, y) == 0) map[x, y] = 1;//помечаем что стреляли
                int length = 1;
                //рекурсивно пройдемся по точкам
                length += markKilledShip(new Точка(place.x-1,place.y));
                length += markKilledShip(new Точка(place.x + 1, place.y));
                length += markKilledShip(new Точка(place.x, place.y-1));
                length += markKilledShip(new Точка(place.x, place.y+1));
                return length;
            }
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

        /// <summary>
        /// стреляем по кораблю ИИ
        /// </summary>
        /// <returns></returns>
        private Точка FightDanger()
        {
            // return new Точка(0,0);
            InitPut();
            for (int x = 0; x < Море.размер_моря.x; x++)
                for (int y = 0; y < Море.размер_моря.y; y++)
                    if (map[x, y] == 2) //если ранен ищем
                    {
                        Точка ship = new Точка(x,y); //идем вврх вниз
                        for (int length = 2; length < shipLength.Length; length++) //берем корабль и начинаем мочить в разные стороны в зависимости от того какие корабли остались
                        {
                            if (shipLength[length] > 0)
                            {
                                CheckShipDirection(ship, -1, 0, length);
                                CheckShipDirection(ship, 1, 0, length);
                                CheckShipDirection(ship, 0, -1, length);
                                CheckShipDirection(ship, 0, 1, length);
                            }
                        }
                    }
                    return RandomPut();
        }

        /// <summary>
        /// проверка направления бомбандировки
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        /// <param name="length"></param>
        private void CheckShipDirection(Точка ship, int sx, int sy, int length)
        //проверить клетки в указанном направлении
        { 
            //текущая клетка должна быть ранен
            if (Map(ship.x, ship.y) != 2) return;//не ранен
            //в остальных напралениях не должно быть раненных клеток
            if (Map(ship.x - sx, ship.y - sy) == 2) return;//за пределами
            if (sx == 0) //перемещение по x
            {
                if (Map(ship.x - 1, ship.y) == 2) return;
                if (Map(ship.x + 1, ship.y) == 2) return;
            }
            if (sy == 0) //перемещение по y
            {
                if (Map(ship.x, ship.y-1) == 2) return;
                if (Map(ship.x, ship.y+1) == 2) return;
            }
            //в выбранном направлении не должно быть клеток МИМО
            //в указанном напралении должна быть хотя бы одна клетка неизвестна
            //может быть клетка ранен
            int unknown = 0;
            int unknown_i = 0;
            for (int i = 1; i < length; i++)
            {
                int p = Map(ship.x + i * sx, ship.y + i * sy);
                if (p == 1) return;
                if (p ==-1) return;

                if (p == 0)
                {
                    unknown++;
                    if (unknown == 1) unknown_i = i;                       
                }
            }
            if (unknown>=1) put[ship.x + unknown_i * sx, ship.y + unknown_i * sy]++;//нашли неизвестную клетку
        }

        private int Map(int x, int y)
        {
            if (sea.НаМоре(new Точка(x, y))) return map[x,y];
            return -1;
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


