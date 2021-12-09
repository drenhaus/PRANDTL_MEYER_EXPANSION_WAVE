﻿using System;
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

        double delta_y_t;

        

        Polygon[,] casillas;
        GridPlotGenerate GPG = new GridPlotGenerate();
        

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
            SeeGrounfOf("NONE");
        }

        // SIMULATE BUTTON CLICK
            //When clicking the simulation button the needed parameters are set and 
            // the functions that compute the simulation in the Malla are initialized        
        private void Simulate_Button_Click(object sender, RoutedEventArgs e)
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
            casillas = GPG.GenerateGridPlot(filas, columnas,m); 

            // We add the casillas values into the grid
            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    GridMalla.Children.Add(casillas[j, i]);
                }
            }

            MessageBox.Show("Please select the data you want to show or reset for any change");
        }

        private void DataGridComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridComboBox.SelectedIndex == 0) //temperature
            {
                casillas=GPG.actualizar_colores_grid(temperature_table, 255, 0, 0);
            }
            if (DataGridComboBox.SelectedIndex == 1) //u
            {
                casillas = GPG.actualizar_colores_grid(u_table, 0, 255, 0);
            }
            if (DataGridComboBox.SelectedIndex == 2) //v
            {
                casillas = GPG.actualizar_colores_grid(v_table, 255, 128, 0);
            }
            if (DataGridComboBox.SelectedIndex == 3) //rho
            {
                casillas = GPG.actualizar_colores_grid(rho_table, 18, 184, 255);
            }
            if (DataGridComboBox.SelectedIndex == 4) //p
            {
                casillas = GPG.actualizar_colores_grid(p_table, 255, 0, 127);
            }
            if (DataGridComboBox.SelectedIndex == 5) //Mach
            {
                casillas = GPG.actualizar_colores_grid(M_table, 255, 255, 255);
            }

        }

        private void ComparationButton_Click(object sender, RoutedEventArgs e)
        {
            AndersonComparationWindow anderson_w = new AndersonComparationWindow();
            anderson_w.m = this.m;
            anderson_w.p = PresitionComboBox.SelectedIndex;
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

        private void LoadPresitionButton_Click(object sender, RoutedEventArgs e)
        {
            if (PresitionComboBox.SelectedIndex == 0) //small
            {
                columnas = 24;
                filas = 11;
                delta_y_t = 0.1;

            }
            if (PresitionComboBox.SelectedIndex == 1) // normal
            {
                columnas = 92;
                filas = 41;
                delta_y_t = 0.025;

            }
            if (PresitionComboBox.SelectedIndex == 2) //high
            {
                columnas = 452;
                filas = 201;
                delta_y_t = 0.005;

            }
            LoadParametersButton.IsEnabled = true;
            MessageBox.Show("Precision selected successfully");



        }

        private void polygon_enter(object sender, EventArgs e)
        {
            Polygon poly = (Polygon)sender;
           

            int i = 0;
            int j = 0;
            

            for (i = 0; i < columnas - 1; i++)
            {
                for (j = 0; j < filas; j++)
                {
                    if (poly == casillas[j, i])
                    {
                      
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

        private void PresitionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PresitionComboBox.SelectedIndex == 0)
            {
                SeeGrounfOf("LOW");
            }
            if (PresitionComboBox.SelectedIndex == 1)
            {
                SeeGrounfOf("MEDIUM");
            }
        }



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
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }


        // NEW WINDOW: WELCOME
        // When clicking to the menu BACK TO START the simulation window closes and it is opened
        // again the welcome to the simulator windows
        private void WelcomeButton_Click(object sender, RoutedEventArgs e)
        {
            WelcomeWindow ww = new WelcomeWindow();
            ww.Show();
            Close();
        }

        // NEW WINDOW: TABLES
        //When clicking to the menu option TABLES a new window is opened showing the information tables
        // It is necessary to transfer the information of the tables from the main simulation window to
        // the tables window
        private void TablesButton_Click(object sender, RoutedEventArgs e)
        {
            TablesWindow tables_w = new TablesWindow();
            tables_w.SetTables(temperature_table, u_table, v_table, rho_table, p_table, M_table, F1_table, F2_table, F3_table, F4_table);
            tables_w.Show();
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
    }
}
