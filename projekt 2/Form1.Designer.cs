namespace projekt_2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			pictureBox1 = new PictureBox();
			fileDialogButton = new Button();
			accuracyTrackBar = new TrackBar();
			label1 = new Label();
			label2 = new Label();
			zTurnTrackBar = new TrackBar();
			label3 = new Label();
			label4 = new Label();
			xTurnTrackBar = new TrackBar();
			label5 = new Label();
			label6 = new Label();
			kdTrackBar = new TrackBar();
			label7 = new Label();
			label8 = new Label();
			ksTrackBar = new TrackBar();
			label9 = new Label();
			label10 = new Label();
			mTrackBar = new TrackBar();
			label11 = new Label();
			label12 = new Label();
			lightColorButton = new Button();
			lightColorPanel = new Panel();
			surfaceColorButton = new Button();
			surfaceColorPanel = new Panel();
			chooseImageButton = new Button();
			colorStructureRadioButton = new RadioButton();
			label13 = new Label();
			ImageRadioButton = new RadioButton();
			normalMapCheckBox = new CheckBox();
			loadNormalButton = new Button();
			drawTrianglesCheckBox = new CheckBox();
			fillTrianglesCheckBox = new CheckBox();
			lightMoveButton = new Button();
			picturePanel = new Panel();
			normalMapPanel = new Panel();
			lightSourceTrackBar = new TrackBar();
			label14 = new Label();
			label15 = new Label();
			pointLightButton = new RadioButton();
			reflectorLightButton = new RadioButton();
			reflectorAngleBar = new TrackBar();
			label16 = new Label();
			label17 = new Label();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)accuracyTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)zTurnTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)xTurnTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)kdTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)ksTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)mTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)lightSourceTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)reflectorAngleBar).BeginInit();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			pictureBox1.Location = new Point(0, 0);
			pictureBox1.Margin = new Padding(4, 5, 4, 5);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(1332, 939);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			pictureBox1.Paint += pictureBox1_Paint;
			// 
			// fileDialogButton
			// 
			fileDialogButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			fileDialogButton.Location = new Point(1369, 50);
			fileDialogButton.Margin = new Padding(4, 5, 4, 5);
			fileDialogButton.Name = "fileDialogButton";
			fileDialogButton.Size = new Size(230, 45);
			fileDialogButton.TabIndex = 1;
			fileDialogButton.Text = "Załaduj plik z punktami";
			fileDialogButton.UseVisualStyleBackColor = true;
			fileDialogButton.Click += fileDialogButton_Click;
			// 
			// accuracyTrackBar
			// 
			accuracyTrackBar.AccessibleName = "";
			accuracyTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			accuracyTrackBar.Location = new Point(1359, 150);
			accuracyTrackBar.Margin = new Padding(4, 5, 4, 5);
			accuracyTrackBar.Maximum = 50;
			accuracyTrackBar.Minimum = 3;
			accuracyTrackBar.Name = "accuracyTrackBar";
			accuracyTrackBar.RightToLeft = RightToLeft.No;
			accuracyTrackBar.Size = new Size(231, 69);
			accuracyTrackBar.TabIndex = 2;
			accuracyTrackBar.Value = 10;
			accuracyTrackBar.Scroll += accuracyTrackBar_Scroll;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label1.AutoSize = true;
			label1.Location = new Point(1374, 120);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(197, 25);
			label1.TabIndex = 3;
			label1.Text = "Dokładność triangulacji";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new Point(1596, 150);
			label2.Margin = new Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new Size(32, 25);
			label2.TabIndex = 4;
			label2.Text = "10";
			// 
			// zTurnTrackBar
			// 
			zTurnTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			zTurnTrackBar.LargeChange = 10;
			zTurnTrackBar.Location = new Point(1359, 258);
			zTurnTrackBar.Margin = new Padding(4, 5, 4, 5);
			zTurnTrackBar.Maximum = 45;
			zTurnTrackBar.Minimum = -45;
			zTurnTrackBar.Name = "zTurnTrackBar";
			zTurnTrackBar.Size = new Size(230, 69);
			zTurnTrackBar.TabIndex = 5;
			zTurnTrackBar.TickFrequency = 5;
			zTurnTrackBar.Scroll += zTurnTrackBar_Scroll;
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new Point(1386, 228);
			label3.Margin = new Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new Size(155, 25);
			label3.TabIndex = 6;
			label3.Text = "Obrót wokół osi z";
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Location = new Point(1596, 258);
			label4.Margin = new Padding(4, 0, 4, 0);
			label4.Name = "label4";
			label4.Size = new Size(22, 25);
			label4.TabIndex = 7;
			label4.Text = "0";
			// 
			// xTurnTrackBar
			// 
			xTurnTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			xTurnTrackBar.Location = new Point(1357, 368);
			xTurnTrackBar.Margin = new Padding(4, 5, 4, 5);
			xTurnTrackBar.Maximum = 90;
			xTurnTrackBar.Minimum = -90;
			xTurnTrackBar.Name = "xTurnTrackBar";
			xTurnTrackBar.Size = new Size(231, 69);
			xTurnTrackBar.TabIndex = 8;
			xTurnTrackBar.TickFrequency = 5;
			xTurnTrackBar.Scroll += xTurnTrackBar_Scroll;
			// 
			// label5
			// 
			label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label5.AutoSize = true;
			label5.Location = new Point(1386, 338);
			label5.Margin = new Padding(4, 0, 4, 0);
			label5.Name = "label5";
			label5.Size = new Size(155, 25);
			label5.TabIndex = 9;
			label5.Text = "Obrót wokół osi x";
			// 
			// label6
			// 
			label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label6.AutoSize = true;
			label6.Location = new Point(1596, 368);
			label6.Margin = new Padding(4, 0, 4, 0);
			label6.Name = "label6";
			label6.Size = new Size(22, 25);
			label6.TabIndex = 10;
			label6.Text = "0";
			// 
			// kdTrackBar
			// 
			kdTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			kdTrackBar.Location = new Point(1359, 492);
			kdTrackBar.Margin = new Padding(4, 5, 4, 5);
			kdTrackBar.Maximum = 100;
			kdTrackBar.Name = "kdTrackBar";
			kdTrackBar.Size = new Size(230, 69);
			kdTrackBar.SmallChange = 5;
			kdTrackBar.TabIndex = 11;
			kdTrackBar.TickFrequency = 5;
			kdTrackBar.Value = 50;
			kdTrackBar.Scroll += kdTrackBar_Scroll;
			// 
			// label7
			// 
			label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label7.AutoSize = true;
			label7.Location = new Point(1374, 462);
			label7.Margin = new Padding(4, 0, 4, 0);
			label7.Name = "label7";
			label7.Size = new Size(218, 25);
			label7.TabIndex = 12;
			label7.Text = "Składowa rozproszona kd";
			// 
			// label8
			// 
			label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label8.AutoSize = true;
			label8.Location = new Point(1589, 492);
			label8.Margin = new Padding(4, 0, 4, 0);
			label8.Name = "label8";
			label8.Size = new Size(36, 25);
			label8.TabIndex = 13;
			label8.Text = "0.5";
			// 
			// ksTrackBar
			// 
			ksTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ksTrackBar.Location = new Point(1359, 602);
			ksTrackBar.Margin = new Padding(4, 5, 4, 5);
			ksTrackBar.Maximum = 100;
			ksTrackBar.Name = "ksTrackBar";
			ksTrackBar.Size = new Size(231, 69);
			ksTrackBar.SmallChange = 5;
			ksTrackBar.TabIndex = 14;
			ksTrackBar.TickFrequency = 5;
			ksTrackBar.Value = 50;
			ksTrackBar.Scroll += ksTrackBar_Scroll;
			// 
			// label9
			// 
			label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label9.AutoSize = true;
			label9.Location = new Point(1369, 572);
			label9.Margin = new Padding(4, 0, 4, 0);
			label9.Name = "label9";
			label9.Size = new Size(220, 25);
			label9.TabIndex = 15;
			label9.Text = "Składowa zwierciadlana ks";
			// 
			// label10
			// 
			label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label10.AutoSize = true;
			label10.Location = new Point(1589, 608);
			label10.Margin = new Padding(4, 0, 4, 0);
			label10.Name = "label10";
			label10.Size = new Size(36, 25);
			label10.TabIndex = 16;
			label10.Text = "0.5";
			// 
			// mTrackBar
			// 
			mTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			mTrackBar.Location = new Point(1374, 742);
			mTrackBar.Margin = new Padding(4, 5, 4, 5);
			mTrackBar.Maximum = 100;
			mTrackBar.Minimum = 1;
			mTrackBar.Name = "mTrackBar";
			mTrackBar.Size = new Size(206, 69);
			mTrackBar.TabIndex = 17;
			mTrackBar.TickFrequency = 5;
			mTrackBar.Value = 50;
			mTrackBar.Scroll += mTrackBar_Scroll;
			// 
			// label11
			// 
			label11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label11.AutoSize = true;
			label11.Location = new Point(1344, 712);
			label11.Margin = new Padding(4, 0, 4, 0);
			label11.Name = "label11";
			label11.Size = new Size(274, 25);
			label11.TabIndex = 18;
			label11.Text = "Współczynnik zwierciadlaności m";
			// 
			// label12
			// 
			label12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label12.AutoSize = true;
			label12.Location = new Point(1589, 752);
			label12.Margin = new Padding(4, 0, 4, 0);
			label12.Name = "label12";
			label12.Size = new Size(32, 25);
			label12.TabIndex = 19;
			label12.Text = "50";
			// 
			// lightColorButton
			// 
			lightColorButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lightColorButton.Location = new Point(1629, 50);
			lightColorButton.Margin = new Padding(4, 5, 4, 5);
			lightColorButton.Name = "lightColorButton";
			lightColorButton.Size = new Size(116, 80);
			lightColorButton.TabIndex = 20;
			lightColorButton.Text = "Kolor światła";
			lightColorButton.UseVisualStyleBackColor = true;
			lightColorButton.Click += lightColorButton_Click;
			// 
			// lightColorPanel
			// 
			lightColorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lightColorPanel.BackColor = Color.White;
			lightColorPanel.BorderStyle = BorderStyle.FixedSingle;
			lightColorPanel.Location = new Point(1761, 51);
			lightColorPanel.Margin = new Padding(4, 5, 4, 5);
			lightColorPanel.Name = "lightColorPanel";
			lightColorPanel.Size = new Size(101, 79);
			lightColorPanel.TabIndex = 21;
			// 
			// surfaceColorButton
			// 
			surfaceColorButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			surfaceColorButton.Location = new Point(1631, 393);
			surfaceColorButton.Margin = new Padding(4, 5, 4, 5);
			surfaceColorButton.Name = "surfaceColorButton";
			surfaceColorButton.Size = new Size(116, 85);
			surfaceColorButton.TabIndex = 22;
			surfaceColorButton.Text = "Kolor obiektu";
			surfaceColorButton.UseVisualStyleBackColor = true;
			surfaceColorButton.Click += surfaceColorButton_Click;
			// 
			// surfaceColorPanel
			// 
			surfaceColorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			surfaceColorPanel.BackColor = Color.Red;
			surfaceColorPanel.BorderStyle = BorderStyle.FixedSingle;
			surfaceColorPanel.Location = new Point(1763, 395);
			surfaceColorPanel.Margin = new Padding(4, 5, 4, 5);
			surfaceColorPanel.Name = "surfaceColorPanel";
			surfaceColorPanel.Size = new Size(101, 84);
			surfaceColorPanel.TabIndex = 23;
			// 
			// chooseImageButton
			// 
			chooseImageButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			chooseImageButton.Location = new Point(1644, 564);
			chooseImageButton.Margin = new Padding(4, 5, 4, 5);
			chooseImageButton.Name = "chooseImageButton";
			chooseImageButton.Size = new Size(116, 92);
			chooseImageButton.TabIndex = 24;
			chooseImageButton.Text = "Wybierz obraz";
			chooseImageButton.UseVisualStyleBackColor = true;
			chooseImageButton.Click += chooseImageButton_Click;
			// 
			// colorStructureRadioButton
			// 
			colorStructureRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			colorStructureRadioButton.AutoSize = true;
			colorStructureRadioButton.Location = new Point(1677, 710);
			colorStructureRadioButton.Margin = new Padding(4, 5, 4, 5);
			colorStructureRadioButton.Name = "colorStructureRadioButton";
			colorStructureRadioButton.Size = new Size(78, 29);
			colorStructureRadioButton.TabIndex = 25;
			colorStructureRadioButton.Text = "kolor";
			colorStructureRadioButton.UseVisualStyleBackColor = true;
			colorStructureRadioButton.CheckedChanged += colorStructureRadioButton_CheckedChanged;
			// 
			// label13
			// 
			label13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label13.AutoSize = true;
			label13.Location = new Point(1664, 676);
			label13.Margin = new Padding(4, 0, 4, 0);
			label13.Name = "label13";
			label13.Size = new Size(184, 25);
			label13.TabIndex = 26;
			label13.Text = "Wypełnienie struktury";
			// 
			// ImageRadioButton
			// 
			ImageRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ImageRadioButton.AutoSize = true;
			ImageRadioButton.Location = new Point(1678, 745);
			ImageRadioButton.Margin = new Padding(4, 5, 4, 5);
			ImageRadioButton.Name = "ImageRadioButton";
			ImageRadioButton.Size = new Size(181, 29);
			ImageRadioButton.TabIndex = 27;
			ImageRadioButton.Text = "załadowany obraz";
			ImageRadioButton.UseVisualStyleBackColor = true;
			ImageRadioButton.CheckedChanged += ImageRadioButton_CheckedChanged;
			// 
			// normalMapCheckBox
			// 
			normalMapCheckBox.AutoSize = true;
			normalMapCheckBox.Location = new Point(1662, 895);
			normalMapCheckBox.Margin = new Padding(4, 5, 4, 5);
			normalMapCheckBox.Name = "normalMapCheckBox";
			normalMapCheckBox.Size = new Size(171, 29);
			normalMapCheckBox.TabIndex = 28;
			normalMapCheckBox.Text = "Użyj NormalMap";
			normalMapCheckBox.UseVisualStyleBackColor = true;
			normalMapCheckBox.CheckedChanged += normalMapCheckBox_CheckedChanged;
			// 
			// loadNormalButton
			// 
			loadNormalButton.Location = new Point(1643, 785);
			loadNormalButton.Margin = new Padding(4, 5, 4, 5);
			loadNormalButton.Name = "loadNormalButton";
			loadNormalButton.Size = new Size(116, 95);
			loadNormalButton.TabIndex = 29;
			loadNormalButton.Text = "Dodaj NormalMap";
			loadNormalButton.UseVisualStyleBackColor = true;
			loadNormalButton.Click += loadNormalButton_Click;
			// 
			// drawTrianglesCheckBox
			// 
			drawTrianglesCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			drawTrianglesCheckBox.AutoSize = true;
			drawTrianglesCheckBox.Location = new Point(1654, 486);
			drawTrianglesCheckBox.Margin = new Padding(4, 5, 4, 5);
			drawTrianglesCheckBox.Name = "drawTrianglesCheckBox";
			drawTrianglesCheckBox.Size = new Size(210, 29);
			drawTrianglesCheckBox.TabIndex = 30;
			drawTrianglesCheckBox.Text = "Rysuj siatkę trójkątów";
			drawTrianglesCheckBox.UseVisualStyleBackColor = true;
			drawTrianglesCheckBox.CheckedChanged += drawTrianglesCheckBox_CheckedChanged;
			// 
			// fillTrianglesCheckBox
			// 
			fillTrianglesCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			fillTrianglesCheckBox.AutoSize = true;
			fillTrianglesCheckBox.Checked = true;
			fillTrianglesCheckBox.CheckState = CheckState.Checked;
			fillTrianglesCheckBox.Location = new Point(1654, 525);
			fillTrianglesCheckBox.Margin = new Padding(4, 5, 4, 5);
			fillTrianglesCheckBox.Name = "fillTrianglesCheckBox";
			fillTrianglesCheckBox.Size = new Size(181, 29);
			fillTrianglesCheckBox.TabIndex = 31;
			fillTrianglesCheckBox.Text = "Wypełniaj trójkąty";
			fillTrianglesCheckBox.UseVisualStyleBackColor = true;
			fillTrianglesCheckBox.CheckedChanged += fillTrianglesCheckBox_CheckedChanged;
			// 
			// lightMoveButton
			// 
			lightMoveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lightMoveButton.Location = new Point(1629, 316);
			lightMoveButton.Margin = new Padding(4, 5, 4, 5);
			lightMoveButton.Name = "lightMoveButton";
			lightMoveButton.Size = new Size(238, 67);
			lightMoveButton.TabIndex = 32;
			lightMoveButton.Text = "Zatrzymaj ruch światła";
			lightMoveButton.UseVisualStyleBackColor = true;
			lightMoveButton.Click += lightMoveButton_Click;
			// 
			// picturePanel
			// 
			picturePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			picturePanel.BorderStyle = BorderStyle.FixedSingle;
			picturePanel.Location = new Point(1772, 566);
			picturePanel.Margin = new Padding(4, 5, 4, 5);
			picturePanel.Name = "picturePanel";
			picturePanel.Size = new Size(105, 90);
			picturePanel.TabIndex = 33;
			// 
			// normalMapPanel
			// 
			normalMapPanel.Location = new Point(1771, 785);
			normalMapPanel.Margin = new Padding(4, 5, 4, 5);
			normalMapPanel.Name = "normalMapPanel";
			normalMapPanel.Size = new Size(106, 95);
			normalMapPanel.TabIndex = 34;
			// 
			// lightSourceTrackBar
			// 
			lightSourceTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lightSourceTrackBar.LargeChange = 50;
			lightSourceTrackBar.Location = new Point(1357, 855);
			lightSourceTrackBar.Maximum = 3000;
			lightSourceTrackBar.Minimum = -500;
			lightSourceTrackBar.Name = "lightSourceTrackBar";
			lightSourceTrackBar.Size = new Size(203, 69);
			lightSourceTrackBar.SmallChange = 10;
			lightSourceTrackBar.TabIndex = 35;
			lightSourceTrackBar.TickFrequency = 100;
			lightSourceTrackBar.Value = 500;
			lightSourceTrackBar.Scroll += lightSourceTrackBar_Scroll;
			// 
			// label14
			// 
			label14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label14.AutoSize = true;
			label14.Location = new Point(1386, 815);
			label14.Name = "label14";
			label14.Size = new Size(207, 25);
			label14.TabIndex = 36;
			label14.Text = "Wysokość źródła światła";
			// 
			// label15
			// 
			label15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label15.AutoSize = true;
			label15.Location = new Point(1579, 855);
			label15.Name = "label15";
			label15.Size = new Size(42, 25);
			label15.TabIndex = 37;
			label15.Text = "500";
			// 
			// pointLightButton
			// 
			pointLightButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			pointLightButton.AutoSize = true;
			pointLightButton.Checked = true;
			pointLightButton.Location = new Point(1631, 138);
			pointLightButton.Name = "pointLightButton";
			pointLightButton.Size = new Size(231, 29);
			pointLightButton.TabIndex = 39;
			pointLightButton.TabStop = true;
			pointLightButton.Text = "Punktowe źródło światła";
			pointLightButton.UseVisualStyleBackColor = true;
			pointLightButton.CheckedChanged += pointLightButton_CheckedChanged;
			// 
			// reflectorLightButton
			// 
			reflectorLightButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			reflectorLightButton.AutoSize = true;
			reflectorLightButton.Location = new Point(1631, 173);
			reflectorLightButton.Name = "reflectorLightButton";
			reflectorLightButton.Size = new Size(235, 29);
			reflectorLightButton.TabIndex = 40;
			reflectorLightButton.TabStop = true;
			reflectorLightButton.Text = "Reflektor - źródło światła";
			reflectorLightButton.UseVisualStyleBackColor = true;
			reflectorLightButton.CheckedChanged += reflectorLightButton_CheckedChanged;
			// 
			// reflectorAngleBar
			// 
			reflectorAngleBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			reflectorAngleBar.Location = new Point(1633, 248);
			reflectorAngleBar.Maximum = 100;
			reflectorAngleBar.Name = "reflectorAngleBar";
			reflectorAngleBar.Size = new Size(196, 69);
			reflectorAngleBar.TabIndex = 41;
			reflectorAngleBar.TickFrequency = 3;
			reflectorAngleBar.Value = 50;
			reflectorAngleBar.Scroll += reflectorAngleBar_Scroll;
			// 
			// label16
			// 
			label16.AutoSize = true;
			label16.Location = new Point(1835, 258);
			label16.Name = "label16";
			label16.Size = new Size(32, 25);
			label16.TabIndex = 42;
			label16.Text = "50";
			// 
			// label17
			// 
			label17.AutoSize = true;
			label17.Location = new Point(1664, 217);
			label17.Name = "label17";
			label17.Size = new Size(140, 25);
			label17.TabIndex = 43;
			label17.Text = "Potęga cosinusa";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1879, 944);
			Controls.Add(label17);
			Controls.Add(label16);
			Controls.Add(reflectorAngleBar);
			Controls.Add(reflectorLightButton);
			Controls.Add(pointLightButton);
			Controls.Add(label15);
			Controls.Add(label14);
			Controls.Add(lightSourceTrackBar);
			Controls.Add(normalMapPanel);
			Controls.Add(picturePanel);
			Controls.Add(lightMoveButton);
			Controls.Add(fillTrianglesCheckBox);
			Controls.Add(drawTrianglesCheckBox);
			Controls.Add(loadNormalButton);
			Controls.Add(normalMapCheckBox);
			Controls.Add(ImageRadioButton);
			Controls.Add(label13);
			Controls.Add(colorStructureRadioButton);
			Controls.Add(chooseImageButton);
			Controls.Add(surfaceColorPanel);
			Controls.Add(surfaceColorButton);
			Controls.Add(lightColorButton);
			Controls.Add(lightColorPanel);
			Controls.Add(label12);
			Controls.Add(label11);
			Controls.Add(mTrackBar);
			Controls.Add(label10);
			Controls.Add(label9);
			Controls.Add(ksTrackBar);
			Controls.Add(label8);
			Controls.Add(label7);
			Controls.Add(kdTrackBar);
			Controls.Add(label6);
			Controls.Add(label5);
			Controls.Add(xTurnTrackBar);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(zTurnTrackBar);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(accuracyTrackBar);
			Controls.Add(fileDialogButton);
			Controls.Add(pictureBox1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Margin = new Padding(4, 5, 4, 5);
			MaximizeBox = false;
			Name = "Form1";
			Text = "Powierzchnia Beziera";
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)accuracyTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)zTurnTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)xTurnTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)kdTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)ksTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)mTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)lightSourceTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)reflectorAngleBar).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictureBox pictureBox1;
        private Button fileDialogButton;
        private TrackBar accuracyTrackBar;
        private Label label1;
        private Label label2;
        private TrackBar zTurnTrackBar;
        private Label label3;
        private Label label4;
        private TrackBar xTurnTrackBar;
        private Label label5;
        private Label label6;
        private TrackBar kdTrackBar;
        private Label label7;
        private Label label8;
        private TrackBar ksTrackBar;
        private Label label9;
        private Label label10;
        private TrackBar mTrackBar;
        private Label label11;
        private Label label12;
        private Button lightColorButton;
        private Panel lightColorPanel;
        private Button surfaceColorButton;
        private Panel surfaceColorPanel;
        private Button chooseImageButton;
        private RadioButton colorStructureRadioButton;
        private Label label13;
        private RadioButton ImageRadioButton;
        private CheckBox normalMapCheckBox;
        private Button loadNormalButton;
        private CheckBox drawTrianglesCheckBox;
        private CheckBox fillTrianglesCheckBox;
        private Button lightMoveButton;
        private Panel picturePanel;
        private Panel normalMapPanel;
		private TrackBar lightSourceTrackBar;
		private Label label14;
		private Label label15;
		private RadioButton radioButton1;
		private RadioButton pointLightButton;
		private RadioButton reflectorLightButton;
		private TrackBar reflectorAngleBar;
		private Label label16;
		private Label label17;
	}
}
