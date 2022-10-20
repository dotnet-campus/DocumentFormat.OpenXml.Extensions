using System.CodeDom.Compiler;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries;

[GeneratedCode("OpenXmlSdkTool", "2.5")]
internal static class ThemedStyle2Accent5_327F97BB_C833_4FB7_BDE5_3F7075034690
{
    public static TableStyleEntry GenerateTableStyleEntry()
    {
        TableStyleEntry tableStyleEntry1 = new TableStyleEntry() { StyleId = "{327F97BB-C833-4FB7-BDE5-3F7075034690}", StyleName = "主题样式 2 - 强调 5" };

        TableBackground tableBackground1 = new TableBackground();

        FillReference fillReference1 = new FillReference() { Index = (UInt32Value) 3U };
        SchemeColor schemeColor1 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        fillReference1.Append(schemeColor1);

        EffectReference effectReference1 = new EffectReference() { Index = (UInt32Value) 3U };
        SchemeColor schemeColor2 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

        effectReference1.Append(schemeColor2);

        tableBackground1.Append(fillReference1);
        tableBackground1.Append(effectReference1);

        WholeTable wholeTable1 = new WholeTable();

        TableCellTextStyle tableCellTextStyle1 = new TableCellTextStyle();

        FontReference fontReference1 = new FontReference() { Index = FontCollectionIndexValues.Minor };
        RgbColorModelPercentage rgbColorModelPercentage1 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

        fontReference1.Append(rgbColorModelPercentage1);
        SchemeColor schemeColor3 = new SchemeColor() { Val = SchemeColorValues.Light1 };

        tableCellTextStyle1.Append(fontReference1);
        tableCellTextStyle1.Append(schemeColor3);

        TableCellStyle tableCellStyle1 = new TableCellStyle();

        TableCellBorders tableCellBorders1 = new TableCellBorders();

        LeftBorder leftBorder1 = new LeftBorder();

        LineReference lineReference1 = new LineReference() { Index = (UInt32Value) 1U };

        SchemeColor schemeColor4 = new SchemeColor() { Val = SchemeColorValues.Accent5 };
        Tint tint1 = new Tint() { Val = 50000 };

        schemeColor4.Append(tint1);

        lineReference1.Append(schemeColor4);

        leftBorder1.Append(lineReference1);

        RightBorder rightBorder1 = new RightBorder();

        LineReference lineReference2 = new LineReference() { Index = (UInt32Value) 1U };

        SchemeColor schemeColor5 = new SchemeColor() { Val = SchemeColorValues.Accent5 };
        Tint tint2 = new Tint() { Val = 50000 };

        schemeColor5.Append(tint2);

        lineReference2.Append(schemeColor5);

        rightBorder1.Append(lineReference2);

        TopBorder topBorder1 = new TopBorder();

        LineReference lineReference3 = new LineReference() { Index = (UInt32Value) 1U };

        SchemeColor schemeColor6 = new SchemeColor() { Val = SchemeColorValues.Accent5 };
        Tint tint3 = new Tint() { Val = 50000 };

        schemeColor6.Append(tint3);

        lineReference3.Append(schemeColor6);

        topBorder1.Append(lineReference3);

        BottomBorder bottomBorder1 = new BottomBorder();

        LineReference lineReference4 = new LineReference() { Index = (UInt32Value) 1U };

        SchemeColor schemeColor7 = new SchemeColor() { Val = SchemeColorValues.Accent5 };
        Tint tint4 = new Tint() { Val = 50000 };

        schemeColor7.Append(tint4);

        lineReference4.Append(schemeColor7);

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

        FillProperties fillProperties2 = new FillProperties();

        SolidFill solidFill1 = new SolidFill();

        SchemeColor schemeColor8 = new SchemeColor() { Val = SchemeColorValues.Light1 };
        Alpha alpha1 = new Alpha() { Val = 20000 };

        schemeColor8.Append(alpha1);

        solidFill1.Append(schemeColor8);

        fillProperties2.Append(solidFill1);

        tableCellStyle2.Append(tableCellBorders2);
        tableCellStyle2.Append(fillProperties2);

        band1Horizontal1.Append(tableCellStyle2);

        Band1Vertical band1Vertical1 = new Band1Vertical();

        TableCellStyle tableCellStyle3 = new TableCellStyle();
        TableCellBorders tableCellBorders3 = new TableCellBorders();

        FillProperties fillProperties3 = new FillProperties();

        SolidFill solidFill2 = new SolidFill();

        SchemeColor schemeColor9 = new SchemeColor() { Val = SchemeColorValues.Light1 };
        Alpha alpha2 = new Alpha() { Val = 20000 };

        schemeColor9.Append(alpha2);

        solidFill2.Append(schemeColor9);

        fillProperties3.Append(solidFill2);

        tableCellStyle3.Append(tableCellBorders3);
        tableCellStyle3.Append(fillProperties3);

        band1Vertical1.Append(tableCellStyle3);

        LastColumn lastColumn1 = new LastColumn();
        TableCellTextStyle tableCellTextStyle2 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle4 = new TableCellStyle();

        TableCellBorders tableCellBorders4 = new TableCellBorders();

        LeftBorder leftBorder2 = new LeftBorder();

        LineReference lineReference5 = new LineReference() { Index = (UInt32Value) 2U };
        SchemeColor schemeColor10 = new SchemeColor() { Val = SchemeColorValues.Light1 };

        lineReference5.Append(schemeColor10);

        leftBorder2.Append(lineReference5);

        tableCellBorders4.Append(leftBorder2);

        tableCellStyle4.Append(tableCellBorders4);

        lastColumn1.Append(tableCellTextStyle2);
        lastColumn1.Append(tableCellStyle4);

        FirstColumn firstColumn1 = new FirstColumn();
        TableCellTextStyle tableCellTextStyle3 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle5 = new TableCellStyle();

        TableCellBorders tableCellBorders5 = new TableCellBorders();

        RightBorder rightBorder2 = new RightBorder();

        LineReference lineReference6 = new LineReference() { Index = (UInt32Value) 2U };
        SchemeColor schemeColor11 = new SchemeColor() { Val = SchemeColorValues.Light1 };

        lineReference6.Append(schemeColor11);

        rightBorder2.Append(lineReference6);

        tableCellBorders5.Append(rightBorder2);

        tableCellStyle5.Append(tableCellBorders5);

        firstColumn1.Append(tableCellTextStyle3);
        firstColumn1.Append(tableCellStyle5);

        LastRow lastRow1 = new LastRow();
        TableCellTextStyle tableCellTextStyle4 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle6 = new TableCellStyle();

        TableCellBorders tableCellBorders6 = new TableCellBorders();

        TopBorder topBorder2 = new TopBorder();

        LineReference lineReference7 = new LineReference() { Index = (UInt32Value) 2U };
        SchemeColor schemeColor12 = new SchemeColor() { Val = SchemeColorValues.Light1 };

        lineReference7.Append(schemeColor12);

        topBorder2.Append(lineReference7);

        tableCellBorders6.Append(topBorder2);

        FillProperties fillProperties4 = new FillProperties();
        NoFill noFill4 = new NoFill();

        fillProperties4.Append(noFill4);

        tableCellStyle6.Append(tableCellBorders6);
        tableCellStyle6.Append(fillProperties4);

        lastRow1.Append(tableCellTextStyle4);
        lastRow1.Append(tableCellStyle6);

        SoutheastCell southeastCell1 = new SoutheastCell();

        TableCellStyle tableCellStyle7 = new TableCellStyle();

        TableCellBorders tableCellBorders7 = new TableCellBorders();

        LeftBorder leftBorder3 = new LeftBorder();

        Outline outline3 = new Outline();
        NoFill noFill5 = new NoFill();

        outline3.Append(noFill5);

        leftBorder3.Append(outline3);

        TopBorder topBorder3 = new TopBorder();

        Outline outline4 = new Outline();
        NoFill noFill6 = new NoFill();

        outline4.Append(noFill6);

        topBorder3.Append(outline4);

        tableCellBorders7.Append(leftBorder3);
        tableCellBorders7.Append(topBorder3);

        tableCellStyle7.Append(tableCellBorders7);

        southeastCell1.Append(tableCellStyle7);

        SouthwestCell southwestCell1 = new SouthwestCell();

        TableCellStyle tableCellStyle8 = new TableCellStyle();

        TableCellBorders tableCellBorders8 = new TableCellBorders();

        RightBorder rightBorder3 = new RightBorder();

        Outline outline5 = new Outline();
        NoFill noFill7 = new NoFill();

        outline5.Append(noFill7);

        rightBorder3.Append(outline5);

        TopBorder topBorder4 = new TopBorder();

        Outline outline6 = new Outline();
        NoFill noFill8 = new NoFill();

        outline6.Append(noFill8);

        topBorder4.Append(outline6);

        tableCellBorders8.Append(rightBorder3);
        tableCellBorders8.Append(topBorder4);

        tableCellStyle8.Append(tableCellBorders8);

        southwestCell1.Append(tableCellStyle8);

        FirstRow firstRow1 = new FirstRow();
        TableCellTextStyle tableCellTextStyle5 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle9 = new TableCellStyle();

        TableCellBorders tableCellBorders9 = new TableCellBorders();

        BottomBorder bottomBorder2 = new BottomBorder();

        LineReference lineReference8 = new LineReference() { Index = (UInt32Value) 3U };
        SchemeColor schemeColor13 = new SchemeColor() { Val = SchemeColorValues.Light1 };

        lineReference8.Append(schemeColor13);

        bottomBorder2.Append(lineReference8);

        tableCellBorders9.Append(bottomBorder2);

        FillProperties fillProperties5 = new FillProperties();
        NoFill noFill9 = new NoFill();

        fillProperties5.Append(noFill9);

        tableCellStyle9.Append(tableCellBorders9);
        tableCellStyle9.Append(fillProperties5);

        firstRow1.Append(tableCellTextStyle5);
        firstRow1.Append(tableCellStyle9);

        NortheastCell northeastCell1 = new NortheastCell();

        TableCellStyle tableCellStyle10 = new TableCellStyle();

        TableCellBorders tableCellBorders10 = new TableCellBorders();

        BottomBorder bottomBorder3 = new BottomBorder();

        Outline outline7 = new Outline();
        NoFill noFill10 = new NoFill();

        outline7.Append(noFill10);

        bottomBorder3.Append(outline7);

        tableCellBorders10.Append(bottomBorder3);

        tableCellStyle10.Append(tableCellBorders10);

        northeastCell1.Append(tableCellStyle10);

        tableStyleEntry1.Append(tableBackground1);
        tableStyleEntry1.Append(wholeTable1);
        tableStyleEntry1.Append(band1Horizontal1);
        tableStyleEntry1.Append(band1Vertical1);
        tableStyleEntry1.Append(lastColumn1);
        tableStyleEntry1.Append(firstColumn1);
        tableStyleEntry1.Append(lastRow1);
        tableStyleEntry1.Append(southeastCell1);
        tableStyleEntry1.Append(southwestCell1);
        tableStyleEntry1.Append(firstRow1);
        tableStyleEntry1.Append(northeastCell1);
        return tableStyleEntry1;
    }

}
