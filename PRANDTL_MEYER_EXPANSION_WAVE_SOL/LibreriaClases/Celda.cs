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

        double dF1_x_av;
        double dF2_x_av;
        double dF3_x_av;
        double dF4_x_av;



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


        public double[] G_Predicted(double Gamma, double F3_p_derecha, double F1_p_derecha, double F4_p_derecha, double F2_p_derecha)
        { 
            double A_p=Math.Pow(F3_p_derecha,2)/(2*F1_p_derecha) - F4_p_derecha;
            double B_p=(Gamma/(Gamma - 1))*F1_p_derecha*F2_p_derecha;
            double C_p=-(Gamma + 1)/(2*(Gamma - 1))*Math.Pow(F1_p_derecha,3);
            
            double Rho_p= (-B_p + (Math.Sqrt((Math.Pow(B_p, 2)) - (4 * A_p * C_p)))) / (2 * A_p);
            P_p= F2_p_derecha - Math.Pow(F1_p_derecha , 2) / Rho_p;

            double G1_p_derecha = Rho_p * (F3_p_derecha / F1_p_derecha);
            double G2_p_derecha = F3_p_derecha;
            double G3_p_derecha = (Rho_p *  Math.Pow(F3_p_derecha /F1_p_derecha, 2)) + F2_p_derecha - (Math.Pow(F1_p_derecha, 2) / Rho_p);
            double G4_p_derecha = ((Gamma / (Gamma - 1)) * ((F2_p_derecha) - (Math.Pow(F1_p_derecha ,2) / Rho_p)) * (F3_p_derecha / F1_p_derecha)) + Rho_p * F3_p_derecha / (2 * F1_p_derecha) * ((F1_p_derecha/ Math.Pow(Rho_p,2)) + F3_p_derecha / Math.Pow(F1_p_derecha,2));


            double[] G_predictedValues = new double[] { G1_p_derecha, G2_p_derecha, G3_p_derecha, G4_p_derecha, Rho_p, P_p };

            return G_predictedValues;

        }

        public double[] CorrectorStep (double F1_p_debajo_derecha, double delta_x, double F1_p_derecha, double delta_y_t, double m1, double m2, double G1_p_debajo_derecha,
            double G1_p_derecha, double F2_p_debajo_derecha, double F2_p_derecha, double G2_p_debajo_derecha, double G2_p_derecha,
            double F3_p_debajo_derecha, double F3_p_derecha, double G3_p_debajo_derecha, double G3_p_derecha, 
            double F4_p_debajo_derecha, double F4_p_derecha, double G4_p_debajo_derecha, double G4_p_derecha,
            double P_p_arriba, double P_p, double P_p_abajo, double F1_p_derecha_arriba, double F1_p_derecha_abajo, double F2_p_derecha_arriba, double F3_p_derecha_arriba, double F4_p_derecha_arriba, double F2_p_derecha_abajo, double F3_p_derecha_abajo, double F4_p_derecha_abajo )
        {
            dF1_p_x = (m1 * ((F1_p_debajo_derecha - F1_p_derecha) / delta_y_t)) + (m2 * ((G1_p_debajo_derecha - G1_p_derecha) / delta_y_t));
            dF2_p_x = (m1 * ((F2_p_debajo_derecha - F2_p_derecha) / delta_y_t)) + (m2 * ((G2_p_debajo_derecha - G2_p_derecha) / delta_y_t));
            dF3_p_x = (m1 * ((F3_p_debajo_derecha - F3_p_derecha) / delta_y_t)) + (m2 * ((G3_p_debajo_derecha - G3_p_derecha) / delta_y_t));
            dF4_p_x = (m1 * ((F4_p_debajo_derecha - F4_p_derecha) / delta_y_t)) + (m2 * ((G4_p_debajo_derecha - G4_p_derecha) / delta_y_t));

            double SF1_p = (((0.6 * (Math.Abs(P_p_arriba) - (2 * P_p) + P_p_abajo))) / (P_p_arriba + 2 * P_p + P_p_abajo)) * (F1_p_derecha_arriba - (2 * F1_p_derecha + F1_p_derecha_abajo));
            double SF2_p = (((0.6 * (Math.Abs(P_p_arriba) - (2 * P_p) + P_p_abajo))) / (P_p_arriba + 2 * P_p + P_p_abajo)) * (F2_p_derecha_arriba - (2 * F2_p_derecha + F2_p_derecha_abajo));
            double SF3_p = (((0.6 * (Math.Abs(P_p_arriba) - (2 * P_p) + P_p_abajo))) / (P_p_arriba + 2 * P_p + P_p_abajo)) * (F3_p_derecha_arriba - (2 * F3_p_derecha + F3_p_derecha_abajo));
            double SF4_p = (((0.6 * (Math.Abs(P_p_arriba) - (2 * P_p) + P_p_abajo))) / (P_p_arriba + 2 * P_p + P_p_abajo)) * (F4_p_derecha_arriba - (2 * F4_p_derecha + F4_p_derecha_abajo));

            dF1_x_av = 0.5*(dF1_x + dF1_p_x);
            dF2_x_av = 0.5*(dF2_x + dF2_p_x);
            dF3_x_av = 0.5*(dF3_x + dF3_p_x);
            dF4_x_av = 0.5*(dF4_x + dF4_p_x);

            double F1_derecha = F1 + (dF1_x_av * delta_x) + SF1_p;
            double F2_derecha = F2 + (dF2_x_av * delta_x) + SF2_p;
            double F3_derecha = F3 + (dF3_x_av * delta_x) + SF3_p;
            double F4_derecha = F4 + (dF4_x_av * delta_x) + SF4_p;

            double[] F_futuras = { F1_derecha, F2_derecha, F3_derecha, F4_derecha };
            return F_futuras;

        }




        public void decode_Principal()
        {
            double A = ((F3(j, i + 1) ^ 2) / (2 * F1(j, i + 1))) - F4(j, i + 1);
            double B = (Gamma / (Gamma - 1)) * F1(j, i + 1) * F2(j, i + 1);
            double C = -(((Gamma + 1) / (2 * (Gamma - 1))) * (F.F1(j, i + 1) ^ 3));


        }

        public void decode_Contorno_Superior()
        { }

        public void decode_Contorno_Inferior()
        { }

        A = ((F.F3(j, i+1)^2)/(2*F.F1(j, i+1))) - F.F4(j, i+1);
        B = (Gamma/(Gamma - 1))*F.F1(j, i+1)*F.F2(j, i+1);
        C = -(((Gamma + 1)/(2*(Gamma - 1)))*(F.F1(j, i+1)^3));


        if (j == 1) % Apply Abbett's wall boundary condition
            Rho_cal = (-B + (sqrt((B^2) - (4*A* C))))/(2*A);
            u_cal = F.F1(j, i+1)/Rho_cal;
            v_cal = F.F3(j, i+1)/F.F1(j, i+1);
            P_cal = F.F2(j, i+1) - (F.F1(j, i+1)*u_cal);
            T_cal = P_cal/(R_air* Rho_cal);
            M_cal = (sqrt((u_cal^2) + (v_cal)^2))/(sqrt(Gamma* R_air* T_cal));
            if (Grid.x(i) < E)
                phi = atan(v_cal/u_cal);
            else
                phi = Theta - (atan(abs(v_cal)/u_cal));
            end
            f_cal = sqrt((Gamma + 1) / (Gamma - 1)) * (atan(sqrt(((Gamma - 1) / (Gamma + 1)) * (M_cal ^ 2 - 1)))) - (atan(sqrt((M_cal ^ 2) - 1))); % Prandtl-Meyer function
            f_act = f_cal + phi; % Rotated Prandtl-Meyer function

        % We need to find the Mach number corresponding to a Prandtl-Meyer
        % function of value "f_act". Anderson suggests a simple trial and
        % error computation, but I have used a bisection method that I think should be
        % more efficient.

            a_int = 1.1; % Left limit of the interval
            b_int = 2.9; % Right limit of the interval
            precision = 0.0000001; % Max error
            zero_f1 = sqrt((Gamma + 1)/(Gamma - 1))*(atan(sqrt(((Gamma - 1)/(Gamma + 1))*(a_int^2 - 1)))) - (atan(sqrt((a_int^2) - 1))) - f_act; % Function used to find its zero
            zero_f2 = sqrt((Gamma + 1)/(Gamma - 1))*(atan(sqrt(((Gamma - 1)/(Gamma + 1))*(((a_int + b_int)/2)^2 - 1)))) - (atan(sqrt((((a_int + b_int)/2)^2) - 1))) - f_act;
            while ((b_int-a_int)/2 > precision)
                if (zero_f1* zero_f2 <=0)
                    b_int = (a_int + b_int)/2;
                else
                    a_int = (a_int + b_int)/2;
                end
                zero_f1 = sqrt((Gamma + 1) / (Gamma - 1)) * (atan(sqrt(((Gamma - 1) / (Gamma + 1)) * (a_int ^ 2 - 1)))) - (atan(sqrt((a_int ^ 2) - 1))) - f_act;
        zero_f2 = sqrt((Gamma + 1)/(Gamma - 1))*(atan(sqrt(((Gamma - 1)/(Gamma + 1))*(((a_int + b_int)/2)^2 - 1)))) - (atan(sqrt((((a_int + b_int)/2)^2) - 1))) - f_act;
            end
            M_act = (a_int + b_int) / 2; % Corrected Mach number
              Flow_field.M(j, i+1) = M_act;
            Flow_field.M_angle(j, i+1) = (asin(1/Flow_field.M(j, i+1)));
            Flow_field.P(j, i+1) = P_cal* (((1 + ((Gamma - 1)/2)*(M_cal^2))/(1 + ((Gamma - 1)/2)*(M_act^2)))^(Gamma/(Gamma - 1)));
            Flow_field.T(j, i+1) = T_cal* ((1 + ((Gamma - 1)/2)*(M_cal^2))/(1 + ((Gamma - 1)/2)*(M_act^2)));
            Flow_field.Rho(j, i+1) = Flow_field.P(j, i+1)/(R_air* Flow_field.T(j, i+1));
            Flow_field.u(j, i+1) = u_cal;
            Flow_field.a(j, i+1) = sqrt(Gamma* R_air*Flow_field.T(j, i+1));
            if (Grid.x(i) > E)
                Flow_field.v(j, i+1) = -(Flow_field.u(j, i+1)*tan(Theta));
            else
                Flow_field.v(j, i+1) = 0;
            end
        % Finally we correct the F terms
            F.F1(j, i+1) = Flow_field.Rho(j, i+1)*Flow_field.u(j, i+1);
            F.F2(j, i+1) = (Flow_field.Rho(j, i+1)*(Flow_field.u(j, i+1)^2)) + Flow_field.P(j, i+1);
            F.F3(j, i+1) = Flow_field.Rho(j, i+1)*Flow_field.u(j, i+1)*Flow_field.v(j, i+1);
            F.F4(j, i+1) = ((Gamma/(Gamma - 1))*Flow_field.P(j, i+1)*Flow_field.u(j, i+1)) + (Flow_field.Rho(j, i+1)*Flow_field.u(j, i+1)*(((Flow_field.u(j, i+1)^2) + (Flow_field.v(j, i+1)^2)))/2);
        else
            Flow_field.Rho(j, i+1) = (-B + (sqrt((B^2) - (4*A* C))))/(2*A);
            Flow_field.u(j, i+1) = F.F1(j, i+1)/Flow_field.Rho(j, i+1);
            Flow_field.v(j, i+1) = F.F3(j, i+1)/F.F1(j, i+1);
            Flow_field.P(j, i+1) = F.F2(j, i+1) - (F.F1(j, i+1)*Flow_field.u(j, i+1));
            Flow_field.T(j, i+1) = Flow_field.P(j, i+1)/(R_air* Flow_field.Rho(j, i+1));
            Flow_field.a(j, i+1) = sqrt(Gamma* R_air*Flow_field.T(j, i+1));
            Flow_field.M(j, i+1) = (sqrt((Flow_field.u(j, i+1)^2) + (Flow_field.v(j, i+1)^2)))/Flow_field.a(j, i+1);
            Flow_field.M_angle(j, i+1) = asin(1/Flow_field.M(j, i+1));
        end
        G.G1(j, i+1) = Flow_field.Rho(j, i+1)*(F.F3(j, i+1)/F.F1(j, i+1));
        G.G2(j, i+1) = F.F3(j, i+1);
        G.G3(j, i+1) = (Flow_field.Rho(j, i+1)*((F.F3(j, i+1)/F.F1(j, i+1))^2)) + F.F2(j, i+1) - ((F.F1(j, i+1)^2)/Flow_field.Rho(j, i+1));
        G.G4(j, i+1) = ((Gamma/(Gamma - 1))*((F.F2(j, i+1)) - ((F.F1(j, i+1)^2)/Flow_field.Rho(j, i+1)))*(F.F3(j, i+1)/F.F1(j, i+1))) + (((Flow_field.Rho(j, i+1)*F.F3(j, i+1))/(2*F.F1(j, i+1)))*(((F.F1(j, i+1)/Flow_field.Rho(j, i+1))^2) + ((F.F3(j, i+1)/F.F1(j, i+1))^2)));
    end
    Updated_flow_field = Flow_field;
        Updated_G = G;






    }
}
