using System;
using Laga.Numbers;

namespace Laga
{
    /// <summary>
    /// Mutate class
    /// </summary>
     public static class Mutation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Func<int, char> Mutate(int min, int max)
        {
            return (geneIndex) => {
                int rangeOffset = geneIndex % (max - min + 1); // Example usage
                return (char)(Rand.NextInt(min + rangeOffset, max + 1));
            };
        }

        /// <summary>
        /// Mutate a char.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static char CharMutation(int min, int max)
        {
            return (char)(Rand.NextInt(min, max + 1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double DblMutation(double min, double max)
        {
            return Rand.NextDouble(min, max);
        }

        #region old code
        //old code
        /*
        private float popPercent;
        private int cant;
        private int[] arrIndex;

        private Random rnd;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MutationRate"></param>
        public Mutate(float MutationRate)
        {
            popPercent = MutationRate;
            rnd = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="percentChrom"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Population<char> CharMutation(Population<char> population, float percentChrom, int start, int end)
        {
            Random rnd = new Random();

            foreach(Chromosome<Char> chr in population)
            {
                for(int i = 0; i < chr.Count; i++)
                {
                    if (rnd.NextDouble() < percentChrom)
                    {        
                        chr.SetGene(i, Tools.RandomChar(start, end));
                    }
                }
            }
            return population;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="MutationRate"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Population<double> Number(Population<double> population, float MutationRate, double min, double max)
        {
            Random rnd = new Random();
            double v;

            foreach(Chromosome<double> chr in population)
            {
                if (rnd.NextDouble() < MutationRate)
                {
                    for (int j = 0; j < chr.Count; j++)
                    {
                        v = min + rnd.NextDouble() * (max - min);
                        chr.SetGene(j, v);
                    }
                }
            }
            return population;
        }

        #region Mutate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chromosomes"></param>
        /// <param name="percentChrom"></param>
        /// <returns></returns>
        public object[][] MutationSwap(object[][] chromosomes, float percentChrom)
        {
            cant = (int)(popPercent * chromosomes.Length);
            if (cant == 0) { cant = 1; }

            //deep copy the array.
            object[][] mutatedPop = chromosomes.Select(a => a.ToArray()).ToArray();

            //random list...
            int[] arrindex = new int[chromosomes.Length];
            for (int i = 0; i < arrindex.Length; ++i) arrindex[i] = i;
            arrindex = Tools.Fisher_Yates(arrindex); // fisher_yate(arrindex);

            for (int i = 0; i < cant; ++i) //loop para 
            {
                MutationSwap(mutatedPop[arrindex[i]], percentChrom);
            }

            return mutatedPop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chrom"></param>
        /// <param name="p"></param>
        private void MutationSwap(object[] chrom, float p)
        {
            int size = (int)(chrom.Length * p);
            if (size == 0) { size = 1; }

            int index, index2;

            for (int i = 0; i < size; i++)
            {
                index = rnd.Next(chrom.Length); // 0 <= j <= i-1

                do
                {
                    index2 = rnd.Next(chrom.Length);
                } while (index == index2);

                //swap
                object temp = chrom[index];
                chrom[index] = chrom[index2];
                chrom[index2] = temp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chromosomes"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="percentChrom"></param>
        /// <returns></returns>
        public double[][] NumbMutation(double[][] chromosomes, double min, double max, float percentChrom)
        {
            cant = (int)(popPercent * chromosomes.Length);
            if (cant == 0) { cant = 1; }

            //deep copy the array.
            double[][] mutatedPop = chromosomes.Select(a => a.ToArray()).ToArray();

            int[] arrindex = new int[chromosomes.Length];
            for (int i = 0; i < arrindex.Length; ++i) arrindex[i] = i;
            arrindex = Tools.Fisher_Yates(arrindex); //fisher_yate(arrindex);

            for (int i = 0; i < cant; ++i) //loop para 
            {
                DoubleMutation(mutatedPop[arrindex[i]], percentChrom, min, max);
            }

            return mutatedPop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mutatedPop"></param>
        /// <param name="percent"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// 
        private void DoubleMutation(double[] mutatedPop, float percent, double min, double max)
        {
            int cant = (int)(mutatedPop.Length * percent);
            if (cant == 0) { cant = 1; }

            Random rnd = new Random();
            int rndIndex;
            for (int i = 0; i < cant; ++i)
            {
                rndIndex = rnd.Next(mutatedPop.Length);
                mutatedPop[rndIndex] = min + rnd.NextDouble() * (max - min);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chromosomes"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="percentChrom"></param>
        /// <returns></returns>
        public float[][] NumbMutation(float[][] chromosomes, float min, float max, float percentChrom)
        {
            cant = (int)(popPercent * chromosomes.Length);
            if (cant == 0) { cant = 1; }

            float[][] mutatedPop = chromosomes.Select(a => a.ToArray()).ToArray();

            int[] arrindex = new int[chromosomes.Length];
            for (int i = 0; i < arrindex.Length; ++i) arrindex[i] = i;
            arrindex = Tools.Fisher_Yates(arrindex); 

            for (int i = 0; i < cant; ++i) 
            {
                FloatMutation(mutatedPop[arrindex[i]], percentChrom, min, max);
            }

            return mutatedPop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mutatedPop"></param>
        /// <param name="percent"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        private void FloatMutation(float[] mutatedPop, float percent, float min, float max)
        {
            int cant = (int)(mutatedPop.Length * percent);
            if (cant == 0) { cant = 1; }

            int rndIndex;
            for (int i = 0; i < cant; ++i)
            {
                rndIndex = rnd.Next(mutatedPop.Length);
                mutatedPop[rndIndex] = (float)(min + rnd.NextDouble() * (max - min));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chromosomes"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="percentChrom"></param>
        /// <returns></returns>
        public int[][] NumbMutation(int[][] chromosomes, int min, int max, float percentChrom)
        {
            cant = (int)(popPercent * chromosomes.Length);
            if (cant == 0) { cant = 1; }

            //deep copy the array.
            int[][] mutatedPop = chromosomes.Select(a => a.ToArray()).ToArray();

            int[] arrindex = new int[chromosomes.Length];
            for (int i = 0; i < arrindex.Length; ++i) arrindex[i] = i;
            arrindex = Tools.Fisher_Yates(arrindex); //fisher_yate(arrindex);

            for (int i = 0; i < cant; ++i) //loop para 
            {
                IntMutation(mutatedPop[arrindex[i]], percentChrom, min, max);
            }

            return mutatedPop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mutatedPop"></param>
        /// <param name="percent"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        private void IntMutation(int[] mutatedPop, float percent, float min, float max)
        {
            int cant = (int)(mutatedPop.Length * percent);
            if (cant == 0) { cant = 1; }

            int rndIndex;
            for (int i = 0; i < cant; ++i)
            {
                rndIndex = rnd.Next(mutatedPop.Length);
                mutatedPop[rndIndex] = (int)(min + rnd.NextDouble() * (max - min));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chromosomes"></param>
        /// <param name="ChroPercent"></param>
        /// <returns></returns>
        public char[][] BinaryCharMutation(char[][] chromosomes, float ChroPercent)
        {
            SelectChromosomes(chromosomes);
            int chroCant = (int)(chromosomes[0].Length * ChroPercent);
            if (chroCant == 0) { chroCant = 1; }

            //deep copy the array.
            char[][] mutatedPop = chromosomes.Select(a => a.ToArray()).ToArray();

            int[] arrPointer;

            char gen, mutGen;
            for (int i = 0; i < cant; ++i) //the loop for the population
            {
                arrPointer = Tools.RandomInt(0, chromosomes[i].Length, ChroPercent);
                for (int j = 0; j < chroCant; ++j) //the loop for the chromosomes
                {
                    gen = mutatedPop[arrIndex[i]][arrPointer[j]];

                    mutGen = (gen == '0') ? '1' : '0';

                    mutatedPop[arrIndex[i]][arrPointer[j]] = mutGen;
                }
            }

            return mutatedPop;
        }

        /// <summary>
        /// A Mutate Algorithm
        /// </summary>
        /// <param name="chromosomes">The population to perform the mutation</param>
        /// <param name="ChroPercent">the percent of mutation in the Chr</param>
        /// <param name="start">the start number for the table, inclusive: Eg: 97</param>
        /// <param name="end">the end number for the table, inclusive: Eg: 122</param>
        /// <returns>char[][]</returns>
        public char[][] CharMutation(char[][] chromosomes, float ChroPercent, int start, int end)
        {
            SelectChromosomes(chromosomes);
            int chroCant = (int)(chromosomes[0].Length * ChroPercent);
            if (chroCant == 0) { chroCant = 1; }

            //clone the array.
            char[][] mutatedPop = chromosomes.Select(a => a.ToArray()).ToArray();

            char rndChar; //random NextChar to replace
            int pointer; //random pointer, for the index

            for (int i = 0; i < cant; ++i) //the loop for the population
            {
                for (int j = 0; j < chroCant; ++j) //the loop for the chromosomes
                {
                    rndChar = Tools.RandomChar(start, end); // RandomChar();
                    pointer = rnd.Next(chromosomes[i].Length);
                    mutatedPop[arrIndex[i]][pointer] = rndChar;
                }
            }

            return mutatedPop;
        }

        private void SelectChromosomes(char[][] charPop)
        {
            //numbers and utilities..
            cant = (int)(popPercent * charPop.Length);
            if (cant == 0) { cant = 1; }

            int c;

            //random zone.
            Random rnd = new Random();
            int index;
            arrIndex = new int[cant];

            //loop to find random and not repeated indexes.
            for (int i = 0; i < cant; ++i)
            {
                do
                {
                    c = 0;
                    index = rnd.Next(charPop.Length);
                    if (i > 0)
                    {
                        for (int j = 0; j < i; ++j)
                        {
                            if (arrIndex[j] == index)
                            {
                                c++;
                            }
                        }
                        if (c == 0)
                        {
                            arrIndex[i] = index;
                        }
                    }
                    else
                    {
                        arrIndex[i] = index;
                    }
                } while (c != 0);
            }
        }
        */
        #endregion

    }
}
