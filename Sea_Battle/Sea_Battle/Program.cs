﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle
{
    public enum Статус  //описание статуса кораблей
    {
        неизвестно, //0
        мимо,       //1
        ранил,
        убил,
        победил
    }
    public struct Точка  //структура точка
    {
        public int x, y;
        public Точка(int x, int y)  
        {
            this.x = x;
            this.y = y;
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


    }
}
