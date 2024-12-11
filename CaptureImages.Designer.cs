namespace ImageRecognitionApp
{
    partial class CaptureImages
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
            picStream = new PictureBox();
            picCapture = new PictureBox();
            btnStart = new Button();
            btnCapture = new Button();
            btnSave = new Button();
            btnTrainModel = new Button();
            cmbModel = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)picStream).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCapture).BeginInit();
            SuspendLayout();
            // 
            // picStream
            // 
            picStream.Location = new Point(111, 112);
            picStream.Name = "picStream";
            picStream.Size = new Size(117, 171);
            picStream.SizeMode = PictureBoxSizeMode.StretchImage;
            picStream.TabIndex = 0;
            picStream.TabStop = false;
            // 
            // picCapture
            // 
            picCapture.Location = new Point(272, 112);
            picCapture.Name = "picCapture";
            picCapture.Size = new Size(117, 171);
            picCapture.SizeMode = PictureBoxSizeMode.StretchImage;
            picCapture.TabIndex = 1;
            picCapture.TabStop = false;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(74, 320);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnCapture
            // 
            btnCapture.Location = new Point(155, 320);
            btnCapture.Name = "btnCapture";
            btnCapture.Size = new Size(75, 23);
            btnCapture.TabIndex = 3;
            btnCapture.Text = "Capture";
            btnCapture.UseVisualStyleBackColor = true;
            btnCapture.Click += btnCapture_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(255, 320);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnTrainModel
            // 
            btnTrainModel.Location = new Point(336, 320);
            btnTrainModel.Name = "btnTrainModel";
            btnTrainModel.Size = new Size(82, 23);
            btnTrainModel.TabIndex = 5;
            btnTrainModel.Text = "Train Model";
            btnTrainModel.UseVisualStyleBackColor = true;
            btnTrainModel.Click += btnTrainModel_Click;
            // 
            // cmbModel
            // 
            cmbModel.FormattingEnabled = true;
            cmbModel.Items.AddRange(new object[] { "Model A", "Model B", "Model C" });
            cmbModel.Location = new Point(107, 31);
            cmbModel.Name = "cmbModel";
            cmbModel.Size = new Size(121, 23);
            cmbModel.TabIndex = 6;
            cmbModel.SelectedIndexChanged += cmbModel_SelectedIndexChanged;
            // 
            // CaptureImages
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(494, 399);
            Controls.Add(cmbModel);
            Controls.Add(btnTrainModel);
            Controls.Add(btnSave);
            Controls.Add(btnCapture);
            Controls.Add(btnStart);
            Controls.Add(picCapture);
            Controls.Add(picStream);
            Name = "CaptureImages";
            Text = "Capture Image for Training Data";
            ((System.ComponentModel.ISupportInitialize)picStream).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCapture).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picStream;
        private PictureBox picCapture;
        private Button btnStart;
        private Button btnCapture;
        private Button btnSave;
        private Button btnTrainModel;
        private ComboBox cmbModel;
    }
}
