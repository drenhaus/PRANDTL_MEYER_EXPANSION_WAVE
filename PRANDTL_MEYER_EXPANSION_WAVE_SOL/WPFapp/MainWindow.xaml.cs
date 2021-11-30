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

      

        //open the tables window
        private void TablesButton_Click(object sender, RoutedEventArgs e)
        {
            TablesWindow tables_w = new TablesWindow();
            tables_w.SetTables(temperature_table, u_table, v_table, rho_table, p_table, M_table, F1_table, F2_table, F3_table, F4_table);
            tables_w.Show();


        }

        private void Simulate_Button_Click(object sender, RoutedEventArgs e)
        {
            m.DefinirMatriz();
            m.Compute();
            m.Fill_DataTable();
            DataTable[] T_U_V_RHO_P_M_F1_F2_F3_F4 = m.GetTables();
            temperature_table = T_U_V_RHO_P_M_F1_F2_F3_F4[0];
            u_table = T_U_V_RHO_P_M_F1_F2_F3_F4[1];
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
            GenerateGridPlot();

            DataGridComboBox.IsEnabled = true;
        }

        public void GenerateGridPlot()
        {
            casillas = new Polygon[filas, columnas - 1];

            double x1 = 0; // column left
            double x2; // column right
            double y1; // top left
            double y2; // top right
            double y3; // down left
            double y4; // down right

            double[] delta_y = m.Vector_Delta_y();

            for (int i = 0; i < columnas - 1; i++)
            {
                x2 = x1 + m.delta_x;
                y3 = 0;
                y4 = 0;

                for (int j = 0; j < filas; j++)
                {
                    y1 = y3 + delta_y[i];
                    y2 = y4 + delta_y[i + 1];

                    Polygon myPolygon = new Polygon();
                    System.Windows.Point Point1 = new System.Windows.Point(x1 * 7, y1 * 7);
                    System.Windows.Point Point2 = new System.Windows.Point(x2 * 7, y2 * 7);
                    System.Windows.Point Point3 = new System.Windows.Point(x1 * 7, y3 * 7);
                    System.Windows.Point Point4 = new System.Windows.Point(x2 * 7, y4 * 7);
                    PointCollection myPointCollection = new PointCollection();
                    myPointCollection.Add(Point1);
                    myPointCollection.Add(Point3);
                    myPointCollection.Add(Point2);
                    myPointCollection.Add(Point4);
                    myPolygon.Points = myPointCollection;
                    casillas[j, i] = myPolygon;

                    y4 = y2;
                    y3 = y1;


                }
                x1 = x2;
            }
            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    GridMalla.Children.Add(casillas[j, i]);
                }
            }
        }

        private void DataGridComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridComboBox.SelectedIndex == 0) //temperature
            {
                actualizar_colores_grid(temperature_table, 255, 0, 0);                 
            }
            if (DataGridComboBox.SelectedIndex == 1) //u
            {
                actualizar_colores_grid(u_table, 0, 255, 0);
            }
            if (DataGridComboBox.SelectedIndex == 2) //v
            {
                actualizar_colores_grid(v_table, 255, 128, 0);
            }
            if (DataGridComboBox.SelectedIndex == 3) //rho
            {
                actualizar_colores_grid(rho_table, 0, 0, 255);
            }
            if (DataGridComboBox.SelectedIndex == 4) //p
            {
                actualizar_colores_grid(p_table, 255, 0, 127);
            }
            if (DataGridComboBox.SelectedIndex == 5) //Mach
            {
                actualizar_colores_grid(M_table, 96, 96, 96);
            }

        }


        public double[] Max_Min_Datatables(DataTable data_t)
        {
            double max = Convert.ToDouble(data_t.Rows[0][0].ToString());
            double min = Convert.ToDouble(data_t.Rows[0][0].ToString());
            for (int i = 0; i < columnas; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    if (Convert.ToDouble(data_t.Rows[j][i].ToString()) < min)
                    {
                        min = Convert.ToDouble(data_t.Rows[j][i].ToString());
                    }
                    if (Convert.ToDouble(data_t.Rows[j][i].ToString()) > max)
                    {
                        max = Convert.ToDouble(data_t.Rows[j][i].ToString());
                    }

                }
            }
            double[] values = { max, min };
            return values;
        }

        public byte Define_Cloroes(double max, double min, double value)
        {
            double rango = max - min;

            byte alpha;

            //max byte --255
            // min byte --25

            //interpolamos para sacar el parametro alpa
            alpha = Convert.ToByte(30 + (255 - 30) / (max - min) * (value - min));

            return alpha;

        }

        public void actualizar_colores_grid(DataTable t, byte R, byte G, byte B)
        {
            double[] max_min;
            max_min = Max_Min_Datatables(t);

            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    byte alpha = Define_Cloroes(max_min[0], max_min[1], Convert.ToDouble(t.Rows[filas - 1 - j][i].ToString()));
                    casillas[j, i].Fill = new SolidColorBrush(Color.FromArgb(alpha, R, G, B));
                }
            }

        }


    }
}
