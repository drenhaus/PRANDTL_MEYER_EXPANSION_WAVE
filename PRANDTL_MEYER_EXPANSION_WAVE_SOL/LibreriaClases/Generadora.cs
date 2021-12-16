﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public class Generadora
    {
        #region ATRIBUTES
        // LISTS OF POINTS AND VALUES
        public List<Punto> Puntos { get; set; } //generamos una lista de puntos  

        public List<double> listaDeXColumna_G { get; set; } // x list
        public List<double> listaTEMPxColumna_G { get; set; } // temperature list
        public List<double> listaMachxColumna_G { get; set; } // mach list
        public List<double> listaDensidadxColumna_G { get; set; } // density list
        public List<double> listaPresurexColumna_G { get; set; } // pressure list
        public List<double> listaUxColumna_G { get; set; } // u list
        public List<double> listaVxColumna_G { get; set; } // v list



        //GENERATING THE TEMPERATURE FUNCTION
            // Function that once gived the upper limit it returns a list of points evaluating temperature
        public List<Punto> GenerarDatosTEMP(double limiteSuperior)
        {
            double limiteInferior = 0; //lower boundary
            double incremento = 1; // increments

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(EvaluarX(x), EvaluarTEMP(x))); // evaluating the points
            }

            return Puntos;
        }

        //GENERATING THE MACH FUNCTION
            // Function that once gived the upper limit it returns a list of points evaluating Mach
        public List<Punto> GenerarDatosMACH(double limiteSuperior)
        {
            double limiteInferior = 0; //lower boundary
            double incremento = 1; // increments

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(EvaluarX(x), EvaluarMACH(x))); // evaluating the points
            }

            return Puntos;
        }

        //GENERATING THE DENSITY FUNCTION
        // Function that once gived the upper limit it returns a list of points evaluating Mach
        public List<Punto> GenerarDatosDensidad(double limiteSuperior)
        {
            double limiteInferior = 0; //lower boundary
            double incremento = 1; // increments

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(EvaluarX(x), EvaluarDensidad(x))); // evaluating the points
            }

            return Puntos;
        }
       
        
        public List<Punto> GenerarDatosPresure(double limiteSuperior)
        {
            double limiteInferior = 0; //lower boundary
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(EvaluarX(x), EvaluarPresure(x)));
            }

            return Puntos;
        }
        public List<Punto> GenerarDatosU(double limiteSuperior)
        {
            double limiteInferior = 0; //lower boundary
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(EvaluarX(x), EvaluarU(x)));
            }

            return Puntos;
        }
        public List<Punto> GenerarDatosV(double limiteSuperior)
        {
            double limiteInferior = 0; //lower boundary
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(EvaluarX(x), EvaluarV(x)));
            }

            return Puntos;
        }


        #endregion ATRIBUTES

        #region EVALUATE FUNTIONS
        //GENERAMOS LAS FUNCIONES QUE VAMOS A PLOTEAR
        // dada una iteración, nos devuelve el valor de la temperatura media en esa iteración
        private double EvaluarX(double x)
        {
            return listaDeXColumna_G[Convert.ToInt32(x)];
        }
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

        #endregion EVALUATE FUNTIONS
    }
}
