using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente_Proyecto
{
    public partial class Eliminar : Form
    {
        string usuario;
        string contra;
        public Eliminar()
        {
            InitializeComponent();
        }

        private void Enviar_Click(object sender, EventArgs e)
        {
            usuario = Usuario.Text;
            contra = Contraseña.Text;
            this.Close();
        }

        private void Eliminar_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.IndianRed;
        }
        public string Mensaje()

        {
            string mensaje = "11/" + usuario + "/" + contra;
            return mensaje;

        }
    }
}
