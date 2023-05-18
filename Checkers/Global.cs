using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    internal class Global
    {
        public static Board GameBoard { get; set; }
        public static int CurrentTeam { get; set; }
        public static int PlayerTeam { get; set; }
        public static int AITeam { get; set; }
        public static int PlayerScore { get; set; }
        public static int AIScore { get; set; }

        public static void SwitchTeam()
        {
            if (CurrentTeam == PlayerTeam)
            {
                CurrentTeam = AITeam;
                CheckersAI.Turn();
            }
            else
            {
                CurrentTeam = PlayerTeam;
            }
        }
    }
}
