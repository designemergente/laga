using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Laga.Numbers;

namespace Laga
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
        /// Chromosome to String
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string geneString = string.Join(", ", genes);
            string fitnessString = cachedFitness.HasValue ? cachedFitness.Value.ToString() : "No fitness";
            return $"Genes: {geneString} | Fitness: {fitnessString}";
        }

        #region Crossover

        /// <summary>
        /// Perform a crossover with another chromosome using a specified function.
        /// </summary>
        /// <param name="parent">The other parent chromosome</param>
        /// <returns>Tuple containing two new Chromosome offspring</returns>
        public (Chromosome<T> child1, Chromosome<T> child2) OnePointCrossover(Chromosome<T> parent)
        {
            if (parent.Count <= 2) //small chromosomes.
            {
                var child1 = new List<T> { parent.GetGene(1), this.GetGene(0)};
                var child2 = new List<T> { this.GetGene(1), parent.GetGene(0)};
                return (new Chromosome<T>(child1), new Chromosome<T>(child2));
            }

            int crossoverPoint = Rand.NextInt(1, parent.Count - 1);

            var child1Genes = this.GetGenes(0, crossoverPoint).ToList();
            child1Genes.AddRange(parent.GetGenes(crossoverPoint + 1, parent.Count - 1));

            var child2Genes = parent.GetGenes(0, crossoverPoint).ToList();
            child2Genes.AddRange(this.GetGenes(crossoverPoint + 1, parent.Count - 1));

            return (new Chromosome<T>(child1Genes), new Chromosome<T>(child2Genes));
        }

        /// <summary>
        /// Perform a crossover with another chromosome using a specified function.
        /// </summary>
        /// <param name="parent">The other parent chromosome</param>
        /// <returns>Tuple containing two new Chromosome offspring</returns>
        public (Chromosome<T> child1, Chromosome<T> child2) ShuffleOnePointCrossover(Chromosome<T> parent)
        {
            // Ensure the parent chromosome has enough genes for crossover
            if (parent.Count <= 2)
            {
                // Small chromosomes: directly swap the genes
                var child1 = new List<T> { parent.GetGene(1), this.GetGene(0) };
                var child2 = new List<T> { this.GetGene(1), parent.GetGene(0) };
                return (new Chromosome<T>(child1), new Chromosome<T>(child2));
            }

            // Randomly select a crossover point
            int crossoverPoint = Rand.NextInt(1, parent.Count - 1);

            // Initialize child genes
            var child1Genes = new List<T>(parent.Count);
            var child2Genes = new List<T>(parent.Count);

            // Create mappings to track assigned genes for validation
            HashSet<T> child1GeneSet = new HashSet<T>();
            HashSet<T> child2GeneSet = new HashSet<T>();

            // Add genes from the first segment up to the crossover point
            child1Genes.AddRange(this.GetGenes(0, crossoverPoint));
            child1GeneSet.UnionWith(this.GetGenes(0, crossoverPoint));

            child2Genes.AddRange(parent.GetGenes(0, crossoverPoint));
            child2GeneSet.UnionWith(parent.GetGenes(0, crossoverPoint));

            // Complete children by filling in missing genes while maintaining order
            foreach (var gene in parent.GetGenes(crossoverPoint, parent.Count))
            {
                if (!child1GeneSet.Contains(gene))
                {
                    child1Genes.Add(gene);
                    child1GeneSet.Add(gene);
                }
            }

            foreach (var gene in this.GetGenes(crossoverPoint, this.Count))
            {
                if (!child2GeneSet.Contains(gene))
                {
                    child2Genes.Add(gene);
                    child2GeneSet.Add(gene);
                }
            }

            return (new Chromosome<T>(child1Genes), new Chromosome<T>(child2Genes));
        }

        /// <summary>
        /// Not implemented yet.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public (Chromosome<T>, Chromosome<T>) TwoPointsCrossover(Chromosome<T> parent)
        {
            return (new Chromosome<T>(), new Chromosome<T>());
        }
        #endregion

        #region Mutation
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

        /// <summary>
        /// Mutate the chromosome
        /// </summary>
        /// <param name="rate">The percentage possibility to occur the mutation</param>
        public Chromosome<int> BinaryMutation(double rate)
        {
            Chromosome<int> chr = new Chromosome<int>();
            for (int i = 0; i < genes.Count; i++)
            {
                if (Rand.NextDouble() < rate)
                    chr.Add(Convert.ToInt16(genes[i].Equals(1) ? (T)(object)0 : (T)(object)1));
                else
                    chr.Add(Convert.ToInt16(genes[i]));
            }

            return chr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Chromosome<char> CharRandom(double rate, int start, int end)
        {
            Chromosome<char> chr = new Chromosome<char>();
            for (int i = 0; i < genes.Count; i++)
            {
                if (Rand.NextDouble() < rate)
                {
                    //Func<int, char> func = Mutation.Mutate(start, end);
                    chr.Add(Mutation.CharMutation(start, end));
                }
                else
                    chr.Add(Convert.ToChar(genes[i]));
            }
            return chr;
        }

        public Chromosome<double> dblRandom(double rate, double start, double end)
        {
            Chromosome<double> chr = new Chromosome<double>();
            for (int i = 0; i < genes.Count; i++)
            {
                if (Rand.NextDouble() < rate)
                {
                    chr.Add(Mutation.DblMutation(start, end));
                }
                else
                    chr.Add(Convert.ToDouble(genes[i]));
            }
            return chr;
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

        #endregion
    }
}