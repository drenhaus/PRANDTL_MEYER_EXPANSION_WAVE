﻿using System;
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
        
        Polygon[,] casillas; // we set a matrix of polygons for showing in the grid
 
        // the matrix of polygons would have the same dimensions as the matrix set in the main
        int filas; 
        int columnas;

        // we set a proportional value (scaling) for determining how big the polygons are
        public int dimension_scale { get; set; } = 7;

        #endregion ATRIBUTES

        #region GRID PLOR GENERATOR
        
        // GENERATING GRID
            // Given the dimensions of rows and columns and the Malla, a new polygon is created with the pysical
            // plane dimensions
        public Polygon[,] GenerateGridPlot(int filas, int columnas, Malla m)
        {
            // seting the number of rows and columns
            this.filas = filas;
            this.columnas = columnas;

            // defining the size of the matrix
            casillas = new Polygon[filas, columnas - 1]; 

            // vertexs of the polygon
            double x1 = 0; // column left
            double x2; // column right
            double y1; // top left
            double y2; // top right
            double y3; // down left
            double y4; // down right

            //defining all the values of delta_y
            double[] delta_y = m.Vector_Delta_y();

            // loop that creates the polygons
            for (int i = 0; i < columnas - 1; i++)
            {
                x2 = x1 + m.delta_x;
                y3 = 0;
                y4 = 0;


                for (int j = 0; j < filas; j++)
                {
                    y1 = y3 + delta_y[i];
                    y2 = y4 + delta_y[i + 1];

                    Polygon myPolygon = new Polygon(); // new polygon

                    //defining the points
                    System.Windows.Point Point1 = new System.Windows.Point(x1 * dimension_scale, y1 * dimension_scale);
                    System.Windows.Point Point2 = new System.Windows.Point(x2 * dimension_scale, y2 * dimension_scale);
                    System.Windows.Point Point3 = new System.Windows.Point(x1 * dimension_scale, y3 * dimension_scale);
                    System.Windows.Point Point4 = new System.Windows.Point(x2 * dimension_scale, y4 * dimension_scale);
                    myPolygon.StrokeThickness = 0;
                    PointCollection myPointCollection = new PointCollection();

                    //adding the points
                    myPointCollection.Add(Point1);
                    myPointCollection.Add(Point2);
                    myPointCollection.Add(Point4);
                    myPointCollection.Add(Point3);
                    myPolygon.Points = myPointCollection;
                    casillas[j, i] = myPolygon;
                  
                    y4 = y2;
                    y3 = y1;


                }
                x1 = x2;
            }
            

            return casillas;
        }
        #endregion GRID PLOT GENERATOR

        #region UPDATE GRID COLOR

        // CHANGING THE COLOUR OF A POLYGON
            // Given a datatable
        public Polygon[,] actualizar_colores_grid(DataTable t, byte R, byte G, byte B, bool DataIsV)
        {
            double[] max_min;
            max_min = Max_Min_Datatables(t, DataIsV);

            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    byte alpha = Define_Cloroes(max_min[0], max_min[1], Convert.ToDouble(t.Rows[filas - 1 - j][i].ToString()),DataIsV);
                    casillas[j, i].Fill = new SolidColorBrush(Color.FromArgb(alpha, R, G, B));
                }
            }
            return casillas;

        }
        public Polygon[,] actualizar_colores_grid_AS(DataTable t, byte R, byte G, byte B, DataTable t_minmax, int filas_t, int columnas_t, DataTable t_minmax2)
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
                    byte alpha = Define_Cloroes(max,min, Convert.ToDouble(t.Rows[filas - 1 - j][i].ToString()),true);
                    casillas[j, i].Fill = new SolidColorBrush(Color.FromArgb(alpha, R, G, B));
                }
            }
            return casillas;

        }
        public byte Define_Cloroes(double max, double min, double value, bool DataIsV)
        {
            double rango = max - min;

            byte alpha=255; // to initialize the variable

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
                    alpha = Convert.ToByte(30 + (255 - 30) / (max - min) * (value - min));
                }
                catch { }
            }
            
           

            return alpha;

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
