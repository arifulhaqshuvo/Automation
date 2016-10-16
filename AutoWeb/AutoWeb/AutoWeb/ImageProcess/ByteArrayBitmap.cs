using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace AutoWeb.ImageProcess
{
    public unsafe class GrayImageProcess
    {
        public int Name { get; set; }
        public byte[,] R;
        public readonly byte[,] G;
        public readonly byte[,] B;
        public readonly byte[,] E;
        public int Width { get; set; }
        public int Height { get; set; }

        private GrayImageProcess(int name, byte[,] b, byte[,] g, byte[,] r)
        {
            Name = name;
            R = r;
            Width = r.GetLength(1);
            Height = r.GetLength(0);
            B = b;
            G = g;
        }

        private byte[,] Copy(byte[,] src, int w, int l)
        {
            var arr = new byte[src.GetLength(0), w];

            for (var i = 0; i < Height; i++)
            {
                var startW = w * l;
                for (var j = 0; j < w; j++)
                    arr[i, j] = src[i, j + startW];
            }

            return arr;
        }
        public GrayImageProcess[] Split(int n)
        {
            var listImg = new List<GrayImageProcess>();
            var w = Width / n;
            for (var l = 0; l < n; l++)
            {
                var b = Copy(B, w, l);
                var g = Copy(G, w, l);
                var r = Copy(R, w, l);

                listImg.Add(new GrayImageProcess(l, b, g, r));
            }
            return listImg.ToArray();
        }

        public Image Image
        {
            get
            {
                var bm = new Bitmap(Width, Height);
                var bmWidth = bm.Width;
                var bmHeight = bm.Height;
                var bmData = bm.LockBits(new Rectangle(0, 0, bmWidth, bmHeight), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                var bmStride = bmData.Stride;
                var bmp = (byte*)bmData.Scan0;
                byte* p;
                for (var i = 0; i < bmHeight; i++)
                {
                    for (var j = 0; j < bmWidth; j++)
                    {
                        p = bmp + i * bmStride + j * 3;
                        p[0] = B[i, j];
                        p[1] = G[i, j];
                        p[2] = R[i, j];
                    }
                }
                bm.UnlockBits(bmData);
                return bm;
            }
        }

        public GrayImageProcess(Bitmap colorImage)
        {
            Width = colorImage.Width;
            Height = colorImage.Height;
            R = new byte[Height, Width];
            B = new byte[Height, Width];
            G = new byte[Height, Width];
            E = new byte[Height, Width];

            var colorImageData = colorImage.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var colorImageStride = colorImageData.Stride;
            var colorImagep = (byte*)colorImageData.Scan0;
            byte* p;
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    p = colorImagep + i * colorImageStride + j * 3;

                    E[i, j] = (byte)(0.5 * p[0] + 0.3 * p[1] + 0.2 * p[2]);
                    B[i, j] = p[0];
                    G[i, j] = p[1];
                    R[i, j] = p[2];
                }
            }
            colorImage.UnlockBits(colorImageData);

        }

        private long SumArr(byte[,] data)
        {
            long l = 0;
            for (var i = 0; i < data.GetLength(0); i++)
            {
                for (var j = 0; j < data.GetLength(1); j++)
                {
                    l += data[i, j];
                }
            }
            return l;
        }

        public long[] Sum()
        {
            return new[] { SumArr(B), SumArr(G), SumArr(R) };
        }

        public static GrayImageProcess[] Process(GrayImageProcess[] arr)
        {
            var x = arr.Select(x1 => x1.Sum()).ToArray();
            long m = -1;
            var re = new GrayImageProcess[0];
            for (var i = 0; i < x.Length - 1; i++)
            {
                for (var j = i + 1; j < x.Length; j++)
                {
                    var r = Math.Abs(x[i][0] - x[j][0]);
                    r += Math.Abs(x[i][1] - x[j][1]);
                    r += Math.Abs(x[i][2] - x[j][2]);


                    if (r >= m && m != -1) continue;
                    m = r;
                    re = new[] { arr[i], arr[j] };
                }
            }
            return re;
        }

        public void Threadso(byte threadso)
        {
            for (var i = 0; i < Height; i++)
                for (var j = 0; j < Width; j++)
                    R[i, j] = R[i, j] > threadso ? (byte)255 : (byte)0;
        }

        public void EdgeFillter()
        {
            var h = new int[,]
            {
                {0,1,0},
                {1,-4,1},
                {0,1,0}
            };

            var arr = new byte[Height, Width];

            for (var i = 0; i < Height - 3; i++)
            {
                for (var j = 0; j < Width - 3; j++)
                {
                    var tong = 0;

                    for (var k = 0; k < 3; k++)
                    {
                        for (var l = 0; l < 3; l++)
                        {
                            tong = tong + h[k, l] * R[i + k, j + l];
                        }
                    }

                    if (tong != 0)
                    {
                        arr[i, j] = 0;
                    }
                    else arr[i, j] = 255;
                }
            }

            R = arr;

        }
    }
}
