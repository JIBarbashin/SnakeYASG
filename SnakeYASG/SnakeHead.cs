namespace SnakeYASG
{
    public class SnakeHead : GameObject
    {
        public delegate void SnakeCollidingHandler(Vector2I pos);
        public event SnakeCollidingHandler onSnakeColliding;

        public delegate void SnakeTailHandler(SnakePart tail);
        public event SnakeTailHandler onTailIncresed;
        public event SnakeTailHandler onTailDecreased;

        public enum Directions { UP, DOWN, LEFT, RIGHT };
        private Directions direction = Directions.LEFT;
        private List<SnakePart> snakeParts = new();

        public bool IsAlive { get; private set; }

        public List<SnakePart> SnakeParts
        {
            get { return snakeParts; }
        }

        public Directions Direction
        {
            get { return direction; }
            set
            {
                Directions prevDirection = direction;
                if (prevDirection == Directions.UP & value == Directions.DOWN)
                    direction = prevDirection;
                else if (prevDirection == Directions.DOWN & value == Directions.UP)
                    direction = prevDirection;
                else if (prevDirection == Directions.LEFT & value == Directions.RIGHT)
                    direction = prevDirection;
                else if (prevDirection == Directions.RIGHT & value == Directions.LEFT)
                    direction = prevDirection;
                else direction = value;
            }
        }

        public SnakeHead(int x, int y)
        {
            position = new Vector2I(x, y);
            visual = Game.SNAKE_HEAD_CHAR;
            snakeParts.Add(new SnakePart());
            IsAlive = true;
        }

        public void Move()
        {
            int headPrevX = position.X;
            int headPrevY = position.Y;

            switch (direction)
            {
                case Directions.UP: position.Y--; break;
                case Directions.DOWN: position.Y++; break;
                case Directions.LEFT: position.X--; break;
                case Directions.RIGHT: position.X++; break;
            }

            if (snakeParts.Count > 0)
            {
                int partPrevX = snakeParts[0].Position.X;
                int partPrevY = snakeParts[0].Position.Y;
                snakeParts[0].Position = new(headPrevX, headPrevY);
                int partPrev2X, partPrev2Y;
                for (int i = 1; i < snakeParts.Count; i++)
                {
                    partPrev2X = snakeParts[i].Position.X;
                    partPrev2Y = snakeParts[i].Position.Y;
                    snakeParts[i].Position = new(partPrevX, partPrevY);
                    partPrevX = partPrev2X;
                    partPrevY = partPrev2Y;
                }
            }

            foreach (SnakePart part in snakeParts)
            {
                if (part.Position.X == Position.X & part.Position.Y == Position.Y)
                {
                    onSnakeColliding?.Invoke(position);
                    Death();
                }
            }
        }

        public bool IsColliding(int x, int y)
        {
            bool col = false;

            if (position.X == x & position.Y == y)
            {
                col = true;
                onSnakeColliding?.Invoke(position);
            }
            else
            {
                foreach (SnakePart part in snakeParts)
                {
                    if (part.Position.X == x & part.Position.Y == y)
                    {
                        col = true;
                        onSnakeColliding?.Invoke(position);
                        break;
                    }
                    else
                    {
                        col = false;
                    }
                }
            }
            return col;
        }

        public void IncreaseTail()
        {
            int x = snakeParts.Last().Position.X;
            int y = snakeParts.Last().Position.Y;
            snakeParts.Add(new SnakePart());
            snakeParts.Last().Position = new(x, y);
            onTailIncresed?.Invoke(snakeParts.Last());
        }

        public void DecreaseTail()
        {
            int x = snakeParts.Last().Position.X;
            int y = snakeParts.Last().Position.Y;
            snakeParts.Add(new SnakePart());
            snakeParts.Remove(snakeParts.Last());
            //onTailDecreased?.Invoke(snakeParts.Last());
        }

        public void Death()
        {
            IsAlive = false;
        }
    }
}
