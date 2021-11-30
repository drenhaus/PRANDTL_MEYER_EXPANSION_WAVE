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

namespace WPFapp
{
    /// <summary>
    /// Lógica de interacción para TablesWindow.xaml
    /// </summary>
    public partial class TablesWindow : Window
    {
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

        bool expanded = true;

        public TablesWindow()
        {
            InitializeComponent();
        }

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

            if (TableSetect_ComboBox.SelectedIndex == 0)
            {
                grid2.DataContext = temperature_t.DefaultView;
            }
            if (TableSetect_ComboBox.SelectedIndex == 1)
            {
                grid2.DataContext = u_t.DefaultView;
            }
            if (TableSetect_ComboBox.SelectedIndex == 2)
            {
                grid2.DataContext = v_t.DefaultView;
            }
            if (TableSetect_ComboBox.SelectedIndex == 3)
            {
                grid2.DataContext = rho_t.DefaultView;
            }
            if (TableSetect_ComboBox.SelectedIndex == 4)
            {
                grid2.DataContext = p_t.DefaultView;
            }
            if (TableSetect_ComboBox.SelectedIndex == 5)
            {
                grid2.DataContext = M_t.DefaultView;
            }
            if (TableSetect_ComboBox.SelectedIndex == 6)
            {
                grid2.DataContext = F1_t.DefaultView;
            }
            if (TableSetect_ComboBox.SelectedIndex == 7)
            {
                grid2.DataContext = F2_t.DefaultView;
            }
            if (TableSetect_ComboBox.SelectedIndex == 8)
            {
                grid2.DataContext = F3_t.DefaultView;
            }
            if (TableSetect_ComboBox.SelectedIndex == 9)
            {
                grid2.DataContext = F4_t.DefaultView;
            }
        }
    }
}
