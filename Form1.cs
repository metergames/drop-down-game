using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drop_Down
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        int gravity = 8;
        int score = 0;
        int speed = 12;
        bool moveLeft;
        bool moveRight;
        bool wait = true;
        bool changing_Colors = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void playtimer(object sender, EventArgs e)
        {
            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            Color randomColor = Color.FromKnownColor(randomColorName);

            if (wait)
            {
                System.Threading.Thread.Sleep(1000);
                wait = false;
            }

            player.Top += gravity;
            label1.Text = "Score: " + score;

            if (moveLeft && player.Left >= speed - 2)
            {
                player.Left -= speed;
            }
            if (moveRight && player.Left + player.Width <= panel1.Width - speed)
            {
                player.Left += speed;
            }

            foreach (Control x in panel1.Controls)
            {
                if (x is PictureBox && x.Tag == "platform")
                {
                    x.Top -= 5;

                    if (x.Top < panel1.Top - x.Height)
                    {
                        x.Top = panel1.Height + x.Height;

                        x.Width = rnd.Next(100, 400);

                        if (changing_Colors)
                        {
                            pictureBox1.BackColor = randomColor;
                            pictureBox2.BackColor = randomColor;
                            pictureBox3.BackColor = randomColor;
                            pictureBox4.BackColor = randomColor;
                        }
                        else
                        {
                            pictureBox1.BackColor = Color.Red;
                            pictureBox2.BackColor = Color.Lime;
                            pictureBox3.BackColor = Color.Aqua;
                            pictureBox4.BackColor = Color.Fuchsia;
                        }

                        score++;
                    }

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        gravity = 0;
                        player.Top = x.Top - player.Height;
                    }
                    else
                    {
                        gravity = 8;
                    }
                }
            }

            if (player.Top + player.Height < 0 || player.Top > panel1.Height)
            {
                gameTimer.Stop();
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }

            if (e.KeyCode == Keys.C)
            {
                if (changing_Colors)
                {
                    changing_Colors = false;
                }
                else
                {
                    changing_Colors = true;
                }
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }
        }
    }
}
