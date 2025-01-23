using System;
using Laga.Geometry;
using Laga.Numbers;

namespace Laga
{
    /// <summary>
    /// GenrPalette DNA for Chromosome classes
    /// </summary>
    public static class GenrGenes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="size"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="generator"></param>
        /// <returns>Chromosome</returns>
        public static Chromosome<T> RandomChromosome<T>(int size, T min, T max, Func<T, T, T> generator)
        {
            Chromosome<T> chrom = new Chromosome<T>(size);
            for (int i = 0; i < size; i++)
                chrom.Add(generator(min, max));
            return chrom;
        }
 
        /// <summary>
        /// Generates a binary Chromosome of 1s and 0s int type.
        /// </summary>
        /// /// <param name="size">the number of integers in the Chr</param> 
        /// <returns>int Chromosome</returns>
        public static Chromosome<int> Binary_Integer(int size)
        {
            Chromosome<int> arrChr = new Chromosome<int>();

            for (int i = 0; i < size; i++)
                arrChr.Add(Rand.NextDouble() >= 0.5 ? 1 : 0);

            return arrChr;
        }

        /// <summary>
        /// the method generate a number Chr composed by non repeated numbers between start and start + size(not inclusive).
        /// the method is based on integer numbers. this method is designed by combinatorial problems.
        /// </summary>
        /// <param name="min">the minimum value in the sequence</param>
        /// /// <param name="max">the maximum value in the sequence</param>
        /// <returns>Array</returns>
        public static Chromosome<int> Shuffle_Integer(int min, int max)
        {
            if (min >= max)
                throw new ArgumentException("the 'min' value must be less than 'max'. ");

            Chromosome<int> arrChr = new Chromosome<int>();

            for (int i = 0; i < (max - min) + 1; i++)
                arrChr.Add(min + i);

            arrChr.Shuffle();

            return arrChr;
        }

        /// <summary>
        /// Generates a binary Chromosome of 1s and 0s char type.
        /// </summary>
        /// <param name="size">the length in the Chr</param> 
        /// <returns>char Chromosome</returns>
        public static Chromosome<char> Binary_Char(int size)
        {
            Chromosome<char> arrChr = new Chromosome<char>();

            for (int i = 0; i < size; i++)
                arrChr.Add(Rand.NextDouble() >= 0.5 ? '1' : '0');

            return arrChr;
        }

        /// <summary>
        /// Generates a random char Chromosome composed by characters.
        /// See <a href="http://www.asciitable.com/">this link</a> for more information.
        /// </summary>
        /// <param name="size">the length of characters in the Chr</param>
        /// <param name="start">the start number in the table, inclusive</param>
        /// <param name="end">the end number in the table, exclusive</param>
        /// <returns>char Chromosome</returns>
        public static Chromosome<char> Rand_Char(int size, int start, int end)
        {
            Chromosome<char> chr = new Chromosome<char>(size);
            chr.AddGenes(Rand.Characters(size, start, end));

            return chr;
        }

        /// <summary>
        /// Generates a random Vector chromosome
        /// </summary>
        /// <param name="size">number of Vectors in the Chromosome</param>
        /// <param name="Xcoord">The range of values in the X coordinate</param>
        /// <param name="Ycoord">The range of values in the Y coordinate</param>
        /// <param name="Zcoord">The range of values in the Z coordinate</param>
        /// <returns>Vector Chromosome</returns>
        public static Chromosome<Vector> Rand_Vector(int size, Range Xcoord, Range Ycoord, Range Zcoord)
        {
            Chromosome<Vector> chrVector = new Chromosome<Vector>();
            chrVector.AddGenes(Rand.Vectors(size, Xcoord, Ycoord, Zcoord));
            return chrVector;
        }



    }
}
