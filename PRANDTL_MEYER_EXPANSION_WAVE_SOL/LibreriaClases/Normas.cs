using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public class Normas
    {
        #region ATRIBUITES

        public double H { get; set; } = 40;
        public double Theta { get; set; } = 5.352 * Math.PI / 180;
        public double L { get; set; } = 67;
        public double E { get; set; } = 10;

        #region INITIAL CONDITION ATRIBUTES
        public double M_in { get; set; } 
        public double P_in { get; set; } 
        public double Rho_in { get; set; }
        public double R_air { get; set; } = 287;
        public double Gamma { get; set; } = 1.4;
        public double T_in { get; set; } 
        public double v_in { get; set; }
        public double u_in { get; set; }
        public double M_angle { get; set; }
        public double a_in { get; set; }
        #endregion INITIAL CONDITIONS ATRIBUITES
        #endregion ATRIBUTES

        #region FUNTION TO COMPUTE THE REST OF INITIAL OCNDITIONS
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

        #endregion FUNTION TO COMPUTE THE REST OF INITIAL OCNDITIONS 
    }
}
