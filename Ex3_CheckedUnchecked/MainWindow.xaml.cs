// Задание 3. Использование Checked UnChecked для обработки переполнения целых чисел 

using System;
using System.Windows;
namespace Ex3_CheckedUnchecked
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

        private void mulUncheckedButton_Click(object sender, RoutedEventArgs e)
        {
            int num1;
            int num2;

            if (!int.TryParse(num1UncheckedTextBox.Text, out num1) & !int.TryParse(num2UncheckedTextBox.Text, out num2))
            {
                MessageBox.Show("Input Error!");
            }

            resultUnheckedLabel.Content = num1 * num2;
        }

        private void mulCheckedButton_Click(object sender, RoutedEventArgs e)
        {
            int num1;
            int num2;

            if (!int.TryParse(num1CheckedTextBox.Text, out num1) & !int.TryParse(num2CheckedTextBox.Text, out num2))
            {
                MessageBox.Show("Input Error!");
            }

            try
            {
                resultCheckedLabel.Content = checked(num1 * num2);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Overflow Exception!");
            }
        }
    }
}
