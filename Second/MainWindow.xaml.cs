using System;
using System.Windows;
using System.Windows.Controls;
using SecondTask.Classes;

namespace SecondTask {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        
        private static Travel OurTravel { get; set; }

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
            try {
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

                    OurTravel = new Travel();
                    OurTravel.AddCity(firstCity);
                    OurTravel.AddCity(secondCity);
                    
                    if (textBox2.Text != "")
                        try {
                            thirdCity = new City(textBox2.Text);
                            OurTravel.AddCity(thirdCity);
                            isThirdExist = true;
                        }
                        catch (Exception) {
                            MessageBox.Show("Third city not found!");
                        }

                    if (textBox3.Text != "")
                        try {
                            fourthCity = new City(textBox3.Text);
                            OurTravel.AddCity(fourthCity);
                            isFourthExist = true;
                        }
                        catch (Exception) {
                            MessageBox.Show("Fourth city not found!");
                        }

                    textBox4.Text = OurTravel.Cities[0].Coord.longitude.ToString() + " X; " +
                                    OurTravel.Cities[0].Coord.latitude.ToString() + " Y";

                    textBox5.Text = OurTravel.Cities[1].Coord.longitude.ToString() + " X; " +
                                    OurTravel.Cities[1].Coord.latitude.ToString() + " Y";

                    if (isThirdExist)
                        textBox6.Text = OurTravel.Cities[2].Coord.longitude.ToString() + " X; " +
                                        OurTravel.Cities[2].Coord.latitude.ToString() + " Y";

                    if (isFourthExist)
                        textBox7.Text = OurTravel.Cities[3].Coord.longitude.ToString() + " X; " +
                                        OurTravel.Cities[3].Coord.latitude.ToString() + " Y";

                    OurTravel.ComputeRoute();

                    tBox_Route.IsEnabled = true;
                    tBox_Route.Text = OurTravel.GetRouteStr();

                    tBox_Dist.IsEnabled = true;
                    tBox_Dist.Text = (OurTravel.RouteLength / 1000).ToString();
                    tBox_Time.IsEnabled = true;
                    tBox_Time.Text = OurTravel.AproxTime;

                    button1.IsEnabled = true;
                }
                else {
                    MessageBox.Show("Enter at least 2 cities");
                }
            }
            catch (Exception er) {
                MessageBox.Show("Ooops, error: " + er.Message);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            try {
                Second.Map routeMap = new Second.Map(OurTravel);
                routeMap.ShowDialog();
            }
            catch (Exception er) {
                MessageBox.Show("Ooops, error: " + er.Message);
            }
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);            
            
            Application.Current.Shutdown(0);
        }
    }
}
