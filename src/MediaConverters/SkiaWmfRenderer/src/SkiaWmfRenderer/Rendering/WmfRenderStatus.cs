using HarfBuzzSharp;

using Oxage.Wmf;

using SkiaSharp;

using System.Runtime.InteropServices;
using System.Text;

namespace SkiaWmfRenderer.Rendering;

class WmfRenderStatus : IDisposable
{
    public required float CurrentX { get; set; }
    public required float CurrentY { get; set; }

    public required int Width { get; set; }
    public required int Height { get; set; }

    public int CurrentRecordsIndex { get; set; }

    public SKColor CurrentPenColor { get; set; } = SKColors.Empty;
    public float CurrentPenThickness { get; set; } = 0;

    public SKColor CurrentFillColor { get; set; } = SKColor.Empty;

    public float CurrentFontSize { get; set; }

    public string? CurrentFontName { get; set; } = null;

    public bool IsItalic { get; set; } = false;
    public int FontWeight { get; set; } = 400;

    public CharacterSet CurrentCharacterSet
    {
        get => _currentCharacterSet;
        set
        {
            _currentCharacterSet = value;
            CurrentEncoding = _currentCharacterSet.CharacterSetToEncoding();
        }
    }

    private CharacterSet _currentCharacterSet = CharacterSet.DEFAULT_CHARSET;

    public Encoding CurrentEncoding { get; private set; } = CharacterSet.DEFAULT_CHARSET.CharacterSetToEncoding();

    public float LastDxOffset { get; set; } = 0;

    public SKColor CurrentTextColor { get; set; } =
        SKColors.Black;

    public SKPaint Paint { get; } = new SKPaint()
    {
        IsAntialias = true
    };

    public SKFont SKFont { get; } = new SKFont();

    public HarfBuzzSharp.Font HarfBuzzFont
    {
        get
        {
            if (_harfBuzzFont is null)
            {
                _harfBuzzFace = new HarfBuzzSharp.Face(GetTable);

                Blob? GetTable(Face f, Tag tag)
                {
                    var skTypeface = SKFont.Typeface ?? SKTypeface.Default;

                    var size = skTypeface.GetTableSize(tag);
                    var data = Marshal.AllocCoTaskMem(size);
                    if (skTypeface.TryGetTableData(tag, 0, size, data))
                    {
                        return new Blob(data, size, MemoryMode.ReadOnly, () => Marshal.FreeCoTaskMem(data));
                    }
                    else
                    {
                        return null;
                    }
                }

                var font = new HarfBuzzSharp.Font(_harfBuzzFace);
                font.SetFunctionsOpenType();
                _harfBuzzFont = font;
            }
            return _harfBuzzFont;
        }
    }

    private HarfBuzzSharp.Font? _harfBuzzFont;
    private HarfBuzzSharp.Face? _harfBuzzFace;

    public void UpdateSkiaTextStatus(string text)
    {
        var skFont = SKFont;
        skFont.Size = CurrentFontSize;

        skFont.Typeface?.Dispose();
        _harfBuzzFont?.Dispose();
        _harfBuzzFace?.Dispose();
        _harfBuzzFont = null;
        _harfBuzzFace = null;

        SKTypeface? typeface;
        if (CurrentFontName == "Symbol")
        {
            var symbolFontFile = Path.Join(AppContext.BaseDirectory, "StandardSymbolsPS.ttf");
            //symbolFontFile = Path.Join(AppContext.BaseDirectory, "symbol.ttf");
            typeface = SKTypeface.FromFile(symbolFontFile);
        }
        else
        {
            typeface = SKTypeface.FromFamilyName(CurrentFontName, (SKFontStyleWeight) FontWeight,
           SKFontStyleWidth.Normal, IsItalic ? SKFontStyleSlant.Italic : SKFontStyleSlant.Upright);
        }

        Console.WriteLine($"CurrentFontName='{CurrentFontName}' get the SKTypeface {(typeface is null ? "is null" : "not null")}. SKTypeface={typeface?.FamilyName} GlyphCount={typeface?.GlyphCount}. Text={text}");

        skFont.Typeface = typeface;

        Paint.Style = SKPaintStyle.Fill;
        Paint.Color = CurrentTextColor;

        //Paint.Typeface = typeface;
    }

    public void UpdateSkiaStrokeStatus()
    {
        Paint.IsStroke = true;
        Paint.Color = CurrentPenColor;
        Paint.StrokeWidth = CurrentPenThickness;
        Paint.Style = SKPaintStyle.Stroke;
    }

    public void UpdateSkiaFillStatus()
    {
        Paint.Style = SKPaintStyle.Fill;
        Paint.Color = CurrentFillColor;
    }

    public bool IsIncludeText { get; set; } = false;
    public bool IsIncludeOtherEncoding { get; set; } = false;
    public bool IsIncludeTextWithDx { get; set; } = false;

    public void Dispose()
    {
        Paint.Dispose();
        SKFont.Typeface?.Dispose();
        SKFont.Dispose();

        _harfBuzzFont?.Dispose();
        _harfBuzzFace?.Dispose();
    }
}