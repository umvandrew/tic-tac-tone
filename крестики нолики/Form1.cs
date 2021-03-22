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

        public static int[,] winComb = new int[5, 2]; //начальная и конечная точка выйгрышной комбинации 

        public static int[] countWin = new int[] {0,0};
        //переменная определяет чей сейчас ход: true - крестики, false - нолики
        public bool turn = true;
        public bool isPlaying = false;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isPlaying) 
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
                        Invalidate();
                    }
                
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
                if (isPlaying)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for ( int j = 0; j < 10; j++)
                        {
                            if (bigMap[i,j]==1)
                            {
                                g.DrawLine(new Pen(Color.Black, 2f), xstart + 5 + i * step, ystart + 5 + j * step, xstart + 5 + i * step + 40, ystart + 5 + j * step + 40);
                                g.DrawLine(new Pen(Color.Black, 2f), xstart + 5 + i * step, ystart + 5 + j * step + 40, xstart + 5 + i * step +40, ystart + 5 + j * step );
                        
                            }
                            if (bigMap[i,j] == 2)
                            {
                                g.DrawEllipse(new Pen(Color.Red, 2f), xstart + 5 + i * step, ystart + 5 + j * step, 40, 40);
                        
                            }
                        }
                    }
                }
                
                //проверка на победу 
                for (int c = 1; c <= 2 ; c++)
                {
                    if (checkWin(c))
                    {
                        g.DrawLine(new Pen(Color.Orange, 5f), xstart + 25 + (winComb[0, 0] * step), ystart + 25 + (winComb[0, 1] * step), 
                            xstart + 25 + (winComb[4, 0] * step), ystart + 25 + (winComb[4, 1] * step ));

                        ClearMap();
                    
                        countWin[c-1] += 1;
                        MessageBox.Show("Победа "+c+" игрока") ;
                        if (c == 1) textBox1.Text = (countWin[0]).ToString();
                        if (c == 2) textBox2.Text = (countWin[1]).ToString();

                    }
                }

                //ничья
                if (CheckMap())
                {
                    MessageBox.Show("Нет победителя");
                    ClearMap();
                }
                
        }
        
        //проверка на пустые клетки(заполненность поля)
        private static bool CheckMap()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (bigMap[i, j] == 0) return false;
                }
            }

            return true;
        }

        //Очистить поле, подготовиться к следующей игре 
        private void ClearMap()
        {
             
            for (var i = 0; i < 10; i++) 
            {
                for (var j = 0; j < 10; j++)
                {
                    bigMap[i, j] = 0;
                }
            }
            
            isPlaying = false;
            turn = true;
            button1.Text = "Играсть снова";
            button1.BackColor = Color.Gold;
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
                
                if (bigMap[(y + i * vy), (x + i * vx)] == c)
                {
                    winComb[i, 0] = (y + i * vy);
                    winComb[i, 1] = (x + i * vx);
                }
                
                if (bigMap[(y + i * vy),(x + i * vx)] != c) return false;   // проверим одинаковые-ли символы в ячейках
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isPlaying = true;
            button1.Text = "Идет игра";
            button1.BackColor = Color.Chartreuse;
            Invalidate();
        }
    }
}
