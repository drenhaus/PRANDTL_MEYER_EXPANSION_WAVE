using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public class Punto
    {
        #region ATTRIBUTES
        // Definimos una clase punto con un valor x e y
        public double X { get; set; }
        public double Y { get; set; }
        #endregion ATTRIBUTES

        #region FUNCTIONS SET POINT AND GIVE A FROMAT
        public Punto(double x, double y)
        {
            X = x;
            Y = y;
        }
       
        #endregion FUNCTIONS SET POINT AND GIVE A FROMAT
    }
}
