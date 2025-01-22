using System;
using Laga.Numbers;

namespace Laga
{
    /// <summary>
    /// GenrPalette different Chromosome types
    /// </summary>
    [Obsolete("Please try GenrGenes instead", false)]
    public class GenrChromosome
    {
        private int size;

        /// <summary>
        /// size of the Chr
        /// </summary>
        public int SizeChrom
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

       readonly private Random rnd;

        /// <summary>
        /// Constructor:
        /// </summary>
        /// <param name="Size">The Lengthof the Chromosome</param>
        public GenrChromosome(int Size)
        {
            this.size = Size;
            rnd = new Random(DateTime.Now.Millisecond);
        }


        /// <summary>
        /// the method generates a Chr composed by random doubles
        /// between min and max.
        /// </summary>
        /// <param name="min">The min value in the Chr</param>
        /// <param name="max">The max value in the Chr(exclusive upper bound)</param>
        /// <returns>double[]</returns>
        /// <example> How to implement this class:
        /// <code>
        /// GenrChromosome Chr = new GenrChromosome(5);
        /// double[] Chrom = Chr.NumberChromosome(0.0, 1.0);
        /// 
        /// result:
        /// 0.207198212485387, 0.253313632334263, 0.566322204920613, 0.735812343068334, 0.479827285036364
        ///
        /// float[] Chrom = Chr.NumberChromosome(0.0f, 1.0f);
        /// 
        /// result:
        /// 9.211745E+08, 1.134843E+09, 1.554793E+09, 1.223132E+09, 1.600176E+09
        ///
        /// int[] Chrom = Chr.NumberChromosome(0, 10);
        /// 
        /// result: 
        /// 8, 3, 7, 2, 1
        /// </code>
        /// </example>
        public double[] NumberChromosome(double min, double max)
        {
            double[] chr = new double[size];

            for (int i = 0; i < size; i++)
            {
                chr[i] = min + rnd.NextDouble() * (max - min);
            }

            return chr;
        }

        /// <summary>
        /// The method generates a Chr composed by random doubles between min and max.
        /// </summary>
        /// <param name="min">The min value in the Chr</param>
        /// <param name="max">The max value in the Chr (excluded)</param>
        /// <returns>double Chromosome</returns>
        public Chromosome<double> DNA_RandDouble(double min, double max)
        {
            Chromosome<double> chrom = new Chromosome<double>();
            for (int i = 0; i < size; i++)
                chrom.Add(min + rnd.NextDouble() * (max - min));

            return chrom;
        }




        /// <summary>
        /// the method generates a Chr composed by random floats
        /// between min and max.
        /// </summary>
        /// <param name="min">The min value in the Chr</param>
        /// <param name="max">The max value in the Chr(exclusive upper bound)</param>
        /// <returns>float[]</returns>
        public float[] NumberChromosome(float min, float max)
        {
            float[] chr = new float[size];

            for (int i = 0; i < size; i++)
            {
                chr[i] = min + rnd.Next() * (max - min);
            }

            return chr;
        }
        /// <summary>
        /// Generates a float Chr between min and max
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns></returns>
        public Chromosome<float> DNA_RandFloat(float min, float max)
        {
            Chromosome<float> ch = new Chromosome<float>();

            for (int i = 0; i < size; i++)
                ch.Add(Rand.NextFloat(min, max));

            return ch;
        }



        /// <summary>
        /// the method generates a Chr composed by random integers
        /// between min and max.
        /// </summary>
        /// <param name="min">The min value in the Chr</param>
        /// <param name="max">The max value in the Chr(exclusive upper bound)</param>
        /// <returns>int[]</returns>
        public int[] NumberChromosome(int min, int max)
        {
            int[] ch = new int[size];

            for (int i = 0; i < size; i++)
                ch[i] = Rand.NextInt(min, max);
            
            return ch;
        }

        /// <summary>
        /// Generates a Chromosome composed by random integers
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>int Chromosome</returns>
        public Chromosome<int> DNA_RandInteger(int min, int max)
        {
            Chromosome<int> ch = new Chromosome<int>();

            for (int i = 0; i < size; i++)
                ch.Add(Rand.NextInt(min, max));

            return ch;
        }

        /// <summary>
        /// creates a binary Chr composed by 1s and 0s;
        /// </summary>
        /// <returns> a random list of 1s and 0s</returns>
        /// <example>
        /// <code>
        /// GenrChromosome Chr = new GenrChromosome(5);
        /// int[] Chrom = Chr.NumberChromosomeBinary();
        /// 
        /// result:
        /// 1, 1, 0, 1, 1
        /// </code>
        /// </example>
        public int[] NumberChromosomeBinary(int size)
        {
            int[] chr = new int[size];
            int binary;

            double v;

            for (int i = 0; i < size; i++)
            {
                v = rnd.NextDouble();
                binary = (v < 0.5) ? 0 : 1;

                chr[i] = binary;
            }

            return chr;
        }

        /// <summary>
        /// Generates a binary Chromosome of 1s and 0s int type.
        /// </summary>
        /// <returns></returns>
        public Chromosome<int> DNA_IntBinary(int size)
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
        /// <returns>a non repeat random integer list</returns>
        /// <example>
        /// <code>
        /// GenrChromosome Chr = new GenrChromosome(5);
        /// char[] Chrom = Chr.NumberChromosomeSwap(0, 4);
        /// 
        /// results:
        /// 4, 1, 0, 2, 3
        /// 3, 1, 2, 0, 4
        /// </code>
        /// </example>
        public int[] NumberChromosomeSwap(int min, int max)
        {
            int[] chr = new int[(max - min) + 1];
            int count = 0;
            for (int i = min; i < max + 1; i++)
            {
                chr[count] = i;
                count++;
            }
            return Tools.Fisher_Yates(chr);
        }

        /// <summary>
        /// GenrPalette a Chromosome composed by non repeated integers between min and max included, designed for combinatorial problems.
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>int Chromosome</returns>
        public static Chromosome<int> DNA_ShuffleInteger(int min, int max)
        {
            _ = new Random(DateTime.Now.Millisecond);
            Chromosome<int> chr = new Chromosome<int>();
                           
            for (int i = min; i < max + 1; i++)
                chr.Add(i);
            
            int n = chr.Count;
            int index, temp;
            for(int i = 0; i < n; i++)
            {
                index = Rand.NextInt(i, n);
                temp = chr.GetGene(index);
                chr.SetGene(index, chr.GetGene(i));
                chr.SetGene(i, temp);
            }

            return chr;
        }


        /// <summary>
        /// Generates a binary Chr of chars.
        /// </summary>
        /// <returns>a random char list of 1s and 0s</returns>
        /// <example>
        /// <code>
        /// GenrChromosome Chr = new GenrChromosome(5);
        /// char[] Chrom = Chr.CharChromosomeBinary();
        /// 
        /// result:
        /// 0, 1, 0, 1, 0,
        /// </code>
        /// </example>
        public char[] CharChromosomeBinary()
        {
            char[] arrChr = new char[size];

            for (int i = 0; i < size; i++)
            {
                arrChr[i] = rnd.NextDouble() >= 0.5 ? '1' : '0';

            }

            return arrChr;
        }

        /// <summary>
        /// Generates a binary Chromosome of 1s and 0s char type.
        /// </summary>
        /// <returns>char Chromosome</returns>
        public Chromosome<char> DNA_CharBinary()
        {
            Chromosome<char> arrChr = new Chromosome<char>();

            for (int i = 0; i < size; i++)
                arrChr.Add(rnd.NextDouble() >= 0.5 ? '1' : '0');

            return arrChr;
        }



        /// <summary>
        /// Generates an random char Chr composed by characters.
        /// See <a href="http://www.asciitable.com/">this link</a> for more information.
        /// </summary>
        /// <param name="start">the start number in the table, inclusive</param>
        /// <param name="end">the end number in the table, exclusive</param>
        /// <returns>returns a random list of characters</returns>
        /// <example>
        /// <code>
        /// GenrChromosome Chr = new GenrChromosome(25);
        /// char[] Chrom = Chr.CharChromosome(65, 90);
        /// 
        /// result:
        /// E, B, C, N, F, C, O, P, C, H, O, U, Q, U, G, L, K, Z, E, K, X, A, L, B, Q,
        ///
        /// char[] Chrom = Chr.CharChromosome(33, 47);
        /// 
        /// result
        /// ,, -, !, /, -, ', %, !, %, %, !, /, ,, +, ), *, ), /, |, /, ], +, (, !, +,
        /// 
        /// char[] Chrom = Chr.CharChromosome(97, 122);
        /// 
        /// result
        /// q, a, h, k, j, d, u, o, d, l, w, b, d, i, l, l, h, c, n, c, s, d, k, r, h,
        /// </code>
        /// </example>
        public char[] CharChromosome(int start, int end)
        {
            char[] chr = new char[size];
            
            for (int i = 0; i < size; i++)
            {
                chr[i] = (char)rnd.Next(start, end);
            }

            return chr;
        }

        /// <summary>
        /// Generates a random char Chromosome composed by characters.
        /// See <a href="http://www.asciitable.com/">this link</a> for more information.
        /// </summary>
        /// <param name="start">the start number in the table, inclusive</param>
        /// <param name="end">the end number in the table, exclusive</param>
        /// <returns>char Chromosome</returns>
        public Chromosome<char> DNA_Char(int start, int end)
        {
            Chromosome<char> chr = new Chromosome<char>(size);

            for (int i = 0; i < size; i++)
                chr.Add((char)rnd.Next(start, end));
            
            return chr;
        }

    }
}
