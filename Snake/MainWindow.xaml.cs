using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static World worldP;
        private static World worldAI;

        private bool isStarted = false;
        private bool isAlive = true;
        private bool isAIAlive = true;

        private SnakeAI snakeAI;

        private static DispatcherTimer timerP = new DispatcherTimer();
        private static DispatcherTimer timerLogic = new DispatcherTimer();
        private static DispatcherTimer timerAI = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            timerP.Tick += new EventHandler(timerP_Tick);
            timerLogic.Tick += new EventHandler(timerLogic_Tick);
            timerAI.Tick += new EventHandler(timerAI_Tick);

            RestartButton.Content = "Start!";
            worldP = new World(WorldP, true);
            worldAI = new World(WorldAI, false);

            snakeAI = new SnakeAI();
            //WinnerDisplay();
        }
        
        public static void ChangeSpeed(bool isPlayer)
        {
            if (isPlayer)
            {
                if (worldP.Speed < worldP.MaxSpeed | worldAI.Speed < worldAI.MaxSpeed)
                {
                    worldP.Speed += 0.25;
                    worldAI.Speed += 0.5;
                }
            }
            else
            {
                if (worldAI.Speed < worldAI.MaxSpeed | worldP.Speed < worldP.MaxSpeed)
                {
                    worldP.Speed += 0.5;
                    worldAI.Speed += 0.25;
                }
            }

            timerP.Interval = TimeSpan.FromMilliseconds(300 / worldP.Speed);
            timerAI.Interval = TimeSpan.FromMilliseconds(300 / worldAI.Speed);
        }

        private void timerLogic_Tick(object sender, EventArgs e)
        {
            GameControls();
            
            worldP.GetSnake.TransCoordToGrid();
            worldAI.GetSnake.TransCoordToGrid();

            worldP.GetSnake.Collide();
            worldAI.GetSnake.Collide();

            isAlive = worldP.GetSnake.IsAlive;
            isAIAlive = worldAI.GetSnake.IsAlive;

            if (!isAlive | !isAIAlive)
            {
                timerP.Stop();
                timerAI.Stop();
                timerLogic.Stop();
                WinnerDisplay();
            }

            PlayerText.Text = $"Player : {worldP.GetSnake.EatenApples}/{worldP.Speed}";
            AIText.Text = $"Computer : {worldAI.GetSnake.EatenApples}/{worldAI.Speed}";

        }

        private void timerP_Tick(object sender, EventArgs e)
        {
            worldP.GetSnake.MoveSnake();
        }

        private void timerAI_Tick(object sender, EventArgs e)
        {
            snakeAI.Control();
            worldAI.GetSnake.MoveSnake();
        }

        private void GameControls()
        {
            if ((Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > 0 |
                (Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
                worldP.GetSnake.Direction = SnakeHead.Directions.UP;

            else if ((Keyboard.GetKeyStates(Key.S) & KeyStates.Down) > 0 |
                (Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
                worldP.GetSnake.Direction = SnakeHead.Directions.DOWN;

            else if ((Keyboard.GetKeyStates(Key.A) & KeyStates.Down) > 0 |
                (Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0)
                worldP.GetSnake.Direction = SnakeHead.Directions.LEFT;

            else if ((Keyboard.GetKeyStates(Key.D) & KeyStates.Down) > 0 |
                (Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0)
                worldP.GetSnake.Direction = SnakeHead.Directions.RIGHT;
        }

        private void Start()
        {
            worldP.Clear();
            worldAI.Clear();

            worldP.Create();
            worldAI.Create();

            snakeAI.Snake = worldAI.GetSnake;
            snakeAI.Init();

            isAlive = true;
            isAIAlive = true;

            worldP.Speed = 12;
            worldAI.Speed = 12;

            worldP.GetSnake.EatenApples = 0;
            worldAI.GetSnake.EatenApples = 0;

            worldP.GetSnake.Draw();
            worldAI.GetSnake.Draw();

            if (!isStarted)
            {
                isStarted = true;

                timerP.Interval = TimeSpan.FromMilliseconds(300 / worldP.Speed);
                timerAI.Interval = TimeSpan.FromMilliseconds(300 / worldAI.Speed);

                timerLogic.Interval = TimeSpan.FromTicks(1);

                timerLogic.Start();
                timerP.Start();
                timerAI.Start();
            }
        }

        private void WinnerDisplay()
        {
            string winner = "Nothing";
            if (!worldP.GetSnake.IsAlive)
                winner = "Computer";
            else if (!worldAI.GetSnake.IsAlive)
                winner = "Player";

            MessageBoxResult msg = MessageBox.Show($"{winner} is alive!\nPlayer: {worldP.GetSnake.EatenApples}\nComputer: {worldAI.GetSnake.EatenApples}", "Game over!");
            
            if (msg == MessageBoxResult.OK)
            {
                isStarted = false;
                RestartButton.Content = "Start!";
            }
            MessageBox.Show($"Collide to Tile {worldAI.GetSnake.CollidedPart}!");
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartButton.Content = "Restart";
            Start();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Yet Another Snake Game (YASG)\n\nCreated by\nJegor I. Barbashin\nVacant Place\n\n(c) 2023", "About YASG");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
