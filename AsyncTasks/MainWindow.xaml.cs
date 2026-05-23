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

namespace AsyncTasks
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Generators generator = new Generators();

        public async void BtnGenerate(object sender, RoutedEventArgs e)
        {
            if (linesCount.Text.Length == 0 || !int.TryParse(linesCount.Text, out int r))
            {
                MessageBox.Show("Некоректное значение");
            }
            else
            {
                var text = await Generators.GenerateText(Convert.ToInt32(linesCount.Text));
                progBuffer.AppendText(text.ToString());
            }
        }

        public void ClearGeneratedText(object sender, RoutedEventArgs e)
        {
            progBuffer.Document.Blocks.Clear();
        }

        public async void LoadFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                string text = await Generators.ExportFromFile(dialog.FileName);
                fileBuff.AppendText(text);
            }
        }

        public async void SaveToFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                string text = new TextRange(fileBuff.Document.ContentStart, fileBuff.Document.ContentEnd).Text;
                await generator.ExportToFile(dialog.FileName, text);
            }
        }

        public void ClearFileBuffer(object sender, RoutedEventArgs e)
        {
            fileBuff.Document.Blocks.Clear();
        }

        public void ProgToFile(object sender, RoutedEventArgs e)
        {
            string text = new TextRange(progBuffer.Document.ContentStart, progBuffer.Document.ContentEnd).Text;
            fileBuff.AppendText(text);
        }

        public void FileToProg(object sender, RoutedEventArgs e)
        {
            string text = new TextRange(fileBuff.Document.ContentStart, fileBuff.Document.ContentEnd).Text;
            progBuffer.AppendText(text);
        }

        
    }
}
