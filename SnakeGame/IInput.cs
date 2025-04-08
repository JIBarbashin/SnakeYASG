using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeYASG
{
    public struct Key
    {
        public static int Null = 0;
        public static int A;
        public static int D;
        public static int Q;
        public static int R;
        public static int S;
        public static int W;
        public static int Escape;
        public static int Enter;
        public static int Space;
        public static int Left;
        public static int Right;
        public static int Up;
        public static int Down;
    }

    public interface IInput
    {
        public bool KeyAvailable();
        bool IsKeyPressed(int keyCode);
        bool IsKeyReleased(int keyCode);
    }
}
