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

        DataTable table_to_Export;
        bool expanded = true;

        DispatcherTimer dispatcherTimer;
        int timer=0;
        DispatcherTimer dispatcherTimer2;
        int timer2 = 0;
        Loading ld;
        #endregion ATRIBUTES

        public TablesWindow()
        {
            InitializeComponent();
        }

        #region TIMERS
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (timer==1)
            {
                ld = new Loading();
                ld.Show();
            }
            if (timer==2)
                {
                Compute();
                }
            if (timer == 3)
            {
                ld.Close();
                timer = -1;
                dispatcherTimer.Stop();
            }
                timer = timer + 1;
        
      
        }
        #endregion TIMERS

        #region TABLES FUNCIONTS
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
        private void DataGridColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (expanded==true)
            {
                for (int i = 1; i < F4_t.Columns.Count+1; i++)
                { grid2.Columns[i].Width = 35; }
                expanded = false;
            }
            else
            {
                for (int i = 1; i < F4_t.Columns.Count+1; i++)
                { grid2.Columns[i].Width = 100; }
                expanded = true;
            }
        }
        private void TableSetect_ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer.Start();
  
        }
        private void Set_headers(DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = Convert.ToString(i + 1);
            }
        }
      
        public void Compute()
        {

            try
            {
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

                if (TableSetect_ComboBox.SelectedIndex == 0)
                {

                    grid2.DataContext =temperature_t;
                    table_to_Export = temperature_t;
                    
                }
                else if (TableSetect_ComboBox.SelectedIndex == 1)
                {
                    grid2.DataContext = u_t.DefaultView;
                    table_to_Export = u_t;
                }
                else if (TableSetect_ComboBox.SelectedIndex == 2)
                {
                    grid2.DataContext = v_t.DefaultView;
                    table_to_Export = v_t;
                }
                else if (TableSetect_ComboBox.SelectedIndex == 3)
                {
                    grid2.DataContext = rho_t.DefaultView;
                    table_to_Export = rho_t;
                }
                else if (TableSetect_ComboBox.SelectedIndex == 4)
                {
                    grid2.DataContext = p_t.DefaultView;
                    table_to_Export = p_t;
                }
                else if (TableSetect_ComboBox.SelectedIndex == 5)
                {
                    grid2.DataContext = M_t.DefaultView;
                    table_to_Export = M_t;
                }
                else if (TableSetect_ComboBox.SelectedIndex == 6)
                {
                    grid2.DataContext = F1_t.DefaultView;
                    table_to_Export = F1_t;
                }
                else if (TableSetect_ComboBox.SelectedIndex == 7)
                {
                    grid2.DataContext = F2_t.DefaultView;
                    table_to_Export = F2_t;
                }
                else if (TableSetect_ComboBox.SelectedIndex == 8)
                {
                    grid2.DataContext = F3_t.DefaultView;
                    table_to_Export = F3_t;
                }
                else if (TableSetect_ComboBox.SelectedIndex == 9)
                {
                    grid2.DataContext = F4_t.DefaultView;
                    table_to_Export = F4_t;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        #endregion TABLES FUNCIONTS

        #region WINDOW MANIPULATION FUNCTIONS
        private void Mini_Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }

        }



        #endregion WINDOW MANIPULATION FUNCTIONS

        private void grid2_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (Convert.ToDouble((e.Row.GetIndex()).ToString()) != F4_t.Rows.Count)
            {
                e.Row.Header = Convert.ToDouble((e.Row.GetIndex()).ToString()) + 1;
            }
            else
            {
                e.Row.Header = "";
            }
        }
    }
}
