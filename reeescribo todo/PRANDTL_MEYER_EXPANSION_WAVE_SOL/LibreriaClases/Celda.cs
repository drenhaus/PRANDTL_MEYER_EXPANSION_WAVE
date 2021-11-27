using System;
using System.Collections.Generic;

namespace LibreriaClases
{
    public class Celda
    {
        public double M { get; set; }
        public double u { get; set; }
        public double v { get; set; }
        public double T { get; set; }
        public double P { get; set; }
        public double Rho { get; set; }
        public double a { get; set; }
        public double M_angle { get; set; }

        public double tan_max { get; set; }


        public double F1 { get; set; }
        public double F2 { get; set; }
        public double F3 { get; set; }
        public double F4 { get; set; }


        public double G1 { get; set; }
        public double G2 { get; set; }
        public double G3 { get; set; }
        public double G4 { get; set; }

        public double F1_p { get; set; }
        public double F2_p { get; set; }
        public double F3_p { get; set; }
        public double F4_p { get; set; }

        public double G1_p { get; set; }
        public double G2_p { get; set; }
        public double G3_p { get; set; }
        public double G4_p { get; set; }


        public double dF1_x { get; set; }
        public double dF2_x { get; set; }
        public double dF3_x { get; set; }
        public double dF4_x { get; set; }

        public double dF1_p_x { get; set; }
        public double dF2_p_x { get; set; }
        public double dF3_p_x { get; set; }
        public double dF4_p_x { get; set; }


        public double x { get; set; }
        public double y { get; set; }
        public double y_t { get; set; }
        public double y_s { get; set; }
        public double h { get; set; }
        public double delta_x { get; set; }
        public double dEta_dx { get; set; }
        public double dEta_dy { get; set; }
        public double delta_y_t { get; set; }


        public double P_p { get; set; }
        public double Rho_p { get; set; }

        public double SF1 { get; set; }
        public double SF2 { get; set; }
        public double SF3 { get; set; }
        public double SF4 { get; set; }


        public double dF1_x_av { get; set; }
        public double dF2_x_av { get; set; }
        public double dF3_x_av { get; set; }
        public double dF4_x_av { get; set; }

        // for the initial data line
        public void Compute_G_F(double Gamma)
        {

            this.F1 = Rho * u;
            this.F2 = Rho * Math.Pow(u, 2) + P;
            this.F3 = Rho * u * v;
            this.F4 = Gamma / (Gamma - 1) * P * u + Rho * u * (Math.Pow(u, 2) + Math.Pow(v, 2)) / 2;

            this.G1 = Rho * (F3 / F1);
            this.G2 = F3;
            this.G3 = Rho * Math.Pow(F3 / F1, 2) + F2 - Math.Pow(F1, 2) / Rho;
            this.G4 = Gamma / (Gamma - 1) * (F2 - Math.Pow(F1, 2) / Rho) * (F3 / F1) + Rho * F3 / (2 * F1) * (Math.Pow(F1 / Rho, 2) + Math.Pow(F3 / F1, 2));

        }

        public void Compute_y_t(double y_t_debajo, double delta_y_t)
        { 
            this.y_t = y_t_debajo + delta_y_t; 
        }

        public void xy_Transformation_ToEtaXi(double H, double E, double Theta) // mirar si es pot comprimir
        {
            if (x < E)
            {
                this.y_s = 0;
                this.h = H;
            }

            else
            {
                this.y_s = -(x - E) * Math.Tan(Theta);
                this.h = H + ((x - E) * Math.Tan(Theta));
            }


            if (x < E)
            {
                this.dEta_dx = 0;
            }
            else
            {
                double Eta = (y - y_s) / h;
                this.dEta_dx = Math.Tan(Theta) / h *(1-Eta);
            }
            
            this.y = h*y_t+y_s;
            this.dEta_dy = 1/h;       
        }

        public double TanMax(double Theta)
        {
            this.tan_max = Math.Max(Math.Abs(Math.Tan(Theta + this.M_angle)), Math.Abs(Math.Tan(Theta - this.M_angle)));
            return tan_max;
            
        }

        public double[] Predictor_Step_Principal (double Cy, double delta_y_t,double delta_x, double F1_arriba, double F2_arriba,double F3_arriba, double F4_arriba, double F1_abajo,double F2_abajo, double F3_abajo, double F4_abajo ,double G1_arriba,double G2_arriba, double G3_arriba, double G4_arriba, double P_arriba, double P_abajo)
        { 
            dF1_x = (dEta_dx * ((F1 - F1_arriba) / delta_y_t)) + (dEta_dy * ((G1 - G1_arriba) / delta_y_t));
            dF2_x = (dEta_dx * ((F2 - F2_arriba) / delta_y_t)) + (dEta_dy * ((G2 - G2_arriba) / delta_y_t));
            dF3_x = (dEta_dx * ((F3 - F3_arriba) / delta_y_t)) + (dEta_dy * ((G3 - G3_arriba) / delta_y_t));
            dF4_x = (dEta_dx * ((F4 - F4_arriba) / delta_y_t)) + (dEta_dy * ((G4 - G4_arriba) / delta_y_t));

            double SF1 = ((Cy * (Math.Abs(P_arriba - (2 * P) + P_abajo))) / (P_arriba + 2 * P + P_abajo)) * (F1_arriba - (2 * F1) + F1_abajo);
            double SF2 = ((Cy * (Math.Abs(P_arriba - (2 * P) + P_abajo))) / (P_arriba + 2 * P + P_abajo)) * (F2_arriba - (2 * F2) + F2_abajo);
            double SF3 = ((Cy * (Math.Abs(P_arriba - (2 * P) + P_abajo))) / (P_arriba + 2 * P + P_abajo)) * (F3_arriba - (2 * F3) + F3_abajo);
            double SF4 = ((Cy * (Math.Abs(P_arriba - (2 * P) + P_abajo))) / (P_arriba + 2 * P + P_abajo)) * (F4_arriba - (2 * F4) + F4_abajo);

            double F1_p_derecha = F1 + (dF1_x * delta_x) + SF1;
            double F2_p_derecha = F2 + (dF2_x * delta_x) + SF2;
            double F3_p_derecha = F3 + (dF3_x * delta_x) + SF3;
            double F4_p_derecha = F4 + (dF4_x * delta_x) + SF4;
            double[] F_p_derecha_vector = { F1_p_derecha, F2_p_derecha, F3_p_derecha, F4_p_derecha };

            return F_p_derecha_vector;
        }

        public double[] Predictor_Step_Contorno_Superior (double delta_y_t, double delta_x, double F1_abajo, double F2_abajo, double F3_abajo, double F4_abajo, double G1_abajo, double G2_abajo, double G3_abajo, double G4_abajo)
        {
            dF1_x = (dEta_dx * ((F1_abajo - F1) / delta_y_t)) + (dEta_dy * ((G1_abajo - G1) / delta_y_t));
            dF2_x = (dEta_dx * ((F2_abajo - F2) / delta_y_t)) + (dEta_dy * ((G2_abajo - G2) / delta_y_t));
            dF3_x = (dEta_dx * ((F3_abajo - F3) / delta_y_t)) + (dEta_dy * ((G3_abajo - G3) / delta_y_t));
            dF4_x = (dEta_dx * ((F4_abajo - F4) / delta_y_t)) + (dEta_dy * ((G4_abajo - G4) / delta_y_t));

            double F1_p_derecha = F1 + (dF1_x * delta_x);
            double F2_p_derecha = F2 + (dF2_x * delta_x);
            double F3_p_derecha = F3 + (dF3_x * delta_x);
            double F4_p_derecha = F4 + (dF4_x * delta_x);

            double[] F_p_derecha_vector = { F1_p_derecha, F2_p_derecha, F3_p_derecha, F4_p_derecha };

            return F_p_derecha_vector;
        }

        public double[] Predictor_Step_Contorno_Inferior(double delta_y_t, double delta_x, double F1_arriba, double F2_arriba, double F3_arriba, double F4_arriba,  double G1_arriba, double G2_arriba, double G3_arriba, double G4_arriba)
        {
            dF1_x = (dEta_dx * ((F1- F1_arriba) / delta_y_t)) + (dEta_dy * ((G1 - G1_arriba) / delta_y_t));
            dF2_x = (dEta_dx * ((F2- F2_arriba) / delta_y_t)) + (dEta_dy * ((G2 - G2_arriba) / delta_y_t));
            dF3_x = (dEta_dx * ((F3- F3_arriba) / delta_y_t)) + (dEta_dy * ((G3 - G3_arriba) / delta_y_t));
            dF4_x = (dEta_dx * ((F4- F4_arriba) / delta_y_t)) + (dEta_dy * ((G4 - G4_arriba) / delta_y_t));

            double F1_p_derecha = F1 + (dF1_x * delta_x);
            double F2_p_derecha = F2 + (dF2_x * delta_x);
            double F3_p_derecha = F3 + (dF3_x * delta_x);
            double F4_p_derecha = F4 + (dF4_x * delta_x);
            double[] F_p_derecha_vector = new double[] { F1_p_derecha, F2_p_derecha, F3_p_derecha, F4_p_derecha };

            return F_p_derecha_vector;
        }

        public double[] Gp_Rhop_Pp_Predicted(double Gamma, double F1_p_derecha, double F2_p_derecha, double F3_p_derecha, double F4_p_derecha)
        { 
            double A_p= ((Math.Pow(F3_p_derecha,2))/(2*F1_p_derecha)) - F4_p_derecha;
            double B_p= (Gamma/(Gamma - 1))*F1_p_derecha*F2_p_derecha;
            double C_p= -(((Gamma + 1)/(2*(Gamma - 1)))*(Math.Pow(F1_p_derecha,3)));
            
            double Rho_p_derecha= (-B_p + (Math.Sqrt((Math.Pow(B_p, 2)) - (4 * A_p * C_p)))) / (2 * A_p);
            double P_p_derecha= F2_p_derecha - ((Math.Pow(F1_p_derecha, 2)) / Rho_p_derecha);

            double G1_p_derecha = Rho_p_derecha * (F3_p_derecha / F1_p_derecha);
            double G2_p_derecha = F3_p_derecha;
            double G3_p_derecha = (Rho_p_derecha * (Math.Pow((F3_p_derecha / F1_p_derecha), 2))) + F2_p_derecha - ((Math.Pow(F1_p_derecha, 2)) / Rho_p_derecha);
            double G4_p_derecha = ((Gamma / (Gamma - 1)) * ((F2_p_derecha) - ((Math.Pow(F1_p_derecha, 2)) / Rho_p_derecha)) * (F3_p_derecha / F1_p_derecha)) + (((Rho_p_derecha * F3_p_derecha) / (2 * F1_p_derecha)) * ((Math.Pow((F1_p_derecha / Rho_p_derecha), 2)) + (Math.Pow((F3_p_derecha / F1_p_derecha), 2))));

            double[] G_predictedValues = { G1_p_derecha, G2_p_derecha, G3_p_derecha, G4_p_derecha, Rho_p_derecha, P_p_derecha };

            return G_predictedValues;

        }

        public double[] Corrector_Step_Principal (double Cy, double delta_x,double delta_y_t, double F1_p_debajo_derecha,double F2_p_debajo_derecha, double F3_p_debajo_derecha, double F4_p_debajo_derecha,
            double F1_p_derecha, double F2_p_derecha, double F3_p_derecha,double F4_p_derecha, double G1_p_debajo_derecha, double G2_p_debajo_derecha,double G3_p_debajo_derecha,double G4_p_debajo_derecha, 
            double G1_p_derecha,   double G2_p_derecha,  double G3_p_derecha, double G4_p_derecha, double F1_p_derecha_arriba, double F2_p_derecha_arriba,double F3_p_derecha_arriba, double F4_p_derecha_arriba,
             double P_p_arriba_derecha, double P_p_derecha, double P_p_abajo_derecha)
        {
            double dF1_p_x_derecha = (dEta_dx * ((F1_p_debajo_derecha - F1_p_derecha) / delta_y_t)) + (dEta_dy * ((G1_p_debajo_derecha - G1_p_derecha) / delta_y_t));
            double dF2_p_x_derecha = (dEta_dx * ((F2_p_debajo_derecha - F2_p_derecha) / delta_y_t)) + (dEta_dy * ((G2_p_debajo_derecha - G2_p_derecha) / delta_y_t));
            double dF3_p_x_derecha = (dEta_dx * ((F3_p_debajo_derecha - F3_p_derecha) / delta_y_t)) + (dEta_dy * ((G3_p_debajo_derecha - G3_p_derecha) / delta_y_t));
            double dF4_p_x_derecha = (dEta_dx * ((F4_p_debajo_derecha - F4_p_derecha) / delta_y_t)) + (dEta_dy * ((G4_p_debajo_derecha - G4_p_derecha) / delta_y_t));

            double SF1_p = (((Cy * (Math.Abs(P_p_arriba_derecha) - (2 * P_p_derecha) + P_p_abajo_derecha))) / (P_p_arriba_derecha + 2 * P_p_derecha + P_p_abajo_derecha)) * (F1_p_derecha_arriba - (2 * F1_p_derecha) + F1_p_debajo_derecha);
            double SF2_p = (((Cy * (Math.Abs(P_p_arriba_derecha) - (2 * P_p_derecha) + P_p_abajo_derecha))) / (P_p_arriba_derecha + 2 * P_p_derecha + P_p_abajo_derecha)) * (F2_p_derecha_arriba - (2 * F2_p_derecha) + F2_p_debajo_derecha);
            double SF3_p = (((Cy * (Math.Abs(P_p_arriba_derecha) - (2 * P_p_derecha) + P_p_abajo_derecha))) / (P_p_arriba_derecha + 2 * P_p_derecha + P_p_abajo_derecha)) * (F3_p_derecha_arriba - (2 * F3_p_derecha) + F3_p_debajo_derecha);
            double SF4_p = (((Cy * (Math.Abs(P_p_arriba_derecha) - (2 * P_p_derecha) + P_p_abajo_derecha))) / (P_p_arriba_derecha + 2 * P_p_derecha + P_p_abajo_derecha)) * (F4_p_derecha_arriba - (2 * F4_p_derecha) + F4_p_debajo_derecha);

            double dF1_x_av = 0.5*(dF1_x + dF1_p_x_derecha);
            double dF2_x_av = 0.5*(dF2_x + dF2_p_x_derecha);
            double dF3_x_av = 0.5*(dF3_x + dF3_p_x_derecha);
            double dF4_x_av = 0.5*(dF4_x + dF4_p_x_derecha);

            double F1_derecha = F1 + (dF1_x_av * delta_x) + SF1_p;
            double F2_derecha = F2 + (dF2_x_av * delta_x) + SF2_p;
            double F3_derecha = F3 + (dF3_x_av * delta_x) + SF3_p;
            double F4_derecha = F4 + (dF4_x_av * delta_x) + SF4_p;

            double[] F_Derecha = { F1_derecha, F2_derecha, F3_derecha, F4_derecha };
            return F_Derecha;

        }

        public double[] Corrector_Step_Contorno_Inferior (double delta_y_t, double F1_p_derecha, double F2_p_derecha, double F3_p_derecha, double F4_p_derecha, double F1_p_arriba_derecha, double F2_p_arriba_derecha, double F3_p_arriba_derecha, double F4_p_arriba_derecha, double G1_p_derecha, double G2_p_derecha, double G3_p_derecha, double G4_p_derecha, double G1_p_arriba_derecha, double G2_p_arriba_derecha, double G3_p_arriba_derecha, double G4_p_arriba_derecha, double delta_x)
        {
            double dF1_p_x_derecha = (dEta_dx * ((F1_p_derecha - F1_p_arriba_derecha) / delta_y_t)) + (dEta_dy * ((G1_p_derecha - G1_p_arriba_derecha) / delta_y_t));
            double dF2_p_x_derecha = (dEta_dx * ((F2_p_derecha - F2_p_arriba_derecha) / delta_y_t)) + (dEta_dy * ((G2_p_derecha - G2_p_arriba_derecha) / delta_y_t));
            double dF3_p_x_derecha = (dEta_dx * ((F3_p_derecha - F3_p_arriba_derecha) / delta_y_t)) + (dEta_dy * ((G3_p_derecha - G3_p_arriba_derecha) / delta_y_t));
            double dF4_p_x_derecha = (dEta_dx * ((F4_p_derecha - F4_p_arriba_derecha) / delta_y_t)) + (dEta_dy * ((G4_p_derecha - G4_p_arriba_derecha) / delta_y_t));

            double dF1_x_av = 0.5 * (dF1_x + dF1_p_x_derecha);
            double dF2_x_av = 0.5 * (dF2_x + dF2_p_x_derecha);
            double dF3_x_av = 0.5 * (dF3_x + dF3_p_x_derecha);
            double dF4_x_av = 0.5 * (dF4_x + dF4_p_x_derecha);

            double F1_derecha = F1 + (dF1_x_av * delta_x);
            double F2_derecha = F2 + (dF2_x_av * delta_x);
            double F3_derecha = F3 + (dF3_x_av * delta_x);
            double F4_derecha = F4 + (dF4_x_av * delta_x);

            double[] F_Derecha = { F1_derecha, F2_derecha, F3_derecha, F4_derecha };
            return F_Derecha;
        }

        public double[] Corrector_Step_Contorno_Superior(double delta_y_t, double F1_p_derecha, double F2_p_derecha, double F3_p_derecha, double F4_p_derecha, double F1_p_debajo_derecha, double F2_p_debajo_derecha, double F3_p_debajo_derecha, double F4_p_debajo_derecha, double G1_p_derecha, double G2_p_derecha, double G3_p_derecha, double G4_p_derecha, double G1_p_debajo_derecha, double G2_p_debajo_derecha, double G3_p_debajo_derecha, double G4_p_debajo_derecha, double delta_x)
        {
            double dF1_p_x_derecha = (dEta_dx * ((F1_p_debajo_derecha - F1_p_derecha) / delta_y_t)) + (dEta_dy * ((G1_p_debajo_derecha - G1_p_derecha) / delta_y_t));
            double dF2_p_x_derecha = (dEta_dx * ((F2_p_debajo_derecha - F2_p_derecha) / delta_y_t)) + (dEta_dy * ((G2_p_debajo_derecha - G2_p_derecha) / delta_y_t));
            double dF3_p_x_derecha = (dEta_dx * ((F3_p_debajo_derecha - F3_p_derecha) / delta_y_t)) + (dEta_dy * ((G3_p_debajo_derecha - G3_p_derecha) / delta_y_t));
            double dF4_p_x_derecha = (dEta_dx * ((F4_p_debajo_derecha - F4_p_derecha) / delta_y_t)) + (dEta_dy * ((G4_p_debajo_derecha - G4_p_derecha) / delta_y_t));

            double dF1_x_av = 0.5 * (dF1_x + dF1_p_x_derecha);
            double dF2_x_av = 0.5 * (dF2_x + dF2_p_x_derecha);
            double dF3_x_av = 0.5 * (dF3_x + dF3_p_x_derecha);
            double dF4_x_av = 0.5 * (dF4_x + dF4_p_x_derecha);

            double F1_derecha = F1 + (dF1_x_av * delta_x);
            double F2_derecha = F2 + (dF2_x_av * delta_x);
            double F3_derecha = F3 + (dF3_x_av * delta_x);
            double F4_derecha = F4 + (dF4_x_av * delta_x);

            double[] F_Derecha = { F1_derecha, F2_derecha, F3_derecha, F4_derecha };
            return F_Derecha;
        }

        public void Wall_Bounday_Condition(double Gamma, double R_aire, double E, double theta)
        {
            double A = (Math.Pow(F3, 2) / (2 * F1)) - F4;
            double B = (Gamma / (Gamma - 1)) * F1 * F2;
            double C = -(((Gamma + 1) / (2 * (Gamma - 1))) * Math.Pow(F1, 3));

            double Rho_cal = (-B + Math.Sqrt(Math.Pow(B, 2) - (4 * A * C))) / (2 * A);
            double u_cal = F1 / Rho_cal;
            double v_cal = F3 / F1;
            double P_cal = F2 - (F1 * u_cal);
            double T_cal = P_cal / (R_aire * Rho_cal);
            double M_cal = (Math.Sqrt((Math.Pow(u_cal, 2)) + (Math.Pow(v_cal, 2)))) / Math.Sqrt(Gamma * R_aire * T_cal);

            double phi;
            if (x < E)
            {
                phi = Math.Atan(v_cal / u_cal);
            }
            else
            {
                phi = theta - (Math.Atan(Math.Abs(v_cal) / u_cal));

            }
            double patata = Math.Sqrt((Gamma + 1) / (Gamma - 1));
            double moniato = Math.Sqrt((Gamma - 1) / (Gamma + 1) * (M_cal* M_cal - 1));
            double pastanaga = Math.Atan(moniato);
            double mandarina = Math.Atan(Math.Sqrt(Math.Pow(M_cal, 2) - 1));

            double f_cal = patata * pastanaga - mandarina;
            double f_act = f_cal + phi;

            //double a_int = 1.1;
            //double b_int = 2.9;
            //double precision = 0.0000001;
            //double zero_f1 = Math.Sqrt((Gamma + 1) / (Gamma - 1)) * (Math.Atan(Math.Sqrt(((Gamma - 1) / (Gamma + 1)) * (Math.Pow(a_int, 2) - 1)))) - (Math.Atan(Math.Sqrt(Math.Pow(a_int, 2) - 1))) - f_act;
            //double zero_f2 = Math.Sqrt((Gamma + 1) / (Gamma - 1)) * (Math.Atan(Math.Sqrt(((Gamma - 1) / (Gamma + 1)) * (Math.Pow(((a_int + b_int) / 2), 2) - 1)))) - (Math.Atan(Math.Sqrt(Math.Pow(((a_int + b_int) / 2), 2) - 1))) - f_act;
            //while ((b_int - a_int) / 2 > precision)
            //{
            //    if (zero_f1 * zero_f2 <= 0)
            //        b_int = (a_int + b_int) / 2;
            //    else
            //        a_int = (a_int + b_int) / 2;
            //    zero_f1 = Math.Sqrt((Gamma + 1) / (Gamma - 1)) * (Math.Atan(Math.Sqrt(((Gamma - 1) / (Gamma + 1)) * (Math.Pow(a_int, 2) - 1)))) - (Math.Atan(Math.Sqrt(Math.Pow(a_int, 2) - 1))) - f_act;
            //    zero_f2 = Math.Sqrt((Gamma + 1) / (Gamma - 1)) * (Math.Atan(Math.Sqrt(((Gamma - 1) / (Gamma + 1)) * (Math.Pow(((a_int + b_int) / 2), 2) - 1)))) - (Math.Atan(Math.Sqrt(Math.Pow(((a_int + b_int) / 2), 2) - 1))) - f_act;
            //}

            //double M_act = (a_int + b_int) / 2;

            double M_act = compute_M_act(Gamma, f_act); // posible lugar de fallo

            M = M_act;
            M_angle = Math.Asin(1 / M);
            P = P_cal * (Math.Pow(((1 + ((Gamma - 1) / 2) * (Math.Pow(M_cal, 2))) / (1 + ((Gamma - 1) / 2) * (Math.Pow(M_act, 2)))), (Gamma / (Gamma - 1))));
            T = T_cal * ((1 + ((Gamma - 1) / 2) * (Math.Pow(M_cal, 2))) / (1 + ((Gamma - 1) / 2) * (Math.Pow(M_act, 2))));
            Rho = P / (R_aire * T);
            u = u_cal;
            a = Math.Sqrt(Gamma * R_aire * T);

            if (x > E)
            { v = -(u * Math.Tan(theta)); }
            else
            { v = 0; }

            F1 = Rho * u;
            F2 = (Rho * Math.Pow(u, 2)) + P;
            F3 = Rho * u * v;
            F4 = (Gamma / (Gamma - 1)) * P * u + Rho * u * ((Math.Pow(u, 2) + Math.Pow(v, 2)) / 2);

            G1 = Rho * (F3 / F1);
            G2 = F3;
            G3 = (Rho * Math.Pow((F3 / F1), 2)) + F2 - (Math.Pow(F1, 2) / Rho);
            G4 = ((Gamma / (Gamma - 1)) * ((F2 - Math.Pow(F1, 2) / Rho)) * (F3 / F1)) + ((Rho * F3) / (2 * F1)) * (Math.Pow((F1 / Rho), 2) + (Math.Pow(F3 / F1, 2)));

        }

        public void ComputeFinalValues(double Gamma, double R_aire)
        {
            double A =(( F3 * F3) / (2 * F1)) - F4;
            double B = (Gamma / (Gamma - 1)) * F1 * F2;
            double C = -((Gamma + 1) / (2 * (Gamma - 1))) * F1 * F1 * F1;

            this.Rho = (-B + Math.Sqrt((B * B) - (4 * A * C))) / (2 * A);
            this.u = F1 / Rho;
            this.v = F3 / F1;
            this.P = F2 - (F1 * u);
            this.T = P / (Rho * R_aire);
            this.a = Math.Sqrt(Gamma * R_aire * T);
            this.M = Math.Sqrt(Math.Pow(u, 2) + Math.Pow(v, 2)) / a;
            this.M_angle = Math.Asin(1 / M);

            this.G1 = Rho * (F3 / F1);
            this.G2 = F3;
            this.G3 = Rho * Math.Pow((F3 / F1), 2) + F2 - (Math.Pow(F1, 2) / Rho);
            this.G4 = ((Gamma / (Gamma - 1)) * ((F2) - ((Math.Pow(F1, 2)) / Rho)) * (F3 / F1)) + (((Rho * F3) / (2 * F1)) * ((Math.Pow((F1 / Rho), 2)) + (Math.Pow((F3 / F1), 2))));
        }

        public double compute_M_act(double Gamma, double f_act)
        {

            List<double> x0 = new List<double>(); //vector posicion inicial
            List<double> xi = new List<double>(); //vector siguientes posiciones

            x0.Add(2.0); 
            xi.Add(2.4);

            int iteraciones = 30; // maximo de iteraciones
            double Tol = 10e-9; // Tolerancia para el criterio de convergencia a superar o igualar(%)

            for (int i = 0; i < iteraciones; i++)
            { 
              double f_M_i = Math.Sqrt((Gamma + 1) / (Gamma - 1)) * Math.Atan(Math.Sqrt((Gamma - 1) / (Gamma + 1) * (Math.Pow(xi[i],2) - 1))) - Math.Atan(Math.Sqrt(Math.Pow(xi[i],2) - 1)) - f_act;
              double f_M_0 = Math.Sqrt((Gamma + 1) / (Gamma - 1)) * Math.Atan(Math.Sqrt((Gamma - 1) / (Gamma + 1) * (Math.Pow(x0[i], 2) - 1))) - Math.Atan(Math.Sqrt(Math.Pow(x0[i], 2) - 1)) - f_act;
              double xi_2 = xi[i] - f_M_i * (x0[i] - xi[i]) / (f_M_0 - f_M_i);
              xi.Add(xi_2);
              x0.Add(xi[i]);

                if (Math.Abs(f_M_i) < Tol)
                { break; }
            }

            return xi[xi.Count - 1];
    
        }




    }
}
