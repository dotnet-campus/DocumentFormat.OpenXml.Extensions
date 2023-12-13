using System.CodeDom.Compiler;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries;

[GeneratedCode("OpenXmlSdkTool", "2.5")]
public static class NoStyleNoGrid_2D5ABB26_0587_4C30_8999_92F81FD0307C
{
    public static TableStyleEntry GenerateTableStyleEntry()
    {
        TableStyleEntry tableStyleEntry1 = new TableStyleEntry() { StyleId = "{2D5ABB26-0587-4C30-8999-92F81FD0307C}", StyleName = "无样式，无网格" };

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

        Outline outline3 = new Outline();
        NoFill noFill3 = new NoFill();

        outline3.Append(noFill3);

        topBorder1.Append(outline3);

        BottomBorder bottomBorder1 = new BottomBorder();

        Outline outline4 = new Outline();
        NoFill noFill4 = new NoFill();

        outline4.Append(noFill4);

        bottomBorder1.Append(outline4);

        InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder();

        Outline outline5 = new Outline();
        NoFill noFill5 = new NoFill();

        outline5.Append(noFill5);

        insideHorizontalBorder1.Append(outline5);

        InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder();

        Outline outline6 = new Outline();
        NoFill noFill6 = new NoFill();

        outline6.Append(noFill6);

        insideVerticalBorder1.Append(outline6);

        tableCellBorders1.Append(leftBorder1);
        tableCellBorders1.Append(rightBorder1);
        tableCellBorders1.Append(topBorder1);
        tableCellBorders1.Append(bottomBorder1);
        tableCellBorders1.Append(insideHorizontalBorder1);
        tableCellBorders1.Append(insideVerticalBorder1);

        FillProperties fillProperties1 = new FillProperties();
        NoFill noFill7 = new NoFill();

        fillProperties1.Append(noFill7);

        tableCellStyle1.Append(tableCellBorders1);
        tableCellStyle1.Append(fillProperties1);

        wholeTable1.Append(tableCellTextStyle1);
        wholeTable1.Append(tableCellStyle1);

        tableStyleEntry1.Append(wholeTable1);
        return tableStyleEntry1;
    }
}
