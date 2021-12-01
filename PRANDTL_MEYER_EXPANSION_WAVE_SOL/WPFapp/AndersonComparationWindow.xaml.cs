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
using System.Data;

namespace WPFapp
{
    /// <summary>
    /// Lógica de interacción para AndersonComparationWindow.xaml
    /// </summary>
    public partial class AndersonComparationWindow : Window
    {
        public AndersonComparationWindow()
        {
            InitializeComponent();
        }

        DataTable Anderson_u_12;
        DataTable Anderson_v_12;
        DataTable Anderson_rho_12;
        DataTable Anderson_p_12;
        DataTable Anderson_T_12;
        DataTable Anderson_M_12;
        DataTable Anderson_F1_12;
        DataTable Anderson_F2_12;
        DataTable Anderson_F3_12;
        DataTable Anderson_F4_12;

        DataTable Anderson_u_66;
        DataTable Anderson_v_66;
        DataTable Anderson_rho_66;
        DataTable Anderson_p_66;
        DataTable Anderson_T_66;
        DataTable Anderson_M_66;
        DataTable Anderson_F1_66;
        DataTable Anderson_F2_66;
        DataTable Anderson_F3_66;
        DataTable Anderson_F4_66;

        double[] A_u_12 = {707,701,691,683,679,678,678,678,678,678,
                            678,678,678,678,678,678,678,678,678,678,
                            678,678,678,678,678,678,678,678,678,678,
                            678,678,678,678,678,678,678,678,678,678,678};
        double[] A_v_12 = {-66.2,-49.4,-26.6,-8.69,-1.31,-1.48,0.326 * Math.Pow(10, -5),-0.167 * Math.Pow(10, -3),0.472 * Math.Pow(10, -4), -0.702 * Math.Pow(10, -4),
                           -0.195* Math.Pow(10, -4),0.18* Math.Pow(10, -4),-0.598* Math.Pow(10, -4),-0.642* Math.Pow(10, -4),-0.325* Math.Pow(10, -13),0,0,0,0,0,0,0,0,0,
                            0.217* Math.Pow(10, -10),0.118* Math.Pow(10, -3),0.12* Math.Pow(10, -3),0.354* Math.Pow(10, -5),0.125* Math.Pow(10, -3),-0.193* Math.Pow(10, -4),
                            -0.617* Math.Pow(10, -4),0.242* Math.Pow(10, -3),0.16* Math.Pow(10, -3),0.161* Math.Pow(10, -3),0.401* Math.Pow(10, -4),-0.848* Math.Pow(10, -4),-0.128* Math.Pow(10, -3),
                            -0.342* Math.Pow(10, -4),-0.107* Math.Pow(10, -3),-0.636* Math.Pow(10, -4),0};
        double[] A_rho_12 = {0.992,1.04,1.12,1.19,1.22,1.23,1.23,1.23,1.23,1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23, 1.23,
                                1.23,1.23,1.23,1.23,1.23,1.23,1.23,1.23,1.23,1.23,1.23,1.23};
        double[] A_P_12 = { 0.734 * Math.Pow(10, 5) ,0.795 * Math.Pow(10, 5) , 0.891 * Math.Pow(10, 5) ,0.969 * Math.Pow(10, 5),0.10 * Math.Pow(10, 6),
                            0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),
                            0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),
                            0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),
                            0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),
                            0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),
                            0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),
                            0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6),0.101 * Math.Pow(10, 6)};
        double[] A_T_12 = {0.258*Math.Pow(10,3),0.267 * Math.Pow(10, 3),0.277 * Math.Pow(10, 3),0.283 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),
                           0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),
                           0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),
                           0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),
                           0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),
                           0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),
                           0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),
                           0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3),0.286 * Math.Pow(10, 3), 0.286 * Math.Pow(10, 3)};
        double[] A_M_12 = { 2.2,2.15,2.08,2.03,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2};
        double[] A_F1_12 = {0.701 * Math.Pow(10, 3),0.728 * Math.Pow(10, 3),0.776 * Math.Pow(10, 3),0.815 * Math.Pow(10, 3), 0.831 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),
                            0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),
                            0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),
                            0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),
                            0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),
                            0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),
                            0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),
                            0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3),0.834 * Math.Pow(10, 3)};
        double[] A_F2_12 = {0.569 * Math.Pow(10, 6),0.590 * Math.Pow(10, 6),0.626 * Math.Pow(10, 6),0.654 * Math.Pow(10, 6),0.665 * Math.Pow(10, 6),
                            0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),
                            0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),
                            0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),
                            0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),
                            0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),
                            0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),
                            0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6),0.667* Math.Pow(10, 6)};
        double[] A_F3_12 = { -0.464 * Math.Pow(10, 5),-0.360 * Math.Pow(10, 5) ,-0.207 * Math.Pow(10, 5) ,-0.708 * Math.Pow(10, 4),-0.109 * Math.Pow(10, 4),-0.123 * Math.Pow(10, 2),
                            0.272* Math.Pow(10, 2),-0.140,0.394* Math.Pow(10, -1),-0.586* Math.Pow(10, -1),-0.162* Math.Pow(10, -1),0.15* Math.Pow(10, -1),-0.499* Math.Pow(10, -1),
                            -0.535* Math.Pow(10, -1),-0.271* Math.Pow(10, -10),0,0,0,0,0,0,0,0,0,0.181* Math.Pow(10, -7),0.988* Math.Pow(10, -1),0.1,0.295* Math.Pow(10, -2),
                            0.104,-0.161* Math.Pow(10, -1),-0.506* Math.Pow(10, -1),0.201,0.133,0.134,0.335* Math.Pow(10, -1),-0.707* Math.Pow(10, -1),-0.106,-0.285* Math.Pow(10, -1),-0.891* Math.Pow(10, -1),
                            -0.53* Math.Pow(10, -1),0};
        double[] A_F4_12 = { 0.358*Math.Pow(10,9),0.375*Math.Pow(10, 9),0.402 * Math.Pow(10, 9),0.422 * Math.Pow(10, 9),0.430 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9)};

        double[] A_u_66 = {0.705 * Math.Pow(10, 3),0.710 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3), 0.711 * Math.Pow(10, 3), 0.711 * Math.Pow(10, 3), 0.711 * Math.Pow(10, 3), 0.711 * Math.Pow(10, 3),
                            0.711 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3),
                            0.711 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3),0.712 * Math.Pow(10, 3),0.713 * Math.Pow(10, 3),0.713 * Math.Pow(10, 3),
                            0.713 * Math.Pow(10, 3),0.711 * Math.Pow(10, 3),0.709 * Math.Pow(10, 3),0.707 * Math.Pow(10, 3),0.705 * Math.Pow(10, 3),0.702 * Math.Pow(10, 3),0.699 * Math.Pow(10, 3),
                            0.696 * Math.Pow(10, 3),0.693* Math.Pow(10, 3),0.69* Math.Pow(10, 3),0.688* Math.Pow(10, 3),0.685* Math.Pow(10, 3),0.683* Math.Pow(10, 3),0.681* Math.Pow(10, 3),0.68* Math.Pow(10, 3),
                            0.679* Math.Pow(10, 3),0.679* Math.Pow(10, 3),0.678* Math.Pow(10, 3),0.678* Math.Pow(10, 3),0.678* Math.Pow(10, 3)};
        double[] A_v_66 = { };
        double[] A_rho_66 = { };
        double[] A_p_66 = { };
        double[] A_T_66 = { };
        double[] A_M_66 = { };
        double[] A_F1_66 = { };
        double[] A_F2_66 = { };
        double[] A_F3_66 = { };
        double[] A_F4_66 = { };

        private void Compare_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Setect_Parameter_ComboBox.SelectedIndex == 0 && Setect_X_ComboBox.SelectedIndex==0) //T AND 12
            {
                

            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 0 && Setect_X_ComboBox.SelectedIndex == 1) //T AND 66
            { }

            if (Setect_Parameter_ComboBox.SelectedIndex == 1 && Setect_X_ComboBox.SelectedIndex == 0) //u AND 12
            { }
            if (Setect_Parameter_ComboBox.SelectedIndex == 1 && Setect_X_ComboBox.SelectedIndex == 1) //u AND 66
            { }

            if (Setect_Parameter_ComboBox.SelectedIndex == 2 && Setect_X_ComboBox.SelectedIndex == 0) //v AND 12
            { }
            if (Setect_Parameter_ComboBox.SelectedIndex == 2 && Setect_X_ComboBox.SelectedIndex == 1) //v AND 66
            { }

            if (Setect_Parameter_ComboBox.SelectedIndex == 3 && Setect_X_ComboBox.SelectedIndex == 0) //rho AND 12
            { }
            if (Setect_Parameter_ComboBox.SelectedIndex == 3 && Setect_X_ComboBox.SelectedIndex == 1) //rho AND 66
            { }

            if (Setect_Parameter_ComboBox.SelectedIndex == 4 && Setect_X_ComboBox.SelectedIndex == 0) //p AND 12
            { }
            if (Setect_Parameter_ComboBox.SelectedIndex == 4 && Setect_X_ComboBox.SelectedIndex == 1) //p AND 66
            { }

            if (Setect_Parameter_ComboBox.SelectedIndex == 5 && Setect_X_ComboBox.SelectedIndex == 0) //M AND 12
            { }
            if (Setect_Parameter_ComboBox.SelectedIndex == 5 && Setect_X_ComboBox.SelectedIndex == 1) //M AND 66
            { }

            if (Setect_Parameter_ComboBox.SelectedIndex == 6 && Setect_X_ComboBox.SelectedIndex == 0) //F1 AND 12
            { }
            if (Setect_Parameter_ComboBox.SelectedIndex == 6 && Setect_X_ComboBox.SelectedIndex == 1) //F1 AND 66
            { }

            if (Setect_Parameter_ComboBox.SelectedIndex == 7 && Setect_X_ComboBox.SelectedIndex == 0) //F2 AND 12
            { }
            if (Setect_Parameter_ComboBox.SelectedIndex == 7 && Setect_X_ComboBox.SelectedIndex == 1) //F2 AND 66
            { }

            if (Setect_Parameter_ComboBox.SelectedIndex == 8 && Setect_X_ComboBox.SelectedIndex == 0) //F3 AND 12
            { }
            if (Setect_Parameter_ComboBox.SelectedIndex == 8 && Setect_X_ComboBox.SelectedIndex == 1) //F3 AND 66
            { }

            if (Setect_Parameter_ComboBox.SelectedIndex == 9 && Setect_X_ComboBox.SelectedIndex == 0) //F4 AND 12
            { }
            if (Setect_Parameter_ComboBox.SelectedIndex == 9 && Setect_X_ComboBox.SelectedIndex == 1) //F4 AND 66
            { }
        }



    }
}
