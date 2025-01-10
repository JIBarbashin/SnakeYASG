using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RestartButton.Content = "Start!";
            Game.Instance.PlayerWorld.WorldGrid = WorldP;
            Game.Instance.AIWorld.WorldGrid = WorldAI;
        }

        public void StatsText(Stats playerStats, Stats AIStats)
        {
            PlayerText.Text = $"Player : {playerStats.Score}/{playerStats.Speed}";
            AIText.Text = $"Computer : {AIStats.Score} / {AIStats.Speed}";
        }

        private void WinnerDisplay(Stats playerStats, Stats AIStats)
        {
            string winner = "Nothing";
            if (!playerStats.IsAlive)
                winner = "Computer";
            else if (!AIStats.IsAlive)
                winner = "Player";

            MessageBoxResult msg = MessageBox.Show($"{winner} is alive!\nPlayer: {playerStats.Score}\nComputer: {AIStats.Score}", "Game over!");

            if (msg == MessageBoxResult.OK)
            {
                Game.Instance.Stop();
                RestartButton.Content = "Start!";
            }
            //MessageBox.Show($"Collide to Tile {worldAI.GetSnake.CollidedPart}!");
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartButton.Content = "Restart";
            Game.Instance.Start();
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
