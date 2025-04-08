using SnakeGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;

namespace SnakeWin
{
    internal class Input : IInput
    {
        public Input()
        {
            Key.A = 65;
            Key.D = 68;
            Key.Q = 81;
            Key.R = 82;
            Key.S = 83;
            Key.W = 87;
            Key.Escape = 256;
            Key.Enter = 257;
            Key.Space = 32;
            Key.Left = 263;
            Key.Right = 262;
            Key.Up = 265;
            Key.Down = 264;
        }

        public bool IsKeyPressed(int keyCode)
        {
            return Raylib.IsKeyPressed(keyCode);
        }

        public bool IsKeyReleased(int keyCode)
        {
            return false;
        }

        public bool KeyAvailable()
        {
            return true;
        }
    }
}
