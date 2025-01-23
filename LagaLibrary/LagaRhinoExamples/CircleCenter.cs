using System;
using System.Linq;
using Rhino.Geometry;
using Rhino;
using Laga.Numbers;
using Laga;

namespace LagaRhinoExamples
{
    public class CircleCenter
    {
        private double topFitness = 100;
        //data
        private static Point3d[] arrPts;
        public CircleCenter(RhinoDoc doc)
        {
            //context.
            RhinoApp.WriteLine("building the context, generate a random circle");
            #region context
            Point3d randLoc = new Point3d(Rand.NextDouble(5, 10), Rand.NextDouble(5, 10), 0);
            double randRad = Rand.NextDouble(1, 5);
            Curve crv = new Circle(randLoc, randRad).ToNurbsCurve();

            double[] arrT = crv.DivideByCount(12, true);
            arrPts = new Point3d[arrT.Length];
            for (int i = 0; i < arrPts.Length; i++)
            {
                arrPts[i] = crv.PointAt(arrT[i]);
                doc.Objects.AddPoint(arrPts[i]);
            }
            doc.Views.Redraw();

            double min, max;
            (min, max) = GetMinMax(arrPts);
            #endregion

            //now the GA.
            RhinoApp.WriteLine("Begin the GA");
            #region GA
            int popSize = 100; //GA parameters
            double pr = 0.5;
            double chr = 0.001;

            Population<double> population = new Population<double>();
            for (int i = 0; i < popSize; i++)
                population.Add(new Chromosome<double>(FitnessFunc, GenrGenes.RandomChromosome<double>(2, min, max, Rand.NextDouble).ToList()));

            Point3d p = new Point3d();
            Point3d pp = new Point3d();

            int c = 0;
            Rhino.DocObjects.Tables.ObjectTable ot;

            while (topFitness >= 0.1) //Genetic loop
            {
                topFitness = population.LowestFitnessChromosome().Fitness;
                RhinoApp.WriteLine("iteration: [" + c.ToString() + "]  " + population.LowestFitnessChromosome().ToString());
                p = new Point3d(population.LowestFitnessChromosome().GetGene(0), population.LowestFitnessChromosome().GetGene(1), 0);

                if (pp != p)
                {
                    pp = p;
                    ot = doc.Objects;
                    ot.Delete(ot.ElementAt(0));
                    doc.Objects.AddPoint(p);
                }
                doc.Views.Redraw();

                population.Selection("roulette", tournamentSize: 20, invert: true, elitism: true, eliteCount: 50); //selection
                population.Crossover("onePoint", 0.1); //crossover
                population.Mutation("dblRandom", populationRate: pr, chromosomeRate: chr, dMin: min, dMax: max);//mutation
                population.Evaluation(FitnessFunc); //evaluation

                c++;
            }
            #endregion
        }

        private Func<Chromosome<double>, double> FitnessFunc = chromosome =>
        {
            double x = chromosome.GetGene(0);
            double y = chromosome.GetGene(1);
            Point3d target = new Point3d(x, y, 0);

            var distances = arrPts.Select(point => point.DistanceTo(target)).ToList();
            double average = distances.Average();
            double deviation = distances.Select(distance => Math.Pow(distance - average, 2)).Sum();
            return deviation;
        };

        private (double, double) GetMinMax(Point3d[] arrPts)
        {
            BoundingBox bb = new BoundingBox(arrPts);
            Point3d ptmin = bb.Min;
            Point3d ptmax = bb.Max;

            double min = ptmin.X < ptmin.Y ? ptmin.X : ptmin.Y;
            double max = ptmax.X > ptmax.Y ? ptmax.X : ptmax.Y;
            return (min, max);
        }

        
    }
}
