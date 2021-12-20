using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public class Punto
    {
        #region ATRIBUTES
        // Definimos una clase punto con un valor x e y
        public double X { get; set; }
        public double Y { get; set; }
        #endregion ATRIBUTES

        #region FUNCTIONS SET POINT AND GIVE A FROMAT
        public Punto(double x, double y)     // Definimos  las x como x y y
        {
            X = x;
            Y = y;
        }
        public override string ToString()   // Definimos en el formato que luego usaremos en en la classe Generadora. 
        {
            return string.Format("({0},{1})", X, Y);
        }
        #endregion FUNCTIONS SET POINT AND GIVE A FROMAT
    }
}
