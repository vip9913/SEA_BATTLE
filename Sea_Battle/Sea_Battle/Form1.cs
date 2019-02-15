using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sea_Battle
{

    public partial class Form1 : Form
    {
        //Море sea_user = new Море();
        //Море sea_pc = new Море();
        
        Редактор sea_pc;
        Редактор sea_user;

        static string abc = "РЕСПУБЛИКА";//"АБВГДЕЖЗИКЛМНОПРСТУФХЧШЩЫЮЯ";
        Color color_back = Color.DarkSeaGreen; //цвет фона
        Color[] color_ship = {
                              Color.DarkOrange,
                              Color.DarkGreen,Color.DarkGreen,
                              Color.DarkViolet,Color.DarkViolet,Color.DarkViolet,
                              Color.DarkRed,Color.DarkRed,Color.DarkRed,Color.DarkRed };

        Color[] color_fight = {
                              Color.DarkSeaGreen,
                              Color.SeaGreen,
                              Color.Orange,
                              Color.Red,
                              Color.Red};              

        public Form1()
        {
            InitializeComponent();
            //sea_user = new Море();
            //sea_pc = new Море();
            
            sea_user = new Редактор();
            sea_user.ShowShip = ShowUserShip;//инициализация делегатов
            sea_user.ShowFight = ShowUserFight;

            sea_pc = new Редактор();
            sea_pc.ShowShip = ShowPcShip;
            sea_pc.ShowFight = ShowPcFight;

            InitGrid(grid_user);
            InitGrid(grid_pc);

            sea_user.Сброс();
            sea_pc.Сброс();
            sea_pc.ПоставитьРовно();
        }

        private void InitGrid(DataGridView grid)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            grid.DefaultCellStyle.BackColor = color_back;
            for (int x = 0; x < Море.размер_моря.x; x++)
            {
                grid.Columns.Add("col_" + x.ToString(), abc.Substring(x, 1));
            }
            for (int y = 0; y < Море.размер_моря.y; y++)
            {
                grid.Rows.Add();
                grid.Rows[y].HeaderCell.Value = (y+1).ToString();
            }
            grid.Height = Море.размер_моря.y * grid.Rows[0].Height + grid.ColumnHeadersHeight + 2;
            grid.ClearSelection();//что бы ничего не отмечалось на сетке
        }

        //private void ShowShips(DataGridView grid, Редактор sea)//перерисовываем картинку
        //{
        //    for (int x = 0; x < Море.размер_моря.x; x++)
        //        for (int y = 0; y < Море.размер_моря.y; y++)
        //        {
        //            int nr = sea.КартаКораблей(new Точка(x, y));
        //            if (nr < 0)
        //                grid[x, y].Style.BackColor = color_back;
        //            else grid[x, y].Style.BackColor = color_ship[nr];
        //        }            
        //}

        private void Form1_Load(object sender, EventArgs e)
        {          
            //sea.ПоставитьКорабль(0, 
            //    new Точка[]{
            //    new Точка(1,1),
            //    new Точка(1,2),
            //    new Точка(1,3),
            //    new Точка(1,4) });

            //sea.ПоставитьКорабль(1,
            //   new Точка[]{
            //    new Точка(4,1),
            //    new Точка(4,2),                
            //    new Точка(4,3) });
            //sea.Выстрел(new Точка(1,2));
            //sea.Выстрел(new Точка(4, 2));
            //sea.Выстрел(new Точка(4, 3));
            //sea.Выстрел(new Точка(4, 4));
            //sea.Выстрел(new Точка(5, 5));
            //ShowSea();
            //ShowFigh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sea_user.Выстрел(new Точка(0,0));
            
            //ShowShip(grid_user, new Точка(2, 5), 4);
            //ShowFight(grid_pc, new Точка(2, 5), Статус.ранил);
            //ShowFight(grid_pc, new Точка(2, 4), Статус.победил);
            return;

            //sea_user.Сброс();
            //sea_user.ПоставитьРовно();

            //sea_user.ПоставитьКорабль(0,
            //    new Точка[]{
            //    new Точка(1,1),
            //    new Точка(1,2),
            //    new Точка(1,3),
            //    new Точка(1,4) });

            //ShowShips(Grid_user, sea_user);           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sea_user.Сброс();
            int loop = 100;//клапан для блокировки зацикливания размещения
            while (--loop>0 && sea_user.создано < Море.всего_кораблей)
            {
                for (int nr = 0; nr < Море.всего_кораблей; nr++)
                    if (sea_user.НетКорабля(nr))
                    sea_user.ПоставитьСлучайно(nr);
            }
            if (sea_user.создано < Море.всего_кораблей) sea_user.Сброс();
          //  ShowShips(grid_user, sea_user);
        }

        //private void ShowSea()
        //{
        //    string text = "";
        //    for (int y = 0; y < Море.размер_моря.y; y++)
        //    { 
        //        for (int x = 0; x < Море.размер_моря.x; x++)                
        //            text += sea.КартаКораблей(new Точка(x, y)) == -1 ? " . " : "# ";
        //            text += Environment.NewLine;
        //     }
        //  //  textBox1.Text = text;
        //}

        //private void ShowFigh()
        //{
        //    string text = "";
        //    for (int x = 0; x < Море.размер_моря.x; x++)
        //    { 
        //        for (int y = 0; y < Море.размер_моря.y; y++)              
        //            switch (sea.КартаПопаданий(new Точка(x, y)))
        //            {
        //                case Статус.неизвестно: text += " . ";break;
        //                case Статус.мимо: text += "* "; break;
        //                case Статус.ранил: text += "x "; break;
        //                case Статус.убил: text += "X "; break;
        //            }
        //            text += Environment.NewLine;
        //        }
        //    textBox2.Text = text;
        //}

        private void ShowShip(DataGridView grid, Точка place, int nr) //отображение кораблей на сетках
        {
            if (nr < 0) grid[place.x, place.y].Style.BackColor = color_back;
            else grid[place.x, place.y].Style.BackColor = color_ship[nr];
        }

        private void ShowFight(DataGridView grid, Точка place, Статус status) //отображение кораблей на сетках
        {
            grid[place.x, place.y].Style.BackColor = color_fight[(int)status];           
        }


        //претенденты на делегатов в другие классы
        private void ShowUserShip(Точка place, int nr)
        {
            ShowShip(grid_user, place, nr);
        }

        private void ShowPcShip(Точка place, int nr)
        {
            ShowShip(grid_pc, place, nr);
        }

        private void ShowUserFight(Точка place, Статус status)
        {
            ShowFight(grid_user, place, status);
        }

        private void ShowPcFight(Точка place, Статус status)
        {
            ShowFight(grid_pc, place, status);
        }

        private void grid_user_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left) PlaceShip();
        }

        /// <summary>
        /// отрисовка ручками кораблей
        /// </summary>
        private void PlaceShip()
        {
            if (grid_user.SelectedCells.Count == 0 || grid_user.SelectedCells.Count>4) return;
            Точка[] ship = new Точка[(grid_user.SelectedCells.Count)];//массив точек будущего корабля
            int i = 0;
            foreach (DataGridViewCell cell in grid_user.SelectedCells)            
                ship[i++] = new Точка(cell.ColumnIndex, cell.RowIndex);                
            
            if (ship.Length == 1) sea_user.ОчиститьТочку(ship[0]);
            sea_user.ПоставитьПоТочкам(ship);
            grid_user.ClearSelection();
            ShowUnPlacedShips();//показать неразмещенные корабли
        }


        /// <summary>
        /// визуализация. Убираем те корабли которые уже не нужно ставить
        /// </summary>
        private void ShowUnPlacedShips()
        {
            sea_pc.ПоставитьРовно();
            for (int i = 0; i < Море.всего_кораблей; i++)
            {
                if (!sea_user.НетКорабля(i)) sea_pc.УбратьКорабль(i);
            }
        }

        //расстановка с клавиатуры
        private void grid_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PlaceShip();
        }
    }
}
