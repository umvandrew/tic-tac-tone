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
                    checkWin(turn);
                    Invalidate();
                }
                
            }
        }

        public void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            if (isplaying)
            {
                MessageBox.Show("Работает...");
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
            else
            {
                Graphics g = this.CreateGraphics();
                g.DrawLine(new Pen(Color.Black, 2f), 1, 1, 2, 2);
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
        private static bool checkWin(bool  cl)
        {
            int c;
            if (cl)
            {
                c = 1;
            }
            else if (cl == false);
            {
                c = 2;
            }
            
            for (int i = 0; i < 10; i++) {// ползём по всему полю
                for (int j = 0; j < 10; j++) {
                    if (checkLine(i, j, 1, 0, 5, c)) return true;   // проверим линию по х 
                    if (checkLine(i, j, 1, 1, 5, c)) return true;   // проверим по диагонали х у
                    if (checkLine(i, j, 0, 1, 5, c)) return true;   // проверим линию по у
                    if (checkLine(i, j, 1, -1, 5, c)) return true;  // проверим по диагонали х -у
                }
            }
            return false;
        }
        
        // проверка линии
        private static bool checkLine(int x, int y, int vx, int vy, int len, int c) {
            int far_x = x + (len - 1) * vx;           // посчитаем конец проверяемой линии
            int far_y = y + (len - 1) * vy;
            if ((far_x < (bigMap.GetLength(0) - 1)) && (far_y < (bigMap.GetLength(1) - 1))
                && (far_x >= 0) && (far_y >= 0)) // проверим не выйдет-ли проверяемая линия за пределы поля
            {
                return false;
            }
            
            for (int i = 0; i < len; i++) {                 // ползём по проверяемой линии
                if (bigMap[(x + i * vx),(y + i * vy)] != c) return false;   // проверим одинаковые-ли символы в ячейках
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            helloForm F2 = new helloForm();
            F2.Show();
        }
    }

    
}
