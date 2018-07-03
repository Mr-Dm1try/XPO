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
using SecondTask.Classes;

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
                City firstCity, secondCity, thirdCity, fourthCity;
                Boolean isThirdExist = false, isFourthExist = false;
                try {
                    firstCity = new City(textBox.Text);
                }
                catch (Exception) {
                    MessageBox.Show("First city not found!");
                    return;
                }

                try {
                    secondCity = new City(textBox1.Text);
                }
                catch (Exception) {
                    MessageBox.Show("Second city not found!");
                    return;
                }

                Classes.Travel travel = new Classes.Travel();
                travel.AddCity(firstCity);
                travel.AddCity(secondCity);

                //travel.AddCity(textBox.Text, 0, 0);
                //travel.AddCity(textBox1.Text, 1, 1);

                if (textBox2.Text != "") 
                    try {
                        thirdCity = new City(textBox2.Text);
                        travel.AddCity(thirdCity);
                        isThirdExist = true;
                    }
                    catch (Exception) {
                        MessageBox.Show("Third city not found!");
                    }
                    

                if (textBox3.Text != "")
                    try {
                        fourthCity = new City(textBox3.Text);
                        travel.AddCity(fourthCity);
                        isFourthExist = true;
                    }
                    catch (Exception) {
                        MessageBox.Show("Fourth city not found!");
                    }
                
                textBox4.Text = travel.Cities[0].Coord.longitude.ToString() + " X, " +
                                travel.Cities[0].Coord.latitude.ToString() + " Y";

                textBox5.Text = travel.Cities[1].Coord.longitude.ToString() + " X, " +
                                travel.Cities[1].Coord.latitude.ToString() + " Y";

                if (isThirdExist) 
                    textBox6.Text = travel.Cities[2].Coord.longitude.ToString() + " X, " + 
                                    travel.Cities[2].Coord.latitude.ToString() + " Y";  

                if (isFourthExist)
                    textBox7.Text = travel.Cities[3].Coord.longitude.ToString() + " X, " +
                                    travel.Cities[3].Coord.latitude.ToString() + " Y";

                travel.ComputeRoute();

                tBox_Route.IsEnabled = true;
                tBox_Route.Text = travel.GetRoute();

                tBox_Dist.IsEnabled = true;
                tBox_Dist.Text = (travel.RouteLength / 1000).ToString();
                tBox_Time.IsEnabled = true;
                tBox_Time.Text = travel.AproxTime;
            }
            else {
                MessageBox.Show("Enter at least 2 cities");
            }
        }
    }
}
