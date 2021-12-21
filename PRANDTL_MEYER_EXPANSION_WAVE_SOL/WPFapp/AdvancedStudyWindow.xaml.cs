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
using System.Windows.Threading;

namespace WPFapp
{
    /// <summary>
    /// Lógica de interacción para AdvancedStudyWindow.xaml
    /// </summary>
    public partial class AdvancedStudyWindow : Window
    {

        #region ATRIBUTES
        // bool parameters
        bool FijarValor1 = false;
        bool FijarValor2 = false;
        // new mallas
        Malla m2 = new Malla();
        Malla m3 = new Malla();
        Malla m4 = new Malla();
        Malla m5 = new Malla();
        //new grid plots
        GridPlotGenerate GPG2 = new GridPlotGenerate();
        GridPlotGenerate GPG3 = new GridPlotGenerate();
        GridPlotGenerate GPG4 = new GridPlotGenerate();
        GridPlotGenerate GPG5 = new GridPlotGenerate();
        // new tables
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
        // new matrixs of polygons
        Polygon[,] casillas2;
        Polygon[,] casillas3;
        Polygon[,] casillas4;
        Polygon[,] casillas5;
        //general tables M2,M3,M4,M5
        DataTable[] tablesM2;
        DataTable[] tablesM3;
        DataTable[] tablesM4;
        DataTable[] tablesM5;

        //timer for displaying the loading window
        DispatcherTimer dispatcherTimer;
        int timer = 0;
        Loading ld; // loading window
        #endregion ATRIBUTES

        public AdvancedStudyWindow()
        {
            InitializeComponent();

            double sumaTheta = (10 * (Math.PI) / 180);

            #region FRIST CASE PARAMETERS (2 STATGE)

            #region MALLA M2 DEFINITION
            // m2  is the first Malla for case 1 (case 1 are two different angles together
          
            // Starting from 0, new Malla and redo all the computations of a matrix with medium resulation
            // the idea is making another matrix with the data gathered in the last column

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
            // list with the information of the last column
            List<Celda> ListadeUltimaColumnadeCeldas_caso1 = GetLastColumOfMatriz(m2);
            #endregion MALLA M2 DEFINITION

            #region MALLA M3 DEFINITION
            // Starting with the second Malla that starts with the last values of the previous matrix

            m3.norma.L = 45;
            m3.norma.E = 0.1;
            m3.norma.H = 40;

            m2.norma.Theta = ((2 * sumaTheta) / 3);

            m3.rows = 41;
            m3.columns = 61;
            m3.delta_y_t = 0.025;


            m3.DefinirMatriz();
            // we define the initial data line as the list of the last values column of the previous Malla
            m3.Compute2(ListadeUltimaColumnadeCeldas_caso1); 
            m3.Fill_DataTable();
            tablesM3 = m3.GetTables();

            #endregion MALLA M3 DEFINITION

            #endregion FRISRT CASE PARAMETERS (2 STAGE)

            #region SECOND CASE PARAMETERS (3 STAGE)
            //CASO 2!!

            // Case 2, we have two angles different separated by a straight surface

            #region MALLA M4 DEFINITION
            //10 m to E, 30 M ti final, 30 metros of stabilization and redoing for two angles
          
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
            // list with the values of the last column of the matrix  
            List<Celda> ListadeUltimaColumnadeCeldas_caso2 = GetLastColumOfMatriz(m4);

            #endregion MALLA M4 DEFINITION

            #region MALLA M5 DEFINITION

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
            #endregion MALLA M5 DEFINITION

            #endregion SECOND CASE PARAMETERS (3 STAGE)


            #region DIMENSION SCALE DEFINITIONM
            // setting the dimension scale lower for fitting the grids in the window
            GPG2.dimension_scale = 6;
            GPG3.dimension_scale = 6;
            GPG4.dimension_scale = 6;
            GPG5.dimension_scale = 6;
            #endregion DIMENSION SCALE DEFINITIONM
            
            // generating grid plots
            casillas2 = GPG2.GenerateGridPlot(m2.rows, m2.columns, m2);
            casillas3 = GPG3.GenerateGridPlot(m3.rows, m3.columns, m3);
            casillas4 = GPG4.GenerateGridPlot(m4.rows, m4.columns, m4);
            casillas5 = GPG5.GenerateGridPlot(m5.rows, m5.columns, m5);


            // CASE 1, Advanced_GridMalla_CASO1 y Advanced_GridMalla_2_CASO1

            //we make a loop through all the columns and rows and add events Mouse enter,
            // right button, left button and add the polygons in the grid
            for (int i = 0; i < m2.columns - 1; i++)
            {
                for (int j = 0; j < m2.rows; j++)
                {
                    casillas2[j, i].MouseEnter += polygon_enter2; // event entering mouse
                    casillas2[j, i].MouseRightButtonDown += right_button1; // event right click
                    casillas2[j, i].MouseLeftButtonDown += left_button1; // event left click
                    Advanced_GridMalla_CASO1.Children.Add(casillas2[j, i]); // adding the polygons in the grid
                }
            }
            //we make a loop through all the columns and rows and add events Mouse enter,
            // right button, left button and add the polygons in the grid
            for (int i = 0; i < m3.columns - 1; i++)
            {
                for (int j = 0; j < m3.rows; j++)
                {
                    casillas3[j, i].MouseLeftButtonDown += left_button1; // event left click
                    casillas3[j, i].MouseEnter += polygon_enter3; // event entering mouse
                    casillas3[j, i].MouseRightButtonDown += right_button1; // event right click
                    Advanced_GridMalla_2_CASO1.Children.Add(casillas3[j, i]); // adding the polygons in the grid
                }
            }
            // CASE 2, Advanced_GridMalla_CASO2 y Advanced_GridMalla_2_CASO2
           
            //we make a loop through all the columns and rows and add events Mouse enter,
            // right button, left button and add the polygons in the grid
            for (int i = 0; i < m4.columns - 1; i++)
            {
                for (int j = 0; j < m4.rows; j++)
                {
                    casillas4[j, i].MouseEnter += polygon_enter4; // event entering mouse
                    casillas4[j, i].MouseRightButtonDown += right_button2; // event right click
                    casillas4[j, i].MouseLeftButtonDown += left_button2; // event left click
                    Advanced_GridMalla_CASO2.Children.Add(casillas4[j, i]); // adding the polygons in the grid
                }
            }
            //we make a loop through all the columns and rows and add events Mouse enter,
            // right button, left button and add the polygons in the grid
            for (int i = 0; i < m5.columns - 1; i++)
            {
                for (int j = 0; j < m5.rows; j++)
                {
                    casillas5[j, i].MouseEnter += polygon_enter5; // event entering mouse
                    casillas5[j, i].MouseLeftButtonDown += left_button2; // event left click
                    casillas5[j, i].MouseRightButtonDown += right_button2; // event right click
                    Advanced_GridMalla_2_CASO2.Children.Add(casillas5[j, i]); // adding the polygons in the grid
                }
            }

            #region TABLE MANIPULATION
            // Put together tables of m2 and m3  
            temperature_table_1 = JuntarTablas(tablesM2[0], tablesM3[0]);
            u_table_1 = JuntarTablas(tablesM2[1], tablesM3[1]);
            v_table_1 = JuntarTablas(tablesM2[2], tablesM3[2]);
            rho_table_1 = JuntarTablas(tablesM2[3], tablesM3[3]);
            p_table_1 = JuntarTablas(tablesM2[4], tablesM3[4]);
            M_table_1 = JuntarTablas(tablesM2[5], tablesM3[5]);

            // Put together tables of m4 and m5  
            temperature_table_2 = JuntarTablas(tablesM4[0], tablesM5[0]);
            u_table_2 = JuntarTablas(tablesM4[1], tablesM5[1]);
            v_table_2 = JuntarTablas(tablesM4[2], tablesM5[2]);
            rho_table_2 = JuntarTablas(tablesM4[3], tablesM5[3]);
            p_table_2 = JuntarTablas(tablesM4[4], tablesM5[4]);
            M_table_2 = JuntarTablas(tablesM4[5], tablesM5[5]);
            #endregion TABLE MANIPULATION

        }

        // GET LAST COLUMN OF A MATRIX
        // Introducing a malla the function returns a list with the values of 
        // the last column of the matrix
        
        #region LAST COLUMN
        public List<Celda> GetLastColumOfMatriz(Malla Nmalla)
        {
            List<Celda> lista = new List<Celda>(); // returning list

            int col = Nmalla.columns; // columns
            int row = Nmalla.rows; // rows

            for (int nrow = 0; nrow < row; nrow++)
            {
                lista.Add(Nmalla.matriz[nrow, col - 1]); // last column
            }

            return lista;

        }
        #endregion LAST COLUMN

        #region GRID CHANGE PARAMETER SHOWN
        // When changing the selecting parameter in the combobox, the grid is going to display the parameters choosen
        // for making that, the polygons have to actualise their colours
        private void DataGridComboBox_AS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DataGridComboBox_AS.SelectedIndex == 0) //temperature
            {
                // actualising polygons' colours
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[0], temperature_table_1, m2.rows + m3.rows, m2.columns + m3.columns, temperature_table_2);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[0], temperature_table_1, m2.rows + m3.rows, m2.columns + m3.columns, temperature_table_2);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[0], temperature_table_2, m4.rows + m5.rows, m4.columns + m5.columns, temperature_table_1);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[0], temperature_table_2, m4.rows + m5.rows, m4.columns + m5.columns, temperature_table_1);
            }
            if (DataGridComboBox_AS.SelectedIndex == 1) //u
            {
                // actualising polygons' colours
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[1], u_table_1, m2.rows + m3.rows, m2.columns + m3.columns, u_table_2);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[1], u_table_1, m2.rows + m3.rows, m2.columns + m3.columns, u_table_2);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[1], u_table_2, m4.rows + m5.rows, m4.columns + m5.columns, u_table_1);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[1], u_table_2, m4.rows + m5.rows, m4.columns + m5.columns, u_table_1);
            }
            if (DataGridComboBox_AS.SelectedIndex == 2) //v
            {
                // actualising polygons' colours
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[2], v_table_1, m2.rows + m3.rows, m2.columns + m3.columns, v_table_2);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[2],  v_table_1, m2.rows + m3.rows, m2.columns + m3.columns, v_table_2);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[2], v_table_2, m4.rows + m5.rows, m4.columns + m5.columns, v_table_1);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[2], v_table_2, m4.rows + m5.rows, m4.columns + m5.columns, v_table_1);
            }
            if (DataGridComboBox_AS.SelectedIndex == 3) //rho
            {
                // actualising polygons' colours
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[3], rho_table_1, m2.rows + m3.rows, m2.columns + m3.columns, rho_table_2);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[3],rho_table_1, m2.rows + m3.rows, m2.columns + m3.columns, rho_table_2);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[3],rho_table_2, m4.rows + m5.rows, m4.columns + m5.columns, rho_table_1);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[3],rho_table_2, m4.rows + m5.rows, m4.columns + m5.columns, rho_table_1);
            }
            if (DataGridComboBox_AS.SelectedIndex == 4) //p
            {
                // actualising polygons' colours
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[4], p_table_1, m2.rows + m3.rows, m2.columns + m3.columns, p_table_2);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[4],p_table_1, m2.rows + m3.rows, m2.columns + m3.columns, p_table_2);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[4],p_table_2, m4.rows + m5.rows, m4.columns + m5.columns, p_table_1);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[4], p_table_2, m4.rows + m5.rows, m4.columns + m5.columns, p_table_1);

            }
            if (DataGridComboBox_AS.SelectedIndex == 5) //Mach
            {
                // actualising polygons' colours
                casillas2 = GPG2.actualizar_colores_grid_AS(tablesM2[5], M_table_1, m2.rows + m3.rows, m2.columns + m3.columns, M_table_2);
                casillas3 = GPG3.actualizar_colores_grid_AS(tablesM3[5], M_table_1, m2.rows + m3.rows, m2.columns + m3.columns, M_table_2);
                casillas4 = GPG4.actualizar_colores_grid_AS(tablesM4[5], M_table_2, m4.rows + m5.rows, m4.columns + m5.columns, M_table_1);
                casillas5 = GPG5.actualizar_colores_grid_AS(tablesM5[5], M_table_2, m4.rows + m5.rows, m4.columns + m5.columns, M_table_1);

            }
        }
        #endregion GRID CHANGE PARAMETER SHOWN

        #region DATA TABLES MANIPULATIONS
        
        //JOIN DATATABLES
            // given two datatables, this function puts together the tables and return the resulting
            // datatable
        public DataTable JuntarTablas(DataTable dt1, DataTable dt2)
        {
            DataTable Unida = new DataTable(); // resulting datatable
            int dt1_rows = dt1.Rows.Count; // rows table 1, that are the same as table 2
            int dt1_columns = dt1.Columns.Count; // columns table 1
            int dt2_columns = dt2.Columns.Count; // columns table 2

            for (int i = 0; i < dt1_columns+dt2_columns; i++)
            {
                // new column
                DataColumn dc = new DataColumn();
                // add the column
                Unida.Columns.Add(dc);
            }

            for (int j = 0; j < dt1_rows; j++)
            {
                // new row
                DataRow dr = Unida.NewRow();
           
                for (int i = 0; i < dt1_columns + dt2_columns; i++)
                {
                    // adding the values
                    if (i < dt1_columns)
                    {
                        dr[i] = Convert.ToDouble(dt1.Rows[j][i].ToString());
                    }
                    else
                    {
                        dr[i] = Convert.ToDouble(dt2.Rows[j][i - dt1_columns].ToString());
                    }
                    
                }
                // adding the row
                Unida.Rows.Add(dr);
              
            }

            return Unida; // resulting datatable 

        }
        
        // SET THE HEADERS
            // Puting the name of the columns headers, starting at 1
        private void Set_headers(DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = Convert.ToString(i + 1);
            }
        }

        //LOAD TABLES BUTTON
            // When pressing the load tables button it is going to be displayed the values of the observing
            // parameters
        private void Load_tables_butt_Click(object sender, RoutedEventArgs e)
        {
            // hiding and setting visible the required elements
            #region VISIBLE/HIDDE
            labelt.Visibility = Visibility.Hidden;
            labelm.Visibility = Visibility.Hidden;
            labelp.Visibility = Visibility.Hidden;
            labelrho.Visibility = Visibility.Hidden;
            labelv.Visibility = Visibility.Hidden;
            labelu.Visibility = Visibility.Hidden;

            labelt2.Visibility = Visibility.Hidden;
            labelm2.Visibility = Visibility.Hidden;
            labelp2.Visibility = Visibility.Hidden;
            labelrho2.Visibility = Visibility.Hidden;
            labelv2.Visibility = Visibility.Hidden;
            labelu2.Visibility = Visibility.Hidden;

            t1_text.Visibility = Visibility.Hidden;
            m1_text.Visibility = Visibility.Hidden;
            p1_text.Visibility = Visibility.Hidden;
            rho1_text.Visibility = Visibility.Hidden;
            u1_text.Visibility = Visibility.Hidden;
            v1_text.Visibility = Visibility.Hidden;

            t2_text.Visibility = Visibility.Hidden;
            m2_text.Visibility = Visibility.Hidden;
            p2_text.Visibility = Visibility.Hidden;
            rho2_text.Visibility = Visibility.Hidden;
            u2_text.Visibility = Visibility.Hidden;
            v2_text.Visibility = Visibility.Hidden;

            Advanced_DataGridMalla.Visibility = Visibility.Visible;
            Advanced_DataGridMalla_2.Visibility = Visibility.Visible;
            #endregion VISIBLE/HIDDE

            // changing is enable functoins
            change_Mouse_butt.IsEnabled = true;
            Load_tables_butt.IsEnabled = true;
            // new timer to start the loading window
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer.Start();

        }
        
        // TIMER TICK FUNCTION
            //Each time the timer ticks the int timer increases its value. When it is 1 a loading window opens
            // then the tables are loaded and, once finished, the window of loading closes and the timer stops
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (timer == 1)
            {
                ld = new Loading(); // new loading window
                ld.Show(); // opening the window
            }
            if (timer == 2)
            {
                TablesLoaded(); // loading tables
            }
            if (timer == 3)
            {
                ld.Close(); // closing loading window
                timer = -1;
                dispatcherTimer.Stop(); // stoping the timer
            }
            timer = timer + 1;
        }

        // LOADED TABLES
            // In function of the parameter selected a table or other is going to be shown
        public void TablesLoaded()
        {

            if (DataGridComboBox_AS.SelectedIndex == 0) //temperature
            {
                Set_headers(temperature_table_1); // seting headers
                Set_headers(temperature_table_2);// seting headers

                Advanced_DataGridMalla.DataContext = temperature_table_1.DefaultView; 
                Advanced_DataGridMalla_2.DataContext = temperature_table_2.DefaultView;
            }
            if (DataGridComboBox_AS.SelectedIndex == 1) //u
            {
                Set_headers(u_table_1);// seting headers
                Set_headers(u_table_2);// seting headers

                Advanced_DataGridMalla.DataContext = u_table_1.DefaultView;
                Advanced_DataGridMalla_2.DataContext = u_table_2.DefaultView;
            }
            if (DataGridComboBox_AS.SelectedIndex == 2) //v
            {
                Set_headers(v_table_1);// seting headers
                Set_headers(v_table_2);// seting headers

                Advanced_DataGridMalla.DataContext = v_table_1.DefaultView;
                Advanced_DataGridMalla_2.DataContext = v_table_2.DefaultView;
            }
            if (DataGridComboBox_AS.SelectedIndex == 3) //rho
            {
                Set_headers(rho_table_1);// seting headers
                Set_headers(rho_table_2);// seting headers

                Advanced_DataGridMalla.DataContext = rho_table_1.DefaultView;
                Advanced_DataGridMalla_2.DataContext = rho_table_2.DefaultView;
            }
            if (DataGridComboBox_AS.SelectedIndex == 4) //p
            {
                Set_headers(p_table_1);// seting headers
                Set_headers(p_table_2);// seting headers

                Advanced_DataGridMalla.DataContext = p_table_1.DefaultView;
                Advanced_DataGridMalla_2.DataContext = p_table_2.DefaultView;
            }
            if (DataGridComboBox_AS.SelectedIndex == 5) //Mach
            {
                Set_headers(M_table_1);// seting headers
                Set_headers(M_table_2);// seting headers

                Advanced_DataGridMalla.DataContext = M_table_1.DefaultView;
                DataContext = M_table_2.DefaultView;
            }
        }

        #endregion DATA TABLES MANIPULATIONS

        #region WINDOW MANIPULATION FUNCTIONS
        // CLOSE BUTTON
            //When clicking to the top right button (red circle) the current window closes
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        // MINIMIZE BUTTON
            //When clicking to the top right button (yellow circle) the current window minimises
        private void Mini_Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        // DRAG MOVE
            //When the left button is pressed and draged, the window can be moved
        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion WINDOW MANIPULATION FUNCTIONS

        #region ROW HEADINGS
        //ROW HEADING OF THE DATAGRIDMALLA 1
            //Headers of the rows are set, starting at 1
        private void Advanced_DataGridMalla_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (Convert.ToDouble((e.Row.GetIndex()).ToString()) != m2.rows)
            {
                e.Row.Header = Convert.ToDouble((e.Row.GetIndex()).ToString()) + 1;
            }
            else
            {
                e.Row.Header = "";
            }
        }
        //ROW HEADING OF THE DATAGRIDMALLA 
            //Headers of the rows are set, starting at 1
        private void Advanced_DataGridMalla_2_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (Convert.ToDouble((e.Row.GetIndex()).ToString()) != m4.rows)
            {
                e.Row.Header = Convert.ToDouble((e.Row.GetIndex()).ToString()) + 1;
            }
            else
            {
                e.Row.Header = "";
            }
        }
        #endregion ROW HEADINGS

        #region MOUSE EVENTS

        // TABLES BUTTON
            // When changing to the tables, the textboxs and labels are going to be hidden and other
            // objects will be visible
        private void change_Mouse_butt_Click(object sender, RoutedEventArgs e)
        {
            // when changing to the tables some buttons and labels are going to be hiden
            change_Mouse_butt.IsEnabled = false;
            Load_tables_butt.IsEnabled = true;

            #region VISIBILITY
            labelt.Visibility = Visibility.Visible;
            labelm.Visibility = Visibility.Visible;
            labelp.Visibility = Visibility.Visible;
            labelrho.Visibility = Visibility.Visible;
            labelv.Visibility = Visibility.Visible;
            labelu.Visibility = Visibility.Visible;

            labelt2.Visibility = Visibility.Visible;
            labelm2.Visibility = Visibility.Visible;
            labelp2.Visibility = Visibility.Visible;
            labelrho2.Visibility = Visibility.Visible;
            labelv2.Visibility = Visibility.Visible;
            labelu2.Visibility = Visibility.Visible;

            t1_text.Visibility = Visibility.Visible;
            m1_text.Visibility = Visibility.Visible;
            p1_text.Visibility = Visibility.Visible;
            rho1_text.Visibility = Visibility.Visible;
            u1_text.Visibility = Visibility.Visible;
            v1_text.Visibility = Visibility.Visible;

            t2_text.Visibility = Visibility.Visible;
            m2_text.Visibility = Visibility.Visible;
            p2_text.Visibility = Visibility.Visible;
            rho2_text.Visibility = Visibility.Visible;
            u2_text.Visibility = Visibility.Visible;
            v2_text.Visibility = Visibility.Visible;

            Advanced_DataGridMalla.Visibility = Visibility.Hidden;
            Advanced_DataGridMalla_2.Visibility = Visibility.Hidden;
            #endregion VISIBILITY
        }

        // MOUSE ENTERING IN A POLYGON
            // When a mouse enterns in a polygon it is posible to observe the attributes of that 
            // polygon in the textboxes at the right side of the window
        public void polygon_enter2(object sender, EventArgs e)
        {
            if (FijarValor1 == false)
            {
                // We obtain the points that define the polygon we are in
                Polygon poly = (Polygon)sender;
                Point p0 = poly.Points[0];
                Point p1 = poly.Points[1];
                Point p2 = poly.Points[2];
                Point p3 = poly.Points[3];

                // we look for which of the polygons saved in CASILLAS corresponts the points
                for (int i = 0; i < m2.columns - 1; i++)
                {
                    for (int j = 0; j < m2.rows; j++)
                    {
                        Point p0_C = casillas2[j, i].Points[0];
                        Point p1_C = casillas2[j, i].Points[1];
                        Point p2_C = casillas2[j, i].Points[2];
                        Point p3_C = casillas2[j, i].Points[3];

                        if ((p0 == p0_C) && (p1 == p1_C) && (p2 == p2_C) && (p3 == p3_C))
                        {
                            // when we find the polygon with this points we show the corresponging labels
                            u1_text.Text = Convert.ToString(m2.matriz[m2.rows - 1 - j, i].u);
                            v1_text.Text = Convert.ToString(m2.matriz[m2.rows - 1 - j, i].v);
                            rho1_text.Text = Convert.ToString(m2.matriz[m2.rows - 1 - j, i].Rho);
                            p1_text.Text = Convert.ToString(m2.matriz[m2.rows - 1 - j, i].P);
                            t1_text.Text = Convert.ToString(m2.matriz[m2.rows - 1 - j, i].T);
                            m1_text.Text = Convert.ToString(m2.matriz[m2.rows - 1 - j, i].M);
                        }
                    }
                }
            }

        }
        // MOUSE ENTERING IN A POLYGON
            // When a mouse enterns in a polygon it is posible to observe the attributes of that 
            // polygon in the textboxes at the right side of the window
        public void polygon_enter3(object sender, EventArgs e)
        {
            if (FijarValor1 == false)
            {
                // We obtain the points that define the polygon we are in
                Polygon poly = (Polygon)sender;
                Point p0 = poly.Points[0];
                Point p1 = poly.Points[1];
                Point p2 = poly.Points[2];
                Point p3 = poly.Points[3];

                // we look for which of the polygons saved in CASILLAS corresponts the points
                for (int i = 0; i < m3.columns - 1; i++)
                {
                    for (int j = 0; j < m3.rows; j++)
                    {
                        Point p0_C = casillas3[j, i].Points[0];
                        Point p1_C = casillas3[j, i].Points[1];
                        Point p2_C = casillas3[j, i].Points[2];
                        Point p3_C = casillas3[j, i].Points[3];

                        if ((p0 == p0_C) && (p1 == p1_C) && (p2 == p2_C) && (p3 == p3_C))
                        {
                            // when we find the polygon with this points we show the corresponging labels
                            u1_text.Text = Convert.ToString(m3.matriz[m3.rows - 1 - j, i].u);
                            v1_text.Text = Convert.ToString(m3.matriz[m3.rows - 1 - j, i].v);
                            rho1_text.Text = Convert.ToString(m3.matriz[m3.rows - 1 - j, i].Rho);
                            p1_text.Text = Convert.ToString(m3.matriz[m3.rows - 1 - j, i].P);
                            t1_text.Text = Convert.ToString(m3.matriz[m3.rows - 1 - j, i].T);
                            m1_text.Text = Convert.ToString(m3.matriz[m3.rows - 1 - j, i].M);
                        }
                    }
                }


            }
        }
        // MOUSE ENTERING IN A POLYGON
            // When a mouse enterns in a polygon it is posible to observe the attributes of that 
            // polygon in the textboxes at the right side of the window
        public void polygon_enter4(object sender, EventArgs e)
        {
            if (FijarValor2 == false)
            {
                // We obtain the points that define the polygon we are in
                Polygon poly = (Polygon)sender;
                Point p0 = poly.Points[0];
                Point p1 = poly.Points[1];
                Point p2 = poly.Points[2];
                Point p3 = poly.Points[3];

                // we look for which of the polygons saved in CASILLAS corresponts the points
                for (int i = 0; i < m4.columns - 1; i++)
                {
                    for (int j = 0; j < m4.rows; j++)
                    {
                        Point p0_C = casillas4[j, i].Points[0];
                        Point p1_C = casillas4[j, i].Points[1];
                        Point p2_C = casillas4[j, i].Points[2];
                        Point p3_C = casillas4[j, i].Points[3];

                        if ((p0 == p0_C) && (p1 == p1_C) && (p2 == p2_C) && (p3 == p3_C))
                        {
                            // when we find the polygon with this points we show the corresponging labels
                            u2_text.Text = Convert.ToString(m4.matriz[m4.rows - 1 - j, i].u);
                            v2_text.Text = Convert.ToString(m4.matriz[m4.rows - 1 - j, i].v);
                            rho2_text.Text = Convert.ToString(m4.matriz[m4.rows - 1 - j, i].Rho);
                            p2_text.Text = Convert.ToString(m4.matriz[m4.rows - 1 - j, i].P);
                            t2_text.Text = Convert.ToString(m4.matriz[m4.rows - 1 - j, i].T);
                            m2_text.Text = Convert.ToString(m4.matriz[m4.rows - 1 - j, i].M);
                        }
                    }
                }

            }
        }
        // MOUSE ENTERING IN A POLYGON
            // When a mouse enterns in a polygon it is posible to observe the attributes of that 
            // polygon in the textboxes at the right side of the window
        public void polygon_enter5(object sender, EventArgs e)
        {
            if (FijarValor2 == false)
            {
                // We obtain the points that define the polygon we are in
                Polygon poly = (Polygon)sender;
                Point p0 = poly.Points[0];
                Point p1 = poly.Points[1];
                Point p2 = poly.Points[2];
                Point p3 = poly.Points[3];

                // we look for which of the polygons saved in CASILLAS corresponts the points
                for (int i = 0; i < m5.columns - 1; i++)
                {
                    for (int j = 0; j < m5.rows; j++)
                    {
                        Point p0_C = casillas5[j, i].Points[0];
                        Point p1_C = casillas5[j, i].Points[1];
                        Point p2_C = casillas5[j, i].Points[2];
                        Point p3_C = casillas5[j, i].Points[3];

                        if ((p0 == p0_C) && (p1 == p1_C) && (p2 == p2_C) && (p3 == p3_C))
                        {
                            // when we find the polygon with this points we show the corresponging labels
                            u2_text.Text = Convert.ToString(m5.matriz[m5.rows - 1 - j, i].u);
                            v2_text.Text = Convert.ToString(m5.matriz[m5.rows - 1 - j, i].v);
                            rho2_text.Text = Convert.ToString(m5.matriz[m5.rows - 1 - j, i].Rho);
                            p2_text.Text = Convert.ToString(m5.matriz[m5.rows - 1 - j, i].P);
                            t2_text.Text = Convert.ToString(m5.matriz[m5.rows - 1 - j, i].T);
                            m2_text.Text = Convert.ToString(m5.matriz[m5.rows - 1 - j, i].M);
                        }
                    }
                }

            }
        }
        // RIGHT BUTTON ON A POLYGON
            // When the right button is press inside a polygon, the values shown in the textboxs is 
            // going to be fixed. Giving the information of that specific cell
        public void right_button2(object sender, EventArgs e)
        {
            FijarValor2 = true;
        }
        // RIGHT BUTTON ON A POLYGON
            // When the right button is press inside a polygon, the values shown in the textboxs is 
            // going to be fixed. Giving the information of that specific cell
        public void right_button1(object sender, EventArgs e)
        {
            FijarValor1 = true;
        }
        // LEFT BUTTON ON A POLYGON
            // When the left button is pressed the fixed value is liberated
        public void left_button1(object sender, EventArgs e)
        {
            FijarValor1 = false;
        }
        // LEFT BUTTON ON A POLYGON
            // When the left button is pressed the fixed value is liberated
        public void left_button2(object sender, EventArgs e)
        {
            FijarValor2 = false;
        }
        #endregion MOUSE EVENTS
    }


}

