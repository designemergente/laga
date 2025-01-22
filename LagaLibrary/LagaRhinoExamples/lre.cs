using System;
using System.Linq;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input;
using Laga;
using System.Runtime.ConstrainedExecution;
using Laga.Numbers;
using Eto.Forms;

namespace LagaRhinoExamples
{
    public class lre : Rhino.Commands.Command
    {
        public lre()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }
        ///<summary>The only instance of this command.</summary>
        public static lre Instance { get; private set; }
        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "lre";

        //points...
        private static Point3d[] arrpts;
        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            #region Original code
            /* ---

            RhinoApp.WriteLine("The {0} command will add a line right now.", EnglishName);

            Point3d pt0;
            using (GetPoint getPointAction = new GetPoint())
            {
                getPointAction.SetCommandPrompt("Please select the start point");
                if (getPointAction.Get() != GetResult.Point)
                {
                    RhinoApp.WriteLine("No start point was selected.");
                    return getPointAction.CommandResult();
                }
                pt0 = getPointAction.Point();
            }

            Point3d pt1;
            using (GetPoint getPointAction = new GetPoint())
            {
                getPointAction.SetCommandPrompt("Please select the end point");
                getPointAction.SetBasePoint(pt0, true);
                getPointAction.DynamicDraw +=
                  (sender, e) => e.Display.DrawLine(pt0, e.CurrentPoint, System.Drawing.Color.DarkRed);
                if (getPointAction.Get() != GetResult.Point)
                {
                    RhinoApp.WriteLine("No end point was selected.");
                    return getPointAction.CommandResult();
                }
                pt1 = getPointAction.Point();
            }

            doc.Objects.AddLine(pt0, pt1);
            doc.Views.Redraw();
            RhinoApp.WriteLine("The {0} command added one line to the document.", EnglishName);

            --- */
            #endregion

            UserInterface ui = new UserInterface(doc, mode);
            ui.Show();

            /*
            RhinoApp.WriteLine("optimization start: ");
            int popSize = 100;
            double topFitness = 100;
            double pr = 0.02;
            double chr = 0.001;

            ObjRef[] obj_ref;
            Result rs = RhinoGet.GetMultipleObjects("Select points", false, ObjectType.Point, out obj_ref);
            if (rs != Result.Success)
                return rs;

            arrpts = new Point3d[obj_ref.Length];
            for (int i = 0; i < obj_ref.Length; i++)
                arrpts[i] = new Point3d(obj_ref[i].Point().Location);

            BoundingBox bb = new BoundingBox(arrpts);
            Point3d ptmin = bb.Min;
            Point3d ptmax = bb.Max;

            double min = ptmin.X < ptmin.Y ? ptmin.X : ptmin.Y;
            double max = ptmax.X > ptmax.Y ? ptmax.X : ptmax.Y;

            Population<double> population = new Population<double>();
            for (int i = 0; i < popSize; i++)
                population.Add(new Chromosome<double>(FitnessFunc, GenrGenes.RandomChromosome<double>(2, min, max, Rand.NextDouble).ToList()));

            Point3d p = new Point3d();
            Point3d pp = new Point3d();

            int c = 0;
            Rhino.DocObjects.Tables.ObjectTable ot;

            while (topFitness >= 0.001) //Genetic loop
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

                population.Selection("roulette", tournamentSize: 10, invert: true, elitism: true, eliteCount: 20); //selection
                population.Crossover("onePointCrossover", 0.2); //crossover
                population.Mutation("dblRandom", populationRate: pr, chromosomeRate: chr, dMin: min, dMax: max);//mutation
                population.Evaluation(FitnessFunc); //evaluation

                c++;
            }
            */

            return Result.Success;
        }

        private static Func<Chromosome<double>, double> FitnessFunc = chromosome =>
        {
            double x = chromosome.GetGene(0);
            double y = chromosome.GetGene(1);
            Point3d target = new Point3d(x, y, 0);

            var distances = arrpts.Select(point => point.DistanceTo(target)).ToList();
            double average = distances.Average();
            double deviation = distances.Select(distance => Math.Pow(distance - average, 2)).Sum();
            return deviation;
        };

    }
}
