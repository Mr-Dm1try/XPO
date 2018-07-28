using System;
using System.Windows;
using System.Windows.Controls;
using SecondTask.Model;
using SecondTask.ViewModel;

namespace SecondTask.View {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
        
        private void txt1City_TextChanged(object sender, TextChangedEventArgs e) {
            String str = (sender as TextBox).GetLineText(0);
            if (str != "") 
                txt1Coord.IsEnabled = true;            
            else {
                txt1Coord.IsEnabled = false;
                txt1Coord.Text = "Coordinates";
            }
        }

        private void txt2City_TextChanged(object sender, TextChangedEventArgs e) {
            String str = (sender as TextBox).GetLineText(0);
            if (str != "") 
                txt2Coord.IsEnabled = true;            
            else {
                txt2Coord.IsEnabled = false;
                txt2Coord.Text = "Coordinates";
            }
        }

        private void txt3City_TextChanged(object sender, TextChangedEventArgs e) {
            String str = (sender as TextBox).GetLineText(0);
            if (str != "") 
                txt3Coord.IsEnabled = true;           
            else {
                txt3Coord.IsEnabled = false;
                txt3Coord.Text = "Coordinates";
            }
        }

        private void txt4City_TextChanged(object sender, TextChangedEventArgs e) {
            String str = (sender as TextBox).GetLineText(0);
            if (str != "")
                txt4Coord.IsEnabled = true;
            else {
                txt4Coord.IsEnabled = false;
                txt4Coord.Text = "Coordinates";
            }
        }        

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);            
            
            Application.Current.Shutdown(0);
        }
    }
}
