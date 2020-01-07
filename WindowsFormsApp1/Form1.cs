using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
   
    public partial class Form1 : Form
    {
        // Variaveis globais do FORM1
        string correct = "";
        Random rnd = new Random();
        int certas = 0;
        int erradas = 0;
        string fotoURL;
        string ficheiro;
        string fx_perfil = "perfil.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
           // ler o ficheiro das perguntas, conforme o tema escolhido
            string[] linhas = File.ReadAllLines(ficheiro);
            // nº de linhas do ficheiro lidas para o array
            int num_linhas = linhas.Length;
            //gerar pergunta aleatoriamente
            int num = rnd.Next(1, num_linhas );
            // dividir a linha, lida do ficheiro, separando os dados pelos ;
            string [] campos = linhas[num].Split(';');

            textBox2.Text = campos[0];       // capital
            radioButton4.Text = campos[1];   // opção 1
            radioButton5.Text = campos[2];   // opção 2
            radioButton6.Text = campos[3];   // opção 3
            radioButton7.Text = campos[4];   // opção 4
            correct = campos[5];             // guarda qual a resposta correta
        }

       
        // Button responder à questão do quizz
        private void button2_Click(object sender, EventArgs e)
        {
          
            string resposta = "";

            if (radioButton4.Checked)
            {
                resposta = radioButton4.Text;
            }
            else if (radioButton5.Checked)
                 {
                    resposta = radioButton5.Text;
                 }
            else
                if (radioButton6.Checked)
                 {
                    resposta = radioButton6.Text;
                 }
            else
                if (radioButton7.Checked)
                {
                    resposta = radioButton7.Text;
                }

            if (resposta == correct)
            {
                label1.Text = "Acertou!!";
                certas++;
            }
            else
            {
                label1.Text = "Errado :( !!";
                erradas++;
            }
            atualiza();
      // atualiza nº de respostas certas, erradas, e´grid com histórico
         
        }

        void atualiza()
        {
            textBox1.Text = certas.ToString();
            textBox3.Text = erradas.ToString();

            dataGridView1.Rows.Add(correct, label1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // arranca com os botões de Nova pergunta e validar inativos.
            // só depois da escolha do tema é que ficam ativos
            button1.Enabled = false;
            button2.Enabled = false;
            // verifica que existe ficheiro com avatar
            if (File.Exists(fx_perfil))
            {
                StreamReader sr = File.OpenText(fx_perfil);
                fotoURL = File.ReadAllText(fx_perfil);
                sr.Close();
                pictureBox1.Image = new Bitmap(fotoURL);
               
            }
            else
            {
             fotoURL = @"imagens\SemPerfil.png";
             pictureBox1.Image = new Bitmap(fotoURL);
            }
          
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //  openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Filter =
        "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + "All files (*.*)|*.*";

            openFileDialog1.Title = "Selecione Imagem de perfil";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                fotoURL = openFileDialog1.SafeFileName;     // APENAS nome do ficheiro
                fotoURL = openFileDialog1.FileName;         // nome do ficheiro com PATH
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamWriter sw = File.CreateText("perfil.txt");
            string linha =  fotoURL;
            sw.WriteLine(linha);
            sw.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            st1.Text = DateTime.Now.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                 ficheiro = @"temas\Europa.txt";
            if (radioButton2.Checked)
                ficheiro = @"temas\America.txt";
            if (radioButton3.Checked)
                ficheiro = @"temas\Asia.txt";
            // desativa a escolha de tema
            button5.Enabled = false;
            // ativar os botões de nova questão e de validar
            button1.Enabled = true;
            button2.Enabled = true;
        }
    }
}
