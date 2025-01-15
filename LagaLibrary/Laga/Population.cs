using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Laga.GeneticAlgorithm
{
    /// <summary>
    /// Create and Manipulate Populations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Population<T> : IEnumerable<Chromosome<T>>
    {
        readonly private List<Chromosome<T>> chromosomes;
        readonly private int popSize;
        private Chromosome<T> highestFitnessChromosome;
        private Chromosome<T> lowestFitnessChromosome;
        private double totalFitness;

        /// <summary>
        /// Population count
        /// </summary>
        public int Count => chromosomes.Count;
        /// <summary>
        /// Return the higher chromosome in the population
        /// </summary>
        /// <returns><![CDATA[Chromosome<T>]]></returns>
        public Chromosome<T> HighestFitnessChromosome() => highestFitnessChromosome;
        /// <summary>
        /// Return the lower chromosome in the population
        /// </summary>
        /// <returns><![CDATA[Chromosome<T>]]></returns>
        public Chromosome<T> LowestFitnessChromosome() => lowestFitnessChromosome;
        /// <summary>
        /// Calculates the sum of all fitness values in the population.
        /// </summary>
        /// <returns>The sum of fitness values of all chromosomes.</returns>
        
        public double SumFitness() => totalFitness;
        
        /// <summary>
        /// return the average fitness in the population
        /// </summary>
        /// <returns>double</returns>
        public double GetAverageFitness() => totalFitness / chromosomes.Count;

        #region Constructors
        /// <summary>
        /// Construct a predifined size population 
        /// </summary>
        /// <param name="SizePopulation"></param>
        public Population(int SizePopulation)
        {
            this.popSize = SizePopulation;
            chromosomes = new List<Chromosome<T>>(SizePopulation);
        }
        /// <summary>
        /// Construct a population with no size
        /// </summary>
        public Population()
        {
            chromosomes = new List<Chromosome<T>>();
        }
        #endregion

        #region Add, Sort and delete methods
        /// <summary>
        /// Sorts the chromosomes in the population by fitness.
        /// </summary>
        /// <param name="ascending">If true, sorts in ascending order; otherwise, descending order.</param>
        public void Sort(bool ascending = true)
        {
            if (ascending) 
                chromosomes.Sort((a, b) => a.Fitness.CompareTo(b.Fitness));
            else
                chromosomes.Sort((a, b) => b.Fitness.CompareTo(a.Fitness));
        }

        /// <summary>
        /// Add a chromosome to the population
        /// </summary>
        /// <typeparamref name="T">The type for Chr</typeparamref>
        /// <param name="chromosome"></param>
        public void Add(Chromosome<T> chromosome)
        {
            if (popSize > 0 && chromosomes.Count >= popSize)
                throw new InvalidOperationException("Population size limit reached");

            chromosomes.Add(chromosome);
            UpdateFitnessStatistics(chromosome);

        }

        /// <summary>
        /// Add a range of chromosomes to the population.
        /// </summary>
        /// <param name="range"></param>
        
        public void AddRange(IEnumerable<Chromosome<T>> range)
        {
            foreach (Chromosome<T> chromosome in range) 
            {
                if (chromosomes.Count >= popSize)
                    throw new InvalidOperationException("Population size limited reached"); 
                
                chromosomes.Add(chromosome); 
            }
            RecalculateFitnessStatistics();
        }
        /// <summary>
        /// Delete a chromosome from the population
        /// </summary>
        /// <param name="index"></param>
       
        public void Delete(int index)
        {
            chromosomes.RemoveAt(index);
            RecalculateFitnessStatistics();
        }
        #endregion

        private void UpdateFitnessStatistics(Chromosome<T> newChromosome)
        {
            totalFitness += newChromosome.Fitness;
            if (highestFitnessChromosome == null || newChromosome.Fitness > highestFitnessChromosome.Fitness)
                highestFitnessChromosome = newChromosome;
            if (lowestFitnessChromosome == null || newChromosome.Fitness < lowestFitnessChromosome.Fitness)
                lowestFitnessChromosome = newChromosome;
        }

        private void RecalculateFitnessStatistics()
        {
            totalFitness = chromosomes.Sum(c => c.Fitness);
            highestFitnessChromosome = chromosomes.OrderByDescending(c => c.Fitness).FirstOrDefault();
            lowestFitnessChromosome = chromosomes.OrderBy(c => c.Fitness).FirstOrDefault();
        }

        public void Evaluation(Func<Chromosome<T>, double> fitnessFunction)
        {
            foreach (var chromosome in chromosomes)
            {
                chromosome.Fitness = fitnessFunction(chromosome);
            }
            RecalculateFitnessStatistics();
        }

        /// <summary>
        /// Get the chromosome from the population.
        /// </summary>
        /// <param name="index"></param>
        /// <returns><![CDATA[Chromosome<T>]]></returns>
        public Chromosome<T> GetChromosome(int index) => chromosomes[index];


        /// <summary>
        /// Print a population
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Population:");

            for (int i = 0; i < chromosomes.Count; i++)
                sb.AppendLine($"Chromosome {i}: {chromosomes[i]}");
            
            return sb.ToString();
        }

        IEnumerator<Chromosome<T>> IEnumerable<Chromosome<T>>.GetEnumerator() => chromosomes.GetEnumerator();
        /// <summary>
        /// IEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator() => chromosomes.GetEnumerator();

        #region Natural Selection part
        /// <summary>
        /// Select a chromosome using the specified selection method.
        /// </summary>
        /// <param name="method">The type of selection to perform ("roulette" or "tournament").</param>
        /// <param name="invert">invert the fitness for roulettewheel method</param>
        /// <param name="tournamentSize"></param>
        /// <param name="elitism"></param>
        /// <param name="eliteCount"></param>
        public void Selection(string method, bool invert = false, int tournamentSize = 3, bool elitism = false,  int eliteCount = 1)
        {
            List<Chromosome<T>> newChromosomes = new List<Chromosome<T>>();
            PrecomputedFitness pf = PrecomputedFitnessValues(invert);

            //step 1: retain elite chromosomes
            if (elitism)
            {
                List<Chromosome<T>> nList = new List<Chromosome<T>>(chromosomes);
                nList = nList.AsParallel().OrderByDescending(c => invert ? -c.Fitness : c.Fitness).ToList();

                for (int i = 0;i < eliteCount && i < nList.Count; i++)
                    newChromosomes.Add(nList[i]);
            }

            //step 2: Fill the remaining slots using the selected method
            while (newChromosomes.Count < this.Count)
            {
                Chromosome<T> selected;
                switch (method.ToLower())
                {
                    case "roulette":
                        selected = RouletteWheelSelection(pf);
                        break;
                    case "tournament":
                        selected = TournamentSelection(tournamentSize);
                        break;
                    default:
                        throw new InvalidOperationException($"Selection method '{method}' not supported.");
                }
                newChromosomes.Add(selected);
            }

            //step 3: replace population
            chromosomes.Clear();
            chromosomes.AddRange(newChromosomes);
            RecalculateFitnessStatistics();
        }

        private struct PrecomputedFitness
        {
            public List<double> FitnessValues{ get; }
            public double TotalFitness { get; }
            public PrecomputedFitness(List<double> fitnessValues, double totalFitness)
            {
                FitnessValues = fitnessValues;
                TotalFitness = totalFitness;
            }
        }

        private PrecomputedFitness PrecomputedFitnessValues(bool invert)
        {
            double totalFitness = invert
        ? chromosomes.Sum(c => 1.0 / c.Fitness)
        : chromosomes.Sum(c => c.Fitness);

            List<double> fitnessValues = chromosomes
                .Select(c => invert ? 1.0 / c.Fitness : c.Fitness)
                .ToList();

            return new PrecomputedFitness(fitnessValues, totalFitness);
        }

        /// <summary>
        /// Roulette Wheel Selection: Selects a chromosome based on relative fitness.
        /// </summary>
        /// <returns>A chromosome selected by roulette wheel.</returns>
        private Chromosome<T> RouletteWheelSelection(PrecomputedFitness fitnessData)
        {
            // Get total fitness of the population
            double randomValue = Numbers.Rand.NextDouble() * fitnessData.TotalFitness; // new Random().NextDouble() * totalFitness; // Random value between 0 and total fitness

            double cumulativeFitness = 0;
            for(int i = 0; i< chromosomes.Count; i++)
            {
                cumulativeFitness += fitnessData.FitnessValues[i];
                if (cumulativeFitness >= randomValue)
                    return chromosomes[i];
            }

            // Fallback: Return the last chromosome if something goes wrong (should not happen in theory)
            return chromosomes.Last();
        }

        /// <summary>
        /// Tournament Selection: Selects the best chromosome from a random subset.
        /// </summary>
        /// <param name="tournamentSize">The number of chromosomes in the tournament.</param>
        /// <returns>A chromosome selected by tournament.</returns>
        private Chromosome<T> TournamentSelection(int tournamentSize = 3)
        {
            Random rand = new Random();
            List<Chromosome<T>> tournamentChromosomes = new List<Chromosome<T>>();

            // Randomly select chromosomes for the tournament
            for (int i = 0; i < tournamentSize; i++)
            {
                int randomIndex = rand.Next(chromosomes.Count);
                tournamentChromosomes.Add(chromosomes[randomIndex]);
            }

            // Return the chromosome with the highest fitness from the tournament
            return tournamentChromosomes.OrderByDescending(c => c.Fitness).First();
        }

        #endregion

        #region Crossover
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="rate"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Crossover(string method, double rate)
        {
            List<Chromosome<T>> newChromosome = new List<Chromosome<T>>();

            for (int i = 0; i < chromosomes.Count; i += 2)
            {
                //select two parents
                Chromosome<T> parent1 = chromosomes[i];
                Chromosome<T> parent2 = chromosomes[(i + 1) % chromosomes.Count];

                if (Numbers.Rand.NextDouble() < rate)
                {
                    Chromosome<T> child1, child2;
                    switch (method.ToLower())
                    {
                        case "onepointcrossover":
                            (child1, child2) = parent1.OnePointCrossover(parent2);
                            break;
                        case "twopointcrossover":
                            (child1, child2) = parent1.TwoPointCrossover(parent2);
                            break;
                        default:
                            throw new InvalidOperationException($"Crossover method '{method}' not supported.");
                    }

                    newChromosome.Add(child1);
                    newChromosome.Add(child2);
                }
                else
                {
                    // No crossover, retain parents
                    newChromosome.Add(parent1);
                    newChromosome.Add(parent2);
                }
            }
            chromosomes.Clear();
            chromosomes.AddRange(newChromosome);
        }
        
        #endregion

        #region Mutation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="populationRate"></param>
        /// <param name="chromosomeRate"></param>
        public void Mutation(string method, double populationRate = 0.1, double chromosomeRate = 0.01)
        {
            List<Chromosome<T>> newChromosome = new List<Chromosome<T>>();

            for (int i = 0; i < chromosomes.Count; i++)
            {
                Chromosome<T> mutatedChromosome = chromosomes[i];
                if (Numbers.Rand.NextDouble() < populationRate)
                {
                    switch (method.ToLower())
                    {
                        case "binary": //only for binary chromosomes...
                            mutatedChromosome = (Chromosome<T>)(object)mutatedChromosome.BinaryMutation(chromosomeRate);
                            break;
                        default:
                            throw new InvalidOperationException($"Crossover method '{method}' not supported.");
                    }
                }
                newChromosome.Add(mutatedChromosome);
            }
            chromosomes.Clear();
            chromosomes.AddRange(newChromosome);
        }
        #endregion
    }
}
