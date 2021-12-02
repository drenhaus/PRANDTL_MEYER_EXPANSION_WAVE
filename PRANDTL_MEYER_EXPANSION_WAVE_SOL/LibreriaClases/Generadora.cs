using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public class Generadora
    {
        public List<Punto> Puntos { get; set; } //generamos una lista de puntos

        List<double> listaTEMPxColumna = new List<double>();

        public List<double> listaMachxColumna_G { get; set; }

        //SET de las listas
        public void SetListaTEMPxColumna(List<double> B)
        { this.listaTEMPxColumna = B; }


        //GENERAMOS EL GRÁFICO DE LA TEMPERATURA
        public List<Punto> GenerarDatosTEMP(double limiteSuperior)
        {
            double limiteInferior = 0; //límite inferior
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(x, EvaluarTEMP(x)));
            }

            return Puntos;
        }

        public List<Punto> GenerarDatosMACH(double limiteSuperior)
        {
            double limiteInferior = 0; //límite inferior
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(x, EvaluarMACH(x)));
            }

            return Puntos;
        }




        //GENERAMOS LAS FUNCIONES QUE VAMOS A PLOTEAR
        // dada una iteración, nos devuelve el valor de la temperatura media en esa iteración
        private double EvaluarTEMP(double x)
        {
            return listaTEMPxColumna[Convert.ToInt32(x)]; // hace la busqueda en la lista
        }

        private double EvaluarMACH(double x)
        {
            return listaMachxColumna_G[Convert.ToInt32(x)]; // hace la busqueda en la lista
        }
    }
}
