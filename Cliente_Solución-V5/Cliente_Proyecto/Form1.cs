using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Cliente_Proyecto
{
    public partial class Form1 : Form
    {

        Socket server;
        Thread atender;
        public Form1()
        {
            InitializeComponent();
            TablaConectados.ColumnCount = 1;
            TablaConectados.Columns[0].Name = "Jugadores conectados";
            TablaConectados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.CadetBlue;
        }
        private void AtenderServidor()
        {
            while (true)
            {
                byte[] msg2 = new byte[700];
                server.Receive(msg2);
                string mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] trozos = mensaje.Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                
                
                switch (codigo)
                {

                    case 1:
                        mensaje = trozos[1];
                        MessageBox.Show(mensaje);
                        break;

                    case 2:
                        mensaje = trozos[1];
                        MessageBox.Show(mensaje);
                        break;

                    case 3:
                        mensaje = trozos[1];
                        MessageBox.Show(mensaje);
                        break;

                    case 4:
                        mensaje = trozos[1];
                        MessageBox.Show(mensaje);
                        break;

                    case 5:
                        mensaje = trozos[1];
                        MessageBox.Show(mensaje);
                        break;

                    case 6:
                        mensaje = trozos[1];
                        MessageBox.Show(mensaje);
                        break;

                    case 7:
                        DelegadoTabla delegadoTabla = new DelegadoTabla(FillData);
                        TablaConectados.Invoke(delegadoTabla, new object[] { trozos });
                           
                        break;

                    case 8:
                        Invitacion f = new Invitacion();
                        f.ShowDialog();
                        string result = f.Mensaje();
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(result);
                        server.Send(msg);
                        break;

                    case 9:
                        mensaje = trozos[1];
                        bool mostrar = true;
                        MessageBox.Show(mensaje);
                        DelegadoShow delegadoShow = new DelegadoShow(ShowTablero);
                        pictureBox4.Invoke(delegadoShow, new object[] { mostrar });
                        pictureBox3.Invoke(delegadoShow, new object[] { mostrar });
                        button2.Invoke(delegadoShow, new object[] { mostrar });
                        button1.Invoke(delegadoShow, new object[] { mostrar });
                        pictureBox2.Invoke(delegadoShow, new object[] { mostrar });

                        break;
                    case 10:
                        mensaje = trozos[1];
                        MessageBox.Show(mensaje);
                        break;

                    case 12:
                        break;

                    case 13:
                        mensaje = trozos[1];
                        MessageBox.Show(mensaje);
                        break;

                    case 14:
                        mensaje = trozos[1];
                        MessageBox.Show("Tirada:  " +  mensaje);
                        break;


                }
            }

        }
        private void ConectarB_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.103");
            IPEndPoint ipep = new IPEndPoint(direc, 9330);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);

                MessageBox.Show("Conectado");
                Conectar V = new Conectar();
                V.ShowDialog();
                string mensaje = V.Mensaje();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);


            }
            catch (SocketException ex)
            {

                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

        }

        private void EnviarB_Click(object sender, EventArgs e)
        {
            if (MostrarJR.Checked)
            {
                string mensaje = "1/";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);


            }

            else if (PganadasR.Checked)
            {
                string mensaje = ("2/" + nombreG.Text + "/");
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);


            }

            else if (ResultadosR.Checked)
            {
                string mensaje = "3/";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);


            }
        }

        private void IniciarSB_Click(object sender, EventArgs e)
        {
            Logging formv = new Logging();
            formv.ShowDialog();
            string mensaje = formv.Mensaje();

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);



        }

        private void DesconectarB_Click(object sender, EventArgs e)
        {
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            atender.Abort();
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            MessageBox.Show("Desconectado");

        }

        private void RegistrarB_Click(object sender, EventArgs e)
        {
            Registro form = new Registro();
            form.ShowDialog();
            string mensaje = form.mensaje();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);





        }



     


        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num1;
            int num2;
            Random r = new Random();
            int iRnd = new int();
            iRnd = r.Next(0, 6);

            if (iRnd == 0)
            {
                num1 = 1;
                pictureBox4.Image = pictureBox10.Image;
            }
            else if (iRnd == 1)
            {
                num1 = 2;
                pictureBox4.Image = pictureBox9.Image;
            }
            else if (iRnd == 2)
            {
                num1 = 3;
                pictureBox4.Image = pictureBox8.Image;
            }
            else if (iRnd == 3)
            {
                num1 = 4;
                pictureBox4.Image = pictureBox7.Image;
            }
            else if (iRnd == 4)
            {
                num1 = 5;
                pictureBox4.Image = pictureBox6.Image;
            }
            else
            {
                num1 = 6;
                pictureBox4.Image = pictureBox5.Image;
            }
            
            iRnd = r.Next(0, 6);

            if (iRnd == 0)
            {
                num2 = 1;
                pictureBox3.Image = pictureBox10.Image;
            }
            else if (iRnd == 1)
            {
                num2 = 2;
                pictureBox3.Image = pictureBox9.Image;
            }
            else if (iRnd == 2)
            {
                num2 = 3;
                pictureBox3.Image = pictureBox8.Image;
            }
            else if (iRnd == 3)
            {
                num2 = 4;
                pictureBox3.Image = pictureBox7.Image;
            }
            else if (iRnd == 4)
            {
                num2 = 5;
                pictureBox3.Image = pictureBox6.Image;
            }
            else
            {
                num2 = 6;
                pictureBox3.Image = pictureBox5.Image;
            }

            int result = num1 + num2;
            string mensaje = "12/" + result + "/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void FillData(string [] trozos)
        {
            TablaConectados.Rows.Clear();
            int i = 2;
           
            while (i<trozos.Length)
            {

                TablaConectados.Rows.Add(trozos[i]) ;
                i=i+2;
            }
        }
        delegate void DelegadoTabla(string[] trozos);

        private void ShowTablero(bool mostrar)
        {
            pictureBox4.Visible = mostrar;
            pictureBox3.Visible = mostrar;
            button2.Visible = mostrar;
            button1.Visible = mostrar;
            pictureBox2.Visible = mostrar;

        }
        delegate void DelegadoShow(bool mostrar);
        private void TablaConectados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = TablaConectados.Rows[index];
            string mensaje = "9/" + selectedRow.Cells[0].Value.ToString()+"/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            Eliminar V = new Eliminar();
            V.ShowDialog();
            string mensaje = V.Mensaje();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }
}

