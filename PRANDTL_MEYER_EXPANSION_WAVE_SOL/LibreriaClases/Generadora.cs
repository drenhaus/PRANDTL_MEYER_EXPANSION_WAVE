using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public class Generadora
    {
        #region ATRIBUTES

        // LISTS OF POINTS AND VALUES
        public List<Punto> Puntos { get; set; } //generating a list of points 

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
                Puntos.Add(new Punto(EvaluarX(x), EvaluarTEMP(x)));  // evaluating the points
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
                Puntos.Add(new Punto(EvaluarX(x), EvaluarMACH(x)));  // evaluating the points
            }

            return Puntos;
        }
        
        //GENERATING THE DENSITY FUNCTION
             // Function that once gived the upper limit it returns a list of points evaluating Mach
        public List<Punto> GenerarDatosDensidad(double limiteSuperior)
        {
            double limiteInferior = 0; //ower boundary
            double incremento = 1; // increment

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(EvaluarX(x), EvaluarDensidad(x))); // evaluating the points
            }

            return Puntos;
        }

        //GENERATING THE PRESSURE FUNCTION
            // Function that once gived the upper limit it returns a list of points evaluating pressure
        public List<Punto> GenerarDatosPresure(double limiteSuperior)
        {
            double limiteInferior = 0; //lower boundary
            double incremento = 1; // increment

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(EvaluarX(x), EvaluarPresure(x))); // evaluating the points
            }

            return Puntos;
        }

        //GENERATING THE U FUNCTION
            // Function that once gived the upper limit it returns a list of points evaluating u velocity
        public List<Punto> GenerarDatosU(double limiteSuperior)
        {
            double limiteInferior = 0; //lower boundary
            double incremento = 1; // increment

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(EvaluarX(x), EvaluarU(x))); // evaluating the points
            }

            return Puntos;
        }

        //GENERATING THE V FUNCTION
            // Function that once gived the upper limit it returns a list of points evaluating v velocity
        public List<Punto> GenerarDatosV(double limiteSuperior)
        {
            double limiteInferior = 0; //lower limit
            double incremento = 1; // increment

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x <= limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(EvaluarX(x), EvaluarV(x))); // evaluating the points
            }

            return Puntos;
        }


        #endregion ATRIBUTES

        #region EVALUATE FUNTIONS
        //GENERATING THE FUNCTIONS THAT ARE GOING TO BE PLOTED
            // given an iteration, it returns the value of mean temperaute, mach, pressure, etc at that iteration
       
        private double EvaluarX(double x)
        {
            return listaDeXColumna_G[Convert.ToInt32(x)]; // searches in the list
        }
        private double EvaluarTEMP(double x)
        {
            return listaTEMPxColumna_G[Convert.ToInt32(x)]; // searches in the list
        }
        private double EvaluarMACH(double x)
        {
            return listaMachxColumna_G[Convert.ToInt32(x)]; // searches in the list
        }
        private double EvaluarDensidad(double x)
        {
            return listaDensidadxColumna_G[Convert.ToInt32(x)]; // searches in the list
        }
        private double EvaluarPresure(double x)
        {
            return listaPresurexColumna_G[Convert.ToInt32(x)]; // searches in the list
        }
        private double EvaluarU(double x)
        {
            return listaUxColumna_G[Convert.ToInt32(x)]; // searches in the list
        }
        private double EvaluarV(double x)
        {
            return listaVxColumna_G[Convert.ToInt32(x)]; // searches in the list
        }

        #endregion EVALUATE FUNTIONS
    }
}
