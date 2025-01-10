using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Snake
{
    public struct Stats
    {
        public bool IsAlive { get; set; }
        public int Score { get; set; }
        public float Speed { get; set; }
        public float MinSpeed { get; set; }
        public float MaxSpeed { get; set; }

        public Stats() 
        {
            IsAlive = true;
            Score = 0;
            Speed = 1f;
            MinSpeed = 0.5f;
            MaxSpeed = 20f;
        }

        public void Reset()
        {
            IsAlive = true;
            Score = 0;
            Speed = 1;
        }
    }

    internal class Game
    {
        public World PlayerWorld { get; private set; }
        public World AIWorld { get; private set; }

        private Stats playerStats, aiStats;

        private bool isStarted = false;
        private bool isAlive = true;
        private bool isAIAlive = true;

        private SnakeAI snakeAI;

        private DispatcherTimer timerP = new DispatcherTimer();
        private DispatcherTimer timerLogic = new DispatcherTimer();
        private DispatcherTimer timerAI = new DispatcherTimer();

        private static Game instance;
        public static Game Instance 
        { 
            get 
            {
                instance ??= new Game();
                return instance;
            }
        }

        public Game()
        {
            timerP.Tick += new EventHandler(timerP_Tick);
            timerLogic.Tick += new EventHandler(timerLogic_Tick);
            timerAI.Tick += new EventHandler(timerAI_Tick);

            PlayerWorld = new World(true);
            AIWorld = new World(false);

            snakeAI = new SnakeAI();
        }

        public void ChangeSpeed(bool isPlayer)
        {
            if (isPlayer)
            {
                if (playerStats.Speed < PlayerWorld.MaxSpeed | aiStats.Speed < AIWorld.MaxSpeed)
                {
                    playerStats.Speed += 0.25f;
                    aiStats.Speed += 0.5f;
                }
            }
            else
            {
                if (aiStats.Speed < AIWorld.MaxSpeed | playerStats.Speed < playerStats.MaxSpeed)
                {
                    playerStats.Speed += 0.5f;
                    aiStats.Speed += 0.25f;
                }
            }

            timerP.Interval = TimeSpan.FromMilliseconds(60 / playerStats.Speed);
            timerAI.Interval = TimeSpan.FromMilliseconds(60 / aiStats.Speed);
        }

        private void timerLogic_Tick(object sender, EventArgs e)
        {
            GameControls();

            PlayerWorld.GetSnake.TransCoordToGrid();
            AIWorld.GetSnake.TransCoordToGrid();

            PlayerWorld.CollideSnake();
            AIWorld.CollideSnake();

            isAlive = PlayerWorld.GetSnake.IsAlive;
            isAIAlive = AIWorld.GetSnake.IsAlive;

            if (!isAlive | !isAIAlive)
            {
                timerP.Stop();
                timerAI.Stop();
                timerLogic.Stop();
                //WinnerDisplay();
            }

            //PlayerText.Text = $"Player : {playerStats.Score}/{playerStats.Speed}";
            //AIText.Text = $"Computer : {aiStats.Score}/{aiStats.Speed}";

        }

        private void timerP_Tick(object sender, EventArgs e)
        {
            PlayerWorld.GetSnake.Move();
        }

        private void timerAI_Tick(object sender, EventArgs e)
        {
            snakeAI.Control();
            AIWorld.GetSnake.Move();
        }

        private void GameControls()
        {
            if ((Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > 0 |
                (Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
                PlayerWorld.GetSnake.Direction = SnakeHead.Directions.UP;

            else if ((Keyboard.GetKeyStates(Key.S) & KeyStates.Down) > 0 |
                (Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
                PlayerWorld.GetSnake.Direction = SnakeHead.Directions.DOWN;

            else if ((Keyboard.GetKeyStates(Key.A) & KeyStates.Down) > 0 |
                (Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0)
                PlayerWorld.GetSnake.Direction = SnakeHead.Directions.LEFT;

            else if ((Keyboard.GetKeyStates(Key.D) & KeyStates.Down) > 0 |
                (Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0)
                PlayerWorld.GetSnake.Direction = SnakeHead.Directions.RIGHT;
        }

        public void Start()
        {
            PlayerWorld.Clear();
            AIWorld.Clear();

            PlayerWorld.Create();
            AIWorld.Create();

            snakeAI.Snake = AIWorld.GetSnake;
            snakeAI.Init();

            isAlive = true;
            isAIAlive = true;

           // PlayerWorld.Speed = 1;
            //AIWorld.Speed = 1;

            //PlayerWorld.GetSnake.EatenApples = 0;
            //AIWorld.GetSnake.EatenApples = 0;

            //PlayerWorld.GetSnake.Draw();
            //AIWorld.GetSnake.Draw();

            if (!isStarted)
            {
                isStarted = true;

                //timerP.Interval = TimeSpan.FromMilliseconds(60 / PlayerWorld.Speed);
               // timerAI.Interval = TimeSpan.FromMilliseconds(60 / AIWorld.Speed);

                timerLogic.Interval = TimeSpan.FromTicks(1);

                timerLogic.Start();
                timerP.Start();
                timerAI.Start();
            }
        }

        public void Stop()
        {
            isStarted = false;
        }
    }
}
