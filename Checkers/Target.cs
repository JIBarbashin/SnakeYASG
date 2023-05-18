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
    internal class Target
    {
        public int X { get; set; }
        public int Y { get; set; }

        private Rectangle targetRect = new Rectangle();
        private Piece piece; 

        public Target(Piece piece, int X, int Y)
        {
            targetRect.MouseLeftButtonDown += new MouseButtonEventHandler(target_MouseLeftButtonUp);

            this.piece = piece;

            this.X = X;
            this.Y = Y;

            targetRect.Fill = Brushes.Aqua;
            targetRect.Width = 32;
            targetRect.Height = 32;


            if (!Global.GameBoard.GetBoard.Children.Contains(targetRect))
            {
                if ((X >= 0 & Y >= 0) & (X <= Global.GameBoard.Size & Y <= Global.GameBoard.Size))
                {
                    Grid.SetColumn(targetRect, this.X);
                    Grid.SetRow(targetRect, this.Y);

                    if (!piece.IsColliding(piece.GetPiece(X, Y), X, Y))
                        Global.GameBoard.GetBoard.Children.Add(targetRect);
                }
            }
        }

        void target_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            piece.Move(X, Y);
            //Clear();
            e.Handled = true;
        }

        public void Clear()
        {
            Global.GameBoard.GetBoard.Children.Remove(targetRect);
        }
    }
}
