using System.CodeDom.Compiler;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries;

[GeneratedCode("OpenXmlSdkTool", "2.5")]
internal static class LightStyle1Accent1_3B4B98B0_60AC_42C2_AFA5_B58CD77FA1E5
{
    public static TableStyleEntry GenerateTableStyleEntry()
    {
        TableStyleEntry tableStyleEntry1 = new TableStyleEntry() { StyleId = "{3B4B98B0-60AC-42C2-AFA5-B58CD77FA1E5}", StyleName = "浅色样式 1 - 强调 1" };

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

        Outline outline1 = new Outline();
        NoFill noFill1 = new NoFill();

        outline1.Append(noFill1);

        leftBorder1.Append(outline1);

        RightBorder rightBorder1 = new RightBorder();

        Outline outline2 = new Outline();
        NoFill noFill2 = new NoFill();

        outline2.Append(noFill2);

        rightBorder1.Append(outline2);

        TopBorder topBorder1 = new TopBorder();

        Outline outline3 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

        SolidFill solidFill1 = new SolidFill();
        SchemeColor schemeColor2 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

        solidFill1.Append(schemeColor2);

        outline3.Append(solidFill1);

        topBorder1.Append(outline3);

        BottomBorder bottomBorder1 = new BottomBorder();

        Outline outline4 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

        SolidFill solidFill2 = new SolidFill();
        SchemeColor schemeColor3 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

        solidFill2.Append(schemeColor3);

        outline4.Append(solidFill2);

        bottomBorder1.Append(outline4);

        InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder();

        Outline outline5 = new Outline();
        NoFill noFill3 = new NoFill();

        outline5.Append(noFill3);

        insideHorizontalBorder1.Append(outline5);

        InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder();

        Outline outline6 = new Outline();
        NoFill noFill4 = new NoFill();

        outline6.Append(noFill4);

        insideVerticalBorder1.Append(outline6);

        tableCellBorders1.Append(leftBorder1);
        tableCellBorders1.Append(rightBorder1);
        tableCellBorders1.Append(topBorder1);
        tableCellBorders1.Append(bottomBorder1);
        tableCellBorders1.Append(insideHorizontalBorder1);
        tableCellBorders1.Append(insideVerticalBorder1);

        FillProperties fillProperties1 = new FillProperties();
        NoFill noFill5 = new NoFill();

        fillProperties1.Append(noFill5);

        tableCellStyle1.Append(tableCellBorders1);
        tableCellStyle1.Append(fillProperties1);

        wholeTable1.Append(tableCellTextStyle1);
        wholeTable1.Append(tableCellStyle1);

        Band1Horizontal band1Horizontal1 = new Band1Horizontal();

        TableCellStyle tableCellStyle2 = new TableCellStyle();
        TableCellBorders tableCellBorders2 = new TableCellBorders();

        FillProperties fillProperties2 = new FillProperties();

        SolidFill solidFill3 = new SolidFill();

        SchemeColor schemeColor4 = new SchemeColor() { Val = SchemeColorValues.Accent1 };
        Alpha alpha1 = new Alpha() { Val = 20000 };

        schemeColor4.Append(alpha1);

        solidFill3.Append(schemeColor4);

        fillProperties2.Append(solidFill3);

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

        FillProperties fillProperties3 = new FillProperties();

        SolidFill solidFill4 = new SolidFill();

        SchemeColor schemeColor5 = new SchemeColor() { Val = SchemeColorValues.Accent1 };
        Alpha alpha2 = new Alpha() { Val = 20000 };

        schemeColor5.Append(alpha2);

        solidFill4.Append(schemeColor5);

        fillProperties3.Append(solidFill4);

        tableCellStyle4.Append(tableCellBorders4);
        tableCellStyle4.Append(fillProperties3);

        band1Vertical1.Append(tableCellStyle4);

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

        TopBorder topBorder2 = new TopBorder();

        Outline outline7 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

        SolidFill solidFill5 = new SolidFill();
        SchemeColor schemeColor6 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

        solidFill5.Append(schemeColor6);

        outline7.Append(solidFill5);

        topBorder2.Append(outline7);

        tableCellBorders7.Append(topBorder2);

        FillProperties fillProperties4 = new FillProperties();
        NoFill noFill6 = new NoFill();

        fillProperties4.Append(noFill6);

        tableCellStyle7.Append(tableCellBorders7);
        tableCellStyle7.Append(fillProperties4);

        lastRow1.Append(tableCellTextStyle4);
        lastRow1.Append(tableCellStyle7);

        FirstRow firstRow1 = new FirstRow();
        TableCellTextStyle tableCellTextStyle5 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

        TableCellStyle tableCellStyle8 = new TableCellStyle();

        TableCellBorders tableCellBorders8 = new TableCellBorders();

        BottomBorder bottomBorder2 = new BottomBorder();

        Outline outline8 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

        SolidFill solidFill6 = new SolidFill();
        SchemeColor schemeColor7 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

        solidFill6.Append(schemeColor7);

        outline8.Append(solidFill6);

        bottomBorder2.Append(outline8);

        tableCellBorders8.Append(bottomBorder2);

        FillProperties fillProperties5 = new FillProperties();
        NoFill noFill7 = new NoFill();

        fillProperties5.Append(noFill7);

        tableCellStyle8.Append(tableCellBorders8);
        tableCellStyle8.Append(fillProperties5);

        firstRow1.Append(tableCellTextStyle5);
        firstRow1.Append(tableCellStyle8);

        tableStyleEntry1.Append(wholeTable1);
        tableStyleEntry1.Append(band1Horizontal1);
        tableStyleEntry1.Append(band2Horizontal1);
        tableStyleEntry1.Append(band1Vertical1);
        tableStyleEntry1.Append(lastColumn1);
        tableStyleEntry1.Append(firstColumn1);
        tableStyleEntry1.Append(lastRow1);
        tableStyleEntry1.Append(firstRow1);
        return tableStyleEntry1;
    }

}
