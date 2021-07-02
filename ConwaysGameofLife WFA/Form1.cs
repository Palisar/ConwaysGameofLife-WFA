using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConwaysGameofLife_WFA
{
    public partial class Form1 : Form
    {
        Bitmap gameBitmap;
        Random r = new Random();
        int maxSize = 50;
        bool[,] cell;

        public Form1()
        {
            InitializeComponent();
            initCells();
        }
        void initCells()
        {
            cell = new bool[maxSize, maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                for (int j = 0; j < maxSize; j++)
                {
                    cell[i, j] = (r.Next(2) == 0);
                }
            }
            gameBitmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            timer1.Enabled = true;
        }
        
        void mainLoop()
        {
            timer1.Enabled = false;
            pictureBox2.Refresh(); 
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            mainLoop();
        }
        
        private void picPaint(object sender, PaintEventArgs e)
        {
            //this using method will allow us to fill in out bitmap. with black squares
            //    using (Graphics g = Graphics.FromImage(pictureBox2.Image))
            Graphics g = Graphics.FromImage(gameBitmap);  
            {
                int w = 4;
                    
                for (int i = 0; i < maxSize; i++)
                {
                    for (int j = 0; j < maxSize; j++)
                    {
                        if (cell[i, j]) g.FillRectangle(Brushes.Black, i * w, j * w , w , w);
                        else g.FillRectangle(Brushes.White , i * w, j * w, w, w);
                    }
                }
            }
            g.Dispose();

            pictureBox2.Image = gameBitmap;
            //this.Text = "Conways Game of Life";

            cell = new bool[maxSize, maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                for (int j = 0; j < maxSize; j++)
                {
                    cell[i, j] = (r.Next(2) == 0);
                }
            }
            timer1.Start();
        }

        
    }

}
