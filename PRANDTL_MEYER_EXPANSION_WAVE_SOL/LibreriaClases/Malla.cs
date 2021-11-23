﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LibreriaClases
{
    class Malla
    {
        int divisiones_eta=41; // nº filas -- i
        int divisiones_xi = 81; // nº columnas -- j
        double delta_y_t = 0.025;
        double Cy = 0.5;
        Normas norma = new Normas();
        Celda[,] matriz;


        //Tables with the data of all iterations
        //DataTable Temperature_table = new DataTable("All Temperature values [K]");
        //DataTable Mach_table = new DataTable("All Mach values");
        //DataTable F1_table = new DataTable("All F1 values");
            

        //Tables for comparing with Anderson


        public void DefinirMatriz(int filas, int columnas)
        {

            this.divisiones_eta = filas;
            this.divisiones_xi = columnas;

            this.matriz = new Celda[filas, columnas];

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
                matriz[i, 0].v = norma.u_in;
                matriz[i, 0].T = norma.T_in;
                matriz[i, 0].P = norma.T_in;
                matriz[i, 0].Rho = norma.Rho_in;
                matriz[i, 0].M_angle = norma.M_angle;
                matriz[i, 0].Compute_G_F(norma.Gamma);

            }
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
                    for (int i = 0; i < divisiones_eta; i++)
                    {
                        matriz[i, j].xy_Transformation_ToEtaXi(norma.H, norma.E, norma.Theta);
                        double[] TanMax_deltax = new double[2];
                        TanMax_deltax = matriz[i, j].StepSize_TanMax(norma.Theta, matriz[2, j].y - matriz[2, j].y);

                        double[] F1_F2_F3_F4_p_derecha_vector = new double[4];
                        if (i==0)
                        {
                            F1_F2_F3_F4_p_derecha_vector = matriz[i, j].Predictor_Step_Contorno_Inferior(delta_y_t, TanMax_deltax[1], matriz[i + 1, j].F1, matriz[i + 1, j].F2, matriz[i + 1, j].F3, matriz[i + 1, j].F4, matriz[i + 1, j].G1, matriz[i + 1, j].G2, matriz[i + 1, j].G3, matriz[i + 1, j].G4);
                        }
                        if (i == divisiones_eta - 1)
                        {
                            F1_F2_F3_F4_p_derecha_vector = matriz[i, j].Predictor_Step_Contorno_Superior(delta_y_t, TanMax_deltax[1], matriz[i - 1, j].F1, matriz[i - 1, j].F2, matriz[i - 1, j].F3, matriz[i - 1, j].F4, matriz[i - 1, j].G1, matriz[i - 1, j].G2, matriz[i - 1, j].G3, matriz[i - 1, j].G4);
                        }
                        if (i > 0 && i < divisiones_eta)
                        {
                            F1_F2_F3_F4_p_derecha_vector = matriz[i, j].Predictor_Step_Principal(Cy, delta_y_t, TanMax_deltax[1], matriz[i + 1, j].F1, matriz[i + 1, j].F2, matriz[i + 1, j].F3, matriz[i + 1, j].F4, matriz[i - 1, j].F1, matriz[i - 1, j].F2, matriz[i - 1, j].F3, matriz[i - 1, j].F4, matriz[i + 1, j].G1, matriz[i + 1, j].G2, matriz[i + 1, j].G3, matriz[i + 1, j].G4, matriz[i + 1, j].P, matriz[i - 1, j].P);
                        }

                        matriz[i, j + 1].F1_p = F1_F2_F3_F4_p_derecha_vector[0];
                        matriz[i, j + 1].F2_p = F1_F2_F3_F4_p_derecha_vector[1];
                        matriz[i, j + 1].F3_p = F1_F2_F3_F4_p_derecha_vector[2];
                        matriz[i, j + 1].F4_p = F1_F2_F3_F4_p_derecha_vector[3];


                        double[] G1p_G2p_G3p_G4p_Rhop_Pp = new double[6];
                        G1p_G2p_G3p_G4p_Rhop_Pp= matriz[i, j].Gp_Rhop_Pp_Predicted(norma.Gamma, F1_F2_F3_F4_p_derecha_vector[0], F1_F2_F3_F4_p_derecha_vector[1], F1_F2_F3_F4_p_derecha_vector[2], F1_F2_F3_F4_p_derecha_vector[3]);

                        matriz[i, j + 1].G1_p = G1p_G2p_G3p_G4p_Rhop_Pp[0];
                        matriz[i, j + 1].G2_p = G1p_G2p_G3p_G4p_Rhop_Pp[1];
                        matriz[i, j + 1].G3_p = G1p_G2p_G3p_G4p_Rhop_Pp[2]; 
                        matriz[i, j + 1].G4_p = G1p_G2p_G3p_G4p_Rhop_Pp[3];
                        matriz[i, j + 1].Rho_p = G1p_G2p_G3p_G4p_Rhop_Pp[4];
                        matriz[i, j + 1].P_p = G1p_G2p_G3p_G4p_Rhop_Pp[5];



                        double[] F1_F2_F3_F4_derecha_corrected = new double[4];
                        if (i == 0)
                        {
                            F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Contorno_Inferior(matriz[i, j].F1_p, matriz[i, j].F2_p, matriz[i, j].F3_p, matriz[i, j].F4_p, matriz[i + 1, j].F1_p,
                                matriz[i + 1, j].F2_p, matriz[i + 1, j].F3_p, matriz[i + 1, j].F4_p, matriz[i, j].G1_p, matriz[i, j].G2_p, matriz[i, j].G3_p, matriz[i, j].G4_p, matriz[i + 1, j].G1_p,
                                matriz[i + 1, j].G2_p, matriz[i + 1, j].G3_p, matriz[i + 1, j].G4_p, TanMax_deltax[1]);
                        }
                        if (i == divisiones_eta - 1)
                        {
                            F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Contorno_Superior(matriz[i, j].F1_p, matriz[i, j].F2_p, matriz[i, j].F3_p, matriz[i, j].F4_p, matriz[i - 1, j].F1_p,
                                matriz[i - 1, j].F2_p, matriz[i - 1, j].F3_p, matriz[i - 1, j].F4_p, matriz[i, j].G1_p, matriz[i, j].G2_p, matriz[i, j].G3_p, matriz[i, j].G4_p, matriz[i - 1, j].G1_p,
                                matriz[i - 1, j].G2_p, matriz[i - 1, j].G3_p, matriz[i - 1, j].G4_p, TanMax_deltax[1]);

                        }
                        if (i > 0 && i < divisiones_eta)
                        {
                            F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Principal(Cy, TanMax_deltax[1], delta_y_t, matriz[i - 1, j].F1_p, matriz[i - 1, j].F2_p, matriz[i - 1, j].F3_p, matriz[i - 1, j].F4_p, matriz[i, j].F1_p, matriz[i, j].F2_p, matriz[i, j].F3_p,
                          matriz[i, j].F4_p, matriz[i - 1, j].G1_p, matriz[i - 1, j].G2_p, matriz[i - 1, j].G3_p, matriz[i - 1, j].G4_p, matriz[i, j].G1_p, matriz[i, j].G2_p, matriz[i, j].G3_p, matriz[i, j].G4_p,
                          matriz[i + 1, j].F1_p, matriz[i + 1, j].F2_p, matriz[i + 1, j].F3_p, matriz[i + 1, j].F4_p, matriz[i + 1, j].P_p, matriz[i, j].P_p, matriz[i - 1, j].P_p);

                        }

                        matriz[i, j + 1].F1 = F1_F2_F3_F4_derecha_corrected[0];
                        matriz[i, j + 1].F2 = F1_F2_F3_F4_derecha_corrected[1];
                        matriz[i, j + 1].F3 = F1_F2_F3_F4_derecha_corrected[2];
                        matriz[i, j + 1].F4 = F1_F2_F3_F4_derecha_corrected[3];


                        double[] resultados = new double[10];
                        if (i == 0)
                        {
                            resultados = matriz[i, j+1].Wall_Bounday_Condition(norma.Gamma, norma.R_air, norma.E, norma.Theta);
                        }

                        if (i != 0)
                        {
                           resultados= matriz[i, j+1].ComputeFinalValues(norma.Gamma, norma.R_air);
                        }

                        

                    }



                }

            }


        }






    }
}
