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
using LibreriaClases;

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

            #region HIDDE LABELS & TEXTBOX
            anderson_error.Visibility = Visibility.Hidden;
            anderson_label.Visibility = Visibility.Hidden;
            computed_label.Visibility = Visibility.Hidden;
            our_error.Visibility = Visibility.Hidden;
            #endregion HIDDE LABELS & TEXTBOX
        }

        #region ATRIBUTES
        double Anderson_M_error;
        double Anderson_p_error;
        double Anderson_rho_error;
        double Anderson_t_error;
        double Anderson_u_error;
        double Anderson_v_error;

        double our_M_error;
        double our_p_error;
        double our_rho_error;
        double our_t_error;
        double our_u_error;
        double our_v_error;

        public Malla m { get; set; }
        public int p { get; set; } // indicates the precision. if p=0 low. p=1 medium. p=2 high

        #region DATATABLES
        // ANDERSON TABLES
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
        DataTable Anderson_v_66 ;
        DataTable Anderson_rho_66;
        DataTable Anderson_p_66;
        DataTable Anderson_T_66;
        DataTable Anderson_M_66;
        DataTable Anderson_F1_66;
        DataTable Anderson_F2_66;
        DataTable Anderson_F3_66;
        DataTable Anderson_F4_66;

        //OUR TABLES
        DataTable our_u_12;
        DataTable our_v_12;
        DataTable our_rho_12;
        DataTable our_p_12;
        DataTable our_T_12;
        DataTable our_M_12;
        DataTable our_F1_12 ;
        DataTable our_F2_12;
        DataTable our_F3_12;
        DataTable our_F4_12;

        DataTable our_u_66;
        DataTable our_v_66 ;
        DataTable our_rho_66;
        DataTable our_p_66;
        DataTable our_T_66;
        DataTable our_M_66;
        DataTable our_F1_66;
        DataTable our_F2_66;
        DataTable our_F3_66;
        DataTable our_F4_66;
        #endregion DATATABLE

        #region VECTORS ANDERSON
        // vectors with the data of Anderson
        double[] A_u_12 = {707,701,691,683,679,
                           678,678,678,678,678,
                           678,678,678,678,678,
                           678,678,678,678,678, 
                           678,678,678,678,678,
                           678,678,678,678,678,
                           678,678,678,678,678,
                           678,678,678,678,678,678};
        double[] A_v_12 = {-66.2,-49.4,-26.6,-8.69,-1.31,-1.48,0.326 * Math.Pow(10, -5),-0.167 * Math.Pow(10, -3),0.472 * Math.Pow(10, -4), -0.702 * Math.Pow(10, -4),-0.195* Math.Pow(10, -4),0.18* Math.Pow(10, -4),-0.598* Math.Pow(10, -4),-0.642* Math.Pow(10, -4),-0.325* Math.Pow(10, -13),0,0,0,0,0,0,0,0,0,
                            0.217* Math.Pow(10, -10),0.118* Math.Pow(10, -3),0.12* Math.Pow(10, -3),0.354* Math.Pow(10, -5),0.125* Math.Pow(10, -3),-0.193* Math.Pow(10, -4),-0.617* Math.Pow(10, -4),0.242* Math.Pow(10, -3),0.16* Math.Pow(10, -3),0.161* Math.Pow(10, -3),0.401* Math.Pow(10, -4),-0.848* Math.Pow(10, -4),-0.128* Math.Pow(10, -3),
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
        double[] A_v_66 = {-66.1,-68.2,-69,-68.8,-68.9,-68.8,-68.9,-69,-69,-68.8,
                           -68.6,-68.8,-69.4,-69.6,-69,-67.8,-67.2,-68.3,-70.8,-73.2,
                            -74,-72.6,-69.3,-64.7,-59.1,-53.1,-46.8,-40.5,-34.3,-28.3,
                            -22.7,-17.5,-12.9,-9.01,-5.91,-3.61,-2.03,-1.05,-0.499,-0.229,0};
        double[] A_rho_66 = {1.09,1.07,0.969,0.977,0.976,0.976,0.976,0.976,0.976,0.977,
                             0.977,0.977,0.975,0.974,0.976,0.980,0.982,0.978,0.97,0.963,0.96,0.964,0.975,0.99,1.01,
                            1.03,1.05,1.07,1.10,1.12,1.14,1.16,1.18,1.19,1.21,1.21,1.22,1.23,1.23,1.23,1.23};
        double[] A_p_66 = { 0.7351 * Math.Pow(10, 5), 0.730 * Math.Pow(10, 5), 0.732 * Math.Pow(10, 5), 0.731 * Math.Pow(10, 5), 0.731 * Math.Pow(10, 5),
                            0.731 * Math.Pow(10, 5),0.731 * Math.Pow(10, 5),0.731 * Math.Pow(10, 5),0.731 * Math.Pow(10, 5),0.731 * Math.Pow(10, 5),
                            0.732 * Math.Pow(10, 5),0.731 * Math.Pow(10, 5),0.729 * Math.Pow(10, 5),0.729 * Math.Pow(10, 5),0.731 * Math.Pow(10, 5),
                            0.735 * Math.Pow(10, 5),0.737 * Math.Pow(10, 5),0.733 * Math.Pow(10, 5),0.725 * Math.Pow(10, 5),0.717 * Math.Pow(10, 5),
                            0.714 * Math.Pow(10, 5),0.719* Math.Pow(10, 5),0.730* Math.Pow(10, 5),0.746* Math.Pow(10, 5),0.765* Math.Pow(10, 5),
                            0.787* Math.Pow(10, 5),0.810* Math.Pow(10, 5),0.834* Math.Pow(10, 5),0.859* Math.Pow(10, 5),0.883* Math.Pow(10, 5),
                            0.907* Math.Pow(10, 5),0.930* Math.Pow(10, 5),0.95* Math.Pow(10, 5),0.968* Math.Pow(10, 5),0.982* Math.Pow(10, 5),
                            0.993* Math.Pow(10, 5),0.1* Math.Pow(10, 6),0.1* Math.Pow(10, 6),0.1* Math.Pow(10, 6),0.1* Math.Pow(10, 6),0.1* Math.Pow(10, 6)};
        double[] A_T_66 = { 0.233 * Math.Pow(10, 3),0.237 * Math.Pow(10, 3) ,0.263 * Math.Pow(10, 3) ,0.261 * Math.Pow(10, 3), 0.261 * Math.Pow(10, 3) ,
                            0.261 * Math.Pow(10, 3),0.261 * Math.Pow(10, 3),0.261 * Math.Pow(10, 3),0.261 * Math.Pow(10, 3),0.261 * Math.Pow(10, 3),
                            0.261 * Math.Pow(10, 3),0.261 * Math.Pow(10, 3),0.261 * Math.Pow(10, 3),0.261 * Math.Pow(10, 3),0.261 * Math.Pow(10, 3),
                            0.261 * Math.Pow(10, 3),0.261 * Math.Pow(10, 3),0.261 * Math.Pow(10, 3),0.260 * Math.Pow(10, 3),0.259 * Math.Pow(10, 3),
                            0.259* Math.Pow(10, 3),0.26* Math.Pow(10, 3),0.261* Math.Pow(10, 3),0.262* Math.Pow(10, 3),0.264* Math.Pow(10, 3),
                            0.266* Math.Pow(10, 3),0.269* Math.Pow(10, 3),0.271* Math.Pow(10, 3),0.273* Math.Pow(10, 3),0.275* Math.Pow(10, 3),
                            0.277* Math.Pow(10, 3),0.279* Math.Pow(10, 3),0.281* Math.Pow(10, 3),0.283* Math.Pow(10, 3),0.284* Math.Pow(10, 3),
                            0.285* Math.Pow(10, 3),0.285* Math.Pow(10, 3),0.286* Math.Pow(10, 3),0.286* Math.Pow(10, 3),0.286* Math.Pow(10, 3),0.286* Math.Pow(10, 3)};
        double[] A_M_66 = {2.31,2.31,2.2,2.21,2.21,2.21,2.21,2.21,2.21,2.21,2.21,2.21,2.21,2.21,2.21,2.2,2.2,2.21,2.21,2.22,2.22,2.22,2.21,2.19,2.18,2.16,2.14,2.12,2.1,2.09,2.07,2.05,2.04,2.03,2.02,2.01,2.01,2,2,2,2};
        double[] A_F1_66 = { 0.769 * Math.Pow(10, 3) ,0.76 * Math.Pow(10, 3) ,0.689 * Math.Pow(10, 3) ,0.694 * Math.Pow(10, 3), 0.694 * Math.Pow(10, 3),0.694 * Math.Pow(10, 3),
                            0.694 * Math.Pow(10, 3),0.694 * Math.Pow(10, 3),0.694 * Math.Pow(10, 3),0.694 * Math.Pow(10, 3),0.695 * Math.Pow(10, 3),0.695 * Math.Pow(10, 3),
                            0.693 * Math.Pow(10, 3),0.693 * Math.Pow(10, 3),0.694 * Math.Pow(10, 3),0.697 * Math.Pow(10, 3),0.698* Math.Pow(10, 3),
                            0.696* Math.Pow(10, 3),0.691* Math.Pow(10, 3),0.686* Math.Pow(10, 3),0.685* Math.Pow(10, 3),0.687* Math.Pow(10, 3),
                            0.694* Math.Pow(10, 3),0.703* Math.Pow(10, 3),0.713* Math.Pow(10, 3),0.725* Math.Pow(10, 3),0.737* Math.Pow(10, 3),0.75* Math.Pow(10, 3),
                            0.763* Math.Pow(10, 3),0.775* Math.Pow(10, 3),0.786* Math.Pow(10, 3),0.797* Math.Pow(10, 3),0.807* Math.Pow(10, 3),0.815* Math.Pow(10, 3),
                            0.822* Math.Pow(10, 3),0.826* Math.Pow(10, 3),0.830* Math.Pow(10, 3),0.832* Math.Pow(10, 3),0.833* Math.Pow(10, 3),0.834* Math.Pow(10, 3),
                            0.834* Math.Pow(10, 3)};
        double[] A_F2_66 = {0.616 * Math.Pow(10, 6), 0.612 * Math.Pow(10, 6), 0.563 * Math.Pow(10, 6), 0.567 * Math.Pow(10, 6), 0.567 * Math.Pow(10, 6),
                            0.567 * Math.Pow(10, 6),0.567 * Math.Pow(10, 6),0.567 * Math.Pow(10, 6),0.567 * Math.Pow(10, 6),0.567 * Math.Pow(10, 6),
                            0.567 * Math.Pow(10, 6),0.567 * Math.Pow(10, 6),0.566 * Math.Pow(10, 6),0.566 * Math.Pow(10, 6),0.567 * Math.Pow(10, 6),
                            0.569 * Math.Pow(10, 6),0.569 * Math.Pow(10, 6),0.568 * Math.Pow(10, 6),0.564 * Math.Pow(10, 6),0.561 * Math.Pow(10, 6),
                            0.560 * Math.Pow(10, 6),0.562 * Math.Pow(10, 6),0.566 * Math.Pow(10, 6),0.573 * Math.Pow(10, 6),0.581 * Math.Pow(10, 6),
                            0.590 * Math.Pow(10, 6),0.599 * Math.Pow(10, 6),0.608 * Math.Pow(10, 6),0.617 * Math.Pow(10, 6),0.625 * Math.Pow(10, 6),
                            0.634 * Math.Pow(10, 6),0.641 * Math.Pow(10, 6),0.648 * Math.Pow(10, 6),0.654 * Math.Pow(10, 6),0.658 * Math.Pow(10, 6),
                            0.661 * Math.Pow(10, 6),0.664 * Math.Pow(10, 6),0.665 * Math.Pow(10, 6),0.666* Math.Pow(10, 6),0.66 * Math.Pow(10, 6),0.667 * Math.Pow(10, 6),};
        double[] A_F3_66 = { -0.508 * Math.Pow(10, 5), -0.519 * Math.Pow(10, 5), -0.475 * Math.Pow(10, 5), -0.478 * Math.Pow(10, 5), -0.478 * Math.Pow(10, 5),
                              -0.478 * Math.Pow(10, 5),-0.478 * Math.Pow(10, 5),-0.479 * Math.Pow(10, 5),-0.479 * Math.Pow(10, 5),-0.478 * Math.Pow(10, 5),
                                -0.477 * Math.Pow(10, 5),-0.478 * Math.Pow(10, 5),-0.481 * Math.Pow(10, 5),-0.483 * Math.Pow(10, 5),-0.479 * Math.Pow(10, 5),
                                -0.472 * Math.Pow(10, 5),-0.469 * Math.Pow(10, 5),-0.475 * Math.Pow(10, 5),-0.489 * Math.Pow(10, 5),-0.502 * Math.Pow(10, 5),
                                -0.506 * Math.Pow(10, 5),-0.499 * Math.Pow(10, 5),-0.481 * Math.Pow(10, 5),-0.454 * Math.Pow(10, 5),-0.422 * Math.Pow(10, 5),
                                -0.385 * Math.Pow(10, 5),-0.345 * Math.Pow(10, 5),-0.304 * Math.Pow(10, 5),-0.262 * Math.Pow(10, 5),-0.220 * Math.Pow(10, 5),
                                -0.178 * Math.Pow(10, 5),-0.139 * Math.Pow(10, 5),-0.104 * Math.Pow(10, 5),-0.734 * Math.Pow(10, 4),-0.485 * Math.Pow(10, 4),
                                -0.298 * Math.Pow(10, 4),-0.169 * Math.Pow(10, 4),-0.877 * Math.Pow(10, 3),-0.416 * Math.Pow(10, 3),-0.191 * Math.Pow(10, 3),0};
        double[] A_F4_66 = {0.374 * Math.Pow(10, 9), 0.374 * Math.Pow(10, 9), 0.358 * Math.Pow(10, 9), 0.359 * Math.Pow(10, 9), 0.359 * Math.Pow(10, 9),
                            0.359 * Math.Pow(10, 9),0.359 * Math.Pow(10, 9),0.359 * Math.Pow(10, 9),0.359 * Math.Pow(10, 9),0.359 * Math.Pow(10, 9),
                            0.359 * Math.Pow(10, 9),0.359 * Math.Pow(10, 9),0.359 * Math.Pow(10, 9),0.358 * Math.Pow(10, 9),0.359 * Math.Pow(10, 9),
                            0.360 * Math.Pow(10, 9),0.361 * Math.Pow(10, 9),0.360 * Math.Pow(10, 9),0.357 * Math.Pow(10, 9),0.355 * Math.Pow(10, 9),
                            0.354 * Math.Pow(10, 9),0.356 * Math.Pow(10, 9),0.359 * Math.Pow(10, 9),0.363 * Math.Pow(10, 9),0.369 * Math.Pow(10, 9),
                            0.375 * Math.Pow(10, 9),0.382 * Math.Pow(10, 9),0.388 * Math.Pow(10, 9),0.394 * Math.Pow(10, 9),0.401 * Math.Pow(10, 9),
                            407 * Math.Pow(10, 9),0.412 * Math.Pow(10, 9),0.417 * Math.Pow(10, 9),0.422 * Math.Pow(10, 9),0.425 * Math.Pow(10, 9),
                            0.428 * Math.Pow(10, 9),0.429 * Math.Pow(10, 9),0.430 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9),0.431 * Math.Pow(10, 9)};
        #endregion VECTORS ANDERSON

        #endregion ATRIBUTES

        #region FILL DATATABLE

        public void Fill_Anderson_Tables()
        {
            DataColumn TEMP_C = new DataColumn();
            DataColumn U_C = new DataColumn();
            DataColumn V_C = new DataColumn();
            DataColumn RHO_C = new DataColumn();
            DataColumn P_C = new DataColumn();
            DataColumn M_C = new DataColumn();
            DataColumn F1_C = new DataColumn();
            DataColumn F2_C = new DataColumn();
            DataColumn F3_C = new DataColumn();
            DataColumn F4_C = new DataColumn();

            DataColumn TEMP_C2 = new DataColumn();
            DataColumn U_C2 = new DataColumn();
            DataColumn V_C2 = new DataColumn();
            DataColumn RHO_C2 = new DataColumn();
            DataColumn P_C2 = new DataColumn();
            DataColumn M_C2 = new DataColumn();
            DataColumn F1_C2 = new DataColumn();
            DataColumn F2_C2 = new DataColumn();
            DataColumn F3_C2 = new DataColumn();
            DataColumn F4_C2 = new DataColumn();

            Anderson_T_12.Columns.Add(TEMP_C);
            Anderson_u_12.Columns.Add(U_C);
            Anderson_v_12.Columns.Add(V_C);
            Anderson_rho_12.Columns.Add(RHO_C);
            Anderson_p_12.Columns.Add(P_C);
            Anderson_M_12.Columns.Add(M_C);
            Anderson_F1_12.Columns.Add(F1_C);
            Anderson_F2_12.Columns.Add(F2_C);
            Anderson_F3_12.Columns.Add(F3_C);
            Anderson_F4_12.Columns.Add(F4_C);

            Anderson_T_66.Columns.Add(TEMP_C2);
            Anderson_u_66.Columns.Add(U_C2);
            Anderson_v_66.Columns.Add(V_C2);
            Anderson_rho_66.Columns.Add(RHO_C2);
            Anderson_p_66.Columns.Add(P_C2);
            Anderson_M_66.Columns.Add(M_C2);
            Anderson_F1_66.Columns.Add(F1_C2);
            Anderson_F2_66.Columns.Add(F2_C2);
            Anderson_F3_66.Columns.Add(F3_C2);
            Anderson_F4_66.Columns.Add(F4_C2);

            for (int i = 0; i < A_F4_66.Length; i++)
            {
                DataRow TEMP_R = Anderson_T_12.NewRow();
                DataRow U_R = Anderson_u_12.NewRow();
                DataRow V_R = Anderson_v_12.NewRow();
                DataRow RHO_R = Anderson_rho_12.NewRow();
                DataRow P_R = Anderson_p_12.NewRow();
                DataRow M_R = Anderson_M_12.NewRow();
                DataRow F1_R = Anderson_F1_12.NewRow();
                DataRow F2_R = Anderson_F2_12.NewRow();
                DataRow F3_R = Anderson_F3_12.NewRow();
                DataRow F4_R = Anderson_F4_12.NewRow();

                DataRow TEMP_R2 = Anderson_T_66.NewRow();
                DataRow U_R2 = Anderson_u_66.NewRow();
                DataRow V_R2 = Anderson_v_66.NewRow();
                DataRow RHO_R2 = Anderson_rho_66.NewRow();
                DataRow P_R2 = Anderson_p_66.NewRow();
                DataRow M_R2 = Anderson_M_66.NewRow();
                DataRow F1_R2 = Anderson_F1_66.NewRow();
                DataRow F2_R2 = Anderson_F2_66.NewRow();
                DataRow F3_R2 = Anderson_F3_66.NewRow();
                DataRow F4_R2 = Anderson_F4_66.NewRow();

                TEMP_R[0] = A_T_12[i];
                U_R[0] = A_u_12[i];
                V_R[0] = A_v_12[i];
                RHO_R[0] = A_rho_12[i];
                P_R[0] = A_P_12[i];
                M_R[0] = A_M_12[i];
                F1_R[0] = A_F1_12[i];
                F2_R[0] = A_F2_12[i];
                F3_R[0] = A_F3_12[i];
                F4_R[0] = A_F4_12[i];

                TEMP_R2[0] = A_T_66[i];
                U_R2[0] = A_u_66[i];
                V_R2[0] = A_v_66[i];
                RHO_R2[0] = A_rho_66[i];
                P_R2[0] = A_p_66[i];
                M_R2[0] = A_M_66[i];
                F1_R2[0] = A_F1_66[i];
                F2_R2[0] = A_F2_66[i];
                F3_R2[0] = A_F3_66[i];
                F4_R2[0] = A_F4_66[i];

                Anderson_T_12.Rows.Add(TEMP_R);
                Anderson_u_12.Rows.Add(U_R);
                Anderson_v_12.Rows.Add(V_R);
                Anderson_rho_12.Rows.Add(RHO_R);
                Anderson_p_12.Rows.Add(P_R);
                Anderson_M_12.Rows.Add(M_R);
                Anderson_F1_12.Rows.Add(F1_R);
                Anderson_F2_12.Rows.Add(F2_R);
                Anderson_F3_12.Rows.Add(F3_R);
                Anderson_F4_12.Rows.Add(F4_R);

                Anderson_T_66.Rows.Add(TEMP_R2);
                Anderson_u_66.Rows.Add(U_R2);
                Anderson_v_66.Rows.Add(V_R2);
                Anderson_rho_66.Rows.Add(RHO_R2);
                Anderson_p_66.Rows.Add(P_R2);
                Anderson_M_66.Rows.Add(M_R2);
                Anderson_F1_66.Rows.Add(F1_R2);
                Anderson_F2_66.Rows.Add(F2_R2);
                Anderson_F3_66.Rows.Add(F3_R2);
                Anderson_F4_66.Rows.Add(F4_R2);

            }
        }
        public void Fill_Our_Tables()
        {
            double x;
            double[] O_T_12= new double[A_F4_66.Length];
            double[] O_V_12 = new double[A_F4_66.Length];
            double[] O_U_12 = new double[A_F4_66.Length];
            double[] O_RHO_12 = new double[A_F4_66.Length];
            double[] O_P_12 = new double[A_F4_66.Length];
            double[] O_M_12 = new double[A_F4_66.Length];
            double[] O_F1_12 = new double[A_F4_66.Length];
            double[] O_F2_12 = new double[A_F4_66.Length];
            double[] O_F3_12 = new double[A_F4_66.Length];
            double[] O_F4_12 = new double[A_F4_66.Length];

            double[] O_T_66 = new double[A_F4_66.Length];
            double[] O_V_66 = new double[A_F4_66.Length];
            double[] O_U_66 = new double[A_F4_66.Length];
            double[] O_RHO_66 = new double[A_F4_66.Length];
            double[] O_P_66 = new double[A_F4_66.Length];
            double[] O_M_66 = new double[A_F4_66.Length];
            double[] O_F1_66 = new double[A_F4_66.Length];
            double[] O_F2_66 = new double[A_F4_66.Length];
            double[] O_F3_66 = new double[A_F4_66.Length];
            double[] O_F4_66 = new double[A_F4_66.Length];

            if (p == 1)
            {
                x = 12.928;
                O_T_12 = Interpolate_our_results(x, "t");
                O_V_12 = Interpolate_our_results(x, "v");
                O_U_12 = Interpolate_our_results(x, "u");
                O_RHO_12 = Interpolate_our_results(x, "rho");
                O_P_12 = Interpolate_our_results(x, "p");
                O_M_12 = Interpolate_our_results(x, "m");
                O_F1_12 = Interpolate_our_results(x, "f1");
                O_F2_12 = Interpolate_our_results(x, "f2");
                O_F3_12 = Interpolate_our_results(x, "f3");
                O_F4_12 = Interpolate_our_results(x, "f4");

                x = 66.278; 
                O_T_66 = Interpolate_our_results(x, "t");
                O_V_66 = Interpolate_our_results(x, "v");
                O_U_66 = Interpolate_our_results(x, "u");
                O_RHO_66 = Interpolate_our_results(x, "rho");
                O_P_66 = Interpolate_our_results(x, "p");
                O_M_66 = Interpolate_our_results(x, "m");
                O_F1_66 = Interpolate_our_results(x, "f1");
                O_F2_66 = Interpolate_our_results(x, "f2");
                O_F3_66 = Interpolate_our_results(x, "f3");
                O_F4_66 = Interpolate_our_results(x, "f4");
            }

            if (p == 0 || p==2)
            {
                x = 12.928;
                O_T_12 = Interpolate_our_results_different_precision(x, "t");
                O_V_12 = Interpolate_our_results_different_precision(x, "v");
                O_U_12 = Interpolate_our_results_different_precision(x, "u");
                O_RHO_12 = Interpolate_our_results_different_precision(x, "rho");
                O_P_12 = Interpolate_our_results_different_precision(x, "p");
                O_M_12 = Interpolate_our_results_different_precision(x, "m");
                O_F1_12 = Interpolate_our_results_different_precision(x, "f1");
                O_F2_12 = Interpolate_our_results_different_precision(x, "f2");
                O_F3_12 = Interpolate_our_results_different_precision(x, "f3");
                O_F4_12 = Interpolate_our_results_different_precision(x, "f4");

                x = 66.278;
                O_T_66 = Interpolate_our_results_different_precision(x, "t");
                O_V_66 = Interpolate_our_results_different_precision(x, "v");
                O_U_66 = Interpolate_our_results_different_precision(x, "u");
                O_RHO_66 = Interpolate_our_results_different_precision(x, "rho");
                O_P_66 = Interpolate_our_results_different_precision(x, "p");
                O_M_66 = Interpolate_our_results_different_precision(x, "m");
                O_F1_66 = Interpolate_our_results_different_precision(x, "f1");
                O_F2_66 = Interpolate_our_results_different_precision(x, "f2");
                O_F3_66 = Interpolate_our_results_different_precision(x, "f3");
                O_F4_66 = Interpolate_our_results_different_precision(x, "f4");
            }


            DataColumn TEMP_C = new DataColumn();
            DataColumn U_C = new DataColumn();
            DataColumn V_C = new DataColumn();
            DataColumn RHO_C = new DataColumn();
            DataColumn P_C = new DataColumn();
            DataColumn M_C = new DataColumn();
            DataColumn F1_C = new DataColumn();
            DataColumn F2_C = new DataColumn();
            DataColumn F3_C = new DataColumn();
            DataColumn F4_C = new DataColumn();

            DataColumn TEMP_C2 = new DataColumn();
            DataColumn U_C2 = new DataColumn();
            DataColumn V_C2 = new DataColumn();
            DataColumn RHO_C2 = new DataColumn();
            DataColumn P_C2 = new DataColumn();
            DataColumn M_C2 = new DataColumn();
            DataColumn F1_C2 = new DataColumn();
            DataColumn F2_C2 = new DataColumn();
            DataColumn F3_C2 = new DataColumn();
            DataColumn F4_C2 = new DataColumn();

            our_T_12.Columns.Add(TEMP_C);
            our_u_12.Columns.Add(U_C);
            our_v_12.Columns.Add(V_C);
            our_rho_12.Columns.Add(RHO_C);
            our_p_12.Columns.Add(P_C);
            our_M_12.Columns.Add(M_C);
            our_F1_12.Columns.Add(F1_C);
            our_F2_12.Columns.Add(F2_C);
            our_F3_12.Columns.Add(F3_C);
            our_F4_12.Columns.Add(F4_C);

            our_T_66.Columns.Add(TEMP_C2);
            our_u_66.Columns.Add(U_C2);
            our_v_66.Columns.Add(V_C2);
            our_rho_66.Columns.Add(RHO_C2);
            our_p_66.Columns.Add(P_C2);
            our_M_66.Columns.Add(M_C2);
            our_F1_66.Columns.Add(F1_C2);
            our_F2_66.Columns.Add(F2_C2);
            our_F3_66.Columns.Add(F3_C2);
            our_F4_66.Columns.Add(F4_C2);

            for (int i = 0; i < A_F4_66.Length; i++)
            {
                DataRow TEMP_R = our_T_12.NewRow();
                DataRow U_R = our_u_12.NewRow();
                DataRow V_R = our_v_12.NewRow();
                DataRow RHO_R = our_rho_12.NewRow();
                DataRow P_R = our_p_12.NewRow();
                DataRow M_R = our_M_12.NewRow();
                DataRow F1_R = our_F1_12.NewRow();
                DataRow F2_R = our_F2_12.NewRow();
                DataRow F3_R = our_F3_12.NewRow();
                DataRow F4_R = our_F4_12.NewRow();

                DataRow TEMP_R2 = our_T_66.NewRow();
                DataRow U_R2 = our_u_66.NewRow();
                DataRow V_R2 = our_v_66.NewRow();
                DataRow RHO_R2 = our_rho_66.NewRow();
                DataRow P_R2 = our_p_66.NewRow();
                DataRow M_R2 = our_M_66.NewRow();
                DataRow F1_R2 = our_F1_66.NewRow();
                DataRow F2_R2 = our_F2_66.NewRow();
                DataRow F3_R2 = our_F3_66.NewRow();
                DataRow F4_R2 = our_F4_66.NewRow();

                TEMP_R[0] = O_T_12[i];
                U_R[0] = O_U_12[i];
                V_R[0] = O_V_12[i];
                RHO_R[0] = O_RHO_12[i];
                P_R[0] = O_P_12[i];
                M_R[0] = O_M_12[i];
                F1_R[0] = O_F1_12[i];
                F2_R[0] = O_F2_12[i];
                F3_R[0] = O_F3_12[i];
                F4_R[0] = O_F4_12[i];

                TEMP_R2[0] = O_T_66[i];
                U_R2[0] = O_U_66[i];
                V_R2[0] = O_V_66[i];
                RHO_R2[0] = O_RHO_66[i];
                P_R2[0] = O_P_66[i];
                M_R2[0] = O_M_66[i];
                F1_R2[0] = O_F1_66[i];
                F2_R2[0] = O_F2_66[i];
                F3_R2[0] = O_F3_66[i];
                F4_R2[0] = O_F4_66[i];

                our_T_12.Rows.Add(TEMP_R);
                our_u_12.Rows.Add(U_R);
                our_v_12.Rows.Add(V_R);
                our_rho_12.Rows.Add(RHO_R);
                our_p_12.Rows.Add(P_R);
                our_M_12.Rows.Add(M_R);
                our_F1_12.Rows.Add(F1_R);
                our_F2_12.Rows.Add(F2_R);
                our_F3_12.Rows.Add(F3_R);
                our_F4_12.Rows.Add(F4_R);

                our_T_66.Rows.Add(TEMP_R2);
                our_u_66.Rows.Add(U_R2);
                our_v_66.Rows.Add(V_R2);
                our_rho_66.Rows.Add(RHO_R2);
                our_p_66.Rows.Add(P_R2);
                our_M_66.Rows.Add(M_R2);
                our_F1_66.Rows.Add(F1_R2);
                our_F2_66.Rows.Add(F2_R2);
                our_F3_66.Rows.Add(F3_R2);
                our_F4_66.Rows.Add(F4_R2);
            }
        }

        #region TABLE COMPUTATIONS
        public void Compute_Errors()
        {
            Anderson_M_error = Math.Round(100 * Math.Abs(2.2 - MeanValue(Anderson_M_66)) / 2.2,4);    
            Anderson_p_error = Math.Round(100 * Math.Abs(0.739*Math.Pow(10,5) - MeanValue(Anderson_p_66)) / (0.739 * Math.Pow(10, 5)),4);
            Anderson_rho_error = Math.Round(100 * Math.Abs(0.984 - MeanValue(Anderson_rho_66)) / 0.984,4);
            Anderson_t_error = Math.Round(100 * Math.Abs(262 - MeanValue(Anderson_T_66)) / 262,4);
            Anderson_u_error = Math.Round(100 * Math.Abs(710 - MeanValue(Anderson_u_66)) / 710,4);
            Anderson_v_error =Math.Abs( Math.Round(100 * Math.Abs(-66.5 - MeanValue(Anderson_v_66)) / -66.5,4));
            
            our_M_error= Math.Round(100 * Math.Abs(2.2 - MeanValue(our_M_66)) / 2.2,4);
            our_p_error= Math.Round(100 * Math.Abs(0.739 * Math.Pow(10, 5) - MeanValue(our_p_66)) / (0.739 * Math.Pow(10, 5)),4);
            our_rho_error= Math.Round(100 * Math.Abs(0.984 - MeanValue(our_rho_66)) / 0.984,4);
            our_t_error= Math.Round(100 * Math.Abs(262 - MeanValue(our_T_66)) / 262,4);
            our_u_error= Math.Round(100 * Math.Abs(710 - MeanValue(our_u_66)) / 710,4);
            our_v_error= Math.Abs(Math.Round(100 * Math.Abs(-66.5 - MeanValue(our_v_66)) / -66.5,4));

        }
        public double MeanValue(DataTable dt)
        {
            double accomulated = 0;

            for (int i = 1; i <= 22; i++)
            {
                accomulated = accomulated + Convert.ToDouble(dt.Rows[i][0].ToString());
            }
            double mean = accomulated / 22;

            return mean;
        }
        #endregion TABLE COMPUTATIONS

        #endregion FILL DATATBLE

        #region INTERPOLATION FUNCTIONS
        public double[] Interpolate_our_results(double x_choosen, string data)
        {
            double[] valores1;
            double[] valores2;
            double[] valores = new double[m.rows];

            int j = 0;
            bool valor_exacto = false;

            for (j = 0; m.matriz[0, j].x <= x_choosen; j++)
            {

                if (m.matriz[0, j].x == x_choosen)
                {
                    valor_exacto = true;
                }
            }

            if (valor_exacto == false)
            {
                // we interpolate between values at j and values at j-1
                valores1 = m.GetColumnData_array(data, j);
                valores2 = m.GetColumnData_array(data, j - 1); //last value checked with x smaller that the choosen x 

                for (int i = 0; i < valores1.Length; i++)
                {
                    //Interpolacion lineal
                    valores[i] = valores2[i] + (valores1[i] - valores2[i]) / (m.matriz[0, j].x - m.matriz[0, j - 1].x) * (x_choosen - m.matriz[0, j - 1].x);
                }
            }
            else if (valor_exacto == true)
            {
                valores = m.GetColumnData_array(data, j - 1);
            }

            return valores;

        }

        public double[] Interpolate_our_results_different_precision(double x_choosen, string data)
        {
            double[] valores1;
            double[] valores2;
            double[] valores = new double[m.rows];

            int j = 0;
            bool valor_exacto = false;

            for (j = 0; m.matriz[0, j].x <= x_choosen; j++)
            {

                if (m.matriz[0, j].x == x_choosen)
                {
                    valor_exacto = true;
                }
            }

            if (valor_exacto == false)
            {
                // we interpolate between values at j and values at j-1
                valores1 = m.GetColumnData_array(data, j);
                valores2 = m.GetColumnData_array(data, j - 1); //last value checked with x smaller that the choosen x 

                for (int i = 0; i < valores1.Length; i++)
                {
                    //Interpolacion lineal
                    valores[i] = valores2[i] + (valores1[i] - valores2[i]) / (m.matriz[0, j].x - m.matriz[0, j - 1].x) * (x_choosen - m.matriz[0, j - 1].x);
                }
            }
            else if (valor_exacto == true)
            {
                valores = m.GetColumnData_array(data, j - 1);
            }

            // now we need to addaptate it to a 41 rows vector
            double[] valores_low = new double[A_u_12.Length];

            if (p==0)
            {

                valores_low[0] = valores[0];
                valores_low[4] = valores[1];
                valores_low[8] = valores[2];
                valores_low[12] = valores[3];
                valores_low[16] = valores[4];
                valores_low[20] = valores[5];
                valores_low[24] = valores[6];
                valores_low[28] = valores[7];
                valores_low[32] = valores[8];
                valores_low[36] = valores[9];
                valores_low[40] = valores[10];

                // interpolamos los valores que faltan
                valores_low[1] = valores_low[0]+(valores_low[0] - valores_low[4]) / (0 - 4) * (1 - 0);
                valores_low[2] = valores_low[0]+ (valores_low[0] - valores_low[4]) / (0 - 4) * (2 - 0);
                valores_low[3] = valores_low[0]+ (valores_low[0] - valores_low[4]) / (0 - 4) * (3 - 0);

                valores_low[5] = valores_low[4]+ (valores_low[4] - valores_low[10]) / (4 - 8) * (5 - 4);
                valores_low[6] = valores_low[4]+ (valores_low[4] - valores_low[10]) / (4 - 8) * (6 - 4);
                valores_low[7] = valores_low[4]+ (valores_low[4] - valores_low[10]) / (4 - 8) * (7 - 4);

                valores_low[9] = valores_low[8]+(valores_low[8] - valores_low[12]) / (8 - 12) * (9 - 8);
                valores_low[10] = valores_low[8]+(valores_low[8] - valores_low[12]) / (8 - 12) * (10 - 8);
                valores_low[11] = valores_low[8]+(valores_low[8] - valores_low[12]) / (8 - 12) * (11 - 8);

                valores_low[13] = valores_low[12]+(valores_low[12] - valores_low[16]) / (12 - 16) * (13 - 12);
                valores_low[14] = valores_low[12]+(valores_low[12] - valores_low[16]) / (12 - 16) * (14 - 12);
                valores_low[15] = valores_low[12]+(valores_low[12] - valores_low[16]) / (12 - 16) * (15 - 12);

                valores_low[17] = valores_low[16]+(valores_low[16] - valores_low[20]) / (20 - 16) * (17 - 16);
                valores_low[18] = valores_low[16]+(valores_low[16] - valores_low[20]) / (20 - 16) * (18 - 16);
                valores_low[19] = valores_low[16]+(valores_low[16] - valores_low[20]) / (20 - 16) * (19 - 16);

                valores_low[21] = valores_low[16]+(valores_low[20] - valores_low[24]) / (20 - 24) * (21 - 20);
                valores_low[22] = valores_low[16]+(valores_low[20] - valores_low[24]) / (20 - 24) * (22 - 20);
                valores_low[23] = valores_low[16]+(valores_low[20] - valores_low[24]) / (20 - 24) * (23 - 20);

                valores_low[25] = valores_low[24]+(valores_low[24] - valores_low[30]) / (24-30) * (25 - 24);
                valores_low[26] = valores_low[24] + (valores_low[24] - valores_low[30]) / (24 - 30) * (26 - 24);
                valores_low[27] = valores_low[24] + (valores_low[24] - valores_low[30]) / (24 - 30) * (27 - 24);

                valores_low[29] = valores_low[28] + (valores_low[28] - valores_low[32]) / (28-32) * (29 - 28);
                valores_low[30] = valores_low[28] + (valores_low[28] - valores_low[32]) / (28 - 32) * (30 - 28);
                valores_low[31] = valores_low[28] + (valores_low[28] - valores_low[32]) / (28 - 32) * (31 - 28);

                valores_low[33] = valores_low[32] + (valores_low[32] - valores_low[38]) / (32-38) * (33-32);
                valores_low[34] = valores_low[32] + (valores_low[32] - valores_low[38]) / (32 - 38) * (34-32);
                valores_low[35] = valores_low[32] + (valores_low[32] - valores_low[38]) / (32 - 38) * (35-32);

                valores_low[37] = valores_low[36] + (valores_low[36] - valores_low[40]) / (36 - 40) * (37 - 36);
                valores_low[38] = valores_low[36] + (valores_low[36] - valores_low[40]) / (36 - 40) * (38 - 36);
                valores_low[39] = valores_low[36] + (valores_low[36] - valores_low[40]) / (36 - 40) * (39- 36);
            }
            if (p == 2)
            {
                for (int y=0; y< A_u_12.Length; y++)
                {
                    valores_low[y] = valores[y*5];
                }
            }
            return valores_low;
        }
        #endregion INTERPOLATION FUNCTIONS

        #region COMPARE BUTTON
        private void Compare_btn_Click(object sender, RoutedEventArgs e)
        {
            #region NEW DATATABLE DEFINITIONS
            Anderson_u_12 = new DataTable();
             Anderson_v_12 = new DataTable();
             Anderson_rho_12 = new DataTable();
             Anderson_p_12 = new DataTable();
             Anderson_T_12 = new DataTable();
             Anderson_M_12 = new DataTable();
             Anderson_F1_12 = new DataTable();
             Anderson_F2_12 = new DataTable();
             Anderson_F3_12 = new DataTable();
             Anderson_F4_12 = new DataTable();
             Anderson_u_66 = new DataTable();
             Anderson_v_66 = new DataTable();
             Anderson_rho_66 = new DataTable();
             Anderson_p_66 = new DataTable();
             Anderson_T_66 = new DataTable();
             Anderson_M_66 = new DataTable();
             Anderson_F1_66 = new DataTable();
             Anderson_F2_66 = new DataTable();
             Anderson_F3_66 = new DataTable();
             Anderson_F4_66 = new DataTable();
             our_u_12 = new DataTable();
             our_v_12 = new DataTable();
             our_rho_12 = new DataTable();
             our_p_12 = new DataTable();
             our_T_12 = new DataTable();
             our_M_12 = new DataTable();
             our_F1_12 = new DataTable();
             our_F2_12 = new DataTable();
             our_F3_12 = new DataTable();
             our_F4_12 = new DataTable();
             our_u_66 = new DataTable();
             our_v_66 = new DataTable();
             our_rho_66 = new DataTable();
             our_p_66 = new DataTable();
             our_T_66 = new DataTable();
             our_M_66 = new DataTable();
             our_F1_66 = new DataTable();
             our_F2_66 = new DataTable();
             our_F3_66 = new DataTable();
             our_F4_66 = new DataTable();
            #endregion NEW DATATABLE DEFINITIONS

            #region FUNCIONTS
            Fill_Anderson_Tables();
            Fill_Our_Tables();
            Compute_Errors();
            #endregion FUNCIONTS

            #region CHANGE WHAT IT IS SHOWN
            if (Setect_Parameter_ComboBox.SelectedIndex == 0 && Setect_X_ComboBox.SelectedIndex==0) //T AND 12
            { AndersonGridData.DataContext = Anderson_T_12.DefaultView;
              SimulationGrid.DataContext = our_T_12.DefaultView;

                anderson_error.Visibility= Visibility.Hidden;
                anderson_label.Visibility= Visibility.Hidden;
                computed_label.Visibility= Visibility.Hidden;
                our_error.Visibility= Visibility.Hidden;

            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 0 && Setect_X_ComboBox.SelectedIndex == 1) //T AND 66
            {AndersonGridData.DataContext = Anderson_T_66.DefaultView;
             SimulationGrid.DataContext = our_T_66.DefaultView;

                anderson_error.Visibility = Visibility.Visible;
                anderson_label.Visibility = Visibility.Visible;
                computed_label.Visibility = Visibility.Visible;
                our_error.Visibility = Visibility.Visible;

                anderson_error.Text = Convert.ToString(Anderson_t_error);
                our_error.Text = Convert.ToString(our_t_error);
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 1 && Setect_X_ComboBox.SelectedIndex == 0) //u AND 12
            { AndersonGridData.DataContext = Anderson_u_12.DefaultView;
              SimulationGrid.DataContext = our_u_12.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 1 && Setect_X_ComboBox.SelectedIndex == 1) //u AND 66
            { AndersonGridData.DataContext = Anderson_u_66.DefaultView;
              SimulationGrid.DataContext = our_u_66.DefaultView;

                anderson_error.Visibility = Visibility.Visible;
                anderson_label.Visibility = Visibility.Visible;
                computed_label.Visibility = Visibility.Visible;
                our_error.Visibility = Visibility.Visible;

                anderson_error.Text = Convert.ToString(Anderson_u_error);
                our_error.Text = Convert.ToString(our_u_error);
            }

            if (Setect_Parameter_ComboBox.SelectedIndex == 2 && Setect_X_ComboBox.SelectedIndex == 0) //v AND 12
            { AndersonGridData.DataContext = Anderson_v_12.DefaultView;
              SimulationGrid.DataContext = our_v_12.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 2 && Setect_X_ComboBox.SelectedIndex == 1) //v AND 66
            { AndersonGridData.DataContext = Anderson_v_66.DefaultView;
              SimulationGrid.DataContext = our_v_12.DefaultView;

                anderson_error.Visibility = Visibility.Visible;
                anderson_label.Visibility = Visibility.Visible;
                computed_label.Visibility = Visibility.Visible;
                our_error.Visibility = Visibility.Visible;

                anderson_error.Text = Convert.ToString(Anderson_v_error);
                our_error.Text = Convert.ToString(our_v_error);
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 3 && Setect_X_ComboBox.SelectedIndex == 0) //rho AND 12
            { AndersonGridData.DataContext = Anderson_rho_12.DefaultView;
              SimulationGrid.DataContext = our_rho_12.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 3 && Setect_X_ComboBox.SelectedIndex == 1) //rho AND 66
            { AndersonGridData.DataContext = Anderson_rho_66.DefaultView;
              SimulationGrid.DataContext = our_rho_12.DefaultView;

                anderson_error.Visibility = Visibility.Visible;
                anderson_label.Visibility = Visibility.Visible;
                computed_label.Visibility = Visibility.Visible;
                our_error.Visibility = Visibility.Visible;

                anderson_error.Text = Convert.ToString(Anderson_rho_error);
                our_error.Text = Convert.ToString(our_rho_error);
            }

            if (Setect_Parameter_ComboBox.SelectedIndex == 4 && Setect_X_ComboBox.SelectedIndex == 0) //p AND 12
            { AndersonGridData.DataContext = Anderson_p_12.DefaultView;
              SimulationGrid.DataContext = our_p_12.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 4 && Setect_X_ComboBox.SelectedIndex == 1) //p AND 66
            { AndersonGridData.DataContext = Anderson_p_66.DefaultView;
              SimulationGrid.DataContext = our_p_66.DefaultView;

                anderson_error.Visibility = Visibility.Visible;
                anderson_label.Visibility = Visibility.Visible;
                computed_label.Visibility = Visibility.Visible;
                our_error.Visibility = Visibility.Visible;

                anderson_error.Text = Convert.ToString(Anderson_p_error);
                our_error.Text = Convert.ToString(our_p_error);
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 5 && Setect_X_ComboBox.SelectedIndex == 0) //M AND 12
            { AndersonGridData.DataContext = Anderson_M_12.DefaultView;
              SimulationGrid.DataContext = our_M_12.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 5 && Setect_X_ComboBox.SelectedIndex == 1) //M AND 66
            { AndersonGridData.DataContext = Anderson_M_66.DefaultView;
              SimulationGrid.DataContext = our_M_66.DefaultView;

                anderson_error.Visibility = Visibility.Visible;
                anderson_label.Visibility = Visibility.Visible;
                computed_label.Visibility = Visibility.Visible;
                our_error.Visibility = Visibility.Visible;

                anderson_error.Text = Convert.ToString(Anderson_M_error);
                our_error.Text = Convert.ToString(our_M_error);
            }

            if (Setect_Parameter_ComboBox.SelectedIndex == 6 && Setect_X_ComboBox.SelectedIndex == 0) //F1 AND 12
            { AndersonGridData.DataContext = Anderson_F1_12.DefaultView;
              SimulationGrid.DataContext = our_F1_12.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 6 && Setect_X_ComboBox.SelectedIndex == 1) //F1 AND 66
            { AndersonGridData.DataContext = Anderson_F1_66.DefaultView;
              SimulationGrid.DataContext = our_F1_66.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }

            if (Setect_Parameter_ComboBox.SelectedIndex == 7 && Setect_X_ComboBox.SelectedIndex == 0) //F2 AND 12
            { AndersonGridData.DataContext = Anderson_F2_12.DefaultView;
              SimulationGrid.DataContext = our_F2_12.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 7 && Setect_X_ComboBox.SelectedIndex == 1) //F2 AND 66
            { AndersonGridData.DataContext = Anderson_F2_66.DefaultView;
              SimulationGrid.DataContext = our_F2_66.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }

            if (Setect_Parameter_ComboBox.SelectedIndex == 8 && Setect_X_ComboBox.SelectedIndex == 0) //F3 AND 12
            { AndersonGridData.DataContext = Anderson_F3_12.DefaultView;
              SimulationGrid.DataContext = our_F3_12.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 8 && Setect_X_ComboBox.SelectedIndex == 1) //F3 AND 66
            { AndersonGridData.DataContext = Anderson_F3_66.DefaultView;
              SimulationGrid.DataContext = our_F3_66.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }

            if (Setect_Parameter_ComboBox.SelectedIndex == 9 && Setect_X_ComboBox.SelectedIndex == 0) //F4 AND 12
            { AndersonGridData.DataContext = Anderson_F4_12.DefaultView;
              SimulationGrid.DataContext = our_F4_12.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }
            if (Setect_Parameter_ComboBox.SelectedIndex == 9 && Setect_X_ComboBox.SelectedIndex == 1) //F4 AND 66
            { AndersonGridData.DataContext = Anderson_F4_66.DefaultView;
              SimulationGrid.DataContext = our_F4_66.DefaultView;

                anderson_error.Visibility = Visibility.Hidden;
                anderson_label.Visibility = Visibility.Hidden;
                computed_label.Visibility = Visibility.Hidden;
                our_error.Visibility = Visibility.Hidden;
            }
            #endregion CHANGE WHAT IT IS SHOWN
        }
        #endregion COMPARE BUTTON


        
    }
}
