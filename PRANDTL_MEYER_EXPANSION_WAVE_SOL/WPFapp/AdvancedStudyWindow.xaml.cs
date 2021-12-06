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


        Malla m2 = new Malla();
        Malla m3 = new Malla();
        Malla m4 = new Malla();
        Malla m5 = new Malla();

        GridPlotGenerate GPG2 = new GridPlotGenerate();
        GridPlotGenerate GPG3 = new GridPlotGenerate();
        GridPlotGenerate GPG4 = new GridPlotGenerate();
        GridPlotGenerate GPG5 = new GridPlotGenerate();

        DataTable temperature_table_1= new DataTable();
        DataTable u_table_1 = new DataTable();
        DataTable v_table_1 = new DataTable();
        DataTable rho_table_1 = new DataTable();
        DataTable p_table_1 = new DataTable();
        DataTable M_table_1 = new DataTable();

        DataTable temperature_table_2 = new DataTable();
        DataTable u_table_2 = new DataTable();
        DataTable v_table_2 = new DataTable();
        DataTable rho_table_2 = new DataTable();
        DataTable p_table_2 = new DataTable();
        DataTable M_table_2 = new DataTable();



        Polygon[,] casillas2;
        Polygon[,] casillas3;
        Polygon[,] casillas4;
        Polygon[,] casillas5;

        DataTable[] tablesM2;
        DataTable[] tablesM3;
        DataTable[] tablesM4;
        DataTable[] tablesM5;

        public AdvancedStudyWindow()
        {
            InitializeComponent();

            // m2 sera la primera malla del caso 1 (caso 1 el de dos pendientes de angulo distinto juntas)
            double sumaTheta = (10 * (Math.PI) / 180);


            // Parimos de cero, es decir, nueva malla y reacemos el calculo de una matriz en resolucion media
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
            m2.Fill_DataTable();
            tablesM2 = m2.GetTables();

            List<Celda> ListadeUltimaColumnadeCeldas_caso1 = GetLastColumOfMatriz(m2);

            //Empezamos con la segunda malla que empieza con los ultimos datos d ela anterior matriz. 

           

            m3.norma.L = 45;
            m3.norma.E = 0.1;
            m3.norma.H = 40;

            m2.norma.Theta = ((2 * sumaTheta) / 3);

            m3.rows = 41;
            m3.columns = 61;
            m3.delta_y_t = 0.025;


            m3.DefinirMatriz();
            m3.Compute2(ListadeUltimaColumnadeCeldas_caso1);
            m3.Fill_DataTable();
            tablesM3 = m3.GetTables();

          

            //CASO 2!!

            // Caso dos, tenemos dos pendientes eparadas por superficie plana


            //10 m llegamos a E, 30 M llegamos a final, 30 metros de estaviliadad, y repetimops pero con
            // angulo dos


            

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
            m4.Fill_DataTable();
            tablesM4 = m4.GetTables();

            List<Celda> ListadeUltimaColumnadeCeldas_caso2 = GetLastColumOfMatriz(m4);

           


            m5.norma.L = 60;
            m5.norma.E = 30;
            m5.norma.H = 40;

            m5.norma.Theta = ((2 * sumaTheta) / 3);

            m5.rows = 41;
            m5.columns = 91;
            m5.delta_y_t = 0.025;


            m5.DefinirMatriz();
            m5.Compute2(ListadeUltimaColumnadeCeldas_caso2);
            m5.Fill_DataTable();
            tablesM5 = m5.GetTables();

            GPG2.dimension_scale = 6;
            GPG3.dimension_scale = 6;
            GPG4.dimension_scale = 6;
            GPG5.dimension_scale = 6;

            casillas2 = GPG2.GenerateGridPlot(m2.rows, m2.columns, m2);
            casillas3 = GPG3.GenerateGridPlot(m3.rows, m3.columns, m3);
            casillas4 = GPG4.GenerateGridPlot(m4.rows, m4.columns, m4);
            casillas5 = GPG5.GenerateGridPlot(m5.rows, m5.columns, m5);

            // CASO 1, Advanced_GridMalla_CASO1 y Advanced_GridMalla_2_CASO1

            for (int i = 0; i < m2.columns - 1; i++)
            {
                for (int j = 0; j < m2.rows; j++)
                {
                    Advanced_GridMalla_CASO1.Children.Add(casillas2[j, i]);
                }
            }

            for (int i = 0; i < m3.columns - 1; i++)
            {
                for (int j = 0; j < m3.rows; j++)
                {
                    Advanced_GridMalla_2_CASO1.Children.Add(casillas3[j, i]);
                }
            }
            // CASO 3, Advanced_GridMalla_CASO2 y Advanced_GridMalla_2_CASO2

            for (int i = 0; i < m4.columns - 1; i++)
            {
                for (int j = 0; j < m4.rows; j++)
                {
                    Advanced_GridMalla_CASO2.Children.Add(casillas4[j, i]);
                }
            }
            for (int i = 0; i < m5.columns - 1; i++)
            {
                for (int j = 0; j < m5.rows; j++)
                {
                    Advanced_GridMalla_2_CASO2.Children.Add(casillas5[j, i]);
                }
            }

            // Unimos las tablas de m2 con m3 
            temperature_table_1 = JuntarTablas(tablesM2[0], tablesM3[0]);
            u_table_1 = JuntarTablas(tablesM2[1], tablesM3[1]);
            v_table_1 = JuntarTablas(tablesM2[2], tablesM3[2]);
            rho_table_1 = JuntarTablas(tablesM2[3], tablesM3[3]);
            p_table_1 = JuntarTablas(tablesM2[4], tablesM3[4]);
            M_table_1 = JuntarTablas(tablesM2[5], tablesM3[5]);

            // Unimos las tablas m4 con m5
            temperature_table_2 = JuntarTablas(tablesM4[0], tablesM5[0]);
            u_table_2 = JuntarTablas(tablesM4[1], tablesM5[1]);
            v_table_2 = JuntarTablas(tablesM4[2], tablesM5[2]);
            rho_table_2 = JuntarTablas(tablesM4[3], tablesM5[3]);
            p_table_2 = JuntarTablas(tablesM4[4], tablesM5[4]);
            M_table_2 = JuntarTablas(tablesM4[5], tablesM5[5]);




            if (DataGridComboBox_AS.SelectedIndex == 0)
            {
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[0], 255, 0, 0,temperature_table_1,m2.rows+m3.rows,m2.columns+m3.columns);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[0], 255, 0, 0, temperature_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[0], 255, 0, 0,temperature_table_2, m4.rows+m5.rows,m4.columns+m5.columns);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[0], 255, 0, 0,temperature_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                Advanced_DataGridMalla.DataContext = temperature_table_1.DefaultView;
                Advanced_DataGridMalla_2.DataContext = temperature_table_2.DefaultView;
            }

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


        private void DataGridComboBox_AS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            if (DataGridComboBox_AS.SelectedIndex == 0) //temperature
            {
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[0], 255, 0, 0,temperature_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[0], 255, 0, 0, temperature_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[0], 255, 0, 0, temperature_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[0], 255, 0, 0, temperature_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                //Advanced_DataGridMalla.DataContext = temperature_table_1.DefaultView;
                //Advanced_DataGridMalla_2.DataContext = temperature_table_2.DefaultView;
            }
            if (DataGridComboBox_AS.SelectedIndex == 1) //u
            {
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[1], 0, 255, 0,u_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[1], 0, 255, 0, u_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[1], 0, 255, 0, u_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[1], 0, 255, 0, u_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                //Advanced_DataGridMalla.DataContext = u_table_1.DefaultView;
                //Advanced_DataGridMalla_2.DataContext = u_table_2.DefaultView;
            }
            if (DataGridComboBox_AS.SelectedIndex == 2) //v
            {
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[2], 255, 128, 0, v_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[2], 255, 128, 0, v_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[2], 255, 128, 0, v_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[2], 255, 128, 0, v_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                //Advanced_DataGridMalla.DataContext = v_table_1.DefaultView;
                //Advanced_DataGridMalla_2.DataContext = v_table_2.DefaultView;
            }
            if (DataGridComboBox_AS.SelectedIndex == 3) //rho
            {
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[3], 0, 0, 255, rho_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[3], 0, 0, 255, rho_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[3], 0, 0, 255,rho_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[3], 0, 0, 255, rho_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                //Advanced_DataGridMalla.DataContext = rho_table_1.DefaultView;
               // Advanced_DataGridMalla_2.DataContext = rho_table_2.DefaultView;
            }
            if (DataGridComboBox_AS.SelectedIndex == 4) //p
            {
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[4], 255, 0, 127,p_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[4], 255, 0, 127,p_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[4], 255, 0, 127,p_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[4], 255, 0, 127, p_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                //Advanced_DataGridMalla.DataContext = p_table_1.DefaultView;
                //Advanced_DataGridMalla_2.DataContext = p_table_2.DefaultView;
            }
            if (DataGridComboBox_AS.SelectedIndex == 5) //Mach
            {
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[5], 96, 96, 96,M_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[5], 96, 96, 96, M_table_1, m2.rows + m3.rows, m2.columns + m3.columns);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[5], 96, 96, 96, M_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[5], 96, 96, 96, M_table_2, m4.rows + m5.rows, m4.columns + m5.columns);
                //Advanced_DataGridMalla.DataContext = M_table_1.DefaultView;
                //DataContext = M_table_2.DefaultView;
            }
        }

        public DataTable JuntarTablas(DataTable dt1, DataTable dt2)
        {
            DataTable Unida = new DataTable();
            int dt1_rows = dt1.Rows.Count;
            int dt1_columns = dt1.Columns.Count;
            int dt2_columns = dt2.Columns.Count;
                        
            for (int i = 0; i < dt1_columns+dt2_columns; i++)
            {
                DataColumn dc = new DataColumn();
                Unida.Columns.Add(dc);
            }

            for (int j = 0; j < dt1_rows; j++)
            {
                DataRow dr = Unida.NewRow();
           
                for (int i = 0; i < dt1_columns + dt2_columns; i++)
                {
                    if (i < dt1_columns)
                    {
                        dr[i] = Convert.ToDouble(dt1.Rows[j][i].ToString());
                    }
                    else
                    {
                        dr[i] = Convert.ToDouble(dt2.Rows[j][i - dt1_columns].ToString());
                    }
                    
                }

                Unida.Rows.Add(dr);
              
            }

            return Unida;


        }


        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Mini_Button_Click(object sender, RoutedEventArgs e)
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

    }

   
}

