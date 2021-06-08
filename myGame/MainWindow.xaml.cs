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
using System.Threading;
using System.Windows.Threading;


/// <summary>
/// PONG GAME
/// AUTHOR: HAREL ISASCHAR
/// </summary>
namespace myGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer dt;

        //Enums for the directon of the ball, and for the side. 
        public enum direction { straigt, upS, downS,upL,downL }
        public enum left_right { left, right }

        bool gameOver = false;
        int winner;

        //initalize direction and side(default straight, left)
        direction dir = direction.straigt;
        left_right lr = left_right.left;

        int score1 = 0, score2 = 0;



        public MainWindow()
        {
            InitializeComponent();
            dt = new DispatcherTimer();
            dt.Tick += work;
            dt.Interval = TimeSpan.FromMilliseconds(0.5);
            gameOverLabel.Visibility = Visibility.Hidden;
            winLabel.Visibility = Visibility.Hidden;
            
        }


        /// <summary>
        /// The main method that checks the position 
        /// of the ball at any given time,
        /// and moves it according to the parameters.
        /// rec1- is the left rectangle
        /// rec2- is the right rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void work(object sender, EventArgs e)
        {

            //moving rec2 in one player mode
            if (mode.SelectedIndex == 0)
            {
                if (Canvas.GetTop(rec2) <= 200)
                {
                    if (Canvas.GetTop(ball) > Canvas.GetTop(rec2))
                        Canvas.SetTop(rec2, Canvas.GetTop(rec2) + 1);
                }
                if (Canvas.GetTop(rec2) >= 10)
                {
                    if (Canvas.GetTop(ball) < Canvas.GetTop(rec2))
                        Canvas.SetTop(rec2, Canvas.GetTop(rec2) - 1);
                }
            }



            //checking if the ball touch rec1
            if (Canvas.GetLeft(ball) == 25 && Canvas.GetTop(ball) + ball.Height >= Canvas.GetTop(rec1) && Canvas.GetTop(ball) <= Canvas.GetTop(rec1) + rec1.Height)
            {

                if (Canvas.GetTop(ball) + (ball.Height / 2) >= Canvas.GetTop(rec1) + 25 && Canvas.GetTop(ball) + (ball.Height / 2) <= Canvas.GetTop(rec1) + 35) // straight
                    dir = direction.straigt;

                if (Canvas.GetTop(ball) + (ball.Height / 2) >= Canvas.GetTop(rec1) && Canvas.GetTop(ball) + (ball.Height / 2) < Canvas.GetTop(rec1) + 10) //Strong up
                    dir = direction.upS;

                if (Canvas.GetTop(ball) + (ball.Height / 2) > Canvas.GetTop(rec1) + 50 && Canvas.GetTop(ball) + (ball.Height / 2) <= Canvas.GetTop(rec1) + 60)// Strong down
                    dir = direction.downS;

                if (Canvas.GetTop(ball) + (ball.Height / 2) > Canvas.GetTop(rec1) + 10 && Canvas.GetTop(ball) + (ball.Height / 2) < Canvas.GetTop(rec1) + 25) // Low up
                    dir = direction.upL;

                if (Canvas.GetTop(ball) + (ball.Height / 2) > Canvas.GetTop(rec1) + 35 && Canvas.GetTop(ball) + (ball.Height / 2) < Canvas.GetTop(rec1) + 50)// Low down
                    dir = direction.downL;

                lr = left_right.right;
            }

            //checking if the ball touch rec2
            if (Canvas.GetLeft(ball)  == 505 && Canvas.GetTop(ball) + ball.Height >= Canvas.GetTop(rec2) && Canvas.GetTop(ball) <= Canvas.GetTop(rec2) + rec1.Height)
            {

                if (Canvas.GetTop(ball) + (ball.Height / 2) >= Canvas.GetTop(rec2) + 25 && Canvas.GetTop(ball) + (ball.Height / 2) <= Canvas.GetTop(rec2) + 35)
                    dir = direction.straigt;

                if (Canvas.GetTop(ball) + (ball.Height / 2) >= Canvas.GetTop(rec2) && Canvas.GetTop(ball) + (ball.Height / 2) < Canvas.GetTop(rec2) + 10)
                    dir = direction.upS;

                if (Canvas.GetTop(ball) + (ball.Height / 2) > Canvas.GetTop(rec2) + 50 && Canvas.GetTop(ball) + (ball.Height / 2) <= Canvas.GetTop(rec2) + 60)
                    dir = direction.downS;

                if (Canvas.GetTop(ball) + (ball.Height / 2) > Canvas.GetTop(rec2) + 10 && Canvas.GetTop(ball) + (ball.Height / 2) < Canvas.GetTop(rec2) + 25)
                    dir = direction.upL;

                if (Canvas.GetTop(ball) + (ball.Height / 2) > Canvas.GetTop(rec2) + 35 && Canvas.GetTop(ball) + (ball.Height / 2) < Canvas.GetTop(rec2) + 50)
                    dir = direction.downL;

                lr = left_right.left; // Swap sides after touching a rectangle
            }



            //**********************************************************************************
            //If the ball touches the upper or lower limit
            if (Canvas.GetTop(ball) <= 10)
            {
                if (dir == direction.upS)
                    dir = direction.downS;
                else
                    dir = direction.downL;
            }

            if (Canvas.GetTop(ball) >= 244)
            {
                if (dir == direction.downS)
                    dir = direction.upS;
                else
                    dir = direction.upL;
            }

            //**********************************************************************************
            //Move the ball according to the direction and angle.
            if (dir == direction.straigt)
            {
                if (lr == left_right.left)
                    Canvas.SetLeft(ball, Canvas.GetLeft(ball) - 2);
                else
                    Canvas.SetLeft(ball, Canvas.GetLeft(ball) + 2);
            }

            if (dir == direction.upS)
            {
                if (lr == left_right.left)
                {
                    Canvas.SetLeft(ball, Canvas.GetLeft(ball) - 2);
                    Canvas.SetTop(ball, Canvas.GetTop(ball) - 2);
                }
                else
                {
                    Canvas.SetLeft(ball, Canvas.GetLeft(ball) + 2);
                    Canvas.SetTop(ball, Canvas.GetTop(ball) - 2);
                }
            }

            if (dir == direction.downS)
            {
                if (lr == left_right.left)
                {
                    Canvas.SetLeft(ball, Canvas.GetLeft(ball) - 2);
                    Canvas.SetTop(ball, Canvas.GetTop(ball) + 2);
                }
                else
                {
                    Canvas.SetLeft(ball, Canvas.GetLeft(ball) + 2);
                    Canvas.SetTop(ball, Canvas.GetTop(ball) + 2);
                }
            }

            if (dir == direction.downL)
            {
                if (lr == left_right.left)
                {
                    Canvas.SetLeft(ball, Canvas.GetLeft(ball) - 2);
                    Canvas.SetTop(ball, Canvas.GetTop(ball) + 1);
                }
                else
                {
                    Canvas.SetLeft(ball, Canvas.GetLeft(ball) + 2);
                    Canvas.SetTop(ball, Canvas.GetTop(ball) + 1);
                }
            }

            if (dir == direction.upL)
            {
                if (lr == left_right.left)
                {
                    Canvas.SetLeft(ball, Canvas.GetLeft(ball) - 2);
                    Canvas.SetTop(ball, Canvas.GetTop(ball) -1);
                }
                else
                {
                    Canvas.SetLeft(ball, Canvas.GetLeft(ball) + 2);
                    Canvas.SetTop(ball, Canvas.GetTop(ball) -1);
                }
            }

            //**********************************************************************************
            //checking if ball out and adding scores or each of scores is 10 and game over
            if (Canvas.GetLeft(ball) ==5)
            {
                ++score2;
                ScoreLabel.Content = " " + score1.ToString() + " -" + " " + score2.ToString();
                if (score2 == 5)
                {
                    gameOver = true;
                    winner = 1;
                    gameOverEvent();
                }
                Canvas.SetTop(ball,160);
                Canvas.SetLeft(ball, 25);
                Canvas.SetTop(rec1, 130);
                lr = left_right.right;
                dir = direction.straigt;
               
            }
            if (Canvas.GetLeft(ball)==527)
            {
                ++score1;
                ScoreLabel.Content = " " + score1.ToString() + " -" + " " + score2.ToString();
                if (score1 == 5)
                {
                    gameOver = true;
                    winner = 0;
                    gameOverEvent();
                }
                Canvas.SetTop(ball, 160);
                Canvas.SetLeft(ball, 505);
                Canvas.SetTop(rec2, 130);
                lr = left_right.left;
                dir = direction.straigt;
            }

            
        }

        //********************************************************************
        //********************************************************************

        /// <summary>
        /// End of the game and display a message: "Game over".
        /// </summary>
        public void gameOverEvent()
        {
            dt.Stop();
            ball.Visibility = Visibility.Hidden;
            gameOverLabel.Visibility = Visibility.Visible;
            if (winner == 1)
                winLabel.Content += "RED";
            else
                winLabel.Content += "BLUE";
            winLabel.Visibility = Visibility.Visible;
            start.IsEnabled = false;

        }

        //********************************************************************
        //********************************************************************

        /// <summary>
        /// An event when you press a button that moves the rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyIsDown(object sender, KeyEventArgs e)
        {
            
                if (e.Key == Key.W) //up for rec1
                {
                    if (Canvas.GetTop(rec1) > 10)
                        Canvas.SetTop(rec1, Canvas.GetTop(rec1) - 10);
                }

                if (e.Key == Key.S)// down for rec1
                {
                    if (Canvas.GetTop(rec1) < 200)
                        Canvas.SetTop(rec1, Canvas.GetTop(rec1) + 10);
                }

            if (mode.SelectedIndex == 1)
            {
                if (e.Key == Key.Up)//up for rec2
                {
                    if (Canvas.GetTop(rec2) > 10)
                        Canvas.SetTop(rec2, Canvas.GetTop(rec2) - 10);
                }

                if (e.Key == Key.Down)//down for rec2
                {
                    if (Canvas.GetTop(rec2) < 200)
                        Canvas.SetTop(rec2, Canvas.GetTop(rec2) + 10);
                }
            }
            

        }

        //********************************************************************
        //********************************************************************

        /// <summary>
        /// An event when you press a button to start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (mode.SelectedIndex==0|| mode.SelectedIndex == 1)
            {
                start.IsEnabled = false;
                dt.Start();
            }
        }
    }
}
