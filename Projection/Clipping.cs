using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math_lib;

namespace Projection
{
    class Clipping
    {
        public static Triangle3 outTri1;
        public static Triangle3 outTri2;

        //calc Intersection Point of two Lines
        public static Point3 IntersectPlane(Point3 planeP, Normal planeN, Point3 lineStart, Point3 lineEnd)
        {
            planeN = new(Vector3.UnitVector(new(planeN)));
            double planeD = -Vector3.Dot(new(planeN), new(planeP));
            double ad = Vector3.Dot(new(lineStart), new(planeN));
            double bd = Vector3.Dot(new(lineEnd), new(planeN));
            double t = (-planeD - ad) / (bd - ad);
            Vector3 lineStartToEnd = lineEnd - lineStart;
            Vector3 lineToIntersect = lineStartToEnd * t;
            return lineStart + lineToIntersect;
        }

        public static int TriangleClipAgainstPlane(Point3 planeP, Normal planeN, Triangle3 inTri)
        {
            //Init Triangles
            outTri1 = new Triangle3();
            outTri2 = new Triangle3();

            //Normalize the Normal
            planeN = new(Vector3.UnitVector(new(planeN)));

            //Storage
            Point3[] insidePoints = new Point3[3];
            Point3[] outsidePoints = new Point3[3];
            int insidePointsCount  = 0;
            int outsidePointsCount = 0;

            //Distances
            double d0 = calcDistance(inTri.Points[0], planeP, planeN);
            double d1 = calcDistance(inTri.Points[1], planeP, planeN);
            double d2 = calcDistance(inTri.Points[2], planeP, planeN);

            //Check if its a point outside or inside
            if(d0 > 0)
            {
                insidePoints[insidePointsCount++] = inTri.Points[0];
            }
            else
            {
                outsidePoints[outsidePointsCount++] = inTri.Points[0];
            }

            if (d1 > 0)
            {
                insidePoints[insidePointsCount++] = inTri.Points[1];
            }
            else
            {
                outsidePoints[outsidePointsCount++] = inTri.Points[1];
            }

            if (d2 > 0)
            {
                insidePoints[insidePointsCount++] = inTri.Points[2];
            }
            else
            {
                outsidePoints[outsidePointsCount++] = inTri.Points[2];
            }

            //all points outside
            if (insidePointsCount == 0)
            {
                return 0;
            }
            //all ponts inside
            if (insidePointsCount == 3)
            {
                outTri1 = inTri;
                return 1;
            }

            //Clipping Triangle
            //One point inside
            if (insidePointsCount == 1 && outsidePointsCount == 2)
            {
                outTri1.Points[0] = insidePoints[0];
                outTri1.Points[1] = IntersectPlane(planeP, planeN, insidePoints[0], outsidePoints[0]);
                outTri1.Points[2] = IntersectPlane(planeP, planeN, insidePoints[0], outsidePoints[1]);

                return 1;
            }

            //Clipping Triangle
            //two points indide
            if (insidePointsCount == 2 && outsidePointsCount == 1)
            {
                outTri1.Points[0] = insidePoints[0];
                outTri1.Points[1] = insidePoints[1];
                outTri1.Points[2] = IntersectPlane(planeP, planeN, insidePoints[0], outsidePoints[0]);

                outTri2.Points[0] = insidePoints[1];
                outTri2.Points[1] = outTri1.Points[2];
                outTri2.Points[2] = IntersectPlane(planeP, planeN, insidePoints[1], outsidePoints[0]);

                return 2;
            }

            return 1;
        }
        static double calcDistance(Point3 p, Point3 planeP, Normal planeN)
        {
            Vector3 erg = new(planeN * (p - planeP));

            return erg.X + erg.Y + erg.Z;
        }
    }
}
