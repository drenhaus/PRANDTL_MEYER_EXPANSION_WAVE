using System;

namespace LibreriaClases
{
    public class Celda
    {
        double M;
        double u;
        double v;
        double T;
        double P;
        double Rho;
        double a;
        double M_angle;

        double F1;
        double F2;
        double F3;
        double F4;

        double G1;
        double G2;
        double G3;
        double G4;

        double F1_p;
        double F2_p;
        double F3_p;
        double F4_p;


        double G1_p;
        double G2_p;
        double G3_p;
        double G4_p;

        double dF1_x;
        double dF2_x;
        double dF3_x;
        double dF4_x;

        double dF1_p_x;
        double dF2_p_x;
        double dF3_p_x;
        double dF4_p_x;

        double x;
        double y;
        double y_t;
        double y_s;
        double h;
        double m1;
        double m2;
        double delta_y_t;

        double P_p;

        double SF1;
        double SF2;
        double SF3;
        double SF4;



        public void Transformation(double H, double E, double Theta)
        { if (x < E)
            {
                y_s = 0;
                h = H;
                m1 = 0;
            }
            else
            {
                y_s = -x * Math.Tan(Theta) + E * Math.Tan(Theta);
                h = H + x * Math.Tan(Theta) - E * Math.Tan(Theta);
                m1 = (Math.Tan(Theta) / h) - y_t * (Math.Tan(Theta) / h);
            }
            y = y_t * h + y_s;
            m2 = 1 / h;       
        }

        public double StepSize(double Theta, double delta_y, double M_angle)
        {
            double tan1 = Math.Abs(Math.Tan(Theta*(Math.PI/180) + M_angle));
            double tan2 = Math.Abs(Math.Tan(Theta * (Math.PI / 180) - M_angle));

            double tan_max = Math.Max(tan1, tan2);
            double delta_x = 0.5 * delta_y / tan_max;

            return delta_x;
        }


        public double[] PredictorStep(double delta_y_t,double delta_x, double F1_arriba, double F2_arriba,double F3_arriba,
            double F4_arriba, double F1_abajo,double F2_abajo, double F3_abajo, double F4_abajo ,double G1_arriba,double G2_arriba,
            double G3_arriba, double G4_arriba, double P_arriba, double P_abajo)
        { 
            dF1_x = (m1 * (F1 - F1_arriba) / delta_y_t) + (m2 * (G1 - G1_arriba) / delta_y_t);
            dF2_x = (m1 * (F2 - F2_arriba) / delta_y_t) + (m2 * (G2 - G2_arriba) / delta_y_t);
            dF3_x = (m1 * (F3 - F3_arriba) / delta_y_t) + (m2 * (G3 - G3_arriba) / delta_y_t);
            dF3_x = (m1 * (F4 - F4_arriba) / delta_y_t) + (m2 * (G4 - G4_arriba) / delta_y_t);

            SF1 = 0.6 * (Math.Abs(P_arriba - (2 * P) + P_abajo) / (P_arriba + 2 * P + P_abajo)) * (F1_arriba - (2 * F1) + F1_abajo);
            SF2 = 0.6 * (Math.Abs(P_arriba - (2 * P) + P_abajo) / (P_arriba + 2 * P + P_abajo)) * (F2_arriba - (2 * F2) + F2_abajo);
            SF3 = 0.6 * (Math.Abs(P_arriba - (2 * P) + P_abajo) / (P_arriba + 2 * P + P_abajo)) * (F3_arriba - (2 * F2) + F3_abajo);
            SF4 = 0.6 * (Math.Abs(P_arriba - (2 * P) + P_abajo) / (P_arriba + 2 * P + P_abajo)) * (F4_arriba - (2 * F2) + F4_abajo);

            double F1_p_derecha = F1 + dF1_x * delta_x + SF1;
            double F2_p_derecha = F2 + dF2_x * delta_x + SF2;
            double F3_p_derecha = F3 + dF3_x * delta_x + SF3;
            double F4_p_derecha = F4 + dF4_x * delta_x + SF4;
            double[] F_p_derecha_vector = new double[] {F1_p_derecha, F2_p_derecha,F3_p_derecha,F4_p_derecha };

            return F_p_derecha_vector;
        }


        public double G_Predicted(double Gamma, double F3_p_derecha, double F1_p_derecha, double F4_p_derecha, double F2_p_derecha)
        { 
            double A_p=Math.Pow(F3_p_derecha,2)/(2*F1_p_derecha) - F4_p_derecha;
            double B_p=(Gamma/(Gamma - 1))*F1_p_derecha*F2_p_derecha;
            double C_p=-(Gamma + 1)/(2*(Gamma - 1))*Math.Pow(F1_p_derecha,3);
            
            double Rho_p= (-B_p + (Math.Sqrt((Math.Pow(B_p, 2)) - (4 * A_p * C_p)))) / (2 * A_p);
            P_p= F2_p_derecha - Math.Pow(F1_p_derecha , 2) / Rho_p;

            double G1_p_derecha = Rho_p * (F3_p_derecha / F1_p_derecha);
            double G2_p_derecha = F3_p_derecha;
            double G3_p_derecha = (Rho_p * ((F3_p_derecha /Math.Pow( F1_p_derecha,2)) + F2_p_derecha - (Math.Pow(F1_p_derecha, 2) / Rho_p);
            double G4_p_derecha = ((Gamma / (Gamma - 1)) * ((F_p.F2_p(j, i + 1)) - ((F_p.F1_p(j, i + 1) ^ 2) / Rho_p)) * (F_p.F3_p(j, i + 1) / F_p.F1_p(j, i + 1))) + (((Rho_p * F_p.F3_p(j, i + 1)) / (2 * F_p.F1_p(j, i + 1))) * (((F_p.F1_p(j, i + 1) / Rho_p) ^ 2) + ((F_p.F3_p(j, i + 1) / F_p.F1_p(j, i + 1)) ^ 2)));
            

        }
 
 
        
    Predicted_pressure_value = P_p;
        Predicted_G_values = G_p;
end

    }
}
