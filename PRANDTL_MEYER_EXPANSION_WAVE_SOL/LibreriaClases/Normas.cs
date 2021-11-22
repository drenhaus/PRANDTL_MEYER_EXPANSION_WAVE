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

        //GETS
        public double GetH () { return H; }
        public double GetTheta() { return Theta; }
        public double GetL() { return L; }
        public double GetE() { return E; }
        public double GetM_in() { return M_in; }
        public double GetP_in() { return P_in; }
        public double GetRho_in() { return Rho_in; }
        public double GetR_air() { return R_air; }
        public double GetV_in() { return v_in; }
        public double GetT_in() { return T_in; }
        public double GetU_in() { return u_in; }
        public double GetM_angle() { return M_angle; }
        public double GetA_in() { return a_in; }

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
