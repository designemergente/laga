using Laga.Numbers;
using Laga;

namespace LagaExamples
{
    /// <summary>
    /// FunctionOptimization
    /// </summary>
    public class FunctionOptimization
    {
        public static void Run()
        {
            Console.WriteLine("\n /// - Example of Function Optimization - /// \n");
            Console.WriteLine("Fitness: F(x,y) = 15 * x * y * (1 - x) * (1 - y) * Sin(PI * x) * Sin(PI * y) (find x and y) \n");

            int c = 0;
            int popSize = 100;
            double pr = 0.5;
            double chr = 0.001;
            Console.WriteLine("GA parameters:");
            Console.WriteLine("Population size: {0}", popSize);
            Console.WriteLine("Mutation in population rate: {0} , Mutation in Chromosome rate: {1}", pr, chr);

            //initialize the population:
            Population<double> population = new Population<double>();
            for (int i = 0; i < popSize; i++)
                population.Add(new Chromosome<double>(FitnessFunc, GenrGenes.RandomChromosome<double>(2, 0.0, 1.0, Rand.NextDouble).ToList()));

            double topFitness = 0;

            while (Math.Abs(topFitness - 0.93749) > 0.001) //Genetic loop
            {
                topFitness = population.HighestFitnessChromosome().Fitness;
                PrintData(population, c);

                population.Selection("roulette", tournamentSize: 10, elitism: true, eliteCount: 60); //selection
                population.Crossover("onePointCrossover", 0.75); //crossover
                population.Mutation("dblRandom", populationRate: pr, chromosomeRate: chr, dMin: 0.0, dMax: 1.0);//mutation
                population.Evaluation(FitnessFunc); //evaluation

                c++;
            }

        }
        private static void PrintData(Population<double> pop, int c)
        {
            Console.SetCursorPosition(0, 16);
            Chromosome<double> chr = pop.HighestFitnessChromosome();
            Console.WriteLine("Iter:(" + c + ") > HighestFitness: {0} ,  Average fitness: {1}", chr.Fitness, pop.GetAverageFitness());
            Console.WriteLine(chr.ToString());
        }

        private static Func<Chromosome<double>, double> FitnessFunc = chromosome =>
        {
            double x = chromosome.GetGene(0);
            double y = chromosome.GetGene(1);
            return 15 * x * y * (1 - x) * (1 - y) * Math.Sin(Math.PI * x) * Math.Sin(Math.PI * y);
        };
    }
}
