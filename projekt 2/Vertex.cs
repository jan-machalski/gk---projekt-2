using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace projekt_2
{
    public class Vertex
    {
        public Vector3 PositionBeforeRotation;
        public Vector3 TangentUBeforeRotation;
        public Vector3 TangentVBeforeRotation;
        public Vector3 NormalBeforeRotation;

        public Vector3 PositionAfterRotation;
        public Vector3 TangentUAfterRotation;
        public Vector3 TangentVAfterRotation;
        public Vector3 NormalAfterRotation;

        public float U, V;

        public Vertex(Vector3 position, Vector3 tangentU, Vector3 tangentV, Vector3 normal, float u, float v)
        {
            PositionBeforeRotation = position;
            TangentUBeforeRotation = tangentU;
            TangentVBeforeRotation = tangentV;
            NormalBeforeRotation = normal;

            PositionAfterRotation = position;
            TangentUAfterRotation = tangentU;
            TangentVAfterRotation = tangentV;
            NormalAfterRotation = normal;

            U = u;
            V = v;
        }
        public void ApplyRotation(float angleX, float angleZ, Vector3[,] controlPoints = null)
        {
            float angleXInRadians = MathF.PI * angleX / 180.0f;
            float angleZInRadians = MathF.PI * angleZ / 180.0f;

            // Macierze obrotu wokół osi X i Z
            float cosX = MathF.Cos(angleXInRadians);
            float sinX = MathF.Sin(angleXInRadians);
            float cosZ = MathF.Cos(angleZInRadians);
            float sinZ = MathF.Sin(angleZInRadians);

            // Zastosowanie obrotów do pozycji
            PositionAfterRotation = RotateVectorZ(PositionBeforeRotation, cosZ, sinZ);
            PositionAfterRotation = RotateVectorX(PositionAfterRotation, cosX, sinX);

            TangentUAfterRotation = RotateVectorZ(TangentUBeforeRotation,cosZ, sinZ);
            TangentUAfterRotation = RotateVectorX(TangentUAfterRotation, cosX, sinX);

            TangentVAfterRotation = RotateVectorZ(TangentVBeforeRotation, cosZ, sinZ);
            TangentVAfterRotation = RotateVectorX(TangentVAfterRotation, cosX, sinX);

            //NormalAfterRotation = Vector3.Cross(TangentUAfterRotation, TangentVAfterRotation);
            //NormalAfterRotation = Vector3.Normalize(NormalAfterRotation);
            NormalAfterRotation = RotateVectorZ(NormalBeforeRotation, cosZ, sinZ);
            NormalAfterRotation = RotateVectorX(NormalAfterRotation, cosX, sinX);


        }

        // Funkcja do obrotu wektora wokół osi Z
        private Vector3 RotateVectorZ(Vector3 vector, float cosZ, float sinZ)
        {
            return new Vector3(
                vector.X * cosZ - vector.Y * sinZ,
                vector.X * sinZ + vector.Y * cosZ,
                vector.Z
            );
        }

        // Funkcja do obrotu wektora wokół osi X
        private Vector3 RotateVectorX(Vector3 vector, float cosX, float sinX)
        {
            return new Vector3(
                vector.X,
                vector.Y * cosX - vector.Z * sinX,
                vector.Y * sinX + vector.Z * cosX
            );
        }

        // Funkcja do ponownego obliczenia wektorów stycznych i normalnych po obrocie
        private void RecalculateTangentsAndNormal(Vector3[,] controlPoints, float cosX, float sinX, float cosZ, float sinZ)
        {
            int n = controlPoints.GetLength(0) - 1;
            int m = controlPoints.GetLength(1) - 1;

            // Obliczenie stycznych na nowo
            TangentUAfterRotation = RotateVectorZ(CalculateTangentU(U, V, n, m, controlPoints), cosZ, sinZ);
            TangentUAfterRotation = RotateVectorX(TangentUAfterRotation, cosX, sinX);

            TangentVAfterRotation = RotateVectorZ(CalculateTangentV(U, V, n, m, controlPoints), cosZ, sinZ);
            TangentVAfterRotation = RotateVectorX(TangentVAfterRotation, cosX, sinX);

            // Obliczenie wektora normalnego na nowo po obrocie
            NormalAfterRotation = Vector3.Cross(TangentUAfterRotation, TangentVAfterRotation);
            NormalAfterRotation = Vector3.Normalize(NormalAfterRotation);
        }

        // Funkcja do obliczania wektora stycznego wzdłuż u (P_u(u, v))
        private Vector3 CalculateTangentU(float u, float v, int n, int m, Vector3[,] controlPoints)
        {
            Vector3 tangentU = new Vector3();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    Vector3 delta = controlPoints[i + 1, j] - controlPoints[i, j];
                    float bernsteinU = Bernstein(i, n - 1, u);
                    float bernsteinV = Bernstein(j, m, v);
                    tangentU += delta * bernsteinU * bernsteinV * n;
                }
            }
            return Vector3.Normalize(tangentU);
        }

        // Funkcja do obliczania wektora stycznego wzdłuż v (P_v(u, v))
        private Vector3 CalculateTangentV(float u, float v, int n, int m, Vector3[,] controlPoints)
        {
            Vector3 tangentV = new Vector3();
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Vector3 delta = controlPoints[i, j + 1] - controlPoints[i, j];
                    float bernsteinU = Bernstein(i, n, u);
                    float bernsteinV = Bernstein(j, m - 1, v);
                    tangentV += delta * bernsteinU * bernsteinV * m;
                }
            }
            return Vector3.Normalize(tangentV);
        }

        // Pomocnicza funkcja do obliczenia wielomianu Bernsteina
        private float Bernstein(int i, int n, float t)
        {
            return BinomialCoefficient(n, i) * MathF.Pow(t, i) * MathF.Pow(1 - t, n - i);
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
