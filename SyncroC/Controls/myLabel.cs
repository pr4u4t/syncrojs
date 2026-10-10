using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

//ABELLO_FI06/11/2008_1 La barra dei messaggi ora ha il testo in OutLine per una megliore visualizzazione 
namespace SyncroC.Controls
{
  public partial class myLabel : SyncroC.Controls.SyncroUserControl
  {
    private string testo;
    public string Testo
    {
      get
      {
        return testo;
      }
      set
      {
        if (testo != value)
        {
          testo = value;
          this.Invalidate();
        }
      }
    }

    private bool outLineText = true;
    public bool OutLineText
    {
      get { return outLineText; }
      set { outLineText = value; }
    }

    private Color colore1 = Color.Red;
    public Color Colore1
    {
      get { return colore1; }
      set { colore1 = value; }
    }

    private Color colore2 = Color.Blue;
    public Color Colore2
    {
      get { return colore2; }
      set { colore2 = value; }
    }

    private Color outLineTextcolore = Color.Black;
    public Color OutLineTextcolore
    {
      get { return outLineTextcolore; }
      set { outLineTextcolore = value; }
    }

    public Color Colore
    {
      set 
      { 
        colore1 = value;
        colore2 = value; 
      }
    }

    private System.Drawing.ContentAlignment textAlign = ContentAlignment.MiddleCenter;
    public System.Drawing.ContentAlignment TextAlign
    {
      get { return textAlign; }
      set { textAlign = value; }
    }

    private Color backcolorTesto = Color.Transparent;
    public Color BackcolorTesto
    {
      get { return backcolorTesto; }
      set { backcolorTesto = value; }
    }

    public myLabel()
    {
      InitializeComponent();
      this.outLineTextcolore = Color.Black;
      this.outLineText = true;
      this.Colore = Color.White;
      this.testo = "";
      this.textAlign = ContentAlignment.MiddleCenter;
    }

    /// <summary>
    /// Resize the internal component according to the parameters.
    /// </summary>
    /// <param name="VerticalResizing">coefficient for vertical resizing.</param>
    /// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
    /// <param name="FontResizing">coefficient for font resizing.</param>
    public override void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
    {
      if (!(VerticalResizing == 1 && OrizontalResizing == 1 && FontResizing == 1))
      {
        this.Font = new Font(this.Font.FontFamily, Convert.ToInt32(this.Font.Size * FontResizing), this.Font.Style);
        
        Invalidate();
      }
    }

    private StringFormat getAlignment()
    {
      StringFormat sf = new StringFormat();

      switch (this.textAlign)
      {
        case ContentAlignment.BottomCenter:
          sf.LineAlignment = StringAlignment.Far;
          sf.Alignment = StringAlignment.Center;
          break;

        case ContentAlignment.BottomLeft:
          sf.LineAlignment = StringAlignment.Far;
          sf.Alignment = StringAlignment.Near;
          break;

        case ContentAlignment.BottomRight:
          sf.LineAlignment = StringAlignment.Far;
          sf.Alignment = StringAlignment.Far;
          break;

        case ContentAlignment.MiddleCenter:
          sf.LineAlignment = StringAlignment.Center;
          sf.Alignment = StringAlignment.Center;
          break;

        case ContentAlignment.MiddleLeft:
          sf.LineAlignment = StringAlignment.Center;
          sf.Alignment = StringAlignment.Near;
          break;

        case ContentAlignment.MiddleRight:
          sf.LineAlignment = StringAlignment.Center;
          sf.Alignment = StringAlignment.Far;
          break;

        case ContentAlignment.TopCenter:
          sf.LineAlignment = StringAlignment.Near;
          sf.Alignment = StringAlignment.Center;
          break;

        case ContentAlignment.TopLeft:
          sf.LineAlignment = StringAlignment.Near;
          sf.Alignment = StringAlignment.Near;
          break;

        case ContentAlignment.TopRight:
          sf.LineAlignment = StringAlignment.Near;
          sf.Alignment = StringAlignment.Far;
          break;
      }

      sf.Trimming = StringTrimming.Character;

      return sf;
    }

    public int CountOccurencesOfChar(string input, string pattern)
    {
      Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
      MatchCollection matches = r.Matches(input);

      return matches.Count; 
    }

    private void myLabel_Paint(object sender, PaintEventArgs e)
    {
      try
      {
        Rectangle myRectangle = new Rectangle(0, 0, this.Width, this.Height);
        
        if (myRectangle.IsEmpty)
          return;

        // Create the drawing area in memory.
        // Double buffering is used to prevent flicker.
        Bitmap bufl = null;
        if (this.BackgroundImage != null)
          bufl = new Bitmap(this.BackgroundImage, myRectangle.Width, myRectangle.Height);
        else
          bufl = new Bitmap(myRectangle.Width, myRectangle.Height);
        Graphics g = Graphics.FromImage(bufl);

        if (this.BackgroundImage == null)
          g.FillRectangle(new SolidBrush(this.BackColor), myRectangle);

        if (this.testo != null)
        {
          StringFormat format = this.getAlignment();

          Brush myBrush = new System.Drawing.Drawing2D.LinearGradientBrush(myRectangle, this.colore1, this.colore2, 10);

          if (this.backcolorTesto != Color.Transparent)
          {
            Rectangle rct = myRectangle;
            rct.Height = this.Font.Height;
            g.FillRectangle(new SolidBrush(this.backcolorTesto), rct);
          }

          if (this.CountOccurencesOfChar(this.testo, Environment.NewLine)==1)
            myRectangle.Offset(0, (int)(myRectangle.Height - g.MeasureString(this.testo, this.Font).Height) / 2);

          if (this.outLineText)
          {
            Brush MyBrushShadow = new SolidBrush(this.outLineTextcolore);
            Rectangle rct2 = myRectangle;
            rct2.X = myRectangle.X - 1;
            g.DrawString(this.testo, this.Font, MyBrushShadow, rct2, format);
            rct2.X = myRectangle.X + 1;
            g.DrawString(this.testo, this.Font, MyBrushShadow, rct2, format);
            rct2 = myRectangle;
            rct2.Y = myRectangle.Y - 1;
            g.DrawString(this.testo, this.Font, MyBrushShadow, rct2, format);
            rct2.Y = myRectangle.Y + 1;
            g.DrawString(this.testo, this.Font, MyBrushShadow, rct2, format);
          }

          g.DrawString(this.testo, this.Font, myBrush, myRectangle, format);
        }

        // Render the finished image on the form.
        e.Graphics.DrawImageUnscaled(bufl, 0, 0);
        g.Dispose();
      }
      catch(Exception ex)
      {
        SyncroC.Utility.Logger.LogError(this, "Attenzione Errore sulla myLabel", ex);
      }
    }
  }
}
