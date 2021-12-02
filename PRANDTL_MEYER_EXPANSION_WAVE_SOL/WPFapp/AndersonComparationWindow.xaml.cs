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
        }

        public int filas { get; set; }
        public Malla m { get; set; }

        DataTable Anderson_u_12 = new DataTable();
        DataTable Anderson_v_12 = new DataTable();
        DataTable Anderson_rho_12 = new DataTable();
        DataTable Anderson_p_12 = new DataTable();
        DataTable Anderson_T_12 = new DataTable();
        DataTable Anderson_M_12 = new DataTable();
        DataTable Anderson_F1_12 = new DataTable();
        DataTable Anderson_F2_12 = new DataTable();
        DataTable Anderson_F3_12 = new DataTable();
        DataTable Anderson_F4_12 = new DataTable();

        DataTable Anderson_u_66 = new DataTable();
        DataTable Anderson_v_66 = new DataTable();
        DataTable Anderson_rho_66 = new DataTable();
        DataTable Anderson_p_66 = new DataTable();
        DataTable Anderson_T_66 = new DataTable();
        DataTable Anderson_M_66 = new DataTable();
        DataTable Anderson_F1_66 = new DataTable();
        DataTable Anderson_F2_66 = new DataTable();
        DataTable Anderson_F3_66 = new DataTable();
        DataTable Anderson_F4_66 = new DataTable();

        double[] A_u_12 = {707,701,691,683,679,678,678,678,678,678,678,678,678,678,678,678,678,678,678,678, 678,678,678,678,678,678,678,678,678,678,678};
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
        double[] A_T_66 = { };
        double[] A_M_66 = { };
        double[] A_F1_66 = { };
        double[] A_F2_66 = { };
        double[] A_F3_66 = { };
        double[] A_F4_66 = { };


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

            for (int i = 0; i < filas; i++)
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

        public void Our_results()
        {
            

        }


        public void Interpolate_our_results(double x_choosen)
        {
            double[] valores1;
            double[] valores2;
            double[] valores;

            int j = 0;
            bool valor_exacto = false;

            for (j=0; m.matriz[0, j].x <= x_choosen; j++)
            {

                if (m.matriz[0, j].x == x_choosen)
                {
                    valor_exacto = true;
                }

            }

            if (valor_exacto == false)
            {
                double last_j = j - 1; //last value checked with x smaller that the choosen x 

                // we interpolate between values at j and values at j-1
                for (int i=0; i<m.rows;i++)
                {




                }
            }

        }

        private void Compare_btn_Click(object sender, RoutedEventArgs e)
        {
            Fill_Anderson_Tables();

            if (Setect_Parameter_ComboBox.SelectedIndex == 0 && Setect_X_ComboBox.SelectedIndex==0) //T AND 12
            {AndersonGridData.DataContext = Anderson_T_12.DefaultView;}
            if (Setect_Parameter_ComboBox.SelectedIndex == 0 && Setect_X_ComboBox.SelectedIndex == 1) //T AND 66
            {AndersonGridData.DataContext = Anderson_T_66.DefaultView;}
            if (Setect_Parameter_ComboBox.SelectedIndex == 1 && Setect_X_ComboBox.SelectedIndex == 0) //u AND 12
            { AndersonGridData.DataContext = Anderson_u_12.DefaultView; }
            if (Setect_Parameter_ComboBox.SelectedIndex == 1 && Setect_X_ComboBox.SelectedIndex == 1) //u AND 66
            { AndersonGridData.DataContext = Anderson_u_66.DefaultView; }

            if (Setect_Parameter_ComboBox.SelectedIndex == 2 && Setect_X_ComboBox.SelectedIndex == 0) //v AND 12
            { AndersonGridData.DataContext = Anderson_v_12.DefaultView; }
            if (Setect_Parameter_ComboBox.SelectedIndex == 2 && Setect_X_ComboBox.SelectedIndex == 1) //v AND 66
            { AndersonGridData.DataContext = Anderson_v_66.DefaultView; }

            if (Setect_Parameter_ComboBox.SelectedIndex == 3 && Setect_X_ComboBox.SelectedIndex == 0) //rho AND 12
            { AndersonGridData.DataContext = Anderson_rho_12.DefaultView; }
            if (Setect_Parameter_ComboBox.SelectedIndex == 3 && Setect_X_ComboBox.SelectedIndex == 1) //rho AND 66
            { AndersonGridData.DataContext = Anderson_rho_66.DefaultView; }

            if (Setect_Parameter_ComboBox.SelectedIndex == 4 && Setect_X_ComboBox.SelectedIndex == 0) //p AND 12
            { AndersonGridData.DataContext = Anderson_p_12.DefaultView; }
            if (Setect_Parameter_ComboBox.SelectedIndex == 4 && Setect_X_ComboBox.SelectedIndex == 1) //p AND 66
            { AndersonGridData.DataContext = Anderson_p_66.DefaultView; }

            if (Setect_Parameter_ComboBox.SelectedIndex == 5 && Setect_X_ComboBox.SelectedIndex == 0) //M AND 12
            { AndersonGridData.DataContext = Anderson_M_12.DefaultView; }
            if (Setect_Parameter_ComboBox.SelectedIndex == 5 && Setect_X_ComboBox.SelectedIndex == 1) //M AND 66
            { AndersonGridData.DataContext = Anderson_M_66.DefaultView; }

            if (Setect_Parameter_ComboBox.SelectedIndex == 6 && Setect_X_ComboBox.SelectedIndex == 0) //F1 AND 12
            { AndersonGridData.DataContext = Anderson_F1_12.DefaultView; }
            if (Setect_Parameter_ComboBox.SelectedIndex == 6 && Setect_X_ComboBox.SelectedIndex == 1) //F1 AND 66
            { AndersonGridData.DataContext = Anderson_F1_66.DefaultView; }

            if (Setect_Parameter_ComboBox.SelectedIndex == 7 && Setect_X_ComboBox.SelectedIndex == 0) //F2 AND 12
            { AndersonGridData.DataContext = Anderson_F2_12.DefaultView; }
            if (Setect_Parameter_ComboBox.SelectedIndex == 7 && Setect_X_ComboBox.SelectedIndex == 1) //F2 AND 66
            { AndersonGridData.DataContext = Anderson_F2_66.DefaultView; }

            if (Setect_Parameter_ComboBox.SelectedIndex == 8 && Setect_X_ComboBox.SelectedIndex == 0) //F3 AND 12
            { AndersonGridData.DataContext = Anderson_F3_12.DefaultView; }
            if (Setect_Parameter_ComboBox.SelectedIndex == 8 && Setect_X_ComboBox.SelectedIndex == 1) //F3 AND 66
            { AndersonGridData.DataContext = Anderson_F3_66.DefaultView; }

            if (Setect_Parameter_ComboBox.SelectedIndex == 9 && Setect_X_ComboBox.SelectedIndex == 0) //F4 AND 12
            { AndersonGridData.DataContext = Anderson_F4_12.DefaultView; }
            if (Setect_Parameter_ComboBox.SelectedIndex == 9 && Setect_X_ComboBox.SelectedIndex == 1) //F4 AND 66
            { AndersonGridData.DataContext = Anderson_F4_66.DefaultView; }
        }



    }
}
