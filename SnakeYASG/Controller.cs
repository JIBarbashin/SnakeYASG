using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeYASG
{
    public interface IController
    {
        public void SetCommand(ICommand command);
        public void ExecuteCommand();
        public void Update();
    }

    public class Controller : IController
    {
        private ICommand? _command;
        private Stack<ICommand> _history = new Stack<ICommand>();

        protected SnakeHead snake;
        protected ICommand moveLeft, moveRight, moveUp, moveDown;

        public Controller(SnakeHead snake)
        {
            this.snake = snake;

            moveLeft = new MoveCommand(this.snake, SnakeHead.Directions.LEFT);
            moveRight = new MoveCommand(this.snake, SnakeHead.Directions.RIGHT);
            moveUp = new MoveCommand(this.snake, SnakeHead.Directions.UP);
            moveDown = new MoveCommand(this.snake, SnakeHead.Directions.DOWN);
        }

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void ExecuteCommand()
        {
            if (_command != null)
            {
                _command.Execute();
                _history.Push(_command);
                _command = null;
            }
        }

        virtual public void Update()
        {

        }
    }
}
