using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Vision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.ML.Transforms.Image;

namespace ImageRecognitionApp
{
    public partial class TrainModels : Form
    {
        VideoCaptureDevice videoCapture;
        FilterInfoCollection filterInfo;
        private MLContext _mlContext;
        private ITransformer _model;
        private PredictionEngine<ImageData, ImagePrediction> _predictionEngine;
        private string _modelPath = "model.zip";

        public class ImageData
        {
            [ImageType(224, 224)]
            public byte[] ImageBytes { get; set; }
            public string Label { get; set; }
            public string ImagePath { get; set; }
        }

        public class ImagePrediction
        {
            [ColumnName("PredictedLabel")]
            public string PredictedLabel { get; set; }

            [ColumnName("Score")]
            public float[] Scores { get; set; }
        }

        public TrainModels()
        {
            InitializeComponent();
            CheckForSavedModel();
        }

        private void CheckForSavedModel()
        {
            if (File.Exists(_modelPath))
            {
                LoadModel();
                MessageBox.Show("Saved model loaded successfully!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveModel()
        {
            _mlContext.Model.Save(_model, null, _modelPath);
        }

        private void LoadModel()
        {
            _mlContext = new MLContext();
            _model = _mlContext.Model.Load(_modelPath, out _);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(_model);
        }

        private async void btnTrainModel_Click(object sender, EventArgs e)
        {
            try
            {
                btnTrainModel.Enabled = false;
                string trainingDataPath = SelectFolder();

                if (string.IsNullOrEmpty(trainingDataPath))
                {
                    MessageBox.Show("Please select a valid folder containing training images.");
                    return;
                }
                MessageBox.Show("Training started. Please wait...");
                await Task.Run(() => TrainModel(trainingDataPath));
                SaveModel();
                MessageBox.Show("Model trained and saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during training: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTrainModel.Enabled = true;
            }
        }

        private void TrainModel(string trainingDataPath)
        {
            _mlContext = new MLContext();
            var images = Directory.GetDirectories(trainingDataPath)
                .SelectMany(folder => Directory.GetFiles(folder))
                .Select(filePath => new ImageData
                {
                    ImagePath = filePath,
                    Label = Path.GetFileName(Path.GetDirectoryName(filePath))
                });

            var trainingData = _mlContext.Data.LoadFromEnumerable(images);

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label")
                  .Append(_mlContext.Transforms.LoadRawImageBytes(
                      outputColumnName: "ImageBytes",
                      imageFolder: trainingDataPath,
                      inputColumnName: "ImagePath"))
                  .Append(_mlContext.MulticlassClassification.Trainers.ImageClassification(
                      new ImageClassificationTrainer.Options
                      {
                          FeatureColumnName = "ImageBytes",
                          LabelColumnName = "Label",
                          ValidationSetFraction = 0.2f,
                          Arch = ImageClassificationTrainer.Architecture.ResnetV2101,
                          MetricsCallback = (metrics) => Console.WriteLine(metrics.ToString()),
                          ReuseTrainSetBottleneckCachedValues = false,
                          ReuseValidationSetBottleneckCachedValues = false
                      }))
                  .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            _model = pipeline.Fit(trainingData);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(_model);
        }

        private void btnClassifyImage_Click(object sender, EventArgs e)
        {
            if (_model == null)
            {
                MessageBox.Show("Please train the model first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (picCapture.Image == null)
            {
                MessageBox.Show("No image to classify. Please capture an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Bitmap capturedImage = new Bitmap(picCapture.Image);
            Bitmap resizedImage = new Bitmap(capturedImage, new Size(224, 224));
            string tempImagePath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");

            resizedImage.Save(tempImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
             
            var inputData = new List<ImageData>
            {
                new ImageData { ImagePath = tempImagePath }
            };

            var inputDataView = _mlContext.Data.LoadFromEnumerable(inputData);
            var predictions = _model.Transform(inputDataView);
            var predictedLabels = _mlContext.Data.CreateEnumerable<ImagePrediction>(predictions, reuseRowObject: false).ToList();

            if (predictedLabels.Count > 0)
            {
                var prediction = predictedLabels[0];
                if (prediction.Scores != null )
                {
                    var maxScoreIndex = Array.IndexOf(prediction.Scores, prediction.Scores.Max());
                    var confidence = prediction.Scores[maxScoreIndex] * 100;
                    if(confidence > 90)
                    {
                        lblResult.Text = $"Predicted Category: {prediction.PredictedLabel} ({confidence:F2}% confidence)";
                        Console.WriteLine($"Predicted Category: {prediction.PredictedLabel} ({confidence:F2}% confidence)");
                    }
                    else
                    {
                        lblResult.Text = "Not Found";
                    }
                    
                }
                else
                {
                    lblResult.Text = "Prediction failed. Scores are null.";
                }
            }
            else
            {
                lblResult.Text = "Prediction failed. No result.";
            }
        }

        private string SelectFolder()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderDialog.SelectedPath;
                }
                return null;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartCamera();
        }

        private void StartCamera()
        {
            try
            {
                filterInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                videoCapture = new VideoCaptureDevice(filterInfo[0].MonikerString);
                videoCapture.NewFrame += Camera_On;
                videoCapture.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting camera: {ex.Message}");
            }
        }

        private void Camera_On(object sender, NewFrameEventArgs eventArgs)
        {
            picStream.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (picStream.Image == null)
            {
                MessageBox.Show("No image to capture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Bitmap capturedImage = new Bitmap(picStream.Image);
            picCapture.Image = capturedImage;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            var form1 = new CaptureImages();
            form1.Show();
            this.Hide();

            if (videoCapture != null && videoCapture.IsRunning)
            {
                videoCapture.SignalToStop();
                videoCapture.WaitForStop();
                videoCapture = null;
            }
        }
    }
}
