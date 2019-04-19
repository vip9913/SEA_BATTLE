using System;
using System.Windows.Forms;

namespace Sea_Battle
{

    public partial class FormGame : Form
    {
        //Море sea_user = new Море();
        //Море sea_pc = new Море();
        
        Редактор sea_pc;
        Редактор sea_user;

        SeaGrid GridUser;
        SeaGrid GridComp;


        Mission mission; //добавление класса ИИ компьютер

        enum Mode //режимы игры
        {
            EditShips,
            PlayUser,
            PlayComp,
            Finish
        };

        Mode mode;

        public FormGame()
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

            GridUser = new SeaGrid(grid_user);
            GridComp = new SeaGrid(grid_pc);

            // InitGrid(grid_user);
            // InitGrid(grid_pc);

            Restart();
            timer1.Enabled = true;//для ходов компьютера        
        }


        private void Restart()
        {
            mode = Mode.EditShips;
            sea_user.Сброс();
            sea_pc.Сброс();
            sea_pc.ПоставитьРовно();
            ShowUnPlacedShips();
            bt_Random.Visible = true;
            bt_Clear.Visible = true;
            bt_Start.Visible = true;
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

            sea_user.ПоставитьСлучайно();
            ShowUnPlacedShips();
            //sea_user.Сброс();
            //int loop = 100;//клапан для блокировки зацикливания размещения
            //while (--loop>0 && sea_user.создано < Море.всего_кораблей)
            //{
            //    for (int nr = 0; nr < Море.всего_кораблей; nr++)
            //        if (sea_user.НетКорабля(nr))
            //        sea_user.ПоставитьСлучайно(nr);
            //}
            //if (sea_user.создано < Море.всего_кораблей) sea_user.Сброс();
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

     


        //претенденты на делегатов в другие классы
        private void ShowUserShip(Точка place, int nr)
        {
           GridUser.ShowShip(place, nr);
        }

        /// <summary>
        /// показывать корабли противника и показывать корабли в режиме редактирования сколько их осталось не установлено
        /// </summary>
        /// <param name="place"></param>
        /// <param name="nr"></param>
        private void ShowPcShip(Точка place, int nr)
        {
            //ShowShip(grid_pc, place, nr);
            if (mode==Mode.EditShips)  GridComp.ShowShip(place, nr);
        }

        private void ShowUserFight(Точка place, Статус status)
        {
            //ShowFight(grid_user, place, status);
            GridUser.ShowFight(place, status);
        }

        private void ShowPcFight(Точка place, Статус status)
        {
            //ShowFight(grid_pc, place, status);
            GridComp.ShowFight(place, status);
        }

        private void grid_user_MouseUp(object sender, MouseEventArgs e)
        {           
            if (e.Button==MouseButtons.Left) PlaceShip();
            grid_user.ClearSelection();
        }

        /// <summary>
        /// отрисовка ручками кораблей
        /// </summary>
        private void PlaceShip()
        {
            //if (grid_user.SelectedCells.Count == 0 || grid_user.SelectedCells.Count>4) return;
            //Точка[] ship = new Точка[(grid_user.SelectedCells.Count)];//массив точек будущего корабля
            //int i = 0;
            //foreach (DataGridViewCell cell in grid_user.SelectedCells)            
            //    ship[i++] = new Точка(cell.ColumnIndex, cell.RowIndex);                

            //if (ship.Length == 1) sea_user.ОчиститьТочку(ship[0]);
            //sea_user.ПоставитьПоТочкам(ship);
            //grid_user.ClearSelection();

            if (mode != Mode.EditShips) return;
            Точка[] ship= GridUser.GetSelectedCells();
            if (ship == null) return;
            if (ship.Length == 1) sea_user.ОчиститьТочку(ship[0]);
            sea_user.ПоставитьПоТочкам(ship);
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
            bt_Start.Enabled = (sea_user.создано == Море.всего_кораблей);
        }

        //расстановка с клавиатуры
        private void grid_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PlaceShip();
            grid_user.ClearSelection();
        }

        private void FormGame_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //ShowPcShip();
            }
        }

        private void bt_Random_Click(object sender, EventArgs e)
        {
            if (mode != Mode.EditShips) return;
               sea_user.ПоставитьСлучайно();
               ShowUnPlacedShips();            
        }

        private void bt_Clear_Click(object sender, EventArgs e)
        {
            if (mode != Mode.EditShips) return;
            sea_user.Сброс();
            ShowUnPlacedShips();
        }

        private void bt_Restart_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void bt_Start_Click(object sender, EventArgs e)
        {
            if (mode != Mode.EditShips) return;
            if (sea_user.создано == Море.всего_кораблей)
            {
                mode = Mode.PlayUser;
                sea_pc.ПоставитьСлучайно();
                mission = new Mission(sea_user);                                
                bt_Random.Visible = false;
                bt_Clear.Visible = false;
                bt_Start.Visible = false;
            }
        }

     
        private void grid_pc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grid_pc.ClearSelection();
            if (mode != Mode.PlayUser) return; //если не наш ход
            Статус status=sea_pc.Выстрел(new Точка(e.ColumnIndex, e.RowIndex));           
            switch (status)
            {
                case Статус.неизвестно:                   
                case Статус.мимо:       mode = Mode.PlayComp;
                                        break;
                case Статус.ранил:                    
                case Статус.убил:
                                        mode = Mode.PlayUser;
                                        break;
                case Статус.победил:    mode = Mode.Finish; WinUser();
                                        break;               
            }
         //   while (mode == Mode.PlayComp) CompFight(); //стреляет комп           
        }

        private void CompFight()
        {
            Точка point;
            Статус status=mission.Fight(out point);
            switch (status)
            {
                case Статус.неизвестно:
                case Статус.мимо:
                    mode = Mode.PlayUser;
                    break;
                case Статус.ранил:
                case Статус.убил:
                    mode = Mode.PlayComp;
                    break;
                case Статус.победил:
                    mode = Mode.Finish; WinComp();
                    break;
            }
        }

        private void WinUser()
        {
            MessageBox.Show("Победа! Ты затопил корабли компьютера.");
        }

        private void WinComp()
        {
            MessageBox.Show("Победа компьютера!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mode == Mode.PlayComp) CompFight();
        }
    }
}
