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

        private int size = 20;
        public Ellipse HeadImage = new Ellipse();

        private List<SnakePart> snakeParts = new List<SnakePart>();
        private int eatenApples;

        private World world;

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
            //this.eatenApples = eatenApples;
            //IsAlive = true;

            X = world.Width / 2;
            Y = world.Height / 2;

            HeadImage.Fill = Brushes.DarkGreen;
            HeadImage.Width = size;
            HeadImage.Height = size;

            snakeParts.Add(new SnakePart(this));

            Draw();
            foreach (SnakePart part in snakeParts)
                part.Draw(world.GetWorld);
        }

        public void Draw()
        {
            if (world.GetWorld.Children.Contains(HeadImage))
                world.GetWorld.Children.Remove(HeadImage);

            Grid.SetColumn(HeadImage, X);
            Grid.SetRow(HeadImage, Y);

            world.GetWorld.Children.Add(HeadImage);
        }

        public void MoveSnake()
        {
            switch (direction)
            {
                case Directions.UP: Y--; break;
                case Directions.DOWN: Y++; break;
                case Directions.LEFT: X--; break;
                case Directions.RIGHT: X++; break;
            }

            if (Y < 0)  Y = 32;
            if (X < 1)  X = 32;
            if (Y > 32) Y = 0;
            if (X > 32) X = 1;

            Draw();

            foreach (SnakePart part in snakeParts)
            {
                switch (direction)
{
                    case Directions.UP: part.Y--; break;
                    case Directions.DOWN: part.Y++; break;
                    case Directions.LEFT: part.X--; break;
                    case Directions.RIGHT: part.X++; break;
                }

                if (part.Y < 0) part.Y = 32;
                if (part.X < 1) part.X = 32;
                if (part.Y > 32) part.Y = 0;
                if (part.X > 32) part.X = 1;

                part.Draw(world.GetWorld);

                if (X == part.X & Y == part.Y)
                    Death();
            }
            if (X == world.GetApple.X & Y == world.GetApple.Y)
                Eat(world.GetApple);
        }

        private void Eat(Apple apple)
        {  
            eatenApples += 1;
            snakeParts.Add(new SnakePart(this));
            apple.Init();
        }

        private void Death()
        {
            IsAlive = false;
            eatenApples = 0;
        }
    }
}
