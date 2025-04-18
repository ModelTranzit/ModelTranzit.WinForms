using Dizignit.Core;
using Dizignit.Core.Enums;
using Dizignit.Core.Helpers;
using Dizignit.DAL;
using Dizignit.DAL.Logging;
using Dizignit.Domain;

namespace Dizignit.Presentation
{
    class frmTile : Form
    {
        private RequestLog _requestLog;
        private Button btnSaveBitmap;
        private PictureBox Tile1;
        private PictureBox Tile2;
        private Label lblNewLatitude;
        private Label lblNewLongitute;
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
            Tile1 = new PictureBox();
            Tile2 = new PictureBox();
            lblNewLatitude = new Label();
            lblNewLongitute = new Label();
            ((System.ComponentModel.ISupportInitialize)Tile1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Tile2).BeginInit();
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
            // Tile1
            // 
            Tile1.BorderStyle = BorderStyle.FixedSingle;
            Tile1.Location = new Point(333, 715);
            Tile1.Name = "Tile1";
            Tile1.Size = new Size(640, 640);
            Tile1.TabIndex = 13;
            Tile1.TabStop = false;
            // 
            // Tile2
            // 
            Tile2.BorderStyle = BorderStyle.FixedSingle;
            Tile2.Location = new Point(333, 69);
            Tile2.Name = "Tile2";
            Tile2.Size = new Size(640, 640);
            Tile2.TabIndex = 14;
            Tile2.TabStop = false;
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
            // frmTile
            // 
            ClientSize = new Size(1881, 1016);
            Controls.Add(lblNewLongitute);
            Controls.Add(lblNewLatitude);
            Controls.Add(Tile2);
            Controls.Add(Tile1);
            Controls.Add(btnSaveBitmap);
            Controls.Add(txtDebug);
            Controls.Add(lblZoom);
            Controls.Add(lblSize);
            Controls.Add(lblPixelsPerDegree);
            Controls.Add(lblLongitude);
            Controls.Add(lblLatitude);
            Controls.Add(btnGenBMP);
            Name = "frmTile";
            ((System.ComponentModel.ISupportInitialize)Tile1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Tile2).EndInit();
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

            // Set up the panel
            tilesPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true // Enable scrolling for overflow
            };

            this.Controls.Add(tilesPanel);;
        }

        private async void btnGenBMP_Click(object sender, EventArgs e)
        {
            _requestLog = new RequestLog();
            _requestLog.Log();

            var httpApiRequest = new HttpApiRequest(
                new MapCordinate(decimal.Parse(lblLatitude.Text), decimal.Parse(lblLongitude.Text)), 
                int.Parse(lblSize.Text), 
                int.Parse(lblZoom.Text));

            // create a tile based the form 
            var tile = new MapTile(
                await httpApiRequest.GetImageAsync(),
                new MapCordinate(decimal.Parse(lblLatitude.Text), decimal.Parse(lblLongitude.Text)),
                int.Parse(lblZoom.Text),
                int.Parse(lblSize.Text),
                ETileType.FullColor);

            var tileRenderer = new TileRenderer(_requestLog.RequestID);
            var blackWhiteTile = tileRenderer.ConverTileToBlack(tile.Pixels, new MapBitDepth(int.Parse(lblSize.Text), 15, 50));

            Tile1.Image = BitmapHelper.ByteArrayToBitmap(blackWhiteTile);


            // lets caluclate the next tile 
            var calcTile = TileCalculator.CalculateAdjacentTileCenter(
                new MapCordinate(decimal.Parse(lblLatitude.Text), decimal.Parse(lblLongitude.Text)), 
                int.Parse(lblSize.Text),
                decimal.Parse(lblPixelsPerDegree.Text),
                ETileDirection.North);

            httpApiRequest = new HttpApiRequest(
                calcTile, 
                int.Parse(lblSize.Text), 
                int.Parse(lblZoom.Text));

            var nexTile = new MapTile(
                await httpApiRequest.GetImageAsync(),
                new MapCordinate(calcTile.Latitude, calcTile.Longitude),
                int.Parse(lblZoom.Text),
                int.Parse(lblSize.Text),
                ETileType.Black);

            lblNewLatitude.Text = nexTile.Cordinate.Latitude.ToString();
            lblNewLongitute.Text = nexTile.Cordinate.Longitude.ToString();  

            tileRenderer = new TileRenderer(_requestLog.RequestID);
            var nextBytes = tileRenderer.ConverTileToBlack(nexTile.Pixels, new MapBitDepth(int.Parse(lblSize.Text), 15, 50));

            Tile2.Image = BitmapHelper.ByteArrayToBitmap(nextBytes);
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