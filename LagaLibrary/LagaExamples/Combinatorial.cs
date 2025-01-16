using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laga.GeneticAlgorithm;

namespace LagaExamples
{
    public static class Combinatorial
    {
        public static void Run()
        {
            Console.WriteLine("\n /// - Example of Combinatorial Problem - /// \n");
            Console.WriteLine("Fitness: pneumonoultramicroscopicsilicovolcanoconiosis (find this word) \n");

            int c = 0;
            int popSize = 200;
            double pr = 0.05;
            double chr = 0.001;
            Console.WriteLine("GA parameters:");
            Console.WriteLine("Population size: {0}", popSize);
            Console.WriteLine("Mutation in population rate: {0} , Mutation in Chromosome rate: {1}", pr, chr);

            //initialize the population:
            Population<char> population = new Population<char>();
            for (int i = 0; i < popSize; i++)
                population.Add(new Chromosome<char>(FitnessFunc, GenrGenes.Rand_Char(45, 97, 122).ToList()));
           double topFitness = 0;

            while (topFitness < 0.999) //Genetic loop
            {
                topFitness = population.HighestFitnessChromosome().Fitness;
                PrintData(population, c);

               population.Selection("roulette", tournamentSize: 10, elitism: true, eliteCount: 60); //selection
               population.Crossover("onePointCrossover", 0.75); //crossover
               population.Mutation("charRandom", populationRate: pr, chromosomeRate: chr, 97, 122);//mutation
               population.Evaluation(FitnessFunc); //evaluation

                c++;
            }

        }
        private static void PrintData(Population<char> pop, int c)
        {
            Console.SetCursorPosition(0, 16);
            Chromosome<char> chr = pop.HighestFitnessChromosome();
            Console.WriteLine("Iter:(" + c + ") > HighestFitness: {0} ,  Average fitness: {1}", chr.Fitness, pop.GetAverageFitness());
            Console.WriteLine(chr.ToString());
        }

        private static Func<Chromosome<char>, double> FitnessFunc = chromosome =>
        {
            string target = "pneumonoultramicroscopicsilicovolcanoconiosis";
            double c = 0;

            for (int i = 0; i < chromosome.Count; i++)
            {
                if(chromosome.GetGene(i) == target[i]) 
                    c++;
            }
            return c / (double)target.Length;
        };
    }
}
