using Laga;
using Laga.Geometry;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LagaRhino
{
    /// <summary>
    /// Simple Manipulations on Rhino Points
    /// </summary>
    public class PointData
    {
        /// <summary>
        /// Mirror a point by the 4 quadrants
        /// </summary>
        /// <param name="point">point to mirror</param>
        /// <returns>Point3d[]</returns>
        public static Point3d[] MirrorPointQuadrant(Point3d point)
        {
            Point3d[] sortedPts = new Point3d[4];
            double posX = Math.Abs(point.X);
            double negX = posX * -1;
            double posY = Math.Abs(point.Y);
            double negY = posY * -1;

            sortedPts[0] = new Point3d(posX, posY, 0);
            sortedPts[1] = new Point3d(posX, negY, 0);
            sortedPts[2] = new Point3d(negX, negY, 0);
            sortedPts[3] = new Point3d(negX, posY, 0);

            return sortedPts;
        }

        /// <summary>
        /// determine in which quadrant is the point, if 0:++, 1:+-, 2:--, 3:-+.
        /// </summary>
        /// <param name="point">Test point</param>
        /// <returns>int</returns>
        public static int PointQuadrant(Point3d point)
        {
            double x = point.X;
            double y = point.Y;

            int Q = 0;

            if (x >= 0 && y >= 0)
            {
                Q = 0;
            }
            else if (x > 0 && y <= 0)
            {
                Q = 1;
            }
            else if (x < 0 && y < 0)
            {
                Q = 2;
            }
            else if (x <= 0 && y > 0)
            {
                Q = 3;
            }
            return Q;
        }

        /// <summary>
        /// Sort points by Z coordinate
        /// </summary>
        /// <param name="points">The points to sort</param>
        /// <returns>point3d[]</returns>
        public static Point3d[] SortPointsByZ(IEnumerable<Point3d> points)
        {
            return points.OrderBy(p => p.Z).ToArray();
        }

        /// <summary>
        /// Sort the Z coordinate from an array of points
        /// </summary>
        /// <param name="points"></param>
        /// <returns>double[]</returns>
        public static double[] SortCoordinateZ(IEnumerable<Point3d> points)
        {
            return points.Select<Point3d, double>((Func<Point3d, double>)(pt => pt.Z)).OrderBy<double, double>((Func<double, double>)(Z => Z)).ToArray<double>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point3d[] SortPointsQuadrant(Point3d point)
        {
            Point3d[] point3dArray = new Point3d[4];
            double num1 = Math.Abs(point.X);
            double num2 = num1 * -1.0;
            double num3 = Math.Abs(point.Y);
            double num4 = num3 * -1.0;
            point3dArray[0] = new Point3d(num1, num3, 0.0);
            point3dArray[1] = new Point3d(num1, num4, 0.0);
            point3dArray[2] = new Point3d(num2, num4, 0.0);
            point3dArray[3] = new Point3d(num2, num3, 0.0);
            return point3dArray;
        }

        /// <summary>
        /// Sort points by the coordinate X and then by the coordinate Y.
        /// </summary>
        /// <param name="points">The points to sort</param>
        /// <returns>point3d[]</returns>
        public static Point3d[] SortPointsByXY(IEnumerable<Point3d> points)
        {
            return points.OrderBy(item => item.X)
                         .ThenBy(item => item.Y).ToArray();

        }

        /// <summary>
        /// Sort a list of points clockwise
        /// </summary>
        /// <param name="points">the list of points</param>
        /// <returns><![CDATA[List<Point3d>]]></returns>
        public static List<Point3d> SortPointsClockwise(IEnumerable<Point3d> points)
        {
            List<Point3d> list = points.OrderBy<Point3d, double>((Func<Point3d, double>)(pt => Math.Atan2(pt.X, pt.Y))).ToList<Point3d>();
            
            if(list.Count == 2)
            { 
            Point3d ptA = list[0];
                Point3d ptB = list[1];
             
                bool pass = false;
                if((ptA.Y <= 0.0 && ptB.Y <= 0.0) && (ptA.X < 0.0 && ptB.X >= 0.0))
                    pass = true;

                if(pass)
                    list.Reverse();
            }
            return list;
        }

        /// <summary>
        /// Group points by Z coordinate.
        /// </summary>
        /// <param name="points">the array of points to group</param>
        /// <returns><![CDATA[List<Point3d[]>]]></returns>
        public static Population<Point3d> GroupPointsByZ(IEnumerable<Point3d> points)
        {
            points = points.OrderBy(p => p.Z).ToArray();

            //List<Point3d[]> lstArrGroup = new List<Point3d[]>();
            Population<Point3d> popGroup = new Population<Point3d>();
            try
            {
                var groupedResult = points.GroupBy(p => p.Z);

                foreach (var zGroup in groupedResult)
                {
                    popGroup.Add(new Chromosome<Point3d>(zGroup.ToList()));
                    //lstArrGroup.Add(zGroup.ToArray());

                }
            }
            catch (Exception)
            {
                return null;
            }

            return popGroup; //lstArrGroup;
        }

        /// <summary>
        /// Interpolate 2 points by span distance
        /// </summary>
        /// <param name="pointA">Start point</param>
        /// <param name="pointB">End point</param>
        /// <param name="span">span distance</param>
        /// <returns>Point3d[,]</returns>
        public static Population<Point3d> Interpolate2Points(Point3d pointA, Point3d pointB, double span)
        {
            Vector va = Point2Vector(pointA);
            Vector vb = Point2Vector(pointB);

            Population<Vector> population = Vector.Interpolation(va, vb, span);
            Population<Point3d> popPts = new Population<Point3d>();
            
            for (int i = 0; i < population.Count; i++)
            {
                List<Vector> vecs = population.GetChromosome(i).ToList();
                popPts.Add(new Chromosome<Point3d>(Vectors2Points(vecs)));
            }

            return popPts;
        }

        /// <summary>
        /// Convert Laga Vector to Rhino Point
        /// </summary>
        /// <param name="vector">The Vector to convert</param>
        /// <returns>Point3d</returns>
        public static Point3d Vector2Point(Vector vector)
        {
            return new Point3d(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Convert Laga Vectors to Rhino Points
        /// </summary>
        /// <param name="vectors">The arry of Vectors to convert</param>
        /// <returns>Point3d[]</returns>
        public static List<Point3d> Vectors2Points(IEnumerable<Vector> vectors)
        {
            List<Point3d> lstPoints = new List<Point3d>(vectors.Count());

            foreach (Vector v in vectors)
                lstPoints.Add(new Point3d(Vector2Point(v)));

            return lstPoints;
        }

        /// <summary>
        /// Convert Rhino Point to Laga Vector
        /// </summary>
        /// <param name="point">The point to convert</param>
        /// <returns>Vector</returns>
        public static Vector Point2Vector(Point3d point)
        {
            if (point != null)
            {
                return new Vector(point.X, point.Y, point.Z);
            }
            else
            {
                return new Vector(0, 0, 0);
            }
        }

        /// <summary>
        /// Convert Rhino Point3ds to Laga Vectors
        /// </summary>
        /// <param name="points">The list of Points3d to convert</param>
        /// <returns>Vectors</returns>
        public static List<Vector> Points2Vectors(IEnumerable<Point3d> points)
        {
            List<Vector> lstVec = new List<Vector>(points.Count());

            foreach(Point3d p in points)
                lstVec.Add(new Vector(Point2Vector(p)));

            return lstVec;
        }

        /// <summary>
        /// list of Z values... not really...
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="test"></param>
        /// <param name="stand"></param>
        /// <returns>double</returns>
        public static double FindZDifference(List<double> pattern, double test, double stand = 0.35)
        {
            int index1 = 0;
            for (int index2 = 0; index2 < pattern.Count; ++index2)
            {
                if (pattern[index2] == test)
                {
                    index1 = index2;
                    break;
                }
            }
            return index1 == pattern.Count - 1 ? stand : pattern[index1 + 1] - pattern[index1];
        }
    }
}
