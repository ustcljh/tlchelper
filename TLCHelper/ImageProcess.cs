using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLCHelper
{
    public static class Homography
    {
        // Compute the 3x3 homography matrix from 4 source and 4 destination points
        public static double[] ComputeHomography(PointF[] src, PointF[] dst)
        {
            if (src.Length != 4 || dst.Length != 4)
                throw new ArgumentException("Exactly 4 source and 4 destination points are required.");

            double[,] A = new double[8, 8];
            double[] B = new double[8];

            for (int i = 0; i < 4; i++)
            {
                float x = src[i].X;
                float y = src[i].Y;
                float xp = dst[i].X;
                float yp = dst[i].Y;

                A[i * 2, 0] = x;
                A[i * 2, 1] = y;
                A[i * 2, 2] = 1;
                A[i * 2, 3] = 0;
                A[i * 2, 4] = 0;
                A[i * 2, 5] = 0;
                A[i * 2, 6] = -x * xp;
                A[i * 2, 7] = -y * xp;
                B[i * 2] = xp;

                A[i * 2 + 1, 0] = 0;
                A[i * 2 + 1, 1] = 0;
                A[i * 2 + 1, 2] = 0;
                A[i * 2 + 1, 3] = x;
                A[i * 2 + 1, 4] = y;
                A[i * 2 + 1, 5] = 1;
                A[i * 2 + 1, 6] = -x * yp;
                A[i * 2 + 1, 7] = -y * yp;
                B[i * 2 + 1] = yp;
            }

            double[] H = SolveLinearSystem(A, B);
            Array.Resize(ref H, 9); // Add h8 = 1
            H[8] = 1;

            return H;
        }

        // Apply the homography to a single point
        public static PointF ApplyHomography(double[] H, PointF pt)
        {
            double x = pt.X;
            double y = pt.Y;
            double denom = H[6] * x + H[7] * y + H[8];
            double xp = (H[0] * x + H[1] * y + H[2]) / denom;
            double yp = (H[3] * x + H[4] * y + H[5]) / denom;
            return new PointF((float)xp, (float)yp);
        }

        // Solves Ax = B using Gaussian elimination
        private static double[] SolveLinearSystem(double[,] A, double[] B)
        {
            int n = B.Length;
            double[] X = new double[n];
            double[,] M = new double[n, n + 1];

            // Augment matrix
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    M[i, j] = A[i, j];
                M[i, n] = B[i];
            }

            // Gaussian elimination
            for (int i = 0; i < n; i++)
            {
                // Pivot row
                int maxRow = i;
                for (int k = i + 1; k < n; k++)
                    if (Math.Abs(M[k, i]) > Math.Abs(M[maxRow, i]))
                        maxRow = k;

                // Swap rows
                for (int k = i; k <= n; k++)
                {
                    double tmp = M[maxRow, k];
                    M[maxRow, k] = M[i, k];
                    M[i, k] = tmp;
                }

                // Eliminate column
                for (int k = i + 1; k < n; k++)
                {
                    double f = M[k, i] / M[i, i];
                    for (int j = i; j <= n; j++)
                        M[k, j] -= f * M[i, j];
                }
            }

            // Back-substitution
            for (int i = n - 1; i >= 0; i--)
            {
                X[i] = M[i, n] / M[i, i];
                for (int k = i - 1; k >= 0; k--)
                    M[k, n] -= M[k, i] * X[i];
            }

            return X;
        }

        public static double[] InvertHomographyMatrix(double[] H)
        {
            if (H.Length != 9)
                throw new ArgumentException("Homography must have 9 elements.");

            // Convert H to 3x3 matrix
            double[,] m = new double[3, 3];
            for (int i = 0; i < 9; i++)
                m[i / 3, i % 3] = H[i];

            // Compute determinant
            double det =
                m[0, 0] * (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1]) -
                m[0, 1] * (m[1, 0] * m[2, 2] - m[1, 2] * m[2, 0]) +
                m[0, 2] * (m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0]);

            if (Math.Abs(det) < 1e-10)
                throw new InvalidOperationException("Homography matrix is singular and cannot be inverted.");

            double invDet = 1.0 / det;

            double[,] inv = new double[3, 3];

            inv[0, 0] = (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1]) * invDet;
            inv[0, 1] = -(m[0, 1] * m[2, 2] - m[0, 2] * m[2, 1]) * invDet;
            inv[0, 2] = (m[0, 1] * m[1, 2] - m[0, 2] * m[1, 1]) * invDet;

            inv[1, 0] = -(m[1, 0] * m[2, 2] - m[1, 2] * m[2, 0]) * invDet;
            inv[1, 1] = (m[0, 0] * m[2, 2] - m[0, 2] * m[2, 0]) * invDet;
            inv[1, 2] = -(m[0, 0] * m[1, 2] - m[0, 2] * m[1, 0]) * invDet;

            inv[2, 0] = (m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0]) * invDet;
            inv[2, 1] = -(m[0, 0] * m[2, 1] - m[0, 1] * m[2, 0]) * invDet;
            inv[2, 2] = (m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0]) * invDet;

            // Flatten to 1D array
            double[] invH = new double[9];
            for (int i = 0; i < 9; i++)
                invH[i] = inv[i / 3, i % 3];

            return invH;
        }
    }

    public class ImageProcess
    {
        public static Bitmap Transform(Bitmap source, PointF[] srcPoints, int destWidth, int destHeight)
        {
            // Step 1: Define destination rectangle points
            PointF[] dstPoints = new PointF[]
            {
                new PointF(0, 0),
                new PointF(destWidth - 1, 0),
                new PointF(destWidth - 1, destHeight - 1),
                new PointF(0, destHeight - 1)
            };

            var h = Homography.ComputeHomography(srcPoints, dstPoints);
            h = Homography.InvertHomographyMatrix(h);

            // Step 4: Create output image
            Bitmap output = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);

            // Step 5: Read source image pixels
            BitmapData srcData = source.LockBits(
                new Rectangle(0, 0, source.Width, source.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            int srcStride = srcData.Stride;
            byte[] srcBuffer = new byte[srcStride * source.Height];
            System.Runtime.InteropServices.Marshal.Copy(srcData.Scan0, srcBuffer, 0, srcBuffer.Length);
            source.UnlockBits(srcData);

            // Step 6: Write output pixels
            for (int y = 0; y < destHeight; y++)
            {
                for (int x = 0; x < destWidth; x++)
                {
                    var newPoint = Homography.ApplyHomography(h, new PointF(x, y));

                    // Debug.WriteLine($"({newPoint.X}, {newPoint.Y}) -> ({x}, {y})");

                    // Bilinear interpolation
                    Color color = BilinearSample(srcBuffer, source.Width, source.Height, srcStride, newPoint.X, newPoint.Y);
                    output.SetPixel(x, y, color);
                }
            }

            return output;
        }

        private static Color BilinearSample(byte[] buffer, int width, int height, int stride, double x, double y)
        {
            int x0 = (int)Math.Floor(x);
            int y0 = (int)Math.Floor(y);
            int x1 = x0 + 1;
            int y1 = y0 + 1;

            if (x0 < 0 || x1 >= width || y0 < 0 || y1 >= height)
                return Color.Black;

            double dx = x - x0;
            double dy = y - y0;

            Color c00 = GetPixel(buffer, stride, x0, y0);
            Color c10 = GetPixel(buffer, stride, x1, y0);
            Color c01 = GetPixel(buffer, stride, x0, y1);
            Color c11 = GetPixel(buffer, stride, x1, y1);

            byte r = (byte)((1 - dx) * (1 - dy) * c00.R +
                             dx * (1 - dy) * c10.R +
                             (1 - dx) * dy * c01.R +
                             dx * dy * c11.R);

            byte g = (byte)((1 - dx) * (1 - dy) * c00.G +
                             dx * (1 - dy) * c10.G +
                             (1 - dx) * dy * c01.G +
                             dx * dy * c11.G);

            byte b = (byte)((1 - dx) * (1 - dy) * c00.B +
                             dx * (1 - dy) * c10.B +
                             (1 - dx) * dy * c01.B +
                             dx * dy * c11.B);

            return Color.FromArgb(r, g, b);
        }

        private static Color GetPixel(byte[] buffer, int stride, int x, int y)
        {
            int index = y * stride + x * 3;
            byte b = buffer[index + 0];
            byte g = buffer[index + 1];
            byte r = buffer[index + 2];
            return Color.FromArgb(r, g, b);
        }

        public static float DistancePointToLine(PointF p, PointF a, PointF b)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;

            // If A and B are the same point, return distance to point A
            if (dx == 0 && dy == 0)
                return Distance(p, a);

            // Compute numerator of the point-to-line formula
            float numerator = Math.Abs(dy * p.X - dx * p.Y + b.X * a.Y - b.Y * a.X);
            float denominator = (float)Math.Sqrt(dx * dx + dy * dy);

            return numerator / denominator;
        }

        // Optional helper: Euclidean distance between two points
        public static float Distance(PointF p1, PointF p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
