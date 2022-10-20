using System.CodeDom.Compiler;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries
{
    [GeneratedCode("OpenXmlSdkTool", "2.5")]
    internal static class MediumStyle4Accent1_69CF1AB2_1976_4502_BF36_3FF5EA218861
    {
        // Creates an TableStyleEntry instance and adds its children.
        public static TableStyleEntry GenerateTableStyleEntry()
        {
            TableStyleEntry tableStyleEntry1 = new TableStyleEntry() { StyleId = "{69CF1AB2-1976-4502-BF36-3FF5EA218861}", StyleName = "中度样式 4 - 强调 1" };

            WholeTable wholeTable1 = new WholeTable();

            TableCellTextStyle tableCellTextStyle1 = new TableCellTextStyle();

            FontReference fontReference1 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            RgbColorModelPercentage rgbColorModelPercentage1 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

            fontReference1.Append(rgbColorModelPercentage1);
            SchemeColor schemeColor1 = new SchemeColor() { Val = SchemeColorValues.Dark1 };

            tableCellTextStyle1.Append(fontReference1);
            tableCellTextStyle1.Append(schemeColor1);

            TableCellStyle tableCellStyle1 = new TableCellStyle();

            TableCellBorders tableCellBorders1 = new TableCellBorders();

            LeftBorder leftBorder1 = new LeftBorder();

            Outline outline1 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill1 = new SolidFill();
            SchemeColor schemeColor2 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

            solidFill1.Append(schemeColor2);

            outline1.Append(solidFill1);

            leftBorder1.Append(outline1);

            RightBorder rightBorder1 = new RightBorder();

            Outline outline2 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill2 = new SolidFill();
            SchemeColor schemeColor3 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

            solidFill2.Append(schemeColor3);

            outline2.Append(solidFill2);

            rightBorder1.Append(outline2);

            TopBorder topBorder1 = new TopBorder();

            Outline outline3 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill3 = new SolidFill();
            SchemeColor schemeColor4 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

            solidFill3.Append(schemeColor4);

            outline3.Append(solidFill3);

            topBorder1.Append(outline3);

            BottomBorder bottomBorder1 = new BottomBorder();

            Outline outline4 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill4 = new SolidFill();
            SchemeColor schemeColor5 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

            solidFill4.Append(schemeColor5);

            outline4.Append(solidFill4);

            bottomBorder1.Append(outline4);

            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder();

            Outline outline5 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill5 = new SolidFill();
            SchemeColor schemeColor6 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

            solidFill5.Append(schemeColor6);

            outline5.Append(solidFill5);

            insideHorizontalBorder1.Append(outline5);

            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder();

            Outline outline6 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill6 = new SolidFill();
            SchemeColor schemeColor7 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

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

            SolidFill solidFill7 = new SolidFill();

            SchemeColor schemeColor8 = new SchemeColor() { Val = SchemeColorValues.Accent1 };
            Tint tint1 = new Tint() { Val = 20000 };

            schemeColor8.Append(tint1);

            solidFill7.Append(schemeColor8);

            fillProperties1.Append(solidFill7);

            tableCellStyle1.Append(tableCellBorders1);
            tableCellStyle1.Append(fillProperties1);

            wholeTable1.Append(tableCellTextStyle1);
            wholeTable1.Append(tableCellStyle1);

            Band1Horizontal band1Horizontal1 = new Band1Horizontal();

            TableCellStyle tableCellStyle2 = new TableCellStyle();
            TableCellBorders tableCellBorders2 = new TableCellBorders();

            FillProperties fillProperties2 = new FillProperties();

            SolidFill solidFill8 = new SolidFill();

            SchemeColor schemeColor9 = new SchemeColor() { Val = SchemeColorValues.Accent1 };
            Tint tint2 = new Tint() { Val = 40000 };

            schemeColor9.Append(tint2);

            solidFill8.Append(schemeColor9);

            fillProperties2.Append(solidFill8);

            tableCellStyle2.Append(tableCellBorders2);
            tableCellStyle2.Append(fillProperties2);

            band1Horizontal1.Append(tableCellStyle2);

            Band1Vertical band1Vertical1 = new Band1Vertical();

            TableCellStyle tableCellStyle3 = new TableCellStyle();
            TableCellBorders tableCellBorders3 = new TableCellBorders();

            FillProperties fillProperties3 = new FillProperties();

            SolidFill solidFill9 = new SolidFill();

            SchemeColor schemeColor10 = new SchemeColor() { Val = SchemeColorValues.Accent1 };
            Tint tint3 = new Tint() { Val = 40000 };

            schemeColor10.Append(tint3);

            solidFill9.Append(schemeColor10);

            fillProperties3.Append(solidFill9);

            tableCellStyle3.Append(tableCellBorders3);
            tableCellStyle3.Append(fillProperties3);

            band1Vertical1.Append(tableCellStyle3);

            LastColumn lastColumn1 = new LastColumn();
            TableCellTextStyle tableCellTextStyle2 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            TableCellStyle tableCellStyle4 = new TableCellStyle();
            TableCellBorders tableCellBorders4 = new TableCellBorders();

            tableCellStyle4.Append(tableCellBorders4);

            lastColumn1.Append(tableCellTextStyle2);
            lastColumn1.Append(tableCellStyle4);

            FirstColumn firstColumn1 = new FirstColumn();
            TableCellTextStyle tableCellTextStyle3 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            TableCellStyle tableCellStyle5 = new TableCellStyle();
            TableCellBorders tableCellBorders5 = new TableCellBorders();

            tableCellStyle5.Append(tableCellBorders5);

            firstColumn1.Append(tableCellTextStyle3);
            firstColumn1.Append(tableCellStyle5);

            LastRow lastRow1 = new LastRow();
            TableCellTextStyle tableCellTextStyle4 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            TableCellStyle tableCellStyle6 = new TableCellStyle();

            TableCellBorders tableCellBorders6 = new TableCellBorders();

            TopBorder topBorder2 = new TopBorder();

            Outline outline7 = new Outline() { Width = 25400, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill10 = new SolidFill();
            SchemeColor schemeColor11 = new SchemeColor() { Val = SchemeColorValues.Accent1 };

            solidFill10.Append(schemeColor11);

            outline7.Append(solidFill10);

            topBorder2.Append(outline7);

            tableCellBorders6.Append(topBorder2);

            FillProperties fillProperties4 = new FillProperties();

            SolidFill solidFill11 = new SolidFill();

            SchemeColor schemeColor12 = new SchemeColor() { Val = SchemeColorValues.Accent1 };
            Tint tint4 = new Tint() { Val = 20000 };

            schemeColor12.Append(tint4);

            solidFill11.Append(schemeColor12);

            fillProperties4.Append(solidFill11);

            tableCellStyle6.Append(tableCellBorders6);
            tableCellStyle6.Append(fillProperties4);

            lastRow1.Append(tableCellTextStyle4);
            lastRow1.Append(tableCellStyle6);

            FirstRow firstRow1 = new FirstRow();
            TableCellTextStyle tableCellTextStyle5 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            TableCellStyle tableCellStyle7 = new TableCellStyle();
            TableCellBorders tableCellBorders7 = new TableCellBorders();

            FillProperties fillProperties5 = new FillProperties();

            SolidFill solidFill12 = new SolidFill();

            SchemeColor schemeColor13 = new SchemeColor() { Val = SchemeColorValues.Accent1 };
            Tint tint5 = new Tint() { Val = 20000 };

            schemeColor13.Append(tint5);

            solidFill12.Append(schemeColor13);

            fillProperties5.Append(solidFill12);

            tableCellStyle7.Append(tableCellBorders7);
            tableCellStyle7.Append(fillProperties5);

            firstRow1.Append(tableCellTextStyle5);
            firstRow1.Append(tableCellStyle7);

            tableStyleEntry1.Append(wholeTable1);
            tableStyleEntry1.Append(band1Horizontal1);
            tableStyleEntry1.Append(band1Vertical1);
            tableStyleEntry1.Append(lastColumn1);
            tableStyleEntry1.Append(firstColumn1);
            tableStyleEntry1.Append(lastRow1);
            tableStyleEntry1.Append(firstRow1);
            return tableStyleEntry1;
        }


    }
}
