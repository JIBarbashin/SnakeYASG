namespace SnakeYASG
{
    public class Apple : GameObject
    {
        public int Eaten { get; private set; } = 0;

        public Apple()
        {
            visual = Game.APPLE_CHAR;
        }

        public void IncreaseApple()
        {
            Eaten++;
        }
    }
}
