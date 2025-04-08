using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SnakeYASG
{
    public interface ICommand
    {
        void Execute();
        //void Undo();
    }

    public class MoveCommand : ICommand
    {
        SnakeHead snake;
        SnakeHead.Directions dir;

        public MoveCommand(SnakeHead snake, SnakeHead.Directions dir)
        {
            this.snake = snake;
            this.dir = dir;
        }

        public void Execute()
        {
            snake.Direction = dir;
        }
    }
}
