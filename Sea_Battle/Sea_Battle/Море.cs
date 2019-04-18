namespace Sea_Battle
{
    public delegate void deShowShip(Точка place, int nr); //делегат
    public delegate void deShowFight(Точка place, Статус status);

    class Море
    {

    public static Точка размер_моря = new Точка(10,10);
    public static int всего_кораблей = 10;

        public deShowShip ShowShip;
        public deShowFight ShowFight;

    protected int[,] карта_кораблей; //0..9 номера кораблей  -1 пусто
    protected Статус[,] карта_попаданий; // неизвестно
    protected Корабль[] корабль; //массив кораблей

    public int создано { get; protected set;}

    public int убито   { get; protected set;}

    public Море() //конструктор для моря
    {
        карта_кораблей = new int[размер_моря.x, размер_моря.y];
        карта_попаданий = new Статус[размер_моря.x, размер_моря.y];
        корабль = new Корабль[всего_кораблей];
        //Сброс();
    }
       
  

        public Статус КартаПопаданий(Точка t)
        {
            if (НаМоре(t)) return карта_попаданий[t.x, t.y];
            return Статус.неизвестно;
        }

        public Статус Выстрел(Точка t)
        {
            if (!НаМоре(t)) return Статус.неизвестно;
            if (карта_попаданий[t.x, t.y] != Статус.неизвестно) return карта_попаданий[t.x, t.y];
            Статус статус;
            if (карта_кораблей[t.x, t.y] == -1)
            {
                карта_попаданий[t.x, t.y] = Статус.мимо;
                статус=Статус.мимо;
            }
            else статус = корабль[карта_кораблей[t.x, t.y]].Выстрел(t);
            карта_попаданий[t.x, t.y] = статус;
            if (статус == Статус.убил)
            {
                убито++;
                if (убито >= создано) return статус=Статус.победил;
            }
            ShowFight(t, статус);
            return статус;
        }


        public bool НаМоре(Точка t)
        {
            return (t.x>=0 && t.x<размер_моря.x &&
                t.y>= 0 && t.y<размер_моря.y);
        }
    }
}
