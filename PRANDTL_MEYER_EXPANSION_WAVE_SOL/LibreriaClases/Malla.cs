using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LibreriaClases
{
    public class Malla
    {
        int divisiones_eta=41; // nº filas -- i
        int divisiones_xi = 81; // nº columnas -- j
        double delta_y_t = 0.025;
        double Cy = 0.5;
        Normas norma = new Normas();
        Celda[,] matriz;


        //Tables with the data of all iterations
        DataTable Temperature_table = new DataTable("All Temperature values [K]");
        DataTable u_table = new DataTable("All u values [m/s]");
        DataTable v_table = new DataTable("All v values [m/s]");
        DataTable Rho_table = new DataTable("All Rho values [kg/m3]");
        DataTable P_table = new DataTable("All P values [N/m3]");
        DataTable Mach_table = new DataTable("All Mach values");
        DataTable F1_table = new DataTable("All F1 values");
        DataTable F2_table = new DataTable("All F2 values");
        DataTable F3_table = new DataTable("All F3 values");
        DataTable F4_table = new DataTable("All F4 values");


        //Tables for comparing with Anderson


        public void DefinirMatriz()
        {

            this.matriz = new Celda[divisiones_eta, divisiones_xi];

            //We fill the matrix with Cells
            for (int i = 0; i < divisiones_eta; i++)
            {
                for (int j = 0; j < divisiones_xi; j++)
                {
                    matriz[i, j] = new Celda();
                }
            }

        }

        public void Compute()
        {
            norma.Compute_a();
            norma.Compute_M_angle();
            norma.Compute_u();

            // We define the initial line conditions
            for (int i = 0; i < divisiones_eta; i++)
            {

                matriz[i, 0].a = norma.a_in;
                matriz[i, 0].M = norma.M_in;
                matriz[i, 0].u = norma.u_in;
                matriz[i, 0].v = norma.v_in;
                matriz[i, 0].T = norma.T_in;
                matriz[i, 0].P = norma.P_in;
                matriz[i, 0].Rho = norma.Rho_in;
                matriz[i, 0].M_angle = norma.M_angle;
                matriz[i, 0].Compute_G_F(norma.Gamma);

            }

            //We define all the y_t values of the matrix
            for (int j = 0; j < divisiones_xi; j++)
            {
                for (int i = 1; i < divisiones_eta; i++)
                {
                    matriz[i, j].Compute_y_t(matriz[i - 1, j].y_t, delta_y_t);
                }
            }


            for (int j = 0; j < divisiones_xi; j++)
            {

                if (matriz[0,j].x<=norma.L)
                {
                    double TanMax = -5; // we define a negative value so any abs value would be bigger
                    for (int i = 0; i < divisiones_eta; i++)
                    {
                        matriz[i, j].xy_Transformation_ToEtaXi(norma.H, norma.E, norma.Theta);
                        double TanMax2 = matriz[i, j].TanMax(norma.Theta);
                        if (TanMax2>TanMax)
                        { TanMax = TanMax2; }
                    }
                    double delta_y = matriz[1, j].y - matriz[0, j].y;
                    double delta_x = 0.5 * delta_y / TanMax;

                    // we set all the delta_x values
                    for (int i=0; i<divisiones_eta; i++)
                    {
                        matriz[i, j].delta_x = delta_x;
                    }

                    for (int i = 0; i < divisiones_eta; i++)
                    {
                        double[] F1_F2_F3_F4_p_derecha_vector = new double[4];
                        if (i == 0)
                        {
                            F1_F2_F3_F4_p_derecha_vector = matriz[i, j].Predictor_Step_Contorno_Inferior(delta_y_t, matriz[i,j].delta_x, matriz[i + 1, j].F1, matriz[i + 1, j].F2, matriz[i + 1, j].F3, matriz[i + 1, j].F4, matriz[i + 1, j].G1, matriz[i + 1, j].G2, matriz[i + 1, j].G3, matriz[i + 1, j].G4);
                        }
                        if (i == divisiones_eta - 1)
                        {
                            F1_F2_F3_F4_p_derecha_vector = matriz[i, j].Predictor_Step_Contorno_Superior(delta_y_t, matriz[i, j].delta_x, matriz[i - 1, j].F1, matriz[i - 1, j].F2, matriz[i - 1, j].F3, matriz[i - 1, j].F4, matriz[i - 1, j].G1, matriz[i - 1, j].G2, matriz[i - 1, j].G3, matriz[i - 1, j].G4);
                        }
                        if (i > 0 && i < divisiones_eta - 1)
                        {
                            F1_F2_F3_F4_p_derecha_vector = matriz[i, j].Predictor_Step_Principal(Cy, delta_y_t, matriz[i, j].delta_x, matriz[i + 1, j].F1, matriz[i + 1, j].F2, matriz[i + 1, j].F3, matriz[i + 1, j].F4, matriz[i - 1, j].F1, matriz[i - 1, j].F2, matriz[i - 1, j].F3, matriz[i - 1, j].F4, matriz[i + 1, j].G1, matriz[i + 1, j].G2, matriz[i + 1, j].G3, matriz[i + 1, j].G4, matriz[i + 1, j].P, matriz[i - 1, j].P);
                        }

                        if (j < divisiones_xi - 1)
                        {
                            matriz[i, j + 1].F1_p = F1_F2_F3_F4_p_derecha_vector[0];
                            matriz[i, j + 1].F2_p = F1_F2_F3_F4_p_derecha_vector[1];
                            matriz[i, j + 1].F3_p = F1_F2_F3_F4_p_derecha_vector[2];
                            matriz[i, j + 1].F4_p = F1_F2_F3_F4_p_derecha_vector[3];
                        }
                    }

                    for (int i = 0; i < divisiones_eta; i++)
                    {
                        if (j < divisiones_xi - 1)
                        {
                            double[] G1p_G2p_G3p_G4p_Rhop_Pp = matriz[i, j].Gp_Rhop_Pp_Predicted(norma.Gamma, matriz[i, j + 1].F1_p, matriz[i, j + 1].F2_p, matriz[i, j + 1].F3_p, matriz[i, j + 1].F4_p);

                            if (j < divisiones_xi - 1)
                            {
                                matriz[i, j + 1].G1_p = G1p_G2p_G3p_G4p_Rhop_Pp[0];
                                matriz[i, j + 1].G2_p = G1p_G2p_G3p_G4p_Rhop_Pp[1];
                                matriz[i, j + 1].G3_p = G1p_G2p_G3p_G4p_Rhop_Pp[2];
                                matriz[i, j + 1].G4_p = G1p_G2p_G3p_G4p_Rhop_Pp[3];
                                matriz[i, j + 1].Rho_p = G1p_G2p_G3p_G4p_Rhop_Pp[4];
                                matriz[i, j + 1].P_p = G1p_G2p_G3p_G4p_Rhop_Pp[5];
                            }
                        }
                    }
                    for (int i = 0; i < divisiones_eta; i++)
                    {

                        double[] F1_F2_F3_F4_derecha_corrected = new double[4];
                        if ((i == 0)&&(j<divisiones_xi-1))
                        {
                            F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Contorno_Inferior(delta_y_t, matriz[i, j+1].F1_p, matriz[i, j+1].F2_p, matriz[i, j+1].F3_p, matriz[i, j+1].F4_p, matriz[i + 1, j+1].F1_p,
                                matriz[i + 1, j+1].F2_p, matriz[i + 1, j+1].F3_p, matriz[i + 1, j+1].F4_p, matriz[i, j+1].G1_p, matriz[i, j+1].G2_p, matriz[i, j+1].G3_p, matriz[i, j+1].G4_p, matriz[i + 1, j+1].G1_p,
                                matriz[i + 1, j+1].G2_p, matriz[i + 1, j+1].G3_p, matriz[i + 1, j+1].G4_p, matriz[i, j].delta_x);
                        }
                        if ((i == divisiones_eta - 1)&& (j < divisiones_xi - 1))
                        {
                            F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Contorno_Superior(delta_y_t, matriz[i, j+1].F1_p, matriz[i, j+1].F2_p, matriz[i, j+1].F3_p, matriz[i, j+1].F4_p, matriz[i - 1, j+1].F1_p,
                                matriz[i - 1, j+1].F2_p, matriz[i - 1, j+1].F3_p, matriz[i - 1, j+1].F4_p, matriz[i, j].G1_p, matriz[i, j+1].G2_p, matriz[i, j+1].G3_p, matriz[i, j+1].G4_p, matriz[i - 1, j+1].G1_p,
                                matriz[i - 1, j+1].G2_p, matriz[i - 1, j+1].G3_p, matriz[i - 1, j+1].G4_p, matriz[i, j].delta_x+1);

                        }
                        if ((i > 0 && i < divisiones_eta - 1) && (j < divisiones_xi - 1))
                        {
                            F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Principal(Cy, matriz[i, j].delta_x, delta_y_t, matriz[i - 1, j+1].F1_p, matriz[i - 1, j+1].F2_p, matriz[i - 1, j+1].F3_p, matriz[i - 1, j+1].F4_p, matriz[i-1, j+1].F1_p, matriz[i, j+1].F2_p, matriz[i, j+1].F3_p,
                          matriz[i, j+1].F4_p, matriz[i - 1, j].G1_p, matriz[i - 1, j+1].G2_p, matriz[i - 1, j+1].G3_p, matriz[i - 1, j+1].G4_p, matriz[i, j+1].G1_p, matriz[i, j].G2_p, matriz[i, j+1].G3_p, matriz[i, j+1].G4_p,
                          matriz[i + 1, j+1].F1_p, matriz[i + 1, j+1].F2_p, matriz[i + 1, j+1].F3_p, matriz[i + 1, j].F4_p, matriz[i + 1, j].P_p, matriz[i, j].P_p, matriz[i - 1, j].P_p);

                        }


                        if (j < divisiones_xi - 1)
                        {
                            matriz[i, j + 1].F1 = F1_F2_F3_F4_derecha_corrected[0];
                            matriz[i, j + 1].F2 = F1_F2_F3_F4_derecha_corrected[1];
                            matriz[i, j + 1].F3 = F1_F2_F3_F4_derecha_corrected[2];
                            matriz[i, j + 1].F4 = F1_F2_F3_F4_derecha_corrected[3];
                        }

                    }

                    for (int i = 0; i < divisiones_eta; i++)
                    {
                        if (i == 0 && j < divisiones_xi - 1)
                        {
                            matriz[i, j + 1].Wall_Bounday_Condition(norma.Gamma, norma.R_air, norma.E, norma.Theta);
                        }

                        if (i != 0 && j < divisiones_xi - 1)
                        {
                            matriz[i, j + 1].ComputeFinalValues(norma.Gamma, norma.R_air);
                        }

                        if (j < divisiones_xi - 1)
                        {
                            matriz[i, j + 1].x = matriz[i, j].x + matriz[i, j].delta_x;
                        }
                    }



                    }

                    

                }

            }


        public DataTable Fill_DataTable()
        {         
            
            for (int j=0;j<divisiones_xi; j++)
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

                Temperature_table.Columns.Add(TEMP_C);
                u_table.Columns.Add(U_C);
                v_table.Columns.Add(V_C);
                Rho_table.Columns.Add(RHO_C);
                P_table.Columns.Add(P_C);
                Mach_table.Columns.Add(M_C);
                F1_table.Columns.Add(F1_C);
                F2_table.Columns.Add(F2_C);
                F3_table.Columns.Add(F3_C);
                F4_table.Columns.Add(F4_C);
            }

            for (int i=0;i<divisiones_eta;i++)
            {
                DataRow TEMP_R = Temperature_table.NewRow();
                DataRow U_R = u_table.NewRow();
                DataRow V_R = v_table.NewRow();
                DataRow RHO_R = Rho_table.NewRow();
                DataRow P_R = P_table.NewRow();
                DataRow M_R = Mach_table.NewRow();
                DataRow F1_R = F1_table.NewRow();
                DataRow F2_R = F2_table.NewRow();
                DataRow F3_R = F3_table.NewRow();
                DataRow F4_R = F4_table.NewRow();

                for (int j = 0; j < divisiones_xi; j++)
                {
                    TEMP_R[j] = matriz[i, j].T;
                    U_R[j] = matriz[i, j].u;
                    V_R[j] = matriz[i, j].v;
                    RHO_R[j] = matriz[i, j].Rho;
                    P_R[j] = matriz[i, j].P;
                    M_R[j] = matriz[i, j].M;
                    F1_R[j] = matriz[i, j].F1;
                    F2_R[j] = matriz[i, j].F2;
                    F3_R[j] = matriz[i, j].F3;
                    F4_R[j] = matriz[i, j].F4;

                }

                Temperature_table.Rows.Add(TEMP_R);
                u_table.Rows.Add(U_R);
                v_table.Rows.Add(V_R);
                Rho_table.Rows.Add(RHO_R);
                P_table.Rows.Add(P_R);
                Mach_table.Rows.Add(M_R);
                F1_table.Rows.Add(F1_R);
                F2_table.Rows.Add(F2_R);
                F3_table.Rows.Add(F3_R);
                F4_table.Rows.Add(F4_R);

            }

            return Temperature_table;
        
        }


    }
}
