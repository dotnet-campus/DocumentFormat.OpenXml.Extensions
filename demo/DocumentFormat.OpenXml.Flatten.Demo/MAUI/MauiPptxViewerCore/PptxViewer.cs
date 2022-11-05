using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Graphics;

namespace MauiPptxViewerCore;

public class PptxViewer
{
    public PptxViewer(FileInfo pptxFile, ICanvas canvas)
    {
        PptxFile = pptxFile;
        Canvas = canvas;
    }

    public FileInfo PptxFile { get; }
    public ICanvas Canvas { get;  }

    public void Open()
    {

    }
}

//public record PptxViewerBuilder(FileInfo PptxFile, ICanvas Canvas)
//{
//}
