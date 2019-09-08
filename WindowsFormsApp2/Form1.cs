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
        List<MouseEventArgs> Coordinates;
        bool flag;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Coordinates = new List<MouseEventArgs>();
            flag = false;
        }

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) //если лкм
            {
                if (!flag)
                {
                    bmp.SetPixel(e.X, e.Y, Color.Black); //делаем пиксель черным
                    Coordinates.Add(e);//добавляем в листок с точками
                }
            }
            else if (e.Button == MouseButtons.Right)//если пкм
            {
                var g = Graphics.FromImage(pictureBox1.Image);
                if (!flag)
                {
                    for (int i = 0; i < Coordinates.Count - 1; i++) //проходим по листку 
                    {
                        g.DrawLine(Pens.Red, new Point(Coordinates[i].X, Coordinates[i].Y), new Point(Coordinates[i + 1].X, Coordinates[i + 1].Y));
                    }
                    g.DrawLine(Pens.Red, new Point(Coordinates[Coordinates.Count - 1].X, Coordinates[Coordinates.Count - 1].Y)
                                       , new Point(Coordinates[0].X, Coordinates[0].Y));
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
