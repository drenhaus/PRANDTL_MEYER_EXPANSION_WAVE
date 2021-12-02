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
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Windows.Threading;
using LibreriaClases;


namespace WPFapp
{
    /// <summary>
    /// Lógica de interacción para Graphics.xaml
    /// </summary>
    public partial class Graphics : Window
    {
        Generadora generador;

        int numDeCOLUMNAS;


        public List<double> listaTEMPxColumna { get; set; }
        public List<double> listaMachxColumna { get; set; }
        public List<double> listaDensidadxColumna { get; set; }
        public List<double> listaPresurexColumna { get; set; }
        public List<double> listaUxColumna { get; set; }
        public List<double> listaVxColumna { get; set; }




        public Graphics()
        {
            InitializeComponent();

            Graf_Temperatura_Butt.Click += Graf_Temperatura_Butt_Click;
            Graf_Mach_Butt.Click += Graf_Mach_Butt_Click;
            Graf_Densidad_Butt.Click += Graf_Densidad_Butt_Click;
            Graf_Presure_Butt.Click += Graf_Presure_Butt_Click;
            Graf_U_vel_Butt.Click += Graf_U_vel_Butt_Click;
            Graf_V_vel_Butt.Click += Graf_V_vel_Butt_Click;

            generador = new LibreriaClases.Generadora(); // generamos una clase Generadora cuando inicializamos

        }

        private void Graf_Temperatura_Butt_Click(object sender, RoutedEventArgs e)
        {
            //Ocultamos las labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //definimos las listas
            generador.listaTEMPxColumna_G = listaTEMPxColumna;
            generador.GenerarDatosTEMP(Convert.ToDouble(numDeCOLUMNAS-1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generamos los ejes
            ejeX.Minimum = 0;
            ejeX.Maximum = numDeCOLUMNAS;  //número  de iteraciones
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 267;
            ejeY.Maximum = 287;
            ejeY.Position = AxisPosition.Left;

            model.Axes.Add(ejeY);
            model.Axes.Add(ejeX);
            model.Title = "Evolución de la Temperatura media";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;

        }
        private void Graf_Mach_Butt_Click(object sender, RoutedEventArgs e)
        {
            //Ocultamos las labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //definimos las listas
            generador.listaMachxColumna_G = listaMachxColumna;
            generador.GenerarDatosMACH(Convert.ToDouble(numDeCOLUMNAS - 1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generamos los ejes
            ejeX.Minimum = 0;
            ejeX.Maximum = numDeCOLUMNAS;  //número  de iteraciones
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 1.99;
            ejeY.Maximum = 2.15;
            ejeY.Position = AxisPosition.Left;

            model.Axes.Add(ejeY);
            model.Axes.Add(ejeX);
            model.Title = "Evolución del Mach medio";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;

        }
        private void Graf_Densidad_Butt_Click(object sender, RoutedEventArgs e)
        {
            //Ocultamos las labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //definimos las listas
            generador.listaDensidadxColumna_G = listaDensidadxColumna;
            generador.GenerarDatosDensidad(Convert.ToDouble(numDeCOLUMNAS - 1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generamos los ejes
            ejeX.Minimum = 0;
            ejeX.Maximum = numDeCOLUMNAS;  //número  de iteraciones
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 1;
            ejeY.Maximum = 1.24;
            ejeY.Position = AxisPosition.Left;

            model.Axes.Add(ejeY);
            model.Axes.Add(ejeX);
            model.Title = "Evolución de la Densidad media";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;



        }
        private void Graf_Presure_Butt_Click(object sender, RoutedEventArgs e)
        {
            //Ocultamos las labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //definimos las listas
            generador.listaPresurexColumna_G = listaPresurexColumna;
            generador.GenerarDatosPresure(Convert.ToDouble(numDeCOLUMNAS - 1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generamos los ejes
            ejeX.Minimum = 0;
            ejeX.Maximum = numDeCOLUMNAS;  //número  de iteraciones
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 80000;
            ejeY.Maximum = 102000;
            ejeY.Position = AxisPosition.Left;

            model.Axes.Add(ejeY);
            model.Axes.Add(ejeX);
            model.Title = "Evolución de la Presión media";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;

        }
        private void Graf_U_vel_Butt_Click(object sender, RoutedEventArgs e)
        {
            //Ocultamos las labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //definimos las listas
            generador.listaUxColumna_G = listaUxColumna;
            generador.GenerarDatosU(Convert.ToDouble(numDeCOLUMNAS - 1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generamos los ejes
            ejeX.Minimum = 0;
            ejeX.Maximum = numDeCOLUMNAS;  //número  de iteraciones
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 677;
            ejeY.Maximum = 702;
            ejeY.Position = AxisPosition.Left;

            model.Axes.Add(ejeY);
            model.Axes.Add(ejeX);
            model.Title = "Evolución de la velocidad U media";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;

        }
        private void Graf_V_vel_Butt_Click(object sender, RoutedEventArgs e)
        {
            //Ocultamos las labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //definimos las listas
            generador.listaVxColumna_G = listaVxColumna;
            generador.GenerarDatosV(Convert.ToDouble(numDeCOLUMNAS - 1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generamos los ejes
            ejeX.Minimum = 0;
            ejeX.Maximum = numDeCOLUMNAS;  //número  de iteraciones
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = -50;
            ejeY.Maximum = 1;
            ejeY.Position = AxisPosition.Left;

            model.Axes.Add(ejeY);
            model.Axes.Add(ejeX);
            model.Title = "Evolución de la velocidad V media";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;


        }
        public void SetnumdeCOLUMNAS(int count)
        {
            this.numDeCOLUMNAS = count;
        }




    }
}
