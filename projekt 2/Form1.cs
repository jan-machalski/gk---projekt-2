using FastBitmapLib;
using System.Diagnostics;
using System.Numerics;

namespace projekt_2
{
    public partial class Form1 : Form
    {
        private Vector3[,] controlPoints;
        private DirectBitmap directBitmap;
        private BezierSurface bezierSurface;
        private float kd = 0.5f;
        private float ks = 0.5f;
        private int m = 50;
        private Vector3 Il = new Vector3(1f, 1f, 1f);
        private Vector3 Io = new Vector3(1f, 0f, 0f);
        private Vector3 lightSourcePos = new Vector3(0, 0, 500);
        private Vector3 V = new Vector3(0, 0, 1);
        private ColorDialog lightColorDialog = new ColorDialog() { Color = Color.White };
        private ColorDialog surfaceColorDialog = new ColorDialog() { Color = Color.Red };
        private bool useTexture = false;
        private DirectBitmap textureImage = null;
        private DirectBitmap normalMap = null;
        private bool useNormalMap = false;
        private bool drawTriangles = false;
        private bool fillTriangles = true;
        private int bitmapHeight, bitmapWidth;
        private System.Windows.Forms.Timer lightMoveTimer;
        private float spiralAngle = 0; // Current angle for spiral movement
        private float spiralRadius = 0; // Current radius for spiral movement
        private float spiralRadiusIncrement = 5; // Radius increment per step
        private float spiralAngleIncrement = 0.1f; // Angle increment per step (in radians)
        bool lightMovement = true;


        public Form1()
        {
            InitializeComponent();

            LoadControlPoints("points.txt");

            InitializeBitmap();
            bitmapHeight = directBitmap.Height;
            bitmapWidth = directBitmap.Width;

            LoadDefaultNormalMap();
            LoadDefaultTexture();

            bezierSurface = new BezierSurface(controlPoints, accuracyTrackBar.Value);
            InitializeLightMovement();

            DrawSurface();
        }
        private void InitializeBitmap()
        {
            directBitmap = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = directBitmap.Bitmap;
        }
        private void InitializeLightMovement()
        {
            lightMoveTimer = new System.Windows.Forms.Timer();
            lightMoveTimer.Interval = 50;
            lightMoveTimer.Tick += LightMoveTimer_Tick;
            lightMoveTimer.Start();
        }

        private void LightMoveTimer_Tick(object sender, EventArgs e)
        {
            if (!lightMovement)
                return;
            // Update the angle and radius for the spiral motion
            spiralAngle += spiralAngleIncrement;
            spiralRadius += spiralRadiusIncrement * spiralAngleIncrement;

            // Calculate new x and y positions in the spiral
            float x = (float)(spiralRadius * Math.Cos(spiralAngle));
            float y = (float)(spiralRadius * Math.Sin(spiralAngle));

            // Update the light source position
            lightSourcePos = new Vector3(x, y, lightSourcePos.Z);

            // Trigger redraw of the surface to reflect the light source's new position
            DrawSurface();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (directBitmap.Bitmap != null)
            {
                e.Graphics.DrawImage(directBitmap.Bitmap, 0, 0);
            }

        }

        private void DrawSurface()
        {
            using (Graphics g = Graphics.FromImage(directBitmap.Bitmap))
            {

                var sortedTriangles = bezierSurface.Triangles.OrderBy(t => t.AverageZ).ToList();

                // Rysowanie trójk¹tów powierzchni Béziera


                g.Clear(Color.White);
                if (fillTriangles)
                {
                    Parallel.ForEach(sortedTriangles, t =>
                    {
                        FillTriangle(t, directBitmap);
                    });
                    /* foreach (var t in sortedTriangles)
                     {
                         FillTriangle(t, directBitmap);
                     }*/
                }

                if (drawTriangles)
                {
                    foreach (var t in sortedTriangles)
                        DrawTriangle(g, t);
                }


                // Rysowanie punktów kontrolnych
                //DrawControlPoints(g);
            }
            pictureBox1.Invalidate();

        }


        private void FillTriangle(Triangle triangle, DirectBitmap fastBitmap)
        {
            // Positions in logical coordinates
            Vector3 v0 = triangle.Vertex1.PositionAfterRotation;
            Vector3 v1 = triangle.Vertex2.PositionAfterRotation;
            Vector3 v2 = triangle.Vertex3.PositionAfterRotation;

            // Normals at each vertex
            Vector3 n0 = triangle.Vertex1.NormalAfterRotation;
            Vector3 n1 = triangle.Vertex2.NormalAfterRotation;
            Vector3 n2 = triangle.Vertex3.NormalAfterRotation;

            Vector3 t0 = triangle.Vertex1.TangentUAfterRotation;
            Vector3 t1 = triangle.Vertex2.TangentUAfterRotation;
            Vector3 t2 = triangle.Vertex3.TangentUAfterRotation;

            Vector3 b0 = triangle.Vertex1.TangentVAfterRotation;
            Vector3 b1 = triangle.Vertex2.TangentVAfterRotation;
            Vector3 b2 = triangle.Vertex3.TangentVAfterRotation;

            float u0 = triangle.Vertex1.U;
            float u1 = triangle.Vertex2.U;
            float u2 = triangle.Vertex3.U;

            float v0_tex = triangle.Vertex1.V;
            float v1_tex = triangle.Vertex2.V;
            float v2_tex = triangle.Vertex3.V;

            // Sort the vertices by Y-coordinate ascending (v0.Y <= v1.Y <= v2.Y)
            if (v1.Y < v0.Y)
            {
                Swap(ref v0, ref v1);
                Swap(ref n0, ref n1);
                Swap(ref t0, ref t1);
                Swap(ref b0, ref b1);
                Swap(ref u0, ref u1);
                Swap(ref v0_tex, ref v1_tex);
            }
            if (v2.Y < v0.Y)
            {
                Swap(ref v0, ref v2);
                Swap(ref n0, ref n2);
                Swap(ref t0, ref t2);
                Swap(ref b0, ref b2);
                Swap(ref u0, ref u2);
                Swap(ref v0_tex, ref v2_tex);
            }
            if (v2.Y < v1.Y)
            {
                Swap(ref v1, ref v2);
                Swap(ref n1, ref n2);
                Swap(ref t1, ref t2);
                Swap(ref b1, ref b2);
                Swap(ref u1, ref u2);
                Swap(ref v1_tex, ref v2_tex);
            }

            // Sort the vertices by Y-coordinate ascending (v0.Y <= v1.Y <= v2.Y)
            if (v1.Y < v0.Y)
            {
                Swap(ref v0, ref v1);
                Swap(ref n0, ref n1);
            }
            if (v2.Y < v0.Y)
            {
                Swap(ref v0, ref v2);
                Swap(ref n0, ref n2);
            }
            if (v2.Y < v1.Y)
            {
                Swap(ref v1, ref v2);
                Swap(ref n1, ref n2);
            }

            float totalHeight = v2.Y - v0.Y;

            // Loop over the triangle's Y range in logical coordinates
            for (int y = (int)Math.Ceiling(v0.Y); y <= (int)Math.Floor(v2.Y); y++)
            {
                bool secondHalf = y > v1.Y || v1.Y == v0.Y;
                float segmentHeight = secondHalf ? v2.Y - v1.Y : v1.Y - v0.Y;
                if (segmentHeight == 0) continue;

                float alpha = (v2.Y == v0.Y) ? 0 : (y - v0.Y) / totalHeight;
                float beta = (y - (secondHalf ? v1.Y : v0.Y)) / segmentHeight;

                Vector3 A = v0 + (v2 - v0) * alpha;
                Vector3 B = secondHalf ? v1 + (v2 - v1) * beta : v0 + (v1 - v0) * beta;

                Vector3 na = n0 + (n2 - n0) * alpha;
                Vector3 nb = secondHalf ? n1 + (n2 - n1) * beta : n0 + (n1 - n0) * beta;

                Vector3 ta = t0 + (t2 - t0) * alpha;
                Vector3 tb = secondHalf ? t1 + (t2 - t1) * beta : t0 + (t1 - t0) * beta;

                Vector3 ba = b0 + (b2 - b0) * alpha;
                Vector3 bb = secondHalf ? b1 + (b2 - b1) * beta : b0 + (b1 - b0) * beta;

                float ua = u0 + (u2 - u0) * alpha;
                float ub = secondHalf ? u1 + (u2 - u1) * beta : u0 + (u1 - u0) * beta;

                float va = v0_tex + (v2_tex - v0_tex) * alpha;
                float vb = secondHalf ? v1_tex + (v2_tex - v1_tex) * beta : v0_tex + (v1_tex - v0_tex) * beta;

                if (A.X > B.X)
                {
                    Swap(ref A, ref B);
                    Swap(ref na, ref nb);
                    Swap(ref ta, ref tb);
                    Swap(ref ba, ref bb);
                    Swap(ref ua, ref ub);
                    Swap(ref va, ref vb);
                }

                // Loop over the X range between A and B in logical coordinates
                for (int x = (int)Math.Ceiling(A.X); x <= (int)Math.Floor(B.X); x++)
                {
                    float phi = (B.X == A.X) ? 1.0f : (x - A.X) / (B.X - A.X);

                    Vector3 position = A + (B - A) * phi;
                    Vector3 normal = na + (nb - na) * phi;
                    Vector3 tangentU = Vector3.Normalize(ta + (tb - ta) * phi);
                    Vector3 tangentV = Vector3.Normalize(ba + (bb - ba) * phi);

                    // Normalize the normal vector
                    normal = Vector3.Normalize(normal);

                    // Compute the color using the lighting model

                    float u = ua + (ub - ua) * phi;
                    float v = va + (vb - va) * phi;

                    Vector3 pixelColor = Io;

                    if (useTexture && textureImage != null)
                    {
                        // Pobierz wspó³rzêdne u i v w zale¿noœci od pozycji w trójk¹cie
                        u = Math.Clamp(u, 0f, 1f);
                        v = 1 - Math.Clamp(v, 0f, 1f);

                        // Map u and v to texture coordinates
                        int textureX = (int)(u * (textureImage.Width - 1));
                        int textureY = (int)(v * (textureImage.Height - 1));

                        // Get the color from the texture
                        Color texColor = textureImage.GetPixel(textureX, textureY);

                        // Convert the texture color to a Vector3
                        pixelColor = new Vector3(texColor.R / 255f, texColor.G / 255f, texColor.B / 255f);
                    }
                    if (useNormalMap && normalMap != null)
                    {
                        int normalMapX = (int)(u * (normalMap.Width - 1));
                        int normalMapY = (int)((1 - v) * (normalMap.Height - 1));
                        Color normalColor = normalMap.GetPixel(normalMapX, normalMapY);
                        //normalColor = Color.FromArgb(127, 127, 255);

                        Vector3 normalMapVector = new Vector3(
                            (normalColor.R / 255f) * 2 - 1,
                            (normalColor.G / 255f) * 2 - 1,
                            (normalColor.B / 255f) * 2 - 1
                        );


                        var normalNew = new Vector3()
                        {
                           X = tangentU.X*normalMapVector.X + tangentV.X*normalMapVector.Y + normal.X*normalMapVector.Z,
                           Y = tangentU.Y*normalMapVector.X + tangentV.Y*normalMapVector.Y + normal.Y*normalMapVector.Z,
                           Z = tangentU.Z*normalMapVector.X + tangentV.Z*normalMapVector.Y + normal.Z*normalMapVector.Z
                        };

                        //normal = Vector3.TransformNormal(normalMapVector, M);
                        normal = Vector3.Normalize(normalNew);
                    }

                    Vector3 color = ComputeLighting(normal, pixelColor, position);


                    // Convert color from Vector3 (0..1) to Color (0..255)
                    int r = (int)(Math.Min(1.0f, color.X) * 255);
                    int gColor = (int)(Math.Min(1.0f, color.Y) * 255);
                    int b = (int)(Math.Min(1.0f, color.Z) * 255);
                    Color resultColor = Color.FromArgb(r, gColor, b);

                    // Transform logical coordinates to screen coordinates
                    int x_screen = x + (bitmapWidth / 2);
                    int y_screen = (bitmapHeight / 2) - y;

                    // Set the pixel on the bitmap if within bounds
                    if (x_screen >= 0 && x_screen < bitmapWidth && y_screen >= 0 && y_screen < bitmapHeight)
                    {
                        fastBitmap.SetPixel(x_screen, y_screen, resultColor);
                    }
                }
            }
        }

        private void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        private Vector3 ComputeLighting(Vector3 normal, Vector3 pixelColor, Vector3 pixelPosition)
        {
            // Compute the light vector dynamically
            Vector3 L = Vector3.Normalize(lightSourcePos - pixelPosition);

            // Normalize the normal vector
            normal = Vector3.Normalize(normal);

            // Normalize the view vector
            Vector3 V_normalized = Vector3.Normalize(V);

            // Compute cos(theta) between N and L
            float cosNL = Vector3.Dot(normal, L);
            cosNL = Math.Max(0, cosNL); // Clamp to 0 if negative

            // Compute R = 2(N•L)N - L
            Vector3 R = 2 * cosNL * normal - L;
            R = Vector3.Normalize(R);

            // Compute cos(phi) between R and V
            float cosRV = Vector3.Dot(R, V_normalized);
            cosRV = Math.Max(0, cosRV); // Clamp to 0 if negative

            // Compute diffuse component
            Vector3 diffuse = kd * Il * pixelColor * cosNL;

            // Compute specular component
            Vector3 specular = ks * Il * pixelColor * (float)Math.Pow(cosRV, m);

            // Total intensity
            Vector3 I = diffuse + specular;

            // Ensure the intensity values are within 0..1
            I = Vector3.Min(I, new Vector3(1, 1, 1));

            return I;
        }

        private void DrawTriangle(Graphics g, Triangle triangle)
        {
            Point[] points = new Point[3];

            points[0] = TransformToScreen(triangle.Vertex1.PositionAfterRotation);
            points[1] = TransformToScreen(triangle.Vertex2.PositionAfterRotation);
            points[2] = TransformToScreen(triangle.Vertex3.PositionAfterRotation);

            // Rysowanie trójk¹ta za pomoc¹ trzech linii
            g.DrawLine(Pens.Black, points[0], points[1]);
            g.DrawLine(Pens.Black, points[1], points[2]);
            g.DrawLine(Pens.Black, points[2], points[0]);
        }

        private void DrawControlPoints(Graphics g)
        {
            int pointSize = 4; // Rozmiar punktu kontrolnego w pikselach
            Brush controlPointBrush = Brushes.Blue;

            // Iterujemy przez wszystkie punkty kontrolne w tablicy 4x4
            for (int i = 0; i < controlPoints.GetLength(0); i++)
            {
                for (int j = 0; j < controlPoints.GetLength(1); j++)
                {
                    Vector3 point = controlPoints[i, j];
                    Point screenPoint = TransformToScreen(point);

                    // Rysowanie punktu jako ma³ego kó³ka
                    g.FillEllipse(controlPointBrush, screenPoint.X - pointSize / 2, screenPoint.Y - pointSize / 2, pointSize, pointSize);
                }
            }
        }

        private Point TransformToScreen(Vector3 point)
        {
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;
            int x = (int)(point.X) + centerX;
            int y = centerY - (int)(point.Y);
            return new Point(x, y);
        }

        private void fileDialogButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Wybierz plik z punktami"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                LoadControlPoints(filePath); // Wywo³anie funkcji do wczytania punktów

            }
        }
        private void LoadControlPoints(string filePath)
        {
            List<Vector3> points = new List<Vector3>();

            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var coords = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (coords.Length != 3)
                    {
                        MessageBox.Show("Nieprawid³owy format pliku. Ka¿dy wiersz powinien zawieraæ dok³adnie 3 wartoœci.");
                        return;
                    }

                    float x = float.Parse(coords[0]);
                    float y = float.Parse(coords[1]);
                    float z = float.Parse(coords[2]);
                    points.Add(new Vector3(x, y, z));
                }

                if (points.Count != 16)
                {
                    MessageBox.Show("Plik powinien zawieraæ dok³adnie 16 punktów kontrolnych.");
                    return;
                }

                // Wype³nij macierz 4x4 punktami kontrolnymi
                controlPoints = new Vector3[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        controlPoints[i, j] = points[i * 4 + j];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d podczas wczytywania pliku: " + ex.Message);
            }
        }

        private void accuracyTrackBar_Scroll(object sender, EventArgs e)
        {
            label2.Text = accuracyTrackBar.Value.ToString();
            bezierSurface.GenerateSurface(accuracyTrackBar.Value); // Generacja z now¹ dok³adnoœci¹
            DrawSurface(); // Rysowanie zaktualizowanej powierzchni
        }

        private void zTurnTrackBar_Scroll(object sender, EventArgs e)
        {
            label4.Text = zTurnTrackBar.Value.ToString();
            int angle = zTurnTrackBar.Value;
            bezierSurface.AngleZ = angle;


            Parallel.ForEach(bezierSurface.Triangles, t =>
            {
                t.Vertex1.ApplyRotation(bezierSurface.AngleX, bezierSurface.AngleZ, controlPoints);
                t.Vertex2.ApplyRotation(bezierSurface.AngleX, bezierSurface.AngleZ, controlPoints);
                t.Vertex3.ApplyRotation(bezierSurface.AngleX, bezierSurface.AngleZ, controlPoints);
                t.UpdateAverageZ();
            });

            DrawSurface();
        }

        private void xTurnTrackBar_Scroll(object sender, EventArgs e)
        {
            label6.Text = xTurnTrackBar.Value.ToString();
            int angle = xTurnTrackBar.Value;
            bezierSurface.AngleX = angle;

            Parallel.ForEach(bezierSurface.Triangles, t =>
            {
                t.Vertex1.ApplyRotation(bezierSurface.AngleX, bezierSurface.AngleZ, controlPoints);
                t.Vertex2.ApplyRotation(bezierSurface.AngleX, bezierSurface.AngleZ, controlPoints);
                t.Vertex3.ApplyRotation(bezierSurface.AngleX, bezierSurface.AngleZ, controlPoints);
                t.UpdateAverageZ();
            });
            DrawSurface();
        }

        private void kdTrackBar_Scroll(object sender, EventArgs e)
        {
            label8.Text = ((float)kdTrackBar.Value / 100).ToString();
            kd = (float)kdTrackBar.Value / 100;
            DrawSurface();
        }

        private void ksTrackBar_Scroll(object sender, EventArgs e)
        {
            label10.Text = ((float)ksTrackBar.Value / 100).ToString();
            ks = (float)ksTrackBar.Value / 100;
            DrawSurface();
        }

        private void mTrackBar_Scroll(object sender, EventArgs e)
        {
            label12.Text = mTrackBar.Value.ToString();
            m = mTrackBar.Value;
            DrawSurface();
        }

        private void lightColorButton_Click(object sender, EventArgs e)
        {
            if (lightColorDialog.ShowDialog(this) == DialogResult.OK)
            {
                lightColorPanel.BackColor = lightColorDialog.Color;
                Il = new Vector3((float)lightColorDialog.Color.R / 255, (float)lightColorDialog.Color.G / 255, (float)lightColorDialog.Color.B / 255);
                DrawSurface();
            }
        }

        private void surfaceColorButton_Click(object sender, EventArgs e)
        {
            if (surfaceColorDialog.ShowDialog(this) == DialogResult.OK)
            {
                surfaceColorPanel.BackColor = surfaceColorDialog.Color;
                Io = new Vector3((float)surfaceColorDialog.Color.R / 255, (float)surfaceColorDialog.Color.G / 255, (float)surfaceColorDialog.Color.B / 255);
                DrawSurface();
            }
        }

        private void colorStructureRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (colorStructureRadioButton.Checked)
            {
                useTexture = false;
                DrawSurface(); // Prze³¹cz na kolor sta³y i rysuj ponownie
            }
        }

        private void ImageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ImageRadioButton.Checked)
            {
                useTexture = true;
                if (textureImage == null)
                {
                    LoadDefaultTexture(); // Wczytaj domyœlny obraz
                }
                DrawSurface(); // Prze³¹cz na teksturê i rysuj ponownie
            }
        }

        private void chooseImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif",
                Title = "Wybierz obraz tekstury"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Dispose the previous texture if it exists
                    textureImage?.Dispose();

                    // Load the new texture as a DirectBitmap
                    using (var loadedBitmap = new Bitmap(openFileDialog.FileName))
                    {
                        textureImage = new DirectBitmap(loadedBitmap.Width, loadedBitmap.Height);
                        using (Graphics g = Graphics.FromImage(textureImage.Bitmap))
                        {
                            g.DrawImage(loadedBitmap, 0, 0, loadedBitmap.Width, loadedBitmap.Height);
                        }
                    }

                    // Update the picture panel to display the thumbnail of the texture
                    UpdatePicturePanelThumbnail(textureImage.Bitmap);

                    useTexture = true;
                    ImageRadioButton.Checked = true; // Switch to texture mode
                    DrawSurface();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("B³¹d podczas ³adowania obrazu tekstury: " + ex.Message);
                }
            }
        }

        private void LoadDefaultTexture()
        {
            try
            {
                // Dispose the previous texture if it exists
                textureImage?.Dispose();

                // Load the default texture as a DirectBitmap
                using (var loadedBitmap = new Bitmap("image.jpg"))
                {
                    textureImage = new DirectBitmap(loadedBitmap.Width, loadedBitmap.Height);
                    using (Graphics g = Graphics.FromImage(textureImage.Bitmap))
                    {
                        g.DrawImage(loadedBitmap, 0, 0, loadedBitmap.Width, loadedBitmap.Height);
                    }
                }

                // Update the picture panel to display the thumbnail of the default texture
                UpdatePicturePanelThumbnail(textureImage.Bitmap);
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d podczas ³adowania domyœlnego obrazu tekstury: " + ex.Message);
            }
        }

        private void UpdatePicturePanelThumbnail(Bitmap textureBitmap)
        {
            try
            {
                // Create a thumbnail of the texture
                Bitmap thumbnail = new Bitmap(picturePanel.Width, picturePanel.Height);
                using (Graphics g = Graphics.FromImage(thumbnail))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(textureBitmap, 0, 0, picturePanel.Width, picturePanel.Height);
                }

                // Set the panel's background image to the thumbnail
                picturePanel.BackgroundImage = thumbnail;
                picturePanel.BackgroundImageLayout = ImageLayout.Zoom; // Scale the image to fit the panel
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d podczas tworzenia miniaturki tekstury: " + ex.Message);
            }
        }


        private void normalMapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useNormalMap = normalMapCheckBox.Checked;
            DrawSurface();
        }

        private void loadNormalButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif",
                Title = "Wybierz mapê wektorów normalnych"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Dispose the previous normal map if it exists
                    normalMap?.Dispose();

                    // Load the new normal map as a DirectBitmap
                    using (var loadedBitmap = new Bitmap(openFileDialog.FileName))
                    {
                        normalMap = new DirectBitmap(loadedBitmap.Width, loadedBitmap.Height);
                        using (Graphics g = Graphics.FromImage(normalMap.Bitmap))
                        {
                            g.DrawImage(loadedBitmap, 0, 0, loadedBitmap.Width, loadedBitmap.Height);
                        }
                    }

                    // Update the panel to display the thumbnail
                    UpdateNormalPanelThumbnail(normalMap.Bitmap);

                    useNormalMap = true;
                    normalMapCheckBox.Checked = true; // Set the checkbox as selected
                    DrawSurface();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("B³¹d podczas ³adowania mapy normalnych: " + ex.Message);
                }
            }
        }

        private void LoadDefaultNormalMap()
        {
            try
            {
                // Dispose the previous normal map if it exists
                normalMap?.Dispose();

                // Load the default normal map as a DirectBitmap
                using (var loadedBitmap = new Bitmap("normal.jpg"))
                {
                    normalMap = new DirectBitmap(loadedBitmap.Width, loadedBitmap.Height);
                    using (Graphics g = Graphics.FromImage(normalMap.Bitmap))
                    {
                        g.DrawImage(loadedBitmap, 0, 0, loadedBitmap.Width, loadedBitmap.Height);
                    }
                }

                // Update the panel to display the thumbnail
                UpdateNormalPanelThumbnail(normalMap.Bitmap);
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d podczas ³adowania domyœlnej mapy normalnych: " + ex.Message);
            }
        }

        private void UpdateNormalPanelThumbnail(Bitmap normalMapBitmap)
        {
            try
            {
                // Create a thumbnail of the normal map
                Bitmap thumbnail = new Bitmap(normalMapPanel.Width, normalMapPanel.Height);
                using (Graphics g = Graphics.FromImage(thumbnail))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(normalMapBitmap, 0, 0, normalMapPanel.Width, normalMapPanel.Height);
                }

                // Set the panel's background image to the thumbnail
                normalMapPanel.BackgroundImage = thumbnail;
                normalMapPanel.BackgroundImageLayout = ImageLayout.Zoom; // Scale the image to fit the panel
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d podczas tworzenia miniaturki mapy normalnych: " + ex.Message);
            }
        }


        private void drawTrianglesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            drawTriangles = drawTrianglesCheckBox.Checked;
            DrawSurface();
        }

        private void fillTrianglesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            fillTriangles = fillTrianglesCheckBox.Checked;
            DrawSurface();
        }

        private void lightMoveButton_Click(object sender, EventArgs e)
        {
            if (lightMovement)
            {
                lightMovement = false;
                lightMoveButton.Text = "Wznów ruch œwiat³a";
            }
            else
            {
                lightMovement = true;
                lightMoveButton.Text = "Zatrzymaj ruch œwiat³a";
            }
        }
    }
}
