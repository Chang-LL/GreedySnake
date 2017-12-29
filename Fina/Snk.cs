using System;
using System.Collections.Generic;
using System.Drawing;

namespace GreedySnake
{
    //关卡 难度 刷新 分数
    public enum Scores
    {
        easy=10,normal=20,difficult=30
    }
    public enum Dict
    {
        Up, Right, Left, Down, mei
    }
    public class Snk
    {
        const int Width = 10;

        const int N = 40;
        const int Blank = 0;
        const int Wall = 1;
        const int Fruit = 3;
        const int Body = 2;
        private Point head;
        private Point tail;
        private Point fru;
        private int[,] snak = new int[N, N];
        private Dict dict;
        private List<Point> sbody;
        private List<Point> wall;
        public Snk()
        {
            dict = Dict.Up;
            
            Head = new Point(8, 6);
            Tail = new Point(8, 7);
            wall = new List<Point>();
            FruitCount = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Snak[i, j] = Blank;
                }
            }
            Fru = new Point(2, 3);
            for (int i = 0; i < N; i++)
            {
                Snak[0, i] = Snak[N - 1, i]
                     = Snak[i, 0] = Snak[i, N - 1] = Wall;
                wall.Add(new Point(0, i));
                wall.Add(new Point(N - 1, i));
                wall.Add(new Point(i, 0));
                wall.Add(new Point(i, N - 1));
            }
            Snak[Head.X, Head.Y] = Body;
            Snak[Tail.X, Tail.Y] = Body;
            sbody = new List<Point>
            {
                Tail,
                Head
            };
        }
        public int FruitCount
        {
            get;
            set;
        }
        public void Setfruit()
        {

            Random r = new Random();
            while (true)
            {

                int x = r.Next(0, N);
                int y = r.Next(0, N);
                if (snak[x, y] == Blank)
                {
                    Fru = new Point(x, y);
                    break;
                }
            }
            FruitCount++;
        }
        public bool Move(Dict di)
        {
            if (di == Dict.mei) di = dict;
            Point p = head;
            switch (dict)
            {
                case Dict.Up:
                    switch (di)
                    {
                        case Dict.Up:
                            p = Up();
                            break;
                        case Dict.Right:
                            p = Right();
                            break;
                        case Dict.Left:
                            p = Left();
                            break;
                        case Dict.Down:
                            //p = Down();
                            break;
                        default:
                            break;
                    }
                    break;
                case Dict.Right:
                    switch (di)
                    {
                        case Dict.Up:
                            p = Up();
                            break;
                        case Dict.Right:
                            p = Right();
                            break;
                        case Dict.Left:
                            //p = Left();
                            break;
                        case Dict.Down:
                            p = Down();
                            break;
                        default:
                            break;
                    }
                    break;
                case Dict.Left:
                    switch (di)
                    {
                        case Dict.Up:
                            p = Up();
                            break;
                        case Dict.Right:
                            //p = Right();
                            break;
                        case Dict.Left:
                            p = Left();
                            break;
                        case Dict.Down:
                            p = Down();
                            break;
                        default:
                            break;
                    }
                    break;
                case Dict.Down:
                    switch (di)
                    {
                        case Dict.Up:
                            //p = Up();
                            break;
                        case Dict.Right:
                            p = Right();
                            break;
                        case Dict.Left:
                            p = Left();
                            break;
                        case Dict.Down:
                            p = Down();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            try
            {
                if (p != head && Over(p))
                {
                    return false;
                }
                else if (p == head) return true;
                else
                {
                    dict = di;
                    Head = p;
                    sbody.Add(Head);
                    if (p != fru)
                        sbody.Remove(tail);
                    else Setfruit();
                    Tail = sbody[0];
                }
            }
            catch { }
            return true;
        }
        public int[,] Snak
        {
            get => snak;
            set => snak = value;
        }
        public Point Head
        {
            get => head;
            set
            {
                head = value;
                snak[head.X, head.Y] = Body;
            }
        }
        public Point Tail
        {
            get => tail;
            set
            {
                snak[tail.X, tail.Y] = Blank;
                tail = value;
            }
        }

        public Point Fru
        {
            get => fru;
            set
            {
                Snak[value.X, value.Y] = Fruit;
                fru = value;
            }
        }

        private bool Over(Point p)//结束返回true
        {
            if (sbody.Contains(p) || wall.Contains(p))
                return true;
            return false;
        }
        private Point Up()
        {

            Point p = new Point
            (
                Head.X, Head.Y - 1
            );
            return p;

        }
        private Point Down()
        {
            Point p = new Point
            (
                Head.X, Head.Y + 1
            );
            return p;
        }
        private Point Right()
        {
            Point p = new Point
            (
                Head.X + 1, Head.Y
            );
            return p;
        }
        private Point Left()
        {
            Point p = new Point
            (
                Head.X - 1, Head.Y
            );
            return p;
        }
        public void Draw(Graphics g)
        {
            Pen[] pen = new Pen[]
            {
             new Pen(Color.AliceBlue),
             new Pen(Color.Black),
             new Pen(Color.Red),
             new Pen(Color.Yellow)
            };

            SolidBrush[] brush = new SolidBrush[]
            {

             new SolidBrush(Color.AliceBlue),
             new SolidBrush(Color.Black),
             new SolidBrush(Color.Red),
             new SolidBrush(Color.Yellow)
            };
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    Rectangle rect = new Rectangle(i * Width,
                        j * Width, Width, Width);
                    g.DrawRectangle(pen[Snak[i, j]], rect);
                    g.FillRectangle(brush[Snak[i, j]], rect);
                }
        }
    }
}
