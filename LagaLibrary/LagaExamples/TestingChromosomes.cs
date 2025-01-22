using Laga;
using Laga.Geometry;
using Laga.Numbers;

namespace LagaExamples
{
    public static class TestingChromosomes
    {
        public static void Run()
        {
            Console.WriteLine("\n /// - Examples of Laga.GeneticAlgorithm.Chromosome() and Laga.GeneticAlgorithm.GenrGenes() - /// \n");

            Console.WriteLine("Chromosome of floats, essentially is a collection with a fitness function parameter.");
            Chromosome<float> chrFloats = GenrGenes.RandomChromosome<float>(5, 6f, 7f, Rand.NextFloat);
            Console.WriteLine(chrFloats.ToString() + "\n");

            Console.WriteLine("We are now asigning the fitness of 0.678");
            chrFloats.Fitness = 0.678;
            Console.WriteLine(chrFloats.ToString() + "\n");

            Console.WriteLine("We can also shuffle the genes in the chromosome:");
            chrFloats.Shuffle();
            Console.WriteLine(chrFloats.ToString() + "\n");

            Console.WriteLine("Integer chromosome:");
            Chromosome<int> intChr = GenrGenes.RandomChromosome<int>(10, 0, 10, Rand.NextInt);
            Console.WriteLine(intChr.ToString() + "\n");

            Console.WriteLine("Vector chromosome");
            Range r = new Range(0, 5);
            Chromosome<Vector> vecChr = GenrGenes.Rand_Vector(3, r, r, r);
            Console.WriteLine(vecChr.ToString() + "\n");

            Console.WriteLine("A chromosome can take any type");

            Console.WriteLine("\n /// - Press any key to return to the menu - /// ");
        }
    }
}
