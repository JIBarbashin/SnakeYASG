using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Seabattle
{
    class Cell
    {
        private bool isAttacked = false;
    }

    internal class Board
    {
        public int Speed { get; set; } = 1;
        public int Size { get; set; } = 10;

        private Rectangle whiteRect;
        private Rectangle blackRect;

        private Grid boardGrid;

        public Grid GetBoard
        {
            get { return boardGrid; }
        }

        public Board(Grid board)
        {
            this.boardGrid = board;
        }

        public void CreateBoard()
        {
            for (int i = 0; i <= Size; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                boardGrid.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i <= Size; i++)
            {
                RowDefinition row = new RowDefinition();
                boardGrid.RowDefinitions.Add(row);
            }

            for (int i = 1; i <= Size; i++)
            {
                TextBlock text = new TextBlock();
                text.Text = i.ToString();

                Grid.SetColumn(whiteRect, 0);
                Grid.SetRow(whiteRect, i);

                if (!boardGrid.Children.Contains(text))
                    boardGrid.Children.Add(text);
            }
        }


        public void Clear()
        {
            boardGrid.Children.Clear();
            boardGrid.RowDefinitions.Clear();
            boardGrid.ColumnDefinitions.Clear();
        }
    }
}
