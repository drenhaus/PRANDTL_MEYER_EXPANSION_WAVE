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
        int columnas;
        int filas;
        Polygon[,] casillas;

        DataTable temperature_table;
        DataTable u_table;
        DataTable v_table;
        DataTable rho_table;
        DataTable p_table;
        DataTable M_table;
        DataTable F1_table;
        DataTable F2_table;
        DataTable F3_table;
        DataTable F4_table;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Loaded(object sender, RoutedEventArgs e)
        {
            m.DefinirMatriz();
            m.Compute();
            m.Fill_DataTable();
            DataTable[] T_U_V_RHO_P_M_F1_F2_F3_F4 = m.GetTables();
            temperature_table= T_U_V_RHO_P_M_F1_F2_F3_F4[0];
            u_table= T_U_V_RHO_P_M_F1_F2_F3_F4[1];
            v_table = T_U_V_RHO_P_M_F1_F2_F3_F4[2];
            rho_table = T_U_V_RHO_P_M_F1_F2_F3_F4[3];
            p_table = T_U_V_RHO_P_M_F1_F2_F3_F4[4];
            M_table = T_U_V_RHO_P_M_F1_F2_F3_F4[5];
            F1_table = T_U_V_RHO_P_M_F1_F2_F3_F4[6];
            F2_table = T_U_V_RHO_P_M_F1_F2_F3_F4[7];
            F3_table = T_U_V_RHO_P_M_F1_F2_F3_F4[8];
            F4_table = T_U_V_RHO_P_M_F1_F2_F3_F4[9];
            columnas = m.columns;
            filas = m.rows;

        }

        //open the tables window
        private void TablesButton_Click(object sender, RoutedEventArgs e)
        {
            TablesWindow tables_w = new TablesWindow();
            tables_w.SetTables(temperature_table, u_table, v_table, rho_table, p_table, M_table, F1_table, F2_table, F3_table, F4_table);
            tables_w.Show();


        }

        private void Simulate_Button_Click(object sender, RoutedEventArgs e)
        {
            casillas = new Polygon[filas, columnas];

            double x1= 0; // column left
            double x2; // column right
            double y1; // top left
            double y2; // top right
            double y3; // down left
            double y4; // down right

            double[] delta_y = m.Vector_Delta_y();

            for (int i=0; i<columnas;i++)
            {
                x2 = x1 + m.delta_x;
                y3 = 0;
                y4 = 0;

                for (int j=0; j<filas;j++)
                {
                    y1 = y3 + delta_y[j];
                    y2 = y4 + delta_y[j+1];

                    Polygon myPolygon = new Polygon();
                    myPolygon.Fill = Brushes.Red;

                    System.Windows.Point Point1 = new System.Windows.Point(x1*10, y1*10);
                    System.Windows.Point Point2 = new System.Windows.Point(x2*10, y2*10);
                    System.Windows.Point Point3 = new System.Windows.Point(x1*10, y3*10);
                    System.Windows.Point Point4 = new System.Windows.Point(x2*10, y4*10);
                    PointCollection myPointCollection = new PointCollection();
                    myPointCollection.Add(Point1);
                    myPointCollection.Add(Point2);
                    myPointCollection.Add(Point3);
                    myPointCollection.Add(Point4);
                    myPolygon.Points = myPointCollection;
                    casillas[j, i] = myPolygon;

                    y4 = y2;
                    y3 = y1;                   


                }
                x1 = x2;                
            }
            for (int i = 0; i < columnas; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    GridMalla.Children.Add(casillas[j, i]);
                }
            }

        }
    }
}
