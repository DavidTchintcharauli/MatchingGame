using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {

        Label firstClicked = null;
        Label secondClicked = null;
        private DateTime startTime;
        private bool isTimerRunning = false;

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

            if (!isTimerRunning)
            {
                StartTime();
            }

            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();

                clickedLabel.ForeColor = Color.Black;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {   
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;


        }

        private void CheckForWinner()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLable = control as Label;
                
                if (iconLable != null)
                {
                    if (iconLable.ForeColor == iconLable.BackColor)
                        return;
                }
            }

            StopTimer();
            MessageBox.Show($"გილოცავთ! თქვენ დაჯგუფეთ ყველა აიქონი.\nრისთვისაც დაგირდათ: {GetElapsedTime()}", "გილოცავთ");
            Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
        }

        private void StartTime()
        {
            startTime = DateTime.Now;
            isTimerRunning = true;
            timer2.Stop();
        }

        private void StopTimer()
        {
            isTimerRunning = false;
        }

        private string GetElapsedTime()
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            return $"{elapsedTime.Minutes} წუთი და {elapsedTime.Seconds} წამი.";
        }
    }
}
