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
        #region ATRIBUTES
        
        Generadora generador;// new class of Generadora

        int numDeCOLUMNAS; // nº of columns
        public double valorMaximdeX { get; set; } // X max

        public List<double> listaDeXColumna { get; set; } // list of x
        public List<double> listaTEMPxColumna { get; set; } // list of temperature
        public List<double> listaMachxColumna { get; set; } // list of mach
        public List<double> listaDensidadxColumna { get; set; } // list of density
        public List<double> listaPresurexColumna { get; set; } // list of pressure
        public List<double> listaUxColumna { get; set; } // list of u
        public List<double> listaVxColumna { get; set; } // list of v
        #endregion ATRIBUTES

        public Graphics()
        {
            InitializeComponent();

            // when initializing we define the event creation on buttons click 
            #region EVENT CREATION ON BUTTON CLICK

            Graf_Temperatura_Butt.Click += Graf_Temperatura_Butt_Click; // temperature button
            Graf_Mach_Butt.Click += Graf_Mach_Butt_Click; // mach button
            Graf_Densidad_Butt.Click += Graf_Densidad_Butt_Click; // density button
            Graf_Presure_Butt.Click += Graf_Presure_Butt_Click; // pressure button
            Graf_U_vel_Butt.Click += Graf_U_vel_Butt_Click; // u button
            Graf_V_vel_Butt.Click += Graf_V_vel_Butt_Click; // v button
            #endregion EVENT CREATION ON BUTTON CLICK

            generador = new LibreriaClases.Generadora(); // generamos una clase Generadora cuando inicializamos
        }

        #region GRAPH CREATION BY BUTTON CLICK
        
        // TEMPERATURE GRAPHICS
            // When clicking in the temperature graphics the mean temperature at each postion
            // is ploted
        private void Graf_Temperatura_Butt_Click(object sender, RoutedEventArgs e)
        {
            //hiding labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //defining list
            generador.listaTEMPxColumna_G = listaTEMPxColumna;
            generador.listaDeXColumna_G = listaDeXColumna;
            generador.GenerarDatosTEMP(Convert.ToDouble(numDeCOLUMNAS-1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generating axes
            ejeX.Minimum = 0;
            ejeX.Maximum = valorMaximdeX;  //number of iterations
            ejeX.Position = AxisPosition.Bottom;
            ejeX.Title = "POSITION [m]";

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 267;
            ejeY.Maximum = 287;
            ejeY.Position = AxisPosition.Left;
            ejeY.Title = "TEMPERATURE [Kelvin]";

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
       
        // MACH GRAPHICS
            // When clicking in the mach graphics the mean mach at each postion
            // is ploted
        private void Graf_Mach_Butt_Click(object sender, RoutedEventArgs e)
        {
            //hiding labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //defining lists
            generador.listaMachxColumna_G = listaMachxColumna;
            generador.listaDeXColumna_G = listaDeXColumna;
            generador.GenerarDatosMACH(Convert.ToDouble(numDeCOLUMNAS - 1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //creating the axes
            ejeX.Minimum = 0;
            ejeX.Maximum = valorMaximdeX;  //number of iterations
            ejeX.Position = AxisPosition.Bottom;
            ejeX.Title = "POSITION [m]";

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 1.99;
            ejeY.Maximum = 2.15;
            ejeY.Position = AxisPosition.Left;
            ejeY.Title = "MACH [-]";

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

        // DENSITY GRAPHICS
            // When clicking in the density graphics the mean density at each postion
            // is ploted
        private void Graf_Densidad_Butt_Click(object sender, RoutedEventArgs e)
        {
            //hiding labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //defining lists
            generador.listaDensidadxColumna_G = listaDensidadxColumna;
            generador.listaDeXColumna_G = listaDeXColumna;
            generador.GenerarDatosDensidad(Convert.ToDouble(numDeCOLUMNAS - 1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //creating the axes
            ejeX.Minimum = 0;
            ejeX.Maximum = valorMaximdeX;  //number of iterations
            ejeX.Position = AxisPosition.Bottom;
            ejeX.Title = "POSITION [m]";

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 1;
            ejeY.Maximum = 1.24;
            ejeY.Position = AxisPosition.Left;
            ejeY.Title = "DENSITY [kg/m^3]";

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

        // PRESSURE GRAPHICS
            // When clicking in the pressure graphics the mean pressure at each postion
            // is ploted
        private void Graf_Presure_Butt_Click(object sender, RoutedEventArgs e)
        {
            //hiding labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //defining lists
            generador.listaPresurexColumna_G = listaPresurexColumna;
            generador.listaDeXColumna_G = listaDeXColumna;
            generador.GenerarDatosPresure(Convert.ToDouble(numDeCOLUMNAS - 1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //creating axes
            ejeX.Minimum = 0;
            ejeX.Maximum = valorMaximdeX;  //number of iterations
            ejeX.Position = AxisPosition.Bottom;
            ejeX.Title = "POSITION [m]";

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 80000;
            ejeY.Maximum = 102000;
            ejeY.Position = AxisPosition.Left;
            ejeY.Title = "PRESURE [N/m^2]";

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

        // U GRAPHICS
            // When clicking in the U graphics the mean U at each postion
            // is ploted
        private void Graf_U_vel_Butt_Click(object sender, RoutedEventArgs e)
        {
            //Hiding labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //defining lists
            generador.listaUxColumna_G = listaUxColumna;
            generador.listaDeXColumna_G = listaDeXColumna;
            generador.GenerarDatosU(Convert.ToDouble(numDeCOLUMNAS - 1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //creating axes
            ejeX.Minimum = 0;
            ejeX.Maximum = valorMaximdeX;  //number of iterations
            ejeX.Position = AxisPosition.Bottom;
            ejeX.Title = "POSITION [m]";

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 677;
            ejeY.Maximum = 702;
            ejeY.Position = AxisPosition.Left; 
            ejeY.Title = "HORIZONTAL VELOCITY (U) [m/s]";

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

        // V GRAPHICS
            // When clicking in the V graphics the mean V at each postion
            // is ploted
        private void Graf_V_vel_Butt_Click(object sender, RoutedEventArgs e)
        {
            //Hiding labels
            labelmedio.Visibility = Visibility.Hidden;
            labelmedio2.Visibility = Visibility.Hidden;

            //defining lists
            generador.listaVxColumna_G = listaVxColumna;
            generador.listaDeXColumna_G = listaDeXColumna;
            generador.GenerarDatosV(Convert.ToDouble(numDeCOLUMNAS - 1));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //creating axes
            ejeX.Minimum = 0;
            ejeX.Maximum = valorMaximdeX;  //number of iterations
            ejeX.Position = AxisPosition.Bottom;
            ejeX.Title = "POSITION [m]";

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = -50;
            ejeY.Maximum = 1;
            ejeY.Position = AxisPosition.Left;
            ejeY.Title = "VERTICAL VELOCITY (V) [m/s]";

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

        #endregion GRAPH CREATION BY BUTTON CLICK

        #region SET INT COLMN
        // SETTING THE NUMBER OF COLUMNS
            // The int number of columns introduced as an input is the numDeCOLUMNAS
            // defined in the attributes section
        public void SetnumdeCOLUMNAS(int count)
        {
            this.numDeCOLUMNAS = count;
        }
        #endregion SET INT COLMN

        #region WINDOW MANIPULATION FUNCTIONS
        // MINIMIZE BUTTON
            //When clicking to the top right button (yellow circle) the current window minimises
        private void Mini_Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // CLOSE BUTTON
            //When clicking to the top right button (red circle) the current window closes
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        // DRAG MOVE
            //When the left button is pressed and draged, the window can be moved
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion WINDOW MANIPULATION FUNCTIONS

    }
}
