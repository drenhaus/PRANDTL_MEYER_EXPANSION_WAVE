using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    class Normas
    {
        double H = 40;
        double Theta = 5.352*Math.PI/180; 
        double L = 66.278; 
        double E = 10; 

        double M_in = 2; 
        double P_in = 101000; 
        double Rho_in = 1.23; 
        double R_air = 287; 
        double Gamma = 1.4;
        double T_in=286.1;
        double v_in = 0;

        double u_in;
        double M_angle;
        double a_in;

        public void Compute_u(double M, double a)
        {
            this.u_in= this.M_in *this.a_in;
        }
        public void Compute_M_angle()
        {
            this.M_angle = Math.Asin(1 / this.M_in);
        }
        public void Compute_a(double M, double a)
        {
            this.a_in = Math.Sqrt(this.Gamma * this.R_air * this.T_in);
        }
    

    
 
       

    }
}
