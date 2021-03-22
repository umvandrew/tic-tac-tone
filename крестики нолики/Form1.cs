using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace крестики_нолики
{
    public partial class Form1 : Form
    {
        //базовые постоянные, отступы, пазмер клеток
        static float xstart = 50, ystart = 50, step = 50;
        //"поля" с отметками ходов
        public int[,] littleMap = new int [3, 3];
        public static int[,] bigMap = new int[10, 10];

        public static int[,] winComb = new int[5, 5];
        //переменная определяет чей сейчас ход: true - крестики, false - нолики
        bool turn = true;
        
        public bool isplaying = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if ((e.X > xstart + i * step) && (e.X < xstart + (i + 1) * step) && (e.Y > ystart + j * step) && (e.Y < ystart + (j + 1) * step))
                    {
                        if ((bigMap[i,j]!=1)&& (bigMap[i, j] != 2))
                        {
                            if (turn)
                            {
                                bigMap[i, j] = 1;
                                turn = false;
                            }
                            else if (turn == false)
                            {
                                bigMap[i, j] = 2;
                                turn = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Клетка занята, выберите другую!");
                        }
                    }
                    if (checkWin(1))
                    {
                        MessageBox.Show("Победа 1 игрока");
                    
                    }
                    if (checkWin(2)) MessageBox.Show("Победа 2 игрока");
                    Invalidate();
                }
                
            }
        }

        public void Form1_Paint(object sender, PaintEventArgs e)
        {
                Graphics g = this.CreateGraphics();
                //рисуем поле 
                for (int i = 50; i <= 550; i+=50)
                {
                    g.DrawLine(new Pen(Color.Black, 2f), i, xstart, i, 550);
                    g.DrawLine(new Pen(Color.Black, 2f), 550, i, ystart, i);
                }

                //рисуем крестик, если клик мышкой был
                for (int i = 0; i < 10; i++)
                {
                    for ( int j = 0; j < 10; j++)
                    {
                        if (bigMap[i,j]==1)
                        {
                            g.DrawLine(new Pen(Color.Black, 2f), xstart + 5 + i * step, ystart + 5 + j * step, xstart + 5 + i * step + 40, ystart + 5 + j * step + 40);
                            g.DrawLine(new Pen(Color.Black, 2f), xstart + 5 + i * step, ystart + 5 + j * step + 40, xstart + 5 + i * step +40, ystart + 5 + j * step );
                        
                        }
                        if (bigMap[i, j] == 2)
                        {
                            g.DrawEllipse(new Pen(Color.Red, 2f), xstart + 5 + i * step, ystart + 5 + j * step, 40, 40);
                        
                        }
                    }
                }

                
        }

        public void drawMap(int[,] Map)
        {
            int size = Map.GetLength(0);
            
            Graphics g = this.CreateGraphics();
            for (int i = 50; i <= 50 + size * 50; i+=50)
            {
                g.DrawLine(new Pen(Color.Black, 2f), i, xstart, i, 550);
                g.DrawLine(new Pen(Color.Black, 2f), 550, i, ystart, i);
            }
        }
        
        // проверка победителя 
        private static bool checkWin(int c)
        {
            for (int i = 0; i < 10; i++) {  // ползём по всему полю
                for (int j = 0; j < 10; j++) {
                    if (i <= 5) 
                    {
                        if (checkLine(i, j, 1, 0, c)) return true;   // проверим линию по х 
                    }
                    if (i <= 5 && j <= 5) 
                    {
                        if (checkLine(i, j, 1, 1, c))  return true;   // проверим по диагонали
                    }
                    if (j <=5) 
                    {
                        if(checkLine(i, j, 0, 1, c)) return true;   // проверим линию по у
                    }
                    if(i <= 5 && j >= 5 ) 
                    {
                        if (checkLine(i, j, 1, -1,  c)) return true;  // проверим по диагонали х -у
                    }
                }
            }
            return false;
        }
        
        // проверка линии
        private static bool checkLine(int x, int y, int vx, int vy, int c)
        {
            int len = 5;
            for (int i = 0; i < len; i++) {              // ползём по проверяемой линии
                if (bigMap[(y + i * vy),(x + i * vx)] != c) return false;   // проверим одинаковые-ли символы в ячейках
            }
            return true;
        }
    }

    
}
