using Laga;

namespace LagaExamples
{
    public static class MathEqualityProblem
    {
        public static void Run()
        {
            Console.WriteLine("\n /// - Example of Math Equality Problem - /// \n");
            Console.WriteLine("Fitness: A2 + B3 + C4 = 60 (What are the values for A, B and C) \n");

            int c = 0;
            int popSize = 5;
            double pr = 0.2;
            double chr = 0.1;
            Console.WriteLine("GA parameters:");
            Console.WriteLine("Population size: {0}", popSize);
            Console.WriteLine("Mutation in population rate: {0} , Mutation in Chromosome rate: {1}", pr, chr);

            //initialize the population:
            Population<int> population = new Population<int>();
            for (int i = 0; i < popSize; i++)
                population.Add(new Chromosome<int>(FitnessFunc, GenrGenes.Binary_Integer(15).ToList()));
            double lf = 100;

            while(lf > 0) //Genetic loop
            {
                lf = population.LowestFitnessChromosome().Fitness;
                PrintData(population, c);

               population.Selection("roulette", invert: true, elitism: true, eliteCount: 2); //selection
               population.Crossover("onePointCrossover", 0.50); //crossover
               population.Mutation("binary", populationRate: pr, chromosomeRate: chr);//mutation
               population.Evaluation(FitnessFunc); //evaluation
                
               c++;
            }
        }

        private static void PrintData(Population<int> pop, int c)
        {
            Console.SetCursorPosition(0, 16);
            Chromosome<int> chr = pop.LowestFitnessChromosome();
            Console.WriteLine("Iter:(" + c + ") > LowestFitness: {0} ,  Average fitness: {1}", chr.Fitness, pop.GetAverageFitness());
            Console.WriteLine(chr.ToString());
            Message(chr);
            Console.WriteLine(pop.ToString());
        }

        //calculate the fitness.
        private static Func<Chromosome<int>, double> FitnessFunc = chromosome =>
        {
            List<int> first = chromosome.GetGenes(0, 4);
            List<int> second = chromosome.GetGenes(5, 9);
            List<int> third = chromosome.GetGenes(10, 14);

            int a = Laga.Numbers.Tools.BinaryToInteger(first);
            int b = Laga.Numbers.Tools.BinaryToInteger(second);
            int c = Laga.Numbers.Tools.BinaryToInteger(third);

            int res = 2 * a + 3 * b + 4 * c;
            return Math.Abs(res - 60); //we want to go closer to 0 here.
        };

        private static void Message(Chromosome<int> chromosome)
        {
            List<int> first = chromosome.GetGenes(0, 4);
            List<int> second = chromosome.GetGenes(5, 9);
            List<int> third = chromosome.GetGenes(10, 14);

            int a = Laga.Numbers.Tools.BinaryToInteger(first);
            int b = Laga.Numbers.Tools.BinaryToInteger(second);
            int c = Laga.Numbers.Tools.BinaryToInteger(third);

            int res = (2 * a) + (3 * b) + (4 * c);
            Console.WriteLine($"{a}x2 + {b}x3 + {c}x4 = {res}");
        }


    }
}

