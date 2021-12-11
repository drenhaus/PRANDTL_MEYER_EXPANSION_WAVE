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
using System.Windows.Threading;
using System.IO;
using Microsoft.Win32;

namespace WPFapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region ATTRIBUTES
        // we create a new Malla
        Malla m = new Malla();
        // we set by default 89 columns and 41 rows, it may be changed if we select a different precision
        int columnas = 89;
        int filas = 41;
        double delta_y_t;

        bool isLOADING = false;

        // we create a matrix of Polygons that are going to be in the gried view
        Polygon[,] casillas;
        // where all the functions related to the polygons creation are found
        GridPlotGenerate GPG = new GridPlotGenerate();

        //DataTables with the solutions 
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

        // We define a dispatcher timer and a inter that are going to be used for the loading advice
        DispatcherTimer dispatcherTimer;
        DispatcherTimer dispatcherTimer2;
        int timer = 0;
        int timer2 = 0;
        // when loading takes too land a new windoe Loading ld will open
        Loading ld;

        #endregion ATTRIBUTES

        public MainWindow()
        {
            InitializeComponent();
            SeeGrounfOf("NONE"); // initially no ground is visible
        }

        #region SIMULATION FUNCTION
        //When the simulation function is called the needed parameters are set and 
        // the functions that compute the simulation in the Malla are initialized        
        private void Simulate()
        {
            // We define the rows, columns and the delta_y_t depending of the precision
            // that had been selected
            m.rows = filas;
            m.columns = columnas;
            m.delta_y_t = this.delta_y_t;

            // We create the matrix, compute the simulation and fill the tables using the
            // functions of the Malla clas
            m.DefinirMatriz();
            m.Compute();
            m.Fill_DataTable();

            // We get the tables that we had computed in the previous step and save them as an
            // array of DataTables
            DataTable[] T_U_V_RHO_P_M_F1_F2_F3_F4 = m.GetTables();

            //We define each Datble as the corresponding one of the array 
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

            //We define the visual grid and call the function that generates them of the class GridPlotGenerate
            casillas = GPG.GenerateGridPlot(filas, columnas, m);

            // We add the casillas values into the grid
            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    casillas[j, i].MouseEnter += polygon_enter;
                    GridMalla.Children.Add(casillas[j, i]);
                }
            }
            if (isLOADING) { }

            else
            {
                MessageBox.Show("Please select the data you want to show or reset for any change");
            }

        }

        // DISPATCHER TIMER TICK FOR Simulation()
        // When the timer is initialized a new window loading is opened during the long process
        // is executed. When this process finishes the loading window closes and the timer is stoped
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (timer == 1)
            {
                ld = new Loading();
                ld.Show();
            }
            if (timer == 2)
            {
                Simulate();
            }
            if (timer == 3)
            {
                ld.Close();
                timer = -1;
                dispatcherTimer.Stop();
            }
            timer = timer + 1;
        }

        // DISPATCHER TIMER TICK FOR DataChangedComboBox
        // When the timer is initialized a new window loading is opened during the long process
        // is executed. When this process finishes the loading window closes and the timer is stoped
        private void dispatcherTimer_Tick2(object sender, EventArgs e)
        {
            if (timer2 == 1)
            {
                ld = new Loading();
                ld.Show();
            }
            if (timer2 == 2)
            {
                DataChangedComboBox();
            }
            if (timer2 == 3)
            {
                ld.Close();
                timer2 = -1;
                dispatcherTimer2.Stop();
            }
            timer2 = timer2 + 1;
        }

        #endregion SIMULATION FUNCTION

        #region SIMULATION WPF CONTROLS

        // FUNCTION CALLED WHEN CHANGING THE SELECTED INDEX OF THE DATA VIEW COMBOBOX
        // Depending the index selected, the polygons saved in CASILLAS are changed their color
        // according to the data displayed
        private void DataChangedComboBox()
        {
            if (DataGridComboBox.SelectedIndex == 0) // if temperature selected
            {
                casillas=GPG.actualizar_colores_grid(temperature_table, 255, 0, 0,false);
            }
            if (DataGridComboBox.SelectedIndex == 1) // if u selected
            {
                casillas = GPG.actualizar_colores_grid(u_table, 0, 255, 0,false);
            }
            if (DataGridComboBox.SelectedIndex == 2) //if v selected
            {
                casillas = GPG.actualizar_colores_grid(v_table, 255, 128, 0,true);
            }
            if (DataGridComboBox.SelectedIndex == 3) //if rho selected
            {
                casillas = GPG.actualizar_colores_grid(rho_table, 18, 184, 255,false);
            }
            if (DataGridComboBox.SelectedIndex == 4) //if p selected
            {
                casillas = GPG.actualizar_colores_grid(p_table, 255, 0, 127,false);
            }
            if (DataGridComboBox.SelectedIndex == 5) //if Mach selected
            {
                casillas = GPG.actualizar_colores_grid(M_table, 255, 255, 255,false);
            }

        }

        // DEPENDING OF THE PRECISION A LOADING ADVICE WILL APPEAR
        //if the selected index of the precision is 2 (high presition) a loading advice is going
        // to be shown
        private void DataGridComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PresitionComboBox.SelectedIndex == 2)
            {
                dispatcherTimer2 = new DispatcherTimer();
                dispatcherTimer2.Tick += new EventHandler(dispatcherTimer_Tick2);
                dispatcherTimer2.Interval = TimeSpan.FromMilliseconds(100);
                dispatcherTimer2.Start();
            }
            else
            {
                DataChangedComboBox();
            }

        }

        // CHECKBOX OF DEFAULT PARAMETERS CHECKED
        // if the checkbox is selected it is written the parameters by default
        private void CheckBox_A_Checked(object sender, RoutedEventArgs e)
        {
            // we write the parameters
            v_TextBox.Text = Convert.ToString(0.0);
            Rho_TextBox.Text = Convert.ToString(1.23);
            P_TextBox.Text = Convert.ToString(101000);
            M_TextBox.Text = Convert.ToString(2.0);
            T_TextBox.Text = Convert.ToString(286.1);

        }

        // LOAD PARAMETERS BUTTON
        // When clicking to load the writen parameters are going to be set as the atributes of the 
        // NORMA class where we find the parameter conditions of the simulation
        private void LoadParametersButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // If we input a . replece it by a ,
                if (v_TextBox.Text.Contains("."))
                {
                    v_TextBox.Text = v_TextBox.Text.Replace(".", ",");
                }
                if (Rho_TextBox.Text.Contains("."))
                {
                    Rho_TextBox.Text = Rho_TextBox.Text.Replace(".", ",");
                }
                if (P_TextBox.Text.Contains("."))
                {
                    P_TextBox.Text = P_TextBox.Text.Replace(".", ",");
                }
                if (M_TextBox.Text.Contains("."))
                {
                    M_TextBox.Text = M_TextBox.Text.Replace(".", ",");
                }
                if (T_TextBox.Text.Contains("."))
                {
                    T_TextBox.Text = T_TextBox.Text.Replace(".", ",");
                }
                // initial data line parameters transfered to NORMA clas of the Malla we are working
                m.norma.v_in = Convert.ToDouble(v_TextBox.Text);
                m.norma.Rho_in = Convert.ToDouble(Rho_TextBox.Text);
                m.norma.P_in = Convert.ToDouble(P_TextBox.Text);
                m.norma.M_in = Convert.ToDouble(M_TextBox.Text);
                m.norma.T_in = Convert.ToDouble(T_TextBox.Text);

                //checking if the data introduced fulfill the equation of state
                double rho_es = m.norma.P_in / (m.norma.R_air * m.norma.T_in);

                if (Math.Round(Convert.ToDecimal(rho_es), 2) != Math.Round(Convert.ToDecimal(m.norma.Rho_in), 2))
                {
                    // if it is not fulfilled a messagebox appears giving the option to continue or change the parameters
                    //MessageBox.Show("The introduced parameters do not fulfill the equation state. Do you want to continue without changing the parameters?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (MessageBox.Show("The introduced parameters do not fulfill the equation stateDo you want to continue without changing the parameters?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        M_TextBox.Text = "";
                        v_TextBox.Text = "";
                        P_TextBox.Text = "";
                        Rho_TextBox.Text = "";
                        T_TextBox.Text = "";
                    }
                    else
                    {
                        m.norma.Compute_a();
                        m.norma.Compute_M_angle();
                        m.norma.Compute_u();

                        // only show the loading advice when we select the higher precision
                        if (PresitionComboBox.SelectedIndex == 2)
                        {
                            dispatcherTimer = new DispatcherTimer();
                            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
                            dispatcherTimer.Start();
                        }

                        else
                        { Simulate(); }

                #region ENABLE/DISABLE BUTTONS
                // The is enable is changed to continue with the simulation
                PresitionComboBox.IsEnabled = false;
                CheckBox_A.IsEnabled = false;
                LoadParametersButton.IsEnabled = false;
                DataGridComboBox.IsEnabled = true;
                SaveSim_button.IsEnabled = true;
                Reset_button.IsEnabled = true;
                T_TextBox.IsEnabled = false;
                Rho_TextBox.IsEnabled = false;
                P_TextBox.IsEnabled = false;
                M_TextBox.IsEnabled = false;
                v_TextBox.IsEnabled = false;
                #endregion ENABLE/ DISABLE BUTTONS

                    } 
                }
                else
                {

                    m.norma.Compute_a();
                    m.norma.Compute_M_angle();
                    m.norma.Compute_u();

                    // only show the loading advice when we select the higher precision
                    if (PresitionComboBox.SelectedIndex == 2)
                    {
                        dispatcherTimer = new DispatcherTimer();
                        dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                        dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
                        dispatcherTimer.Start();
                    }

                    else
                    { Simulate(); }


                #region ENABLE/DISABLE BUTTONS
                // The is enable is changed to continue with the simulation
                PresitionComboBox.IsEnabled = false;
                CheckBox_A.IsEnabled = false;
                LoadParametersButton.IsEnabled = false;
                DataGridComboBox.IsEnabled = true;
                SaveSim_button.IsEnabled = true;
                Reset_button.IsEnabled = true;
                T_TextBox.IsEnabled = false;
                Rho_TextBox.IsEnabled = false;
                P_TextBox.IsEnabled = false;
                M_TextBox.IsEnabled = false;
                v_TextBox.IsEnabled = false;
                #endregion ENABLE/ DISABLE BUTTONS
                }
                }

            catch (Exception ex)
            {
                // MessageBox to indicate the parameters had not been correctly defined
                MessageBox.Show(ex.Message);
            }
        }

        // RESET BUTTON
        //If some simulation has been executed but the user wants to make some change in the parameters or
        // redo the simulation, the reset button creates a new Malla and empties all the DataTables and grid
        private void Reset_button_Click(object sender, RoutedEventArgs e)
        {
            // new Malla created
            m = new Malla();
            //empty casillas
            casillas = null;
            // empty tables
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

            //empty grid
            GridMalla.Children.Clear();
            // no selected item
            DataGridComboBox.SelectedItem = null;
            PresitionComboBox.SelectedItem = null;
            // checkbox not selected and labels empty
            CheckBox_A.IsChecked = false;
            T_TextBox.Text = "";
            Rho_TextBox.Text = "";
            P_TextBox.Text = "";
            M_TextBox.Text = "";
            v_TextBox.Text = "";

            // is enable of buttons fals
            DataGridComboBox.IsEnabled = false;
            Reset_button.IsEnabled = false;
            LoadParametersButton.IsEnabled = false;
            SaveSim_button.IsEnabled = false;
            LoadSim_button.IsEnabled = true;
            CheckBox_A.IsEnabled = false;
            PresitionComboBox.IsEnabled = true;
            T_TextBox.IsEnabled = false;
            Rho_TextBox.IsEnabled = false;
            P_TextBox.IsEnabled = false;
            M_TextBox.IsEnabled = false;
            v_TextBox.IsEnabled = false;

        }

        public bool AreDefautParametersLoaded()
        {
            if (m.norma.v_in == 0.0 && m.norma.Rho_in == 1.23 && m.norma.P_in == 101000 && m.norma.M_in == 2.0 && m.norma.T_in == 286.1)
            { return true; }
            else
            { return false; }
        }

        //WHRN THE MOUSE ENTER INSIDE A POLYGON
            // When the mouse is seted inside a polygon, a group of labels appear showing the properties
            // of the polygon: temperature, pressure, density ,etc.
        public void polygon_enter(object sender, EventArgs e)
        {
            // We obtain the points that define the polygon we are in
            Polygon poly = (Polygon)sender;
            Point p0 = poly.Points[0];
            Point p1= poly.Points[1];
            Point p2 = poly.Points[2];
            Point p3 = poly.Points[3];
              
            // we look for which of the polygons saved in CASILLAS corresponts the points
                for (int i = 0; i < columnas - 1; i++)
                {
                    for (int j = 0; j < filas; j++)
                    {
                        Point p0_C = casillas[j, i].Points[0];
                        Point p1_C = casillas[j, i].Points[1];
                        Point p2_C = casillas[j, i].Points[2];
                        Point p3_C = casillas[j, i].Points[3];

                        if ((p0 == p0_C) && (p1 == p1_C) && (p2 == p2_C) && (p3 == p3_C))
                        {
                            // when we find the polygon with this points we show the corresponging labels
                            u_text.Text = Convert.ToString(m.matriz[filas - 1 - j, i].u);
                            v_text.Text = Convert.ToString(m.matriz[filas - 1 - j, i].v);
                            rho_text.Text= Convert.ToString(m.matriz[filas - 1 - j, i].Rho);
                            p_text.Text = Convert.ToString(m.matriz[filas - 1 - j, i].P);
                            t_text.Text = Convert.ToString(m.matriz[filas - 1 - j, i].T);
                            m_text.Text = Convert.ToString(m.matriz[filas - 1 - j, i].M);
                        }
                    }
                }
            
        }


        // SEE GROUND 
        // This function sets if the ground of the simulation is visible or hidden and if is visible which configuration
        // low, medium or high
        private void SeeGrounfOf(string str)
        {
            if (str == "LOW")
            {
                low_gnd1.Visibility = Visibility.Visible;
                low_gnd2.Visibility = Visibility.Visible;
                low1.Visibility = Visibility.Visible;
                low2.Visibility = Visibility.Visible;
                low3.Visibility = Visibility.Visible;
                low4.Visibility = Visibility.Visible;
                low5.Visibility = Visibility.Visible;
                low6.Visibility = Visibility.Visible;
                low7.Visibility = Visibility.Visible;
                low8.Visibility = Visibility.Visible;

                med_gnd1.Visibility = Visibility.Hidden;
                med_gnd2.Visibility = Visibility.Hidden;
                med1.Visibility = Visibility.Hidden;
                med2.Visibility = Visibility.Hidden;
                med3.Visibility = Visibility.Hidden;
                med4.Visibility = Visibility.Hidden;
                med5.Visibility = Visibility.Hidden;
                med6.Visibility = Visibility.Hidden;
                med7.Visibility = Visibility.Hidden;
                med8.Visibility = Visibility.Hidden;

            }
            if (str == "MEDIUM")
            {
                low_gnd1.Visibility = Visibility.Hidden;
                low_gnd2.Visibility = Visibility.Hidden;
                low1.Visibility = Visibility.Hidden;
                low2.Visibility = Visibility.Hidden;
                low3.Visibility = Visibility.Hidden;
                low4.Visibility = Visibility.Hidden;
                low5.Visibility = Visibility.Hidden;
                low6.Visibility = Visibility.Hidden;
                low7.Visibility = Visibility.Hidden;
                low8.Visibility = Visibility.Hidden;

                med_gnd1.Visibility = Visibility.Visible;
                med_gnd2.Visibility = Visibility.Visible;
                med1.Visibility = Visibility.Visible;
                med2.Visibility = Visibility.Visible;
                med3.Visibility = Visibility.Visible;
                med4.Visibility = Visibility.Visible;
                med5.Visibility = Visibility.Visible;
                med6.Visibility = Visibility.Visible;
                med7.Visibility = Visibility.Visible;
                med8.Visibility = Visibility.Visible;

            }
            if (str == "HIGH")
            {
                low_gnd1.Visibility = Visibility.Hidden;
                low_gnd2.Visibility = Visibility.Hidden;
                low1.Visibility = Visibility.Hidden;
                low2.Visibility = Visibility.Hidden;
                low3.Visibility = Visibility.Hidden;
                low4.Visibility = Visibility.Hidden;
                low5.Visibility = Visibility.Hidden;
                low6.Visibility = Visibility.Hidden;
                low7.Visibility = Visibility.Hidden;
                low8.Visibility = Visibility.Hidden;

                med_gnd1.Visibility = Visibility.Visible;
                med_gnd2.Visibility = Visibility.Visible;
                med1.Visibility = Visibility.Visible;
                med2.Visibility = Visibility.Visible;
                med3.Visibility = Visibility.Visible;
                med4.Visibility = Visibility.Visible;
                med5.Visibility = Visibility.Visible;
                med6.Visibility = Visibility.Visible;
                med7.Visibility = Visibility.Visible;
                med8.Visibility = Visibility.Visible;
            }
            if (str == "NONE")
            {
                low_gnd1.Visibility = Visibility.Hidden;
                low_gnd2.Visibility = Visibility.Hidden;
                low1.Visibility = Visibility.Hidden;
                low2.Visibility = Visibility.Hidden;
                low3.Visibility = Visibility.Hidden;
                low4.Visibility = Visibility.Hidden;
                low5.Visibility = Visibility.Hidden;
                low6.Visibility = Visibility.Hidden;
                low7.Visibility = Visibility.Hidden;
                low8.Visibility = Visibility.Hidden;

                med_gnd1.Visibility = Visibility.Hidden;
                med_gnd2.Visibility = Visibility.Hidden;
                med1.Visibility = Visibility.Hidden;
                med2.Visibility = Visibility.Hidden;
                med3.Visibility = Visibility.Hidden;
                med4.Visibility = Visibility.Hidden;
                med5.Visibility = Visibility.Hidden;
                med6.Visibility = Visibility.Hidden;
                med7.Visibility = Visibility.Hidden;
                med8.Visibility = Visibility.Hidden;

            }

        }

        // CHANGING THE PRECISION INDEX
        // Depending of the precision selected in the Combobox the dimensions of delta_y_t, rows and columns are
        // higher or lower and thus is more accurate or not and the ground is visible
        private void PresitionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (PresitionComboBox.SelectedIndex == 0) //low precision
                {
                    SeeGrounfOf("LOW");
                    columnas = 24;
                    filas = 11;
                    delta_y_t = 0.1;
                }
                if (PresitionComboBox.SelectedIndex == 1) // normal precision
                {
                    SeeGrounfOf("MEDIUM");
                    columnas = 92;
                    filas = 41;
                    delta_y_t = 0.025;
                }
                if (PresitionComboBox.SelectedIndex == 2) //high precision
                {
                    columnas = 452;
                    filas = 201;
                    delta_y_t = 0.005;                
                }


                // we change the isenable of buttons and elements to continue with the simulation
                LoadParametersButton.IsEnabled = true;
                CheckBox_A.IsEnabled = true;
                T_TextBox.IsEnabled = true;
                Rho_TextBox.IsEnabled = true;
                P_TextBox.IsEnabled = true;
                M_TextBox.IsEnabled = true;
                v_TextBox.IsEnabled = true;

                if (isLOADING)
                { }
                else
                {
                    // Messagebox to show the presicion has been selected
                    MessageBox.Show("Precision selected successfully");
                }
            }
            catch (Exception ex)
            {
                // MessageBox to indicate that the precision has not been set well
                MessageBox.Show(ex.Message);
            }
        }


        #endregion SIMULATION WPF CONTROLS

        #region SAVE/LOAD SIMULATION
        public int GuardarSimulacion(string nombre)
        {
            try
            {
                StreamWriter w = new StreamWriter(nombre);

                w.Write(PresitionComboBox.SelectedIndex);
                w.Write('\n');
                w.Write(m.norma.v_in + " " + m.norma.Rho_in + " " + m.norma.P_in + " " + m.norma.M_in + " " + m.norma.T_in);
                w.Close();
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public Malla CargarSimulacion(string name)
        {
            //Malla m = new Malla();
            isLOADING = true;

            StreamReader sr = new StreamReader(name);
            string linea = sr.ReadLine();

            #region PRESITION OF SAVED FILE
            if (Convert.ToInt16(linea) == 0)
            { 
                SeeGrounfOf("LOW");
                columnas = 24;
                filas = 11;
                delta_y_t = 0.1;
                PresitionComboBox.SelectedIndex = 0;
            }
            if (Convert.ToInt16(linea) == 1)
            {
                SeeGrounfOf("MEDIUM");
                columnas = 92;
                filas = 41;
                delta_y_t = 0.025;
                PresitionComboBox.SelectedIndex = 1;
            }
            if (Convert.ToInt16(linea) == 2)
            {
                SeeGrounfOf("MEDIUM");
                columnas = 452;
                filas = 201;
                delta_y_t = 0.005;
                PresitionComboBox.SelectedIndex = 2;
            }
            
            #endregion PRESITION OF SAVED FILE

            LoadParametersButton.IsEnabled = true;
            CheckBox_A.IsEnabled = true;
            T_TextBox.IsEnabled = true;
            Rho_TextBox.IsEnabled = true;
            P_TextBox.IsEnabled = true;
            M_TextBox.IsEnabled = true;
            v_TextBox.IsEnabled = true;
            SaveSim_button.IsEnabled = true;
            Reset_button.IsEnabled = true;


            string lineaParam = sr.ReadLine();
            string[] trozosParam = lineaParam.Split(' ');

            m.norma.v_in = Convert.ToDouble(trozosParam[0]);
            m.norma.Rho_in = Convert.ToDouble(trozosParam[1]);
            m.norma.P_in = Convert.ToDouble(trozosParam[2]);
            m.norma.M_in = Convert.ToDouble(trozosParam[3]);
            m.norma.T_in = Convert.ToDouble(trozosParam[4]);

            m.norma.Compute_a();
            m.norma.Compute_M_angle();
            m.norma.Compute_u();

            v_TextBox.Text = Convert.ToString(trozosParam[0]);
            Rho_TextBox.Text = Convert.ToString(trozosParam[1]);
            P_TextBox.Text = Convert.ToString(trozosParam[2]);
            M_TextBox.Text = Convert.ToString(trozosParam[3]);
            T_TextBox.Text = Convert.ToString(trozosParam[4]);

            if (Convert.ToInt16(linea) == 2)
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
                dispatcherTimer.Start();
            }

            else
            { Simulate(); }

            isLOADING = false;
            sr.Close();
            return m;
            
        }
        private void SaveSim_button_Click(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Simulación"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                int n = GuardarSimulacion(filename);
                if (n == 0)
                { MessageBox.Show("Simulación guardada correctamente!"); }
                else
                { MessageBox.Show("No ha sido posible guardar la simulación"); }
            }
            else
            { MessageBox.Show("No ha sido posible guardar la simulación"); }
        }
        private void LoadSim_button_Click(object sender, RoutedEventArgs e)
        {
            #region CLEAR ALL
            // new Malla created
            m = new Malla();
            //empty casillas
            casillas = null;
            // empty tables
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

            //empty grid
            GridMalla.Children.Clear();
            // no selected item
            DataGridComboBox.SelectedItem = null;
            PresitionComboBox.SelectedItem = null;
            // checkbox not selected and labels empty
            CheckBox_A.IsChecked = false;
            T_TextBox.Text = "";
            Rho_TextBox.Text = "";
            P_TextBox.Text = "";
            M_TextBox.Text = "";
            v_TextBox.Text = "";

            //// is enable of buttons fals
            //DataGridComboBox.IsEnabled = false;
            //Reset_button.IsEnabled = false;
            //LoadParametersButton.IsEnabled = false;
            //SaveSim_button.IsEnabled = false;
            //LoadSim_button.IsEnabled = false;
            //CheckBox_A.IsEnabled = false;
            //PresitionComboBox.IsEnabled = true;
            //T_TextBox.IsEnabled = false;
            //Rho_TextBox.IsEnabled = false;
            //P_TextBox.IsEnabled = false;
            //M_TextBox.IsEnabled = false;
            //v_TextBox.IsEnabled = false;

            #endregion CLEAR ALL

            #region LOAD FILE

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                ofd.Filter = "Text documents (.txt)|*.txt";
                Nullable<bool> result = ofd.ShowDialog();

                if (result == true)
                {
                    // Cargar documento
                    string filename = ofd.FileName;
                    m = CargarSimulacion(filename);
                    DataGridComboBox.IsEnabled = true;
                    MessageBox.Show("Please select the data you want to show or reset for any change");
                }
                else
                { MessageBox.Show("No ha sido posible cargar la simulación"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            #endregion LOAD FILE

        }

        #endregion SAVE/LOAD SIMULATION

        #region LEFT BUTTONS (OPEN TABLES, COMPARATIONS, GRAPHICS AND ADVANCED STUDY)
        // NEW WINDOW: WELCOME
        // When clicking to the menu BACK TO START the simulation window closes and it is opened
        // again the welcome to the simulator windows
        private void WelcomeButton_Click(object sender, RoutedEventArgs e)
        {
            WelcomeWindow ww = new WelcomeWindow();
            ww.Show();
            Close();
        }

        //NEW WINDOW: COMPARE WITH ANDERSON
            //When clicking to the menu COMPARING WITH ANDERSON a new window is opened allowing comparing
            // the results obtained with the Anderson results
        private void ComparationButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreDefautParametersLoaded())
            {
                AndersonComparationWindow anderson_w = new AndersonComparationWindow();

                // We set the precision and the Malla of the new window as the one simulated in the main Simulation
                anderson_w.m = this.m;
                anderson_w.p = PresitionComboBox.SelectedIndex;
                anderson_w.Show();
            }
            else
            {
                MessageBox.Show("Please to compare with Anderson's results use the defaud parameters");
            }
            

        }

        // NEW WINDOW: TABLES
        //When clicking to the menu option TABLES a new window is opened showing the information tables
        // It is necessary to transfer the information of the tables from the main simulation window to
        // the tables window
        private void TablesButton_Click(object sender, RoutedEventArgs e)
        {
            if (temperature_table != null)
            {
                TablesWindow tables_w = new TablesWindow();
                tables_w.SetTables(temperature_table, u_table, v_table, rho_table, p_table, M_table, F1_table, F2_table, F3_table, F4_table);
                tables_w.Show();
            }
            else
                MessageBox.Show("Please make a simulation to acces tables");
            
        }

        // NEW WINDOW: ADVANCED STUDY
            // When clicking to the menu option ADVANCED STUDY a new window is opened showing the studied carried
        private void AdvancedStudyButton_Click(object sender, RoutedEventArgs e)
        {
            AdvancedStudyWindow ad_w = new AdvancedStudyWindow();
            ad_w.Show();
        }

        // NEW WINDOW: GRAPHIC
            // When clicking to the menu option GRAPHICS a new window is opened showing the graphics
            //Before that it is required the list of the parameters, defined in Malla clas and some 
            // parameters have to be set for making the plots: columns, parameters, the values of P,T,u, etc
        private void GraficButton_Click(object sender, RoutedEventArgs e)
        {
            Graphics gr = new Graphics();

            // We call the Malla function CrearListade and create the lists for x, T, M, Rho, P, u, v
            m.CrearListade("x");
            m.CrearListade("T");
            m.CrearListade("M");
            m.CrearListade("Rho");
            m.CrearListade("P");
            m.CrearListade("u");
            m.CrearListade("v");

            gr.Show(); // Showing the new window

            // We set diferent parameters needed in the new window
            gr.SetnumdeCOLUMNAS(m.columns); // Set of the number of columns
            gr.valorMaximdeX = m.norma.L; // Set of the L parameter
            gr.listaDeXColumna = m.listaDeXColumna; // Set of the X values
            gr.listaTEMPxColumna = m.listaTemperaturaxColumna; //Set of the T
            gr.listaMachxColumna = m.listaMachxColumna; //Set of the Mach
            gr.listaDensidadxColumna = m.listaDensidadxColumna; // Set of the Density
            gr.listaPresurexColumna = m.listaPresurexColumna; // Set of the Pressure
            gr.listaUxColumna = m.listaU_velxColumna; // Set of the u
            gr.listaVxColumna = m.listaV_velxColumna; //Set of the v

        }
        #endregion LEFT BUTTONS (OPEN TABLES, COMPARATIONS, GRAPHICS AND ADVANCED STUDY)

        #region WINDOW MANIPULATION FUNCTIONS
        // CLOSE BUTTON
        //When clicking to the top right button (red circle) the current window closes
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // MINIMIZE BUTTON
        //When clicking to the top right button (yellow circle) the current window minimises
        private void MiniButton_Click(object sender, RoutedEventArgs e)
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


    }
}
