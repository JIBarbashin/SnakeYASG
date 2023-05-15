using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Snake
{
    internal class SnakeHead
    { 
        public enum Directions { UP, DOWN, LEFT, RIGHT};
        private Directions direction = Directions.LEFT;
        public bool IsAlive { get; set; } = true;

        public int X { get; set; }
        public int Y { get; set; }

        private int size = 38;

        public Ellipse HeadImage = new Ellipse();

        private List<SnakePart> snakeParts = new List<SnakePart>();
        private int eatenApples;
        private int checkpointApples = 0;
        private int checkpointAllApples = 5;

        private World world;

        public World GetWorld
        {
            get { return world; }
        }

        public List<SnakePart> SnakeParts
        {
            get { return snakeParts; }
        }

        public int EatenApples
        {
            get { return eatenApples; }
            set { eatenApples = value; }
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

        public SnakeHead(World world)
        {
            this.world = world;

            X = world.Width / 2;
            Y = world.Height / 2;

            HeadImage.Fill = Brushes.DarkGreen;
            HeadImage.Width = size;
            HeadImage.Height = size;

            snakeParts.Add(new SnakePart(this));

            //Draw();
            //snakeParts[0].Draw(world.GetWorld);
        }

        public void TransCoordToGrid()
        {
            Grid.SetColumn(HeadImage, X);
            Grid.SetRow(HeadImage, Y);

            foreach (SnakePart part in snakeParts)
                part.TransCoordToGrid();
        }

        public void Draw()
        {
            if (world.GetWorld.Children.Contains(HeadImage))
                world.GetWorld.Children.Remove(HeadImage);

            world.GetWorld.Children.Add(HeadImage);

            DrawParts();
        }

        public void DrawParts()
        {
            foreach (SnakePart part in snakeParts)
                part.Draw(world.GetWorld);
        }

        public void MoveSnake()
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
                for (int i = 0; i < snakeParts.Count; i++)
                {
                    if (snakeParts[i].Y < 0) snakeParts[i].Y = world.Height - 1;
                    if (snakeParts[i].X < 0) snakeParts[i].X = world.Width - 1;
                    if (snakeParts[i].Y > world.Height - 1) snakeParts[i].Y = 0;
                    if (snakeParts[i].X > world.Width - 1) snakeParts[i].X = 0;
                }
            }

            if (Y < 0) Y = world.Height - 1;
            if (X < 0) X = world.Width - 1;
            if (Y > world.Height - 1) Y = 0;
            if (X > world.Width - 1) X = 0;
        }

        public void Collide()
        {
            foreach (SnakePart part in snakeParts)
            {
                if (X == part.X & Y == part.Y)
                    Death();
            }
            if (X == world.GetApple.X & Y == world.GetApple.Y)
                Eat();
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

        private void Eat()
        {
            checkpointApples++;
            eatenApples++;
            snakeParts.Add(new SnakePart(this));
            world.GetApple.Init();
            DrawParts();
            snakeParts.Last().TransCoordToGrid();

            if (checkpointApples >= checkpointAllApples)
            {
                checkpointApples = 0;
                if (world.Speed < world.MaxSpeed)
                    world.Speed += 0.5;
            }
        }

        private void Death()
        {
            IsAlive = false;
        }
    }
}
