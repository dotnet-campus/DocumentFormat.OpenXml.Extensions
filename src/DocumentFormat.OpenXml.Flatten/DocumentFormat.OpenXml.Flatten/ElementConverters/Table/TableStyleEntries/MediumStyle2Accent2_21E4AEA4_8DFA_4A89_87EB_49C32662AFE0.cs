using System.CodeDom.Compiler;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries
{
    [GeneratedCode("OpenXmlSdkTool", "2.5")]
    internal static class MediumStyle2Accent2_21E4AEA4_8DFA_4A89_87EB_49C32662AFE0
    {
        // Creates an TableStyleEntry instance and adds its children.
        public static TableStyleEntry GenerateTableStyleEntry()
        {
            TableStyleEntry tableStyleEntry1 = new TableStyleEntry() { StyleId = "{21E4AEA4-8DFA-4A89-87EB-49C32662AFE0}", StyleName = "中度样式 2 - 强调 2" };

            WholeTable wholeTable1 = new WholeTable();

            TableCellTextStyle tableCellTextStyle1 = new TableCellTextStyle();

            FontReference fontReference1 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            PresetColor presetColor1 = new PresetColor() { Val = PresetColorValues.Black };

            fontReference1.Append(presetColor1);
            SchemeColor schemeColor1 = new SchemeColor() { Val = SchemeColorValues.Dark1 };

            tableCellTextStyle1.Append(fontReference1);
            tableCellTextStyle1.Append(schemeColor1);

            TableCellStyle tableCellStyle1 = new TableCellStyle();

            TableCellBorders tableCellBorders1 = new TableCellBorders();

            LeftBorder leftBorder1 = new LeftBorder();

            Outline outline1 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill1 = new SolidFill();
            SchemeColor schemeColor2 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill1.Append(schemeColor2);

            outline1.Append(solidFill1);

            leftBorder1.Append(outline1);

            RightBorder rightBorder1 = new RightBorder();

            Outline outline2 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill2 = new SolidFill();
            SchemeColor schemeColor3 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill2.Append(schemeColor3);

            outline2.Append(solidFill2);

            rightBorder1.Append(outline2);

            TopBorder topBorder1 = new TopBorder();

            Outline outline3 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill3 = new SolidFill();
            SchemeColor schemeColor4 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill3.Append(schemeColor4);

            outline3.Append(solidFill3);

            topBorder1.Append(outline3);

            BottomBorder bottomBorder1 = new BottomBorder();

            Outline outline4 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill4 = new SolidFill();
            SchemeColor schemeColor5 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill4.Append(schemeColor5);

            outline4.Append(solidFill4);

            bottomBorder1.Append(outline4);

            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder();

            Outline outline5 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill5 = new SolidFill();
            SchemeColor schemeColor6 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill5.Append(schemeColor6);

            outline5.Append(solidFill5);

            insideHorizontalBorder1.Append(outline5);

            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder();

            Outline outline6 = new Outline() { Width = 12700, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill6 = new SolidFill();
            SchemeColor schemeColor7 = new SchemeColor() { Val = SchemeColorValues.Light1 };

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

            SchemeColor schemeColor8 = new SchemeColor() { Val = SchemeColorValues.Accent2 };
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

            SchemeColor schemeColor9 = new SchemeColor() { Val = SchemeColorValues.Accent2 };
            Tint tint2 = new Tint() { Val = 40000 };

            schemeColor9.Append(tint2);

            solidFill8.Append(schemeColor9);

            fillProperties2.Append(solidFill8);

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

            SolidFill solidFill9 = new SolidFill();

            SchemeColor schemeColor10 = new SchemeColor() { Val = SchemeColorValues.Accent2 };
            Tint tint3 = new Tint() { Val = 40000 };

            schemeColor10.Append(tint3);

            solidFill9.Append(schemeColor10);

            fillProperties3.Append(solidFill9);

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

            FontReference fontReference2 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            PresetColor presetColor2 = new PresetColor() { Val = PresetColorValues.Black };

            fontReference2.Append(presetColor2);
            SchemeColor schemeColor11 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            tableCellTextStyle2.Append(fontReference2);
            tableCellTextStyle2.Append(schemeColor11);

            TableCellStyle tableCellStyle6 = new TableCellStyle();
            TableCellBorders tableCellBorders6 = new TableCellBorders();

            FillProperties fillProperties4 = new FillProperties();

            SolidFill solidFill10 = new SolidFill();
            SchemeColor schemeColor12 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

            solidFill10.Append(schemeColor12);

            fillProperties4.Append(solidFill10);

            tableCellStyle6.Append(tableCellBorders6);
            tableCellStyle6.Append(fillProperties4);

            lastColumn1.Append(tableCellTextStyle2);
            lastColumn1.Append(tableCellStyle6);

            FirstColumn firstColumn1 = new FirstColumn();

            TableCellTextStyle tableCellTextStyle3 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            FontReference fontReference3 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            PresetColor presetColor3 = new PresetColor() { Val = PresetColorValues.Black };

            fontReference3.Append(presetColor3);
            SchemeColor schemeColor13 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            tableCellTextStyle3.Append(fontReference3);
            tableCellTextStyle3.Append(schemeColor13);

            TableCellStyle tableCellStyle7 = new TableCellStyle();
            TableCellBorders tableCellBorders7 = new TableCellBorders();

            FillProperties fillProperties5 = new FillProperties();

            SolidFill solidFill11 = new SolidFill();
            SchemeColor schemeColor14 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

            solidFill11.Append(schemeColor14);

            fillProperties5.Append(solidFill11);

            tableCellStyle7.Append(tableCellBorders7);
            tableCellStyle7.Append(fillProperties5);

            firstColumn1.Append(tableCellTextStyle3);
            firstColumn1.Append(tableCellStyle7);

            LastRow lastRow1 = new LastRow();

            TableCellTextStyle tableCellTextStyle4 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            FontReference fontReference4 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            PresetColor presetColor4 = new PresetColor() { Val = PresetColorValues.Black };

            fontReference4.Append(presetColor4);
            SchemeColor schemeColor15 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            tableCellTextStyle4.Append(fontReference4);
            tableCellTextStyle4.Append(schemeColor15);

            TableCellStyle tableCellStyle8 = new TableCellStyle();

            TableCellBorders tableCellBorders8 = new TableCellBorders();

            TopBorder topBorder2 = new TopBorder();

            Outline outline7 = new Outline() { Width = 38100, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill12 = new SolidFill();
            SchemeColor schemeColor16 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill12.Append(schemeColor16);

            outline7.Append(solidFill12);

            topBorder2.Append(outline7);

            tableCellBorders8.Append(topBorder2);

            FillProperties fillProperties6 = new FillProperties();

            SolidFill solidFill13 = new SolidFill();
            SchemeColor schemeColor17 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

            solidFill13.Append(schemeColor17);

            fillProperties6.Append(solidFill13);

            tableCellStyle8.Append(tableCellBorders8);
            tableCellStyle8.Append(fillProperties6);

            lastRow1.Append(tableCellTextStyle4);
            lastRow1.Append(tableCellStyle8);

            FirstRow firstRow1 = new FirstRow();

            TableCellTextStyle tableCellTextStyle5 = new TableCellTextStyle() { Bold = BooleanStyleValues.On };

            FontReference fontReference5 = new FontReference() { Index = FontCollectionIndexValues.Minor };
            PresetColor presetColor5 = new PresetColor() { Val = PresetColorValues.Black };

            fontReference5.Append(presetColor5);
            SchemeColor schemeColor18 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            tableCellTextStyle5.Append(fontReference5);
            tableCellTextStyle5.Append(schemeColor18);

            TableCellStyle tableCellStyle9 = new TableCellStyle();

            TableCellBorders tableCellBorders9 = new TableCellBorders();

            BottomBorder bottomBorder2 = new BottomBorder();

            Outline outline8 = new Outline() { Width = 38100, CompoundLineType = CompoundLineValues.Single };

            SolidFill solidFill14 = new SolidFill();
            SchemeColor schemeColor19 = new SchemeColor() { Val = SchemeColorValues.Light1 };

            solidFill14.Append(schemeColor19);

            outline8.Append(solidFill14);

            bottomBorder2.Append(outline8);

            tableCellBorders9.Append(bottomBorder2);

            FillProperties fillProperties7 = new FillProperties();

            SolidFill solidFill15 = new SolidFill();
            SchemeColor schemeColor20 = new SchemeColor() { Val = SchemeColorValues.Accent2 };

            solidFill15.Append(schemeColor20);

            fillProperties7.Append(solidFill15);

            tableCellStyle9.Append(tableCellBorders9);
            tableCellStyle9.Append(fillProperties7);

            firstRow1.Append(tableCellTextStyle5);
            firstRow1.Append(tableCellStyle9);

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
}
