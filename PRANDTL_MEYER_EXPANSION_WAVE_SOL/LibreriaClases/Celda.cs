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
        double dEta_dx;
        double dEta_dy;
        double delta_y_t;

        double P_p;

        double SF1;
        double SF2;
        double SF3;
        double SF4;

        double dF1_x_av;
        double dF2_x_av;
        double dF3_x_av;
        double dF4_x_av;



        public void xy_Transformation_ToEtaXi(double H, double E, double Theta)
        { if (x < E)
            {
                y_s = 0;
                h = H;
                dEta_dx = 0;
            }
            else
            {
                y_s = -(x-E) * Math.Tan(Theta);
                h = H+(x-E) * Math.Tan(Theta) ;
                double Eta = (y - y_s) / h;
                dEta_dx = (Math.Tan(Theta) / h) *(1-Eta);
            }
            y = h*y_t+y_s;
            dEta_dy = 1/h;       
        }

        public double[] StepSize_TanMax(double Theta, double delta_y, double M_angle)
        {
            double tan_max = Math.Max(Math.Abs(Math.Tan(Theta + M_angle)), Math.Abs(Math.Tan(Theta - M_angle)));
            double delta_x = 0.5 * delta_y / tan_max;
            double[] values = { tan_max, delta_x };
            return values;
        }

        public double[] Predictor_Step_Principal (double Cy, double delta_y_t,double delta_x, double F1_arriba, double F2_arriba,double F3_arriba, double F4_arriba, double F1_abajo,double F2_abajo, double F3_abajo, double F4_abajo ,double G1_arriba,double G2_arriba, double G3_arriba, double G4_arriba, double P_arriba, double P_abajo)
        { 
            dF1_x = (dEta_dx) * (F1 - F1_arriba) / delta_y_t + (dEta_dy) * (G1 - G1_arriba) / delta_y_t;
            dF2_x = (dEta_dx) * (F2 - F2_arriba) / delta_y_t + (dEta_dy) * (G2 - G2_arriba) / delta_y_t;
            dF3_x = (dEta_dx) * (F3 - F3_arriba) / delta_y_t + (dEta_dy) * (G3 - G3_arriba) / delta_y_t;
            dF3_x = (dEta_dx) * (F4 - F4_arriba) / delta_y_t + (dEta_dy) * (G4 - G4_arriba) / delta_y_t;

            SF1 = Cy * (Math.Abs(P_arriba - (2 * P) + P_abajo) / (P_arriba + 2 * P + P_abajo)) * (F1_arriba - (2 * F1) + F1_abajo);
            SF2 = Cy * (Math.Abs(P_arriba - (2 * P) + P_abajo) / (P_arriba + 2 * P + P_abajo)) * (F2_arriba - (2 * F2) + F2_abajo);
            SF3 = Cy * (Math.Abs(P_arriba - (2 * P) + P_abajo) / (P_arriba + 2 * P + P_abajo)) * (F3_arriba - (2 * F3) + F3_abajo);
            SF4 = Cy * (Math.Abs(P_arriba - (2 * P) + P_abajo) / (P_arriba + 2 * P + P_abajo)) * (F4_arriba - (2 * F4) + F4_abajo);

            double F1_p_derecha = F1 + dF1_x * delta_x + SF1;
            double F2_p_derecha = F2 + dF2_x * delta_x + SF2;
            double F3_p_derecha = F3 + dF3_x * delta_x + SF3;
            double F4_p_derecha = F4 + dF4_x * delta_x + SF4;
            double[] F_p_derecha_vector = new double[] {F1_p_derecha, F2_p_derecha,F3_p_derecha,F4_p_derecha };

            return F_p_derecha_vector;
        }

        public double[] Predictor_Step_Contorno_Superior (double delta_y_t, double delta_x, double F1_abajo, double F2_abajo, double F3_abajo, double F4_abajo, double G1_abajo, double G2_abajo, double G3_abajo, double G4_abajo)
        {
            dF1_x = (dEta_dx) * (F1_abajo - F1) / delta_y_t + (dEta_dy) * (G1_abajo - G1) / delta_y_t;
            dF2_x = (dEta_dx) * (F2_abajo - F2) / delta_y_t + (dEta_dy) * (G2_abajo - G2) / delta_y_t;
            dF3_x = (dEta_dx) * (F3_abajo - F3) / delta_y_t + (dEta_dy) * (G3_abajo - G3) / delta_y_t;
            dF4_x = (dEta_dx) * (F4_abajo - F4) / delta_y_t + (dEta_dy) * (G4_abajo - G4) / delta_y_t;

            double F1_p_derecha = F1 + dF1_x * delta_x;
            double F2_p_derecha = F2 + dF2_x * delta_x;
            double F3_p_derecha = F3 + dF3_x * delta_x;
            double F4_p_derecha = F4 + dF4_x * delta_x;
            double[] F_p_derecha_vector = new double[] { F1_p_derecha, F2_p_derecha, F3_p_derecha, F4_p_derecha };

            return F_p_derecha_vector;
        }

        public double[] Predictor_Step_Contorno_Inferior(double delta_y_t, double delta_x, double F1_arriba, double F2_arriba, double F3_arriba, double F4_arriba,  double G1_arriba, double G2_arriba, double G3_arriba, double G4_arriba)
        {
            dF1_x = (dEta_dx) * (F1- F1_arriba) / delta_y_t + (dEta_dy) * (G1 - G1_arriba) / delta_y_t;
            dF2_x = (dEta_dx) * (F2- F2_arriba) / delta_y_t + (dEta_dy) * (G2 - G2_arriba) / delta_y_t;
            dF3_x = (dEta_dx) * (F3- F3_arriba) / delta_y_t + (dEta_dy) * (G3 - G3_arriba) / delta_y_t;
            dF4_x = (dEta_dx) * (F4- F4_arriba) / delta_y_t + (dEta_dy) * (G4 - G4_arriba) / delta_y_t;
            
            double F1_p_derecha = F1 + dF1_x * delta_x;
            double F2_p_derecha = F2 + dF2_x * delta_x;
            double F3_p_derecha = F3 + dF3_x * delta_x;
            double F4_p_derecha = F4 + dF4_x * delta_x;
            double[] F_p_derecha_vector = new double[] { F1_p_derecha, F2_p_derecha, F3_p_derecha, F4_p_derecha };

            return F_p_derecha_vector;
        }

        public double[] G_Predicted(double Gamma, double F3_p_derecha, double F1_p_derecha, double F4_p_derecha, double F2_p_derecha)
        { 
            double A_p=Math.Pow(F3_p_derecha,2)/(2*F1_p_derecha) - F4_p_derecha;
            double B_p=Gamma/(Gamma - 1)*F1_p_derecha*F2_p_derecha;
            double C_p=-(Gamma + 1)/(2*(Gamma - 1))*Math.Pow(F1_p_derecha,3);
            
            double Rho_p= (-B_p + (Math.Sqrt(Math.Pow(B_p, 2) - (4 * A_p * C_p)))) / (2 * A_p);
            double P_p= F2_p_derecha - Math.Pow(F1_p_derecha , 2) / Rho_p;

            double G1_p_derecha = Rho_p * (F3_p_derecha / F1_p_derecha);
            double G2_p_derecha = F3_p_derecha;
            double G3_p_derecha = (Rho_p *  Math.Pow(F3_p_derecha /F1_p_derecha, 2)) + F2_p_derecha - (Math.Pow(F1_p_derecha, 2) / Rho_p);
            double G4_p_derecha = (Gamma / (Gamma - 1) * ((F2_p_derecha) - (Math.Pow(F1_p_derecha ,2) / Rho_p)) * (F3_p_derecha / F1_p_derecha)) + Rho_p * F3_p_derecha / (2 * F1_p_derecha) * ((F1_p_derecha/ Math.Pow(Rho_p,2)) + F3_p_derecha / Math.Pow(F1_p_derecha,2));


            double[] G_predictedValues = new double[] { G1_p_derecha, G2_p_derecha, G3_p_derecha, G4_p_derecha, Rho_p, P_p };

            return G_predictedValues;

        }

        public double[] Corrector_Step_Principal (double Cy, double F1_p_debajo_derecha, double delta_x, double F1_p_derecha, double delta_y_t, double G1_p_debajo_derecha, double G1_p_derecha, double F2_p_debajo_derecha, double F2_p_derecha, double G2_p_debajo_derecha, double G2_p_derecha, double F3_p_debajo_derecha, double F3_p_derecha, double G3_p_debajo_derecha, double G3_p_derecha,  double F4_p_debajo_derecha, double F4_p_derecha, double G4_p_debajo_derecha, double G4_p_derecha, double P_p_arriba, double P_p, double P_p_abajo, double F1_p_derecha_arriba, double F1_p_derecha_abajo, double F2_p_derecha_arriba, double F3_p_derecha_arriba, double F4_p_derecha_arriba, double F2_p_derecha_abajo, double F3_p_derecha_abajo, double F4_p_derecha_abajo )
        {
            double dF1_p_x_derecha = (dEta_dx) * ((F1_p_debajo_derecha - F1_p_derecha) / delta_y_t) + (dEta_dy * ((G1_p_debajo_derecha - G1_p_derecha) / delta_y_t));
            double dF2_p_x_derecha = (dEta_dx) * ((F2_p_debajo_derecha - F2_p_derecha) / delta_y_t) + (dEta_dy * ((G2_p_debajo_derecha - G2_p_derecha) / delta_y_t));
            double dF3_p_x_derecha = (dEta_dx) * ((F3_p_debajo_derecha - F3_p_derecha) / delta_y_t) + (dEta_dy * ((G3_p_debajo_derecha - G3_p_derecha) / delta_y_t));
            double dF4_p_x_derecha = (dEta_dx) * ((F4_p_debajo_derecha - F4_p_derecha) / delta_y_t) + (dEta_dy * ((G4_p_debajo_derecha - G4_p_derecha) / delta_y_t));

            double SF1_p = (((Cy * (Math.Abs(P_p_arriba) - (2 * P_p) + P_p_abajo))) / (P_p_arriba + 2 * P_p + P_p_abajo)) * (F1_p_derecha_arriba - (2 * F1_p_derecha + F1_p_derecha_abajo));
            double SF2_p = (((Cy * (Math.Abs(P_p_arriba) - (2 * P_p) + P_p_abajo))) / (P_p_arriba + 2 * P_p + P_p_abajo)) * (F2_p_derecha_arriba - (2 * F2_p_derecha + F2_p_derecha_abajo));
            double SF3_p = (((Cy * (Math.Abs(P_p_arriba) - (2 * P_p) + P_p_abajo))) / (P_p_arriba + 2 * P_p + P_p_abajo)) * (F3_p_derecha_arriba - (2 * F3_p_derecha + F3_p_derecha_abajo));
            double SF4_p = (((Cy * (Math.Abs(P_p_arriba) - (2 * P_p) + P_p_abajo))) / (P_p_arriba + 2 * P_p + P_p_abajo)) * (F4_p_derecha_arriba - (2 * F4_p_derecha + F4_p_derecha_abajo));

            dF1_x_av = 0.5*(dF1_x + dF1_p_x_derecha);
            dF2_x_av = 0.5*(dF2_x + dF2_p_x_derecha);
            dF3_x_av = 0.5*(dF3_x + dF3_p_x_derecha);
            dF4_x_av = 0.5*(dF4_x + dF4_p_x_derecha);

            double F1_derecha = F1 + (dF1_x_av * delta_x) + SF1_p;
            double F2_derecha = F2 + (dF2_x_av * delta_x) + SF2_p;
            double F3_derecha = F3 + (dF3_x_av * delta_x) + SF3_p;
            double F4_derecha = F4 + (dF4_x_av * delta_x) + SF4_p;

            double[] F_futuras = { F1_derecha, F2_derecha, F3_derecha, F4_derecha };
            return F_futuras;

        }

        public double[] Corrector_Step_Contorno_Inferior (double F1_p_derecha, double F2_p_derecha, double F3_p_derecha, double F4_p_derecha, double F1_p_arriba_derecha, double F2_p_arriba_derecha, double F3_p_arriba_derecha, double F4_p_arriba_derecha, double G1_p_derecha, double G2_p_derecha, double G3_p_derecha, double G4_p_derecha, double G1_p_arriba_derecha, double G2_p_arriba_derecha, double G3_p_arriba_derecha, double G4_p_arriba_derecha, double delta_x)
        {
            double dF1_p_x_derecha = (dEta_dx) * ((F1_p_derecha - F1_p_arriba_derecha) / delta_y_t) + (dEta_dy * ((G1_p_derecha - G1_p_arriba_derecha) / delta_y_t));
            double dF2_p_x_derecha = (dEta_dx) * ((F2_p_derecha - F2_p_arriba_derecha) / delta_y_t) + (dEta_dy * ((G2_p_derecha - G2_p_arriba_derecha) / delta_y_t));
            double dF3_p_x_derecha = (dEta_dx) * ((F3_p_derecha - F3_p_arriba_derecha) / delta_y_t) + (dEta_dy * ((G3_p_derecha - G3_p_arriba_derecha) / delta_y_t));
            double dF4_p_x_derecha = (dEta_dx) * ((F4_p_derecha - F4_p_arriba_derecha) / delta_y_t) + (dEta_dy * ((G4_p_derecha - G4_p_arriba_derecha) / delta_y_t));

            dF1_x_av = 0.5 * (dF1_x + dF1_p_x_derecha);
            dF2_x_av = 0.5 * (dF2_x + dF2_p_x_derecha);
            dF3_x_av = 0.5 * (dF3_x + dF3_p_x_derecha);
            dF4_x_av = 0.5 * (dF4_x + dF4_p_x_derecha);

            double F1_derecha = F1 + (dF1_x_av * delta_x);
            double F2_derecha = F2 + (dF2_x_av * delta_x);
            double F3_derecha = F3 + (dF3_x_av * delta_x);
            double F4_derecha = F4 + (dF4_x_av * delta_x);

            double[] F_futuras = { F1_derecha, F2_derecha, F3_derecha, F4_derecha };
            return F_futuras;
        }

        public double[] Corrector_Step_Contorno_Superior(double F1_p_derecha, double F2_p_derecha, double F3_p_derecha, double F4_p_derecha, double F1_p_debajo_derecha, double F2_p_debajo_derecha, double F3_p_debajo_derecha, double F4_p_debajo_derecha, double G1_p_derecha, double G2_p_derecha, double G3_p_derecha, double G4_p_derecha, double G1_p_debajo_derecha, double G2_p_debajo_derecha, double G3_p_debajo_derecha, double G4_p_debajo_derecha, double delta_x)
        {
            double dF1_p_x_derecha = (dEta_dx) * ((F1_p_debajo_derecha - F1_p_derecha) / delta_y_t) + (dEta_dy * ((G1_p_debajo_derecha - G1_p_derecha) / delta_y_t));
            double dF2_p_x_derecha = (dEta_dx) * ((F2_p_debajo_derecha - F2_p_derecha) / delta_y_t) + (dEta_dy * ((G2_p_debajo_derecha - G2_p_derecha) / delta_y_t));
            double dF3_p_x_derecha = (dEta_dx) * ((F3_p_debajo_derecha - F3_p_derecha) / delta_y_t) + (dEta_dy * ((G3_p_debajo_derecha - G3_p_derecha) / delta_y_t));
            double dF4_p_x_derecha = (dEta_dx) * ((F4_p_debajo_derecha - F4_p_derecha) / delta_y_t) + (dEta_dy * ((G4_p_debajo_derecha - G4_p_derecha) / delta_y_t));

            dF1_x_av = 0.5 * (dF1_x + dF1_p_x_derecha);
            dF2_x_av = 0.5 * (dF2_x + dF2_p_x_derecha);
            dF3_x_av = 0.5 * (dF3_x + dF3_p_x_derecha);
            dF4_x_av = 0.5 * (dF4_x + dF4_p_x_derecha);

            double F1_derecha = F1 + (dF1_x_av * delta_x);
            double F2_derecha = F2 + (dF2_x_av * delta_x);
            double F3_derecha = F3 + (dF3_x_av * delta_x);
            double F4_derecha = F4 + (dF4_x_av * delta_x);

            double[] F_futuras = { F1_derecha, F2_derecha, F3_derecha, F4_derecha };
            return F_futuras;
        }




    }
}
