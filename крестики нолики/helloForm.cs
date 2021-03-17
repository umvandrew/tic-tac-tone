using System;
using System.Drawing;
using System.Windows.Forms;

namespace крестики_нолики
{
    public partial class helloForm : Form
    {
        public Form1 F1 = new Form1();
        //<--Объявляем форму два как элемент класса формы один

        public helloForm()
        {
            InitializeComponent();
        }

        public helloForm(Form1 f)
        {
            InitializeComponent(); 
        }


        private void button1_Click(object sender, EventArgs e)
        {
            F1.isplaying = true;
            F1.drawMap(F1.littleMap);
            
            // Graphics g = CreateGraphics();
            // Rectangle rec=new Rectangle();
            // PaintEventArgs es = new PaintEventArgs(g, rec);
            // F1.Form1_Paint(sender, es);
            this.Close();
            
        }
    }
}