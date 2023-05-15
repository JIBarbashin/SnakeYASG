﻿using System;
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

            isAlive = worldP.GetSnake.IsAlive;
            isAIAlive = worldAI.GetSnake.IsAlive;

            PlayerText.Text = $"Player : {worldP.GetSnake.EatenApples}/{worldP.Speed}";
            AIText.Text = $"Computer : {worldAI.GetSnake.EatenApples}/{worldAI.Speed}";
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            timerGame.Interval = TimeSpan.FromMilliseconds(400 / worldP.Speed);

            worldP.GetSnake.MoveSnake();
            worldP.GetSnake.TransCoordToGrid();

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
            timerAI.Interval = TimeSpan.FromMilliseconds(400 / worldAI.Speed);

            worldAI.GetSnake.MoveSnake();
            worldAI.GetSnake.TransCoordToGrid();

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

            worldP.Speed = 1;
            worldAI.Speed = 1;

            worldP.GetSnake.EatenApples = 0;
            worldAI.GetSnake.EatenApples = 0;

            worldP.GetSnake.Draw();
            worldAI.GetSnake.Draw();

            if (!isStarted)
            {
                isStarted = true;

                timerGame.Interval = TimeSpan.FromTicks(1);
                timerAI.Interval = TimeSpan.FromTicks(1);
                timerControl.Interval = TimeSpan.FromTicks(1);
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

            MessageBoxResult msg = MessageBox.Show($"{winner} is alive!\nPlayer: {worldP.GetSnake.EatenApples}\nComputer: {worldAI.GetSnake.EatenApples}", "Game over!");
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
