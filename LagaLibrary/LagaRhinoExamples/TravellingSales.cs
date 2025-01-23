using System;
using System.Linq;
using Rhino.Geometry;
using Rhino;
using Laga.Numbers;
using Laga;
using System.Collections.Generic;

namespace LagaRhinoExamples
{
    public class TravellingSales
    {
        
        private static Point3d[] cityPoints;
        public TravellingSales(RhinoDoc doc)
        {
            RhinoApp.WriteLine("building the context, generate a random points");

            #region context
            int numOfCities = 5;
            
            cityPoints = new Point3d[numOfCities];

            for (int i = 0; i < numOfCities; i++)
            {
                cityPoints[i] = new Point3d(Rand.NextFloat(0, 15), Rand.NextFloat(0, 15), 0);
                doc.Objects.AddPoint(cityPoints[i]);
                doc.Objects.AddTextDot(i.ToString(), cityPoints[i]);
                doc.Views.Redraw();
            }
            #endregion

            RhinoApp.WriteLine("Begin the GA");

            int popSize = 50;
            double pr = 0.5;
            double chr = 0.001;
            double topFitness = 0;
            double iter = 100;
            int c = 0;

            Population<int> population = new Population<int>();
            for (int i = 0; i < popSize; i++)
                population.Add(new Chromosome<int>(FitnessFunc, GenrGenes.Shuffle_Integer(0, numOfCities - 1).ToList()));

            Polyline poly;

            //while (iter < c) //Genetic loop
            //{
                topFitness = population.LowestFitnessChromosome().Fitness;
                RhinoApp.WriteLine("iteration: [" + c.ToString() + "]  " + population.LowestFitnessChromosome().ToString());
               
                poly = DrawCurve(population.LowestFitnessChromosome());
                doc.Objects.AddPolyline(poly);

                doc.Views.Redraw();

                population.Selection("roulette", tournamentSize: 20, invert: true, elitism: true, eliteCount: 10); //selection
                population.Crossover("ShuffleOnePoint", 0.1); //crossover
                population.Mutation("Shuffle", populationRate: pr, chromosomeRate: chr);//mutation
                population.Evaluation(FitnessFunc); //evaluation

                //c++;
            //}

        }
        private Polyline DrawCurve(Chromosome<int> chr)
        {
            Polyline pop = new Polyline();
            for (int i = 0; i < chr.Count; i++)
                pop.Add(cityPoints[chr.GetGene(i)]);

            return pop;
        }

        private Func<Chromosome<int>, double> FitnessFunc = chromosome =>
        {
            double d = 0;
            for(int i = 0; i < cityPoints.Length - 1; i++)
            {
                int geneA = chromosome.GetGene(i);
                int geneB = chromosome.GetGene(i + 1);
                d += cityPoints[geneA].DistanceTo(cityPoints[geneB]);
            }
            return d;
        };
    }
}