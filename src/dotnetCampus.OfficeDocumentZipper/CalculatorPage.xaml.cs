using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotnetCampus.OpenXMLUnitConverter;

namespace dotnetCampus.OfficeDocumentZipper
{
    /// <summary>
    /// CalculatorPage.xaml 的交互逻辑
    /// </summary>
    public partial class CalculatorPage : UserControl
    {
        public CalculatorPage()
        {
            InitializeComponent();
        }

        private void Convert(TextBox textBox, Func<double, Emu> toEmu)
        {
            if (_isConverting)
            {
                return;
            }

            _isConverting = true;

            var text = textBox.Text;
            if (double.TryParse(text, out var value))
            {
                var emu = toEmu(value);
                SetEmu(textBox, emu);
            }
            else
            {
                SetInvalidate(textBox);
            }

            _isConverting = false;
        }

        private bool _isConverting;

        private void SetEmu(TextBox currentTextBox, Emu emu)
        {
            SetEmuInner(CentimetersText, emu.ToCm().Value);
            SetEmuInner(EmuText, emu.Value);
            SetEmuInner(PixelText, emu.ToPixel().Value);
            SetEmuInner(PoundText, emu.ToPixel().ToPound().Value);

            void SetEmuInner(TextBox textBox, double value)
            {
                SetTextInner(textBox, value.ToString());
            }

            void SetTextInner(TextBox textBox, string text)
            {
                if (!ReferenceEquals(textBox, currentTextBox))
                {
                    textBox.Text = text;
                }
            }
        }

        private void SetInvalidate(TextBox currentTextBox)
        {
            SetInvalidateInner(CentimetersText);
            SetInvalidateInner(EmuText);
            SetInvalidateInner(PixelText);
            SetInvalidateInner(PoundText);

            void SetInvalidateInner(TextBox textBox)
            {
                if (!ReferenceEquals(textBox, currentTextBox))
                {
                    textBox.Text = InvalidateText;
                }
            }
        }

        private const string InvalidateText = "-";

        private void CentimetersText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Convert(CentimetersText, value => new Cm(value).ToEmu());
        }

        private void EmuText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Convert((TextBox)sender, value => new Emu(value));
        }

        private void PixelText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Convert((TextBox)sender, value => new Pixel(value).ToEmu());
        }

        private void PoundText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Convert((TextBox)sender, value => new Pound(value).ToPixel().ToEmu());
        }
    }
}
