using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using SyncroC.Controls.Interfaces;

namespace SyncroC.Controls.Photoeye
{
	/// <summary>
	/// Delegate to use for OnBusyChanged event handling.
	/// </summary>
	public delegate void BusyChangedHandler(object sender, BusyChangedEventArgs args);

	/// <summary>
	/// Photoeye.
	/// </summary>
	public class Photoeye : SyncroC.Controls.SyncroUserControl
	{
		/// <summary>
		/// Event raised when the Busy property change value.
		/// </summary>
		[Description("Event raised when the Busy property change value."), Category("Fosbersoft")]
		public event BusyChangedHandler OnBusyChanged; 

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>Color assumed by the control when it is busy.</summary>
		private Color busyColor = Color.Blue; 
		/// <summary>Color assumed by the control when it is free.</summary>
		private Color freeColor = Color.Green;

		/// <summary>Current state of the photoeye.</summary>
		private bool busy = false;
		/// <summary>
		/// Gets or sets the state of the photoeye.
		/// </summary>
		[Description("Gets or sets the state of the photoeye."), Category("FosberSoft")]
		public bool Busy
		{
			get {return busy;}
			set 
			{
				busy = value;
				Invalidate();
				if (OnBusyChanged != null)
					OnBusyChanged(this, new BusyChangedEventArgs(busy));
			}
		}

		public Photoeye()
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
			// Photoeye
			// 
			this.Name = "Photoeye";
			this.Size = new System.Drawing.Size(48, 48);

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			BackColor = (busy) ? busyColor : freeColor;
		}
	}
}
