using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace LibreriaClases
{
    public class Malla
    {
        int rows=41; //   j filas
        int columns = 120; // i les columnas
        double delta_y_t = 0.025;
        double Cy = 0.6;
        double C = 0.5;

        double delta_x;
        double delta_y;

        double delta_xi;

        Normas norma = new Normas();
        Celda[,] matriz;

        public void DefinirMatriz()
        {

            this.matriz = new Celda[rows, columns];

            //We fill the matrix with Cells
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    matriz[j, i] = new Celda();
                }
            }

        }

        public void Compute()
        {
            norma.Compute_a();
            norma.Compute_M_angle();
            norma.Compute_u();

            // We define the initial line conditions
            for (int j = 0; j < rows; j++)
            {

                matriz[j, 0].a = norma.a_in;
                matriz[j, 0].M = norma.M_in;
                matriz[j, 0].u = norma.u_in;
                matriz[j, 0].v = norma.v_in;
                matriz[j, 0].T = norma.T_in;
                matriz[j, 0].P = norma.P_in;
                matriz[j, 0].Rho = norma.Rho_in;
                matriz[j, 0].M_angle = norma.M_angle;
                matriz[j, 0].Compute_G_F(norma.Gamma);

            }


            //We define all the y_t values of the matrix
            for (int i = 0; i < columns; i++)
            {
                for (int j = 1; j < rows; j++)
                {
                    matriz[j, i].Compute_y_t(matriz[j - 1, i].y_t, delta_y_t);
                }
            }


            for (int j = 0; matriz[0, j].x <= norma.L; j++)
            {

                for (int i = 0; i < rows; i++)
                {
                    matriz[i, j].xy_Transformation_ToEtaXi(norma.H, norma.E, norma.Theta);
                }

                double[] max_tan_Array = new double[rows];

                for (int i = 0; i < rows; i++)
                {
                    delta_y = matriz[2, j].y - matriz[1, j].y; // mirar si posar 1 i 0 afecta
                    max_tan_Array[i] = matriz[i, j].TanMax(norma.Theta);

                }
                double max_tan = max_tan_Array.Max();
                delta_x = C * delta_y / max_tan;
                delta_xi = delta_x;
                for (int i = 0; i < rows; i++)
                {
                    matriz[i,j+1].x = matriz[i, j].x + delta_x;
                }

                for (int i = 0; i < rows; i++)
                    {
                        double[] F1_F2_F3_F4_p_derecha_vector;
                        if (i == 0)
                        {
                            F1_F2_F3_F4_p_derecha_vector = matriz[i, j].Predictor_Step_Contorno_Inferior(delta_y_t, delta_xi, matriz[i + 1, j].F1, matriz[i + 1, j].F2, 
                                                                                                         matriz[i + 1, j].F3, matriz[i + 1, j].F4, matriz[i + 1, j].G1, 
                                                                                                         matriz[i + 1, j].G2, matriz[i + 1, j].G3, matriz[i + 1, j].G4);
                        }
                        else if (i == rows - 1)
                        {
                            F1_F2_F3_F4_p_derecha_vector = matriz[i, j].Predictor_Step_Contorno_Superior(delta_y_t, delta_xi, matriz[i - 1, j].F1, matriz[i - 1, j].F2, 
                                                                                                         matriz[i - 1, j].F3, matriz[i - 1, j].F4, matriz[i - 1, j].G1, 
                                                                                                         matriz[i - 1, j].G2, matriz[i - 1, j].G3, matriz[i - 1, j].G4);
                        }
                        else
                        {
                            F1_F2_F3_F4_p_derecha_vector = matriz[i, j].Predictor_Step_Principal(Cy, delta_y_t, delta_xi, matriz[i + 1, j].F1, matriz[i + 1, j].F2,
                                                                                                 matriz[i + 1, j].F3, matriz[i + 1, j].F4, matriz[i - 1, j].F1,
                                                                                                 matriz[i - 1, j].F2, matriz[i - 1, j].F3, matriz[i - 1, j].F4,
                                                                                                 matriz[i + 1, j].G1, matriz[i + 1, j].G2, matriz[i + 1, j].G3,
                                                                                                 matriz[i + 1, j].G4, matriz[i + 1, j].P, matriz[i - 1, j].P);
                        }
                        matriz[i, j + 1].F1_p = F1_F2_F3_F4_p_derecha_vector[0];
                        matriz[i, j + 1].F2_p = F1_F2_F3_F4_p_derecha_vector[1];
                        matriz[i, j + 1].F3_p = F1_F2_F3_F4_p_derecha_vector[2];
                        matriz[i, j + 1].F4_p = F1_F2_F3_F4_p_derecha_vector[3];
                    }



                    for (int i = 0; i < rows; i++)
                    {
                        double[] G1p_G2p_G3p_G4p_Rhop_Pp = matriz[i, j].Gp_Rhop_Pp_Predicted(norma.Gamma, matriz[i, j + 1].F1_p, matriz[i, j + 1].F2_p, 
                                                                                            matriz[i, j + 1].F3_p, matriz[i, j + 1].F4_p);

                        matriz[i, j + 1].G1_p = G1p_G2p_G3p_G4p_Rhop_Pp[0];
                        matriz[i, j + 1].G2_p = G1p_G2p_G3p_G4p_Rhop_Pp[1];
                        matriz[i, j + 1].G3_p = G1p_G2p_G3p_G4p_Rhop_Pp[2];
                        matriz[i, j + 1].G4_p = G1p_G2p_G3p_G4p_Rhop_Pp[3];
                        matriz[i, j + 1].Rho_p = G1p_G2p_G3p_G4p_Rhop_Pp[4];
                        matriz[i, j + 1].P_p = G1p_G2p_G3p_G4p_Rhop_Pp[5];
                    }

                    for (int i = 0; i < rows; i++)
                    {
                        double[] F1_F2_F3_F4_derecha_corrected;

                        if (i == 0)
                        {
                            F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Contorno_Inferior(delta_y_t, matriz[i, j+1].F1_p, matriz[i, j+1].F2_p, matriz[i, j+1].F3_p, matriz[i, j+1].F4_p, matriz[i + 1, j+1].F1_p,
                                matriz[i + 1, j+1].F2_p, matriz[i + 1, j+1].F3_p, matriz[i + 1, j+1].F4_p, matriz[i, j+1].G1_p, matriz[i, j+1].G2_p, matriz[i, j+1].G3_p, matriz[i, j+1].G4_p, matriz[i + 1, j+1].G1_p,
                                matriz[i + 1, j+1].G2_p, matriz[i + 1, j+1].G3_p, matriz[i + 1, j+1].G4_p, delta_xi);
                        }
                        else if (i == rows - 1)
                        {
                            F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Contorno_Superior(delta_y_t, matriz[i, j+1].F1_p, matriz[i, j+1].F2_p, matriz[i, j+1].F3_p, matriz[i, j+1].F4_p, matriz[i - 1, j+1].F1_p,
                                matriz[i - 1, j+1].F2_p, matriz[i - 1, j+1].F3_p, matriz[i - 1, j+1].F4_p, matriz[i, j+1].G1_p, matriz[i, j+1].G2_p, matriz[i, j+1].G3_p, matriz[i, j+1].G4_p, matriz[i - 1, j+1].G1_p,
                                matriz[i - 1, j+1].G2_p, matriz[i - 1, j+1].G3_p, matriz[i - 1, j+1].G4_p, delta_xi);

                        }
                        else
                        {
                            F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Principal(Cy, delta_xi, delta_y_t, 
                                matriz[i - 1, j+1].F1_p, 
                                matriz[i - 1, j+1].F2_p, 
                                matriz[i - 1, j+1].F3_p, 
                                matriz[i - 1, j+1].F4_p, 
                                matriz[i, j+1].F1_p, 
                                matriz[i, j+1].F2_p, 
                                matriz[i, j+1].F3_p,
                                matriz[i, j+1].F4_p, 
                                matriz[i - 1, j+1].G1_p, 
                                matriz[i - 1, j+1].G2_p, 
                                matriz[i - 1, j+1].G3_p, 
                                matriz[i - 1, j+1].G4_p, 
                                matriz[i, j+1].G1_p, 
                                matriz[i, j+1].G2_p, 
                                matriz[i, j+1].G3_p, 
                                matriz[i, j+1].G4_p, 
                                matriz[i + 1, j+1].F1_p, 
                                matriz[i + 1, j+1].F2_p, 
                                matriz[i + 1, j+1].F3_p, 
                                matriz[i + 1, j+1].F4_p, 
                                matriz[i + 1, j+1].P_p, 
                                matriz[i, j+1].P_p, 
                                matriz[i - 1, j+1].P_p);
                        }
                            matriz[i, j + 1].F1 = F1_F2_F3_F4_derecha_corrected[0];
                            matriz[i, j + 1].F2 = F1_F2_F3_F4_derecha_corrected[1];
                            matriz[i, j + 1].F3 = F1_F2_F3_F4_derecha_corrected[2];
                            matriz[i, j + 1].F4 = F1_F2_F3_F4_derecha_corrected[3];
                    }

                    for (int i = 0; i < rows; i++)
                    {
                        if (i == 0)
                        {
                            matriz[i, j + 1].Wall_Bounday_Condition(norma.Gamma, norma.R_air, norma.E, norma.Theta);
                        }

                        else
                        {
                            matriz[i, j + 1].ComputeFinalValues(norma.Gamma, norma.R_air);
                        }

                    }

                }

            }



    }
}
