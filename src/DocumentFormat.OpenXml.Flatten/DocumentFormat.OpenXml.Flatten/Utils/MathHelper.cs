using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.Utils
{
    static class MathHelper
    {
        public static bool NearlyEquals(EmuPoint a, EmuPoint b, double tolerance = 0.001)
        {
            return NearlyEquals(a.X.Value, b.X.Value, tolerance) && NearlyEquals(a.Y.Value, b.Y.Value, tolerance);
        }

        public static bool NearlyEquals(double a, double b, double tolerance = 0.001)
        {
            return System.Math.Abs(a - b) < tolerance;
        }
    }
}
