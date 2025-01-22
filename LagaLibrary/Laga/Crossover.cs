using System;
using System.Collections.Generic;
using Laga.Numbers;

namespace Laga
{
    /// <summary>
    /// Different crossover type operations
    /// </summary>
    public static class Crossover<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentA"></param>
        /// <param name="parentB"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static (Chromosome<T>, Chromosome<T>) OneRandPoint(Chromosome<T> parentA, Chromosome<T> parentB)
        {
            if (parentA.Count != parentB.Count)
                throw new ArgumentException("Chromosomes must have the same length for crossover.");

            int crossoverPoint = Rand.NextInt(1, parentA.Count - 1);

            var offspring1Genes = new List<T>();
            var offspring2Genes = new List<T>();

            for (int i = 0; i < parentA.Count; i++)
            {
                if (i < crossoverPoint)
                {
                    offspring1Genes.Add(parentA.GetGene(i));
                    offspring2Genes.Add(parentB.GetGene(i));
                }
                else
                {
                    offspring1Genes.Add(parentB.GetGene(i));
                    offspring2Genes.Add(parentA.GetGene(i));
                }
            }

            return (new Chromosome<T>(offspring1Genes), new Chromosome<T>(offspring2Genes));
        }

        //old code
        /*private int[] arrIndex;

        /// <summary>
        /// get and set indexes for parents in crossover.
        /// </summary>
        public int[] IndexParent
        {
            get => arrIndex;
            set
            {
                arrIndex = value;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public Crossover()
        {
        }

        /// <summary>
        /// Performs a Single point crossover over a mating pool or population.
        /// </summary>
        /// <typeparamref name="T">The choromsome type</typeparamref>
        /// <param name="matPool">The mating pool is formed by candidate solutions to have the highest fitness </param>
        /// <param name="popSize">the population size</param>
        /// <param name="cut">the index to cut the Chr</param>
        /// <returns>population</returns>
        public static Population<T> SinglePoint(Population<T> matPool, int popSize, int cut)
        {
            int sizeMatPool = matPool.Count;
            Population<T> selectedPop = new Population<T>(popSize);
            Chromosome<T> crA, crB;
            Chromosome<T> child;

            for (int i = 0; i < popSize; i++)
            {
                crA = matPool.GetChromosome(Rand.NextInt(0, sizeMatPool));
                crB = matPool.GetChromosome(Rand.NextInt(0, sizeMatPool));
                child = new Chromosome<T>();
                
                for (int j = 0; j < crA.Count; j++)
                {
                    if (j >= cut)
                    {
                        child.Add(crA.GetGene(j));
                    }
                    else
                    {
                        child.Add(crB.GetGene(j));
                    }
                }
                selectedPop.Add(child);
            }
            return selectedPop;
        }
        */

        #region testing algorithms
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matingPool"></param>
        /// <param name="populationSize"></param>
        /// <returns>Population</returns>
        public static Population<T> RandomPointCrossover(Population<T> matingPool, int populationSize)
        {
            Random rnd = new Random();
            int natSelectionCount = matingPool.Count;
            int ChromosomeCut = matingPool.GetChromosome(0).Count;
            Population<T> popCrossover = new Population<T>(populationSize);
 
            for (int i = 0; i < populationSize; i ++)
            {
                int a = rnd.Next(natSelectionCount);
                int b = rnd.Next(natSelectionCount);

                Chromosome<T> chrA = matingPool.GetChromosome(a);
                Chromosome<T> chrB = matingPool.GetChromosome(b);

                popCrossover.Add(SinglePointCrossover(chrA, chrB, rnd.Next(ChromosomeCut)));

            }

            return popCrossover;

        }

        /// <summary>
        /// Sinngle Point Chrossover at specific location
        /// </summary>
        /// <param name="chromosomeA">Parent A</param>
        /// <param name="chromosomeB">Parent B</param>
        /// <param name="cut">Cutting location</param>
        /// <returns>Chromosome</returns>
        public static Chromosome<T> SinglePointCrossover(Chromosome<T> chromosomeA, Chromosome<T> chromosomeB, int cut)
        {
            Chromosome<T> child = new Chromosome<T>();

            for(int i = 0; i < chromosomeA.Count; i++)
            {
                if( i > cut)
                {
                    child.Add(chromosomeA.GetGene(i));
                }
                else
                {
                    child.Add(chromosomeB.GetGene(i));
                }
            }

            return child;
        }
        */
        #endregion

        #region Single Point crossover
        /*
        /// <summary>
        /// A crossover algorithm performed in a single point of the Chr.
        /// </summary>
        /// <param name="population">The population to perform the crossover</param>
        /// <param name="percent">Which percent of chromosomes will be croosver</param>
        /// <param name="pointCutter">Where the crossover will be executed</param>
        /// <returns>object[][]</returns>
        public object[][] SinglePointCrossover(object[][] population, float percent, int pointCutter)
        {
            int popLength = population.Length;
            arrIndex = Tools.Mom_Dad(popLength, percent);
            int iLength = arrIndex.Length; 
            object[] dad;
            object[] mom;
            object[] son1;
            object[] son2;

            object[][] inherencePop = new object[iLength][];

            object[][] crossPop = population.Select(a => a.ToArray()).ToArray();

            for (int i = 0; i < iLength - 1; i += 2)
            {
                dad = crossPop[arrIndex[i]];
                mom = crossPop[arrIndex[i + 1]];
                son1 = new object[dad.Length];
                son2 = new object[mom.Length];

                int t = pointCutter;
                int t2 = pointCutter;

                for (int j = 0; j < pointCutter; ++j)
                {
                    son1[j] = dad[j];
                    son2[j] = mom[j];
                }

                for (int j = 0; j < mom.Length; ++j)
                {
                    for (int k = pointCutter; k < dad.Length; ++k)
                    {
                        if (mom[j].Equals(dad[k]))
                        {
                            son1[t] = mom[j];
                            t++;
                        }
                    }
                }

                for (int j = 0; j < dad.Length; ++j)
                {
                    for (int k = pointCutter; k < mom.Length; ++k)
                    {
                        if (dad[j].Equals(mom[k]))
                        {
                            son2[t2] = dad[j];
                            t2++;
                        }
                    }
                }
                inherencePop[i] = son1;
                inherencePop[i + 1] = son2;
            }
            return inherencePop;
        }

        /// <summary>
        /// A crossover algorithm performed in a single point of the Chr.
        /// </summary>
        /// <param name="population">The population to perform the crossover</param>
        /// <param name="percent">Which percent of chromosomes will be croosver</param>
        /// <param name="pointCutter">Where the crossover will be executed</param>
        /// <returns>double[][]</returns>
        public double[][] SinglePointCrossover(double[][] population, float percent, int pointCutter)
        {
            int popLength = population.Length;
            arrIndex = Tools.Mom_Dad(popLength, percent);
            int iLength = arrIndex.Length;

            double[] dad;
            double[] mom;
            double[] son1;
            double[] son2;

            double[][] inherencePop = new double[iLength][];

            //clone the array.
            double[][] crossPop = population.Select(a => a.ToArray()).ToArray();

            for (int i = 0; i < iLength - 1; i += 2)
            {
                dad = crossPop[arrIndex[i]];
                mom = crossPop[arrIndex[i + 1]];
                son1 = new double[dad.Length];
                son2 = new double[mom.Length];

                for (int j = 0; j < pointCutter; ++j)
                {
                    son1[j] = dad[j];
                    son2[j] = mom[j];
                }

                for (int k = pointCutter; k < dad.Length; ++k)
                {
                    son1[k] = mom[k];
                    son2[k] = dad[k];
                }

                inherencePop[i] = son1;
                inherencePop[i + 1] = son2;
            }
            return inherencePop;
        }

        /// <summary>
        /// A crossover algorithm performed in a single point of the Chr.
        /// </summary>
        /// <param name="population">The population to perform the crossover</param>
        /// <param name="percent">Which percent of chromosomes will be croosver</param>
        /// <param name="pointCutter">Where the crossover will be executed</param>
        /// <returns>float[][]</returns>
        public float[][] SinglePointCrossover(float[][] population, float percent, int pointCutter)
        {
            int popLength = population.Length;
            arrIndex = Tools.Mom_Dad(popLength, percent);
            int iLength = arrIndex.Length;
            float[] dad;
            float[] mom;
            float[] son1;
            float[] son2;

            float[][] inherencePop = new float[iLength][];

            //clone the array.
            float[][] crossPop = population.Select(a => a.ToArray()).ToArray();

            for (int i = 0; i < iLength - 1; i += 2)
            {
                dad = crossPop[arrIndex[i]];
                mom = crossPop[arrIndex[i + 1]];
                son1 = new float[dad.Length];
                son2 = new float[mom.Length];

                for (int j = 0; j < pointCutter; ++j)
                {
                    son1[j] = dad[j];
                    son2[j] = mom[j];
                }

                for (int k = pointCutter; k < dad.Length; ++k)
                {
                    son1[k] = mom[k];
                    son2[k] = dad[k];
                }

                inherencePop[i] = son1;
                inherencePop[i + 1] = son2;
            }
            return inherencePop;
        }

        /// <summary>
        /// A crossover algorithm performed in a single point of the Chr.
        /// </summary>
        /// <param name="population">The population to perform the crossover</param>
        /// <param name="percent">Which percent of chromosomes will be croosver</param>
        /// <param name="pointCutter">Where the crossover will be executed</param>
        /// <returns>int[][]</returns>
        public int[][] SinglePointCrossover(int[][] population, float percent, int pointCutter)
        {
            int popLength = population.Length;
            arrIndex = Tools.Mom_Dad(popLength, percent);
            int iLength = arrIndex.Length;

            int[] dad;
            int[] mom;
            int[] son1;
            int[] son2;

            int[][] inherencePop = new int[iLength][];

            //clone the array.
            int[][] crossPop = population.Select(a => a.ToArray()).ToArray();

            for (int i = 0; i < iLength - 1; i += 2)
            {
                dad = crossPop[arrIndex[i]];
                mom = crossPop[arrIndex[i + 1]];
                son1 = new int[dad.Length];
                son2 = new int[mom.Length];

                for (int j = 0; j < pointCutter; ++j)
                {
                    son1[j] = dad[j];
                    son2[j] = mom[j];
                }

                for (int k = pointCutter; k < dad.Length; ++k)
                {
                    son1[k] = mom[k];
                    son2[k] = dad[k];
                }

                inherencePop[i] = son1;
                inherencePop[i + 1] = son2;
            }
            return inherencePop;
        }

        /// <summary>
        /// A crossover algorithm performed in a single point of the Chr.
        /// </summary>
        /// <param name="population">The population to perform the crossover</param>
        /// <param name="percent">Which percent of chromosomes will be croosver</param>
        /// <param name="pointCutter">Where the crossover will be executed</param>
        /// <returns>Char[][]</returns>
        /// <example>
        /// <code>
        /// GenrPopulation chromosomes = new GenrPopulation(6);
        /// char[][] charPop = chromosomes.CharPopulation(5, 97, 122);
        /// float[] rndFitness = Rand.RandomNumbers(6, 0f, 1f);
        /// 
        /// sort:
        /// RankingSort rs = new RankingSort();
        /// rs.BidirectionalBubbleSort(charPop, rndFitness, false);
        /// 
        /// Crossover cs = new Crossover();
        /// char[][] croossovers = cs.SinglePointCrossover(srtPop, 0.8f, 2);
        /// 
        /// result:
        /// POPULATION:
        /// vnqaw: 0.6631602
        /// smzbu: 0.9322885
        /// cewwe: 0.8222669
        /// jsxgr: 0.7555377
        /// ujklr: 0.181477
        /// uqmvo: 0.6832687
        /// 
        /// SORTED POPULATION:
        /// ujklr: 0.181477
        /// vnqaw: 0.6631602
        /// uqmvo: 0.6832687
        /// jsxgr: 0.7555377
        /// cewwe: 0.8222669
        /// smzbu: 0.9322885
        /// 
        /// CROSSOVER EXAMPLE: 80%
        /// smwwe // smzbu - cewwe
        /// cezbu // cewwe - smzbu
        /// jsklr // jsxgr - ujklr
        /// ujxgr // ujklr - jsxgr
        /// 
        /// </code>
        /// </example>
        public char[][] SinglePointCrossover(char[][] population, float percent, int pointCutter)
        {
            int popLength = population.Length;
            arrIndex = Tools.Mom_Dad(popLength, percent);
            int iLength = arrIndex.Length;

            char[][] inherencePop = new char[iLength][];
            int count = 0;

            //deep copy the array.
            char[][] crossPop = population.Select(a => a.ToArray()).ToArray();

            for (int i = 0; i < iLength - 1; i += 2)
            {

                char[] dad = crossPop[arrIndex[i]];
                char[] mom = crossPop[arrIndex[i + 1]];

                char[] son1 = new char[dad.Length];
                char[] son2 = new char[mom.Length];

                for (int j = 0; j < pointCutter; ++j)
                {
                    son1[j] = dad[j];
                    son2[j] = mom[j];
                }

                for (int k = pointCutter; k < dad.Length; ++k)
                {
                    son1[k] = mom[k];
                    son2[k] = dad[k];
                }

                inherencePop[count] = son1;
                inherencePop[count + 1] = son2;

                count += 2;
            }
            return inherencePop;
        }
        */
        #endregion

    }

}