using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace projekt_2
{
    public class AETEntry
    {
        public Vertex V0;         // Starting vertex of the edge
        public Vertex V2;         // Ending vertex of the edge
        public float YMin;        // Minimum Y value of the edge
        public float YMax;        // Maximum Y value of the edge
        public float OneOverM;    // Inverse slope of the edge
        public float CurrentX;    // Current X position on the scanline

        public AETEntry(Vertex v0, Vertex v2)
        {
            // Ensure V0.Y <= V2.Y
            if (v0.PositionAfterRotation.Y > v2.PositionAfterRotation.Y)
            {
                Swap(ref v0, ref v2);
            }

            V0 = v0;
            V2 = v2;

            YMin = V0.PositionAfterRotation.Y;
            YMax = V2.PositionAfterRotation.Y;

            float dy = V2.PositionAfterRotation.Y - V0.PositionAfterRotation.Y;
            float dx = V2.PositionAfterRotation.X - V0.PositionAfterRotation.X;

            OneOverM = dy == 0 ? 0 : dx / dy;
            CurrentX = V0.PositionAfterRotation.X;
        }

        // Method to check if the edge matches given vertices
        public bool EqualsEdge(Vertex v1, Vertex v2)
        {
            return (V0 == v1 && V2 == v2) || (V0 == v2 && V2 == v1);
        }
        private void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
