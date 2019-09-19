using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        List<Point> Coordinates;
        bool flag;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Coordinates = new List<Point>();
            flag = false;
        }

        private static void PutPixel(Graphics g, Color col, int x, int y, int alpha)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, col)), x, y, 1, 1);
        }

        static public void Bresenham4Line(Graphics g, Color clr, int x0, int y0,
                                                                 int x1, int y1)
        {
            //Изменения координат
            int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
            int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
            //Направление приращения
            int sx = (x1 >= x0) ? (1) : (-1);
            int sy = (y1 >= y0) ? (1) : (-1);

            if (dy < dx)
            {
                int d = (dy << 1) - dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                PutPixel(g, clr, x0, y0, 255);
                int x = x0 + sx;
                int y = y0;
                for (int i = 1; i <= dx; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 255);
                    x += sx;
                }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;
                PutPixel(g, clr, x0, y0, 255);
                int x = x0;
                int y = y0 + sy;
                for (int i = 1; i <= dy; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 255);
                    y += sy;
                }
            }
        }

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            var p = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left) //если лкм
            {
                if (!flag)
                {
                    bmp.SetPixel(e.X, e.Y, Color.Black); //делаем пиксель черным
                    Coordinates.Add(p);//добавляем в листок с точками
                }
            }
            else if (e.Button == MouseButtons.Right)//если пкм
            {
                var g = Graphics.FromImage(pictureBox1.Image);
                if (!flag)
                {
                    for (int i = 0; i < Coordinates.Count - 1; i++) //проходим по листку 
                    {
                        Bresenham4Line(g, Color.Red, Coordinates[i].X, Coordinates[i].Y,
                                                                 Coordinates[i + 1].X, Coordinates[i + 1].Y);// ( Pens.Red, new Point(Coordinates[i].X, Coordinates[i].Y), new Point(Coordinates[i + 1].X, Coordinates[i + 1].Y));
                }
                    //g.DrawLine(Pens.Red, new Point(Coordinates[Coordinates.Count - 1].X, Coordinates[Coordinates.Count - 1].Y)
                    //                   , new Point(Coordinates[0].X, Coordinates[0].Y));
                    Bresenham4Line(g, Color.Red, Coordinates[Coordinates.Count - 1].X, Coordinates[Coordinates.Count - 1].Y,
                                                                 Coordinates[0].X, Coordinates[0].Y);
                    flag = true;
                }
                else
                {
                    g.Clear(Color.White);
                    Coordinates.Clear();
                    flag = false;
                }
            }
            pictureBox1.Image = bmp; //отображаем
        }
    }
}
