using Laga.Numbers;
using Laga.GeneticAlgorithm;
using Laga.Geometry;


namespace LagaExamples
{
    public static class TestingPopulation
    {
        public static void Run()
        {
            Console.WriteLine("\n /// - Examples of Laga.GeneticAlgorithm.Population() - /// \n");

            Population<char> pop = new Population<char>(10);
            for (int i = 0; i < 10; i++)
                pop.Add(new Chromosome<char>(FitnessFunc, GenrGenes.Rand_Char(10, 97, 123).ToList()));

            Console.WriteLine(pop.ToString());

            Console.WriteLine("To create a population, you have to intialize the fitness in the chromosome.");
            Console.WriteLine("In this case, the population is created while each chromosome is evaluated against the word \"rhinoceros\" \n");

            double sum = pop.SumFitness();
            Console.WriteLine("The sumatory of the fitness in the population: " + sum + "\n");

            double average = pop.GetAverageFitness();
            Console.WriteLine("The average fitness in the population is: " + average + "\n");

            Chromosome<char> chr = pop.GetHighestFitnessChromosome();
            Console.WriteLine("The highest fitness chromosome is: \n" + chr + "\n");

            Population<double> dblPop = new Population<double>(5);
            Chromosome<double> dblChr;
            for (int i = 0; i < 5; i++)
            {
                dblChr = GenrGenes.RandomChromosome<double>(3, 6, 7, Rand.NextDouble);
                dblChr.Fitness = 0.00;
                dblPop.Add(dblChr);
            }

            Console.WriteLine("Apopulation of doubles with fitness 0");
            Console.WriteLine(dblPop.ToString() + "\n");

            Console.WriteLine("\n /// - Press any key to return to the menu - /// ");
        }

        //calculate the fitness.
        private static Func<Chromosome<char>, double> FitnessFunc = chromosome =>
        {
            char[] target = "rhinoceros".ToCharArray();
            int matchCount = 0;

            for (int i = 0; i < target.Length && i < chromosome.Count; i++)
            {
                if (chromosome.GetGene(i) == target[i])
                    matchCount++;
            }

            return (double)matchCount / target.Length;
        };
    }
}
