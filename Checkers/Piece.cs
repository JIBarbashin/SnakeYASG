using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Checkers
{
    internal class Piece
    {
        private bool isKing = false;
        private bool isWhite;

        private Ellipse PieceEllipse = new Ellipse();

        public int X { get; set; }
        public int Y { get; set; }
        private int size = 32;


        private Board board;

        public Piece(Board board, bool isWhite, int X, int Y)
        {
            this.board = board;
            this.isWhite = true;
            this.X = X;
            this.Y = Y;

            if (isWhite)
                PieceEllipse.Fill = Brushes.White;
            else
                PieceEllipse.Fill = Brushes.Black;
            PieceEllipse.Width = size;
            PieceEllipse.Height = size;

            Draw();
        }

        private void PieceKing()
        {
            isKing = true;
            if (isWhite)
                PieceEllipse.Fill = Brushes.WhiteSmoke;
            else
                PieceEllipse.Fill = Brushes.DarkGray;
        }

        private void Move(int X, int Y)
        {
            this.X = X;
            this.Y = Y;

            Draw();
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            //do something here
        }

        private void Draw()
        {
            Grid.SetColumn(PieceEllipse,X);
            Grid.SetRow(PieceEllipse, Y);

            if (!board.GetBoard.Children.Contains(PieceEllipse))
                board.GetBoard.Children.Add(PieceEllipse);
            else
                board.GetBoard.Children.Remove(PieceEllipse);
        }
    }
}
