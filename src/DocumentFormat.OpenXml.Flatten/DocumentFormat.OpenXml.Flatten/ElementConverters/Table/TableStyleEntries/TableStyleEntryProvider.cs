using System;
using System.IO;
using System.Text.RegularExpressions;

using DocumentFormat.OpenXml.Drawing;

using Path = System.IO.Path;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.TableStyleEntries
{
    /// <summary>
    /// 表格样式提供器
    /// </summary>
    public static class TableStyleEntryProvider
    {
        /// <summary>
        /// 尝试创建表格样式，如果给定的 <paramref name="styleId"/> 无法找到对应的创建器，将返回空值
        /// </summary>
        /// <param name="styleId"></param>
        /// <returns></returns>
        /// 此代码是生成代码，通过 OpenXmlSdkTool.exe 生成，工具版本 V2.5。
        /// 生成代码方法请看 <see cref="GenerateTableStyleEntriesCode"/>
        /// 文档： MS-OE376 的 2.1.1343 Part 4 Section 5.1.6.10, tableStyleId (Table Style ID
        /// 文档里面有各个样式的对应值
        public static TableStyleEntry? CreateTableStyleEntry(string styleId)
        {
            return styleId switch
            {
                "{2D5ABB26-0587-4C30-8999-92F81FD0307C}" => NoStyleNoGrid_2D5ABB26_0587_4C30_8999_92F81FD0307C.GenerateTableStyleEntry(),

                "{284E427A-3D55-4303-BF80-6455036E1DE7}" => ThemedStyle1Accent2_284E427A_3D55_4303_BF80_6455036E1DE7.GenerateTableStyleEntry(),
                "{69C7853C-536D-4A76-A0AE-DD22124D55A5}" => ThemedStyle1Accent3_69C7853C_536D_4A76_A0AE_DD22124D55A5.GenerateTableStyleEntry(),
                "{775DCB02-9BB8-47FD-8907-85C794F793BA}" => ThemedStyle1Accent4_775DCB02_9BB8_47FD_8907_85C794F793BA.GenerateTableStyleEntry(),
                "{35758FB7-9AC5-4552-8A53-C91805E547FA}" => ThemedStyle1Accent5_35758FB7_9AC5_4552_8A53_C91805E547FA.GenerateTableStyleEntry(),
                "{08FB837D-C827-4EFA-A057-4D05807E0F7C}" => ThemedStyle1Accent6_08FB837D_C827_4EFA_A057_4D05807E0F7C.GenerateTableStyleEntry(),

                "{5940675A-B579-460E-94D1-54222C63F5DA}" => NoStyleTableGrid_5940675A_B579_460E_94D1_54222C63F5DA.GenerateTableStyleEntry(),

                "{D113A9D2-9D6B-4929-AA2D-F23B5EE8CBE7}" => ThemedStyle2Accent1_D113A9D2_9D6B_4929_AA2D_F23B5EE8CBE7.GenerateTableStyleEntry(),
                "{18603FDC-E32A-4AB5-989C-0864C3EAD2B8}" => ThemedStyle2Accent2_18603FDC_E32A_4AB5_989C_0864C3EAD2B8.GenerateTableStyleEntry(),
                "{306799F8-075E-4A3A-A7F6-7FBC6576F1A4}" => ThemedStyle2Accent3_306799F8_075E_4A3A_A7F6_7FBC6576F1A4.GenerateTableStyleEntry(),
                "{E269D01E-BC32-4049-B463-5C60D7B0CCD2}" => ThemedStyle2Accent4_E269D01E_BC32_4049_B463_5C60D7B0CCD2.GenerateTableStyleEntry(),
                "{327F97BB-C833-4FB7-BDE5-3F7075034690}" => ThemedStyle2Accent5_327F97BB_C833_4FB7_BDE5_3F7075034690.GenerateTableStyleEntry(),
                "{638B1855-1B75-4FBE-930C-398BA8C253C6}" => ThemedStyle2Accent6_638B1855_1B75_4FBE_930C_398BA8C253C6.GenerateTableStyleEntry(),

                "{9D7B26C5-4107-4FEC-AEDC-1716B250A1EF}" => LightStyle1_9D7B26C5_4107_4FEC_AEDC_1716B250A1EF.GenerateTableStyleEntry(),
                "{3B4B98B0-60AC-42C2-AFA5-B58CD77FA1E5}" => LightStyle1Accent1_3B4B98B0_60AC_42C2_AFA5_B58CD77FA1E5.GenerateTableStyleEntry(),
                "{0E3FDE45-AF77-4B5C-9715-49D594BDF05E}" => LightStyle1Accent2_0E3FDE45_AF77_4B5C_9715_49D594BDF05E.GenerateTableStyleEntry(),
                "{C083E6E3-FA7D-4D7B-A595-EF9225AFEA82}" => LightStyle1Accent3_C083E6E3_FA7D_4D7B_A595_EF9225AFEA82.GenerateTableStyleEntry(),
                "{D27102A9-8310-4765-A935-A1911B00CA55}" => LightStyle1Accent4_D27102A9_8310_4765_A935_A1911B00CA55.GenerateTableStyleEntry(),
                "{5FD0F851-EC5A-4D38-B0AD-8093EC10F338}" => LightStyle1Accent5_5FD0F851_EC5A_4D38_B0AD_8093EC10F338.GenerateTableStyleEntry(),
                "{68D230F3-CF80-4859-8CE7-A43EE81993B5}" => LightStyle1Accent6_68D230F3_CF80_4859_8CE7_A43EE81993B5.GenerateTableStyleEntry(),

                "{7E9639D4-E3E2-4D34-9284-5A2195B3D0D7}" => LightStyle2_7E9639D4_E3E2_4D34_9284_5A2195B3D0D7.GenerateTableStyleEntry(),
                "{69012ECD-51FC-41F1-AA8D-1B2483CD663E}" => LightStyle2Accent1_69012ECD_51FC_41F1_AA8D_1B2483CD663E.GenerateTableStyleEntry(),
                "{72833802-FEF1-4C79-8D5D-14CF1EAF98D9}" => LightStyle2Accent2_72833802_FEF1_4C79_8D5D_14CF1EAF98D9.GenerateTableStyleEntry(),
                "{F2DE63D5-997A-4646-A377-4702673A728D}" => LightStyle2Accent3_F2DE63D5_997A_4646_A377_4702673A728D.GenerateTableStyleEntry(),
                "{17292A2E-F333-43FB-9621-5CBBE7FDCDCB}" => LightStyle2Accent4_17292A2E_F333_43FB_9621_5CBBE7FDCDCB.GenerateTableStyleEntry(),
                "{5A111915-BE36-4E01-A7E5-04B1672EAD32}" => LightStyle2Accent5_5A111915_BE36_4E01_A7E5_04B1672EAD32.GenerateTableStyleEntry(),
                "{912C8C85-51F0-491E-9774-3900AFEF0FD7}" => LightStyle2Accent6_912C8C85_51F0_491E_9774_3900AFEF0FD7.GenerateTableStyleEntry(),

                "{616DA210-FB5B-4158-B5E0-FEB733F419BA}" => LightStyle3_616DA210_FB5B_4158_B5E0_FEB733F419BA.GenerateTableStyleEntry(),
                "{BC89EF96-8CEA-46FF-86C4-4CE0E7609802}" => LightStyle3Accent1_BC89EF96_8CEA_46FF_86C4_4CE0E7609802.GenerateTableStyleEntry(),
                "{5DA37D80-6434-44D0-A028-1B22A696006F}" => LightStyle3Accent2_5DA37D80_6434_44D0_A028_1B22A696006F.GenerateTableStyleEntry(),
                "{8799B23B-EC83-4686-B30A-512413B5E67A}" => LightStyle3Accent3_8799B23B_EC83_4686_B30A_512413B5E67A.GenerateTableStyleEntry(),
                "{ED083AE6-46FA-4A59-8FB0-9F97EB10719F}" => LightStyle3Accent4_ED083AE6_46FA_4A59_8FB0_9F97EB10719F.GenerateTableStyleEntry(),
                "{BDBED569-4797-4DF1-A0F4-6AAB3CD982D8}" => LightStyle3Accent5_BDBED569_4797_4DF1_A0F4_6AAB3CD982D8.GenerateTableStyleEntry(),
                "{E8B1032C-EA38-4F05-BA0D-38AFFFC7BED3}" => LightStyle3Accent6_E8B1032C_EA38_4F05_BA0D_38AFFFC7BED3.GenerateTableStyleEntry(),

                "{793D81CF-94F2-401A-BA57-92F5A7B2D0C5}" => MediumStyle1_793D81CF_94F2_401A_BA57_92F5A7B2D0C5.GenerateTableStyleEntry(),
                "{B301B821-A1FF-4177-AEE7-76D212191A09}" => MediumStyle1Accent1_B301B821_A1FF_4177_AEE7_76D212191A09.GenerateTableStyleEntry(),
                "{9DCAF9ED-07DC-4A11-8D7F-57B35C25682E}" => MediumStyle1Accent2_9DCAF9ED_07DC_4A11_8D7F_57B35C25682E.GenerateTableStyleEntry(),
                "{1FECB4D8-DB02-4DC6-A0A2-4F2EBAE1DC90}" => MediumStyle1Accent3_1FECB4D8_DB02_4DC6_A0A2_4F2EBAE1DC90.GenerateTableStyleEntry(),
                "{1E171933-4619-4E11-9A3F-F7608DF75F80}" => MediumStyle1Accent4_1E171933_4619_4E11_9A3F_F7608DF75F80.GenerateTableStyleEntry(),
                "{FABFCF23-3B69-468F-B69F-88F6DE6A72F2}" => MediumStyle1Accent5_FABFCF23_3B69_468F_B69F_88F6DE6A72F2.GenerateTableStyleEntry(),
                "{10A1B5D5-9B99-4C35-A422-299274C87663}" => MediumStyle1Accent6_10A1B5D5_9B99_4C35_A422_299274C87663.GenerateTableStyleEntry(),

                "{073A0DAA-6AF3-43AB-8588-CEC1D06C72B9}" => MediumStyle2_073A0DAA_6AF3_43AB_8588_CEC1D06C72B9.GenerateTableStyleEntry(),
                "{5C22544A-7EE6-4342-B048-85BDC9FD1C3A}" => MediumStyle2Accent1_5C22544A_7EE6_4342_B048_85BDC9FD1C3A.GenerateTableStyleEntry(),
                "{21E4AEA4-8DFA-4A89-87EB-49C32662AFE0}" => MediumStyle2Accent2_21E4AEA4_8DFA_4A89_87EB_49C32662AFE0.GenerateTableStyleEntry(),
                "{F5AB1C69-6EDB-4FF4-983F-18BD219EF322}" => MediumStyle2Accent3_F5AB1C69_6EDB_4FF4_983F_18BD219EF322.GenerateTableStyleEntry(),
                "{00A15C55-8517-42AA-B614-E9B94910E393}" => MediumStyle2Accent4_00A15C55_8517_42AA_B614_E9B94910E393.GenerateTableStyleEntry(),
                "{7DF18680-E054-41AD-8BC1-D1AEF772440D}" => MediumStyle2Accent5_7DF18680_E054_41AD_8BC1_D1AEF772440D.GenerateTableStyleEntry(),
                "{93296810-A885-4BE3-A3E7-6D5BEEA58F35}" => MediumStyle2Accent6_93296810_A885_4BE3_A3E7_6D5BEEA58F35.GenerateTableStyleEntry(),

                "{8EC20E35-A176-4012-BC5E-935CFFF8708E}" => MediumStyle3_8EC20E35_A176_4012_BC5E_935CFFF8708E.GenerateTableStyleEntry(),
                "{6E25E649-3F16-4E02-A733-19D2CDBF48F0}" => MediumStyle3Accent1_6E25E649_3F16_4E02_A733_19D2CDBF48F0.GenerateTableStyleEntry(),
                "{85BE263C-DBD7-4A20-BB59-AAB30ACAA65A}" => MediumStyle3Accent2_85BE263C_DBD7_4A20_BB59_AAB30ACAA65A.GenerateTableStyleEntry(),
                "{EB344D84-9AFB-497E-A393-DC336BA19D2E}" => MediumStyle3Accent3_EB344D84_9AFB_497E_A393_DC336BA19D2E.GenerateTableStyleEntry(),
                "{EB9631B5-78F2-41C9-869B-9F39066F8104}" => MediumStyle3Accent4_EB9631B5_78F2_41C9_869B_9F39066F8104.GenerateTableStyleEntry(),
                "{74C1A8A3-306A-4EB7-A6B1-4F7E0EB9C5D6}" => MediumStyle3Accent5_74C1A8A3_306A_4EB7_A6B1_4F7E0EB9C5D6.GenerateTableStyleEntry(),
                "{2A488322-F2BA-4B5B-9748-0D474271808F}" => MediumStyle3Accent6_2A488322_F2BA_4B5B_9748_0D474271808F.GenerateTableStyleEntry(),
                "{D7AC3CCA-C797-4891-BE02-D94E43425B78}" => MediumStyle4_D7AC3CCA_C797_4891_BE02_D94E43425B78.GenerateTableStyleEntry(),
                "{69CF1AB2-1976-4502-BF36-3FF5EA218861}" => MediumStyle4Accent1_69CF1AB2_1976_4502_BF36_3FF5EA218861.GenerateTableStyleEntry(),
                "{8A107856-5554-42FB-B03E-39F5DBC370BA}" => MediumStyle4Accent2_8A107856_5554_42FB_B03E_39F5DBC370BA.GenerateTableStyleEntry(),
                "{0505E3EF-67EA-436B-97B2-0124C06EBD24}" => MediumStyle4Accent3_0505E3EF_67EA_436B_97B2_0124C06EBD24.GenerateTableStyleEntry(),
                "{C4B1156A-380E-4F78-BDF5-A606A8083BF9}" => MediumStyle4Accent4_C4B1156A_380E_4F78_BDF5_A606A8083BF9.GenerateTableStyleEntry(),
                "{22838BEF-8BB2-4498-84A7-C5851F593DF1}" => MediumStyle4Accent5_22838BEF_8BB2_4498_84A7_C5851F593DF1.GenerateTableStyleEntry(),
                "{16D9F66E-5EB9-4882-86FB-DCBF35E3C3E4}" => MediumStyle4Accent6_16D9F66E_5EB9_4882_86FB_DCBF35E3C3E4.GenerateTableStyleEntry(),
                "{E8034E78-7F5D-4C2E-B375-FC64B27BC917}" => DarkStyle1_E8034E78_7F5D_4C2E_B375_FC64B27BC917.GenerateTableStyleEntry(),

                "{125E5076-3810-47DD-B79F-674D7AD40C01}" => DarkStyle1Accent1_125E5076_3810_47DD_B79F_674D7AD40C01.GenerateTableStyleEntry(),
                "{37CE84F3-28C3-443E-9E96-99CF82512B78}" => DarkStyle1Accent2_37CE84F3_28C3_443E_9E96_99CF82512B78.GenerateTableStyleEntry(),
                "{D03447BB-5D67-496B-8E87-E561075AD55C}" => DarkStyle1Accent3_D03447BB_5D67_496B_8E87_E561075AD55C.GenerateTableStyleEntry(),
                "{E929F9F4-4A8F-4326-A1B4-22849713DDAB}" => DarkStyle1Accent4_E929F9F4_4A8F_4326_A1B4_22849713DDAB.GenerateTableStyleEntry(),
                "{8FD4443E-F989-4FC4-A0C8-D5A2AF1F390B}" => DarkStyle1Accent5_8FD4443E_F989_4FC4_A0C8_D5A2AF1F390B.GenerateTableStyleEntry(),
                "{AF606853-7671-496A-8E4F-DF71F8EC918B}" => DarkStyle1Accent6_AF606853_7671_496A_8E4F_DF71F8EC918B.GenerateTableStyleEntry(),

                "{5202B0CA-FC54-4496-8BCA-5EF66A818D29}" => DarkStyle2_5202B0CA_FC54_4496_8BCA_5EF66A818D29.GenerateTableStyleEntry(),
                "{0660B408-B3CF-4A94-85FC-2B1E0A45F4A2}" => DarkStyle2Accent1AndAccent2_0660B408_B3CF_4A94_85FC_2B1E0A45F4A2.GenerateTableStyleEntry(),
                "{91EBBBCC-DAD2-459C-BE2E-F6DE35CF9A28}" => DarkStyle2Accent3AndAccent4_91EBBBCC_DAD2_459C_BE2E_F6DE35CF9A28.GenerateTableStyleEntry(),
                "{46F890A9-2807-4EBB-B81D-B2AA78EC7F39}" => DarkStyle2Accent5AndAccent6_46F890A9_2807_4EBB_B81D_B2AA78EC7F39.GenerateTableStyleEntry(),

                _ => null,
            };
        }

        /// <summary>
        /// 此文件夹 表格样式 的代码生成逻辑
        /// </summary>
        public static void GenerateTableStyleEntriesCode()
        {
            // 先确保存在 OpenXmlSdkTool.exe 生成，工具版本 V2.5 的工具
            // 接着新建一份 PPTX 文件，在此文件里面存放表格元素，设置表格元素的样式
            // 将此 PPTX 文件拖入到 OpenXmlSdkTool 工具，点击 Reflect Code 生成代码
            // 将生成的代码放入到以下的 file 变量里，接着执行本函数即可生成代码
            // 将生成的代码拷贝到本文件夹，修改类型名即可

            var file = @"File.txt";
            file = Path.GetFullPath(file);

            var outputFolder = Path.GetDirectoryName(file);
            var testFile = Path.Combine(outputFolder, "Test.txt");

            while (true)
            {
                try
                {
                    var text = File.ReadAllText(file);

                    string styleId = "";

                    var styleIdRegex = new Regex(@"StyleId = ""(\S+)"", ");
                    var styleIdMatch = styleIdRegex.Match(text);
                    if (styleIdMatch.Success)
                    {
                        styleId = styleIdMatch.Groups[1].Value;
                    }
                    else
                    {
                        Console.WriteLine($"匹配失败");
                    }

                    string styleName = "";

                    var styleNameRegex = new Regex(@"StyleName = ""([\S\s]+)"" };");
                    var styleNameMatch = styleNameRegex.Match(text);
                    if (styleNameMatch.Success)
                    {
                        styleName = styleNameMatch.Groups[1].Value;
                    }
                    else
                    {
                        Console.WriteLine($"匹配失败");
                    }

                    var outputFileName = $"{styleName}.cs";

                    var outputFile = Path.Combine(outputFolder, outputFileName);

                    var className = styleId.Replace("{", "").Replace("}", "").Replace("-", "_");
                    text = text.Replace("public class GeneratedClass", $"public static class _{className}").Replace("public TableStyleEntry GenerateTableStyleEntry()", "public static TableStyleEntry GenerateTableStyleEntry()");
                    File.WriteAllText(outputFile, text);

                    File.AppendAllLines(testFile, new string[]
                    {
                        $"\"{styleId}\" => _{className}.GenerateTableStyleEntry(),"
                    });

                    Console.WriteLine($"生成 {styleName}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Console.ReadLine();
            }
        }
    }
}
