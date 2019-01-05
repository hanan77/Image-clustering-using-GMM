﻿using Mapack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace GMM
{
    public partial class Form1 : Form
    {
        // reference for equations involved in Gaussian Mixture Model using Expectation Maximization
        // https://brilliant.org/wiki/gaussian-mixture-model/

        int k = 2; // number of clusters
        int dataSize = 20;      //N=20
        double[] mu = null;  // mean for cluster k
        Random rand = new Random();
        double[] sigma = null;  // standard dev for cluster k
        double[,] pdf = null;   // calculated pdf for each data point based on mean and var for cluster k
        double[,] Gamma = null; // probablity matrix for each data point
                                // i.e., probablity that a data point belongs to cluster i
        double[] phi = null;    // prior probabilities for each cluster

        List<ImagePoints> imageDataList = null; //data points for image data
        Bitmap bmp = null;  //image bmp
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTestMM_Click(object sender, EventArgs e)
        {
            mu = new double[k];
            sigma = new double[k];  // standard dev for cluster k
            mu[0] = 10; mu[1] = 15;
            sigma[0] = 1; sigma[1] = 3;
            phi = new double[k];    // prior probabilities for each cluster
            Gamma = new double[dataSize, k];   // probablity matrix for each data point
            // i.e., probablity that a data point belongs to cluster i

            pdf = new double[dataSize, k]; // calculated pdf for each data point based on mean and var for cluster k

            //double[] X1 = new double[] {8.57356384052028,8.98554923229468,9.78673281169264,9.67465221963951,
            //    11.9443977919663,9.42822678165054,9.74996772196496,8.43068452965930,9.52261733630593,
            //    8.66202334643774 };  // mean1 = 10, sigma1 = 1
            double[] X1 = new double[10];
            for (int i = 0; i < 10; i++)
                X1[i] = rand.NextDouble() * sigma[0] + mu[0];


            //double[] X2 = new double[] {20.0908970713645,22.5592603025557,21.2127603953281,17.8981393597454,
            //    15.1083713217583,24.3800395554279,26.1501281988136,20.3615017990982,17.0302951873364,
            //    23.5933144229446 }; // mean2 = 20, sigma2 = 3
            double[] X2 = new double[10];
            for (int i = 0; i < 10; i++)
                X2[i] = rand.NextDouble() * sigma[1] + mu[1];

            double[] X = X1.Concat(X2).ToArray<double>();  // catenate X1 and X2

            // ----------Initialization step - randonly select k data poits to act as means
            List<int> RList = new List<int>();
            for (int i = 0; i < k; i++)
            {
                int rpos = rand.Next(X.Length);
                if (RList.Contains(rpos))
                    rpos = rand.Next(X.Length);
                mu[i] = X[rpos];
            }

            // set the variance of each cluster to be the overall variance
            double varianceOfData = ComputeVariance(X);
            for (int i = 0; i < k; i++)
            {
                sigma[i] = Math.Sqrt(varianceOfData);
            }

            // set prior probablities of each cluster to be uniform
            for (int i = 0; i < k; i++)
            {
                phi[i] = 1.0 / k;
            }
            //--------------------------end initialization-------------------------------------


            // ---------------------------Expectation Maximization------------------------------
            for (int n = 0; n < 1000; n++)  
            {
                //---------perform Expectation step---------------------
                for (int i = 0; i < X.Length; i++)
                {
                    for (int kk = 0; kk < k; kk++)
                    {
                        pdf[i, kk] = Gaussian(X[i], mu[kk], sigma[kk]);
                    }
                }
                double[] Gdenom = new double[X.Length];
                for (int i = 0; i < X.Length; i++) // denominator for Gamma
                {
                    double sum = 0;
                    for (int kk = 0; kk < k; kk++)
                    {
                        sum = sum + phi[kk] * pdf[i, kk];
                    }
                    Gdenom[i] = sum;
                }

                for (int i = 0; i < X.Length; i++) 
                {
                    for (int kk = 0; kk < k; kk++)
                    {
                        Gamma[i, kk] = (phi[kk] * pdf[i, kk])/Gdenom[i];
                    }
                }

                //-------------------end Expectation--------------------

                //---------perform Maximization Step--------------------
                //----------update phi--------------
                for (int kk = 0; kk < k; kk++)
                {
                    double sum = 0;
                    for (int i = 0; i < X.Length; i++)
                    {
                        sum += Gamma[i, kk];
                    }
                    phi[kk] = sum / (X.Length);
                }
                //---------------------------------

                //-------------update mu-----------
                double[] MuNumer = new double[X.Length];
                for (int kk = 0; kk < k; kk++)
                {
                    double sum = 0;
                    for (int i = 0; i < X.Length; i++)
                    {
                        sum += Gamma[i, kk] * X[i];
                    }
                    MuNumer[kk] = sum;
                }

                double[] MuDenom = new double[X.Length];
                for (int kk = 0; kk < k; kk++)
                {
                    double sum = 0;
                    for (int i = 0; i < X.Length; i++)
                    {
                        sum += Gamma[i, kk];
                    }
                    MuDenom[kk] = sum;
                }
                for (int i = 0; i < k; i++)
                    mu[i] = MuNumer[i] / MuDenom[i];
                //-----------------------------------

                //-------------update sigma----------
                double[] VarianceNumer = new double[X.Length];
                for (int kk = 0; kk < k; kk++)
                {
                    double sum = 0;
                    for (int i = 0; i < X.Length; i++)
                    {
                        sum += Gamma[i, kk] * (X[i] - mu[kk]) * (X[i] - mu[kk]);
                    }
                    VarianceNumer[kk] = sum;
                }
                for (int i = 0; i < k; i++)
                    sigma[i] = Math.Sqrt(VarianceNumer[i] / MuDenom[i]);
                //--------------end update Sigma--------

                //---------------end Maximization-------------------------------
            }
            var G = Gamma;
            MessageBox.Show("End Expectation Maximization..");
        }

        public double ComputeVariance(double[] data)
        {
            double avg = data.Average();
            double sum = 0;
            foreach (double num in data)
            {
                sum += (num - avg)*(num - avg);
            }
            return sum / data.Length;
        }

        public double Gaussian(double num, double mean, double stddev)  // 1-D gaussian
        {
            double res = (1 / (stddev * Math.Sqrt(2.0 * Math.PI))) * 
                Math.Exp( (-1 * (num - mean) * (num - mean)) / (2 * stddev * stddev));
            return res;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //double res = Gaussian(8.0, 8.0, 2.0);
            //MessageBox.Show(res.ToString());
            double num = double.Parse(txtNum.Text);
            double denom = 0;
            for (int i = 0; i < k; i++)
                denom += phi[i] * Gaussian(num, mu[i], sigma[i]);
            double[] C = new double[k];  // p(Ci|x)   - probablity x belongs to cluster Ci
            for (int i = 0; i < k; i++)
                C[i] = phi[i] * Gaussian(num, mu[i], sigma[i])/denom;
            string out1 = "";
            int cnum = 0;
            foreach (double p in C)
            {
                out1 += "Cluster " + cnum.ToString() + " Probab. = " + p.ToString() + "\n";
                cnum++;
            }
            MessageBox.Show(out1);
            
        }

      
        private void btnGMMND_Click(object sender, EventArgs e)
        {
            int k = int.Parse(txtNumClusters.Text);   // number of clusters
            int dim = 2; // number of dimensions for data
            //int datSize = 20;

            // read 2-D data
            //FileInfo fi = new FileInfo("d:\\csharp2016\\clusterdata\\data2D.txt");
            //FileInfo fi = new FileInfo("d:\\csharp2016\\clusterdata\\cdata1.txt");
            //StreamReader sr = new StreamReader(fi.Open(FileMode.Open, FileAccess.Read));
            //string sline = sr.ReadLine();
            //int datSize = 1;
            //while (sline != null)
            //{
            //    datSize++;
            //    sline = sr.ReadLine();
            //}
            //sr.Close();
            //sr = new StreamReader(fi.Open(FileMode.Open, FileAccess.Read));
            //sline = sr.ReadLine();
            //Matrix X = new Matrix(datSize, dim);
            //int count = 0;
            //while (sline != null)
            //{
            //    string[] parts = sline.Split(new char[] { '\t', ' ' });
            //    X[count, 0] = double.Parse(parts[1].Trim());
            //    X[count, 1] = double.Parse(parts[2].Trim());
            //    count++;
            //    sline = sr.ReadLine();
            //}
            //sr.Close();

            int datSize = 1000;
            List<MyPoint> PList = InitializeData(datSize);
            Mapack.Matrix X = new Matrix(datSize, dim);
            for (int i = 0; i < PList.Count; i++)
            {
                X[i, 0] = PList[i].X;
                X[i, 1] = PList[i].Y;
            }
            

            GMM_NDim gmmnd = new GMM_NDim(k, dim, X,dataSize);
            gmmnd.ComputeGMM_ND();

            // determine class membership i.e., which point belongs to which cluster
            PList = new List<MyPoint>();
            for (int i = 0; i < X.Rows; i++)
            {
                // Gamma matrix has the probabilities for a data point for its membership in each cluster
                double[] probabs = new double[k];
                int cnum = 0;
                double maxprobab = gmmnd.Gamma[i, 0];
                for (int m = 0; m < k; m++)
                {
                    if (gmmnd.Gamma[i, m] > maxprobab)
                    {
                        cnum = m;  // data i belongs to cluster m
                        maxprobab = gmmnd.Gamma[i, m];
                    }
                }
                MyPoint pt = new MyPoint { ClusterId = cnum, X = X[i, 0], Y = X[i, 1] };
                PList.Add(pt);
            }
            MyImageProc.DrawClusters(pic1, PList, 1, k);
        }

        List<MyPoint> InitializeData(int datSize)
        {
            List<MyPoint> PList = new List<MyPoint>();
            PList.Clear();
            Random rand = new Random();
            int dataLength = datSize;   // number of data points
            // create 4 distributions with different means and std devs
            double meanx0 = 150;
            double meanx1 = 180;
            double meanx2 = 425;
            double meanx3 = 475;
            double meany0 = 175;
            double meany1 = 250;
            double meany2 = 300;
            double meany3 = 320;

            double stddevx0 = 100;
            double stddevx1 = 90;
            double stddevx2 = 220;
            double stddevx3 = 260;
            double stddevy0 = 50;
            double stddevy1 = 80;
            double stddevy2 = 180;
            double stddevy3 = 195;
            int index = 0;
            for (int i = 0; i < dataLength / 4; i++)
            {
                MyPoint pt = new MyPoint();
                double rnum = rand.NextDouble();
                if (rnum < 0.5)
                    pt.X = rand.NextDouble() * stddevx0 / 2 + meanx0;
                else
                    pt.X = -1 * rand.NextDouble() * stddevx0 / 2 + meanx0;
                if (rnum < 0.5)
                    pt.Y = rand.NextDouble() * stddevy0 / 2 + meany0;
                else
                    pt.Y = -1 * rand.NextDouble() * stddevy0 / 2 + meany0;
                index++;
                PList.Add(pt);
            }

            for (int i = 0; i < dataLength / 4; i++)
            {
                MyPoint pt = new MyPoint();
                double rnum = rand.NextDouble();
                if (rnum < 0.5)
                    pt.X = rand.NextDouble() * stddevx1 / 2 + meanx1;
                else
                    pt.X = -1 * rand.NextDouble() * stddevx1 / 2 + meanx1;
                if (rnum < 0.5)
                    pt.Y = rand.NextDouble() * stddevy1 / 2 + meany1;
                else
                    pt.Y = -1 * rand.NextDouble() * stddevy1 / 2 + meany1;
                index++;
                PList.Add(pt);
            }

            for (int i = 0; i < dataLength / 4; i++)
            {
                double rnum = rand.NextDouble();
                MyPoint pt = new MyPoint();
                if (rnum < 0.5)
                    pt.X = rand.NextDouble() * stddevx2 / 2 + meanx2;
                else
                    pt.X = -1 * rand.NextDouble() * stddevx2 / 2 + meanx2;
                if (rnum < 0.5)
                    pt.Y = rand.NextDouble() * stddevy2 / 2 + meany2;
                else
                    pt.Y = -1 * rand.NextDouble() * stddevy2 / 2 + meany2;
                PList.Add(pt);
                index++;
            }

            for (int i = 0; i < dataLength / 4; i++)
            {
                double rnum = rand.NextDouble();
                MyPoint pt = new MyPoint();
                if (rnum < 0.5)
                    pt.X = rand.NextDouble() * stddevx3 / 2 + meanx3;
                else
                    pt.X = -1 * rand.NextDouble() * stddevx3 / 2 + meanx3;
                if (rnum < 0.5)
                    pt.Y = rand.NextDouble() * stddevy3 / 2 + meany3;
                else
                    pt.Y = -1 * rand.NextDouble() * stddevy3 / 2 + meany3;
                PList.Add(pt);
                index++;
            }
            MyImageProc.DrawClusters(pic1, PList, 1.0, 1);
            return PList;
        }

        private void btnGMMImage_Click(object sender, EventArgs e)
        {
            pic1.Load("Tennis Ball 620.jpg");
            bmp = new Bitmap("Tennis Ball 620.jpg");
            imageDataList = new List<ImagePoints>(bmp.Width * bmp.Height);

            int k = int.Parse(txtNumClusters.Text);
            int dim = 3;
            imageDataList = InitializeImageData(bmp,k);
            Matrix imgMatrix = new Matrix(bmp.Width * bmp.Height, dim);
            for(int i = 0; i < (bmp.Width * bmp.Height); i++)
            {
                imgMatrix[i, 0] = imageDataList[i].red;
                imgMatrix[i, 1] = imageDataList[i].green;
                imgMatrix[i, 2] = imageDataList[i].blue;
            }

            GMM_NDim gmmnd = new GMM_NDim(k, dim, imgMatrix, bmp.Width*bmp.Height);
            gmmnd.ComputeGMM_ND();

            // determine class membership i.e., which point belongs to which cluster
            imageDataList = new List<ImagePoints>();
            for (int i = 0; i < imgMatrix.Rows; i++)
            {
                // Gamma matrix has the probabilities for a data point for its membership in each cluster
                double[] probabs = new double[k];
                int cnum = 0;
                double maxprobab = gmmnd.Gamma[i, 0];
                for (int m = 0; m < k; m++)
                {
                    if (gmmnd.Gamma[i, m] > maxprobab)
                    {
                        cnum = m;  // data i belongs to cluster m
                        maxprobab = gmmnd.Gamma[i, m];
                    }
                }
                ImagePoints imgpt = new ImagePoints { clusterID = cnum, red = gmmnd.X[i, 0], green = gmmnd.X[i, 1], blue = gmmnd.X[i, 2] };
                
                imageDataList.Add(imgpt);
            }
            //MyImageProc.DrawClusters(pic1, imageDataList, 1, k);
            //redraw image from rgb values
            MessageBox.Show("img list len = " + imageDataList.Count.ToString()+" img width = "+bmp.Width.ToString()+" img height = "+bmp.Height.ToString());
            
            //MessageBox.Show("new image done");
        }

        List<ImagePoints> InitializeImageData(Bitmap bmp,int numClusters)
        {
            System.Diagnostics.Debug.WriteLine("in intialise");
            List<ImagePoints> PList = new List<ImagePoints>();
            PList.Clear();
            Random rand = new Random();
            int dataLength = bmp.Width*bmp.Height;   // number of data points
            // create numCLuster distributions with different means and std devs
            double[] meanX = new double[numClusters];
            double[] meanY = new double[numClusters];
            double[] meanZ = new double[numClusters];
            double meanSampleX = 0;
            double meanSampleY = 0;
            double meanSampleZ = 0;
            double[] stdDevX = new double[numClusters];
            double[] stdDevY = new double[numClusters];
            double[] stdDevZ = new double[numClusters];
            double stdDevSampleX = 0;
            double stdDevSampleY = 0;
            double stdDevSampleZ = 0;

            //initialise datapoints to RGB values from image
            for (int i = 0; i < bmp.Width; i++)
            {
                for(int j = 0; j < bmp.Height; j++)
                {
                    ImagePoints imgpt = new ImagePoints();
                    imgpt.red = (double)bmp.GetPixel(i, j).R;
                    imgpt.green = (double)bmp.GetPixel(i, j).G;
                    imgpt.blue = (double)bmp.GetPixel(i, j).B;
                    PList.Add(imgpt);
                }
            }

            //selecting means i.e. random pts from the dataset.
            //for (int i = 0; i < numClusters; i++)
            //{
            //    meanX[i] = PList[rand.Next(0, dataLength)].red;
            //    meanY[i] = PList[rand.Next(0, dataLength)].green;
            //    meanZ[i] = PList[rand.Next(0, dataLength)].blue;                
            //}
            
            ////calculate sample mean
            //for(int i = 0; i < dataLength; i++)
            //{
            //    meanSampleX += PList[i].red;
            //    meanSampleY += PList[i].green;
            //    meanSampleZ += PList[i].blue;
            //}
            //meanSampleX /= dataLength;
            //meanSampleY /= dataLength;
            //meanSampleZ /= dataLength;

            ////calculate sample variance
            //for(int i = 0; i < dataLength; i++)
            //{
            //    stdDevSampleX += ((PList[i].red - meanSampleX) * (PList[i].red - meanSampleX));
            //    stdDevSampleY += ((PList[i].green - meanSampleY) * (PList[i].green - meanSampleY));
            //    stdDevSampleZ += ((PList[i].blue - meanSampleZ) * (PList[i].blue - meanSampleY));
            //}
            //stdDevSampleX /= dataLength;
            //stdDevSampleY /= dataLength;
            //stdDevSampleZ /= dataLength;

            ////set component variances to sample variance
            //for (int i = 0; i < numClusters; i++)
            //{
            //    stdDevX[i] = Math.Sqrt(stdDevSampleX);
            //    stdDevY[i] = Math.Sqrt(stdDevSampleY);
            //    stdDevZ[i] = Math.Sqrt(stdDevSampleZ);
            //}
                        
            return PList;
        }

        private void btnClusterImage_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color newColor = new Color();
                    if (imageDataList[i * bmp.Height + j].clusterID == int.Parse(txtClusterID.Text))
                    {
                        int R = (int)imageDataList[i * bmp.Height + j].red;
                        int G = (int)imageDataList[i * bmp.Height + j].green;
                        int B = (int)imageDataList[i * bmp.Height + j].blue;
                        newColor = Color.FromArgb(R, G, B);
                    }
                    else
                        newColor = Color.White;

                    bmp.SetPixel(i, j, newColor);
                }
            }
            //Thread.Sleep(1000);
            pic1.Image = bmp;
        }
    }
}
