using System.CodeDom.Compiler;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries;

[GeneratedCode("OpenXmlSdkTool", "2.5")]
internal static class NoStyleTableGrid_5940675A_B579_460E_94D1_54222C63F5DA
{
    public static TableStyleEntry GenerateTableStyleEntry()
    {
        TableStyleEntry tableStyleEntry1 = new TableStyleEntry() { StyleId = "{5940675A-B579-460E-94D1-54222C63F5DA}", StyleName = "无样式，网格型" };

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

        Outline outline1 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

        SolidFill solidFill1 = new SolidFill();
        SchemeColor schemeColor2 = new SchemeColor() { Val = SchemeColorValues.Text1 };

        solidFill1.Append(schemeColor2);

        outline1.Append(solidFill1);

        leftBorder1.Append(outline1);

        RightBorder rightBorder1 = new RightBorder();

        Outline outline2 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

        SolidFill solidFill2 = new SolidFill();
        SchemeColor schemeColor3 = new SchemeColor() { Val = SchemeColorValues.Text1 };

        solidFill2.Append(schemeColor3);

        outline2.Append(solidFill2);

        rightBorder1.Append(outline2);

        TopBorder topBorder1 = new TopBorder();

        Outline outline3 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

        SolidFill solidFill3 = new SolidFill();
        SchemeColor schemeColor4 = new SchemeColor() { Val = SchemeColorValues.Text1 };

        solidFill3.Append(schemeColor4);

        outline3.Append(solidFill3);

        topBorder1.Append(outline3);

        BottomBorder bottomBorder1 = new BottomBorder();

        Outline outline4 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

        SolidFill solidFill4 = new SolidFill();
        SchemeColor schemeColor5 = new SchemeColor() { Val = SchemeColorValues.Text1 };

        solidFill4.Append(schemeColor5);

        outline4.Append(solidFill4);

        bottomBorder1.Append(outline4);

        InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder();

        Outline outline5 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

        SolidFill solidFill5 = new SolidFill();
        SchemeColor schemeColor6 = new SchemeColor() { Val = SchemeColorValues.Text1 };

        solidFill5.Append(schemeColor6);

        outline5.Append(solidFill5);

        insideHorizontalBorder1.Append(outline5);

        InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder();

        Outline outline6 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

        SolidFill solidFill6 = new SolidFill();
        SchemeColor schemeColor7 = new SchemeColor() { Val = SchemeColorValues.Text1 };

        solidFill6.Append(schemeColor7);

        outline6.Append(solidFill6);

        insideVerticalBorder1.Append(outline6);

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

        tableStyleEntry1.Append(wholeTable1);
        return tableStyleEntry1;
    }
}
