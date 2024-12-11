namespace ImageRecognitionApp
{
    partial class TrainModels
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTrainModel = new Button();
            btnClassifyImage = new Button();
            picCapture = new PictureBox();
            lblResult = new Label();
            picStream = new PictureBox();
            btnStart = new Button();
            btnCapture = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)picCapture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picStream).BeginInit();
            SuspendLayout();
            // 
            // btnTrainModel
            // 
            btnTrainModel.Location = new Point(36, 122);
            btnTrainModel.Name = "btnTrainModel";
            btnTrainModel.Size = new Size(121, 23);
            btnTrainModel.TabIndex = 0;
            btnTrainModel.Text = "Train Model";
            btnTrainModel.UseVisualStyleBackColor = true;
            btnTrainModel.Click += btnTrainModel_Click;
            // 
            // btnClassifyImage
            // 
            btnClassifyImage.Location = new Point(604, 122);
            btnClassifyImage.Name = "btnClassifyImage";
            btnClassifyImage.Size = new Size(115, 23);
            btnClassifyImage.TabIndex = 1;
            btnClassifyImage.Text = "Classify Image";
            btnClassifyImage.UseVisualStyleBackColor = true;
            btnClassifyImage.Click += btnClassifyImage_Click;
            // 
            // picCapture
            // 
            picCapture.Location = new Point(376, 27);
            picCapture.Name = "picCapture";
            picCapture.Size = new Size(161, 169);
            picCapture.SizeMode = PictureBoxSizeMode.StretchImage;
            picCapture.TabIndex = 2;
            picCapture.TabStop = false;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.BackColor = Color.YellowGreen;
            lblResult.ForeColor = SystemColors.ButtonHighlight;
            lblResult.Location = new Point(564, 181);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(0, 15);
            lblResult.TabIndex = 3;
            // 
            // picStream
            // 
            picStream.Location = new Point(196, 27);
            picStream.Name = "picStream";
            picStream.Size = new Size(154, 169);
            picStream.SizeMode = PictureBoxSizeMode.StretchImage;
            picStream.TabIndex = 4;
            picStream.TabStop = false;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(224, 214);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 5;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnCapture
            // 
            btnCapture.Location = new Point(408, 211);
            btnCapture.Name = "btnCapture";
            btnCapture.Size = new Size(75, 23);
            btnCapture.TabIndex = 6;
            btnCapture.Text = "Capture";
            btnCapture.UseVisualStyleBackColor = true;
            btnCapture.Click += btnCapture_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(615, 27);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 7;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // TrainModels
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(767, 309);
            Controls.Add(btnBack);
            Controls.Add(btnCapture);
            Controls.Add(btnStart);
            Controls.Add(picStream);
            Controls.Add(lblResult);
            Controls.Add(picCapture);
            Controls.Add(btnClassifyImage);
            Controls.Add(btnTrainModel);
            Name = "TrainModels";
            Text = "TrainModel";
            ((System.ComponentModel.ISupportInitialize)picCapture).EndInit();
            ((System.ComponentModel.ISupportInitialize)picStream).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTrainModel;
        private Button btnClassifyImage;
        private PictureBox picCapture;
        private Label lblResult;
        private PictureBox picStream;
        private Button btnStart;
        private Button btnCapture;
        private Button btnBack;
    }
}