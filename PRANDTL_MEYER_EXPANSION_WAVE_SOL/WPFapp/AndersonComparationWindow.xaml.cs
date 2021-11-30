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
        double[] A_v_12 = {-66.2,-49.4,-26.6,-8.69,-1.31,-0.0148,0.00000326,-0.000167,0.0000472,-0.0000702,
                            -0.0000195,0.000018,-0.0000598,-0.0000642,-0.0000000000000325,
                             0,0,0,0,0,0,0,0,0,0.0000000000217,0.000118,};

        // COMPLETAR TABLAS CON VALORES DEL ANDERSON

    }
}
