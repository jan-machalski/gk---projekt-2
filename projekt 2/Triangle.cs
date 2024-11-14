using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_2
{
    public class Triangle
    {
        public Vertex Vertex1, Vertex2, Vertex3;
        public float AverageZ { get; private set; }

        public Triangle(Vertex v1, Vertex v2, Vertex v3)
        {
            Vertex1 = v1;
            Vertex2 = v2;
            Vertex3 = v3;
            UpdateAverageZ();
        }
        public void UpdateAverageZ()
        {
            // Obliczamy średnią wartość Z dla trójkąta
            AverageZ = (Vertex1.PositionAfterRotation.Z + Vertex2.PositionAfterRotation.Z + Vertex3.PositionAfterRotation.Z) / 3.0f;
        }
    }
}
