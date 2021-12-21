using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibreriaClases;
using System.Data;

namespace LibreriaClases
{
    public class GridPlotGenerate
    {
        #region ATRIBUTES

        Polygon[,] casillas;
 
        int filas;
        int columnas;

        public int dimension_scale { get; set; } = 7;

        #endregion ATRIBUTES

        #region GRID PLOR GENERATOR
        public Polygon[,] GenerateGridPlot(int filas, int columnas, Malla m)
        {
            this.filas = filas;
            this.columnas = columnas;

            casillas = new Polygon[filas, columnas - 1];

            double x1 = 0; // column left
            double x2; // column right
            double y1; // top left
            double y2; // top right
            double y3; // down left
            double y4; // down right

            double[] delta_y = m.Vector_Delta_y();


            for (int i = 0; i < columnas - 1; i++)
            {
                x2 = x1 + m.delta_x;
                y3 = 0;
                y4 = 0;

                

                for (int j = 0; j < filas; j++)
                {
                    y1 = y3 + delta_y[i];
                    y2 = y4 + delta_y[i + 1];

                    Polygon myPolygon = new Polygon();
                    System.Windows.Point Point1 = new System.Windows.Point(x1 * dimension_scale, y1 * dimension_scale);
                    System.Windows.Point Point2 = new System.Windows.Point(x2 * dimension_scale, y2 * dimension_scale);
                    System.Windows.Point Point3 = new System.Windows.Point(x1 * dimension_scale, y3 * dimension_scale);
                    System.Windows.Point Point4 = new System.Windows.Point(x2 * dimension_scale, y4 * dimension_scale);
                    myPolygon.StrokeThickness = 0;
                    PointCollection myPointCollection = new PointCollection();
                    myPointCollection.Add(Point1);
                    myPointCollection.Add(Point2);
                    myPointCollection.Add(Point4);
                    myPointCollection.Add(Point3);
                    myPolygon.Points = myPointCollection;
                    casillas[j, i] = myPolygon;
                    
                   // myPolygon.MouseEnter += new System.Windows.Input.MouseEventHandler(polygon_enter);

                    y4 = y2;
                    y3 = y1;


                }
                x1 = x2;
            }
            

            return casillas;
        }
        #endregion GRID PLOT GENERATOR

        #region UPDATE GRID COLOR
        public Polygon[,] actualizar_colores_grid(DataTable t, bool DataIsV)
        {
            double[] max_min;
            max_min = Max_Min_Datatables(t, DataIsV);

            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    byte[] ARGB = Define_Cloroes(max_min[0], max_min[1], Convert.ToDouble(t.Rows[filas - 1 - j][i].ToString()),DataIsV);
                    casillas[j, i].Fill = new SolidColorBrush(Color.FromArgb(ARGB[0], ARGB[1], ARGB[2], ARGB[3]));
                }
            }
            return casillas;

        }
        public Polygon[,] actualizar_colores_grid_AS(DataTable t, DataTable t_minmax, int filas_t, int columnas_t, DataTable t_minmax2)
        {
            
            double[] max_min1;
            double[] max_min2;
            max_min1 = Max_Min_Datatables_AS(t_minmax);
            max_min2 = Max_Min_Datatables_AS(t_minmax2);

            double max = Math.Max(max_min1[0], max_min2[0]);
            double min = Math.Min(max_min1[1], max_min2[1]);

            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    byte[] ARGB = Define_Cloroes(max,min, Convert.ToDouble(t.Rows[filas - 1 - j][i].ToString()),true);
                    casillas[j, i].Fill = new SolidColorBrush(Color.FromArgb(ARGB[0], ARGB[1], ARGB[2], ARGB[3]));
                }
            }
            return casillas;

        }
        public byte[] Define_Cloroes(double max, double min, double value, bool DataIsV)
        {
            double rango = max - min;

            byte R=0; // from value 255 to 51
            byte G=0; // from 51 to 255 and 51 again
            byte B=0; // from 51 to 255

            byte alpha = 255;

            //max byte --255
            // min byte --30

            if (DataIsV==false && value==0)
            {
                alpha = 0;
            }

            else
            {
                try
                {
                    // defining the 3 different variables
                    if (value <= min+ rango/4)
                    {
                        // R: 51 bytes assigned. From 51 to 102
                        // G: 102 bytes assigned. From 51 to 153
                        // B: 51 bytes assigned. From 255 to 204
                       
                        // DEFINING BYTE R
                       int bytemin = 51;
                       int bytemax = 102;
                       double min_value = min;
                       double max_value = min + rango / 4;

                       R = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));

                        // DEFINING BYTE G
                        bytemin = 51;
                        bytemax = 153;

                        G = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));

                        // DEFINING BYTE B
                        bytemin = 255;
                        bytemax = 204;
                       
                        B = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));


                    }
                    else if ((min+rango/4<value) && (value<= min+2*rango/4))
                    {
                        // R: 51 bytes assigned. From 102 to 153
                        // G: 102 bytes assigned. From 153 to 255
                        // B: 51 bytes assigned. From 204 to 153


                        // DEFINING BYTE R
                        int bytemin = 102;
                        int bytemax = 153;
                        double min_value = min+rango/4;
                        double max_value = min + 2 * rango / 4;

                        R = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));

                        // DEFINING BYTE G
                        bytemin = 153;
                        bytemax = 255;

                        G = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));

                        // DEFINING BYTE B
                        bytemin = 204;
                        bytemax = 153;

                        B = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));

                    }
                    else if ((min + 2*rango / 4 < value) && (value <= min + 3 * rango / 4))
                    {
                        // R: 51 bytes assigned. From 153 to 204
                        // G: 102 bytes assigned. From 255 to 153
                        // B: 51 bytes assigned. From 153 to 102


                        // DEFINING BYTE R
                        int bytemin = 153;
                        int bytemax = 204;
                        double min_value = min + 2 * rango / 4;
                        double max_value = min + 3 * rango / 4;

                        R = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));

                        // DEFINING BYTE G
                        bytemin = 255;
                        bytemax = 153;

                        G = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));

                        // DEFINING BYTE B
                        bytemin = 153;
                        bytemax = 102;

                        B = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));


                    }
                    else
                    {
                        // R: 51 bytes assigned. From 204 to 255
                        // G: 102 bytes assigned. From 153 to 51
                        // B: 51 bytes assigned. From 102 to 51

                        // DEFINING BYTE R
                        int bytemin = 204;
                        int bytemax = 255;
                        double min_value = min + 3 * rango / 4;
                        double max_value = max;

                        R = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));

                        // DEFINING BYTE G
                        bytemin = 153;
                        bytemax = 51;

                        G = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));

                        // DEFINING BYTE B
                        bytemin = 102;
                        bytemax = 51;

                        B = Convert.ToByte(bytemin + (bytemax - bytemin) / (max_value - min_value) * (value - min_value));
                    }

                    alpha = 255;
                }
                catch { }
            }

            byte[] byte_Values = { alpha, R, G, B };
           

            return byte_Values;

        }

        #endregion UPDATE GRID COLOR

        #region DATATABLE MANIPULATIONS
        public double[] Max_Min_Datatables(DataTable data_t, bool DataIsV)
        {

            double max = Convert.ToDouble(data_t.Rows[0][0].ToString());
            double min = Convert.ToDouble(data_t.Rows[0][0].ToString());
            for (int i = 0; i < columnas; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    if (Convert.ToDouble(data_t.Rows[j][i].ToString()) < min)
                    {
                        if (DataIsV == false && data_t.Rows[j][i].ToString() == "0")
                        { }
                        else
                        {
                            min = Convert.ToDouble(data_t.Rows[j][i].ToString());
                        }
                    }
                    if (Convert.ToDouble(data_t.Rows[j][i].ToString()) > max)
                    {
                        max = Convert.ToDouble(data_t.Rows[j][i].ToString());
                    }

                }
            }
            double[] values = { max, min };
            return values;
        }

        public double[] Max_Min_Datatables_AS(DataTable data_t)
        {
            int columnas_C = data_t.Columns.Count;
            int filas_C = data_t.Rows.Count;
            double max = Convert.ToDouble(data_t.Rows[0][0].ToString());
            double min = Convert.ToDouble(data_t.Rows[0][0].ToString());
            for (int i = 0; i < columnas_C; i++)
            {
                for (int j = 0; j < filas_C; j++)
                {
                    if (Convert.ToDouble(data_t.Rows[j][i].ToString()) < min)
                    {
                        min = Convert.ToDouble(data_t.Rows[j][i].ToString());
                    }
                    if (Convert.ToDouble(data_t.Rows[j][i].ToString()) > max)
                    {
                        max = Convert.ToDouble(data_t.Rows[j][i].ToString());
                    }

                }
            }
            double[] values = { max, min };
            return values;
        }
        #endregion DATATABLE MANIPULATIONS

    }
}
