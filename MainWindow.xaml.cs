using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace PR_1_krestiki_noliki
{
    public partial class MainWindow : Window
    {
        private Button[] Buttons;
        private string Player;
        private bool gameOver;
        private int movesMade;
        private Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Buttons = new Button[] { Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9 };
            Player = "X";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Content = Player;
            button.IsEnabled = false;
            movesMade++;

            if (ProverkaPobedi())
            {
                MessageBox.Show($"Победил {Player}!");
                gameOver = true;
                DisableButtons();
            }
            else if (Nichya())
            {
                MessageBox.Show("Ничья");
                gameOver = true;
                DisableButtons();
            }
            else
            {
                if (Player == "X")
                {
                    MakeAIMove();
                }
                else
                {
                    Player = "X";
                }
            }
        }

        private void MakeAIMove()
        {
            int AiMove = rnd.Next(0, 9);

            while (Buttons[AiMove].Content != "" && Buttons[AiMove].Content != null)
            {
                AiMove = rnd.Next(0, 9);
            }

            Buttons[AiMove].Content = "O";
            Buttons[AiMove].IsEnabled = false;
            movesMade++;

            if (ProverkaPobedi("O"))
            {
                MessageBox.Show("Выиграл АИ, лузер как так можно жесть, он же рандомно ходит");
                gameOver = true;
                DisableButtons();
            }
        }

        private bool ProverkaPobedi(string player = null)
        {
            player = player ?? Player;


            int[] rowIndices = { 0, 3, 6 };
            foreach (int rowIndex in rowIndices)
            {
                if (Buttons[rowIndex].Content?.ToString() == player &&
                Buttons[rowIndex + 1].Content?.ToString() == player &&
                Buttons[rowIndex + 2].Content?.ToString() == player)
                {
                    return true;
                }
            }
            int[] colIndices = { 0, 1, 2 };
            foreach (int colIndex in colIndices)
            {
                if (Buttons[colIndex].Content?.ToString() == player &&
                Buttons[colIndex + 3].Content?.ToString() == player &&
                Buttons[colIndex + 6].Content?.ToString() == player)
                {
                    return true;
                }
            }

            if (Buttons[0].Content?.ToString() == player && Buttons[4].Content?.ToString() == player && Buttons[8].Content?.ToString() == player
            Buttons[2].Content?.ToString() == player && Buttons[4].Content?.ToString() == player && Buttons[6].Content?.ToString() == player)
            {
                return true;
            }
            return false;
        }

        private bool ProverkaPobedi()
        {
            return ProverkaPobedi(Player);
        }

        private bool Nichya()
        {

            foreach (Button button in Buttons)
            {
                if (button.Content == null  button.Content == "")
                    return false;
            }

            if (ProverkaPobedi())
                return false;

            return true;
        }

        private void DisableButtons()
        {
            foreach (Button button in Buttons)
            {
                button.IsEnabled = false;
            }
        }
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in Buttons)
            {
                button.Content = null;
                button.IsEnabled = true;
            }
            gameOver = false;


        }
    }
}