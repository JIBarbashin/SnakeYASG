using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Checkers
{
    internal class Board
    {
        public int Speed { get; set; } = 1;
        public int Size { get; set; } = 10;

        private Rectangle whiteRect;
        private Rectangle blackRect;

        private Grid boardGrid;
        private List<Piece> pieces = new List<Piece>();

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
            for (int i = 0; i < Size; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                boardGrid.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < Size; i++)
            {
                RowDefinition row = new RowDefinition();
                boardGrid.RowDefinitions.Add(row);
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if ((j % 2 == 0 & i % 2 == 0) | (j % 2 == 1 & i % 2 == 1))
                    {
                        whiteRect = new Rectangle();
                        whiteRect.Fill = Brushes.Khaki;
                        whiteRect.Width = 128;
                        whiteRect.Height = 128;

                        Grid.SetColumn(whiteRect, i);
                        Grid.SetRow(whiteRect, j);

                        if (!boardGrid.Children.Contains(whiteRect))
                            boardGrid.Children.Add(whiteRect);
                    }
                    else
                    {
                        blackRect = new Rectangle();
                        blackRect.Fill = Brushes.Chocolate;
                        blackRect.Width = 128;
                        blackRect.Height = 128;

                        Grid.SetColumn(blackRect, i);
                        Grid.SetRow(blackRect, j);

                        if (!boardGrid.Children.Contains(blackRect))
                            boardGrid.Children.Add(blackRect);
                    }
                }
            }
        }

        public void PiecePlacement()
        {
            int i, j;
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < Size; j++)
                {
                    if ((j % 2 == 1 & i % 2 == 0) | (j % 2 == 0 & i % 2 == 1))
                        pieces.Add(new Piece(this, true, j, i));
                }
            }
            for (i = 7; i < Size; i++)
            {
                for (j = 0; j < Size; j++)
                {
                    if ((j % 2 == 1 & i % 2 == 0) | (j % 2 == 0 & i % 2 == 1))
                        pieces.Add(new Piece(this, false, j, i));
                }
            }
        }

        public void Clear()
        {
            boardGrid.Children.Clear();
            boardGrid.RowDefinitions.Clear();
            boardGrid.ColumnDefinitions.Clear();
            pieces.Clear();
        }
    }
}
