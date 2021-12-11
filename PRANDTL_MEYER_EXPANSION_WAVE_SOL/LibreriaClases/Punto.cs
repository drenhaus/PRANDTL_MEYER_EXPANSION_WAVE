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
        public Punto(double x, double y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
        #endregion FUNCTIONS SET POINT AND GIVE A FROMAT
    }
}
