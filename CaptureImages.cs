using AForge.Video.DirectShow;
using AForge.Video;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ImageRecognitionApp
{
    public partial class CaptureImages : Form
    {
        VideoCaptureDevice videoCapture;
        FilterInfoCollection filterInfo;
        public CaptureImages()
        {
            InitializeComponent();
            //bindDDL();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            startCamera();
        }
        void startCamera()
        {
            if (cmbModel.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the Model first", "Error", MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    filterInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                    videoCapture = new VideoCaptureDevice(filterInfo[0].MonikerString);
                    videoCapture.NewFrame += new NewFrameEventHandler(Camera_On);
                    videoCapture.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error starting camera: {ex.Message}");
                }
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (picCapture.Image == null)
            {
                MessageBox.Show("No captured image to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string folderPath = @"D:\Ankit_Singh\TrainingData";
            string finalPath = Path.Combine(folderPath, path2: cmbModel.SelectedItem.ToString());
            if (!Directory.Exists(finalPath))
                Directory.CreateDirectory(finalPath);
            string fileName = Path.Combine(finalPath, $"{cmbModel.SelectedItem}_{DateTime.Now:yyyyMMddHHmmss}.jpg");
            picCapture.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            MessageBox.Show($"Image saved successfully at: {fileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnTrainModel_Click(object sender, EventArgs e)
        {
            TrainModels model = new TrainModels();
            model.Show();
            this.Hide();
            if (videoCapture != null && videoCapture.IsRunning)
            {
                videoCapture.SignalToStop();
                videoCapture.WaitForStop();
                videoCapture = null;
            }
        }
        private void bindDDL()
        {
            try
            {
                string query = "SELECT ModelId, Model FROM Models";
                string connectionString = "Data Source=DESKTOP-LNVC5VP\\SQLEXPRESS;Initial Catalog=ImageRecognition;Integrated Security=True;Trust Server Certificate=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cmbModel.DataSource = dataTable;
                    cmbModel.DisplayMember = "Model"; 
                    cmbModel.ValueMember = "ModelId";
                    cmbModel.SelectedIndex = -1;     
                    conn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
