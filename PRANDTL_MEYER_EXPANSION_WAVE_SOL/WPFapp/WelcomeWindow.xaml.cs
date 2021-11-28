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

namespace WPFapp
{
    /// <summary>
    /// Lógica de interacción para WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            label_explicacion.Visibility = Visibility.Visible;
            label_explicacion.Content = "The objective of this project is to simulate and obtain a numerical solution of a flow over a Prandtl-Meyer expansion corner. In this simulation, we are going to suppose that the flow that moves on the surface is two-dimensional, supersonic and invisible and we are going to establish a series of initial conditions in order to simplify the problem and obtain the main properties of the flow that interests us to study its behaviour.";
            //Load text for main label explaining the project
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
