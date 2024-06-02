using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace egzaminas
{
    public partial class MainWindow : Window
    {
        private PasswordManager passwordManager;
        private Stopwatch stopwatch;

        public MainWindow()
        {
            InitializeComponent();
            passwordManager = new PasswordManager();
            ThreadSlider.ValueChanged += ThreadSlider_ValueChanged;
            ThreadCountTextBlock.Text = ThreadSlider.Value.ToString();
            stopwatch = new Stopwatch();
        }

        private void ThreadSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ThreadCountTextBlock.Text = ((int)ThreadSlider.Value).ToString();
        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordTextBox.Text;
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Prašome įvesti slaptažodį");
                return;
            }

            passwordManager.EncodePassword(password);
            MessageBox.Show("Slaptažodis užkoduotas ir išsaugotas į password.txt");
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int threadCount = (int)ThreadSlider.Value;
            stopwatch.Reset();
            stopwatch.Start();
            string decodedPassword = await Task.Run(() => passwordManager.BruteForceDecodePassword(threadCount));
            stopwatch.Stop();

            DecodedPasswordTextBox.Text = decodedPassword;
            TimeTextBox.Text = stopwatch.ElapsedMilliseconds.ToString() + " ms";
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            DecodedPasswordTextBox.Text = "";
            TimeTextBox.Text = "";
        }
    }
}
