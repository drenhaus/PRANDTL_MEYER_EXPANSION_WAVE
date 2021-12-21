using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public class Normas
    {
        #region ATRIBUITES

        // We define the class normas for being able to change the initial conditions
        // and the desired parameters in a separate class
        public double H { get; set; } = 40; // heigh, by default 40
        public double Theta { get; set; } = 5.352 * Math.PI / 180; // theta angle in rad, by default 5.352º
        public double L { get; set; } = 67; // length of the plane, by default 67
        public double E { get; set; } = 10; // position of the expansion corner, by default 10

        #region INITIAL CONDITION ATRIBUTES
        // initial data line 
        public double M_in { get; set; } 
        public double P_in { get; set; } 
        public double Rho_in { get; set; }
        public double R_air { get; set; } = 287; // constant values set by default
        public double Gamma { get; set; } = 1.4; // constant values set by default
        public double T_in { get; set; } 
        public double v_in { get; set; }
        public double u_in { get; set; }
        public double M_angle { get; set; }
        public double a_in { get; set; }
        #endregion INITIAL CONDITIONS ATRIBUITES
        #endregion ATRIBUTES

        #region FUNCTION TO COMPUTE THE REST OF INITIAL CONDITIONS
        
        //COMPUTING THE HORIZONTAL SPEED
            // We can compute the horizontal speed thanks to Mach number
        public void Compute_u()
        {
            this.u_in= this.M_in *this.a_in;
        }
        
        // COMPUTING THE MACH ANGLE
            // We can compute the Mach angle in function of the Mach number value
        public void Compute_M_angle()
        {
            this.M_angle = Math.Asin(1 / this.M_in);
        }
        
        // COMPUTING THE SPEED SOUND
            // Thanks to the constants Gamma and R of air and the temperature, it can 
            // be computed the velocity of sound
        public void Compute_a()
        {
            this.a_in = Math.Sqrt(this.Gamma * this.R_air * this.T_in);
        }

        #endregion FUNCTION TO COMPUTE THE REST OF INITIAL CONDITIONS 
    }
}
