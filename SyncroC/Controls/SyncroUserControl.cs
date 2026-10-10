using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections;

using SyncroC.Controls.Colors;

namespace SyncroC.Controls
{
	/// <summary>
	/// Encapsulates information about to dispose text into the component
	/// </summary>
	public struct TextLayout
	{
		private Rectangle[] rectangles;
		private string[] strings;

    public TextLayout(Rectangle[] rectangles, string[] strings)
		{
			this.rectangles = rectangles;
			this.strings = strings;
		}

		/// <summary>
		/// Number of elements present into the structure.
		/// </summary>
		public int Count
		{
			get { return Math.Min(this.rectangles.Length, this.strings.Length); }
		}

		/// <summary>
		/// Areas where to put text.
		/// i th area is where to draw the i th string.
		/// </summary>
		public Rectangle[] Rectangles
		{
			get { return this.rectangles; }
			set { this.rectangles = value; }
		}

		/// <summary>
		/// Strings to be written.
		/// i th string is to be drawn into the i th area.
		/// </summary>
		public string[] Strings
		{
			get { return this.strings; }
			set { this.strings = value; }
		}
	}

	/// <summary>
	/// Base class to be extended when a new control for the Syncro is being developed.
	/// </summary>
	public class SyncroUserControl : UserControl
	{
		// Required designer variable.
		private System.ComponentModel.IContainer components = null;

    /// <summary>
		/// Color to be used by inheritors to draw the focus feedback
		/// </summary>
		protected readonly Color FOCUS_COLOR = ColConv.Convert(SyncroColor.Green);

		/// <summary>Font height factor</summary>
		private double FONT_HEIGHT_FACTOR = 0.90;

		private bool translateText = false;
		/// <summary>
		/// Gets or sets a value determining whether the text property 
		/// of the current instance has to be translated.
		/// </summary>
		[Category("Language Translation"), Description("Gets or sets a value determining whether the text property of the current instance has to be translated.")]
		public bool TranslateText
		{
			get { return translateText; }
			set { translateText = value; }
		}

		public SyncroUserControl()
		{
			InitializeComponent();
    }

    /// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary> 
		/// Must be overridden by inheritors to show an error
		/// </summary>
		public virtual void SetError(string error)
		{
		}

		/// <summary>
		/// Calculates how to split the text to be written into the control and the areas
		/// where it should be written to obtain all the text centered and properly visible.
		/// </summary>
		/// <param name="writableArea">Control's area where the text can be written.</param>
		/// <param name="text">text that has to be written into the control.</param>
		/// <param name="font">Font to be used for calculations, it is supposed to be the same that will be used by the component to draw the text.</param>
		/// <returns>TextLayout structure containing the splitted strings and areas where to write them</returns>
		protected TextLayout CalculateTextLayout(Rectangle writableArea, string text, Font font)
		{
			Graphics g = this.CreateGraphics();

			int textHeight = (int)(g.MeasureString(text, font).Height * FONT_HEIGHT_FACTOR);	//vertical space needed for each line
			ArrayList linesText = new ArrayList(1);	//lines to split the text string
			int lines = 1; //lines counter
			string[] textWords = text.Split(new char[] { ' ' }); //text splitted word by word
			ArrayList linesWidth = new ArrayList(1);	//space needed by each line to be drawn
			int wordCount = 1;//sequence number of the current word into the line

			//first line
			linesWidth.Add(0);
			linesText.Add(string.Empty);

			for (int i = 0; i < textWords.Length; i++)
			{
				//int wordWidth = (int)g.MeasureString((wordCount == 1) ? "" : " " + textWords[i], font).Width;
				if (wordCount != 1)
				{
					if (g.MeasureString(
						(string)linesText[lines - 1] + 
						((wordCount == 1) ? "" : " ") + 
						textWords[i], font).Width >= writableArea.Width)
					{	//new line needed
						lines++;
						linesWidth.Add(0);
						linesText.Add(string.Empty);
						wordCount = 1;
					}
					else
					{	//no new line needed so a separator is needed
						linesText[lines - 1] = (string)linesText[lines - 1] + " ";
					}
				}
				linesText[lines - 1] = (string)linesText[lines - 1] + textWords[i];
				linesWidth[lines - 1] = Convert.ToInt32(Math.Ceiling(g.MeasureString((string)linesText[lines - 1], font).Width));
				wordCount++;
			}

			int verticalOffset = Convert.ToInt32((writableArea.Height - (lines * textHeight)) / 2); //vertical space between the writable area and the first line of text

			Rectangle[] rectangles = new Rectangle[lines];//areas where to write calculated lines
			String[] strings = new String[linesText.Count]; //lines 

			for (int i = 0; i < lines; i++)
			{
				rectangles[i] = new Rectangle(
					Convert.ToInt32(writableArea.X + ((writableArea.Width - (int)linesWidth[i]) / 2)),
					writableArea.Y + verticalOffset + textHeight * i,
					(int)linesWidth[i],
					textHeight);
				strings[i] = (string)linesText[i];
			}
				
			return new TextLayout(rectangles, strings);
		}

		/// <summary>
		/// Implementing classes must define in this method the internal resizing logic. 
		/// </summary>
		/// <param name="VerticalResizing">coefficient for vertical resizing.</param>
		/// <param name="OrizontalResizing">coefficient for orizontal resizing.</param>
		/// <param name="FontResizing">coefficient for font resizing.</param>
		public virtual void AdaptToScreen(double VerticalResizing, double OrizontalResizing, float FontResizing)
		{ 
		}

		protected void AdaptStringForComponentSize(Graphics g, Control c, int width, Font f)
		{
			if (! this.DesignMode)
			{
				bool first = true;
				while (g.MeasureString(c.Text, f).Width > width)
				{
					if (first)
					{
						c.Text += "..";
						first = false;
					}
					if (c.Text.Length > 3)
						c.Text = c.Text.Remove(c.Text.Length - 3, 1);
					else
						break;
				}
			}
		}
	}
}
