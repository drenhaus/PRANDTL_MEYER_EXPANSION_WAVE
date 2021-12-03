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
        int columnas = 89;
        int filas = 41;
        int dimension_scale = 7;
        double delta_y_t;

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
            m.rows = filas;
            m.columns = columnas;
            m.delta_y_t = this.delta_y_t;

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
            GenerateGridPlot();

            if (DataGridComboBox.SelectedIndex == 0)
            {
                actualizar_colores_grid(temperature_table, 255, 0, 0);
            }

            LoadParametersButton.IsEnabled = false;
            LoadPresitionButton.IsEnabled = false;
            DataGridComboBox.IsEnabled = true;
            Simulate_Button.IsEnabled = false;
            Reset_button.IsEnabled = true;

            MessageBox.Show("Please select the data you want to show or reset for any change");


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
                    System.Windows.Point Point1 = new System.Windows.Point(x1 * dimension_scale, y1 * dimension_scale);
                    System.Windows.Point Point2 = new System.Windows.Point(x2 * dimension_scale, y2 * dimension_scale);
                    System.Windows.Point Point3 = new System.Windows.Point(x1 * dimension_scale, y3 * dimension_scale);
                    System.Windows.Point Point4 = new System.Windows.Point(x2 * dimension_scale, y4 * dimension_scale);
                    myPolygon.StrokeThickness = 0;
                    PointCollection myPointCollection = new PointCollection();
                    myPointCollection.Add(Point1);
                    myPointCollection.Add(Point2);
                    myPointCollection.Add(Point4);
                    myPointCollection.Add(Point3);
                    myPolygon.Points = myPointCollection;
                    casillas[j, i] = myPolygon;

                    myPolygon.MouseEnter += new System.Windows.Input.MouseEventHandler(polygon_enter);

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

        private void ComparationButton_Click(object sender, RoutedEventArgs e)
        {
            AndersonComparationWindow anderson_w = new AndersonComparationWindow();
            anderson_w.m = this.m;
            anderson_w.filas = this.filas;
            // define the columns we want to check
            //tables_w.SetTables(temperature_table, u_table, v_table, rho_table, p_table, M_table, F1_table, F2_table, F3_table, F4_table);
            anderson_w.Show();


        }

        private void CheckBox_A_Checked(object sender, RoutedEventArgs e)
        {
            v_TextBox.Text = Convert.ToString(0.0);
            Rho_TextBox.Text = Convert.ToString(1.23);
            P_TextBox.Text = Convert.ToString(101000);
            M_TextBox.Text = Convert.ToString(2.0);
            T_TextBox.Text = Convert.ToString(286.1);
        }

        private void LoadParametersButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                m.norma.v_in = Convert.ToDouble(v_TextBox.Text);
                m.norma.Rho_in = Convert.ToDouble(Rho_TextBox.Text);
                m.norma.P_in = Convert.ToDouble(P_TextBox.Text);
                m.norma.M_in = Convert.ToDouble(M_TextBox.Text);
                m.norma.T_in = Convert.ToDouble(T_TextBox.Text);

                m.norma.Compute_a();
                m.norma.Compute_M_angle();
                m.norma.Compute_u();

                Simulate_Button.IsEnabled = true;

                MessageBox.Show("Parameters defined");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Reset_button_Click(object sender, RoutedEventArgs e)
        {

            m = new Malla();
            casillas = null;
            temperature_table = null;
            u_table = null;
            v_table = null;
            rho_table = null;
            p_table = null;
            M_table = null;
            F1_table = null;
            F2_table = null;
            F3_table = null;
            F4_table = null;
            DataGridComboBox.IsEnabled = false;
            GridMalla.Children.Clear();

            DataGridComboBox.SelectedItem = null;

            Simulate_Button.IsEnabled = false;
            Reset_button.IsEnabled = false;
            LoadParametersButton.IsEnabled = false;
            LoadPresitionButton.IsEnabled = true;
        }

        private void AdvancedStudyButton_Click(object sender, RoutedEventArgs e)
        {
            AdvancedStudyWindow ad_w = new AdvancedStudyWindow();
            ad_w.Show();

            Malla m2 = new Malla();

            columnas = 89;
            filas = 41;
            delta_y_t = 0.025;

            m2.rows = filas;
            m2.columns = columnas;
            m2.delta_y_t = delta_y_t;


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
            m2.Fill_DataTable();



        }

        private void LoadPresitionButton_Click(object sender, RoutedEventArgs e)
        {
            if (PresitionComboBox.SelectedIndex == 0) //small
            {
                columnas = 23;
                filas = 11;
                delta_y_t = 0.1;

            }
            if (PresitionComboBox.SelectedIndex == 1) // normal
            {
                columnas = 89;
                filas = 41;
                delta_y_t = 0.025;

            }
            if (PresitionComboBox.SelectedIndex == 2) //high
            {
                columnas = 440;
                filas = 201;
                delta_y_t = 0.005;

            }
            LoadParametersButton.IsEnabled = true;
            MessageBox.Show("Precision selected successfully");



        }

        private void GraficButton_Click(object sender, RoutedEventArgs e)
        {
            Graphics gr = new Graphics();
            m.CrearListade("T");
            m.CrearListade("M");
            m.CrearListade("Rho");
            m.CrearListade("P");
            m.CrearListade("u");
            m.CrearListade("v");
            gr.Show();

            gr.SetnumdeCOLUMNAS(m.columns);

            gr.listaTEMPxColumna = m.listaTemperaturaxColumna;
            gr.listaMachxColumna = m.listaMachxColumna;
            gr.listaDensidadxColumna = m.listaDensidadxColumna;
            gr.listaPresurexColumna = m.listaPresurexColumna;
            gr.listaUxColumna = m.listaU_velxColumna;
            gr.listaVxColumna = m.listaV_velxColumna;


        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MiniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void polygon_enter(object sender, EventArgs e)
        {
            Polygon poly = (Polygon)sender;

            int i = 0;
            int j = 0;
            int w = 0;

            for (i = 0; i < columnas - 1; i++)
            {
                for (j = 0; j < filas; j++)
                {
                    if (poly == casillas[j, i])
                    {
                        w = 1;
                        break;
                    }
                }

                // u_label.Content = Convert.ToString(m.matriz[filas-j, columnas-i-1].u);
                // v_label.Content= Convert.ToString(m.matriz[filas - j, columnas - i - 1].v);
                // rho_label.Content= Convert.ToString(m.matriz[filas - j, columnas - i - 1].Rho);
                // p_label.Content= Convert.ToString(m.matriz[filas - j, columnas - i - 1].P);
                // temeprature_label.Content= Convert.ToString(m.matriz[filas - j, columnas - i - 1].T);
                // mach_label.Content= Convert.ToString(m.matriz[filas - j, columnas - i - 1].M);


            }

        }



    }

}
