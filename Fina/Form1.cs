using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreedySnake
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }
        private Snk snake;
        private Dict direction;
        private bool Game = true;
        public Dict Direction
        {
            get => direction;
            set
            {
                try
                {
                    if (snake != null)
                        Game = snake.Move(value);
                }
                catch { }
                direction = value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            snake = new Snk();
            Direction = Dict.Up;        
            timer1.Enabled = true;
            
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W:
                case Keys.Up:
                    Direction = Dict.Up;
                    break;
                case Keys.A:
                case Keys.Left:
                    Direction = Dict.Left;
                    break;
                case Keys.S:
                case Keys.Down:
                    Direction = Dict.Down;
                    break;
                case Keys.D:
                case Keys.Right:
                    Direction = Dict.Right;
                    break;
                default:
                    break;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (Game == true)
            {
               
                try
                {
                    Graphics g = SkBox.CreateGraphics();
                    snake.Draw(g);
                }
                catch { }
                Game=snake.Move(Dict.mei);
                timer1.Enabled = true;
            }
            else
            {
                var t=MessageBox.Show("是否重新开始",
                    "GameOver",MessageBoxButtons.OKCancel);
                if (t == DialogResult.Cancel) this.Close();
                else if (t == DialogResult.OK)
                {
                    snake = new Snk();
                    Direction = Dict.Up;
                    timer1.Enabled = true;
                }
            }
        }

    }
}
