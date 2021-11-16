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


        public double[] PredictorStep(double delta_y_t, double F1_arriba, double F2_arriba,double F3_arriba, double F4_arriba, double G1_arriba,double G2_arriba, double G3_arriba, double G4_arriba)
        { 
            dF1_x = (m1 * (F1 - F1_arriba) / delta_y_t) + (m2 * (G1 - G1_arriba) / delta_y_t);
            dF2_x = (m1 * (F2 - F2_arriba) / delta_y_t) + (m2 * (G2 - G2_arriba) / delta_y_t);
            dF3_x = (m1 * (F3 - F3_arriba) / delta_y_t) + (m2 * (G3 - G3_arriba) / delta_y_t);
            dF3_x = (m1 * (F4 - F4_arriba) / delta_y_t) + (m2 * (G4 - G4_arriba) / delta_y_t);

           
            double[] vector = new double[] { };

            return vector ;
        }


       

    }
}
