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
        private Directions direction = Directions.UP;
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
            set { direction = value; }
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
            snakeParts.Add(new SnakePart(this));

            //Draw();
            //snakeParts[0].Draw(world.GetWorld);
        }

        public void Draw()
        {
            if (world.GetWorld.Children.Contains(HeadImage))
                world.GetWorld.Children.Remove(HeadImage);

            Grid.SetColumn(HeadImage, X);
            Grid.SetRow(HeadImage, Y);

            world.GetWorld.Children.Add(HeadImage);

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

            if (Y < 0) Y = world.Height;
            if (X < 0) X = world.Width;
            if (Y > world.Height) Y = 0;
            if (X > world.Width) X = 0;

            if (snakeParts.Count > 0)
            {
                snakeParts[0].X = headPrevX;
                snakeParts[0].Y = headPrevY;
                int partPrevX = snakeParts[0].X;
                int partPrevY = snakeParts[0].Y;
                int partPrev2X, partPrev2Y;
                for (int i = 1; i < snakeParts.Count; i++)
                {
                    partPrev2X = snakeParts[i].X;
                    partPrev2Y = snakeParts[i].Y;
                    snakeParts[i].X = partPrevX;
                    snakeParts[i].Y = partPrevY;
                    partPrevX = partPrev2X;
                    partPrevY = partPrev2Y;

                    if (snakeParts[i].Y < 0) snakeParts[i].Y = world.Height;
                    if (snakeParts[i].X < 0) snakeParts[i].X = world.Width;
                    if (snakeParts[i].Y > world.Height) snakeParts[i].Y = 0;
                    if (snakeParts[i].X > world.Width) snakeParts[i].X = 0;
                }
            }
        }

        public void Collide()
        {
            foreach (SnakePart part in snakeParts)
            {
                if (X == part.X & Y == part.Y)
                    Death();
            }
            if (X == world.GetApple.X & Y == world.GetApple.Y)
                Eat(world.GetApple);
        }

        private void Eat(Apple apple)
        {
            checkpointApples++;
            eatenApples++;
            snakeParts.Add(new SnakePart(this));
            apple.Init();

            if (checkpointApples >= checkpointAllApples)
            {
                checkpointApples = 0;
                world.Speed /= 2;
            }
        }

        private void Death()
        {
            IsAlive = false;
            //eatenApples = 0;
        }
    }
}
