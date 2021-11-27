using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibreriaClases;
using System.Data;

namespace WPFapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Malla m = new Malla();
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Loaded(object sender, RoutedEventArgs e)
        {
            m.DefinirMatriz();
            m.Compute();
           
          

            DataTable temperature = m.Fill_DataTable();
            grid2.DataContext = temperature.DefaultView;


        }
    }
}
