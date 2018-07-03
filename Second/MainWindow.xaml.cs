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

namespace SecondTask {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            SetRandom = new Random(3);
        }

        private static Random SetRandom { get; set; }
        public static Int32 GetRandom { get => SetRandom.Next(0, 101); }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e) {
            String str = (sender as TextBox).GetLineText(0);
            if (str != "") {
                textBox1.IsEnabled = true;
                textBox4.IsEnabled = true;
            }
            else {
                textBox1.Clear();
                textBox1.IsEnabled = false;
                textBox4.IsEnabled = false;
                textBox4.Text = "Coordinates";
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e) {
            String str = (sender as TextBox).GetLineText(0);
            if (str != "") {
                textBox2.IsEnabled = true;
                textBox5.IsEnabled = true;
            }
            else {
                textBox2.Clear();
                textBox2.IsEnabled = false;
                textBox5.IsEnabled = false;
                textBox5.Text = "Coordinates";
            }
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e) {
            String str = (sender as TextBox).GetLineText(0);
            if (str != "") {
                textBox3.IsEnabled = true;
                textBox6.IsEnabled = true;
            }
            else {
                textBox3.Clear();
                textBox3.IsEnabled = false;
                textBox6.IsEnabled = false;
                textBox6.Text = "Coordinates";
            }
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e) {
            String str = (sender as TextBox).GetLineText(0);
            if (str != "")
                textBox7.IsEnabled = true;
            else {
                textBox7.IsEnabled = false;
                textBox7.Text = "Coordinates";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            if (textBox.Text != "" && textBox1.Text != "") {
                Classes.Travel travel = new Classes.Travel();
                travel.AddCity(textBox.Text, 0, 0);
                travel.AddCity(textBox1.Text, 1, 1);

                if (textBox2.Text != "") {
                    travel.AddCity(textBox2.Text, 2, 2);

                    if (textBox3.Text != "")
                        travel.AddCity(textBox3.Text, 3, 3);
                }

                travel.ComputeRoute();
                textBox4.Text = travel.Cities[0].Coord.longitude.ToString() + " X, " +
                                travel.Cities[0].Coord.latitude.ToString() + " Y";

                textBox5.Text = travel.Cities[1].Coord.longitude.ToString() + " X, " +
                                travel.Cities[1].Coord.latitude.ToString() + " Y";

                if (textBox2.Text != "") {
                    textBox6.Text = travel.Cities[2].Coord.longitude.ToString() + " X, " + 
                                    travel.Cities[2].Coord.latitude.ToString() + " Y";  

                    if (textBox3.Text != "")
                        textBox7.Text = travel.Cities[3].Coord.longitude.ToString() + " X, " +
                                        travel.Cities[3].Coord.latitude.ToString() + " Y";
                }

                tBox_Route.IsEnabled = true;
                tBox_Route.Text = travel.GetRoute();

                tBox_Dist.IsEnabled = true;
                tBox_Dist.Text = travel.RouteLength.ToString();
                tBox_Time.Text = travel.AproxTime;
            }
            else {
                MessageBox.Show("Enter at least 2 cities");
            }
        }
    }
}
