using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Snake
{
    internal class WorldDrawer
    {
        public static Ellipse HeadImage = new Ellipse();

        public static void DrawSnake(ref World world, ref SnakeHead snake)
        {
            if (world.WorldGrid.Children.Contains(HeadImage))
                world.WorldGrid.Children.Remove(HeadImage);

            world.WorldGrid.Children.Add(HeadImage);

            //DrawParts(ref world);
        }

        public static void DrawParts(ref World world, ref SnakeHead snake)
        {
            foreach (SnakePart part in snake.SnakeParts)
                part.Draw(world.WorldGrid);
        }

        public static void DrawApple(ref World world, ref Apple apple)
        {

        }
    }
}
