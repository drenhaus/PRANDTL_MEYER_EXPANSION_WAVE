using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    class Malla
    {
        int divisiones_xi; // columnas
        int divisiones_eta; // filas
        Normas norma = new Normas();
        Celda[,] matriz;


        public void DefinirMatriz(int filas, int columnas)
        {

            this.divisiones_xi = columnas;
            this.divisiones_eta = filas;

            this.matriz = new Celda[filas, columnas];

            //We fill the matrix with Cells
            for (int j = 0; j < divisiones_eta; j++)
            {
                for (int i = 0; i < divisiones_xi; i++)
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
            for (int j = 0; j < divisiones_eta; j++)
            {
                matriz[j, 0].SetA(norma.GetA_in());
                matriz[j, 0].SetM(norma.GetM_in());
                matriz[j, 0].SetU(norma.GetU_in());
                matriz[j, 0].SetV(norma.GetV_in());
                matriz[j, 0].SetT(norma.GetT_in());
                matriz[j, 0].SetP(norma.GetP_in());
                matriz[j, 0].SetRho(norma.GetRho_in());
                matriz[j, 0].SetMAngle(norma.GetM_angle());


            }




        }






    }
}
