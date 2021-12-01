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
        double[] A_v_12 = {};
        double[] A_rho_12 = { };
        double[] A_P_12 = { };
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
        double[] A_F2_12 = { };
        double[] A_F3_12 = { };
        double[] A_F4_12 = { 0.358*Math.Pow(10,9),0.375*Math.Pow(10, 9),0.402 * Math.Pow(10, 9),0.422 * Math.Pow(10, 9),0.430 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),
                             0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9)};
    

        // COMPLETAR TABLAS CON VALORES DEL ANDERSON

    }
}
