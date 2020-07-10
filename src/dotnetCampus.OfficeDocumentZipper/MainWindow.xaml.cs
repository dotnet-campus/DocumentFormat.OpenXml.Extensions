using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace dotnetCampus.OfficeDocumentZipper
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

        private void OpenOfficeFile_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void UnZip_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Explorer_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(OfficeFolder.Text))
            {
                Warn($"OfficeFolder can not be empty");
                return;
            }

            if (!Directory.Exists(OfficeFolder.Text))
            {
                Warn($"Office Folder {OfficeFolder.Text} not found");
            }

            Process.Start(new ProcessStartInfo("explorer")
            {
                ArgumentList =
                {
                    OfficeFolder.Text
                }
            });
        }

        private void Zip_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Warn(string text)
        {
            if (Dispatcher.CheckAccess())
            {
                Warning.Text = text;
            }
            else
            {
                Dispatcher.InvokeAsync(() =>
                {
                    Warning.Text = text;
                });
            }
        }

        private void OfficeFile_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var file = GetFile();

            OfficeFolder.Text = Path.GetDirectoryName(file) ?? string.Empty;
        }

        private string GetFile()
        {
            var file = OfficeFile.Text;
            if (file.StartsWith("\""))
            {
                file = file.Substring(1);
            }

            if (file.EndsWith("\""))
            {
                file = file.Remove(file.Length - 1);
            }

            file = Path.GetFullPath(file);
            return file;
        }
    }
}
