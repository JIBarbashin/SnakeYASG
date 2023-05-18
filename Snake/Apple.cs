﻿using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake
{
    internal class Apple
    {
        public int X { get; set; }
        public int Y { get; set; }
        private Ellipse appleImage = new Ellipse();
        private int size = 10;
        public int Score { get; set; }
        private Random random = new Random();

        private World world;

        public Ellipse AppleImage
        {
            get { return appleImage; }
        }

        public Apple(World world)
        {
            this.world = world;
            //AppleImage.Source = new BitmapImage(new Uri("apple.png", UriKind.Relative));
            appleImage.Fill = Brushes.Red;
            appleImage.Width = size;
            appleImage.Height = size;
            Draw();
            Init();
        }

        public void Init()
        {
            bool spawned = false;

            while (!spawned)
            {
                X = 1 + random.Next(world.Width - 1);
                Y = 1 + random.Next(world.Height - 1);

                if (!world.GetSnake.IsColliding(X, Y))
                    spawned = true;
                else
                    spawned = false;
            }
            Score = 1;
            SetCoord();
        }

        private void SetCoord()
        {
            Grid.SetColumn(appleImage, X);
            Grid.SetRow(appleImage, Y);
        }

        public void Draw()
        {
            if (!world.GetWorld.Children.Contains(appleImage))
                world.GetWorld.Children.Add(appleImage);
        }
    }
}
