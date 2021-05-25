/**
 * 
 * T.P. Snake 
 * 
 * @author Santiago Romar
 * Profesora: Natalia Gonzalez
 * Curso: 6to 1ra
 * Escuela: Hogar Naval Stella Maris
 * Año: 2020
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace $safeprojectname$
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Timer tiempo1;
        int tamanoMatriz;
        int[,] matriz;
        direccionSerpiente direccion;
        Point posicionCabeza;
        int ultimoSegmento;
        Random rdm;
        int segundos;
        int puntos;
        int mejorPuntaje;
        enum ObjetoMatriz
        {
            Comida = -1,
           Trampa = -2
        }

        enum direccionSerpiente { 
        Arriba,
        Derecha,
        Abajo,
        Izquierda
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            rdm = new Random();
            tiempo1 = new Timer();

            
            tiempo1.Tick += funcionTiempo1;
            tamanoMatriz = 20;
            matriz = new int[tamanoMatriz, tamanoMatriz];

            InicializarComponentes();
            tiempo1.Interval = segundos;
            Dibujar();
        }

        private void InicializarComponentes()
        {
            for (int i = 0; i < tamanoMatriz; i++)
            {
                for (int j = 0; j < tamanoMatriz; j++)
                {
                    matriz[i, j] = 0;
                }
            }
            generarComida();
            segundos = 250;
            puntos = 0;
            lblPuntos.Text = puntos.ToString();
            posicionCabeza = new Point(5, 5);
            matriz[5, 5] = 1;
            matriz[6, 5] = 2;
            matriz[7, 5] = 3;
            ultimoSegmento = 3;
            direccion = direccionSerpiente.Izquierda;
        }

        private void funcionTiempo1(object sender, EventArgs e)
        {
            LogicaJuego();
            Dibujar();


        }

        private void Dibujar()
        {
            Bitmap mapaDeBits = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graficos = Graphics.FromImage(mapaDeBits);

            graficos.FillRectangle(Brushes.Black, 0, 0, pictureBox1.Width, pictureBox1.Height);

            SizeF tamCelda = new SizeF(((float)pictureBox1.Width / tamanoMatriz), ((float)pictureBox1.Height / tamanoMatriz));


            for (int i = 0; i < tamanoMatriz; i++) {
                for (int j = 0; j < tamanoMatriz; j++)
                {
                    if (matriz[i, j] == 0)
                    {
                        graficos.FillRectangle(Brushes.Black, i * tamCelda.Width + 1, j * tamCelda.Height + 1, tamCelda.Width - 2, tamCelda.Height - 2);

                    }
                    else if(matriz[i,j] == (int)ObjetoMatriz.Comida)
                    {
                        graficos.FillRectangle(Brushes.Red, i * tamCelda.Width + 1, j * tamCelda.Height + 1, tamCelda.Width - 2, tamCelda.Height - 2);

                    }
                    else
                    {
                        graficos.FillRectangle(Brushes.Yellow, i * tamCelda.Width + 1, j * tamCelda.Height + 1, tamCelda.Width - 2, tamCelda.Height - 2);

                    }


                }
            }



            pictureBox1.BackgroundImage = mapaDeBits;

        }

        private void LogicaJuego() {
            
            Point posicion;
            switch (direccion)
            {
                case direccionSerpiente.Arriba:
                    posicion = new Point(posicionCabeza.X, posicionCabeza.Y - 1);
                    break;
                case direccionSerpiente.Derecha:
                    posicion = new Point(posicionCabeza.X + 1, posicionCabeza.Y);
                    break;
                case direccionSerpiente.Abajo:
                    posicion = new Point(posicionCabeza.X, posicionCabeza.Y + 1);
                    break;
                case direccionSerpiente.Izquierda:
                    posicion = new Point(posicionCabeza.X - 1, posicionCabeza.Y);
                    break;
                default:
                    throw new Exception("No es posible que la serpiente no tenga una direccion.");
            }
            if (posicion.X < 0 || posicion.Y < 0 || posicion.X == tamanoMatriz || posicion.Y == tamanoMatriz || matriz[posicion.X, posicion.Y] > 0)
            {
                tiempo1.Interval = 250;
                InicializarComponentes();
                return;
            }
            if (matriz[posicion.X, posicion.Y] == (int)ObjetoMatriz.Comida)
            {
                ultimoSegmento++;
                puntos += 10;
                lblPuntos.Text = puntos.ToString();
                if(puntos > mejorPuntaje)
                {
                    mejorPuntaje = puntos;
                    label3.Text = mejorPuntaje.ToString();
                }


                if (segundos > 50)
                {
                 
                    segundos = segundos -25;
                    tiempo1.Interval = segundos;
                }
                generarComida();
            }
            matriz[posicion.X, posicion.Y] = 1;
            matriz[posicionCabeza.X, posicionCabeza.Y]++;
            for (int i = 0; i < tamanoMatriz; i++)
            {
                for (int j = 0; j < tamanoMatriz; j++)
                {
                    if(posicionCabeza.X == i && posicionCabeza.Y==j){

                    continue;

                    }
                    if(matriz[i,j] == ultimoSegmento)
                    {
                        matriz[i, j] = 0;
                    }
                    else if(matriz[i, j] > 1) 
                    {
                        matriz[i, j]++;
                    }
                    
                    }
                    

                }

            posicionCabeza = posicion;
            }

        

        
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    if(direccion != direccionSerpiente.Abajo)
                    direccion = direccionSerpiente.Arriba;
                    break;
                case 'd':
                    if (direccion != direccionSerpiente.Izquierda)
                        direccion = direccionSerpiente.Derecha;
                    break;
                case 's':
                    if (direccion != direccionSerpiente.Arriba)
                        direccion = direccionSerpiente.Abajo;
                    break;
                case 'a':
                    if (direccion != direccionSerpiente.Derecha)
                        direccion = direccionSerpiente.Izquierda;
                    break;
                case 'W':
                    if (direccion != direccionSerpiente.Abajo)
                        direccion = direccionSerpiente.Arriba;
                    break;
                case 'D':
                    if (direccion != direccionSerpiente.Izquierda)
                        direccion = direccionSerpiente.Derecha;
                    break;
                case 'S':
                    if (direccion != direccionSerpiente.Arriba)
                        direccion = direccionSerpiente.Abajo;
                    break;
                case 'A':
                    if (direccion != direccionSerpiente.Derecha)
                        direccion = direccionSerpiente.Izquierda;
                    break;

            }


        }


        private void espacio(object sender, KeyEventArgs e)
        {
         
            if (e.KeyCode == Keys.Space && tiempo1.Enabled)
            {
                label10.Visible = true;

                tiempo1.Stop();
            }
            else
            {
                label10.Visible = false;

                tiempo1.Start();
            }
        }

        private void generarComida()
        {
            Point posicionComida;

            do
            {
                posicionComida = new Point(rdm.Next() % tamanoMatriz, rdm.Next() % tamanoMatriz);
            } while (matriz[posicionComida.X, posicionComida.Y] != 0);

            matriz[posicionComida.X, posicionComida.Y] = (int)ObjetoMatriz.Comida;
        }

     
        
    }
}
