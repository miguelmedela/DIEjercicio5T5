using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIEjercicio5T5
{
    public enum eTipo
    {
        Numerico,
        Textual
    }

    public partial class ValidateTextBox : UserControl
    {
        public ValidateTextBox()
        {
            InitializeComponent();
            this.Location = new Point(10, 10);
            CambioSize();
        }

        private void CambioSize()
        {
            this.Height =textBox1.Height + 20;
            textBox1.Width = this.Width - 20;
        }

        private bool flag;
        private Color color = Color.Red;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(color);
            
            int offSetX = 4; //Aqui puse 4 porque si no el recuadro se sale del componente y no se dibuja
            int offSetY = 4;
            g.DrawRectangle(p, new Rectangle(offSetX, offSetY, Width - 5, Height - 5));

            //Lo que no entiendo es si lo pinto con Fill porque sale perfectamente dibujado
            //SolidBrush b = new SolidBrush(color);
            //g.FillRectangle(b, new Rectangle(5, 5, Width - 5, Height - 5));
            //b.Dispose();

        }

        private eTipo tipo = eTipo.Numerico;

        [Category("AAppearance")]
        [Description("Forma asociada al componente")]
        public eTipo Tipo
        {
            set
            {
                tipo = value;
            }
            get
            {
                return tipo;
            }
        }


        [Category("AAppearance")]
        [Description("Texto asociado al textBox del componente")]
        public string Texto
        {
            set
            {
                textBox1.Text = value;
                CambioSize();
            }
            get
            {
                return textBox1.Text;
            }
        }


        [Category("AAppearance")]
        [Description("Controla si el textBox abarca una linea o varias")]
        public bool Multilinea
        {
            set
            {
                textBox1.Multiline = value;
                CambioSize();
            }
            get
            {
                return textBox1.Multiline;
            }
        }


        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Refresh();
            
        }

        [Category("event")]
        [Description("Se lanza al cambiar el texto")]
        public event EventHandler CambiaTexto;


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CambiaTexto?.Invoke(this, EventArgs.Empty);
            if (tipo == eTipo.Numerico)
            {
                try
                {
                    //Convierto a int, si es letra salta format, si salta el overflow sigue siendo numerico

                    Convert.ToInt32(textBox1.Text.Trim());
                    if (int.TryParse(textBox1.Text.Trim(), out int i))
                    {
                        flag = true;
                    }
                }
                catch(OverflowException)
                {
                    flag = true;
                }
                catch (FormatException)
                {
                    flag = false;
                }
                
            }
            else
            {
                try
                {
                    Convert.ToInt32(textBox1.Text.Trim());
                    if (!Int32.TryParse(textBox1.Text.Trim(), out int i))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                catch (OverflowException)
                {
                    flag = false;
                }
                catch (FormatException)
                {

                }
            }
            switch (flag)
            {
                case true:
                    color = Color.Green;
                    break;

                case false:
                    color = Color.Red;
                    break;
            }
            this.Refresh();
        }

        private void ValidateTextBox_Resize(object sender, EventArgs e)
        {
            CambioSize();
        }
    }
}
