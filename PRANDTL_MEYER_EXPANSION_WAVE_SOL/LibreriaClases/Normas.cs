using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public class Normas
    {
        public double H { get; set; } = 40;
        public double Theta { get; set; } = 5.352 * Math.PI / 180;
        public double L { get; set; } = 65;
        public double E { get; set; } = 10;



        public double M_in { get; set; } = 2;
        public double P_in { get; set; } = 101000;
        public double Rho_in { get; set; } = 1.23;
        public double R_air { get; set; } = 287;
        public double Gamma { get; set; } = 1.4;
        public double T_in { get; set; } = 286.1;
        public double v_in { get; set; } = 0;
        public double u_in { get; set; }
        public double M_angle { get; set; }
        public double a_in { get; set; }


        public void Compute_u()
        {
            this.u_in= this.M_in *this.a_in;
        }
        public void Compute_M_angle()
        {
            this.M_angle = Math.Asin(1 / this.M_in);
        }
        public void Compute_a()
        {
            this.a_in = Math.Sqrt(this.Gamma * this.R_air * this.T_in);
        }
 
       

    }
}
