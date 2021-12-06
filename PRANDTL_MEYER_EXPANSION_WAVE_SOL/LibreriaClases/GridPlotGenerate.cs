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
        Polygon[,] casillas;
        int dimension_scale = 7;

        int filas;
        int columnas;
        
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

                    //myPolygon.MouseEnter += new System.Windows.Input.MouseEventHandler(polygon_enter);

                    y4 = y2;
                    y3 = y1;


                }
                x1 = x2;
            }
            

            return casillas;
        }

        public Polygon[,] actualizar_colores_grid(DataTable t, byte R, byte G, byte B)
        {
            double[] max_min;
            max_min = Max_Min_Datatables(t);

            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    byte alpha = Define_Cloroes(max_min[0], max_min[1], Convert.ToDouble(t.Rows[filas - 1 - j][i].ToString()));
                    casillas[j, i].Fill = new SolidColorBrush(Color.FromArgb(alpha, R, G, B));
                }
            }
            return casillas;

        }

        public Polygon[,] actualizar_colores_grid_AS(DataTable t, byte R, byte G, byte B, DataTable t_minmax, int filas_t, int columnas_t)
        {
            double[] max_min;
            max_min = Max_Min_Datatables_AS(t_minmax);

            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    byte alpha = Define_Cloroes(max_min[0], max_min[1], Convert.ToDouble(t.Rows[filas - 1 - j][i].ToString()));
                    casillas[j, i].Fill = new SolidColorBrush(Color.FromArgb(alpha, R, G, B));
                }
            }
            return casillas;

        }
        public double[] Max_Min_Datatables(DataTable data_t)
        {
            double max = Convert.ToDouble(data_t.Rows[0][0].ToString());
            double min = Convert.ToDouble(data_t.Rows[0][0].ToString());
            for (int i = 0; i < columnas; i++)
            {
                for (int j = 0; j < filas; j++)
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


        public byte Define_Cloroes(double max, double min, double value)
        {
            double rango = max - min;

            byte alpha;

            //max byte --255
            // min byte --30

            //interpolamos para sacar el parametro alpa
            alpha = Convert.ToByte(30 + (255 - 30) / (max - min) * (value - min));

            return alpha;

        }

    }
}
