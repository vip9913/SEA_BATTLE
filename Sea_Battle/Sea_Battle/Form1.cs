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
        public Form1()
        {
            InitializeComponent();
        }

        Море sea = new Море();
        private void Form1_Load(object sender, EventArgs e)
        {          
            sea.ПоставитьКорабль(0, 
                new Точка[]{
                new Точка(1,1),
                new Точка(1,2),
                new Точка(1,3),
                new Точка(1,4) });

            sea.ПоставитьКорабль(1,
               new Точка[]{
                new Точка(4,1),
                new Точка(4,2),                
                new Точка(4,3) });
            sea.Выстрел(new Точка(1,2));
            sea.Выстрел(new Точка(4, 2));
            sea.Выстрел(new Точка(4, 3));
            sea.Выстрел(new Точка(4, 4));
            sea.Выстрел(new Точка(5, 5));
            ShowSea();
            ShowFigh();
        }

        private void ShowSea()
        {
            string text = "";
            for (int y = 0; y < Море.размер_моря.y; y++)
            { 
                for (int x = 0; x < Море.размер_моря.x; x++)                
                    text += sea.КартаКораблей(new Точка(x, y)) == -1 ? ".  " : "# ";
                    text += Environment.NewLine;
             }
            textBox1.Text = text;
        }

        private void ShowFigh()
        {
            string text = "";
            for (int x = 0; x < Море.размер_моря.x; x++)
            { 
                for (int y = 0; y < Море.размер_моря.y; y++)              
                    switch (sea.КартаПопаданий(new Точка(x, y)))
                    {
                        case Статус.неизвестно: text += ".  ";break;
                        case Статус.мимо: text += "* "; break;
                        case Статус.ранил: text += "x "; break;
                        case Статус.убил: text += "X "; break;
                    }
                    text += Environment.NewLine;
                }
            textBox2.Text = text;
        }
    }
}
