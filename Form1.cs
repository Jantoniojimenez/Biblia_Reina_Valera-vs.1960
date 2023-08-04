using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblia_Reina_valera_Vs._1960.Clases_Extraer_Datos;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Biblia_Reina_valera_Vs._1960
{
    public partial class Form1 : Form
    {
       bool status = false;
        bool status3 = true;
        bool status2 = false;
        string nextLibro;

        public Form1()
        {
            InitializeComponent();

            var lista = ExtraerLibrosCapitulosVersiculos.ExtraerContenido();
            this.CBoxLibro.DataSource = lista.ToArray();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listBox1.Visible = false;
            numericUpDown1.Value = (int)RichTxt.Font.Size;
            radioButton1.Checked = true;

        }

        private void CBoxLibro_SelectedIndexChanged(object sender, EventArgs e)
        {
            var libro = this.CBoxLibro.Text;
            nextLibro = CBoxLibro.Text != "APOCALIPSIS" ? CBoxLibro.Items[CBoxLibro.Items.IndexOf(CBoxLibro.Text) + 1].ToString() : nextLibro = null;

            this.CBoxCapitulo.DataSource = ExtraerLibrosCapitulosVersiculos.TotalCapitulos(libro);
            ActualizarTabcontrol();


        }

        private void CBoxCapitulo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            if (!status2) CBoxVersiculo.DataSource = ExtraerLibrosCapitulosVersiculos.Totalversiculos(CBoxLibro.Text, CBoxCapitulo.Text, nextLibro);

            label1.Text =  "Libros " + CBoxLibro.Items.Count;
            label2.Text =  "Capítulos " + CBoxCapitulo.Items.Count;
            label3.Text =  "Versiculos " + CBoxVersiculo.Items.Count.ToString();
            listBox1.DataSource = CBoxVersiculo.Items;
          
        }

        private void CBoxVersiculo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (status2) return;

            RichTxt.Lines = BuscarCitas.BuscarVersiculo(CBoxLibro.Text, Convert.ToInt32(CBoxCapitulo.Text), CBoxVersiculo.Text.Split('\n').ToArray(), nextLibro);

            if (status3)
            {
               ActualizarTabcontrol();
                status3 = false;
            }
                ActualizarTabActiva();

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                   
                if (ModifierKeys != Keys.Control)
                {
                    if (!status2)
                    {
                        RichTxt.Lines = BuscarCitas.BuscarVersiculo(CBoxLibro.Text, Convert.ToInt32(CBoxCapitulo.Text), listBox1.SelectedItems.Cast<string>().ToArray(), nextLibro);
                        checkBox1.Checked = false;

                         ActualizarTabActiva();
                    }

                }

            
        }
        private void ActualizarTabcontrol()
        {

            status = true;
            //tabCtr.SuspendLayout();

            tabCtr.TabPages.Clear();

                foreach (var captlo in this.CBoxCapitulo.Items)
                {
                    var pagina = new TabPage("Capítulo " + captlo.ToString());
                    tabCtr.TabPages.Add(pagina);
                }
            
                ActualizarTabActiva();
             
            //tabCtr.ResumeLayout();
            status = false;

        }


        private void CBoxVersiculo_DropDown(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void ListBox1_KeyUp(object sender, KeyEventArgs e)
        {

            nextLibro = CBoxLibro.Text != "APOCALIPSIS" ? CBoxLibro.Items[CBoxLibro.Items.IndexOf(CBoxLibro.Text) + 1].ToString() : nextLibro = null;

            RichTxt.Lines = BuscarCitas.BuscarVersiculo(CBoxLibro.Text, Convert.ToInt32(CBoxCapitulo.Text), listBox1.SelectedItems.Cast<string>().ToArray(), nextLibro);
            checkBox1.Checked = false;      

         }


        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

                if (checkBox1.Checked)
                    listBox1.Visible = true;
                else
                    listBox1.Visible = false;        

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

                if (RichTxt.Text != null)
                {

                    RichTxt.Font = new Font(RichTxt.Font.FontFamily, Convert.ToInt32(numericUpDown1.Value));

                    RichTxt_TextChanged(RichTxt, EventArgs.Empty);

                    ActualizarTabActiva();
                }
            
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {

                RichTxt.BackColor = Color.White;
                RichTxt.ForeColor = Color.Black;
                ActualizarTabActiva();

            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                RichTxt.BackColor = Color.Black;
                RichTxt.ForeColor = Color.White;
                ActualizarTabActiva();
            }
        }

        private void TabCtr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!status)
            {
                status2 = true;

                var numCpitulos = Convert.ToInt32(tabCtr.SelectedTab.Text.Substring(8));

                CBoxVersiculo.DataSource = ExtraerLibrosCapitulosVersiculos.Totalversiculos(CBoxLibro.Text, numCpitulos.ToString(), nextLibro);
                CBoxCapitulo.Text = numCpitulos.ToString();
                RichTxt.Lines = BuscarCitas.BuscarVersiculo(CBoxLibro.Text, numCpitulos, CBoxVersiculo.Text.Split('\n').ToArray(), nextLibro);

                ActualizarTabActiva();

                status2 = false;
            }

        }

        private void ActualizarTabActiva()
        {

            tabCtr.SelectTab(Convert.ToInt32(CBoxCapitulo.Text) - 1);

            var micontrol = RichTxt;
            micontrol.Dock = DockStyle.Fill;

            tabCtr.SelectedTab.Controls.Add(micontrol);
        }

        private void RichTxt_TextChanged(object sender, EventArgs e)
        {
    
            //        RichTxt.Select(RichTxt.GetFirstCharIndexFromLine(I), 2);

            foreach (string linea in RichTxt.Lines)
            {
                if (linea.Length > 0)
                {
                    if (char.IsNumber(linea[0]))
                    {

                        int startIndex = RichTxt.Text.IndexOf(linea);
                        RichTxt.Select(startIndex, linea.IndexOf(" "));

                        RichTxt.SelectionFont = new Font(RichTxt.SelectionFont, FontStyle.Bold);

                    }
                }

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            foreach(Control control in panel1.Controls)
            {
                if (control.Visible)
                    control.Visible = false;
                else
                    control.Visible = true;
            }

            panel1.BackgroundImageLayout = panel1.BackgroundImageLayout != ImageLayout.Stretch ? ImageLayout.Stretch: ImageLayout.Zoom;
            pictureBox1.Visible = true;

        }
    }
}
