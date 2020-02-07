using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace BlocDeNotas
{
    public partial class Form1 : Form
    {
        int limite = 20;
        int x, y;
        bool rectangulo = false,circulo=false,linea=false,d=false,u=false;
        float zoom;
        PaperSize carta = new PaperSize("Letter", 850, 1170);
        PaperSize oficio = new PaperSize("Letter", 850, 1170);
        bool c = true, l = false;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = richTextBox1.ZoomFactor.ToString();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.Filter = "Archivo de Texto (*.txt)|*.txt|Todos los Archivos (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                richTextBox1.LoadFile(openFileDialog1.FileName);
                this.Text =   "JRNM - Bloc de Notas | "+openFileDialog1.FileName+openFileDialog1.FileName;

            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Archivo de Texto (*.txt)|*.txt|Todos los Archivos (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                    richTextBox1.SaveFile(saveFileDialog1.FileName);
            }
        }
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (c)
                    {
                        printDocument1.DefaultPageSettings.PaperSize = carta;
                    }
                    else
                    {
                        printDocument1.DefaultPageSettings.PaperSize = oficio;
                    }
                    printDocument1.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
                    printDocument1.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Un error ocurrió: ", ex.ToString());
                }
            }
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void fuenteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SelectionColor = colorDialog1.Color;
                }
            }
            else
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.ForeColor = colorDialog1.Color;
                }
            }
        }

        private void fondoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SelectionBackColor = colorDialog1.Color;
                }
            }
            else
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.BackColor = colorDialog1.Color;

                }
            }
        }

        private void fuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
            {
                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SelectionFont = fontDialog1.Font;
                }
            }
            else
            {
                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Font = fontDialog1.Font;
                }
            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            SolidBrush sb = new SolidBrush(richTextBox1.ForeColor);
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, sb, 100.0f, 100.0f);
        }

        private void izquierdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void derechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length >= limite) {
                MessageBox.Show("Limite");
            }
            /*if (richTextBox1.Text.Length > 105)
            {
                richTextBox1.Text += "\n";
            }
            if (int.Parse(richTextBox1.Lines.Length.ToString()) >= 20)
            {
                MessageBox.Show("20");
            }
            else {MessageBox.Show(richTextBox1.Lines.Length.ToString());}*/

        }
        private void cartaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cartaToolStripMenuItem.BackColor = Color.Cyan;
            oficioToolStripMenuItem.BackColor = SystemColors.Control;
            c = true;
            l = false;
        }

        private void oficioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cartaToolStripMenuItem.BackColor = SystemColors.Control;
            oficioToolStripMenuItem.BackColor = Color.Cyan;
            l = true;
            c = false;
        }

        private void imagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Imagen (*.jpg)|*.jpg|Imagen (*.png)|*.png|Todos los Archivos (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                Image img = Image.FromFile(openFileDialog1.FileName);
                //Bitmap img = new Bitmap(openFileDialog1.FileName);
                Bitmap bm = new Bitmap(img,100,100);
                Clipboard.SetDataObject(bm);
                DataFormats.Format mf = DataFormats.GetFormat(DataFormats.Bitmap);
                if (richTextBox1.CanPaste(mf))
                {
                    richTextBox1.Paste(mf);
                }
                else {
                    MessageBox.Show("El archivo no se ha podido cargar");
                }
            }
        }

        private void hojaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                zoom = float.Parse(textBox1.Text);
                if (zoom >= 64) { zoom = 63; }
                if (zoom <= 0.015625) { zoom = 0.015626f; }
            }
            catch { zoom = 1; }
            richTextBox1.ZoomFactor = zoom;
            richTextBox1.Size = new Size(int.Parse((850 * zoom).ToString()), int.Parse((1170 * zoom).ToString()));
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;
        }

        private void rectanguloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rectangulo = true;
            circulo = false;
            linea = false;
        }

        private void circuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rectangulo = false;
            circulo = true;
            linea = false;
        }

        private void lineaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rectangulo = false;
            circulo = false;
            linea = true;
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (u)
            {
                x = e.X;
                y = e.Y;
                d = true;
            }
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(d){
                Graphics g = richTextBox1.CreateGraphics();
            if (rectangulo) {
                Rectangle r = new Rectangle();
                g.FillRectangle(new SolidBrush(Color.Black),x,y,e.X,e.Y);
                
            }
                u = true;
        }
        }
    }
}
