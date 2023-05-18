using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TimePeriodNamespace;
using static System.Net.Mime.MediaTypeNames;

namespace TimePeriodNamespace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            byte hours, minutes, seconds;

            if (byte.TryParse(t1h.Text, out hours) &&
                 byte.TryParse(t1m.Text, out minutes) &&
                 byte.TryParse(t1s.Text, out seconds))
            {
                Time time = new Time(hours, minutes, seconds);

                t1Result.Text = time.ToString();
                logsTextBox.Text += "Successfully added Time" + Environment.NewLine;
                if (Right.Visibility == Visibility.Visible || Both.Visibility == Visibility.Visible)
                {
                    No.Visibility = Visibility.Hidden;
                    Right.Visibility = Visibility.Hidden;
                    Left.Visibility = Visibility.Hidden;
                    Both.Visibility = Visibility.Visible;
                }
                else
                {
                    Left.Visibility = Visibility.Visible;
                    No.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                logsTextBox.Text += "Wrong data inputed." + Environment.NewLine;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SecondButton_Click(object sender, RoutedEventArgs e)
        {
            byte hours, minutes, seconds;

            if (byte.TryParse(t2h.Text, out hours) &&
                 byte.TryParse(t2m.Text, out minutes) &&
                 byte.TryParse(t2s.Text, out seconds))
            {
                Time time = new Time(hours, minutes, seconds);

                t2Result.Text = time.ToString();
                logsTextBox.Text += "Successfully added Time" + Environment.NewLine;
                

                if(Left.Visibility == Visibility.Visible || Both.Visibility == Visibility.Visible)
                {
                    No.Visibility = Visibility.Hidden;
                    Right.Visibility = Visibility.Hidden;
                    Left.Visibility = Visibility.Hidden;
                    Both.Visibility = Visibility.Visible;
                }
                else
                {
                    Right.Visibility = Visibility.Visible;
                    No.Visibility= Visibility.Hidden;
                }
            }
            else
            {
                logsTextBox.Text += "Wrong data inputed." + Environment.NewLine;
            }
        }

        private void timeperiodButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(t1Result.Text) || string.IsNullOrEmpty(t2Result.Text))
                    throw new ArgumentNullException("Field t1:Time or t2:Time is null or empty.");
                else
                {
                    Time ss = new(t1Result.Text);
                    Time dd = new(t2Result.Text);
                    TimePeriod sd = new(ss, dd);
                    timeperiod.Text = sd.ToString();
                    logsTextBox.Text += "Successfully added TimePeriod." + Environment.NewLine;  
                }
            }
            catch (Exception ex)
            {
                logsTextBox.Text += ex.Message + Environment.NewLine;
            }
            
            
        }

        private void Clear_button_Click(object sender, RoutedEventArgs e)
        {
            No.Visibility = Visibility.Visible;
            Right.Visibility = Visibility.Hidden;
            Left.Visibility = Visibility.Hidden;
            Both.Visibility = Visibility.Hidden;
            t1Result.Text = string.Empty;
            t2Result.Text = string.Empty;
            timeperiod.Text = string.Empty;
            logsTextBox.Text = string.Empty;
            t1h.Text = string.Empty;
            t2h.Text = string.Empty;
            t1m.Text = string.Empty;
            t2m.Text = string.Empty;
            t1s.Text = string.Empty;
            t2s.Text = string.Empty;
        }

        private void ClearConsole_button_Click(object sender, RoutedEventArgs e)
        {
            logsTextBox.Text = string.Empty;
        }

        private void Greater_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(t1Result.Text) || string.IsNullOrEmpty(t2Result.Text))
                    throw new ArgumentNullException("Field t1:Time or t2:Time is null or empty.");
                else
                {
                    Time ss = new(t1Result.Text);
                    Time dd = new(t2Result.Text);
                    
                    logsTextBox.Text += (ss > dd) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                logsTextBox.Text += ex.Message + Environment.NewLine;
            }
        }

        private void Less_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(t1Result.Text) || string.IsNullOrEmpty(t2Result.Text))
                    throw new ArgumentNullException("Field t1:Time or t2:Time is null or empty.");
                else
                {
                    Time ss = new(t1Result.Text);
                    Time dd = new(t2Result.Text);

                    logsTextBox.Text += (ss < dd) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                logsTextBox.Text += ex.Message + Environment.NewLine;
            }
        }

        private void Equal_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(t1Result.Text) || string.IsNullOrEmpty(t2Result.Text))
                    throw new ArgumentNullException("Field t1:Time or t2:Time is null or empty.");
                else
                {
                    Time ss = new(t1Result.Text);
                    Time dd = new(t2Result.Text);

                    logsTextBox.Text += (ss == dd) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                logsTextBox.Text += ex.Message + Environment.NewLine;
            }
        }
    }
}
