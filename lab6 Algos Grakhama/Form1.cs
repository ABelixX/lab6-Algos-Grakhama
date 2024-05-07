using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace lab6_Algos_Grakhama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class Point1: IComparable<Point1> 
        {
            public int X; public int Y;
            public double PolarAngle = 0;
            public Point1()
            {
            }

            public Point1(int x, int y)
            {
                this.X = x; this.Y = y;
            }

            public int CompareTo(Point1? other)
            {
                if (other != null)
                {
                    if (this.PolarAngle < other.PolarAngle)
                        return 1;
                    else if (this.PolarAngle > other.PolarAngle)
                        return -1;
                    else
                        return 0;
                }
                else
                    throw new Exception("������, ����� ��� Point1");
            }

            public Point1 Copy()
            {
                Point1 copy = new Point1(X, Y);
                return copy;
            }
            
        }
        public static int k = 20;
        public static int width = 601;
        public static int height = 601;
        static public List<Point1> listOfPoints = new List<Point1>()
        {
           new Point1(1,-2),
           new Point1(-3,6),
           new Point1(0,-5),
           new Point1(-4,-1),
           new Point1(5,-5),
           new Point1(2,2),
           new Point1(0,4),
           new Point1(4,-1),
           new Point1(6,0),
           new Point1(4,5)
        };
        static public List<Point1> listOfPoints2 = new List<Point1>();
        public static Point1 p;
        public static int countOfTouching = 0;
        void draw_dude()
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Pen pen = new Pen(Color.Black, 1);
            //��������� ���������
            for (int i = 0; i < width; i += 20)
            {
                g.DrawLine(pen, new Point(i, 0), new Point(i, height));
                g.DrawLine(pen, new Point(0, i), new Point(width, i));
            }
            g.DrawLine(new Pen(Color.Black, 2f), new Point(width / 2, 0), new Point(width / 2, height));
            g.DrawLine(new Pen(Color.Black, 2f), new Point(0, height / 2), new Point(width, height / 2));
            //������� �� -15;15
            //������ �������
            //g.DrawLine(new Pen(Color.Red, 2f), new Point(NormalX(x1), NormalY(y1)), new Point(NormalX(x2), NormalY(y2)));
            //������ �������������
            for(int i =0;i<listOfPoints.Count;i++)
            {
                g.DrawEllipse(new Pen(Color.Red, 2f), NormalX(listOfPoints[i].X), NormalY(listOfPoints[i].Y), 2, 2);
            }
            int indexOfMinXY = getMinXY();
            p = listOfPoints[indexOfMinXY].Copy();
            g.DrawEllipse(new Pen(Color.Blue, 2f), NormalX(listOfPoints[indexOfMinXY].X), NormalY(listOfPoints[indexOfMinXY].Y), 2, 2);//��������� �����

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (countOfTouching == 0)
            {
                draw_dude();
                Cdvig();
                getPolarAngle();
                listOfPoints.Sort();
                listOfPoints2.Add(new Point1(0, 0));
                for (int i = 0; i < listOfPoints.Count; ++i)
                {
                    if (!(listOfPoints[i].X == 0 && listOfPoints[i].Y == 0))
                        listOfPoints2.Add(listOfPoints[i]);
                }
              
                for(int i =0;i<listOfPoints2.Count;++i)
                {
                    listOfPoints2[i].X += p.X;
                    listOfPoints2[i].Y += p.Y;
                }
                ++countOfTouching;
            }
            else if (countOfTouching == 1)
            {
                Stack<Point1> points = new Stack<Point1>();
                points.Push(listOfPoints2[0]); points.Push(listOfPoints2[1]); points.Push(listOfPoints2[2]);
                for(int i = 3;i<listOfPoints2.Count; ++i)
                {
                    bool stop = false;
                    while(!stop)
                    {
                        Point1 p = points.Pop();
                        Point1 p1 = points.Peek();
                        if (right(p1, p, listOfPoints2[i]))
                        {
                            stop = true; 
                            points.Push(p); 
                            points.Push(listOfPoints2[i]);
                        }
                    }
                     
                }
                draw_dude();
                

            }
        

        }

        bool right(Point1 p1, Point1 p2, Point1 p3)
        {
            return !(((p2.X - p1.X) * (p3.Y - p2.Y) - (p2.Y - p1.Y) * (p3.X - p2.X)) >= 0);
        }
        int getMinXY()
        {
            Point1 minXY = new Point1();
            int index=0;
            minXY = listOfPoints[0];
            for(int i =1; i < listOfPoints.Count;++i)
            {
                if (minXY.X > listOfPoints[i].X)
                {   
                    minXY = listOfPoints[i];
                    index = i;
                }
                else if (minXY.X == listOfPoints[i].X)
                {
                    if (minXY.Y > listOfPoints[i].Y)
                    {
                        minXY = listOfPoints[i];
                        index = i;
                    }
                }

            }
            
            return index;
        }

        void Cdvig()
        {
            for(int i = 0;i<listOfPoints.Count;++i)
            {
                listOfPoints[i].X -= p.X;
                listOfPoints[i].Y -= p.Y;
            }

        }
        void getPolarAngle()
        {
            for(int i =0;i<listOfPoints.Count;++i)
            {
                listOfPoints[i].PolarAngle= Math.Atan2((double)listOfPoints[i].Y, (double)listOfPoints[i].X);
                
            }
        }
        
        void sortByAngle(int index)
        {
            //Collection.Sort(listOfPoint);
        }
        int NormalX(int x)
        {
            return x * k + width / 2;
        }
        int NormalY(int y)
        {
            return -(y * k) + height / 2;
        }
    }
}