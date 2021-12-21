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
using System.Diagnostics;

namespace WPFapp
{
    /// <summary>
    /// Lógica de interacción para WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }
       
        // EXIT BUTTON
            // When exit button is pressed the windows closes and the program stops
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close(); // closing the window
        }

        // START BUTTON
            // When the start button is pressed, the main simulation window opens
            // allowing the user start making simulations and the welcome window closes
        private void StarButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow(); // new window of MainWindow
            MW.Show(); // showing the window
            Close(); // closing the welcome window
        }

        // DRAGING FUNCTION
            // When the mouse is pressed and moven on the top of the window, it can be moved
            // to the place wanted
        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove(); // drag the window
            }
        }

        // TUTORIAL BUTTON
            //When the tutorial window is pressed, a new window opens and the manual of use and tutorial 
            // can be obtained
        private void TutorialButton_Click(object sender, RoutedEventArgs e)
        {
            // link
            var uri = "https://drive.google.com/drive/folders/12VDQVxcK5ydkCNb3lHm-_q2y88NVEP2n?usp=sharing";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }
    }
}
