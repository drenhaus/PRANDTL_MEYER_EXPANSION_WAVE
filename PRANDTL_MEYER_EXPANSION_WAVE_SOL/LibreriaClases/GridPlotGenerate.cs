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

        Polygon[,] casillas;  // we set a matrix of polygons for showing in the grid
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
            // Given a datatable it is actualized the colour of the polygons according to the information
            // that contains the table and the magnitudes. It is used the Max_Min_Datatables function and
            // the Define_Cloroes function
            // A condition is added saying if the data is vertical velocity, in order to avoid some problems 
            // with the 0 values
        public Polygon[,] actualizar_colores_grid(DataTable t, bool DataIsV)
        {
            double[] max_min; // define an array with at posiyion 0 the max value of the datatable and in position 1 the minimum
            max_min = Max_Min_Datatables(t, DataIsV); // calling the max_min_datatable functions to obtain the minimum and maximum

            // Making a loop through all the polygons saved in casillas ans actualisind the colour, calling the function Define_Cloroes
            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    // geting the colour of the polygon
                    byte[] ARGB = Define_Cloroes(max_min[0], max_min[1], Convert.ToDouble(t.Rows[filas - 1 - j][i].ToString()),DataIsV);
                    casillas[j, i].Fill = new SolidColorBrush(Color.FromArgb(ARGB[0], ARGB[1], ARGB[2], ARGB[3])); // changing the colour
                }
            }
            return casillas; // returning the whole matrix with the actulised colours
        }
       
        //CHANGING THE COLOUR OF THE POLYGONS IN ADVANCED STUDY
            // This function works in the same way as the previous one, however, as we want to stablish the same
            // range of colours for the two cases comparing grids, two datatables are introduced
        public Polygon[,] actualizar_colores_grid_AS(DataTable t, DataTable t_minmax, int filas_t, int columnas_t, DataTable t_minmax2)
        {
            
            double[] max_min1; // max and min values of datatable 1
            double[] max_min2; // max and min values of datatable 2
            max_min1 = Max_Min_Datatables_AS(t_minmax); // calling the function to obtain max and min values
            max_min2 = Max_Min_Datatables_AS(t_minmax2); // calling the function to obtain max and min values

            double max = Math.Max(max_min1[0], max_min2[0]); // the maximum value would be the higher between the two max from tables 1 and 2
            double min = Math.Min(max_min1[1], max_min2[1]); // the minimum value would be the lower between the two min from tables 1 and 2

            // Making a loop to actualise the colours of the polygons
            for (int i = 0; i < columnas - 1; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    // obtaining the new colour of the polygon
                    byte[] ARGB = Define_Cloroes(max,min, Convert.ToDouble(t.Rows[filas - 1 - j][i].ToString()),true);
                    casillas[j, i].Fill = new SolidColorBrush(Color.FromArgb(ARGB[0], ARGB[1], ARGB[2], ARGB[3])); // actualising the colour
                }
            }
            return casillas; // it is returned the matrix with all the polygons' colours actualised

        }
       
        // DEFINING COLOUR
            // By introducing the maximum, minimum of the table and the current value of the polygon, it is
            // computed the actualised colour that would have the polygon
            // A bool saying if the data introduced is the velocity is added in order to avoid potential mistakes
        public byte[] Define_Cloroes(double max, double min, double value, bool DataIsV)
        {
            // we define the range as the diference between the maximum and minimum value
            double rango = max - min;

            // define the bytes of a RGB colour code and delimiting the values to obtain a good range of colour 

            byte R=0; // from value 255 to 51
            byte G=0; // from 51 to 255 and 51 again
            byte B=0; // from 51 to 255

            byte alpha = 255; // transparency, it is going to be seted as the maximum

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
       
        //MAXIMUM AND MINIMUM VALUE OF A DATATABLE
            // Introducing a datatable the function returns the maximum and minimum
            // value of it
            // Moreover, it is introduced a bool saying if we are dealing with velocity V 
            // to avoid some problems
        public double[] Max_Min_Datatables(DataTable data_t, bool DataIsV)
        {
            // defininf a maximum parameter, as a reference the first one
            double max = Convert.ToDouble(data_t.Rows[0][0].ToString());
            // defining a minimum parameter, as a reference the first one
            double min = Convert.ToDouble(data_t.Rows[0][0].ToString());

            // We make a loop through all the values of the datatable to check the values
            for (int i = 0; i < columnas; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    // if we find a smaller value as the one defined as min
                    if (Convert.ToDouble(data_t.Rows[j][i].ToString()) < min)
                    {
                        if (DataIsV == false && data_t.Rows[j][i].ToString() == "0")
                        { } 
                        // if we are not dealing with vertical velocity and we have a 0 it is going 
                        // to be an indicator that we are making more columns than needed and are filled 
                        // with 0 values

                        else
                        {
                            min = Convert.ToDouble(data_t.Rows[j][i].ToString());
                            // actualising the minimum value
                        }
                    }
                    // if we find a bigger value than the one set as max
                    if (Convert.ToDouble(data_t.Rows[j][i].ToString()) > max)
                    {
                        max = Convert.ToDouble(data_t.Rows[j][i].ToString());
                        // we actualise the max value
                    }

                }
            }
            double[] values = { max, min };
            return values; // returning the max and min values
        }
        // MAXIMUM AND MINIMUM VALUE OF A DATATABLE FOR ADVANCED STUDY
            // The same function that before but addapted for advanced study
        public double[] Max_Min_Datatables_AS(DataTable data_t)
        {
            // we first define the number of columns of the table
            int columnas_C = data_t.Columns.Count;
            // defining the number of rows of the table
            int filas_C = data_t.Rows.Count;
            // maximum and minimum values, set as the first value of the table by default
            double max = Convert.ToDouble(data_t.Rows[0][0].ToString());
            double min = Convert.ToDouble(data_t.Rows[0][0].ToString());

            // making a loop to get the absolute max and min
            for (int i = 0; i < columnas_C; i++)
            {
                for (int j = 0; j < filas_C; j++)
                {
                    // if we find a smaller value as the one defined as min
                    if (Convert.ToDouble(data_t.Rows[j][i].ToString()) < min)
                    {
                        min = Convert.ToDouble(data_t.Rows[j][i].ToString()); // actualising the min value
                    }
                    // if we find a bigger value than the one set as max
                    if (Convert.ToDouble(data_t.Rows[j][i].ToString()) > max)
                    {
                        max = Convert.ToDouble(data_t.Rows[j][i].ToString()); // actualising the max value
                    }

                }
            }
            double[] values = { max, min };
            return values; // returning the max and min values
        }
        #endregion DATATABLE MANIPULATIONS

    }
}
