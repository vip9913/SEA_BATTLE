using System.Drawing;
using System.Windows.Forms;

namespace Sea_Battle
{
    class SeaGrid
    {
        DataGridView grid;

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

        public SeaGrid(DataGridView grid)
        {
            this.grid = grid;
            InitGrid();
        }

        private void InitGrid()
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
                grid.Rows[y].HeaderCell.Value = (y + 1).ToString();
            }
            grid.Height = Море.размер_моря.y * grid.Rows[0].Height + grid.ColumnHeadersHeight + 2;
            grid.ClearSelection();//что бы ничего не отмечалось на сетке
        }

        public void ShowShip(Точка place, int nr) //отображение кораблей на сетках
        {
            if (nr < 0) grid[place.x, place.y].Style.BackColor = color_back;
            else grid[place.x, place.y].Style.BackColor = color_ship[nr];
        }

        public void ShowFight(Точка place, Статус status) //отображение кораблей на сетках
        {
            grid[place.x, place.y].Style.BackColor = color_fight[(int)status];
        }

        public Точка[] GetSelectedCells()
        {
            if (grid.SelectedCells.Count == 0 || grid.SelectedCells.Count > 4) return null;
            Точка[] ship = new Точка[(grid.SelectedCells.Count)];//массив точек будущего корабля
            int i = 0;
            foreach (DataGridViewCell cell in grid.SelectedCells)
                ship[i++] = new Точка(cell.ColumnIndex, cell.RowIndex);          
            grid.ClearSelection();
            return ship;
        }
    }
}
