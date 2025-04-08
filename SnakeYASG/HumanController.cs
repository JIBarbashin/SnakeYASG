namespace SnakeYASG
{
    public class HumanController : Controller
    {
        private readonly int id;
        private readonly IInput input;

        public HumanController(int id, SnakeHead snake, IInput input) : base(snake)
        {
            this.id = id;
            this.input = input;
        }

        private bool Pressed(int a, int b)
        {
            return id == 0 & input.IsKeyPressed(a) | id == 1 & input.IsKeyPressed(b);
        }

        public override void Update()
        {
            if (input.KeyAvailable())
            {
                if (Pressed(Key.W, Key.Up))
                {
                    SetCommand(moveUp);
                }
                else if (Pressed(Key.S, Key.Down))
                {
                    SetCommand(moveDown);
                }
                else if(Pressed(Key.A, Key.Left))
                {
                    SetCommand(moveLeft);
                }
                else if (Pressed(Key.D, Key.Right))
                {
                    SetCommand(moveRight);
                }

                ExecuteCommand();
            }
        }
    }
}
