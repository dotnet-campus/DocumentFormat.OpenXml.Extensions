using System;
using System.Collections.Generic;
using System.IO;
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

namespace OpenXmlFlattenMauiForWpfDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var folder = System.IO.Path.GetDirectoryName(GetType().Assembly.Location)!;
        var testFile = System.IO.Path.Combine(folder, @"TestFiles\Shape Triangle.pptx");

        Open(testFile);
    }

    private void OpenPptxFileButton_OnClick(object sender, RoutedEventArgs e)
    {
        var pptxFilePath = PptxFilePathTextBox.Text;

        Open(pptxFilePath);
    }

    private void Open(string pptxFilePath)
    {
        if (!string.IsNullOrEmpty(pptxFilePath) && File.Exists(pptxFilePath))
        {
            PaintBoardUserControl.Open(new FileInfo(pptxFilePath));

            PptxFilePathTextBox.Text = pptxFilePath;
        }
    }
}

