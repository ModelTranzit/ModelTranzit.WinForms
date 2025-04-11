using Dizignit.Domain;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dizignit.Presentation
{
    class frmTile : Form
    {

        private Panel tilesPanel;

        private void InitializeComponent()
        {
            btnGenBMP = new Button();
            lblLatitude = new TextBox();
            lblLongitude = new TextBox();
            lblColor = new TextBox();
            lblSize = new TextBox();
            lblZoom = new TextBox();
            txtDebug = new TextBox();
            pctFullColor = new PictureBox();
            pctOneColorBitmap = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pctFullColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctOneColorBitmap).BeginInit();
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
            lblLatitude.Text = "Latitude";
            // 
            // lblLongitude
            // 
            lblLongitude.Location = new Point(145, 12);
            lblLongitude.Name = "lblLongitude";
            lblLongitude.Size = new Size(100, 23);
            lblLongitude.TabIndex = 2;
            lblLongitude.Text = "Longitude";
            // 
            // lblColor
            // 
            lblColor.Location = new Point(494, 12);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(100, 23);
            lblColor.TabIndex = 4;
            lblColor.Text = "Color";
            // 
            // lblSize
            // 
            lblSize.Location = new Point(366, 12);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(100, 23);
            lblSize.TabIndex = 5;
            lblSize.Text = "100";
            // 
            // lblZoom
            // 
            lblZoom.Location = new Point(251, 12);
            lblZoom.Name = "lblZoom";
            lblZoom.Size = new Size(100, 23);
            lblZoom.TabIndex = 6;
            lblZoom.Text = "15";
            // 
            // txtDebug
            // 
            txtDebug.Location = new Point(884, 11);
            txtDebug.Name = "txtDebug";
            txtDebug.Size = new Size(510, 23);
            txtDebug.TabIndex = 8;
            // 
            // pctFullColor
            // 
            pctFullColor.Location = new Point(25, 73);
            pctFullColor.Name = "pctFullColor";
            pctFullColor.Size = new Size(640, 640);
            pctFullColor.TabIndex = 9;
            pctFullColor.TabStop = false;
            // 
            // pctOneColorBitmap
            // 
            pctOneColorBitmap.Location = new Point(687, 73);
            pctOneColorBitmap.Name = "pctOneColorBitmap";
            pctOneColorBitmap.Size = new Size(640, 640);
            pctOneColorBitmap.TabIndex = 10;
            pctOneColorBitmap.TabStop = false;
            // 
            // frmTile
            // 
            ClientSize = new Size(1881, 853);
            Controls.Add(pctOneColorBitmap);
            Controls.Add(pctFullColor);
            Controls.Add(txtDebug);
            Controls.Add(lblZoom);
            Controls.Add(lblSize);
            Controls.Add(lblColor);
            Controls.Add(lblLongitude);
            Controls.Add(lblLatitude);
            Controls.Add(btnGenBMP);
            Name = "frmTile";
            ((System.ComponentModel.ISupportInitialize)pctFullColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctOneColorBitmap).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Button btnGenBMP;
        private TextBox lblLatitude;
        private TextBox lblLongitude;
        private TextBox lblColor;
        private TextBox lblZoom;
        private TextBox txtDebug;
        private PictureBox pctFullColor;
        private PictureBox pctOneColorBitmap;
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

            this.Controls.Add(tilesPanel);

            //_refresMaps();
        }

        public static bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    // File is not locked
                    fs.Close();
                }
                return false;
            }
            catch (IOException)
            {
                // File is locked
                return true;
            }
        }

        private void _displayAllBMP()
        {
            string[] tileFiles = Directory.GetFiles(@"bmp\", "*.bmp");
            int tileSize = 1000; // Size of each tile
            int padding = 10;   // Padding between tiles

            int x = padding, y = padding;

            foreach (string tileFile in tileFiles)
            {
                // Load the image
                Image tileImage = Image.FromFile(tileFile);

                // Create a PictureBox to display the tile
                PictureBox pictureBox = new PictureBox
                {
                    Image = tileImage,
                    Size = new Size(tileSize, tileSize),
                    Location = new Point(x, y),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                // Add the PictureBox to the panel
                tilesPanel.Controls.Add(pictureBox);

                // Update x and y for the next tile
                x += tileSize + padding;
                if (x + tileSize > tilesPanel.Width) // Move to the next row
                {
                    x = padding;
                    y += tileSize + padding;
                }
            }
        }

        private void _createBlackWhiteBitmap(Bitmap bitmap)
        {
            string railFilePath = @"C:\bmp\640x640rail.bmp";
            // this is going to be the new rail bitmap
            var railBits = new Bitmap(bitmap.Width, bitmap.Height);

            // Loop through every pixel in the image
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var color = bitmap.GetPixel(x, y);
                    var hexValue = ColorToHex(color).ToUpper();

                    // now we need to know if the color is one of the rail colors
                    // if it is, we need to change it to make the color black
                    // if not then the pixel should be white

                    var railColors = new List<string>
                    {
                        "#cdd1d5".ToUpper(),
                        "#d1d1d5".ToUpper(),
                        "#d8d8da".ToUpper()
                    };

                    // if rail color exists then draw it 
                    if (railColors.Contains(hexValue))
                    {
                        // Change the pixel color to black
                        railBits.SetPixel(x, y, Color.Black);
                    }

                    // Print the hex value
                    pctOneColorBitmap.Image = new Bitmap(railBits);
                    txtDebug.Text = $"Pixel at ({x}, {y}): {hexValue}";
                }
            }

            // now we are ready to save the new bitmap is a 2 bit bitmap
            //pctRailBmp.Image = railBits;
            //railBits.Save(filePath, ImageFormat.MemoryBmp);
            txtDebug.Text = $"Sucsess: Height: {railBits.Height} Width: {railBits.Width} ";
            railBits.Dispose();

        }

        private async void btnGenBMP_Click(object sender, EventArgs e)
        {
            // generate an image based on user input
            // check to see if image exists first
            // if it does, load it
            // if it doesn't, generate it
            // and save it to a file
            // This is a tool that is used to create map tiles for a given location
            // right now we need to find an ideeal size that we can use to create line data that represent rails on the map
            // using the Google Maps Static API
            // this will find and replace colors in the bitmap. 
            // we will neet to create a bitmap and loop through the pixels

            string apiKey = Environment.GetEnvironmentVariable("GoogleMapsAPIKey");

            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("GoogleMapsAPIKey variable not set.");

            string center = "30th+Street+Station,Philadelphia,PA"; // Location name
            string zoom = lblZoom.Text; // Zoom level (1 = World, 20 = Building)
            string size = lblSize.Text + "x" + lblSize.Text; // Tile size in pixels
            string mapType = "roadmap"; // Options: roadmap, satellite, hybrid, terrain


            string url = $"https://maps.googleapis.com/maps/api/staticmap?center={center}&zoom={zoom}&size={size}&maptype={mapType}&key={apiKey}";
            string filePath = "C:\\bmp\\640x640roadmap.bmp";

            // if the main full colored bitmap
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var image = await FetchImageAsync(url);

                    if (image != null)
                    {
                        try
                        {
                            using (var ms = new MemoryStream(image))
                            {
                                // thisis setting the UI picture box
                                pctFullColor.Image = Image.FromStream(ms);

                                // now we need to convert this to a black and white bitmap
                                _createBlackWhiteBitmap(new Bitmap(ms));
                            }
                        }
                        catch (Exception ex)
                        {
                            txtDebug.Text = ex.Message;
                        }
                        finally
                        {
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to fetch image.");
                    }

                }
                else
                {
                    throw new Exception($"Error fetching image: {response.StatusCode}");    
                }
            }


            // 


            // should fix this at some point to handle the file more like a draft vs starting over all the time. 



            // lets try and draw a moveable rectagle around a pictureBox




        }

        // Method to convert a Color object to a hex string
        static string ColorToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        static async Task<byte[]> FetchImageAsync(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    return await client.GetByteArrayAsync(uri);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching image: {ex.Message}");
                    return null;
                }
            }
        }

        private void btnGenerateLine_Click(object sender, EventArgs e)
        {
            //var filePath = "C:\\bmp\\640x640rail.bmp";
            //var railBits = new Bitmap(filePath);
            //var buff = new LineRenderer(filePath);
            //var line = buff.Render(railBits);
            //line.Save(filePath.Replace("rail", "rail-lines"));
            //pctLines.Image = Image.FromFile(filePath.Replace("rail", "rail-lines"));
        }
    }
}