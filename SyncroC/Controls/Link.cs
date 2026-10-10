using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

using SyncroC.Controls.Interfaces;

namespace SyncroC.Controls
{
	/// <summary>
	/// Styles of the link, determine its shape its positioning.
	/// </summary>
	public enum LinkStyle : int
	{
		/// <summary>the link has a corner up pointing.</summary>
		Over,
		/// <summary>the link has no corner and it is positioned obliquely.</summary>
		Oblique,
		/// <summary>the link has a corner down pointing.</summary>
		Under
	}
	
	/// <summary>
	/// Mode of the link, determine which points has to be linked (always from left to right)
	/// </summary>
	public enum LinkMode : int
	{
		/// <summary>hight-left corner will be linked to the down-right corner.</summary>
		TopDown,
		/// <summary>down-left corner will be linked to the hight-right corner.</summary>
		DownTop
	}

	/// <summary>
	/// Implements a simple graphical link to connect different parts into a page.
	/// </summary>
	public class Link : SyncroC.Controls.SyncroUserControl
	{
		/// <summary>Determine the width of the link.</summary>
		private const int LINK_WIDTH = 2; 
		
		// Required designer variable.
		private System.ComponentModel.Container components = null;

		/// <summary>current style of the link.</summary>
		private LinkStyle style = LinkStyle.Oblique;
		/// <summary>
		/// Gets or sets the style of the link.
		/// </summary>
		[Description("Gets or sets the style of the link."), Category("FosberSoft")]
		public LinkStyle Style
		{
			get {	return style;	}
			set 
			{	
				style = value;
				SetRegion();
			}
		}

		/// <summary>current mode of the link.</summary>
		private LinkMode mode = LinkMode.TopDown;
		/// <summary>
		/// Gets or sets the mode of the link.
		/// </summary>
		[Description("Gets or sets the mode of the link."), Category("FosberSoft")]
		public LinkMode Mode
		{
			get {	return mode;	}
			set 
			{	
				mode = value;
				SetRegion();
			}
		}

		/// <summary>Determine the location from where the link departs.</summary>
		private Point startPoint;
		/// <summary>Determine a "close and proper" location from where the link departs.</summary>
		private Point startPointNext;
		/// <summary>Determine the location where the link has a corner.</summary>
		private Point cornerPoint;
		/// <summary>Determine a "close and proper" location where the link has a corner.</summary>
		private Point cornerPointNext;
		/// <summary>Determine the location where the link ends.</summary>
		private Point endPoint;
		/// <summary>Determine a "close and proper" location where the link ends.</summary>
		private Point endPointNext;
		
		/// <summary>
		/// Builder.
		/// </summary>
		public Link()
		{
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Link
			// 
			this.BackColor = System.Drawing.Color.Black;
			this.Name = "Link";
			this.Load += new System.EventHandler(this.Link_Load);
			this.SizeChanged += new System.EventHandler(this.Link_SizeChanged);

		}
		#endregion

		/// <summary>
		/// Set the region accordingly to the current style and mode.
		/// </summary>
		private void SetRegion()
		{
			//calculation of the region corners
			switch (style)
			{
				case LinkStyle.Over:
					if (LinkMode.TopDown.Equals(mode))
					{/*
					  * LinkStyle.Over
					  * LinkMode.TopDown
					  */
						startPoint = new Point(
							0,//x
							0);//y
						startPointNext = new Point(
							0, //x
							LINK_WIDTH);//y
						cornerPoint = new Point(
							this.Width,//x
							0);//y
						cornerPointNext = new Point(
							this.Width - LINK_WIDTH, //x
							LINK_WIDTH);//y
						endPoint = new Point(
							this.Width, //x
							this.Height);//y
						endPointNext = new Point(
							this.Width - LINK_WIDTH,//x
							this.Height);//y
					}
					else
					{/*
					  * LinkStyle.Over
					  * LinkMode.DownTop
					  */
						startPoint = new Point(
							0,//x
							this.Height);//y
						startPointNext = new Point(
							LINK_WIDTH, //x
							this.Height);//y
						cornerPoint = new Point(
							0,//x
							0);//y
						cornerPointNext = new Point(
							LINK_WIDTH, //x
							LINK_WIDTH);//y
						endPoint = new Point(
							this.Width, //x
							0);//y
						endPointNext = new Point(
							this.Width,//x
							LINK_WIDTH);//y
					}
					break;
				case LinkStyle.Oblique:
					if (LinkMode.TopDown.Equals(mode))
					{/*
					  * LinkStyle.Oblique
					  * LinkMode.TopDown
					  */ 
						startPoint = new Point(
							0,//x
							0);//y
						startPointNext = new Point(
							0, //x
							LINK_WIDTH);//y
						cornerPoint = 
							startPoint;
						cornerPointNext =
							startPointNext;
						endPoint = new Point(
							this.Width, //x
							this.Height - LINK_WIDTH);//y
						endPointNext = new Point(
							this.Width,//x
							this.Height);//y
					}
					else
					{/*
					  * LinkStyle.Oblique
					  * LinkMode.DownTop
					  */
						startPoint = new Point(
							0,//x
							this.Height);//y
						startPointNext = new Point(
							0, //x
							this.Height - LINK_WIDTH);//y
						cornerPoint = 
							startPoint;
						cornerPointNext = 
							startPointNext;
						endPoint = new Point(
							this.Width, //x
							LINK_WIDTH);//y
						endPointNext = new Point(
							this.Width,//x
							0);//y
					}
					break;
				case LinkStyle.Under:
					if (LinkMode.TopDown.Equals(mode))
					{/*
					  * LinkStyle.Under
					  * LinkMode.TopDown
					  */ 
						startPoint = new Point(
							0,//x
							0);//y
						startPointNext = new Point(
							LINK_WIDTH, //x
							0);//y
						cornerPoint = new Point(
							0,//x
							this.Height);//y
						cornerPointNext = new Point(
							LINK_WIDTH, //x
							this.Height - LINK_WIDTH);//y
						endPoint = new Point(
							this.Width, //x
							this.Height);//y
						endPointNext = new Point(
							this.Width,//x
							this.Height - LINK_WIDTH);//y
					}
					else
					{/*
					  * LinkStyle.Under
					  * LinkMode.DownTop
					  */ 
						startPoint = new Point(
							0,//x
							this.Height);//y
						startPointNext = new Point(
							0, //x
							this.Height - LINK_WIDTH);//y
						cornerPoint = new Point(
							this.Width,//x
							this.Height);//y
						cornerPointNext = new Point(
							this.Width - LINK_WIDTH, //x
							this.Height - LINK_WIDTH);//y
						endPoint = new Point(
							this.Width, //x
							0);//y
						endPointNext = new Point(
							this.Width - LINK_WIDTH,//x
							0);//y
					}
					break;
			}
			//region building and assignement
			GraphicsPath gp = new GraphicsPath();
			gp.AddPolygon(new Point[]{
										 startPoint,
										 cornerPoint,
										 endPoint,
										 endPointNext,
										 cornerPointNext,
										 startPointNext});
			this.Region = new Region(gp);
		}

		private void Link_Load(object sender, System.EventArgs e)
		{
			SetRegion();
		}

		private void Link_SizeChanged(object sender, System.EventArgs e)
		{
			SetRegion();
		}
	}
}
