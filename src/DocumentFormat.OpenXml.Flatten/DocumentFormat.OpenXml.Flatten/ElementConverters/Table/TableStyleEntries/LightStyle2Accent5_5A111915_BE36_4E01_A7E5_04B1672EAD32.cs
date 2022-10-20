using System.CodeDom.Compiler;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries;

[GeneratedCode("OpenXmlSdkTool", "2.5")]
internal static class LightStyle2Accent5_5A111915_BE36_4E01_A7E5_04B1672EAD32
{
    public static TableStyleEntry GenerateTableStyleEntry()
    {
        TableStyleEntry tableStyleEntry1 = new TableStyleEntry() { StyleId = "{5A111915-BE36-4E01-A7E5-04B1672EAD32}", StyleName = "浅色样式 2 - 强调 5" };

        WholeTable wholeTable1 = new WholeTable();

        TableCellTextStyle tableCellTextStyle1 = new TableCellTextStyle();

        FontReference fontReference1 = new FontReference() { Index = FontCollectionIndexValues.Minor };
        RgbColorModelPercentage rgbColorModelPercentage1 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

        fontReference1.Append(rgbColorModelPercentage1);
        SchemeColor schemeColor1 = new SchemeColor() { Val = SchemeColorValues.Text1 };

        tableCellTextStyle1.Append(fontReference1);
        tableCellTextStyle1.Append(schemeColor1);

        TableCellStyle tableCellStyle1 = new TableCellStyle();

        TableCellBorders tableCellBorders1 = new TableCellBorders();

        LeftBorder leftBorder1 = new LeftBorder();

        LineReference lineReference1 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor2 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        lineReference1.Append(schemeColor2);

        leftBorder1.Append(lineReference1);

        RightBorder rightBorder1 = new RightBorder();

        LineReference lineReference2 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor3 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        lineReference2.Append(schemeColor3);

        rightBorder1.Append(lineReference2);

        TopBorder topBorder1 = new TopBorder();

        LineReference lineReference3 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor4 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        lineReference3.Append(schemeColor4);

        topBorder1.Append(lineReference3);

        BottomBorder bottomBorder1 = new BottomBorder();

        LineReference lineReference4 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor5 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        lineReference4.Append(schemeColor5);

        bottomBorder1.Append(lineReference4);

        InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder();

        Outline outline1 = new Outline();
        NoFill noFill1 = new NoFill();

        outline1.Append(noFill1);

        insideHorizontalBorder1.Append(outline1);

        InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder();

        Outline outline2 = new Outline();
        NoFill noFill2 = new NoFill();

        outline2.Append(noFill2);

        insideVerticalBorder1.Append(outline2);

        tableCellBorders1.Append(leftBorder1);
        tableCellBorders1.Append(rightBorder1);
        tableCellBorders1.Append(topBorder1);
        tableCellBorders1.Append(bottomBorder1);
        tableCellBorders1.Append(insideHorizontalBorder1);
        tableCellBorders1.Append(insideVerticalBorder1);

        FillProperties fillProperties1 = new FillProperties();
        NoFill noFill3 = new NoFill();

        fillProperties1.Append(noFill3);

        tableCellStyle1.Append(tableCellBorders1);
        tableCellStyle1.Append(fillProperties1);

        wholeTable1.Append(tableCellTextStyle1);
        wholeTable1.Append(tableCellStyle1);

        Band1Horizontal band1Horizontal1 = new Band1Horizontal();

        TableCellStyle tableCellStyle2 = new TableCellStyle();

        TableCellBorders tableCellBorders2 = new TableCellBorders();

        TopBorder topBorder2 = new TopBorder();

        LineReference lineReference5 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor6 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        lineReference5.Append(schemeColor6);

        topBorder2.Append(lineReference5);

        BottomBorder bottomBorder2 = new BottomBorder();

        LineReference lineReference6 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor7 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        lineReference6.Append(schemeColor7);

        bottomBorder2.Append(lineReference6);

        tableCellBorders2.Append(topBorder2);
        tableCellBorders2.Append(bottomBorder2);

        tableCellStyle2.Append(tableCellBorders2);

        band1Horizontal1.Append(tableCellStyle2);

        Band1Vertical band1Vertical1 = new Band1Vertical();

        TableCellStyle tableCellStyle3 = new TableCellStyle();

        TableCellBorders tableCellBorders3 = new TableCellBorders();

        LeftBorder leftBorder2 = new LeftBorder();

        LineReference lineReference7 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor8 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        lineReference7.Append(schemeColor8);

        leftBorder2.Append(lineReference7);

        RightBorder rightBorder2 = new RightBorder();

        LineReference lineReference8 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor9 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        lineReference8.Append(schemeColor9);

        rightBorder2.Append(lineReference8);

        tableCellBorders3.Append(leftBorder2);
        tableCellBorders3.Append(rightBorder2);

        tableCellStyle3.Append(tableCellBorders3);

        band1Vertical1.Append(tableCellStyle3);

        Band2Vertical band2Vertical1 = new Band2Vertical();

        TableCellStyle tableCellStyle4 = new TableCellStyle();

        TableCellBorders tableCellBorders4 = new TableCellBorders();

        LeftBorder leftBorder3 = new LeftBorder();

        LineReference lineReference9 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor10 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        lineReference9.Append(schemeColor10);

        leftBorder3.Append(lineReference9);

        RightBorder rightBorder3 = new RightBorder();

        LineReference lineReference10 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor11 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        lineReference10.Append(schemeColor11);

        rightBorder3.Append(lineReference10);

        tableCellBorders4.Append(leftBorder3);
        tableCellBorders4.Append(rightBorder3);

        tableCellStyle4.Append(tableCellBorders4);

        band2Vertical1.Append(tableCellStyle4);

        LastColumn lastColumn1 = new LastColumn();
        TableCellTextStyle tableCellTextStyle2 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle5 = new TableCellStyle();
        TableCellBorders tableCellBorders5 = new TableCellBorders();

        tableCellStyle5.Append(tableCellBorders5);

        lastColumn1.Append(tableCellTextStyle2);
        lastColumn1.Append(tableCellStyle5);

        FirstColumn firstColumn1 = new FirstColumn();
        TableCellTextStyle tableCellTextStyle3 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle6 = new TableCellStyle();
        TableCellBorders tableCellBorders6 = new TableCellBorders();

        tableCellStyle6.Append(tableCellBorders6);

        firstColumn1.Append(tableCellTextStyle3);
        firstColumn1.Append(tableCellStyle6);

        LastRow lastRow1 = new LastRow();
        TableCellTextStyle tableCellTextStyle4 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle7 = new TableCellStyle();

        TableCellBorders tableCellBorders7 = new TableCellBorders();

        TopBorder topBorder3 = new TopBorder();

        Outline outline3 = new Outline() { Width = 50800, CompoundLineType = CompoundLineValues.Double };

        SolidFill solidFill1 = new SolidFill();
        SchemeColor schemeColor12 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        solidFill1.Append(schemeColor12);

        outline3.Append(solidFill1);

        topBorder3.Append(outline3);

        tableCellBorders7.Append(topBorder3);

        tableCellStyle7.Append(tableCellBorders7);

        lastRow1.Append(tableCellTextStyle4);
        lastRow1.Append(tableCellStyle7);

        FirstRow firstRow1 = new FirstRow();

        TableCellTextStyle tableCellTextStyle5 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        FontReference fontReference2 = new FontReference() { Index = FontCollectionIndexValues.Minor };
        RgbColorModelPercentage rgbColorModelPercentage2 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

        fontReference2.Append(rgbColorModelPercentage2);
        SchemeColor schemeColor13 = new SchemeColor() { Val = SchemeColorValues.Background1 };

        tableCellTextStyle5.Append(fontReference2);
        tableCellTextStyle5.Append(schemeColor13);

        TableCellStyle tableCellStyle8 = new TableCellStyle();
        TableCellBorders tableCellBorders8 = new TableCellBorders();

        FillReference fillReference1 = new FillReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor14 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        fillReference1.Append(schemeColor14);

        tableCellStyle8.Append(tableCellBorders8);
        tableCellStyle8.Append(fillReference1);

        firstRow1.Append(tableCellTextStyle5);
        firstRow1.Append(tableCellStyle8);

        tableStyleEntry1.Append(wholeTable1);
        tableStyleEntry1.Append(band1Horizontal1);
        tableStyleEntry1.Append(band1Vertical1);
        tableStyleEntry1.Append(band2Vertical1);
        tableStyleEntry1.Append(lastColumn1);
        tableStyleEntry1.Append(firstColumn1);
        tableStyleEntry1.Append(lastRow1);
        tableStyleEntry1.Append(firstRow1);
        return tableStyleEntry1;
    }

}
