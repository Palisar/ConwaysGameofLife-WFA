using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConwaysGameofLife_WFA
{
    public enum Status
    {
        Alive,
        Dead,
    }
    public partial class Form1 : Form
    {
        Bitmap gameBitmap;
        Random r = new Random();
        const int maxSize = 50;
        Status[,] cellGrid;
        Status[,] nextGen;
        int index = 0;
        int speed = 30;
        
        public Form1()
        {
            InitializeComponent();
            initCells();
            
        }
        public static Status[,] NextGeneration(Status[,] cellGrid)
        {
            var nextGen = new Status[maxSize, maxSize];
            for (int row = 1; row < maxSize -1 ; row++)
            {
                for (int col = 1; col < maxSize -1; col++)
                {
                    int neighbours = 0;
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            neighbours += cellGrid[row + i, col + j] == Status.Alive ? 1 : 0;
                        }
                        
                    }
                    var currentCell = cellGrid[row, col];
                    neighbours -= currentCell == Status.Alive ? 1 : 0;

                    //check for under population
                    if (currentCell == Status.Alive && neighbours < 2)
                    {
                        nextGen[row,col] = Status.Dead;
                    }
                    //check for over population
                    else if (currentCell == Status.Alive && neighbours > 3)
                    {
                        nextGen[row, col] = Status.Dead;
                    }
                    //check to be born
                    else if(currentCell == Status.Dead && neighbours == 3)
                    {
                        nextGen[row, col] = Status.Alive;
                    }
                    else
                    {
                        nextGen[row, col] = currentCell; 
                    }
                }
            }
            return nextGen;
        }
        void initCells()
        {
            cellGrid = new Status[maxSize, maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                for (int j = 0; j < maxSize; j++)
                {
                    cellGrid[i, j] =  (Status)(r.Next(2));
                }
            }
            gameBitmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            nextGen = NextGeneration(cellGrid);
        }
        
        private void picPaint(object sender, PaintEventArgs e)
        {
            //this using method will allow us to fill in out bitmap. with black squares
            //    using (Graphics g = Graphics.FromImage(pictureBox2.Image))
            Graphics g = Graphics.FromImage(gameBitmap);  
            {
                int w = 6;
                if (index == 1)
                {
                    for (int i = 0; i < maxSize; i++)
                    {
                        for (int j = 0; j < maxSize; j++)
                        {
                            if (nextGen[i, j] == Status.Alive) g.FillRectangle(Brushes.Black, i * w, j * w, w, w);
                            else g.FillRectangle(Brushes.White, i * w, j * w, w, w);
                            
                        }
                    }
                    index = 0;
                    cellGrid = NextGeneration(nextGen);
                    Debug.WriteLine("nextGen population was used");
                }
                else
                {
                    for (int i = 0; i < maxSize; i++)
                    {
                        for (int j = 0; j < maxSize; j++)
                        {
                            if (cellGrid[i, j] == Status.Alive) g.FillRectangle(Brushes.Black, i * w, j * w, w, w);
                            else g.FillRectangle(Brushes.White, i * w, j * w, w, w);
                            
                        }
                    }
                    index++;
                     nextGen = NextGeneration(cellGrid);
                    Debug.WriteLine("cellGrid population was used");
                }
               Thread.Sleep(speed);
            }
            g.Dispose();

            pictureBox2.Image = gameBitmap;
            this.Text = "Conways Game of Life";

        }



        private void button1_Click(object sender, EventArgs e)
        {
            initCells();    
        }

        private void button2_Click(object sender, EventArgs e)
        {
             speed += 10;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (speed > 10)
            {
                speed -= 10;
            }
           
        }
    }

}
