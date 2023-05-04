using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Snake
{
    internal class World
    {
        public int Speed { get; set; } = 1;
        public int Width { get; set; } = 16;
        public int Height { get; set; } = 16;

        private SnakeHead snake;
        private Apple apple;

        private Grid world;

        public Grid GetWorld
        {
            get { return world; }
        }

        public SnakeHead GetSnake
        {
            get { return snake; }
        }

        public Apple GetApple
        {
            get { return apple; }
        }

        public World(Grid world, bool isPlayer)
        {
            this.world = world;
        }

        public void Create()
        {
            snake = new SnakeHead(this);
            apple = new Apple(this);

            for (int i = 0; i < Width; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                world.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < Height; i++)
            {
                RowDefinition row = new RowDefinition();
                world.RowDefinitions.Add(row);
            }
        }

        public void Clear()
        {
            world.Children.Clear();
            world.RowDefinitions.Clear();
            world.ColumnDefinitions.Clear();

            snake = null;
            apple = null;
        }
    }
}
