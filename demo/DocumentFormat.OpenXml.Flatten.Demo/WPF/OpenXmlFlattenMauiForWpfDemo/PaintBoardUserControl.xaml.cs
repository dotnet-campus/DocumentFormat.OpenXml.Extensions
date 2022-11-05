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
using MauiPptxViewerCore;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Xaml;

namespace OpenXmlFlattenMauiForWpfDemo;

/// <summary>
/// PaintBoardUserControl.xaml 的交互逻辑
/// </summary>
public partial class PaintBoardUserControl : UserControl
{
    public PaintBoardUserControl()
    {
        InitializeComponent();

        var xamlCanvas = new XamlCanvas()
        {
            Canvas = MauiCanvas,
        };
        _xamlCanvas = xamlCanvas;
    }

    public void Open(FileInfo pptxFile)
    {
        _pptxViewer = new PptxViewer(pptxFile, _xamlCanvas);
        _pptxViewer.Open();
    }

    private PptxViewer? _pptxViewer;
    private XamlCanvas _xamlCanvas;
}
