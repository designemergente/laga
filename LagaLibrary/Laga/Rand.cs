using Laga;
using System;
using System.Drawing;
using System.Threading;
using Laga.Geometry;

namespace Laga.Numbers
{
    /// <summary>
    /// Random number class
    /// </summary>
    public class Rand
    {
        private static readonly ThreadLocal<Random> rnd = new ThreadLocal<Random>(() => new Random());

        /// <summary>
        /// GenrPalette a list of Integers between min and max paremeters
        /// </summary>
        /// <param name="size">number of integers in the list</param>
        /// <param name="min">the min value</param>
        /// <param name="max">the max value</param>
        /// <returns>Array of integers</returns>
        public static int[] Integers(int size, int min, int max)
        {
            int[] arrN = new int[size];
            for (int i = 0; i < size; i++)
                arrN[i] = rnd.Value.Next(min, max);
            return arrN;
        }

        /// <summary>
        /// GenrPalette a list of floats between min and max paremeters
        /// </summary>
        /// <param name="size">number of floats in the list</param>
        /// <param name="min">the min value</param>
        /// <param name="max">the max value</param>
        /// <returns>Array of floats</returns>
        public static float[] Floats(int size, float min, float max)
        {
            float[] arrN = new float[size];
            for (int i = 0; i < size; i++)
                arrN[i] = min + (float)rnd.Value.NextDouble() * (max - min);
            return arrN;
        }

        /// <summary>
        /// GenrPalette a list of doubles between min and max paremeters
        /// </summary>
        /// <param name="size">number of doubles in the list</param>
        /// <param name="min">the min value</param>
        /// <param name="max">the max value</param>
        /// <returns>Array of doubles</returns>
        public static double[] Doubles(int size, double min, double max)
        {
            double[] arrN = new double[size];
            for (int i = 0; i < size; i++)
                arrN[i] = min + rnd.Value.NextDouble() * (max - min);
            return arrN;
        }

        /// <summary>
        /// Generates a random array of characters.
        /// See <a href="http://www.asciitable.com/">this link</a> for more information.
        /// </summary>
        /// <param name="size">the length of characters in the Chr</param>
        /// <param name="start">the start number in the table, inclusive</param>
        /// <param name="end">the end number in the table, exclusive</param>
        /// <returns>Array of chars</returns>
        public static char[] Characters(int size, int start, int end)
        {
            char[] arrChr = new char[size];

            for (int i = 0; i < size; i++)
                arrChr[i] = Rand.NextChar(start, end);
            return arrChr;
        }

        /// <summary>
        /// GenrPalette a random array of colours.
        /// </summary>
        /// <param name="size">number of colours in the array</param>
        /// <returns>Array of colours</returns>
        public static Color[] Colours(int size)
        {
            Color[] arrN = new Color[size];
            for (int i = 0; i < size; i++)
                arrN[i] = NextColor();
            return arrN;
        }

        /// <summary>
        /// GenrPalette a random array of vectors.
        /// </summary>
        /// <param name="size">number of vectors in the array</param>
        /// <param name="Xcoord">The range of values in the X coordinate</param>
        /// <param name="Ycoord">The range of values in the Y coordinate</param>
        /// <param name="Zcoord">The range of values in the Z coordinate</param>
        /// <returns>Array of vectors</returns>
        public static Vector[] Vectors(int size, Range Xcoord, Range Ycoord, Range Zcoord)
        {
            Vector[] arrVectors = new Vector[size];
            for (int i = 0; i < size; i++)
                arrVectors[i] = NextVector(Xcoord, Ycoord, Zcoord);

            return arrVectors;
        }

        /// <summary>
        ///  Generates a random a random integer between min and max paremeters
        /// </summary>
        /// <param name="min">The minimum value in the range</param>
        /// <param name="max">The maximum value in the range</param>
        /// <returns>integer</returns> 
        public static int NextInt(int min, int max)
        {
            return rnd.Value.Next(min, max);
        }
        
        /// <summary>
        /// Random value between 0 and 1
        /// </summary>
        /// <returns>double</returns>
        public static double NextDouble()
        {
            return rnd.Value.NextDouble();
        }

        /// <summary>
        /// Generates a random double between min and max paremeters
        /// </summary>
        /// <param name="min">The minimum value in the range</param>
        /// <param name="max">The maximum value in the range</param>
        /// <returns>double</returns>
        public static double NextDouble(double min, double max)
        {
            return min + rnd.Value.NextDouble() * (max - min);
        }

        /// <summary>
        ///  Generates a random float Number between min and max paremeters
        /// </summary>
        /// <param name="min">The minimum value in the range</param>
        /// <param name="max">The maximum value in the range</param>
        /// <returns>float</returns>
        public static float NextFloat(float min, float max)
        {
            return min + (float)rnd.Value.NextDouble() * (max - min);
        }

        /// <summary>
        /// Generates a random character between min and max paremeters
        /// See <a href="http://www.asciitable.com/">this link</a> for more information.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>char</returns>
        public static char NextChar(int start, int end)
        {
            if (start < 0 || end > 255 || start > end)
                throw new ArgumentOutOfRangeException("NextChar range is invalid.");

            return (char)rnd.Value.Next(start, end);
        }

        /// <summary>
        /// Generates a random color
        /// </summary>
        /// <returns>A random color</returns>
        public static Color NextColor()
        {
            return Color.FromArgb(rnd.Value.Next(0, 255), rnd.Value.Next(0, 255), NextChar(0, 255));
        }

        /// <summary>
        /// Generates a random vector
        /// </summary>
        /// <param name="Xcoord">The range of values in the X coordinate</param>
        /// <param name="Ycoord">The range of values in the Y coordinate</param>
        /// <param name="Zcoord">The range of values in the Z coordinate</param>
        /// <returns>Random vector</returns>
        public static Vector NextVector(Range Xcoord, Range Ycoord, Range Zcoord)
        {
            return new Vector(Rand.NextDouble() * (Xcoord.Max - Xcoord.Min), Rand.NextDouble() * (Ycoord.Max - Ycoord.Min), Rand.NextDouble() * (Zcoord.Max - Zcoord.Min));
        }

    }
}
