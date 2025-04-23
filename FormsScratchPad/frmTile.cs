using Dizignit.Core;
using Dizignit.Core.Enums;
using Dizignit.Core.Helpers;
using Dizignit.DAL;
using Dizignit.DAL.Logging;
using Dizignit.Domain;
using System.Text;
using System.Windows.Forms;

namespace Dizignit.Presentation
{
    class frmTile : Form
    {
        private RequestLog _requestLog;
        private Button btnSaveBitmap;
        private Label lblNewLatitude;
        private Label lblNewLongitute;
        private FlowLayoutPanel imgCanvas;
        private Panel tilesPanel;

        private void InitializeComponent()
        {
            btnGenBMP = new Button();
            lblLatitude = new TextBox();
            lblLongitude = new TextBox();
            lblPixelsPerDegree = new TextBox();
            lblSize = new TextBox();
            lblZoom = new TextBox();
            txtDebug = new TextBox();
            btnSaveBitmap = new Button();
            lblNewLatitude = new Label();
            lblNewLongitute = new Label();
            imgCanvas = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // btnGenBMP
            // 
            btnGenBMP.Location = new Point(617, 11);
            btnGenBMP.Name = "btnGenBMP";
            btnGenBMP.Size = new Size(75, 23);
            btnGenBMP.TabIndex = 0;
            btnGenBMP.Text = "Gen BMP's";
            btnGenBMP.UseVisualStyleBackColor = true;
            btnGenBMP.Click += btnGenBMP_Click;
            // 
            // lblLatitude
            // 
            lblLatitude.AccessibleName = "";
            lblLatitude.Location = new Point(25, 12);
            lblLatitude.Name = "lblLatitude";
            lblLatitude.Size = new Size(100, 23);
            lblLatitude.TabIndex = 1;
            lblLatitude.Text = "39.9580";
            // 
            // lblLongitude
            // 
            lblLongitude.Location = new Point(145, 12);
            lblLongitude.Name = "lblLongitude";
            lblLongitude.Size = new Size(100, 23);
            lblLongitude.TabIndex = 2;
            lblLongitude.Text = "-75.1816";
            // 
            // lblPixelsPerDegree
            // 
            lblPixelsPerDegree.Location = new Point(494, 12);
            lblPixelsPerDegree.Name = "lblPixelsPerDegree";
            lblPixelsPerDegree.Size = new Size(100, 23);
            lblPixelsPerDegree.TabIndex = 4;
            lblPixelsPerDegree.Text = "500000";
            // 
            // lblSize
            // 
            lblSize.Location = new Point(366, 12);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(100, 23);
            lblSize.TabIndex = 5;
            lblSize.Text = "640";
            // 
            // lblZoom
            // 
            lblZoom.Location = new Point(251, 12);
            lblZoom.Name = "lblZoom";
            lblZoom.Size = new Size(100, 23);
            lblZoom.TabIndex = 6;
            lblZoom.Text = "19";
            // 
            // txtDebug
            // 
            txtDebug.Location = new Point(884, 11);
            txtDebug.Name = "txtDebug";
            txtDebug.Size = new Size(510, 23);
            txtDebug.TabIndex = 8;
            // 
            // btnSaveBitmap
            // 
            btnSaveBitmap.Location = new Point(717, 10);
            btnSaveBitmap.Name = "btnSaveBitmap";
            btnSaveBitmap.Size = new Size(75, 23);
            btnSaveBitmap.TabIndex = 12;
            btnSaveBitmap.Text = "Save BMP";
            btnSaveBitmap.UseVisualStyleBackColor = true;
            btnSaveBitmap.Click += btnSaveBitmap_Click;
            // 
            // lblNewLatitude
            // 
            lblNewLatitude.AutoSize = true;
            lblNewLatitude.Location = new Point(25, 50);
            lblNewLatitude.Name = "lblNewLatitude";
            lblNewLatitude.Size = new Size(38, 15);
            lblNewLatitude.TabIndex = 15;
            lblNewLatitude.Text = "label1";
            // 
            // lblNewLongitute
            // 
            lblNewLongitute.AutoSize = true;
            lblNewLongitute.Location = new Point(145, 50);
            lblNewLongitute.Name = "lblNewLongitute";
            lblNewLongitute.Size = new Size(38, 15);
            lblNewLongitute.TabIndex = 16;
            lblNewLongitute.Text = "label2";
            // 
            // imgCanvas
            // 
            imgCanvas.AutoScroll = true;
            imgCanvas.Location = new Point(55, 119);
            imgCanvas.Name = "imgCanvas";
            imgCanvas.Size = new Size(30000, 30000);
            imgCanvas.TabIndex = 17;
            // 
            // frmTile
            // 
            ClientSize = new Size(1881, 1016);
            Controls.Add(imgCanvas);
            Controls.Add(lblNewLongitute);
            Controls.Add(lblNewLatitude);
            Controls.Add(btnSaveBitmap);
            Controls.Add(txtDebug);
            Controls.Add(lblZoom);
            Controls.Add(lblSize);
            Controls.Add(lblPixelsPerDegree);
            Controls.Add(lblLongitude);
            Controls.Add(lblLatitude);
            Controls.Add(btnGenBMP);
            Name = "frmTile";
            ResumeLayout(false);
            PerformLayout();
        }
        private Button btnGenBMP;
        private TextBox lblLatitude;
        private TextBox lblLongitude;
        private TextBox lblPixelsPerDegree;
        private TextBox lblZoom;
        private TextBox txtDebug;
        private TextBox lblSize;

        public frmTile()
        {
            // Set up the form
            this.Text = "BMP Tiles Display";
            this.Size = new Size(1920, 1200);
            InitializeComponent();
        }

        private async void btnGenBMP_Click(object sender, EventArgs e)
        {
            _requestLog = new RequestLog();
            _requestLog.Log();

            // Get the coordinates from the text boxes
            double latitude = double.Parse(lblLatitude.Text);
            double longitude = double.Parse(lblLongitude.Text);

            // Get the zoom level and tile size from the text boxes
            int zoomLevel = Constants.Zoom;

            int tileSize = Constants.TileSize;

            // Get the pixels per degree from the text box
            double pixelsPerDegree = Constants.PixelsPerDegree;

            // user map selection 
            var upperLeftCordinate = new MapCordinate(39.9725515, -75.1983052);
            var lowerRightCordinate = new MapCordinate(39.9556774, -75.1801474);


            // Create a new MapCoordinate object
            var centerCoordinate = TileCalculator.CalculateCenter(upperLeftCordinate, lowerRightCordinate);

            // Calculate the tile center
            var tileCenter = TileCalculator.CalculateTileCenter(centerCoordinate, tileSize, pixelsPerDegree);

            // Calculate the adjacent tile centers
            var northTileCenter = TileCalculator.CalculateAdjacentTileCenter(tileCenter, tileSize, pixelsPerDegree, ETileDirection.North);
            var southTileCenter = TileCalculator.CalculateAdjacentTileCenter(tileCenter, tileSize, pixelsPerDegree, ETileDirection.South);
            var eastTileCenter = TileCalculator.CalculateAdjacentTileCenter(tileCenter, tileSize, pixelsPerDegree, ETileDirection.East);
            var westTileCenter = TileCalculator.CalculateAdjacentTileCenter(tileCenter, tileSize, pixelsPerDegree, ETileDirection.West);
;
            // Generate the tiles based on uaer selected cordinantes
            var tiles = await TileGenerator.GenerateTiles(upperLeftCordinate, lowerRightCordinate, _requestLog.RequestID);


            // get all files from drive 
            var images = Directory.GetFiles("C:\\images");

            var folderPath = "C:\\images"; // Path to the folder containing BMP files
            imgCanvas.Controls.Clear(); // Clear previous images
            imgCanvas.AutoScroll = true;


            var bmpFiles = Directory.GetFiles(folderPath, "*.bmp")
                     .Select(file => new FileInfo(file))
                     .OrderBy(fileInfo => fileInfo.CreationTime);


            AddImagesToPanel(LoadImages());
        }




        private List<Bitmap> LoadImages()
        {


            var retVal = new List<Bitmap>();
            var folderPath = "C:\\images"; // Path to the folder containing BMP files
            var bmpFiles = Directory.GetFiles(folderPath, "*.bmp")
                     .Select(file => new FileInfo(file))
                     .OrderBy(fileInfo => fileInfo.CreationTime);

            foreach (var file in bmpFiles)
            {
                retVal.Add(new Bitmap(file.FullName));
            }

            return retVal;
        }

        private void AddImagesToPanel(List<Bitmap> bmpFiles)
        {
            int x = 10; // Starting X position
            int y = 10; // Starting Y position
            int padding = 10; // Space between images
            int imagesPerRow = 15; // Number of images per row
            int currentImage = 0;

            foreach (Bitmap bmp in bmpFiles)
            {
                PictureBox picBox = new PictureBox();
                picBox.Size = new Size(Constants.TileSize, Constants.TileSize);
                picBox.Image = bmp;
                picBox.SizeMode = PictureBoxSizeMode.Zoom;
                picBox.Location = new Point(x, y);

                imgCanvas.Controls.Add(picBox);

                x += picBox.Width + padding;
                currentImage++;

                // Move to the next row after reaching the specified number of images per row
                if (currentImage % imagesPerRow == 0)
                {
                    x = 10; // Reset X position
                    y += picBox.Height + padding; // Move Y position down
                }
            }

            // Adjust AutoScrollMinSize to accommodate all images
            imgCanvas.AutoScrollMinSize = new Size(imgCanvas.Width, y + 100);
        }

        public void LogImage(byte[] message, ETileType imageType)
        {
            var imageLog = new ImageLog(message, imageType, _requestLog.RequestID);
            imageLog.Log();
        }

        private void btnSaveBitmap_Click(object sender, EventArgs e)
        {
            //if (pctOneColorBitmap.Image != null)
            //{
            //        pctOneColorBitmap.Image.Save($"C:/bmp/{Guid.NewGuid().ToString()}.bmp", System.Drawing.Imaging.ImageFormat.Jpeg);
            //        MessageBox.Show("Image saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show("No image to save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }
    }
}