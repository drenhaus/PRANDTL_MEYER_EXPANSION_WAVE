using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibreriaClases;
using System.Data;

namespace WPFapp
{
    /// <summary>
    /// Lógica de interacción para AdvancedStudyWindow.xaml
    /// </summary>
    public partial class AdvancedStudyWindow : Window
    {
        
        public AdvancedStudyWindow()
        {
            InitializeComponent();

        }
        private void CheckBox_A_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void LoadParametersButton_Click(object sender, RoutedEventArgs e)
        {
            // m2 sera la primera malla del caso 1 (caso 1 el de dos pendientes de angulo distinto juntas)
            double sumaTheta = (10 * (Math.PI) / 180);


            Malla m2 = new Malla(); // Parimos de cero, es decir, nueva malla y reacemos el calculo de una matriz en resolucion media
            //la idea es hacer otra matriz con los datos recogidos de la ultima columna de la simulacion.


            m2.rows = 41;
            m2.columns = 72;
            m2.delta_y_t = 0.025;

            m2.norma.L = 55;
            m2.norma.E = 10;
            m2.norma.H = 40;

            m2.norma.Theta = sumaTheta / 3;

            m2.norma.v_in = 0;
            m2.norma.Rho_in = 1.23;
            m2.norma.P_in = 101000;
            m2.norma.M_in = 2;
            m2.norma.T_in = 286.1;

            m2.norma.Compute_a();
            m2.norma.Compute_M_angle();
            m2.norma.Compute_u();


            m2.DefinirMatriz();
            m2.Compute();

            List<Celda> ListadeUltimaColumnadeCeldas_caso1 = GetLastColumOfMatriz(m2);

            //Empezamos con la segunda malla que empieza con los ultimos datos d ela anterior matriz. 

            Malla m3 = new Malla();

            m3.norma.L = 45;
            m3.norma.E = 1;
            m3.norma.H = 40;

            m2.norma.Theta = ((2 * sumaTheta) / 3);

            m3.rows = 41;
            m3.columns = 61;
            m3.delta_y_t = 0.025;


            m3.DefinirMatriz();
            m3.Compute2(ListadeUltimaColumnadeCeldas_caso1);


            //CASO 2!!

            // Caso dos, tenemos dos pendientes eparadas por superficie plana


            //10 m llegamos a E, 30 M llegamos a final, 30 metros de estaviliadad, y repetimops pero con
            // angulo dos


            Malla m4 = new Malla();

            m4.rows = 41;
            m4.columns = 53;
            m4.delta_y_t = 0.025;

            m4.norma.L = 40;
            m4.norma.E = 10;
            m4.norma.H = 40;

            m4.norma.Theta = sumaTheta / 3;

            m4.norma.v_in = 0;
            m4.norma.Rho_in = 1.23;
            m4.norma.P_in = 101000;
            m4.norma.M_in = 2;
            m4.norma.T_in = 286.1;

            m4.norma.Compute_a();
            m4.norma.Compute_M_angle();
            m4.norma.Compute_u();


            m4.DefinirMatriz();
            m4.Compute();

            List<Celda> ListadeUltimaColumnadeCeldas_caso2 = GetLastColumOfMatriz(m4);

            Malla m5 = new Malla();


            m5.norma.L = 60;
            m5.norma.E = 30;
            m5.norma.H = 40;

            m5.norma.Theta = ((2 * sumaTheta) / 3);

            m5.rows = 41;
            m5.columns = 91;
            m5.delta_y_t = 0.025;


            m5.DefinirMatriz();
            m5.Compute2(ListadeUltimaColumnadeCeldas_caso2);








        }

        public List<Celda> GetLastColumOfMatriz(Malla Nmalla)
        {
            List<Celda> lista = new List<Celda>();

            int col = Nmalla.columns;
            int row = Nmalla.rows;

            for (int nrow = 0; nrow < row; nrow++)
            {
                lista.Add(Nmalla.matriz[nrow, col - 1]);
            }

            return lista;

        }
        private void Simulate_Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }

   
}

