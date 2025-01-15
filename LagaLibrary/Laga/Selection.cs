using System;
using System.Collections.Generic;
using System.Linq;
using Laga.Numbers;

namespace Laga.GeneticAlgorithm
{
    /// <summary>
    /// Natural selection class
    /// </summary>
    public class Selection<T>
    {
        /// <summary>
        /// The class to select and operates on populations
        /// </summary>
        public Selection()
        {

        }

        /// <summary>
        /// Performs a roulette wheel selection over a population.
        /// Chromosomes with higher fitness have a proportionally higher chance of selection.
        /// </summary>
        /// <param name="population">The popolution to perform roulette wheel selection on</param>
        /// <param name="maxItem">>Maximum number of chromosomes to select for the mating pool</param>
        /// <param name="normalizeFitness">If true, uses normalized fitness probabilities for selection</param>
        /// <returns>Population</returns>
        public static Population<T> RouletteWheel(Population<T> population, int maxItem, bool normalizeFitness = true)
        {
            Population<T> matingPool = new Population<T>(population.Count);
            
            double totalFitness = normalizeFitness ? population.SumFitness() : population.HighestFitnessChromosome().Fitness;
            double selectionProbability;
            int numberOfCopies;

            for (int i = 0; i < population.Count; i++)
            {
                Chromosome<T> chromosome = population.GetChromosome(i);
                selectionProbability = chromosome.Fitness / totalFitness;
                numberOfCopies = (int)Math.Round(selectionProbability * maxItem);

                //Ensures at least one copy is added to the mating pool if selection probability is low
                numberOfCopies = Math.Max(numberOfCopies, 1);

                for (int j = 0; j < numberOfCopies; ++j)
                    matingPool.Add(chromosome);
            }

            return matingPool;
        }

        /// <summary>
        /// Performs a tournament selection, where a subset of chromosomes compete,
        /// and the chromosome with the highest fitness in the subset is selected.
        /// </summary>
        /// <param name="population">The population to perform tournament selection on</param>
        /// <param name="tournamentSize">The number of chromosomes in each tournament, sometimes called pressure</param>
        /// <param name="selectionCount">The number of chromosomes to select for the mating pool</param>
        /// <returns>A new population representing the mating pool</returns>
        public static Population<T> TournamentSelection(Population<T> population, int tournamentSize, int selectionCount)
        {
            //clone the array.
            Population<T> matingPool = new Population<T>(selectionCount);
            Chromosome<T> fittest;
            
            int randindex;

            for (int i = 0; i < selectionCount; i++)
            {
                List<Chromosome<T>> tournament = new List<Chromosome<T>>();

                //select random chromosomes for the tournament
                for(int j = 0; j < tournamentSize; ++j)
                {
                    randindex = Rand.NextInt(0, population.Count);
                    tournament.Add(population.GetChromosome(randindex));
                }

                fittest = tournament.OrderByDescending(chromosome  => chromosome.Fitness).First();
                matingPool.Add(fittest);
            }
            return matingPool;
        }
    }
}