using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using LibreriaClases;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Threading;



namespace WPFapp
{
    /// <summary>
    /// Lógica de interacción para TablesWindow.xaml
    /// </summary>
    public partial class TablesWindow : Window
    {
        #region ATRIBUTES
        // Tables of the different parameters
        DataTable temperature_t;
        DataTable u_t;
        DataTable v_t;
        DataTable rho_t;
        DataTable p_t;
        DataTable M_t;
        DataTable F1_t;
        DataTable F2_t;
        DataTable F3_t;
        DataTable F4_t;
        
        // bool indicating if the table is expanded
        bool expanded = true;
        // timer for the loading function
        DispatcherTimer dispatcherTimer;
        int timer=0; 
        //window of the loading function
        Loading ld;
        #endregion ATRIBUTES

        public TablesWindow()
        {
            InitializeComponent();
        }

        #region TIMERS
        // TMER TICK OF THE FIRST TIMER
            //In the first tick, when timer is =1 the loading window opens and is shown
            // In the second tick the computing function is executed and finally in the last 
            // tick the window closes and the timer is stoped

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (timer==1)
            { 
                //opening new window
                ld = new Loading();
                ld.Show();
            }
            if (timer==2)
                {
                // computing
                Compute();
                }
            if (timer == 3)
            {
                // closing the loading window and stoping the timer
                ld.Close();
                timer = -1;
                dispatcherTimer.Stop();
            }
                timer = timer + 1;
        }
        
       
        #endregion TIMERS

        #region TABLES FUNCIONTS
        //SETING THE TABLES
            // Introducing the different tables in a concrete order, actualises the tables from
            // the attributes
        public void SetTables(DataTable T, DataTable u, DataTable v, DataTable rho, DataTable p, DataTable M, DataTable f1, DataTable f2, DataTable f3, DataTable f4)
        {
            this.temperature_t= T;
            this.u_t=u;
            this.v_t=v;
            this.rho_t=rho;
            this.p_t=p;
            this.M_t=M;
            this.F1_t=f1;
            this.F2_t=f2;
            this.F3_t=f3;
            this.F4_t=f4;
        }
        
        // BY CLICKING IN THE HEADER THE COLUMN EXPANDS OR CONTRACT
            //the dimensions of the columns are changed when clicking in the header of the column
        private void DataGridColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (expanded==true)
            {
                // if it is expanded we define smaller dimensions of the columns
                for (int i = 1; i < F4_t.Columns.Count+1; i++)
                { grid2.Columns[i].Width = 35; }
                expanded = false; // we set the bool in false
            }
            else
            {   // if it is not expanded, we defube bigger dimensions
                for (int i = 1; i < F4_t.Columns.Count+1; i++)
                { grid2.Columns[i].Width = 100; }
                expanded = true; // bool is true
            }
        }
        
        //TABLES SELECTION COMBOBOZ
            // When it is initiallised, firstly an initial comprovation is done and then added a Event
            // it is not an accedible way
        private void TableSetect_ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            // we initilise the timer 
            dispatcherTimer = new DispatcherTimer();
            // define the new function of the mouse in the polyfon
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100); // interval 
            dispatcherTimer.Start();
  
        }
       
        // SETTING HEADERS OF THE TABLES
            // Adding the name of each header, that would be an int value starting at 1        
        private void Set_headers(DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = Convert.ToString(i + 1); // adding the name
            }
        }

        // SETTING HEADERS OF THE ROW TABLES
            // Adding the name of the row we are in, starting at 1
        private void grid2_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (Convert.ToDouble((e.Row.GetIndex()).ToString()) != F4_t.Rows.Count)
            {
                e.Row.Header = Convert.ToDouble((e.Row.GetIndex()).ToString()) + 1; // adding the header
            }
            else
            {
                e.Row.Header = "";
            }
        }
       
        // COMPUTING FUNCTION
            // Initially this function set the headers and the selected item in the combobox
            // is shown into the grid
          
        public void Compute()
        {

            try
            {
                // set the headers of the tables
                Set_headers(temperature_t);
                Set_headers(u_t);
                Set_headers(v_t);
                Set_headers(rho_t);
                Set_headers(p_t);
                Set_headers(M_t);
                Set_headers(F1_t);
                Set_headers(F2_t);
                Set_headers(F3_t);
                Set_headers(F4_t);

                // if temperature is selecting
                if (TableSetect_ComboBox.SelectedIndex == 0)
                {
                    grid2.DataContext =temperature_t; // we visualise temperature table 
                                        
                }
                // if u is selecting
                else if (TableSetect_ComboBox.SelectedIndex == 1)
                {
                    grid2.DataContext = u_t.DefaultView; // we visualise u table 
               }
                // if v is selecting
                else if (TableSetect_ComboBox.SelectedIndex == 2)
                {
                    grid2.DataContext = v_t.DefaultView; // we visualise v table 
                }
                // if density is selecting
                else if (TableSetect_ComboBox.SelectedIndex == 3)
                {
                    grid2.DataContext = rho_t.DefaultView; // we visualise density table 
                }
                // if pressure is selecting
                else if (TableSetect_ComboBox.SelectedIndex == 4)
                {
                    grid2.DataContext = p_t.DefaultView; // we visualise pressure table 
                }
                // if mach is selecting
                else if (TableSetect_ComboBox.SelectedIndex == 5)
                {
                    grid2.DataContext = M_t.DefaultView; // we visualise mach table 
                }
                // if f1 is selecting
                else if (TableSetect_ComboBox.SelectedIndex == 6)
                {
                    grid2.DataContext = F1_t.DefaultView; // we visualise f1 table 
                }
                // if f2 is selecting
                else if (TableSetect_ComboBox.SelectedIndex == 7)
                {
                    grid2.DataContext = F2_t.DefaultView; // we visualise f2 table 
                }
                // if f3 is selecting
                else if (TableSetect_ComboBox.SelectedIndex == 8)
                {
                    grid2.DataContext = F3_t.DefaultView; // we visualise f3 table 
                }
                // if f4 is selecting
                else if (TableSetect_ComboBox.SelectedIndex == 9)
                {
                    grid2.DataContext = F4_t.DefaultView; // we visualise f4 table 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        #endregion TABLES FUNCIONTS

        #region WINDOW MANIPULATION FUNCTIONS
        // MINIMISE BUTTON
            //When pressing this button the window minimises
        private void Mini_Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        
        //CLOSING BUTTON
            // When this button is pressed, the window closes
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        // DRAG WINDOW MOVE
            // When the mouse is placed in the top place of the window and is draged
            // the window moves
        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }

        }

        #endregion WINDOW MANIPULATION FUNCTIONS


    }
}
