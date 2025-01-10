using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Snake
{
    public class SnakeHead : GameObject
    {
        public enum Directions { UP, DOWN, LEFT, RIGHT };
        private Directions direction = Directions.LEFT;
        public bool IsAlive { get; set; } = true;

        public int Speed { get; set; }

        public Ellipse HeadImage = new();
        private List<SnakePart> snakeParts = new();

        private int collidedPart = 0;
        public int CollidedPart { get { return collidedPart; } }

        private SoundPlayer sp = new();

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
            X = x;
            Y = y;

            Size = 12;

            HeadImage.Fill = Brushes.DarkGreen;
            HeadImage.Width = Size;
            HeadImage.Height = Size;

            snakeParts.Add(new SnakePart());

            //Draw();
            //snakeParts[0].Draw(world.GetWorld);
        }

        //public void TransCoordToGrid()
        //{
        //    Grid.SetColumn(HeadImage, X);
        //    Grid.SetRow(HeadImage, Y);

        //    foreach (SnakePart part in snakeParts)
        //        part.TransCoordToGrid();
        //}

        public void Draw(ref Grid world)
        {
            if (world.Children.Contains(HeadImage))
                world.Children.Remove(HeadImage);

            world.Children.Add(HeadImage);

            DrawParts(ref world);
        }

        public void DrawParts(ref Grid world)
        {
            foreach (SnakePart part in snakeParts)
                part.Draw(world);
        }

        public void Move()
        {
            int headPrevX = X;
            int headPrevY = Y;
            switch (direction)
            {
                case Directions.UP: Y--; break;
                case Directions.DOWN: Y++; break;
                case Directions.LEFT: X--; break;
                case Directions.RIGHT: X++; break;
            }

            if (snakeParts.Count > 0)
            {
                int partPrevX = snakeParts[0].X;
                int partPrevY = snakeParts[0].Y;
                snakeParts[0].X = headPrevX;
                snakeParts[0].Y = headPrevY;
                int partPrev2X, partPrev2Y;
                for (int i = 1; i < snakeParts.Count; i++)
                {
                    partPrev2X = snakeParts[i].X;
                    partPrev2Y = snakeParts[i].Y;
                    snakeParts[i].X = partPrevX;
                    snakeParts[i].Y = partPrevY;
                    partPrevX = partPrev2X;
                    partPrevY = partPrev2Y;
                }
            }
        }

        public bool IsColliding(int x, int y)
        {
            bool col = false;

            if (X == x & Y == y)
            {
                col = true;
            }
            else
            {
                foreach (SnakePart part in snakeParts)
                {
                    if (part.X == x & part.Y == y)
                    {
                        col = true;
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

        public void Eat(ref Apple apple)
        {
            //sp.SoundLocation = "coin_sound.wav";
            //sp.Load();
            //sp.Play();

            int x = snakeParts.Last().X;
            int y = snakeParts.Last().Y;

            //checkpointApples++;
            //eatenApples++;
            snakeParts.Add(new SnakePart());
            apple.Init();
            //DrawParts();
            snakeParts.Last().X = x;
            snakeParts.Last().Y = y;
            //snakeParts.Last().TransCoordToGrid();
            World.TransCoordToGrid(snakeParts.Last().PartImage, snakeParts.Last().X, snakeParts.Last().Y);

            //if (checkpointApples >= checkpointAllApples)
            //{
            //    checkpointApples = 0;
            //    Game.Instance.ChangeSpeed(IsPlayer);
            //}
        }

        public void Death(int index)
        {
            //sp.SoundLocation = "death_from_cringe.wav";
            //sp.Load();
            //sp.Play();

            collidedPart = index;
            IsAlive = false;
        }
    }
}
