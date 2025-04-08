namespace SnakeYASG
{
    internal class Input : IInput
    { 
        ConsoleKeyInfo key;
        public Input()
        {
            Key.A = 65;
            Key.D = 68;
            Key.Q = 81;
            Key.R = 82;
            Key.S = 83;
            Key.W = 87;
            Key.Escape = 27;
            Key.Enter = 13;
            Key.Space = 32;
            Key.Left = 37;
            Key.Right = 39;
            Key.Up = 38;
            Key.Down = 40;
        }

        public bool KeyAvailable()
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(intercept: true);
                return true;
            }
            return false;
        }

        public bool IsKeyPressed(int keyCode)
        {
            return key.Key == (ConsoleKey)keyCode;
        }

        public bool IsKeyReleased(int keyCode)
        {
            return false;
        }
    }
}
