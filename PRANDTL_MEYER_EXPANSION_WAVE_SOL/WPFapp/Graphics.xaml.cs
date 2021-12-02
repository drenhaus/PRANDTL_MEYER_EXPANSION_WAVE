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
        List<double> listaTEMPxColumna = new List<double>();

        bool estamosTEMP = true;
        public Graphics()
        {
            InitializeComponent();

            Graf_Temperatura_Butt.Click += Graf_Temperatura_Butt_Click;
            generador = new LibreriaClases.Generadora(); // generamos una clase Generadora cuando inicializamos




        }

        private void Graf_Temperatura_Butt_Click(object sender, RoutedEventArgs e)
        {
            estamosTEMP = true;
            //definimos las listas
            generador.SetListaTEMPxColumna(listaTEMPxColumna);
            generador.GenerarDatosTEMP(Convert.ToDouble(numDeCOLUMNAS-1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generamos los ejes
            ejeX.Minimum = 0;
            ejeX.Maximum = numDeCOLUMNAS;  //número  de iteraciones
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 0;
            ejeY.Maximum = 300;
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







        public void SetListaTEMPxColumna(List<double> B)
        {
            this.listaTEMPxColumna = B;
        }
        public void SetnumdeCOLUMNAS(int count)
        {
            this.numDeCOLUMNAS = count;
        }

    }
}
