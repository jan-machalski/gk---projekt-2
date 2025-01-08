using System;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Forms;

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
		private Vector3 lightSourcePos2 = new Vector3(0, 0, 500);
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
		private System.Windows.Forms.Timer pyramidMovementTimer;
		private float spiralAngle = 0; // Current angle for spiral movement
		private float spiralRadius = 0; // Current radius for spiral movement
		private float spiralRadiusIncrement = 2; // Radius increment per step
		private float spiralAngleIncrement = 0.1f; // Angle increment per step (in radians)
		bool lightMovement = true;
		string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
		private int reflectorIntensity = 50;
		private bool useReflector = false;
		List<Triangle> pyramid = new List<Triangle>();
		int rotationDeg = 0;


		public Form1()
		{
			InitializeComponent();

			LoadControlPoints("data/points.txt");

			InitializeBitmap();
			bitmapHeight = directBitmap.Height;
			bitmapWidth = directBitmap.Width;

			LoadDefaultNormalMap();
			LoadDefaultTexture();


			bezierSurface = new BezierSurface(controlPoints, accuracyTrackBar.Value);
			InitializePyramid();

			InitializeLightMovement();

			DrawSurface();
		}
		private void InitializeBitmap()
		{
			directBitmap = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
			pictureBox1.Image = directBitmap.Bitmap;
		}
		private Triangle MakeTriangle(Vector3 posA, Vector3 posB, Vector3 posC)
		{
			Vector3 AB = posB - posA;
			Vector3 AC = posC - posA;
			Vector3 normal = Vector3.Cross(AB, AC);
			normal = Vector3.Normalize(normal);

			var vA = new Vertex(posA, Vector3.Zero, Vector3.Zero, Vector3.Zero, 0f, 0f);
			var vB = new Vertex(posB, Vector3.Zero, Vector3.Zero, Vector3.Zero, 0f, 0f);
			var vC = new Vertex(posC, Vector3.Zero, Vector3.Zero, Vector3.Zero, 0f, 0f);


			vA.NormalBeforeRotation = normal;
			vB.NormalBeforeRotation = normal;
			vC.NormalBeforeRotation = normal;

			return new Triangle(vA, vB, vC);
		}
		private void InitializePyramid()
		{
			// Lista trójk¹tów ostros³upa
			pyramid = new List<Triangle>();

			// Rozmiar podstawy i wysokoœæ ostros³upa
			float halfBaseSize = 100f; // Po³owa d³ugoœci boku podstawy
			float height = 400f;      // Wysokoœæ ostros³upa

			// Wierzcho³ki podstawy (w p³aszczyŸnie Z = 0)
			var v0 = new Vertex(
				new Vector3(-halfBaseSize, -halfBaseSize, 0), // Pozycja
				new Vector3(1, 0, 0),                        // TangentU dla podstawy
				new Vector3(0, 1, 0),                        // TangentV dla podstawy
				new Vector3(0, 0, 1),                        // Normal (w górê dla podstawy)
				0, 0                                         // Wspó³rzêdne tekstury (UV)
			);

			var v1 = new Vertex(
				new Vector3(halfBaseSize, -halfBaseSize, 0),
				new Vector3(1, 0, 0),
				new Vector3(0, 1, 0),
				new Vector3(0, 0, 1),
				1, 0
			);

			var v2 = new Vertex(
				new Vector3(halfBaseSize, halfBaseSize, 0),
				new Vector3(1, 0, 0),
				new Vector3(0, 1, 0),
				new Vector3(0, 0, 1),
				1, 1
			);

			var v3 = new Vertex(
				new Vector3(-halfBaseSize, halfBaseSize, 0),
				new Vector3(1, 0, 0),
				new Vector3(0, 1, 0),
				new Vector3(0, 0, 1),
				0, 1
			);

			var apex = new Vertex(
				new Vector3(0, 0, height),
				Vector3.Zero,                             
				Vector3.Zero,                             
				Vector3.Zero,                               
				0.5f, 0.5f                               
			);

			pyramid.Add(new Triangle(v0, v1, v2)); 
			pyramid.Add(new Triangle(v0, v2, v3));
			pyramid.Add(MakeTriangle(v0.PositionBeforeRotation,v1.PositionBeforeRotation,apex.PositionBeforeRotation));
			pyramid.Add(MakeTriangle(v1.PositionBeforeRotation, v2.PositionBeforeRotation, apex.PositionBeforeRotation));
			pyramid.Add(MakeTriangle(v2.PositionBeforeRotation, v3.PositionBeforeRotation, apex.PositionBeforeRotation));
			pyramid.Add(MakeTriangle(v3.PositionBeforeRotation, v0.PositionBeforeRotation, apex.PositionBeforeRotation));
		}

		private void InitializeLightMovement()
		{
			lightMoveTimer = new System.Windows.Forms.Timer();
			lightMoveTimer.Interval = 50;
			lightMoveTimer.Tick += LightMoveTimer_Tick;
			lightMoveTimer.Start();
			pyramidMovementTimer = new System.Windows.Forms.Timer();
			pyramidMovementTimer.Interval = 50;
			pyramidMovementTimer.Tick += PyramidTimer_Tick;
			pyramidMovementTimer.Start();
		}
		private void PyramidTimer_Tick(object sender, EventArgs e)
		{
			foreach(var t in pyramid)
			{
				t.Vertex1.ApplyRotation(rotationDeg, rotationDeg);
				t.Vertex2.ApplyRotation(rotationDeg, rotationDeg);
				t.Vertex3.ApplyRotation(rotationDeg, rotationDeg);
			}
			rotationDeg = (rotationDeg + 1) % 360;
		}
		private void LightMoveTimer_Tick(object sender, EventArgs e)
		{
			if (lightMovement)
			{
				// Update the angle and radius for the spiral motion
				spiralAngle += spiralAngleIncrement;
				spiralRadius += spiralRadiusIncrement * spiralAngleIncrement;

				// Calculate new x and y positions in the spiral
				float x = (float)(spiralRadius * Math.Cos(spiralAngle));
				float y = (float)(spiralRadius * Math.Sin(spiralAngle));

				// Update the light source position
				lightSourcePos = new Vector3(x, y, lightSourcePos.Z);

				lightSourcePos2 = new Vector3(-x, -y, lightSourcePos2.Z);


			}

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
			float[,] zBuffer = new float[directBitmap.Width, directBitmap.Height];
			for (int x = 0; x < directBitmap.Width; x++)
			{
				for (int y = 0; y < directBitmap.Height; y++)
				{
					zBuffer[x, y] = float.MinValue; // Inicjuj jako maksymaln¹ wartoœæ
				}
			}
			using (Graphics g = Graphics.FromImage(directBitmap.Bitmap))
			{

				// Rysowanie trójk¹tów powierzchni Béziera


				g.Clear(Color.White);
				if (fillTriangles)
				{
					/*Parallel.ForEach(bezierSurface.Triangles, t =>
					{
						FillTriangle(t, directBitmap,zBuffer);
					});*/
					Parallel.ForEach(bezierSurface.Triangles, t =>
					{
						FillPolygon(new List<Vertex>() { t.Vertex1, t.Vertex2, t.Vertex3 }, directBitmap, zBuffer);
					});
					Parallel.ForEach(pyramid, t =>
					{
						FillPolygon(new List<Vertex>() { t.Vertex1, t.Vertex2, t.Vertex3 }, directBitmap, zBuffer, true);
					});
				}

				if (drawTriangles)
				{
					foreach (var t in bezierSurface.Triangles)
					{
						DrawTriangle(g, t);
					}
					foreach(var t in pyramid)
						DrawTriangle(g, t);
				}


				// Rysowanie punktów kontrolnych
				//DrawControlPoints(g);
			}
			pictureBox1.Invalidate();

		}

		private void FillPolygon(List<Vertex> vertices, DirectBitmap fastBitmap, float[,] zBuffer,bool pyramid = false)
		{
			int n = vertices.Count;

			// Create an array of indices sorted by Y-coordinate
			int[] ind = new int[n];
			for (int i = 0; i < n; i++)
			{
				ind[i] = i;
			}
			ind = ind.OrderBy(i => vertices[i].PositionAfterRotation.Y).ToArray();

			// Find ymin and ymax
			int ymin = (int)Math.Ceiling(vertices[ind[0]].PositionAfterRotation.Y);
			int ymax = (int)Math.Floor(vertices[ind[n - 1]].PositionAfterRotation.Y);

			// Initialize Active Edge Table (AET)
			List<AETEntry> AET = new List<AETEntry>();

			// For each scanline y from ymin to ymax
			for (int y = ymin; y <= ymax; y++)
			{
				// Update AET for the current scanline
				UpdateAET(vertices, ind, y, AET);

				// Remove edges where yMax == current y
				AET.RemoveAll(e => (int)Math.Ceiling(e.YMax) == y);

				// Update AET: sort by CurrentX
				AET.Sort((e1, e2) => e1.CurrentX.CompareTo(e2.CurrentX));

				// Fill pixels between pairs of edges
				for (int i = 0; i < AET.Count - 1; i += 2)
				{
					AETEntry e1 = AET[i];
					AETEntry e2 = AET[i + 1];

					int xStart = (int)Math.Ceiling(e1.CurrentX);
					int xEnd = (int)Math.Floor(e2.CurrentX);

					if (xEnd < xStart)
						continue;

					float totalHeight = ymax - ymin;

					// Compute alpha for the overall height
					float alpha = totalHeight == 0 ? 0 : (y - ymin) / totalHeight;

					// Compute beta for each edge
					float segmentHeight1 = e1.YMax - e1.YMin;
					float beta1 = segmentHeight1 == 0 ? 0 : (y - e1.YMin) / segmentHeight1;

					float segmentHeight2 = e2.YMax - e2.YMin;
					float beta2 = segmentHeight2 == 0 ? 0 : (y - e2.YMin) / segmentHeight2;

					// Interpolate attributes along the edges
					var A = InterpolateVertexAttributes(e1.V0, e1.V2, beta1);
					var B = InterpolateVertexAttributes(e2.V0, e2.V2, beta2);

					if (A.Position.X > B.Position.X)
					{
						Swap(ref A, ref B);
					}

					// Loop over the X range between A and B in logical coordinates
					for (int x = xStart; x <= xEnd; x++)
					{
						float phi = (B.Position.X == A.Position.X) ? 1.0f : (x - A.Position.X) / (B.Position.X - A.Position.X);

						Vector3 position = A.Position + (B.Position - A.Position) * phi;
						float z = position.Z;
						Vector3 normal = Vector3.Normalize(A.Normal + (B.Normal - A.Normal) * phi);
						Vector3 tangentU = Vector3.Normalize(A.TangentU + (B.TangentU - A.TangentU) * phi);
						Vector3 tangentV = Vector3.Normalize(A.TangentV + (B.TangentV - A.TangentV) * phi);

						float u = A.U + (B.U - A.U) * phi;
						float v = A.V + (B.V - A.V) * phi;

						Vector3 pixelColor = Io;

						if (!pyramid && useTexture && textureImage != null)
						{
							u = Math.Clamp(u, 0f, 1f);
							v = 1 - Math.Clamp(v, 0f, 1f);

							int textureX = (int)(u * (textureImage.Width - 1));
							int textureY = (int)(v * (textureImage.Height - 1));

							Color texColor = textureImage.GetPixel(textureX, textureY);
							pixelColor = new Vector3(texColor.R / 255f, texColor.G / 255f, texColor.B / 255f);
						}
						if (!pyramid && useNormalMap && normalMap != null)
						{
							u = Math.Clamp(u, 0f, 1f);
							v = 1 - Math.Clamp(v, 0f, 1f);
							int normalMapX = (int)(u * (normalMap.Width - 1));
							int normalMapY = (int)((v) * (normalMap.Height - 1));
							Color normalColor = normalMap.GetPixel(normalMapX, normalMapY);

							Vector3 normalMapVector = new Vector3(
								(normalColor.R / 255f) * 2 - 1,
								(normalColor.G / 255f) * 2 - 1,
								(normalColor.B / 255f) * 2 - 1
							);

							var normalNew = new Vector3()
							{
								X = tangentU.X * normalMapVector.X + tangentV.X * normalMapVector.Y + normal.X * normalMapVector.Z,
								Y = tangentU.Y * normalMapVector.X + tangentV.Y * normalMapVector.Y + normal.Y * normalMapVector.Z,
								Z = tangentU.Z * normalMapVector.X + tangentV.Z * normalMapVector.Y + normal.Z * normalMapVector.Z
							};

							normal = Vector3.Normalize(normalNew);
						}
						if (pyramid)
						{
							Swap(ref pixelColor.Y, ref pixelColor.Z);
							Swap(ref pixelColor.X, ref pixelColor.Y);	
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
							if (z > zBuffer[x_screen, y_screen])
							{
								zBuffer[x_screen, y_screen] = z; // Update Z-buffer
								fastBitmap.SetPixel(x_screen, y_screen, resultColor); // Draw pixel
							}
						}
					}
				}

				// Update CurrentX for each edge in AET
				foreach (var edge in AET)
				{
					edge.CurrentX += edge.OneOverM;
				}
			}
		}

		// Method to update AET for the current scanline
		private void UpdateAET(List<Vertex> vertices, int[] ind, int y, List<AETEntry> AET)
		{
			int n = vertices.Count;

			for (int k = 0; k < n; k++)
			{
				int i = ind[k];
				int vertexY = (int)Math.Ceiling(vertices[i].PositionAfterRotation.Y);

				if (vertexY == y)
				{
					Vertex Pi = vertices[i];
					int prevIndex = (i - 1 + n) % n;
					Vertex Pi_prev = vertices[prevIndex];
					int nextIndex = (i + 1) % n;
					Vertex Pi_next = vertices[nextIndex];

					// Edge Pi to Pi_prev
					if ((int)Math.Ceiling(Pi_prev.PositionAfterRotation.Y) > vertexY)
						AET.Add(new AETEntry(Pi, Pi_prev));
					else if ((int)Math.Ceiling(Pi_prev.PositionAfterRotation.Y) < vertexY)
						AET.RemoveAll(e => e.EqualsEdge(Pi, Pi_prev));

					// Edge Pi to Pi_next
					if ((int)Math.Ceiling(Pi_next.PositionAfterRotation.Y) > vertexY)
						AET.Add(new AETEntry(Pi, Pi_next));
					else if ((int)Math.Ceiling(Pi_next.PositionAfterRotation.Y) < vertexY)
						AET.RemoveAll(e => e.EqualsEdge(Pi, Pi_next));
				}
			}
		}

		// Method to interpolate attributes between two vertices
		private InterpolatedAttributes InterpolateVertexAttributes(Vertex v0, Vertex v2, float t)
		{
			Vector3 position = v0.PositionAfterRotation + (v2.PositionAfterRotation - v0.PositionAfterRotation) * t;
			Vector3 normal = Vector3.Normalize(v0.NormalAfterRotation + (v2.NormalAfterRotation - v0.NormalAfterRotation) * t);
			Vector3 tangentU = Vector3.Normalize(v0.TangentUAfterRotation + (v2.TangentUAfterRotation - v0.TangentUAfterRotation) * t);
			Vector3 tangentV = Vector3.Normalize(v0.TangentVAfterRotation + (v2.TangentVAfterRotation - v0.TangentVAfterRotation) * t);
			float u = v0.U + (v2.U - v0.U) * t;
			float v = v0.V + (v2.V - v0.V) * t;

			return new InterpolatedAttributes
			{
				Position = position,
				Normal = normal,
				TangentU = tangentU,
				TangentV = tangentV,
				U = u,
				V = v
			};
		}

		// Class for interpolated attributes
		private class InterpolatedAttributes
		{
			public Vector3 Position;
			public Vector3 Normal;
			public Vector3 TangentU;
			public Vector3 TangentV;
			public float U;
			public float V;
		}

		private void Swap<T>(ref T a, ref T b)
		{
			T temp = a;
			a = b;
			b = temp;
		}

		private Vector3 ComputeLighting(Vector3 normal, Vector3 pixelColor, Vector3 pixelPosition)
		{
			// Compute the light vector
			Vector3 L = Vector3.Normalize(lightSourcePos - pixelPosition);

			// Normalize the normal vector
			normal = Vector3.Normalize(normal);

			// Normalize the view vector
			Vector3 V_normalized = Vector3.Normalize(V);

			if (Vector3.Dot(normal, V_normalized) < 0)
			{
				normal = -normal; // Odwróæ normaln¹
			}
			// Compute cos(theta) between N and L
			float cosNL = Vector3.Dot(normal, L);

			cosNL = Math.Max(0, cosNL); // Clamp to 0 if negative

			// Compute R = 2(N•L)N - L
			Vector3 R = 2 * Vector3.Dot(normal, L) * normal - L;
			//R = Vector3.Normalize(R);

			// Compute cos(phi) between R and V
			float cosRV = Vector3.Dot(R, V_normalized);
			cosRV = Math.Max(0, cosRV); // Clamp to 0 if negative
			

			Vector3 L2 = Vector3.Normalize(lightSourcePos2 - pixelPosition);
			float cosNL2 = Vector3.Dot(normal, L2);

			cosNL2 = Math.Max(0, cosNL2);
			Vector3 R2 = 2 * Vector3.Dot(normal, L2) * normal - L2;
			//R2 = Vector3.Normalize(R2);

			float cosRV2 = Vector3.Dot(R2, V_normalized);
			cosRV2 = Math.Max(0, cosRV2); // Clamp to 0 if negative
			Vector3 lp1 = Vector3.Normalize(lightSourcePos);
			Vector3 lp2 = Vector3.Normalize(lightSourcePos2);

			float factor1 = (float)Math.Pow(Vector3.Dot(L,lp1), reflectorIntensity);
			float factor2 = (float)Math.Pow(Vector3.Dot(L2,lp2), reflectorIntensity);
			if (!useReflector)
				factor1 = factor2 = 1;

			Vector3 diffuse = factor1 * kd * Il * pixelColor * cosNL;
			Vector3 diffuse2 = factor2 * kd * Il * pixelColor * cosNL2;

			// Compute specular component
			Vector3 specular = factor1 * ks * Il * pixelColor * (float)Math.Pow(cosRV, m);

			Vector3 specular2 = factor2 * ks * Il * pixelColor * (float)Math.Pow(cosRV2, m);

			// Total intensity
			Vector3 I = diffuse + specular + diffuse2 + specular2;

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
				bezierSurface = new BezierSurface(controlPoints, accuracyTrackBar.Value);
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
		}

		private void zTurnTrackBar_Scroll(object sender, EventArgs e)
		{
			label4.Text = zTurnTrackBar.Value.ToString();
			int angle = zTurnTrackBar.Value;
			bezierSurface.AngleZ = angle;


			Parallel.ForEach(bezierSurface.Vertices, v =>
			{
				v.ApplyRotation(bezierSurface.AngleX, bezierSurface.AngleZ, controlPoints);

			});

		}

		private void xTurnTrackBar_Scroll(object sender, EventArgs e)
		{
			label6.Text = xTurnTrackBar.Value.ToString();
			int angle = xTurnTrackBar.Value;
			bezierSurface.AngleX = angle;

			Parallel.ForEach(bezierSurface.Vertices, v =>
			{
				v.ApplyRotation(bezierSurface.AngleX, bezierSurface.AngleZ, controlPoints);

			});
		}

		private void kdTrackBar_Scroll(object sender, EventArgs e)
		{
			label8.Text = ((float)kdTrackBar.Value / 100).ToString();
			kd = (float)kdTrackBar.Value / 100;
		}

		private void ksTrackBar_Scroll(object sender, EventArgs e)
		{
			label10.Text = ((float)ksTrackBar.Value / 100).ToString();
			ks = (float)ksTrackBar.Value / 100;
		}

		private void mTrackBar_Scroll(object sender, EventArgs e)
		{
			label12.Text = mTrackBar.Value.ToString();
			m = mTrackBar.Value;
		}

		private void lightColorButton_Click(object sender, EventArgs e)
		{
			if (lightColorDialog.ShowDialog(this) == DialogResult.OK)
			{
				lightColorPanel.BackColor = lightColorDialog.Color;
				Il = new Vector3((float)lightColorDialog.Color.R / 255, (float)lightColorDialog.Color.G / 255, (float)lightColorDialog.Color.B / 255);
			}
		}

		private void surfaceColorButton_Click(object sender, EventArgs e)
		{
			if (surfaceColorDialog.ShowDialog(this) == DialogResult.OK)
			{
				surfaceColorPanel.BackColor = surfaceColorDialog.Color;
				Io = new Vector3((float)surfaceColorDialog.Color.R / 255, (float)surfaceColorDialog.Color.G / 255, (float)surfaceColorDialog.Color.B / 255);
			}
		}

		private void colorStructureRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (colorStructureRadioButton.Checked)
			{
				useTexture = false;
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
				//DrawSurface(); // Prze³¹cz na teksturê i rysuj ponownie
			}
		}

		private void chooseImageButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif",
				Title = "Wybierz obraz tekstury"
			};
			if (Directory.Exists(dataFolderPath))
			{
				openFileDialog.InitialDirectory = dataFolderPath;
			}

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
				using (var loadedBitmap = new Bitmap("data/image.jpg"))
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
		}

		private void loadNormalButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif",
				Title = "Wybierz obraz tekstury"
			};
			if (Directory.Exists(dataFolderPath))
			{
				openFileDialog.InitialDirectory = dataFolderPath;
			}

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
				using (var loadedBitmap = new Bitmap("data/normal.jpg"))
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
		}

		private void fillTrianglesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			fillTriangles = fillTrianglesCheckBox.Checked;
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

		private void lightSourceTrackBar_Scroll(object sender, EventArgs e)
		{
			label15.Text = lightSourceTrackBar.Value.ToString();
			lightSourcePos = new Vector3(lightSourcePos.X, lightSourcePos.Y, lightSourceTrackBar.Value);
			lightSourcePos2 = new Vector3(lightSourcePos2.X, lightSourcePos2.Y, lightSourceTrackBar.Value);
		}

		private void reflectorAngleBar_Scroll(object sender, EventArgs e)
		{
			reflectorIntensity = reflectorAngleBar.Value;
			label16.Text = reflectorIntensity.ToString();
		}

		private void pointLightButton_CheckedChanged(object sender, EventArgs e)
		{
			if (pointLightButton.Checked)
				useReflector = false;

		}

		private void reflectorLightButton_CheckedChanged(object sender, EventArgs e)
		{
			if (reflectorLightButton.Checked)
				useReflector = true;
		}
	}
}
