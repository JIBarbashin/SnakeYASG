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
        private bool isAlive = true;
        private bool isKing = false;
        private bool isWhite;

        private bool isTurning = false;
        private bool canTurning = true;
        private bool canBeat = false;
        private bool inDanger = false;

        private Ellipse PieceEllipse = new Ellipse();

        private SolidColorBrush blackBrush = Brushes.Black;
        private SolidColorBrush whiteBrush = Brushes.White;
        private SolidColorBrush blackKingBrush = Brushes.DarkGray;
        private SolidColorBrush whiteKingBrush = Brushes.WhiteSmoke;

        private List<Target> targets = new List<Target>();

        public int X { get; set; }
        public int Y { get; set; }
        private int size = 32;

        private List<Piece> piecesForBeat = new List<Piece>();

        public Piece(Board board, bool isWhite, int X, int Y)
        {
            PieceEllipse.MouseLeftButtonDown += new MouseButtonEventHandler(piece_MouseLeftButtonUp);

            this.isWhite = isWhite;
            this.X = X;
            this.Y = Y;

            if (isWhite)
                PieceEllipse.Fill = whiteBrush;
            else
                PieceEllipse.Fill = blackBrush;
            PieceEllipse.Width = size;
            PieceEllipse.Height = size;

            Draw();
            TransCoordToGrid();
        }

        private void piece_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Global.PlayerTeam == Global.CurrentTeam)
            {
                if (!isTurning)
                {
                    isTurning = true;
                    piecesForBeat = DetectedPieces();
                    if (canTurning)
                        CreateTargets(2);
                    else
                        isTurning = false;
                }
                else
                {
                    isTurning = false;
                    ClearTargets();
                }
            }
            e.Handled = true;
        }

        private void CreateTargets(int count)
        {
            int squares = 1;
            if (canBeat)
            {
                squares = 2;
            }
            if (isWhite)
            {
                if (X < Global.GameBoard.Size | Y > 0)
                    targets.Add(new Target(Global.GameBoard.GetBoard, this, X + squares, Y + squares));
                if (X > 0 | Y > 0)
                    targets.Add(new Target(Global.GameBoard.GetBoard, this, X - squares, Y + squares));
            }
            else
            {
                if (X < Global.GameBoard.Size | Y > 0)
                    targets.Add(new Target(Global.GameBoard.GetBoard, this, X + squares, Y - squares));
                if (X > 0 | Y > 0)
                    targets.Add(new Target(Global.GameBoard.GetBoard, this, X - squares, Y - squares));
            }
        }

        private void ClearTargets()
        {
            foreach (Target target in targets)
            {
                target.Clear();
            }
            targets.Clear();
        }

        private void PieceKing()
        {
            isKing = true;
            if (isWhite)
                PieceEllipse.Fill = whiteKingBrush;
            else
                PieceEllipse.Fill = blackKingBrush;
        }

        private List<Piece> DetectedPieces(int squares = 1)
        {
            List<Piece> result = new List<Piece>();
            foreach (Piece piece in Global.GameBoard.Pieces)
            {
                if (IsColliding(piece.X, piece.Y, squares, squares))
                {
                    if (isWhite != piece.isWhite)
                    {
                        result.Add(piece);
                        canBeat = true;

                        if (IsColliding(piece.X, piece.Y, 1, 1))
                            canTurning = false;
                    }
                    //if ()
                    return result;
                    //else
                    //    canTurning = false;
                }
            }
            return result;
        }

        private bool IsColliding(int X, int Y, int diff1 = 0, int diff2 = 0)
        {
            if ((this.X == X + diff1 & this.Y == Y + diff2))
                return true;
            else return false;
        }

        public void Move(int X, int Y)
        {
            this.X = X;
            this.Y = Y;

            TransCoordToGrid();
            ClearTargets();

            if (canBeat)
            {
                bool killed = false;
                foreach (Piece piece in piecesForBeat)
                {
                    if (IsColliding(piece.X, piece.Y, 1, 1))
                    {
                        killed = true;
                        piece.Clear();

                    }
                }
                if (!killed)
                {
                    Clear();
                }
            }

            if ((isWhite & this.Y == 0) | (!isWhite & this.Y == Global.GameBoard.Size))
                PieceKing();

            if (Global.CurrentTeam == Global.PlayerTeam)
                Global.CurrentTeam = Global.AITeam;
            else
                Global.CurrentTeam = Global.PlayerTeam;
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            //do something here
        }

        private void TransCoordToGrid()
        {
            Grid.SetColumn(PieceEllipse, X);
            Grid.SetRow(PieceEllipse, Y);
        }

        private void Draw()
        {
            if (!Global.GameBoard.GetBoard.Children.Contains(PieceEllipse))
                Global.GameBoard.GetBoard.Children.Add(PieceEllipse);
        }

        private void Clear()
        {
            Global.GameBoard.GetBoard.Children.Remove(PieceEllipse);
            Global.GameBoard.Pieces.Remove(this);
        }
    }
}
