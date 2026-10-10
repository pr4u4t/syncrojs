using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections;
using System.Drawing.Drawing2D;

namespace SyncroC.Utility
{
	class SyncroImage
	{
    //di più bitmap ne fa una sola più lunga
    public static Bitmap CombineBitmaps(Bitmap[] bitmaps)
    {
      if (bitmaps.Length == 0)
        return new Bitmap(1, 1);
      int width = 0;
      int height = 0;
      foreach (Bitmap bmp in bitmaps)
      {
        height += bmp.Height;
        if (width < bmp.Width)
          width = bmp.Width;
      }
      Bitmap bitmap = new Bitmap(width, height);
      using (Graphics grfx = Graphics.FromImage(bitmap))
      {
        int x = 0;
        foreach (Bitmap bmp in bitmaps)
        {
          grfx.DrawImage(bmp, 0, x, width, bmp.Height);
          x += bmp.Height;
        }
      }
      return bitmap;
    }

    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    private static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
    public static Bitmap createSnapShot(Control ctrl)
    {
      Graphics mygraphics = ctrl.CreateGraphics();
      Size s = ctrl.Size;
      Bitmap memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
      Graphics memoryGraphics = Graphics.FromImage(memoryImage);
      IntPtr dc1 = mygraphics.GetHdc();
      IntPtr dc2 = memoryGraphics.GetHdc();
      BitBlt(dc2, 0, 0, ctrl.ClientRectangle.Width, ctrl.ClientRectangle.Height, dc1, 0, 0, 13369376);
      mygraphics.ReleaseHdc(dc1);
      memoryGraphics.ReleaseHdc(dc2);

      return memoryImage;
    }

    public static Bitmap createSnapShot(Control ctrl, int nXSrc, int nYSrc, int nWidth, int nHeight)
    {
      Graphics mygraphics = ctrl.CreateGraphics();
      Bitmap memoryImage = new Bitmap(nWidth, nHeight, mygraphics);
      Graphics memoryGraphics = Graphics.FromImage(memoryImage);
      IntPtr dc1 = mygraphics.GetHdc();
      IntPtr dc2 = memoryGraphics.GetHdc();
      BitBlt(dc2, 0, 0, nWidth, nHeight, dc1, nXSrc, nYSrc, 13369376);
      mygraphics.ReleaseHdc(dc1);
      memoryGraphics.ReleaseHdc(dc2);

      return memoryImage;
    }

		/// <summary>
		/// Converts the given image to the same "black & white" image
		/// </summary>
		/// <param name="image">the image to convert.</param>
		/// <returns></returns>
		public static Image ConvertToBlacknWhite(Image colorImage)
		{
			Image blackNWhiteImage = null;
			if (colorImage == null)
				return blackNWhiteImage;
			blackNWhiteImage = new Bitmap(colorImage);
			Graphics g = Graphics.FromImage(blackNWhiteImage);
			ImageAttributes ia = new ImageAttributes();
			ia.SetColorMatrix(new ColorMatrix(new float[][]{   
				//matrix 1							
																	new float[]{0.3f,0.3f,0.3f,0,0},
															   new float[]{0.59f,0.59f,0.59f,0,0},
															   new float[]{0.11f,0.11f,0.11f,0,0},
															   new float[]{0,0,0,1,0,0},
															   new float[]{0,0,0,0,1,0},
															   new float[]{0,0,0,0,0,1}
				//matrix 2
//																new float[] {0.393f, 0.349f, 0.272f, 0, 0}, 
//																new float[] {0.769f, 0.686f, 0.534f, 0, 0}, 
//																new float[] {0.189f, 0.168f, 0.131f, 0, 0}, 
//																new float[] {   0,   0,   0, 1, 0}, 
//																new float[] {   0,   0,   0, 0, 1} 
				}));
			g.DrawImage(blackNWhiteImage,
				new Rectangle(0, 0, blackNWhiteImage.Width, blackNWhiteImage.Height),
				0, 0, blackNWhiteImage.Width, blackNWhiteImage.Height, GraphicsUnit.Pixel, ia);
			return blackNWhiteImage;
		}

    public static Image InvertImage(Image originalImg)
    {
      Bitmap invertedBmp = new Bitmap(originalImg.Width, originalImg.Height);

      //Setup color matrix
      ColorMatrix clrMatrix = new ColorMatrix(new float[][]
                                                    {
                                                    new float[] {-1, 0, 0, 0, 0},
                                                    new float[] {0, -1, 0, 0, 0},
                                                    new float[] {0, 0, -1, 0, 0},
                                                    new float[] {0, 0, 0, 1, 0},
                                                    new float[] {1, 1, 1, 0, 1}
                                                    });

      using (ImageAttributes attr = new ImageAttributes())
      {
        //Attach matrix to image attributes
        attr.SetColorMatrix(clrMatrix);

        using (Graphics g = Graphics.FromImage(invertedBmp))
        {
          g.DrawImage(originalImg, new Rectangle(0, 0, originalImg.Width, originalImg.Height),
                      0, 0, originalImg.Width, originalImg.Height, GraphicsUnit.Pixel, attr);
        }
      }

      return invertedBmp;
    }

    public enum RectangleCorners
    {
      None = 0, TopLeft = 1, TopRight = 2,
      BottomLeft = 4, BottomRight = 8,
      All = TopLeft | TopRight | BottomLeft | BottomRight
    }

    public static Region BitmapToRegion(Bitmap bitmap, Color transparencyColor)
    {
      if (bitmap == null)
        throw new ArgumentNullException("Bitmap", "Bitmap cannot be null!");

      int height = bitmap.Height;
      int width = bitmap.Width;

      GraphicsPath path = new GraphicsPath();

      for (int j = 0; j < height; j++)
        for (int i = 0; i < width; i++)
        {
          if (bitmap.GetPixel(i, j) == transparencyColor)
            continue;

          int x0 = i;

          while ((i < width) && (bitmap.GetPixel(i, j) != transparencyColor))
            i++;

          path.AddRectangle(new Rectangle(x0, j, i - x0, 1));
        }

      Region region = new Region(path);
      path.Dispose();
      return region;
    }

    public static GraphicsPath Create(int x, int y, int width,
             int height, int radius, RectangleCorners corners)
    {
      int xw = x + width;
      int yh = y + height;
      int xwr = xw - radius;
      int yhr = yh - radius;
      int xr = x + radius;
      int yr = y + radius;
      int r2 = radius * 2;
      int xwr2 = xw - r2;
      int yhr2 = yh - r2;

      GraphicsPath p = new GraphicsPath();
      p.StartFigure();

      //Top Left Corner
      if ((RectangleCorners.TopLeft & corners)
          == RectangleCorners.TopLeft)
      {
        p.AddArc(x, y, r2, r2, 180, 90);
      }
      else
      {
        p.AddLine(x, yr, x, y);
        p.AddLine(x, y, xr, y);
      }

      //Top Edge
      p.AddLine(xr, y, xwr, y);

      //Top Right Corner
      if ((RectangleCorners.TopRight & corners)
          == RectangleCorners.TopRight)
      {
        p.AddArc(xwr2, y, r2, r2, 270, 90);
      }
      else
      {
        p.AddLine(xwr, y, xw, y);
        p.AddLine(xw, y, xw, yr);
      }

      //Right Edge
      p.AddLine(xw, yr, xw, yhr);

      //Bottom Right Corner
      if ((RectangleCorners.BottomRight & corners)
          == RectangleCorners.BottomRight)
      {
        p.AddArc(xwr2, yhr2, r2, r2, 0, 90);
      }
      else
      {
        p.AddLine(xw, yhr, xw, yh);
        p.AddLine(xw, yh, xwr, yh);
      }

      //Bottom Edge
      p.AddLine(xwr, yh, xr, yh);

      //Bottom Left Corner
      if ((RectangleCorners.BottomLeft & corners)
          == RectangleCorners.BottomLeft)
      {
        p.AddArc(x, yhr2, r2, r2, 90, 90);
      }
      else
      {
        p.AddLine(xr, yh, x, yh);
        p.AddLine(x, yh, x, yhr);
      }

      //Left Edge
      p.AddLine(x, yhr, x, yr);

      p.CloseFigure();
      return p;
    }

    public static GraphicsPath Create(Rectangle rect,
             int radius, RectangleCorners corners)
    {
      return Create(rect.X, rect.Y, rect.Width,
                   rect.Height, radius, corners);
    }

    public static GraphicsPath Create(int x, int y, int width,
             int height, int radius)
    {
      return Create(x, y, width, height, radius,
                   RectangleCorners.All);
    }

    public static GraphicsPath Create(Rectangle rect, int radius)
    {
      return Create(rect.X, rect.Y, rect.Width,
                    rect.Height, radius);
    }

    public static GraphicsPath Create(int x, int y,
             int width, int height)
    { return Create(x, y, width, height, 5); }

    public static GraphicsPath Create(Rectangle rect)
    { return Create(rect.X, rect.Y, rect.Width, rect.Height); }
	}
}
