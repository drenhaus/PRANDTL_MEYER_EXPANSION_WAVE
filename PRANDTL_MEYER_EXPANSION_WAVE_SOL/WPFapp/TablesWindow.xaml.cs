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



        public void T_selected()
        {
            grid2.Items.Clear();
            grid2.DataContext = temperature_t.DefaultView;
        
        }
        public void u_selected()
        {
            grid2.Items.Clear();
            grid2.DataContext = u_t.DefaultView;

        }
        public void v_selected()
        {
            grid2.Items.Clear();
            grid2.DataContext = v_t.DefaultView;

        }
        public void rho_selected()
        {
            grid2.Items.Clear();
            grid2.DataContext = rho_t.DefaultView;

        }
        public void p_selected()
        {
            grid2.Items.Clear();
            grid2.DataContext = p_t.DefaultView;

        }
        public void m_selected()
        {
            grid2.Items.Clear();
            grid2.DataContext = M_t.DefaultView;

        }
        public void F1_selected()
        {
            grid2.Items.Clear();
            grid2.DataContext = F1_t.DefaultView;

        }
        public void F2_selected()
        {
            grid2.Items.Clear();
            grid2.DataContext = F2_t.DefaultView;

        }
        public void F3_selected()
        {
            grid2.Items.Clear();
            grid2.DataContext = F3_t.DefaultView;

        }
        public void F4_selected()
        {
            grid2.Items.Clear();
            grid2.DataContext = F4_t.DefaultView;

        }

        private void TableSetect_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableSetect_ComboBox.SelectedIndex==0)
            {
                T_selected();
            }
            if (TableSetect_ComboBox.SelectedIndex == 1)
            {
                u_selected();
            }
            if (TableSetect_ComboBox.SelectedIndex == 2)
            {
                v_selected();
            }
            if (TableSetect_ComboBox.SelectedIndex == 3)
            {
                rho_selected();
            }
            if (TableSetect_ComboBox.SelectedIndex == 4)
            {
                p_selected();
            }
            if (TableSetect_ComboBox.SelectedIndex == 5)
            {
                m_selected();
            }
            if (TableSetect_ComboBox.SelectedIndex == 6)
            {
                F1_selected();
            }
            if (TableSetect_ComboBox.SelectedIndex == 7)
            {
                F2_selected();
            }
            if (TableSetect_ComboBox.SelectedIndex == 8)
            {
                F3_selected();
            }
            if (TableSetect_ComboBox.SelectedIndex == 9)
            {
                F4_selected();
            }
        }
    }
}
