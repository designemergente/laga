using System;
using System.Collections.Generic;
using System.Drawing;

namespace Laga.Numbers
{
    /// <summary>
    /// Color range class
    /// </summary>
    public class ColorRange
    {
        // Predefined color palettes (e.g., Matplotlib-style)
        private static readonly Dictionary<string, List<Color>> Palettes = new Dictionary<string, List<Color>>
        {
            { "Viridis", new List<Color> { Color.FromArgb(68, 1, 84), Color.FromArgb(59, 82, 139), Color.FromArgb(33, 145, 140), Color.FromArgb(94, 201, 98), Color.FromArgb(253, 231, 37) } },
            { "Plasma", new List<Color> { Color.FromArgb(13, 8, 135), Color.FromArgb(126, 3, 167), Color.FromArgb(224, 71, 48), Color.FromArgb(248, 148, 0), Color.FromArgb(240, 249, 33) } },
            { "Inferno", new List<Color> { Color.FromArgb(0, 0, 4), Color.FromArgb(87, 15, 109), Color.FromArgb(187, 55, 84), Color.FromArgb(249, 159, 45), Color.FromArgb(252, 255, 164) } },
            { "Magma", new List<Color> { Color.FromArgb(0, 0, 4), Color.FromArgb(72, 23, 107), Color.FromArgb(163, 73, 102), Color.FromArgb(240, 142, 50), Color.FromArgb(253, 252, 191) } },
            { "Fifa", new List<Color> { Color.Red, Color.Orange, Color.Green, Color.Blue, Color.Magenta } }
        };

        /// <summary>
        /// Linearly interpolates between two colors.
        /// </summary>
        /// <param name="color1">The starting color.</param>
        /// <param name="color2">The ending color.</param>
        /// <param name="t">Interpolation factor (0.0 to 1.0).</param>
        /// <returns>An interpolated color.</returns>
        public static Color LerpColor(Color color1, Color color2, double t)
        {
            int r = (int)(color1.R + t * (color2.R - color1.R));
            int g = (int)(color1.G + t * (color2.G - color1.G));
            int b = (int)(color1.B + t * (color2.B - color1.B));
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Interpolates a color from a given palette based on a normalized position (0.0 to 1.0).
        /// </summary>
        /// <param name="palette">The base palette colors.</param>
        /// <param name="t">Normalized position (0.0 to 1.0).</param>
        /// <returns>An interpolated color.</returns>
        public static Color InterpolateColor(List<Color> palette, double t)
        {
            if (t <= 0) return palette[0];
            if (t >= 1) return palette[palette.Count - 1];

            double scaledPosition = t * (palette.Count - 1);
            int lowerIndex = (int)Math.Floor(scaledPosition);
            int upperIndex = (int)Math.Ceiling(scaledPosition);

            double localT = scaledPosition - lowerIndex;

            return LerpColor(palette[lowerIndex], palette[upperIndex], localT);

        }
        /// <summary>
        /// Generates a range of colors based on a selected palette.
        /// </summary>
        /// <param name="paletteName">Name of the predefined palette.</param>
        /// <param name="count">Number of colors to generate in the range.</param>
        /// <returns>A list of interpolated colors.</returns>
        public static List<Color> GenrPalette(string paletteName, int count)
        {
            if (!Palettes.ContainsKey(paletteName))
                throw new ArgumentException($"Palette '{paletteName}' does not exist.");

            var baseColors = Palettes[paletteName];
            var result = new List<Color>();

            // Interpolate colors between base palette colors
            for (int i = 0; i < count; i++)
            {
                double t = (double)i / (count - 1); // Normalized position in the range
                result.Add(InterpolateColor(baseColors, t));
            }

            return result;
        }
    }
}
