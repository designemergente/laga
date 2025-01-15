using Laga.GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagaExamples
{
    public static class MathEqualityProblem
    {
        private static string message;
        public static void Run()
        {
            Console.WriteLine("\n /// - Example of Math Equality Problem - /// \n");
            Console.WriteLine("Fitness: A2 + B3 + C4 = 60 (What are the values for A, B and C) \n");
            Console.WriteLine("GA parameters:");
            Console.WriteLine("Population size: 20");
            Console.WriteLine("Chromosome size: 15");
            
            Population<int> population = new Population<int>();
            for (int i = 0; i < 10; i++)
                population.Add(new Chromosome<int>(FitnessFunc, GenrGenes.Binary_Integer(15).ToList()));

            int c = 0;

            //population.Sort(false);
            Console.WriteLine("iter (" + c + "): " + message);

            //natural selection
            population.Selection("roulette", invert : true, elitism : true, eliteCount: 2);

            //crossover
            population.Crossover("onePointCrossover", 0.70);

            //mutation
            population.Mutation("binary");

            //replacement
            //population.Mutation("Binary", PopulationMutationRate = 0.01, ChromosomePopulationRate = 0.001);

            //do
            //{


            //} while (population.GetLowestFitnessChromosome().Fitness == 0);
        }

        //calculate the fitness.
        private static Func<Chromosome<int>, double> FitnessFunc = chromosome =>
        {
            List<int> first = chromosome.GetGenes(0, 5);
            List<int> second = chromosome.GetGenes(6, 10);
            List<int> third = chromosome.GetGenes(11, 14);

            int a = Laga.Numbers.Tools.BinaryToInteger(first);
            int b = Laga.Numbers.Tools.BinaryToInteger(second);
            int c = Laga.Numbers.Tools.BinaryToInteger(third);

            int res = 2 * a + 3 * b + 4 * c;
            message = String.Format("{0}*2 + {1}*3 + {2}*4 = {3}", a, b, c, res);
            return Math.Abs(res - 60); //we want to go closer to 0 here.
        };



    }
}

