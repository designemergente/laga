using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Laga.Numbers;

namespace Laga.GeneticAlgorithm
{
    /// <summary>
    /// Create and manipulate Chromosomes
    /// </summary>
    public class Chromosome<T>
    {
        private List<T> genes;
        private double? cachedFitness;
        private Func<Chromosome<T>, double> fitnessFunction;

        /// <summary>
        /// The size of the Chr++                                                                        
        /// </summary>
        public int Count
        {
            get
            {
                return genes.Count;
            }
        }

        /// <summary>
        /// Constructor accepting a fitness function
        /// </summary>
        /// <param name="FitnessFunction">Function to evaluate chromosome fitness</param>
        /// <param name="genes"></param>
        public Chromosome(Func<Chromosome<T>, double> FitnessFunction, IEnumerable<T> genes)
        {
            this.genes = genes?.ToList() ?? new List<T>();
            this.fitnessFunction = FitnessFunction ?? throw new ArgumentNullException(nameof(fitnessFunction), "Fitness function cannot be null.");
            cachedFitness = this.Fitness;
        }

        /// <summary>
        /// Constructor by size
        /// </summary>
        public Chromosome(int size)
        {
            this.genes = new List<T>(size);
        }

        /// <summary>
        /// constructor from a list of genes
        /// </summary>
        /// <param name="genes"></param>
        public Chromosome(IEnumerable<T> genes)
        {
            this.genes = genes?.ToList();
        }

        /// <summary>
        ///  Generic constructor
        /// </summary>
        public Chromosome()
        {
            this.genes = new List<T>();
        }

        /// <summary>
        /// Get and set the Chr fitnessString
        /// </summary>
        public double Fitness
        {
            get
            {
                if (!cachedFitness.HasValue)
                {
                    cachedFitness = fitnessFunction(this);
                }
                return cachedFitness.Value;
            }
            set { cachedFitness = value; }
        }

        /// <summary>
        /// Get the gene at specific index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>T</returns>
        public T GetGene(int index)
        {
            return genes[index];
        }

        /// <summary>
        /// GEt the genes from the start and end index.
        /// </summary>
        /// <param name="indexStart">The index at start</param>
        /// <param name="indexEnd">The index at the end</param>
        /// <returns>List</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public List<T> GetGenes(int indexStart, int indexEnd)
        {
            // Validate inputs to ensure indexEnd is inclusive
            if (indexStart < 0 || indexEnd >= genes.Count || indexStart > indexEnd)
                throw new ArgumentOutOfRangeException("Invalid start or end index.");

            // Calculate the count from the start and end indices
            int count = indexEnd - indexStart + 1;
            return genes.GetRange(indexStart, count);
        }

        /// <summary>
        /// set a gene at a specific index
        /// </summary>
        /// <param name="index">The location in the Chr</param>
        /// <param name="gene">The gene to insert</param>
        public void SetGene(int index, T gene)
        {
            if (index < 0 || index >= genes.Count)
                throw new ArgumentOutOfRangeException("index", "Index is out of range.");
            genes[index] = gene;
            cachedFitness = null;
        }

        /// <summary>
        /// Add gene to the Chromosome
        /// </summary>
        /// <param name="gene">The gene type</param>
        public void Add(T gene)
        {
            genes.Add(gene);
            cachedFitness = null;
        }

        /// <summary>
        /// Add data as collection into the Chr
        /// </summary>
        /// <param name="geneCollection">The collection of data</param>
        public void AddGenes(IEnumerable<T> geneCollection)
        {
            genes.AddRange(geneCollection);
            cachedFitness = null;
        }

        /// <summary>
        /// Convert the Chromosome in a list
        /// </summary>
        /// <returns>List</returns>
        public List<T> ToList()
        {
            return new List<T>(genes);
        }

        /// <summary>
        /// Convert the Chromosome in Array;
        /// </summary>
        /// <returns>Array</returns>
        public T[] ToArray()
        {
            return genes.ToArray();
        }

        /// <summary>
        /// Perform Fisher-Yates shuffle on the genes.
        /// </summary>
        public void Shuffle()
        {
            int count = this.Count;

            for (int i = count - 1; i > 0; i--)
            {
                int index = Rand.NextInt(i, count);

                T temp = this.GetGene(i);

                this.SetGene(i, this.GetGene(index));
                this.SetGene(index, temp);
            }
        }

        /// <summary>
        /// Chromosome to String
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string geneString = string.Join(", ", genes);
            string fitnessString = cachedFitness.HasValue ? cachedFitness.Value.ToString() : "No fitness";
            return $"Genes: {geneString} | Fitness: {fitnessString}";
        }

        /// <summary>
        /// Perform a crossover with another chromosome using a specified function.
        /// </summary>
        /// <param name="partner">The other parent chromosome</param>
        /// <param name="CrossoverFunction">The crossover function to use</param>
        /// <returns>Tuple containing two new Chromosome offspring</returns>
        public (Chromosome<T>, Chromosome<T>) Crossover(Chromosome<T> partner, Func<Chromosome<T>, Chromosome<T>, (Chromosome<T>, Chromosome<T>)> CrossoverFunction)
        {
            return CrossoverFunction(this, partner);
        }

        /// <summary>
        /// Mutate the chromosome
        /// </summary>
        /// <param name="mutationRate">The percentage possibility to occur the mutation</param>
        /// <param name="MutationFunction">The Mutation function</param>
        public void Mutate(double mutationRate, Func<int, T> MutationFunction)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (Rand.NextDouble() < mutationRate)
                {
                        genes[i] = MutationFunction(i);
                        cachedFitness = null;
                } 
            }
        }
    }
}