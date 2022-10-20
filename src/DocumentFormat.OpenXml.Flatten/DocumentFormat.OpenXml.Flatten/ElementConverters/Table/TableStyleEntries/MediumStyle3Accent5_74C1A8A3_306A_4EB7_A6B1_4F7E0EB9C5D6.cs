using System.CodeDom.Compiler;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries
{
    [GeneratedCode("OpenXmlSdkTool", "2.5")]
    internal static class MediumStyle3Accent5_74C1A8A3_306A_4EB7_A6B1_4F7E0EB9C5D6
    {
        // Creates an TableStyleEntry instance and adds its children.
        public static TableStyleEntry GenerateTableStyleEntry()
        {
            TableStyleEntry tableStyleEntry1 = new TableStyleEntry() { StyleId = "{74C1A8A3-306A-4EB7-A6B1-4F7E0EB9C5D6}", StyleName = "中度样式 3 - 强调 5" };

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

            Outline outline3 = new Outline() { Width = 25400, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill1 = new SolidFill();
            SchemeColor schemeColor2 = new SchemeColor() { Val = SchemeColorValues.Dark1 };

            solidFill1.Append(schemeColor2);

            outline3.Append(solidFill1);

            topBorder1.Append(outline3);

            BottomBorder bottomBorder1 = new BottomBorder();

            Outline outline4 = new Outline() { Width = 25400, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill2 = new SolidFill();
            SchemeColor schemeColor3 = new SchemeColor() { Val = SchemeColorValues.Dark1 };

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

            SolidFill solidFill3 = new SolidFill();
            SchemeColor schemeColor4 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill3.Append(schemeColor4);

            fillProperties1.Append(solidFill3);

            tableCellStyle1.Append(tableCellBorders1);
            tableCellStyle1.Append(fillProperties1);

            wholeTable1.Append(tableCellTextStyle1);
            wholeTable1.Append(tableCellStyle1);

            Band1Horizontal band1Horizontal1 = new Band1Horizontal();

            TableCellStyle tableCellStyle2 = new TableCellStyle();
            TableCellBorders tableCellBorders2 = new TableCellBorders();

            FillProperties fillProperties2 = new FillProperties();

            SolidFill solidFill4 = new SolidFill();

            SchemeColor schemeColor5 = new SchemeColor() { Val = SchemeColorValues.Dark1 };
            Tint tint1 = new Tint() { Val = 20000 };

            schemeColor5.Append(tint1);

            solidFill4.Append(schemeColor5);

            fillProperties2.Append(solidFill4);

            tableCellStyle2.Append(tableCellBorders2);
            tableCellStyle2.Append(fillProperties2);

            band1Horizontal1.Append(tableCellStyle2);

            Band1Vertical band1Vertical1 = new Band1Vertical();

            TableCellStyle tableCellStyle3 = new TableCellStyle();
            TableCellBorders tableCellBorders3 = new TableCellBorders();

            FillProperties fillProperties3 = new FillProperties();

            SolidFill solidFill5 = new SolidFill();

            SchemeColor schemeColor6 = new SchemeColor() { Val = SchemeColorValues.Dark1 };
            Tint tint2 = new Tint() { Val = 20000 };

            schemeColor6.Append(tint2);

            solidFill5.Append(schemeColor6);

            fillProperties3.Append(solidFill5);

            tableCellStyle3.Append(tableCellBorders3);
            tableCellStyle3.Append(fillProperties3);

            band1Vertical1.Append(tableCellStyle3);

            LastColumn lastColumn1 = new LastColumn();

            TableCellTextStyle tableCellTextStyle2 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            FontReference fontReference2 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            RgbColorModelPercentage rgbColorModelPercentage2 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

            fontReference2.Append(rgbColorModelPercentage2);
            SchemeColor schemeColor7 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            tableCellTextStyle2.Append(fontReference2);
            tableCellTextStyle2.Append(schemeColor7);

            TableCellStyle tableCellStyle4 = new TableCellStyle();
            TableCellBorders tableCellBorders4 = new TableCellBorders();

            FillProperties fillProperties4 = new FillProperties();

            SolidFill solidFill6 = new SolidFill();
            SchemeColor schemeColor8 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

            solidFill6.Append(schemeColor8);

            fillProperties4.Append(solidFill6);

            tableCellStyle4.Append(tableCellBorders4);
            tableCellStyle4.Append(fillProperties4);

            lastColumn1.Append(tableCellTextStyle2);
            lastColumn1.Append(tableCellStyle4);

            FirstColumn firstColumn1 = new FirstColumn();

            TableCellTextStyle tableCellTextStyle3 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            FontReference fontReference3 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            RgbColorModelPercentage rgbColorModelPercentage3 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

            fontReference3.Append(rgbColorModelPercentage3);
            SchemeColor schemeColor9 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            tableCellTextStyle3.Append(fontReference3);
            tableCellTextStyle3.Append(schemeColor9);

            TableCellStyle tableCellStyle5 = new TableCellStyle();
            TableCellBorders tableCellBorders5 = new TableCellBorders();

            FillProperties fillProperties5 = new FillProperties();

            SolidFill solidFill7 = new SolidFill();
            SchemeColor schemeColor10 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

            solidFill7.Append(schemeColor10);

            fillProperties5.Append(solidFill7);

            tableCellStyle5.Append(tableCellBorders5);
            tableCellStyle5.Append(fillProperties5);

            firstColumn1.Append(tableCellTextStyle3);
            firstColumn1.Append(tableCellStyle5);

            LastRow lastRow1 = new LastRow();
            TableCellTextStyle tableCellTextStyle4 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            TableCellStyle tableCellStyle6 = new TableCellStyle();

            TableCellBorders tableCellBorders6 = new TableCellBorders();

            TopBorder topBorder2 = new TopBorder();

            Outline outline7 = new Outline() { Width = 50800, CompoundLineType = CompoundLineValues.Double };

            SolidFill solidFill8 = new SolidFill();
            SchemeColor schemeColor11 = new SchemeColor() { Val = SchemeColorValues.Dark1 };

            solidFill8.Append(schemeColor11);

            outline7.Append(solidFill8);

            topBorder2.Append(outline7);

            tableCellBorders6.Append(topBorder2);

            FillProperties fillProperties6 = new FillProperties();

            SolidFill solidFill9 = new SolidFill();
            SchemeColor schemeColor12 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill9.Append(schemeColor12);

            fillProperties6.Append(solidFill9);

            tableCellStyle6.Append(tableCellBorders6);
            tableCellStyle6.Append(fillProperties6);

            lastRow1.Append(tableCellTextStyle4);
            lastRow1.Append(tableCellStyle6);

            SoutheastCell southeastCell1 = new SoutheastCell();

            TableCellTextStyle tableCellTextStyle5 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            FontReference fontReference4 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            RgbColorModelPercentage rgbColorModelPercentage4 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

            fontReference4.Append(rgbColorModelPercentage4);
            SchemeColor schemeColor13 = new SchemeColor() { Val = SchemeColorValues.Dark1 };

            tableCellTextStyle5.Append(fontReference4);
            tableCellTextStyle5.Append(schemeColor13);

            TableCellStyle tableCellStyle7 = new TableCellStyle();
            TableCellBorders tableCellBorders7 = new TableCellBorders();

            tableCellStyle7.Append(tableCellBorders7);

            southeastCell1.Append(tableCellTextStyle5);
            southeastCell1.Append(tableCellStyle7);

            SouthwestCell southwestCell1 = new SouthwestCell();

            TableCellTextStyle tableCellTextStyle6 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            FontReference fontReference5 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            RgbColorModelPercentage rgbColorModelPercentage5 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

            fontReference5.Append(rgbColorModelPercentage5);
            SchemeColor schemeColor14 = new SchemeColor() { Val = SchemeColorValues.Dark1 };

            tableCellTextStyle6.Append(fontReference5);
            tableCellTextStyle6.Append(schemeColor14);

            TableCellStyle tableCellStyle8 = new TableCellStyle();
            TableCellBorders tableCellBorders8 = new TableCellBorders();

            tableCellStyle8.Append(tableCellBorders8);

            southwestCell1.Append(tableCellTextStyle6);
            southwestCell1.Append(tableCellStyle8);

            FirstRow firstRow1 = new FirstRow();

            TableCellTextStyle tableCellTextStyle7 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            FontReference fontReference6 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            RgbColorModelPercentage rgbColorModelPercentage6 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

            fontReference6.Append(rgbColorModelPercentage6);
            SchemeColor schemeColor15 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            tableCellTextStyle7.Append(fontReference6);
            tableCellTextStyle7.Append(schemeColor15);

            TableCellStyle tableCellStyle9 = new TableCellStyle();

            TableCellBorders tableCellBorders9 = new TableCellBorders();

            BottomBorder bottomBorder2 = new BottomBorder();

            Outline outline8 = new Outline() { Width = 25400, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill10 = new SolidFill();
            SchemeColor schemeColor16 = new SchemeColor() { Val = SchemeColorValues.Dark1 };

            solidFill10.Append(schemeColor16);

            outline8.Append(solidFill10);

            bottomBorder2.Append(outline8);

            tableCellBorders9.Append(bottomBorder2);

            FillProperties fillProperties7 = new FillProperties();

            SolidFill solidFill11 = new SolidFill();
            SchemeColor schemeColor17 = new SchemeColor() { Val = SchemeColorValues.Accent5 };

            solidFill11.Append(schemeColor17);

            fillProperties7.Append(solidFill11);

            tableCellStyle9.Append(tableCellBorders9);
            tableCellStyle9.Append(fillProperties7);

            firstRow1.Append(tableCellTextStyle7);
            firstRow1.Append(tableCellStyle9);

            tableStyleEntry1.Append(wholeTable1);
            tableStyleEntry1.Append(band1Horizontal1);
            tableStyleEntry1.Append(band1Vertical1);
            tableStyleEntry1.Append(lastColumn1);
            tableStyleEntry1.Append(firstColumn1);
            tableStyleEntry1.Append(lastRow1);
            tableStyleEntry1.Append(southeastCell1);
            tableStyleEntry1.Append(southwestCell1);
            tableStyleEntry1.Append(firstRow1);
            return tableStyleEntry1;
        }


    }
}
