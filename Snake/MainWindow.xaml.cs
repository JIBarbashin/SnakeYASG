using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private Thread thread1;
        private Thread thread2;

        private SnakeAI snakeAI;

        public MainWindow()
        {
            InitializeComponent();

            thread1 = new Thread(NextThread1) { IsBackground = true };
            thread2 = new Thread(NextThread2) { IsBackground = true };

            RestartButton.Content = "Start!";
            worldP = new World(WorldP, true);
            worldAI = new World(WorldAI, false);
            snakeAI = new SnakeAI();
            //WinnerDisplay();
        }
        public void NextThread1()
        {
            int i = 0;
            while (isAlive)
            {
                Dispatcher.Invoke(() => { GameLoop();});
                Thread.Sleep(1000 / worldP.Speed);
                i++;
            }
            WinnerDisplay();
        }

        public void NextThread2()
        {
            int i = 0;
            while (isAlive)
            {
                Dispatcher.Invoke(() => { Controls(); UILoop(); });
                Thread.Sleep(10);
                i++;
            }
        }

        private void GameLoop()
        {
            worldP.GetSnake.MoveSnake();
            worldAI.GetSnake.MoveSnake();
            isAlive = worldP.GetSnake.IsAlive;
        }

        private void UILoop()
        {
            PlayerText.Text = $"Player : {worldP.GetSnake.EatenApples}";
            AIText.Text = $"Computer : {worldAI.GetSnake.EatenApples}";
        }

        private void Controls()
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

            worldP.GetSnake.EatenApples = 0;
            worldAI.GetSnake.EatenApples = 0;

            if (!isStarted)
            {
                isStarted = true;
                thread1.Start();
                thread2.Start();
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
