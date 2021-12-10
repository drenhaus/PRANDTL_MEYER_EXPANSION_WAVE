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
using System.Windows.Threading;

namespace WPFapp
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {

        #region ATRIBUTES
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int timer = 0;
        #endregion ATRIBUTES

        public Loading()
        {
            InitializeComponent();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            #region IFs FOR EACH TIEMER TICK
            if (timer == 0)
            {
                ellipse1.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                ellipse2.Fill= new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
                ellipse3.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
                ellipse4.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
            }
            else if (timer == 1)
            {
                ellipse2.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                ellipse1.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
                ellipse3.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
                ellipse4.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));

            }
            else if (timer == 2)
            {
                ellipse3.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                ellipse2.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
                ellipse1.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
                ellipse4.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
            }
            
            else if (timer==3)
            {
                ellipse4.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                ellipse2.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
                ellipse3.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
                ellipse1.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));

            }
            timer = timer + 1;
            if (timer==4)
            { timer = 0; }
            #endregion IFs FOR EACH TIEMER TICK
        }



    }
}
