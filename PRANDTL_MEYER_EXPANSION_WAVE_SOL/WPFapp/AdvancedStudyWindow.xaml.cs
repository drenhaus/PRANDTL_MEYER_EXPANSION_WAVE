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
        Malla m1 = new Malla();
        Malla m2 = new Malla();

        int columnas1=89;
        int filas1=41;
        Polygon[,] casillas1;

        DataTable temperature_table1;
        DataTable u_table1;
        DataTable v_table1;
        DataTable rho_table1;
        DataTable p_table1;
        DataTable M_table1;
        DataTable F1_table1;
        DataTable F2_table1;
        DataTable F3_table1;
        DataTable F4_table1;
        public AdvancedStudyWindow()
        {
            InitializeComponent();
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

                m1.norma.v_in = Convert.ToDouble(v_TextBox.Text);
                m1.norma.Rho_in = Convert.ToDouble(Rho_TextBox.Text);
                m1.norma.P_in = Convert.ToDouble(P_TextBox.Text);
                m1.norma.M_in = Convert.ToDouble(M_TextBox.Text);
                m1.norma.T_in = Convert.ToDouble(T_TextBox.Text);

                m1.norma.Compute_a();
                m1.norma.Compute_M_angle();
                m1.norma.Compute_u();

                m2.norma.v_in = Convert.ToDouble(v_TextBox.Text);
                m2.norma.Rho_in = Convert.ToDouble(Rho_TextBox.Text);
                m2.norma.P_in = Convert.ToDouble(P_TextBox.Text);
                m2.norma.M_in = Convert.ToDouble(M_TextBox.Text);
                m2.norma.T_in = Convert.ToDouble(T_TextBox.Text);

                m2.norma.Compute_a();
                m2.norma.Compute_M_angle();
                m2.norma.Compute_u();

                MessageBox.Show("Parameters defined");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Simulate_Button_Click(object sender, RoutedEventArgs e)
        {
            // FIRST CASE
            m1.rows = filas1;
            m1.columns = columnas1;

            m1.DefinirMatriz();
            m1.Compute();
            m1.Fill_DataTable();
            DataTable[] T_U_V_RHO_P_M_F1_F2_F3_F4 = m1.GetTables();
            temperature_table1 = T_U_V_RHO_P_M_F1_F2_F3_F4[0];
            u_table1 = T_U_V_RHO_P_M_F1_F2_F3_F4[1];
            v_table1 = T_U_V_RHO_P_M_F1_F2_F3_F4[2];
            rho_table1 = T_U_V_RHO_P_M_F1_F2_F3_F4[3];
            p_table1 = T_U_V_RHO_P_M_F1_F2_F3_F4[4];
            M_table1 = T_U_V_RHO_P_M_F1_F2_F3_F4[5];
            F1_table1 = T_U_V_RHO_P_M_F1_F2_F3_F4[6];
            F2_table1 = T_U_V_RHO_P_M_F1_F2_F3_F4[7];
            F3_table1 = T_U_V_RHO_P_M_F1_F2_F3_F4[8];
            F4_table1 = T_U_V_RHO_P_M_F1_F2_F3_F4[9];
            //GenerateGridPlot();

        }
    }

   
}

