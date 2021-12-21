using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public class Punto
    {
        #region ATTRIBUTES
        // We define a class Punto with an x and y value 
        public double X { get; set; }
        public double Y { get; set; }
        #endregion ATTRIBUTES

        #region FUNCTIONS SET POINT AND GIVE A FROMAT
        
        //FUNCTION WITH THE SAME NAME OF THE CLASS 
            // When calling the class a double x and y would be introduced and
            // automatically saved as thee attributes of the class Punto
        public Punto(double x, double y)
        {
            X = x; // we define the x as x
            Y = y; // defining the y as y
        }
       
        #endregion FUNCTIONS SET POINT AND GIVE A FROMAT
    }
}
