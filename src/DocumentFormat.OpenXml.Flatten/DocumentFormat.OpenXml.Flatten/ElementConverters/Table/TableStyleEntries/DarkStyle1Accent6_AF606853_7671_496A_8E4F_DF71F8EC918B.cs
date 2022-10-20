using System.CodeDom.Compiler;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries
{
    [GeneratedCode("OpenXmlSdkTool", "2.5")]
    internal static class DarkStyle1Accent6_AF606853_7671_496A_8E4F_DF71F8EC918B
    {
        // Creates an TableStyleEntry instance and adds its children.
        public static TableStyleEntry GenerateTableStyleEntry()
        {
            TableStyleEntry tableStyleEntry1 = new TableStyleEntry() { StyleId = "{AF606853-7671-496A-8E4F-DF71F8EC918B}", StyleName = "深色样式 1 - 强调 6" };

            WholeTable wholeTable1 = new WholeTable();

            TableCellTextStyle tableCellTextStyle1 = new TableCellTextStyle();

            FontReference fontReference1 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            RgbColorModelPercentage rgbColorModelPercentage1 = new RgbColorModelPercentage() { RedPortion = 0, GreenPortion = 0, BluePortion = 0 };

            fontReference1.Append(rgbColorModelPercentage1);
            SchemeColor schemeColor1 = new SchemeColor() { Val = SchemeColorValues.Light1 };

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

            SolidFill solidFill1 = new SolidFill();
            SchemeColor schemeColor2 = new SchemeColor() { Val = SchemeColorValues.Accent6 };

            solidFill1.Append(schemeColor2);

            fillProperties1.Append(solidFill1);

            tableCellStyle1.Append(tableCellBorders1);
            tableCellStyle1.Append(fillProperties1);

            wholeTable1.Append(tableCellTextStyle1);
            wholeTable1.Append(tableCellStyle1);

            Band1Horizontal band1Horizontal1 = new Band1Horizontal();

            TableCellStyle tableCellStyle2 = new TableCellStyle();
            TableCellBorders tableCellBorders2 = new TableCellBorders();

            FillProperties fillProperties2 = new FillProperties();

            SolidFill solidFill2 = new SolidFill();

            SchemeColor schemeColor3 = new SchemeColor() { Val = SchemeColorValues.Accent6 };
            Shade shade1 = new Shade() { Val = 60000 };

            schemeColor3.Append(shade1);

            solidFill2.Append(schemeColor3);

            fillProperties2.Append(solidFill2);

            tableCellStyle2.Append(tableCellBorders2);
            tableCellStyle2.Append(fillProperties2);

            band1Horizontal1.Append(tableCellStyle2);

            Band1Vertical band1Vertical1 = new Band1Vertical();

            TableCellStyle tableCellStyle3 = new TableCellStyle();
            TableCellBorders tableCellBorders3 = new TableCellBorders();

            FillProperties fillProperties3 = new FillProperties();

            SolidFill solidFill3 = new SolidFill();

            SchemeColor schemeColor4 = new SchemeColor() { Val = SchemeColorValues.Accent6 };
            Shade shade2 = new Shade() { Val = 60000 };

            schemeColor4.Append(shade2);

            solidFill3.Append(schemeColor4);

            fillProperties3.Append(solidFill3);

            tableCellStyle3.Append(tableCellBorders3);
            tableCellStyle3.Append(fillProperties3);

            band1Vertical1.Append(tableCellStyle3);

            LastColumn lastColumn1 = new LastColumn();
            TableCellTextStyle tableCellTextStyle2 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            TableCellStyle tableCellStyle4 = new TableCellStyle();

            TableCellBorders tableCellBorders4 = new TableCellBorders();

            LeftBorder leftBorder2 = new LeftBorder();

            Outline outline7 = new Outline() { Width = 25400, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill4 = new SolidFill();
            SchemeColor schemeColor5 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill4.Append(schemeColor5);

            outline7.Append(solidFill4);

            leftBorder2.Append(outline7);

            tableCellBorders4.Append(leftBorder2);

            FillProperties fillProperties4 = new FillProperties();

            SolidFill solidFill5 = new SolidFill();

            SchemeColor schemeColor6 = new SchemeColor() { Val = SchemeColorValues.Accent6 };
            Shade shade3 = new Shade() { Val = 60000 };

            schemeColor6.Append(shade3);

            solidFill5.Append(schemeColor6);

            fillProperties4.Append(solidFill5);

            tableCellStyle4.Append(tableCellBorders4);
            tableCellStyle4.Append(fillProperties4);

            lastColumn1.Append(tableCellTextStyle2);
            lastColumn1.Append(tableCellStyle4);

            FirstColumn firstColumn1 = new FirstColumn();
            TableCellTextStyle tableCellTextStyle3 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            TableCellStyle tableCellStyle5 = new TableCellStyle();

            TableCellBorders tableCellBorders5 = new TableCellBorders();

            RightBorder rightBorder2 = new RightBorder();

            Outline outline8 = new Outline() { Width = 25400, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill6 = new SolidFill();
            SchemeColor schemeColor7 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill6.Append(schemeColor7);

            outline8.Append(solidFill6);

            rightBorder2.Append(outline8);

            tableCellBorders5.Append(rightBorder2);

            FillProperties fillProperties5 = new FillProperties();

            SolidFill solidFill7 = new SolidFill();

            SchemeColor schemeColor8 = new SchemeColor() { Val = SchemeColorValues.Accent6 };
            Shade shade4 = new Shade() { Val = 60000 };

            schemeColor8.Append(shade4);

            solidFill7.Append(schemeColor8);

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

            Outline outline9 = new Outline() { Width = 25400, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill8 = new SolidFill();
            SchemeColor schemeColor9 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill8.Append(schemeColor9);

            outline9.Append(solidFill8);

            topBorder2.Append(outline9);

            tableCellBorders6.Append(topBorder2);

            FillProperties fillProperties6 = new FillProperties();

            SolidFill solidFill9 = new SolidFill();

            SchemeColor schemeColor10 = new SchemeColor() { Val = SchemeColorValues.Accent6 };
            Shade shade5 = new Shade() { Val = 40000 };

            schemeColor10.Append(shade5);

            solidFill9.Append(schemeColor10);

            fillProperties6.Append(solidFill9);

            tableCellStyle6.Append(tableCellBorders6);
            tableCellStyle6.Append(fillProperties6);

            lastRow1.Append(tableCellTextStyle4);
            lastRow1.Append(tableCellStyle6);

            SoutheastCell southeastCell1 = new SoutheastCell();

            TableCellStyle tableCellStyle7 = new TableCellStyle();

            TableCellBorders tableCellBorders7 = new TableCellBorders();

            LeftBorder leftBorder3 = new LeftBorder();

            Outline outline10 = new Outline();
            NoFill noFill7 = new NoFill();

            outline10.Append(noFill7);

            leftBorder3.Append(outline10);

            tableCellBorders7.Append(leftBorder3);

            tableCellStyle7.Append(tableCellBorders7);

            southeastCell1.Append(tableCellStyle7);

            SouthwestCell southwestCell1 = new SouthwestCell();

            TableCellStyle tableCellStyle8 = new TableCellStyle();

            TableCellBorders tableCellBorders8 = new TableCellBorders();

            RightBorder rightBorder3 = new RightBorder();

            Outline outline11 = new Outline();
            NoFill noFill8 = new NoFill();

            outline11.Append(noFill8);

            rightBorder3.Append(outline11);

            tableCellBorders8.Append(rightBorder3);

            tableCellStyle8.Append(tableCellBorders8);

            southwestCell1.Append(tableCellStyle8);

            FirstRow firstRow1 = new FirstRow();
            TableCellTextStyle tableCellTextStyle5 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            TableCellStyle tableCellStyle9 = new TableCellStyle();

            TableCellBorders tableCellBorders9 = new TableCellBorders();

            BottomBorder bottomBorder2 = new BottomBorder();

            Outline outline12 = new Outline() { Width = 25400, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill10 = new SolidFill();
            SchemeColor schemeColor11 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill10.Append(schemeColor11);

            outline12.Append(solidFill10);

            bottomBorder2.Append(outline12);

            tableCellBorders9.Append(bottomBorder2);

            FillProperties fillProperties7 = new FillProperties();

            SolidFill solidFill11 = new SolidFill();
            SchemeColor schemeColor12 = new SchemeColor() { Val = SchemeColorValues.Dark1 };

            solidFill11.Append(schemeColor12);

            fillProperties7.Append(solidFill11);

            tableCellStyle9.Append(tableCellBorders9);
            tableCellStyle9.Append(fillProperties7);

            firstRow1.Append(tableCellTextStyle5);
            firstRow1.Append(tableCellStyle9);

            NortheastCell northeastCell1 = new NortheastCell();

            TableCellStyle tableCellStyle10 = new TableCellStyle();

            TableCellBorders tableCellBorders10 = new TableCellBorders();

            LeftBorder leftBorder4 = new LeftBorder();

            Outline outline13 = new Outline();
            NoFill noFill9 = new NoFill();

            outline13.Append(noFill9);

            leftBorder4.Append(outline13);

            tableCellBorders10.Append(leftBorder4);

            tableCellStyle10.Append(tableCellBorders10);

            northeastCell1.Append(tableCellStyle10);

            NorthwestCell northwestCell1 = new NorthwestCell();

            TableCellStyle tableCellStyle11 = new TableCellStyle();

            TableCellBorders tableCellBorders11 = new TableCellBorders();

            RightBorder rightBorder4 = new RightBorder();

            Outline outline14 = new Outline();
            NoFill noFill10 = new NoFill();

            outline14.Append(noFill10);

            rightBorder4.Append(outline14);

            tableCellBorders11.Append(rightBorder4);

            tableCellStyle11.Append(tableCellBorders11);

            northwestCell1.Append(tableCellStyle11);

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
            tableStyleEntry1.Append(northwestCell1);
            return tableStyleEntry1;
        }


    }
}
