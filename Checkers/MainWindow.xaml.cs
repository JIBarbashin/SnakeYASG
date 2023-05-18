using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board world;

        public MainWindow()
        {
            InitializeComponent();
            RestartButton.Content = "Start!";
            world = new Board(World);
        }

        private void Start()
        {
            Global.CurrentTeam = 0;

            Global.PlayerScore = 0;
            Global.AIScore = 0;

            Random random = new Random();
            Global.PlayerTeam = random.Next(2);
            string teamName;

            if (Global.PlayerTeam == 0)
            {
                Global.AITeam = 1;
                teamName = "White";
                PlayerText.Foreground = Brushes.White;
                ComputerText.Foreground = Brushes.Black;
            }
            else 
            {
                Global.AITeam = 0;
                teamName = "Black";
                PlayerText.Foreground = Brushes.Black;
                ComputerText.Foreground = Brushes.White;
            }

            MessageBox.Show($"You are {teamName}");
            CheckersAI.Init();
            world.Clear();
            world.CreateBoard();
            world.PiecePlacement();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartButton.Content = "Restart";
            Start();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Yet Another Checkers Game (YACG)\n\nCreated by\nJegor I. Barbashin\nVacant Place\n\n(c) 2023", "About YACG");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
