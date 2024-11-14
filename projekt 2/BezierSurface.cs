using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace projekt_2
{
    public class BezierSurface
    {
        private Vector3[,] controlPoints = new Vector3[4, 4];
        public List<Triangle> Triangles { get; private set; }
        public float AngleX { get; set; } = 0; // Kąt obrotu wokół osi X
        public float AngleZ { get; set; } = 0; // Kąt obrotu wokół osi Z

        public BezierSurface(Vector3[,] points, int accuracy)
        {
            controlPoints = points;
            Triangles = new List<Triangle>();
            GenerateSurface(accuracy);
        }
        public void GenerateSurface(int accuracy)
        {
            Triangles.Clear();
            float step = 1.0f / accuracy;

            for (int i = 0; i < accuracy; i++)
            {
                for (int j = 0; j < accuracy; j++)
                {
                    float u1 = i * step;
                    float v1 = j * step;
                    float u2 = (i + 1) * step;
                    float v2 = (j + 1) * step;

                    Vertex vtx1 = CalculateVertex(u1, v1);
                    Vertex vtx2 = CalculateVertex(u2, v1);
                    Vertex vtx3 = CalculateVertex(u1, v2);
                    Vertex vtx4 = CalculateVertex(u2, v2);

                    vtx1.ApplyRotation(AngleX, AngleZ, controlPoints);
                    vtx2.ApplyRotation(AngleX, AngleZ, controlPoints);
                    vtx3.ApplyRotation(AngleX, AngleZ, controlPoints);
                    vtx4.ApplyRotation(AngleX, AngleZ, controlPoints);

                    Triangles.Add(new Triangle(vtx1, vtx2, vtx4));
                    Triangles.Add(new Triangle(vtx1, vtx4, vtx3));
                }
            }
            foreach (var triangle in Triangles)
            {
                triangle.UpdateAverageZ();
            }
        }
        private Vertex CalculateVertex(float u, float v)
        {
            int n = controlPoints.GetLength(0) - 1; // Stopień w kierunku u
            int m = controlPoints.GetLength(1) - 1; // Stopień w kierunku v

            Vector3 position = new Vector3();
            Vector3 tangentU = new Vector3();
            Vector3 tangentV = new Vector3();

            // Obliczanie pozycji punktu P(u, v) na powierzchni
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    // Oblicz wielomiany Bernsteina B_i^n(u) i B_j^m(v)
                    float bernsteinU = Bernstein(i, n, u);
                    float bernsteinV = Bernstein(j, m, v);

                    // Dodaj wkład punktu kontrolnego do pozycji
                    position += controlPoints[i, j] * bernsteinU * bernsteinV;
                }
            }

            // Obliczanie wektora stycznego wzdłuż u - P_u(u, v)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    // Oblicz różnicę między punktami kontrolnymi V_{i+1,j} - V_{i,j}
                    Vector3 delta = controlPoints[i + 1, j] - controlPoints[i, j];

                    // Oblicz wielomiany Bernsteina B_i^{n-1}(u) i B_j^m(v)
                    float bernsteinU = Bernstein(i, n - 1, u);
                    float bernsteinV = Bernstein(j, m, v);

                    // Dodaj wkład punktu kontrolnego do wektora stycznego wzdłuż u
                    tangentU += delta * bernsteinU * bernsteinV * n;
                }
            }

            // Obliczanie wektora stycznego wzdłuż v - P_v(u, v)
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    // Oblicz różnicę między punktami kontrolnymi V_{i,j+1} - V_{i,j}
                    Vector3 delta = controlPoints[i, j + 1] - controlPoints[i, j];

                    // Oblicz wielomiany Bernsteina B_i^n(u) i B_j^{m-1}(v)
                    float bernsteinU = Bernstein(i, n, u);
                    float bernsteinV = Bernstein(j, m - 1, v);

                    // Dodaj wkład punktu kontrolnego do wektora stycznego wzdłuż v
                    tangentV += delta * bernsteinU * bernsteinV * m;
                }
            }

            tangentU = Vector3.Normalize(tangentU);
            tangentV = Vector3.Normalize(tangentV);

            // Obliczenie wektora normalnego N(u, v) = P_u(u, v) x P_v(u, v)
            Vector3 normal = Vector3.Cross(tangentU, tangentV);
            normal = Vector3.Normalize(normal);

            return new Vertex(position, tangentU, tangentV, normal, u, v);
        }
        private float Bernstein(int i, int n, float t)
        {
            return BinomialCoefficient(n, i) * (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
        }

        private int BinomialCoefficient(int n, int k)
        {
            if (k > n) return 0;
            if (k == 0 || k == n) return 1;
            int c = 1;
            for (int i = 1; i <= k; i++)
            {
                c = c * (n - i + 1) / i;
            }
            return c;
        }
    }
}
