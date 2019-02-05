using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle
{   
    public partial class Form1 : Form
    {
        //Море sea_user = new Море();
        //Море sea_pc = new Море();
        Редактор sea_user;
        Редактор sea_pc;

        static string abc = "РЕСПУБЛИКА";//"АБВГДЕЖЗИКЛМНОПРСТУФХЧШЩЫЮЯ";
        Color color_back = Color.DarkSeaGreen; //цвет фона
        Color[] color_ship = {
                              Color.DarkOrange,
                              Color.DarkGreen,Color.DarkGreen,
                              Color.DarkViolet,Color.DarkViolet,Color.DarkViolet,
                              Color.DarkRed,Color.DarkRed,Color.DarkRed,Color.DarkRed };
        

        public Form1()
        {
            InitializeComponent();
            //sea_user = new Море();
            //sea_pc = new Море();
            sea_user = new Редактор();
            sea_pc = new Редактор();

            InitGrid(Grid_user);
            InitGrid(Grid_pc);
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

        private void ShowShips(DataGridView grid, Море sea)//перерисовываем картинку
        {
            for (int x = 0; x < Море.размер_моря.x; x++)
                for (int y = 0; y < Море.размер_моря.y; y++)
                {
                    int nr = sea.КартаКораблей(new Точка(x, y));
                    if (nr < 0)
                        grid[x, y].Style.BackColor = color_back;
                    else grid[x, y].Style.BackColor = color_ship[nr];
                }            
        }

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
            sea_user.Сброс();
            sea_user.ПоставитьРовно();
            //sea_user.ПоставитьКорабль(0,
            //    new Точка[]{
            //    new Точка(1,1),
            //    new Точка(1,2),
            //    new Точка(1,3),
            //    new Точка(1,4) });
            ShowShips(Grid_user, sea_user);
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
            ShowShips(Grid_user, sea_user);
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
    }
}
