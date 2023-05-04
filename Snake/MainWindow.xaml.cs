using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Snake.SnakeHead;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private World worldP;
        private World worldAI;

        private bool isStarted = false;
        private bool isAlive = true;
        private bool isAIAlive = true;

        private SnakeAI snakeAI;

        private DispatcherTimer timerGame = new DispatcherTimer();
        private DispatcherTimer timerControl = new DispatcherTimer();
        private DispatcherTimer timerAI = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            timerGame.Tick += new EventHandler(timerGame_Tick);
            timerControl.Tick += new EventHandler(timerControl_Tick);
            timerAI.Tick += new EventHandler(timerAI_Tick);

            RestartButton.Content = "Start!";
            worldP = new World(WorldP, true);
            worldAI = new World(WorldAI, false);

            snakeAI = new SnakeAI();
            //WinnerDisplay();
        }

        private void timerControl_Tick(object sender, EventArgs e)
        {
            GameControls();
            snakeAI.Control();

            worldP.GetSnake.Collide();
            worldAI.GetSnake.Collide();

            PlayerText.Text = $"Player : {worldP.GetSnake.EatenApples}";
            AIText.Text = $"Computer : {worldAI.GetSnake.EatenApples}";
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            worldP.GetSnake.MoveSnake();
            worldP.GetSnake.Draw();

            isAlive = worldP.GetSnake.IsAlive;

            if (!isAlive)
            {
                timerGame.Stop();
                timerAI.Stop();
                timerControl.Stop();
                WinnerDisplay();
            }
        }

        private void timerAI_Tick(object sender, EventArgs e)
        {
            worldAI.GetSnake.MoveSnake();
            worldAI.GetSnake.Draw();

            isAIAlive = worldAI.GetSnake.IsAlive;

            if (!isAIAlive)
            {
                timerGame.Stop();
                timerAI.Stop();
                timerControl.Stop();
                WinnerDisplay();
            }
        }

        private void GameControls()
        {
            if ((Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > 0 | (Keyboard.GetKeyStates(Key.Up)    & KeyStates.Down) > 0)
                worldP.GetSnake.Direction = SnakeHead.Directions.UP;
            if ((Keyboard.GetKeyStates(Key.S) & KeyStates.Down) > 0 | (Keyboard.GetKeyStates(Key.Down)  & KeyStates.Down) > 0)
                worldP.GetSnake.Direction = SnakeHead.Directions.DOWN;
            if ((Keyboard.GetKeyStates(Key.A) & KeyStates.Down) > 0 | (Keyboard.GetKeyStates(Key.Left)  & KeyStates.Down) > 0)
                worldP.GetSnake.Direction = SnakeHead.Directions.LEFT;
            if ((Keyboard.GetKeyStates(Key.D) & KeyStates.Down) > 0 | (Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0)
                worldP.GetSnake.Direction = SnakeHead.Directions.RIGHT;
        }

        private void Start()
        {
            worldP.Clear();
            worldAI.Clear();

            worldP.Create();
            worldAI.Create();

            snakeAI.Snake = worldAI.GetSnake;

            worldP.GetSnake.EatenApples = 0;
            worldAI.GetSnake.EatenApples = 0;

            if (!isStarted)
            {
                isStarted = true;

                timerGame.Interval = TimeSpan.FromMilliseconds(500 / worldP.Speed);
                timerAI.Interval = TimeSpan.FromMilliseconds(500 / worldAI.Speed);
                timerControl.Interval = TimeSpan.FromMilliseconds(10);
                timerGame.Start();
                timerAI.Start();
                timerControl.Start();
            }
        }

        private void WinnerDisplay()
        {
            string winner = "Nothing";
            if (!worldP.GetSnake.IsAlive)
                winner = "Computer";
            else if (!worldAI.GetSnake.IsAlive)
                winner = "Player";

            MessageBoxResult msg = MessageBox.Show($"{winner} wins!\nPlayer: {worldP.GetSnake.EatenApples}\nComputer: {worldAI.GetSnake.EatenApples}", "Game over!");
            if (msg == MessageBoxResult.OK)
            {
                isStarted = false;
                RestartButton.Content = "Start!";
            }
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
            System.Windows.Application.Current.Shutdown();
        }
    }
}
