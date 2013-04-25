using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 *  NOMBRE:
 *  APELLIDOS: 
 *  ESTO ES UNA PRUEBA DE GITHUB
 * 
 */

namespace Buscaminas
{
    public partial class Form1 : Form
    {
        //declaro el array de botones
        Button[,] matrizBotones, matrizBotones2;
        int filas = 15;
        int columnas = 15;
        int anchoBoton = 20;

        int minas = 20;

        Random aleatorio = new Random();
  

        public Form1()
        {
            InitializeComponent();        

            this.Height = columnas * anchoBoton + 40;
            this.Width = filas * anchoBoton + 20;

            matrizBotones = new Button[filas, columnas];
            matrizBotones2 = new Button[filas, columnas];

            for (int i = 0; i < filas; i++)
                for (int j = 0; j < columnas; j++)
                {
                    Button boton = new Button();
                    Button boton2 = new Button();

                    boton2.Enabled = false;

                    boton.Size = new Size(anchoBoton, anchoBoton);
                    boton2.Size = new Size(anchoBoton, anchoBoton);
                    
                    /*
                    int numeroAleatorio = aleatorio.Next(5);

                    if (numeroAleatorio == 1)
                    {
                        boton2.Text = "X";
                    }
                    else {
                        boton2.Text = "";
                    }
                     */ 

                    boton.Location = new Point(i * anchoBoton, j * anchoBoton);
                    boton2.Location = new Point(i * anchoBoton, j * anchoBoton);

                    boton.Click += chequeaBoton;
                    boton.Tag = "1";
                    
                    matrizBotones[i, j] = boton;
                    matrizBotones2[i, j] = boton2;

                    panel1.Controls.Add(boton);
                    panel1.Controls.Add(boton2);
                }
            poneMinas();
        }

        private void poneMinas() 
        {
            int numeroAleatorio = aleatorio.Next();
            int x, y = 0;
            /*
             * Si el Tag es 1 es que hay bomba.
             * Si el Tag es 2 es que NO hay bomba.
             */ 
            for (int i = 0; i < minas; i++) 
            {
                x = aleatorio.Next(filas);
                y = aleatorio.Next(columnas);
                while (!matrizBotones[x, y].Tag.Equals("1")) 
                {
                    x = aleatorio.Next(filas);
                    y = aleatorio.Next(columnas);
                }
                matrizBotones[x, y].Tag = "2";
                matrizBotones[x, y].Text = "X";
            }
        }

        private void chequeaBoton(object sender, EventArgs e)
        {
            //(sender as Button).Enabled = false;
            //(sender as Button).Visible = false;
            
            // Casteo hecho en C#.
            Button b = (sender as Button);

            // Creamos estas variables para la posición.
            int columna = b.Location.X / anchoBoton;
            int fila    = b.Location.Y / anchoBoton;


            // Este for nos permite comprobar si hay "algo" alrededor de la celda pulsada.
            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    if ((columna + j < columnas) && (columna + j >= 0)) {
                        if ((fila + i < filas) && (fila + i >= 0)) {
                            /*
                             * Vamos a hacer un bucle recurrente, aunque no sea la manera más optima.
                             * Para poder salir, creamos el siguiente if que nos permitira salir.
                             */ 
                            if (matrizBotones[columna + j, fila + i].Visible != false)
                            {
                               // if (!matrizBotones2[columna + j, fila + i].Text.Contains("X")) 
                               // {
                                    //matrizBotones[columna + i, fila + j].BackColor = Color.Coral;
                                    matrizBotones[columna + j, fila + i].Visible = false;
                                    // Vuelve a llamar a este método.
                                    chequeaBoton(matrizBotones[columna + j, fila + i], e);
                                //}
                               
                            }   
                        }
                    }
                    /*
                    //matrizBotones[columna + i, fila + j].BackColor = Color.Coral;
                    
                     */
                }
            } 
        }
    }
}
