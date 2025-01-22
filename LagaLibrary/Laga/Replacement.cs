using System;
using Laga.Numbers;

namespace Laga
{
    /// <summary>
    /// To replace old populations with new populations
    /// </summary>
    public class Replacement
    {
        /// <summary>
        ///  Replacement class
        /// </summary>
        public Replacement()
        {

        }

        /// <summary>
        /// This replace method is ideal for combinatorial problems with objects types in chromsome. and where is not possible generate new data in the replacement population.
        /// </summary>
        /// <param name="sonMutPopulation">the son mutated population</param>
        /// <param name="ParentsPopulation">the parents population</param>
        /// <param name="sizePopulation">the size of the population</param>
        /// <returns>object[][]</returns>
        public object[][] ReplaceInheritance(object[][] sonMutPopulation, object[][] ParentsPopulation, int sizePopulation)
        {
            object[][] newPopulation = new object[sizePopulation][];

            object[][] newPopulation1 = new object[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                newPopulation1[i] = new object[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, newPopulation1[i], 0, sonMutPopulation[i].Length);
            }

            object[][] newPopulation2 = new object[ParentsPopulation.Length][]; //copy the parents part.
            for (int i = 0; i < ParentsPopulation.Length; i++)
            {
                newPopulation2[i] = new object[ParentsPopulation[i].Length];
                Array.Copy(ParentsPopulation[i], 0, newPopulation2[i], 0, ParentsPopulation[i].Length);
            }

            for (int i = 0; i < sonMutPopulation.Length; ++i)
            { //copy the son parts...
                newPopulation[i] = newPopulation1[i];
            }

            Random rnd = new Random();
            int rndIndex;
            for (int i = sonMutPopulation.Length; i < sizePopulation; ++i)
            {
                rndIndex = rnd.Next(ParentsPopulation.Length);
                newPopulation[i] = newPopulation2[rndIndex];
            }

            return newPopulation;
        }

        /// <summary>
        /// Creates a new population using the mutated inheritance and new random chromosomes.
        /// </summary>
        /// <param name="sonMutPopulation">Object[][]</param>
        /// <param name="percent">float</param>
        /// <param name="sizePopulation">int</param>
        /// <returns>object[][]</returns>
        public object[][] ReplaceRandom(object[][] sonMutPopulation, float percent, int sizePopulation)
        {
            object[][] newPopulation = new object[sizePopulation][];

            object[][] newPopulation1 = new object[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                newPopulation1[i] = new object[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, newPopulation1[i], 0, sonMutPopulation[i].Length);
            }

            for (int i = 0; i < sonMutPopulation.Length; ++i)
            { //copy the son parts...
                newPopulation[i] = newPopulation1[i];
            }

            int c = 0;
            for (int i = sonMutPopulation.Length; i < sizePopulation; ++i)
            {
                newPopulation[i] = Tools.Fisher_YatesPercent(newPopulation1[c], percent);   //fisher_yates_Percent(newPopulation1[c], percent);
                c++;
                if (c == newPopulation1.Length) c = 0;
            }

            return newPopulation;
        }

        /**
 * ReplaceInheritanceRandom method.
 * this replace method is based in a mix between inheritance and random method, the percentInherit, determines how many
 * individuals will (parents) and how many new ones will be created for the new population.
 * 
 *
 * @param sonMutPopulation  -> the son mutated population
 * @param ParentsPopulation -> the parents population.
 * @param sizePopulation    -> the size of the population.
 * @param min    			-> min value in the chromosome.
 * @param max    			-> max value in the chromosome.
 * @param percentInherit    -> percentInherit.
 * @return object[][] new population.
 * 
 * 
 */



        /// <summary>
        /// this replace method is based in a mix between inheritance and random method, the percentInherit, determines how many individuals will(parents) and how many new ones will be created for the new population.
        /// </summary>
        /// <param name="sonMutPopulation">the son mutated population</param>
        /// <param name="ParentsPopulation">the parents population</param>
        /// <param name="sizePopulation">the size of the population</param>
        /// <param name="percentInherit">percentInherit</param>
        /// <returns></returns>
        public object[][] ReplaceInheritanceRandom(object[][] sonMutPopulation, object[][] ParentsPopulation, int sizePopulation, float percentInherit)
        {
            int resta = sizePopulation - sonMutPopulation.Length;
            int cant = (int)(resta * percentInherit);
            if (cant > ParentsPopulation.Length) { cant = ParentsPopulation.Length; }

            object[][] newPopulation = new object[sizePopulation][];

            object[][] newSon = new object[sonMutPopulation.Length][]; //copiamos la herencia...
            for (int i = 0; i < sonMutPopulation.Length; ++i)
            {
                newSon[i] = new object[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, newSon[i], 0, sonMutPopulation[i].Length);
            }

            object[][] newParents = new object[ParentsPopulation.Length][]; //copiamos los padres...
            for (int i = 0; i < ParentsPopulation.Length; ++i)
            {
                newParents[i] = new object[ParentsPopulation[i].Length];
                Array.Copy(ParentsPopulation[i], 0, newParents[i], 0, ParentsPopulation[i].Length);
            }

            //construccion
            //copiamos los hijos...
            for (int i = 0; i < sonMutPopulation.Length; ++i)
            { //copy the son parts...
                newPopulation[i] = newSon[i];
            }

            //copiamos a los padres...
            int c = newSon.Length;
            for (int i = 0; i < cant; ++i)
            {
                if (i == ParentsPopulation.Length) { break; }
                newPopulation[c] = newParents[i];
                c++;
            }

            for (int i = c; i < sizePopulation; i++)
            {
                newPopulation[i] = Tools.Fisher_Yates(ParentsPopulation[0]); //) fisher_yates(ParentsPopulation[0]);
            }

            return newPopulation;
        }

        //////////////////////////DOUBLE/////////////////////////////////////
        /**
         * ReplaceInheritance method.
         * this replace method is ideal for combinatorial problems with float types in chromsome.
         * and where is not possible generate new data in the replacement population.
         * The Replace Inheritance use the parents and the mutated Inheritance. to create a new population
         *
         * @param sonMutPopulation  -> the son mutated population
         * @param ParentsPopulation -> the parents population.
         * @param sizePopulation    -> the size of the population.
         * @return double[][] new population.
         * 
         * 
         */


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sonMutPopulation"></param>
        /// <param name="ParentsPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <returns></returns>
        public double[][] ReplaceInheritance(double[][] sonMutPopulation, double[][] ParentsPopulation, int sizePopulation)
        {
            double[][] newPopulation = new double[sizePopulation][];

            double[][] newPopulation1 = new double[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                newPopulation1[i] = new double[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, newPopulation1[i], 0, sonMutPopulation[i].Length);
            }

            double[][] newPopulation2 = new double[ParentsPopulation.Length][]; //copy the parents part.
            for (int i = 0; i < ParentsPopulation.Length; i++)
            {
                newPopulation2[i] = new double[ParentsPopulation[i].Length];
                Array.Copy(ParentsPopulation[i], 0, newPopulation2[i], 0, ParentsPopulation[i].Length);
            }


            for (int i = 0; i < sonMutPopulation.Length; ++i)
            { //copy the son parts...
                newPopulation[i] = newPopulation1[i];
            }

            Random rnd = new Random();
            int rndIndex;
            for (int i = sonMutPopulation.Length; i < sizePopulation; ++i)
            {
                rndIndex = rnd.Next(ParentsPopulation.Length);
                newPopulation[i] = newPopulation2[rndIndex];
            }

            return newPopulation;
        }

        /**
 * ReplaceRandom method.
 * this replace method create a new population using the mutated inheritance and new random chromsomes.
 *
 * @param sonMutPopulation  -> the son mutated population
 * @param sizePopulation    -> the size of the population.
 * @param min				-> the minimum value in the crhomosome
 * @param max				-> the maximum value in the chromosome
 * @return double[][] new population.
 * 
 * 
 */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sonMutPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double[][] ReplaceRandom(double[][] sonMutPopulation, int sizePopulation, double min, double max)
        {
            double[][] newPopulation = new double[sizePopulation][];

            double[][] newPopulation1 = new double[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                newPopulation1[i] = new double[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, newPopulation1[i], 0, sonMutPopulation[i].Length);
            }

            for (int i = 0; i < newPopulation1.Length; ++i)
            {
                newPopulation[i] = newPopulation1[i];
            }

            double[] newChrom;
            Random rnd = new Random();
            for (int i = newPopulation1.Length; i < sizePopulation; ++i)
            {
                newChrom = new double[sonMutPopulation[0].Length];
                for (int j = 0; j < newChrom.Length; ++j)
                {
                    newChrom[j] = min + rnd.NextDouble() * (max - min);
                }
                newPopulation[i] = newChrom;
            }

            return newPopulation;
        }

        /**
 * ReplaceInheritanceRandom method.
 * this replace method is based in a mix between inheritance and random method, the percentInherit, determines how many
 * individuals will (parents) and how many new ones will be created for the new population.
 * 
 *
 * @param sonMutPopulation  -> the son mutated population
 * @param ParentsPopulation -> the parents population.
 * @param sizePopulation    -> the size of the population.
 * @param min    			-> min value in the chromosome.
 * @param max    			-> max value in the chromosome.
 * @param percentInherit    -> percentInherit.
 * @return double[][] new population.
 * 
 * 
 */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sonMutPopulation"></param>
        /// <param name="ParentsPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="percentInherit"></param>
        /// <returns></returns>
        public double[][] ReplaceInheritanceRandom(double[][] sonMutPopulation, double[][] ParentsPopulation, int sizePopulation, double min, double max, float percentInherit)
        {
            int resta = sizePopulation - sonMutPopulation.Length;
            int cant = (int)(resta * percentInherit);
            if (cant > ParentsPopulation.Length) { cant = ParentsPopulation.Length; }

            double[][] newPopulation = new double[sizePopulation][];

            double[][] sonPop = new double[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                sonPop[i] = new double[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, sonPop[i], 0, sonMutPopulation[i].Length);
            }

            double[][] newPopulation2 = new double[ParentsPopulation.Length][]; //copy the parents part.
            for (int i = 0; i < ParentsPopulation.Length; i++)
            {
                newPopulation2[i] = new double[ParentsPopulation[i].Length];
                Array.Copy(ParentsPopulation[i], 0, newPopulation2[i], 0, ParentsPopulation[i].Length);
            }

            //copiamos a los hijos
            for (int i = 0; i < sonPop.Length; ++i) { newPopulation[i] = sonPop[i]; }

            //copiamos a los padres.
            int c = sonPop.Length;
            for (int i = 0; i < cant; ++i)
            {
                if (i == ParentsPopulation.Length) { break; }
                newPopulation[c] = newPopulation2[i];
                c++;
            }

            Random rnd = new Random();
            double[] newChrom;
            for (int i = c; i < newPopulation.Length; ++i)
            {
                newChrom = new double[sonMutPopulation[0].Length];
                for (int j = 0; j < newChrom.Length; ++j)
                {
                    newChrom[j] = min + rnd.NextDouble() * (max - min);
                }
                newPopulation[i] = newChrom;
            }

            return newPopulation;
        }

        //////////////////////////FLOAT/////////////////////////////////////	
        /**
         * ReplaceInheritance method.
         * this replace method is ideal for combinatorial problems with float types in chromsome.
         * and where is not possible generate new data in the replacement population.
         * The Replace Inheritance use the parents and the mutated Inheritance. to create a new population
         *
         * @param sonMutPopulation  -> the son mutated population
         * @param ParentsPopulation -> the parents population.
         * @param sizePopulation    -> the size of the population.
         * @return float[][] new population.
         * 
         * 
         */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sonMutPopulation"></param>
        /// <param name="ParentsPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <returns></returns>
        public float[][] ReplaceInheritance(float[][] sonMutPopulation, float[][] ParentsPopulation, int sizePopulation)
        {
            float[][] newPopulation = new float[sizePopulation][];

            float[][] newPopulation1 = new float[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                newPopulation1[i] = new float[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, newPopulation1[i], 0, sonMutPopulation[i].Length);
            }

            float[][] newPopulation2 = new float[ParentsPopulation.Length][]; //copy the parents part.
            for (int i = 0; i < ParentsPopulation.Length; i++)
            {
                newPopulation2[i] = new float[ParentsPopulation[i].Length];
                Array.Copy(ParentsPopulation[i], 0, newPopulation2[i], 0, ParentsPopulation[i].Length);
            }


            for (int i = 0; i < sonMutPopulation.Length; ++i)
            { //copy the son parts...
                newPopulation[i] = newPopulation1[i];
            }

            Random rnd = new Random();
            int rndIndex;
            for (int i = sonMutPopulation.Length; i < sizePopulation; ++i)
            {
                rndIndex = rnd.Next(ParentsPopulation.Length);
                newPopulation[i] = newPopulation2[rndIndex];
            }

            return newPopulation;
        }

        /**
 * ReplaceRandom method.
 * this replace method create a new population using the mutated inheritance and new random chromsomes.
 *
 * @param sonMutPopulation  -> the son mutated population
 * @param sizePopulation    -> the size of the population.
 * @param min				-> the minimum value in the crhomosome
 * @param max				-> the maximum value in the chromosome
 * @return float[][] new population.
 * 
 * 
 */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sonMutPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public float[][] ReplaceRandom(float[][] sonMutPopulation, int sizePopulation, float min, float max)
        {
            float[][] newPopulation = new float[sizePopulation][];

            float[][] newPopulation1 = new float[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                newPopulation1[i] = new float[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, newPopulation1[i], 0, sonMutPopulation[i].Length);
            }

            for (int i = 0; i < newPopulation1.Length; ++i)
            {
                newPopulation[i] = newPopulation1[i];
            }

            float[] newChrom;
            Random rnd = new Random();
            for (int i = newPopulation1.Length; i < sizePopulation; ++i)
            {
                newChrom = new float[sonMutPopulation[0].Length];
                for (int j = 0; j < newChrom.Length; ++j)
                {
                    newChrom[j] = (float)(min + rnd.NextDouble() * (max - min));
                }
                newPopulation[i] = newChrom;
            }

            return newPopulation;
        }

        /**
 * ReplaceInheritanceRandom method.
 * this replace method is based in a mix between inheritance and random method, the percentInherit, determines how many
 * individuals will (parents) and how many new ones will be created for the new population.
 * 
 *
 * @param sonMutPopulation  -> the son mutated population
 * @param ParentsPopulation -> the parents population.
 * @param sizePopulation    -> the size of the population.
 * @param min    			-> min value in the chromosome.
 * @param max    			-> max value in the chromosome.
 * @param percentInherit    -> percentInherit.
 * @return float[][] new population.
 * 
 * 
 */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sonMutPopulation"></param>
        /// <param name="ParentsPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="percentInherit"></param>
        /// <returns></returns>
        public float[][] ReplaceInheritanceRandom(float[][] sonMutPopulation, float[][] ParentsPopulation, int sizePopulation, float min, float max, float percentInherit)
        {
            int resta = sizePopulation - sonMutPopulation.Length;
            int cant = (int)(resta * percentInherit);
            if (cant > ParentsPopulation.Length) { cant = ParentsPopulation.Length; }

            float[][] newPopulation = new float[sizePopulation][];

            float[][] sonPop = new float[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                sonPop[i] = new float[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, sonPop[i], 0, sonMutPopulation[i].Length);
            }

            float[][] newPopulation2 = new float[ParentsPopulation.Length][]; //copy the parents part.
            for (int i = 0; i < ParentsPopulation.Length; i++)
            {
                newPopulation2[i] = new float[ParentsPopulation[i].Length];
                Array.Copy(ParentsPopulation[i], 0, newPopulation2[i], 0, ParentsPopulation[i].Length);
            }

            //copiamos a los hijos
            for (int i = 0; i < sonPop.Length; ++i) { newPopulation[i] = sonPop[i]; }

            //copiamos a los padres.
            int c = sonPop.Length;
            for (int i = 0; i < cant; ++i)
            {
                if (i == ParentsPopulation.Length) { break; }
                newPopulation[c] = newPopulation2[i];
                c++;
            }

            Random rnd = new Random();
            float[] newChrom;
            for (int i = c; i < newPopulation.Length; ++i)
            {
                newChrom = new float[sonMutPopulation[0].Length];
                for (int j = 0; j < newChrom.Length; ++j)
                {
                    newChrom[j] = (float)(min + rnd.NextDouble() * (max - min));
                }
                newPopulation[i] = newChrom;
            }

            return newPopulation;
        }

        //////////////////////////INTEGER///////////////////////////////////////
        /**
         * ReplaceInheritance method.
         * this replace method is ideal for combinatorial problems with float types in chromsome.
         * and where is not possible generate new data in the replacement population.
         * The Replace Inheritance use the parents and the mutated Inheritance. to create a new population
         *
         * @param sonMutPopulation  -> the son mutated population
         * @param ParentsPopulation -> the parents population.
         * @param sizePopulation    -> the size of the population.
         * @return int[][] new population.
         * 
         * 
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sonMutPopulation"></param>
        /// <param name="ParentsPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <returns></returns>
        public int[][] ReplaceInheritance(int[][] sonMutPopulation, int[][] ParentsPopulation, int sizePopulation)
        {
            int[][] newPopulation = new int[sizePopulation][];

            int[][] newPopulation1 = new int[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                newPopulation1[i] = new int[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, newPopulation1[i], 0, sonMutPopulation[i].Length);
            }

            int[][] newPopulation2 = new int[ParentsPopulation.Length][]; //copy the parents part.
            for (int i = 0; i < ParentsPopulation.Length; i++)
            {
                newPopulation2[i] = new int[ParentsPopulation[i].Length];
                Array.Copy(ParentsPopulation[i], 0, newPopulation2[i], 0, ParentsPopulation[i].Length);
            }


            for (int i = 0; i < sonMutPopulation.Length; ++i)
            { //copy the son parts...
                newPopulation[i] = newPopulation1[i];
            }

            Random rnd = new Random();
            int rndIndex;
            for (int i = sonMutPopulation.Length; i < sizePopulation; ++i)
            {
                rndIndex = rnd.Next(ParentsPopulation.Length);
                newPopulation[i] = newPopulation2[rndIndex];
            }

            return newPopulation;
        }

        /**
 * ReplaceRandom method.
 * this replace method create a new population using the mutated inheritance and new random chromsomes.
 *
 * @param sonMutPopulation  -> the son mutated population
 * @param sizePopulation    -> the size of the population.
 * @param min				-> the minimum value in the crhomosome
 * @param max				-> the maximum value in the chromosome
 * @return int[][] new population.
 * 
 * 
 */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sonMutPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int[][] ReplaceRandom(int[][] sonMutPopulation, int sizePopulation, int min, int max)
        {
            int[][] newPopulation = new int[sizePopulation][];

            int[][] newPopulation1 = new int[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                newPopulation1[i] = new int[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, newPopulation1[i], 0, sonMutPopulation[i].Length);
            }

            for (int i = 0; i < newPopulation1.Length; ++i)
            {
                newPopulation[i] = newPopulation1[i];
            }

            int[] newChrom;
            Random rnd = new Random();
            for (int i = newPopulation1.Length; i < sizePopulation; ++i)
            {
                newChrom = new int[sonMutPopulation[0].Length];
                for (int j = 0; j < newChrom.Length; ++j)
                {
                    newChrom[j] = (int)(min + rnd.NextDouble() * (max - min));
                }
                newPopulation[i] = newChrom;
            }

            return newPopulation;
        }


        /**
 * ReplaceInheritanceRandom method.
 * this replace method is based in a mix between inheritance and random method, the percentInherit, determines how many
 * individuals will (parents) and how many new ones will be created for the new population.
 * 
 *
 * @param sonMutPopulation  -> the son mutated population
 * @param ParentsPopulation -> the parents population.
 * @param sizePopulation    -> the size of the population.
 * @param min    			-> min value in the chromosome.
 * @param max    			-> max value in the chromosome.
 * @param percentInherit    -> percentInherit.
 * @return int[][] new population.
 * 
 * 
 */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sonMutPopulation"></param>
        /// <param name="ParentsPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="percentInherit"></param>
        /// <returns></returns>
        public int[][] ReplaceInheritanceRandom(int[][] sonMutPopulation, int[][] ParentsPopulation, int sizePopulation, int min, double max, float percentInherit)
        {
            int resta = sizePopulation - sonMutPopulation.Length;
            int cant = (int)(resta * percentInherit);
            if (cant > ParentsPopulation.Length) { cant = ParentsPopulation.Length; }

            int[][] newPopulation = new int[sizePopulation][];

            int[][] sonPop = new int[sonMutPopulation.Length][]; //copy the sons part.
            for (int i = 0; i < sonMutPopulation.Length; i++)
            {
                sonPop[i] = new int[sonMutPopulation[i].Length];
                Array.Copy(sonMutPopulation[i], 0, sonPop[i], 0, sonMutPopulation[i].Length);
            }

            int[][] newPopulation2 = new int[ParentsPopulation.Length][]; //copy the parents part.
            for (int i = 0; i < ParentsPopulation.Length; i++)
            {
                newPopulation2[i] = new int[ParentsPopulation[i].Length];
                Array.Copy(ParentsPopulation[i], 0, newPopulation2[i], 0, ParentsPopulation[i].Length);
            }

            //copiamos a los hijos
            for (int i = 0; i < sonPop.Length; ++i) { newPopulation[i] = sonPop[i]; }

            //copiamos a los padres.
            int c = sonPop.Length;
            for (int i = 0; i < cant; ++i)
            {
                if (i == ParentsPopulation.Length) { break; }
                newPopulation[c] = newPopulation2[i];
                c++;
            }

            Random rnd = new Random();
            int[] newChrom;
            for (int i = c; i < newPopulation.Length; ++i)
            {
                newChrom = new int[sonMutPopulation[0].Length];
                for (int j = 0; j < newChrom.Length; ++j)
                {
                    newChrom[j] = (int)(min + rnd.NextDouble() * (max - min));
                }
                newPopulation[i] = newChrom;
            }

            return newPopulation;
        }

        //////////////////////////CHARACTER/////////////////////////////////////
        /** CharRandomReplace method.
         * set a new a population from the selected individuals.
         *
         * @param newPartOfPopulation -> selected individuals.
         * @param sizePopulation      -> the size of the population.
         * @return char[][] population.
         * 
         * 
         */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPartOfPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <returns></returns>
        public char[][] BinaryCharRandomReplace(char[][] newPartOfPopulation, int sizePopulation)
        {
            char[][] newPopulation = new char[sizePopulation][];
            char[] charRand;

            for (int i = 0; i < newPartOfPopulation.Length; i++)
            {
                newPopulation[i] = new char[newPartOfPopulation[i].Length];
                Array.Copy(newPartOfPopulation[i], 0, newPopulation[i], 0, newPartOfPopulation[i].Length);
            }

            for (int i = newPartOfPopulation.Length; i < sizePopulation; ++i)
            {
                charRand = new char[newPartOfPopulation[0].Length];
                for (int j = 0; j < newPartOfPopulation[0].Length; ++j)
                {
                    charRand[j] = Tools.RandomCharBinary(1);
                }
                newPopulation[i] = charRand;
            }
            return newPopulation;
        }

        /** CharRandomReplace method.
 * set a new a population from the selected individuals.
 *
 * @param newPartOfPopulation -> selected individuals.
 * @param sizePopulation      -> the size of the population.
 * @return char[][] population.
 * 
 * 
 */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPartOfPopulation"></param>
        /// <param name="sizePopulation"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public char[][] CharRandomReplace(char[][] newPartOfPopulation, int sizePopulation, int start, int end)
        {
            char[][] newPopulation = new char[sizePopulation][];
            char[] charRand;

            for (int i = 0; i < newPartOfPopulation.Length; i++)
            {
                newPopulation[i] = new char[newPartOfPopulation[i].Length];
                Array.Copy(newPartOfPopulation[i], 0, newPopulation[i], 0, newPartOfPopulation[i].Length);
            }

            for (int i = newPartOfPopulation.Length; i < sizePopulation; ++i)
            {
                charRand = new char[newPartOfPopulation[0].Length];
                for (int j = 0; j < newPartOfPopulation[0].Length; ++j)
                {
                    charRand[j] = Tools.RandomChar(start, end);
                }
                newPopulation[i] = charRand;
            }
            return newPopulation;
        }
    }
}
