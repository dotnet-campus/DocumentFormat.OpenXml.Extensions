using System.CodeDom.Compiler;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries;

[GeneratedCode("OpenXmlSdkTool", "2.5")]
internal static class ThemedStyle1Accent2_284E427A_3D55_4303_BF80_6455036E1DE7
{
    public static TableStyleEntry GenerateTableStyleEntry()
    {
        TableStyleEntry tableStyleEntry1 = new TableStyleEntry() { StyleId = "{284E427A-3D55-4303-BF80-6455036E1DE7}", StyleName = "主题样式 1 - 强调 2" };

        TableBackground tableBackground1 = new TableBackground();

        FillReference fillReference1 = new FillReference() { Index = (UInt32Value) 2U };
        SchemeColor schemeColor1 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        fillReference1.Append(schemeColor1);

        EffectReference effectReference1 = new EffectReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor2 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        effectReference1.Append(schemeColor2);

        tableBackground1.Append(fillReference1);
        tableBackground1.Append(effectReference1);

        WholeTable wholeTable1 = new WholeTable();

        TableCellTextStyle tableCellTextStyle1 = new TableCellTextStyle();

        FontReference fontReference1 = new FontReference() { Index = FontCollectionIndexValues.Minor };
        RgbColorModelPercentage rgbColorModelPercentage1 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

        fontReference1.Append(rgbColorModelPercentage1);
        SchemeColor schemeColor3 = new SchemeColor() { Val = SchemeColorValues.Dark1 };

        tableCellTextStyle1.Append(fontReference1);
        tableCellTextStyle1.Append(schemeColor3);

        TableCellStyle tableCellStyle1 = new TableCellStyle();

        TableCellBorders tableCellBorders1 = new TableCellBorders();

        LeftBorder leftBorder1 = new LeftBorder();

        LineReference lineReference1 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor4 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference1.Append(schemeColor4);

        leftBorder1.Append(lineReference1);

        RightBorder rightBorder1 = new RightBorder();

        LineReference lineReference2 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor5 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference2.Append(schemeColor5);

        rightBorder1.Append(lineReference2);

        TopBorder topBorder1 = new TopBorder();

        LineReference lineReference3 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor6 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference3.Append(schemeColor6);

        topBorder1.Append(lineReference3);

        BottomBorder bottomBorder1 = new BottomBorder();

        LineReference lineReference4 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor7 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference4.Append(schemeColor7);

        bottomBorder1.Append(lineReference4);

        InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder();

        LineReference lineReference5 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor8 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference5.Append(schemeColor8);

        insideHorizontalBorder1.Append(lineReference5);

        InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder();

        LineReference lineReference6 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor9 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference6.Append(schemeColor9);

        insideVerticalBorder1.Append(lineReference6);

        tableCellBorders1.Append(leftBorder1);
        tableCellBorders1.Append(rightBorder1);
        tableCellBorders1.Append(topBorder1);
        tableCellBorders1.Append(bottomBorder1);
        tableCellBorders1.Append(insideHorizontalBorder1);
        tableCellBorders1.Append(insideVerticalBorder1);

        FillProperties fillProperties1 = new FillProperties();
        NoFill noFill1 = new NoFill();

        fillProperties1.Append(noFill1);

        tableCellStyle1.Append(tableCellBorders1);
        tableCellStyle1.Append(fillProperties1);

        wholeTable1.Append(tableCellTextStyle1);
        wholeTable1.Append(tableCellStyle1);

        Band1Horizontal band1Horizontal1 = new Band1Horizontal();

        TableCellStyle tableCellStyle2 = new TableCellStyle();
        TableCellBorders tableCellBorders2 = new TableCellBorders();

        FillProperties fillProperties2 = new FillProperties();

        SolidFill solidFill1 = new SolidFill();

        SchemeColor schemeColor10 = new SchemeColor() { Val = SchemeColorValues.Accent2 };
        Alpha alpha1 = new Alpha() { Val = 40000 };

        schemeColor10.Append(alpha1);

        solidFill1.Append(schemeColor10);

        fillProperties2.Append(solidFill1);

        tableCellStyle2.Append(tableCellBorders2);
        tableCellStyle2.Append(fillProperties2);

        band1Horizontal1.Append(tableCellStyle2);

        Band2Horizontal band2Horizontal1 = new Band2Horizontal();

        TableCellStyle tableCellStyle3 = new TableCellStyle();
        TableCellBorders tableCellBorders3 = new TableCellBorders();

        tableCellStyle3.Append(tableCellBorders3);

        band2Horizontal1.Append(tableCellStyle3);

        Band1Vertical band1Vertical1 = new Band1Vertical();

        TableCellStyle tableCellStyle4 = new TableCellStyle();

        TableCellBorders tableCellBorders4 = new TableCellBorders();

        TopBorder topBorder2 = new TopBorder();

        LineReference lineReference7 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor11 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference7.Append(schemeColor11);

        topBorder2.Append(lineReference7);

        BottomBorder bottomBorder2 = new BottomBorder();

        LineReference lineReference8 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor12 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference8.Append(schemeColor12);

        bottomBorder2.Append(lineReference8);

        tableCellBorders4.Append(topBorder2);
        tableCellBorders4.Append(bottomBorder2);

        FillProperties fillProperties3 = new FillProperties();

        SolidFill solidFill2 = new SolidFill();

        SchemeColor schemeColor13 = new SchemeColor() { Val = SchemeColorValues.Accent2 };
        Alpha alpha2 = new Alpha() { Val = 40000 };

        schemeColor13.Append(alpha2);

        solidFill2.Append(schemeColor13);

        fillProperties3.Append(solidFill2);

        tableCellStyle4.Append(tableCellBorders4);
        tableCellStyle4.Append(fillProperties3);

        band1Vertical1.Append(tableCellStyle4);

        Band2Vertical band2Vertical1 = new Band2Vertical();

        TableCellStyle tableCellStyle5 = new TableCellStyle();
        TableCellBorders tableCellBorders5 = new TableCellBorders();

        tableCellStyle5.Append(tableCellBorders5);

        band2Vertical1.Append(tableCellStyle5);

        LastColumn lastColumn1 = new LastColumn();
        TableCellTextStyle tableCellTextStyle2 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle6 = new TableCellStyle();

        TableCellBorders tableCellBorders6 = new TableCellBorders();

        LeftBorder leftBorder2 = new LeftBorder();

        LineReference lineReference9 = new LineReference() { Index = (UInt32Value) 2U };
        SchemeColor schemeColor14 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference9.Append(schemeColor14);

        leftBorder2.Append(lineReference9);

        RightBorder rightBorder2 = new RightBorder();

        LineReference lineReference10 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor15 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference10.Append(schemeColor15);

        rightBorder2.Append(lineReference10);

        TopBorder topBorder3 = new TopBorder();

        LineReference lineReference11 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor16 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference11.Append(schemeColor16);

        topBorder3.Append(lineReference11);

        BottomBorder bottomBorder3 = new BottomBorder();

        LineReference lineReference12 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor17 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference12.Append(schemeColor17);

        bottomBorder3.Append(lineReference12);

        InsideHorizontalBorder insideHorizontalBorder2 = new InsideHorizontalBorder();

        LineReference lineReference13 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor18 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference13.Append(schemeColor18);

        insideHorizontalBorder2.Append(lineReference13);

        InsideVerticalBorder insideVerticalBorder2 = new InsideVerticalBorder();

        Outline outline1 = new Outline();
        NoFill noFill2 = new NoFill();

        outline1.Append(noFill2);

        insideVerticalBorder2.Append(outline1);

        tableCellBorders6.Append(leftBorder2);
        tableCellBorders6.Append(rightBorder2);
        tableCellBorders6.Append(topBorder3);
        tableCellBorders6.Append(bottomBorder3);
        tableCellBorders6.Append(insideHorizontalBorder2);
        tableCellBorders6.Append(insideVerticalBorder2);

        tableCellStyle6.Append(tableCellBorders6);

        lastColumn1.Append(tableCellTextStyle2);
        lastColumn1.Append(tableCellStyle6);

        FirstColumn firstColumn1 = new FirstColumn();
        TableCellTextStyle tableCellTextStyle3 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle7 = new TableCellStyle();

        TableCellBorders tableCellBorders7 = new TableCellBorders();

        LeftBorder leftBorder3 = new LeftBorder();

        LineReference lineReference14 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor19 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference14.Append(schemeColor19);

        leftBorder3.Append(lineReference14);

        RightBorder rightBorder3 = new RightBorder();

        LineReference lineReference15 = new LineReference() { Index = (UInt32Value) 2U };
        SchemeColor schemeColor20 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference15.Append(schemeColor20);

        rightBorder3.Append(lineReference15);

        TopBorder topBorder4 = new TopBorder();

        LineReference lineReference16 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor21 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference16.Append(schemeColor21);

        topBorder4.Append(lineReference16);

        BottomBorder bottomBorder4 = new BottomBorder();

        LineReference lineReference17 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor22 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference17.Append(schemeColor22);

        bottomBorder4.Append(lineReference17);

        InsideHorizontalBorder insideHorizontalBorder3 = new InsideHorizontalBorder();

        LineReference lineReference18 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor23 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference18.Append(schemeColor23);

        insideHorizontalBorder3.Append(lineReference18);

        InsideVerticalBorder insideVerticalBorder3 = new InsideVerticalBorder();

        Outline outline2 = new Outline();
        NoFill noFill3 = new NoFill();

        outline2.Append(noFill3);

        insideVerticalBorder3.Append(outline2);

        tableCellBorders7.Append(leftBorder3);
        tableCellBorders7.Append(rightBorder3);
        tableCellBorders7.Append(topBorder4);
        tableCellBorders7.Append(bottomBorder4);
        tableCellBorders7.Append(insideHorizontalBorder3);
        tableCellBorders7.Append(insideVerticalBorder3);

        tableCellStyle7.Append(tableCellBorders7);

        firstColumn1.Append(tableCellTextStyle3);
        firstColumn1.Append(tableCellStyle7);

        LastRow lastRow1 = new LastRow();
        TableCellTextStyle tableCellTextStyle4 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle8 = new TableCellStyle();

        TableCellBorders tableCellBorders8 = new TableCellBorders();

        LeftBorder leftBorder4 = new LeftBorder();

        LineReference lineReference19 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor24 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference19.Append(schemeColor24);

        leftBorder4.Append(lineReference19);

        RightBorder rightBorder4 = new RightBorder();

        LineReference lineReference20 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor25 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference20.Append(schemeColor25);

        rightBorder4.Append(lineReference20);

        TopBorder topBorder5 = new TopBorder();

        LineReference lineReference21 = new LineReference() { Index = (UInt32Value) 2U };
        SchemeColor schemeColor26 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference21.Append(schemeColor26);

        topBorder5.Append(lineReference21);

        BottomBorder bottomBorder5 = new BottomBorder();

        LineReference lineReference22 = new LineReference() { Index = (UInt32Value) 2U };
        SchemeColor schemeColor27 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference22.Append(schemeColor27);

        bottomBorder5.Append(lineReference22);

        InsideHorizontalBorder insideHorizontalBorder4 = new InsideHorizontalBorder();

        Outline outline3 = new Outline();
        NoFill noFill4 = new NoFill();

        outline3.Append(noFill4);

        insideHorizontalBorder4.Append(outline3);

        InsideVerticalBorder insideVerticalBorder4 = new InsideVerticalBorder();

        Outline outline4 = new Outline();
        NoFill noFill5 = new NoFill();

        outline4.Append(noFill5);

        insideVerticalBorder4.Append(outline4);

        tableCellBorders8.Append(leftBorder4);
        tableCellBorders8.Append(rightBorder4);
        tableCellBorders8.Append(topBorder5);
        tableCellBorders8.Append(bottomBorder5);
        tableCellBorders8.Append(insideHorizontalBorder4);
        tableCellBorders8.Append(insideVerticalBorder4);

        FillProperties fillProperties4 = new FillProperties();
        NoFill noFill6 = new NoFill();

        fillProperties4.Append(noFill6);

        tableCellStyle8.Append(tableCellBorders8);
        tableCellStyle8.Append(fillProperties4);

        lastRow1.Append(tableCellTextStyle4);
        lastRow1.Append(tableCellStyle8);

        FirstRow firstRow1 = new FirstRow();

        TableCellTextStyle tableCellTextStyle5 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        FontReference fontReference2 = new FontReference() { Index = FontCollectionIndexValues.Minor };
        RgbColorModelPercentage rgbColorModelPercentage2 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

        fontReference2.Append(rgbColorModelPercentage2);
        SchemeColor schemeColor28 = new SchemeColor() { Val = SchemeColorValues.Light1 };

        tableCellTextStyle5.Append(fontReference2);
        tableCellTextStyle5.Append(schemeColor28);

        TableCellStyle tableCellStyle9 = new TableCellStyle();

        TableCellBorders tableCellBorders9 = new TableCellBorders();

        LeftBorder leftBorder5 = new LeftBorder();

        LineReference lineReference23 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor29 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference23.Append(schemeColor29);

        leftBorder5.Append(lineReference23);

        RightBorder rightBorder5 = new RightBorder();

        LineReference lineReference24 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor30 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference24.Append(schemeColor30);

        rightBorder5.Append(lineReference24);

        TopBorder topBorder6 = new TopBorder();

        LineReference lineReference25 = new LineReference() { Index = (UInt32Value) 1U };
        SchemeColor schemeColor31 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        lineReference25.Append(schemeColor31);

        topBorder6.Append(lineReference25);

        BottomBorder bottomBorder6 = new BottomBorder();

        LineReference lineReference26 = new LineReference() { Index = (UInt32Value) 2U };
        SchemeColor schemeColor32 = new SchemeColor() { Val = SchemeColorValues.Light1 };

        lineReference26.Append(schemeColor32);

        bottomBorder6.Append(lineReference26);

        InsideHorizontalBorder insideHorizontalBorder5 = new InsideHorizontalBorder();

        Outline outline5 = new Outline();
        NoFill noFill7 = new NoFill();

        outline5.Append(noFill7);

        insideHorizontalBorder5.Append(outline5);

        InsideVerticalBorder insideVerticalBorder5 = new InsideVerticalBorder();

        Outline outline6 = new Outline();
        NoFill noFill8 = new NoFill();

        outline6.Append(noFill8);

        insideVerticalBorder5.Append(outline6);

        tableCellBorders9.Append(leftBorder5);
        tableCellBorders9.Append(rightBorder5);
        tableCellBorders9.Append(topBorder6);
        tableCellBorders9.Append(bottomBorder6);
        tableCellBorders9.Append(insideHorizontalBorder5);
        tableCellBorders9.Append(insideVerticalBorder5);

        FillProperties fillProperties5 = new FillProperties();

        SolidFill solidFill3 = new SolidFill();
        SchemeColor schemeColor33 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

        solidFill3.Append(schemeColor33);

        fillProperties5.Append(solidFill3);

        tableCellStyle9.Append(tableCellBorders9);
        tableCellStyle9.Append(fillProperties5);

        firstRow1.Append(tableCellTextStyle5);
        firstRow1.Append(tableCellStyle9);

        tableStyleEntry1.Append(tableBackground1);
        tableStyleEntry1.Append(wholeTable1);
        tableStyleEntry1.Append(band1Horizontal1);
        tableStyleEntry1.Append(band2Horizontal1);
        tableStyleEntry1.Append(band1Vertical1);
        tableStyleEntry1.Append(band2Vertical1);
        tableStyleEntry1.Append(lastColumn1);
        tableStyleEntry1.Append(firstColumn1);
        tableStyleEntry1.Append(lastRow1);
        tableStyleEntry1.Append(firstRow1);
        return tableStyleEntry1;
    }
}
