using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public class Generadora
    {
        public List<Punto> Puntos { get; set; } //generamos una lista de puntos  
        public List<double> listaTEMPxColumna_G { get; set; }
        public List<double> listaMachxColumna_G { get; set; }
        public List<double> listaDensidadxColumna_G { get; set; }
        public List<double> listaPresurexColumna_G { get; set; }
        public List<double> listaUxColumna_G { get; set; }
        public List<double> listaVxColumna_G { get; set; }





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
        public List<Punto> GenerarDatosDensidad(double limiteSuperior)
        {
            double limiteInferior = 0; //límite inferior
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(x, EvaluarDensidad(x)));
            }

            return Puntos;
        }
        public List<Punto> GenerarDatosPresure(double limiteSuperior)
        {
            double limiteInferior = 0; //límite inferior
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(x, EvaluarPresure(x)));
            }

            return Puntos;
        }
        public List<Punto> GenerarDatosU(double limiteSuperior)
        {
            double limiteInferior = 0; //límite inferior
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(x, EvaluarU(x)));
            }

            return Puntos;
        }
        public List<Punto> GenerarDatosV(double limiteSuperior)
        {
            double limiteInferior = 0; //límite inferior
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(x, EvaluarV(x)));
            }

            return Puntos;
        }




        //GENERAMOS LAS FUNCIONES QUE VAMOS A PLOTEAR
        // dada una iteración, nos devuelve el valor de la temperatura media en esa iteración
        private double EvaluarTEMP(double x)
        {
            return listaTEMPxColumna_G[Convert.ToInt32(x)]; // hace la busqueda en la lista
        }
        private double EvaluarMACH(double x)
        {
            return listaMachxColumna_G[Convert.ToInt32(x)]; // hace la busqueda en la lista
        }
        private double EvaluarDensidad(double x)
        {
            return listaDensidadxColumna_G[Convert.ToInt32(x)]; // hace la busqueda en la lista
        }
        private double EvaluarPresure(double x)
        {
            return listaPresurexColumna_G[Convert.ToInt32(x)]; // hace la busqueda en la lista
        }
        private double EvaluarU(double x)
        {
            return listaUxColumna_G[Convert.ToInt32(x)]; // hace la busqueda en la lista
        }
        private double EvaluarV(double x)
        {
            return listaVxColumna_G[Convert.ToInt32(x)]; // hace la busqueda en la lista
        }
 






    }
}
