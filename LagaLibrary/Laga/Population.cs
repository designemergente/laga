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
        /// Return the higher chromosome in the population
        /// </summary>
        /// <returns><![CDATA[Chromosome<T>]]></returns>
        public Chromosome<T> GetHighestFitnessChromosome() => highestFitnessChromosome;

        /// <summary>
        /// Return the lower chromosome in the population
        /// </summary>
        /// <returns><![CDATA[Chromosome<T>]]></returns>
        public Chromosome<T> GetLowestFitnessChromosome() => lowestFitnessChromosome;

        /// <summary>
        /// Calculates the sum of all fitness values in the population.
        /// </summary>
        /// <returns>The sum of fitness values of all chromosomes.</returns>
        public double SumFitness()
        {
            return chromosomes.Sum(Chromosome => Chromosome.Fitness);
        }

        /// <summary>
        /// return the average fitness in the population
        /// </summary>
        /// <returns>double</returns>
        public double GetAverageFitness() => totalFitness / chromosomes.Count;

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

        /// <summary>
        /// Sorts the chromosomes in the population by fitness.
        /// </summary>
        /// <param name="ascending">If true, sorts in ascending order; otherwise, descending order.</param>
        public void Sort(bool ascending = true)
        {
            if (ascending) 
            {
                chromosomes.Sort((a, b) => a.Fitness.CompareTo(b.Fitness));
            }
            else
            {
                chromosomes.Sort((a, b) => a.Fitness.CompareTo(b.Fitness));
            }
        }
        /// <summary>
        /// Population count
        /// </summary>
        public int Count => chromosomes.Count;

        /// <summary>
        /// Add a chromosome to the population
        /// </summary>
        /// <typeparamref name="T">The type for Chr</typeparamref>
        /// <param name="chromosome"></param>
        public void Add(Chromosome<T> chromosome)
        {
            if (popSize > 0 && chromosomes.Count >= popSize)
                throw new InvalidOperationException("Population size limit reached");

            UpdateFitnessStatistics(chromosome);

            chromosomes.Add(chromosome);
        }

        private void UpdateFitnessStatistics(Chromosome<T> newChromosome)
        {
            totalFitness += newChromosome.Fitness;
            if (highestFitnessChromosome == null || newChromosome.Fitness > highestFitnessChromosome.Fitness)
                highestFitnessChromosome = newChromosome;
            if (lowestFitnessChromosome == null || newChromosome.Fitness < lowestFitnessChromosome.Fitness)
                lowestFitnessChromosome = newChromosome;
        }

        /// <summary>
        /// Delete a chromosome from the population
        /// </summary>
        /// <param name="index"></param>
        public void Delete(int index)
        {
            chromosomes.RemoveAt(index);
        }

        /// <summary>
        /// Get the chromosome from the population.
        /// </summary>
        /// <param name="index"></param>
        /// <returns><![CDATA[Chromosome<T>]]></returns>
        public Chromosome<T> GetChromosome(int index)
        {
            return chromosomes[index];
        }

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

        IEnumerator<Chromosome<T>> IEnumerable<Chromosome<T>>.GetEnumerator()
        {
            return chromosomes.GetEnumerator();
        }

        /// <summary>
        /// IEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return chromosomes.GetEnumerator();
        }

        //Natural Selection part

        /// <summary>
        /// Select a chromosome using the specified selection method.
        /// </summary>
        /// <param name="selectionType">The type of selection to perform ("roulette" or "tournament").</param>
        /// <returns>The selected chromosome.</returns>
        public Chromosome<T> SelectChromosome(string selectionType = "roulette")
        {
            switch (selectionType.ToLower())
            {
                case "roulette":
                    return RouletteWheelSelection();
                case "tournament":
                    return TournamentSelection();
                default:
                    throw new InvalidOperationException("Selection method not supported.");
            }
        }

        /// <summary>
        /// Roulette Wheel Selection: Selects a chromosome based on relative fitness.
        /// </summary>
        /// <returns>A chromosome selected by roulette wheel.</returns>
        private Chromosome<T> RouletteWheelSelection()
        {
            double totalFitness = SumFitness();  // Get total fitness of the population
            double randomValue = new Random().NextDouble() * totalFitness;  // Random value between 0 and total fitness

            double cumulativeFitness = 0;
            foreach (var chromosome in chromosomes)
            {
                cumulativeFitness += chromosome.Fitness;
                if (cumulativeFitness >= randomValue)
                    return chromosome;
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


    }
}
