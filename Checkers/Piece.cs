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

        private Ellipse pieceEllipse = new Ellipse();

        private SolidColorBrush blackBrush = Brushes.Black;
        private SolidColorBrush whiteBrush = Brushes.White;
        private SolidColorBrush blackKingBrush = Brushes.DarkGray;
        private SolidColorBrush whiteKingBrush = Brushes.WhiteSmoke;

        private List<Target> targets = new List<Target>();

        private List<Piece> piecesForBeat = new List<Piece>();

        public int X { get; set; }
        public int Y { get; set; }
        private int size = 32;

        private Piece leftPiece;
        private Piece rightPiece;

        public bool IsWhite
        {
            get { return isWhite; }
        }

        public Ellipse PieceEllipse
        {
            get { return pieceEllipse; }
        }

        public Piece(bool isWhite, int X, int Y)
        {
            pieceEllipse.MouseLeftButtonDown += new MouseButtonEventHandler(piece_MouseLeftButtonUp);

            this.isWhite = isWhite;
            this.X = X;
            this.Y = Y;

            if (isWhite)
                pieceEllipse.Fill = whiteBrush;
            else
                pieceEllipse.Fill = blackBrush;
            pieceEllipse.Width = size;
            pieceEllipse.Height = size;

            Draw();
            TransCoordToGrid();
        }

        private void piece_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Global.PlayerTeam == Global.CurrentTeam)
            {
                if ((Global.PlayerTeam == 0 & isWhite) | (Global.PlayerTeam == 1 & !isWhite))
                {
                    if (!isTurning)
                    {
                        isTurning = true;
                        CreateTargets();
                    }
                    else
                    {
                        isTurning = false;
                        ClearTargets();
                    }
                }
            }
            e.Handled = true;
        }

        private void CreateTargets()
        {
            if (isWhite)
            {
                leftPiece = GetPiece(X - 1, Y + 1);
                rightPiece = GetPiece(X + 1, Y + 1);

                if (rightPiece != null)
                {
                    if (!rightPiece.isWhite & IsColliding(rightPiece, X + 1, Y + 1))
                    {
                        canBeat = true;
                        targets.Add(new Target(this, X + 2, Y + 2));
                    }
                    else
                    {
                        targets.Add(new Target(this, X + 1, Y + 1));
                    }
                }
                if (leftPiece != null)
                {
                    if (!leftPiece.isWhite & IsColliding(leftPiece, X - 1, Y + 1))
                    {
                        canBeat = true;
                        targets.Add(new Target(this, X - 2, Y + 2));
                    }
                    else
                    {
                        targets.Add(new Target(this, X - 1, Y + 1));
                    }
                }
                if (leftPiece == null | rightPiece == null)
                {
                    targets.Add(new Target(this, X - 1, Y + 1));
                    targets.Add(new Target(this, X + 1, Y + 1));
                }
            }
            else if (!isWhite)
            {
                leftPiece = GetPiece(X - 1, Y - 1);
                rightPiece = GetPiece(X + 1, Y - 1);

                if (rightPiece != null)
                {
                    if (rightPiece.isWhite & IsColliding(rightPiece, X + 1, Y - 1))
                    {
                        canBeat = true;
                        targets.Add(new Target(this, X + 2, Y - 2));
                    }
                    else
                    {
                        targets.Add(new Target(this, X + 1, Y - 1));
                    }
                }
                else if (leftPiece != null)
                {
                    if (leftPiece.isWhite & IsColliding(leftPiece, X - 1, Y - 1))
                    {
                        canBeat = true;
                        targets.Add(new Target(this, X - 2, Y - 2));
                    }
                    else
                    {
                        targets.Add(new Target(this, X - 1, Y - 1));
                    }
                }
                if (leftPiece == null | rightPiece == null)
                {
                    targets.Add(new Target(this, X - 1, Y - 1));
                    targets.Add(new Target(this, X + 1, Y - 1));
                }
            }
            else if (isKing)
            {
                
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
                pieceEllipse.Fill = whiteKingBrush;
            else
                pieceEllipse.Fill = blackKingBrush;
        }

        public Piece GetPiece(int X, int Y)
        {
            foreach (Piece piece in Global.GameBoard.Pieces)
            {
                if (piece.X == X & piece.Y == Y)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool IsColliding(Piece p, int X, int Y)
        {
            if (p != null)
            {
                if (p.X == X & p.Y == Y)
                {
                    return true;
                }
                else return false;
            }
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
                if (leftPiece != null)
                    KillPiece(leftPiece);
                if (rightPiece != null)
                    KillPiece(rightPiece);
            }

            if ((isWhite & this.Y == Global.GameBoard.Size) | (!isWhite & this.Y == 0))
                PieceKing();

            Global.SwitchTeam();
        }

        private void KillPiece(Piece p)
        {
            bool killed = false;
            if ((IsColliding(this, p.X + 1, p.Y + 1)) |
                (IsColliding(this, p.X - 1, p.Y - 1)) |
                (IsColliding(this, p.X + 1, p.Y - 1)) |
                (IsColliding(this, p.X - 1, p.Y + 1)))
            {
                killed = true;
                p.Clear();
            }
            if (!killed)
            {
                Clear();
            }
        }

        private void TransCoordToGrid()
        {
            Grid.SetColumn(pieceEllipse, X);
            Grid.SetRow(pieceEllipse, Y);
        }

        private void Draw()
        {
            if (!Global.GameBoard.GetBoard.Children.Contains(pieceEllipse))
                Global.GameBoard.GetBoard.Children.Add(pieceEllipse);
        }

        private void Clear()
        {
            Global.GameBoard.GetBoard.Children.Remove(pieceEllipse);
            Global.GameBoard.Pieces.Remove(this);
        }
    }
}
