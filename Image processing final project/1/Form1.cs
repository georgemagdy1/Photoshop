using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using openCV;
using System.Drawing.Imaging;
using System.Threading;


namespace _1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IplImage image1;
        IplImage image;
        Bitmap bitImage;
        private Bitmap inputImage;

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = " ";
            openFileDialog1.Filter = "JPEG|*JPG|Bitmap|*.bmp|All|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
                   CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);
                   IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
                   cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);
                pictureBox1.BackgroundImage = (Image)resized_image;

                  // pictureBox1.BackgroundImage = (Image)image1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image  = cvlib.CvCreateImage(new CvSize(image1.width,image1.height), image1.depth, image1.nChannels);
            int srcAdd = image1.imageData.ToInt32();
            int dstAdd = image.imageData.ToInt32();
            unsafe
            {
                int srcIndex ,  dstIndex ;
                for(int r= 0; r< image.height; r++) 
                    for(int c= 0; c< image.width; c++)
                    {
                        srcIndex = dstIndex = (image.width * r * image.nChannels) + (c * image.nChannels);
                        *(byte*)(dstAdd + dstIndex + 0) = 0;
                        *(byte*)(dstAdd + dstIndex + 1) = 0;
                        *(byte*)(dstAdd + dstIndex + 2) = *(byte*)(srcAdd + srcIndex + 2);

                    }

            }
            CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image.depth, image.nChannels);
            cvlib.CvResize(ref image, ref resized_image, cvlib.CV_INTER_LINEAR);
            pictureBox2.BackgroundImage = (Image)resized_image;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void convertToToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image = cvlib.CvCreateImage(new CvSize(image1.width, image1.height), image1.depth, image1.nChannels);
            int srcAdd = image1.imageData.ToInt32();
            int dstAdd = image.imageData.ToInt32();
            unsafe
            {
                int srcIndex, dstIndex;
                for (int r = 0; r < image.height; r++)
                    for (int c = 0; c < image.width; c++)
                    {
                        srcIndex = dstIndex = (image.width * r * image.nChannels) + (c * image.nChannels);
                        *(byte*)(dstAdd + dstIndex + 0) = *(byte*)(srcAdd + srcIndex + 0);
                        *(byte*)(dstAdd + dstIndex + 1) = 0;
                        *(byte*)(dstAdd + dstIndex + 2) = 0;

                    }

            }
            CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image.depth, image.nChannels);
            cvlib.CvResize(ref image, ref resized_image, cvlib.CV_INTER_LINEAR);
            pictureBox2.BackgroundImage = (Image)resized_image;

        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image = cvlib.CvCreateImage(new CvSize(image1.width, image1.height), image1.depth, image1.nChannels);
            int srcAdd = image1.imageData.ToInt32();
            int dstAdd = image.imageData.ToInt32();
            unsafe
            {
                int srcIndex, dstIndex;
                for (int r = 0; r < image.height; r++)
                    for (int c = 0; c < image.width; c++)
                    {
                        srcIndex = dstIndex = (image.width * r * image.nChannels) + (c * image.nChannels);
                        *(byte*)(dstAdd + dstIndex + 0) = 0;
                        *(byte*)(dstAdd + dstIndex + 1) = *(byte*)(srcAdd + srcIndex + 1);
                        *(byte*)(dstAdd + dstIndex + 2) = 0;

                    }

            }
            CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image.depth, image.nChannels);
            cvlib.CvResize(ref image, ref resized_image, cvlib.CV_INTER_LINEAR);
            pictureBox2.BackgroundImage = (Image)resized_image;

        }

        private void greyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
            cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);


            //convert the image type from IplImage to Bitmap
            bitImage = (Bitmap)resized_image;
            int width = bitImage.Width;
            int height = bitImage.Height;
            Color p;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    p = bitImage.GetPixel(x, y);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;
                    int avg = (r + g + b) / 3;
                    bitImage.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));

                    pictureBox2.Image = (Image)bitImage;


                }

            }

        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.Series["Red"].Points.Clear();
            chart1.Series["Green"].Points.Clear();
            chart1.Series["Blue"].Points.Clear();
            Bitmap bmpImg = (Bitmap)image1;
            int width = bmpImg.Width;
            int hieght = bmpImg.Height;
            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hieght; j++)
                {
                    Color pixelColor = bmpImg.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;

                }
            }
            for (int i = 0; i < 256; i++)
            {
                chart1.Series["Red"].Points.AddY(ni_Red[i]);
                chart1.Series["Green"].Points.AddY(ni_Green[i]);
                chart1.Series["Blue"].Points.AddY(ni_Blue[i]);
            }
        }

        private void equalizedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
            cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);

            Bitmap bmpImg = (Bitmap)resized_image;
            Bitmap newImage = bmpImg;
            int width = bmpImg.Width;
            int hieght = bmpImg.Height;
            //******************* Calculate N(i) **************//

            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hieght; j++)
                {
                    Color pixelColor = bmpImg.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;
                }
            }
            //******************* Calculate P(Ni) **************//
            decimal[] prob_ni_Red = new decimal[256];
            decimal[] prob_ni_Green = new decimal[256];
            decimal[] prob_ni_Blue = new decimal[256];

            for (int i = 0; i < 256; i++)
            {
                prob_ni_Red[i] = (decimal)ni_Red[i] / (decimal)(width * hieght);
                prob_ni_Green[i] = (decimal)ni_Green[i] / (decimal)(width * hieght);
                prob_ni_Blue[i] = (decimal)ni_Blue[i] / (decimal)(width * hieght);
            }
            //******************* Calculate CDF **************//

            decimal[] cdf_Red = new decimal[256];
            decimal[] cdf_Green = new decimal[256];
            decimal[] cdf_Blue = new decimal[256];

            cdf_Red[0] = prob_ni_Red[0];
            cdf_Green[0] = prob_ni_Green[0];
            cdf_Blue[0] = prob_ni_Blue[0];

            for (int i = 1; i < 256; i++)
            {
                cdf_Red[i] = prob_ni_Red[i] + cdf_Red[i - 1];
                cdf_Green[i] = prob_ni_Green[i] + cdf_Green[i - 1];
                cdf_Blue[i] = prob_ni_Blue[i] + cdf_Blue[i - 1];
            }

            //******************* Calculate CDF(L-1) **************//


            int red, green, blue;
            int constant = 255;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hieght; j++)
                {
                    Color pixelColor = bmpImg.GetPixel(i, j);

                    red = (int)Math.Round(cdf_Red[pixelColor.R] * constant);
                    green = (int)Math.Round(cdf_Red[pixelColor.G] * constant);
                    blue = (int)Math.Round(cdf_Red[pixelColor.B] * constant);

                    Color newColor = Color.FromArgb(red, green, blue);
                    newImage.SetPixel(i, j, newColor);

                }
            }

            pictureBox2.Image = (Image)newImage;
        }

        private void equalizedHistogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart2.Series["Red"].Points.Clear();
            chart2.Series["Green"].Points.Clear();
            chart2.Series["Blue"].Points.Clear();
            Bitmap bmpImg = (Bitmap)pictureBox2.Image;
            int width = bmpImg.Width;
            int hieght = bmpImg.Height;
            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hieght; j++)
                {
                    Color pixelColor = bmpImg.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;

                }
            }
            for (int i = 0; i < 256; i++)
            {
                chart2.Series["Red"].Points.AddY(ni_Red[i]);
                chart2.Series["Green"].Points.AddY(ni_Green[i]);
                chart2.Series["Blue"].Points.AddY(ni_Blue[i]);
            }
        
        
        }
        private void ApplyNeighborhoodFilter(Func<Bitmap, int, int, Color> filterFunction)

        {
            image1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
            CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
            cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);
            inputImage = (Bitmap)resized_image;
            // Check if the inputImage variable is not null
            if (inputImage != null)
            {
                // Create a new bitmap for the filtered image
                Bitmap resultImage = new Bitmap(inputImage.Width, inputImage.Height);

                // Apply the filter to each pixel in the image
                for (int x = 0; x < inputImage.Width; x++)
                {
                    for (int y = 0; y < inputImage.Height; y++)
                    {
                        resultImage.SetPixel(x, y, filterFunction(inputImage, x, y));
                    }
                }

                // Display the filtered image
                DisplayImage(resultImage, $"{filterFunction.Method.Name} Filter");
            }
            else
            {
                MessageBox.Show("No image loaded. Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DisplayImage(Bitmap image, string windowName)
        {
            // Display the image in the PictureBox
            pictureBox2.Image = image;

            // Show the window name as a title
            this.Text = windowName;
        }
        private Color MinFilter(Bitmap image, int x, int y)
        {
            int minR = 255, minG = 255, minB = 255;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newX = Math.Max(0, Math.Min(x + i, image.Width - 1));
                    int newY = Math.Max(0, Math.Min(y + j, image.Height - 1));

                    Color pixel = image.GetPixel(newX, newY);

                    minR = Math.Min(minR, pixel.R);
                    minG = Math.Min(minG, pixel.G);
                    minB = Math.Min(minB, pixel.B);
                }
            }

            return Color.FromArgb(minR, minG, minB);
        }

        private Color MaxFilter(Bitmap image, int x, int y)
        {
            int maxR = 0, maxG = 0, maxB = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newX = Math.Max(0, Math.Min(x + i, image.Width - 1));
                    int newY = Math.Max(0, Math.Min(y + j, image.Height - 1));

                    Color pixel = image.GetPixel(newX, newY);

                    maxR = Math.Max(maxR, pixel.R);
                    maxG = Math.Max(maxG, pixel.G);
                    maxB = Math.Max(maxB, pixel.B);
                }
            }

            return Color.FromArgb(maxR, maxG, maxB);
        }
        private Color MedianFilter(Bitmap image, int x, int y)
        {
            int[] redValues = new int[9];
            int[] greenValues = new int[9];
            int[] blueValues = new int[9];

            int index = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newX = Math.Max(0, Math.Min(x + i, image.Width - 1));
                    int newY = Math.Max(0, Math.Min(y + j, image.Height - 1));

                    Color pixel = image.GetPixel(newX, newY);

                    redValues[index] = pixel.R;
                    greenValues[index] = pixel.G;
                    blueValues[index] = pixel.B;

                    index++;
                }
            }

            Array.Sort(redValues);
            Array.Sort(greenValues);
            Array.Sort(blueValues);

            int medianR = redValues[4];
            int medianG = greenValues[4];
            int medianB = blueValues[4];

            return Color.FromArgb(medianR, medianG, medianB);
        }
        private Bitmap ApplyThreshold(Bitmap image, int threshold)
        {
            Bitmap resultImage = new Bitmap(image.Width, image.Height);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel = image.GetPixel(x, y);

                    // Calculate the intensity as the average of red, green, and blue values
                    int intensity = (pixel.R + pixel.G + pixel.B) / 3;

                    // Set the pixel value based on the threshold
                    Color thresholdedPixel = (intensity >= threshold) ? Color.White : Color.Black;

                    resultImage.SetPixel(x, y, thresholdedPixel);
                }
            }

            return resultImage;
        }
        private Bitmap AdjustBrightness(Bitmap image, int brightness)
        {
            Bitmap resultImage = new Bitmap(image.Width, image.Height);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel = image.GetPixel(x, y);

                    // Adjust the brightness of each color channel
                    int newRed = Math.Max(0, Math.Min(pixel.R + brightness, 255));
                    int newGreen = Math.Max(0, Math.Min(pixel.G + brightness, 255));
                    int newBlue = Math.Max(0, Math.Min(pixel.B + brightness, 255));

                    // Set the pixel value with adjusted brightness
                    Color adjustedPixel = Color.FromArgb(newRed, newGreen, newBlue);

                    resultImage.SetPixel(x, y, adjustedPixel);
                }
            }

            return resultImage;
        }

        private void filtersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void adjustBToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyNeighborhoodFilter(MinFilter);
        }

        private void maxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyNeighborhoodFilter(MaxFilter);
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyNeighborhoodFilter(MedianFilter);
        }

        private void thresholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
            CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
            cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);
            inputImage = (Bitmap)resized_image;
            if (inputImage != null)
            {
                // Apply the thresholding filter to the image
                Bitmap resultImage = ApplyThreshold(inputImage, 128); // You can adjust the threshold value

                // Display the filtered image
                DisplayImage(resultImage, "Thresholding Filter");
            }
            else
            {
                MessageBox.Show("No image loaded. Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void adjustBrightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
            CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
            cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);
            inputImage = (Bitmap)resized_image;
            if (inputImage != null)
            {
                // Apply the brightness adjustment filter to the image
                Bitmap resultImage = AdjustBrightness(inputImage, 50); // You can adjust the brightness value

                // Display the filtered image
                DisplayImage(resultImage, "Brightness Adjustment Filter");
            }
            else
            {
                MessageBox.Show("No image loaded. Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
