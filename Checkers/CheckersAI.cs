using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    internal class CheckersAI
    {
        private static Random random = new Random();
        private static List<Piece> pieceList = new List<Piece>();

        public static void Init()
        {
            if (Global.AITeam == 0)
                pieceList = Global.GameBoard.WhitePieces;
            else
                pieceList = Global.GameBoard.BlackPieces;
        }

        public static void Analize()
        {

        }

        public static void Turn()
        {
            if (Global.CurrentTeam == Global.AITeam)
            {
                Piece choosedPiece = pieceList[random.Next(0, pieceList.Count - 1)];
                choosedPiece.Move(choosedPiece.X + 1, choosedPiece.Y + 1);
            }
        }
    }
}
