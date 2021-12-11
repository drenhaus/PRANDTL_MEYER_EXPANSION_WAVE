using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace LibreriaClases
{
    public class Malla
    {
        #region ATRIBUTES

        public int rows { get; set; }  //   j filas
        public int columns { get; set; }  // i les columnas
        public double delta_y_t { get; set; }
        double Cy = 0.6;
        double C = 0.5;

        public double delta_x { get; set; }
        double delta_y;

        double delta_xi;

        public double[] delta_y_array { get; set; }

        public Normas norma { get; set; } = new Normas();
        public Celda[,] matriz { get; set; }

        public List<double> listaDeXColumna = new List<double>();
        public List<double> listaTemperaturaxColumna = new List<double>();
        public List<double> listaMachxColumna = new List<double>();
        public List<double> listaDensidadxColumna = new List<double>();
        public List<double> listaPresurexColumna = new List<double>();
        public List<double> listaU_velxColumna = new List<double>();
        public List<double> listaV_velxColumna = new List<double>();


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

        #endregion ATRIBUTES 

        #region MATRIX DEFINITION
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
        //We compute the delta_y values
        public double[] Vector_Delta_y()
        {
            delta_y_array = new double[columns];
            for (int i = 0; i < columns; i++)
            {
                delta_y_array[i] = matriz[1, i].y - matriz[0, i].y;
                if (i == columns - 1)
                {
                    delta_y_array[i] = delta_y_array[i - 1];
                }
            }
            return delta_y_array;
        }

        #endregion MATRIX DEFINITION

        #region COMPUTE

        public void Compute()
        {

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

        public void Compute2(List<Celda> listaUltimaColumna)
        {

            int numerodeFilasdeLista = listaUltimaColumna.Count();
            // We define the initial line conditions
            for (int j = 0; j < rows; j++)
            {
                matriz[j, 0].a = listaUltimaColumna[j].a;
                matriz[j, 0].M = listaUltimaColumna[j].M;
                matriz[j, 0].u = listaUltimaColumna[j].u;
                matriz[j, 0].v = listaUltimaColumna[j].v;
                matriz[j, 0].T = listaUltimaColumna[j].T;
                matriz[j, 0].P = listaUltimaColumna[j].P;
                matriz[j, 0].Rho = listaUltimaColumna[j].Rho;
                matriz[j, 0].M_angle = listaUltimaColumna[j].M_angle;
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
                    matriz[i, j + 1].x = matriz[i, j].x + delta_x;
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
                        F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Contorno_Inferior(delta_y_t, matriz[i, j + 1].F1_p, matriz[i, j + 1].F2_p, matriz[i, j + 1].F3_p, matriz[i, j + 1].F4_p, matriz[i + 1, j + 1].F1_p,
                            matriz[i + 1, j + 1].F2_p, matriz[i + 1, j + 1].F3_p, matriz[i + 1, j + 1].F4_p, matriz[i, j + 1].G1_p, matriz[i, j + 1].G2_p, matriz[i, j + 1].G3_p, matriz[i, j + 1].G4_p, matriz[i + 1, j + 1].G1_p,
                            matriz[i + 1, j + 1].G2_p, matriz[i + 1, j + 1].G3_p, matriz[i + 1, j + 1].G4_p, delta_xi);
                    }
                    else if (i == rows - 1)
                    {
                        F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Contorno_Superior(delta_y_t, matriz[i, j + 1].F1_p, matriz[i, j + 1].F2_p, matriz[i, j + 1].F3_p, matriz[i, j + 1].F4_p, matriz[i - 1, j + 1].F1_p,
                            matriz[i - 1, j + 1].F2_p, matriz[i - 1, j + 1].F3_p, matriz[i - 1, j + 1].F4_p, matriz[i, j + 1].G1_p, matriz[i, j + 1].G2_p, matriz[i, j + 1].G3_p, matriz[i, j + 1].G4_p, matriz[i - 1, j + 1].G1_p,
                            matriz[i - 1, j + 1].G2_p, matriz[i - 1, j + 1].G3_p, matriz[i - 1, j + 1].G4_p, delta_xi);

                    }
                    else
                    {
                        F1_F2_F3_F4_derecha_corrected = matriz[i, j].Corrector_Step_Principal(Cy, delta_xi, delta_y_t,
                            matriz[i - 1, j + 1].F1_p,
                            matriz[i - 1, j + 1].F2_p,
                            matriz[i - 1, j + 1].F3_p,
                            matriz[i - 1, j + 1].F4_p,
                            matriz[i, j + 1].F1_p,
                            matriz[i, j + 1].F2_p,
                            matriz[i, j + 1].F3_p,
                            matriz[i, j + 1].F4_p,
                            matriz[i - 1, j + 1].G1_p,
                            matriz[i - 1, j + 1].G2_p,
                            matriz[i - 1, j + 1].G3_p,
                            matriz[i - 1, j + 1].G4_p,
                            matriz[i, j + 1].G1_p,
                            matriz[i, j + 1].G2_p,
                            matriz[i, j + 1].G3_p,
                            matriz[i, j + 1].G4_p,
                            matriz[i + 1, j + 1].F1_p,
                            matriz[i + 1, j + 1].F2_p,
                            matriz[i + 1, j + 1].F3_p,
                            matriz[i + 1, j + 1].F4_p,
                            matriz[i + 1, j + 1].P_p,
                            matriz[i, j + 1].P_p,
                            matriz[i - 1, j + 1].P_p);
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

        #endregion COMPUTE

        #region TABLE MANIPULATION FUNCION
        public void Fill_DataTable()
        {

            for (int j = 0; j < columns; j++)
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

            for (int i = 0; i < rows; i++)
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

                for (int j = 0; j < columns; j++)
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


        }

        public double[] GetColumnData_array(string data, int columna_int)
        {
            double[] valores= new double[rows];
            if (data=="t")
            { 
                for (int j=0;j<rows;j++)
                {
                    valores[j] = matriz[j, columna_int].T;
                }
            }
            else if (data == "u")
            {
                for (int j = 0; j < rows; j++)
                {
                    valores[j] = matriz[j, columna_int].u;
                }
            }
            else if (data == "v")
            {
                for (int j = 0; j < rows; j++)
                {
                    valores[j] = matriz[j, columna_int].v;
                }
            }
            else if (data == "m")
            {
                for (int j = 0; j < rows; j++)
                {
                    valores[j] = matriz[j, columna_int].M;
                }
            }
            else if (data == "p")
            {
                for (int j = 0; j < rows; j++)
                {
                    valores[j] = matriz[j, columna_int].P;
                }
            }
            else if (data == "rho")
            {
                for (int j = 0; j < rows; j++)
                {
                    valores[j] = matriz[j, columna_int].Rho;
                }
            }
            else if (data == "f1")
            {
                for (int j = 0; j < rows; j++)
                {
                    valores[j] = matriz[j, columna_int].F1;
                }
            }
            else if (data == "f2")
            {
                for (int j = 0; j < rows; j++)
                {
                    valores[j] = matriz[j, columna_int].F2;
                }
            }
            else if (data == "f3")
            {
                for (int j = 0; j < rows; j++)
                {
                    valores[j] = matriz[j, columna_int].F3;
                }
            }
            else if (data == "f4")
            {
                for (int j = 0; j < rows; j++)
                {
                    valores[j] = matriz[j, columna_int].F4;
                }
            }

            return valores;
        }

        public double[] GetFilaData_array(string data, int fila_int)
        {
            double[] valores = new double[columns - 1];
            if (data == "t")
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    valores[i] = matriz[fila_int, i].T;
                }
            }
            else if (data == "u")
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    valores[i] = matriz[fila_int, i].u;
                }
            }
            else if (data == "v")
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    valores[i] = matriz[fila_int, i].v;
                }
            }
            else if (data == "m")
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    valores[i] = matriz[fila_int, i].M;
                }
            }
            else if (data == "p")
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    valores[i] = matriz[fila_int, i].P;
                }
            }
            else if (data == "rho")
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    valores[i] = matriz[fila_int, i].Rho;
                }
            }
            else if (data == "f1")
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    valores[i] = matriz[fila_int, i].F1;
                }
            }
            else if (data == "f2")
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    valores[i] = matriz[fila_int, i].F2;
                }
            }
            else if (data == "f3")
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    valores[i] = matriz[fila_int, i].F3;
                }
            }
            else if (data == "f4")
            {
                for (int i = 0; i < columns - 1; i++)
                {
                    valores[i] = matriz[fila_int, i].F4;
                }
            }

            return valores;
        }

        public DataTable [] GetTables()
        {
            DataTable [] T_U_V_RHO_P_M_F1_F2_F3_F4 = new DataTable[] {Temperature_table,u_table,v_table,Rho_table,P_table,Mach_table,F1_table,F2_table,F3_table,F4_table};
            return T_U_V_RHO_P_M_F1_F2_F3_F4;
        }

        public void CrearListade(string attribute)
        {
            if (attribute == "x")
            {
                for (int i = 0; i < columns; i++)
                {
                    listaDeXColumna.Add(matriz[0, i].x);
                }
            }
            if (attribute == "T")
            {
                for (int i = 0; i < columns; i++)
                {
                    double suma_en_columna = 0;

                    for (int j = 0; j < rows; j++)
                    {
                        suma_en_columna = suma_en_columna + matriz[j, i].T;
                    }
                    suma_en_columna = suma_en_columna / rows;
                    listaTemperaturaxColumna.Add(suma_en_columna);
                }
            }

            if (attribute == "M")
            {
                for (int i = 0; i < columns; i++)
                {
                    double suma_en_columna = 0;

                    for (int j = 0; j < rows; j++)
                    {
                        suma_en_columna = suma_en_columna + matriz[j, i].M;
                    }
                    suma_en_columna = suma_en_columna / rows;
                    listaMachxColumna.Add(suma_en_columna);
                }
            }

            if (attribute == "Rho")
            {
                for (int i = 0; i < columns; i++)
                {
                    double suma_en_columna = 0;

                    for (int j = 0; j < rows; j++)
                    {
                        suma_en_columna = suma_en_columna + matriz[j, i].Rho;
                    }
                    suma_en_columna = suma_en_columna / rows;
                    listaDensidadxColumna.Add(suma_en_columna);
                }
            }

            if (attribute == "P")
            {
                for (int i = 0; i < columns; i++)
                {
                    double suma_en_columna = 0;

                    for (int j = 0; j < rows; j++)
                    {
                        suma_en_columna = suma_en_columna + matriz[j, i].P;
                    }
                    suma_en_columna = suma_en_columna / rows;
                    listaPresurexColumna.Add(suma_en_columna);
                }
            }

            if (attribute == "u")
            {
                for (int i = 0; i < columns; i++)
                {
                    double suma_en_columna = 0;

                    for (int j = 0; j < rows; j++)
                    {
                        suma_en_columna = suma_en_columna + matriz[j, i].u;
                    }
                    suma_en_columna = suma_en_columna / rows;
                    listaU_velxColumna.Add(suma_en_columna);
                }
            }

            if (attribute == "v")
            {
                for (int i = 0; i < columns; i++)
                {
                    double suma_en_columna = 0;

                    for (int j = 0; j < rows; j++)
                    {
                        suma_en_columna = suma_en_columna + matriz[j, i].v;
                    }
                    suma_en_columna = suma_en_columna / rows;
                    listaV_velxColumna.Add(suma_en_columna);
                }
            }
        }

        #endregion TABLE MANIPULATION FUNCION

    }
}
