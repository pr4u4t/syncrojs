using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using SyncroC.Controls.Interfaces;
using SyncroC.Utility;

namespace SyncroC.Controls.IncDecButton
{
	/// <summary>
	/// Kinds of buttons present on the IncDecButtonKit.
	/// </summary>
	public enum IncDecButtonType
	{
		Increase,
		Decrease
	}

	/// <summary>
	/// Positions of the Inc/Dec buttons into the Kit
	/// </summary>
	public enum IncDecButtonPositionType
	{
		Horizontal,
		Vertical
	}

	/// <summary>
	/// Delegate to use for OnIncDecButtonKitDown.
	/// </summary>
	public delegate void IncDecButtonKitDownHandler(object sender, IncDecButtonKitDownEventArgs args);

	/// <summary>
	/// Summary description for IncDecButtonKit.
	/// </summary>
	public class IncDecButtonKit : SyncroC.Controls.SyncroUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private SyncroC.Controls.IncDecButton.IncDecButton IncreaseButton;
		private SyncroC.Controls.IncDecButton.IncDecButton DecreaseButton;
		//private System.Windows.Forms.Label lbl_Value;
		private bool lowLevelEnabled;

		[Description("Gets or sets whether the unit measure is visible."), Category("FosberSoft"), DefaultValue(true)]
		public bool UnitMeasureVisible
		{
			get { return this.tnud_Value.UnitMeasureVisible; }
			set
			{
				this.tnud_Value.UnitMeasureVisible = value;
			}
		}

		/// <summary>
		/// Gets or sets the unit measure displayed by the component. 
		/// </summary>
		[Description("Gets or sets the unit measure displayed by the component."), Category("FosberSoft")]
		public UnitMeasure UnitMeasure
		{
			get { return this.tnud_Value.UnitMeasure; }
			set
			{
				this.tnud_Value.UnitMeasure = value;
			}
		}

		/// <summary>
		/// Gets or sets the unit measure internally used by the component.
		/// </summary>
		[Description("Gets or sets the unit measure internally used by the component."), Category("FosberSoft")]
		public UnitMeasure InternalUnitMeasure
		{
			get { return this.tnud_Value.InternalUnitMeasure; }
			set
			{
				this.tnud_Value.InternalUnitMeasure = value;
			}
		}

		/// <summary>
		/// Gets or sets the central label text.
		/// </summary>
		[Description("Gets or sets the central label text."), Category("FosberSoft")]
		public string Value
		{
			get {	return tnud_Value.Value;	}
			set { tnud_Value.Value = value; }
		}

		private bool valueVisible;
		private TitledNumericUpDown tnud_Value;
		/// <summary>
		/// Gets or sets the Value visibility of the titled label.
		/// </summary>
		[Description("Gets or sets the Value visibility of the titled label."), Category("FosberSoft")]
		public bool ValueVisible
		{
			get { return this.valueVisible; }
			set 
			{
				this.valueVisible = value;
				this.tnud_Value.Visible = this.valueVisible;
				if ((this.valueVisible) && (this.tnud_Value.UnitMeasureVisible))
				{
					this.tnud_Value.Font = new Font(this.tnud_Value.Font.Name, (this.tnud_Value.Font.Size - 2.0F), this.tnud_Value.Font.Style, this.tnud_Value.Font.Unit);
				}
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets facility Low level.
		/// </summary>
		[Description("Gets or sets facility Low level."), Category("FosberSoft")]
		public bool LowLevelEnabled
		{
			get { return this.lowLevelEnabled; }
			set
			{
				this.lowLevelEnabled = value;
				this.IncreaseButton.LowLevelEnabled = value;
				this.DecreaseButton.LowLevelEnabled = value;
			}
		}

		public IncDecButtonPositionType position = IncDecButtonPositionType.Vertical;
		/// <summary>
		/// Gets or sets the ReadOnly property.
		/// </summary>
		[Description("Gets or sets the ReadOnly property."), Category("FosberSoft")]
		public IncDecButtonPositionType Position
		{
			get {	return position;	}
			set
			{ 
				position = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Event raised every interval time when one of the button is down.
		/// </summary>
		[Description("Event raised every interval time when one of the button is down."), Category("Fosbersoft")]
		public event IncDecButtonKitDownHandler OnIncDecButtonKitDown; 

		public IncDecButtonKit()
		{
			InitializeComponent();
			this.tnud_Value.Minimum = Decimal.MinValue;
			this.tnud_Value.Maximum = Decimal.MaxValue;
			this.lowLevelEnabled = true;
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

		private void AnyComponent_Enter(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void AnyComponent_Leave(object sender, EventArgs e)
		{
			Invalidate();
		}


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.IncreaseButton = new SyncroC.Controls.IncDecButton.IncDecButton();
			this.DecreaseButton = new SyncroC.Controls.IncDecButton.IncDecButton();
			this.tnud_Value = new SyncroC.Controls.TitledNumericUpDown();
			this.SuspendLayout();
			// 
			// IncreaseButton
			// 
			this.IncreaseButton.BackColor = System.Drawing.Color.WhiteSmoke;
			this.IncreaseButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.IncreaseButton.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.IncreaseButton.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.UpArrow;
			this.IncreaseButton.Location = new System.Drawing.Point(4, 4);
			this.IncreaseButton.LowLevelEnabled = true;
			this.IncreaseButton.Name = "IncreaseButton";
			this.IncreaseButton.Size = new System.Drawing.Size(104, 88);
			this.IncreaseButton.TabIndex = 2;
			this.IncreaseButton.TranslateText = false;
			this.IncreaseButton.Leave += new System.EventHandler(this.AnyComponent_Leave);
			this.IncreaseButton.Enter += new System.EventHandler(this.AnyComponent_Enter);
			this.IncreaseButton.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.IncreaseButton_OnIncDecButtonDown);
			// 
			// DecreaseButton
			// 
			this.DecreaseButton.BackColor = System.Drawing.Color.WhiteSmoke;
			this.DecreaseButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DecreaseButton.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DecreaseButton.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.DownArrow;
			this.DecreaseButton.Location = new System.Drawing.Point(0, 144);
			this.DecreaseButton.LowLevelEnabled = true;
			this.DecreaseButton.Name = "DecreaseButton";
			this.DecreaseButton.Size = new System.Drawing.Size(104, 88);
			this.DecreaseButton.TabIndex = 3;
			this.DecreaseButton.TranslateText = false;
			this.DecreaseButton.Leave += new System.EventHandler(this.AnyComponent_Leave);
			this.DecreaseButton.Enter += new System.EventHandler(this.AnyComponent_Enter);
			this.DecreaseButton.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.DecreaseButton_OnIncDecButtonDown);
			// 
			// tnud_Value
			// 
			this.tnud_Value.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tnud_Value.DecimalPlaces = 0;
			this.tnud_Value.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tnud_Value.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.tnud_Value.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.tnud_Value.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
			this.tnud_Value.Location = new System.Drawing.Point(0, 96);
			this.tnud_Value.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.tnud_Value.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
			this.tnud_Value.Name = "tnud_Value";
			this.tnud_Value.ReadOnly = true;
			this.tnud_Value.Size = new System.Drawing.Size(104, 40);
			this.tnud_Value.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
			this.tnud_Value.TabIndex = 4;
			this.tnud_Value.Text = "titledNumericUpDown1";
			this.tnud_Value.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.tnud_Value.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.tnud_Value.TranslateText = false;
			this.tnud_Value.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
			this.tnud_Value.Value = "";
			this.tnud_Value.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			// 
			// IncDecButtonKit
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.tnud_Value);
			this.Controls.Add(this.DecreaseButton);
			this.Controls.Add(this.IncreaseButton);
			this.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "IncDecButtonKit";
			this.Size = new System.Drawing.Size(112, 240);
			this.Leave += new System.EventHandler(this.AnyComponent_Leave);
			this.Enter += new System.EventHandler(this.AnyComponent_Enter);
			this.SizeChanged += new System.EventHandler(this.IncDecButtonKit_SizeChanged);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Change the visual feedback of the Increase button according to the parameter.
		/// </summary>
		/// <param name="state">new state the Increase button has to show.</param>
		public void Increase(bool state)
		{
			IncreaseButton._signal(state);
		}

		/// <summary>
		/// Change the visual feedback of the Decrease button according to the parameter.
		/// </summary>
		/// <param name="state">new state the Decrease button has to show.</param>
		public void Decrease(bool state)
		{
			DecreaseButton._signal(state);
		}

		/// <summary>
		/// Raise an OnIncDecButtonKitDown event for Increase. 
		/// </summary>
		private void IncreaseButton_OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
		{
			if (!this.lowLevelEnabled)
				return;

			if (OnIncDecButtonKitDown != null)
			{
				OnIncDecButtonKitDown(this, new IncDecButtonKitDownEventArgs(args.Frequency, IncDecButtonType.Increase));
			}
		}

		/// <summary>
		/// Raise an OnIncDecButtonKitDown event for Decrease. 
		/// </summary>
		private void DecreaseButton_OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
		{
			if (!this.lowLevelEnabled)
				return;

			if (OnIncDecButtonKitDown != null)
			{
				OnIncDecButtonKitDown(this, new IncDecButtonKitDownEventArgs(args.Frequency, IncDecButtonType.Decrease));
			}
		}	

		/// <summary>
		/// Keep proportional dimensions at design time and runtime of the inside components
		/// </summary>
		private void IncDecButtonKit_SizeChanged(object sender, System.EventArgs e)
		{
			ArrangeComponents();
		}

		private void ArrangeComponents()
		{
			if (position.Equals(IncDecButtonPositionType.Vertical))
			{//Vertical, value visible
				//width setting
				int insideComponentsWidth = this.Size.Width;
				IncreaseButton.Width = insideComponentsWidth;
				tnud_Value.Width =	insideComponentsWidth;
				DecreaseButton.Width = insideComponentsWidth;
				//height setting
				int insideComponentsTotalHeight = this.Size.Height;
				if (ValueVisible)
				{
					IncreaseButton.Height = Convert.ToInt32(insideComponentsTotalHeight * 0.39);
					tnud_Value.Height = Convert.ToInt32(insideComponentsTotalHeight * 0.19);
					DecreaseButton.Height = Convert.ToInt32(insideComponentsTotalHeight * 0.39);

					int spacing = Convert.ToInt32((insideComponentsTotalHeight
						- IncreaseButton.Height - tnud_Value.Height - DecreaseButton.Height) / 2);

					IncreaseButton.Location = new Point(
						0,
						0);
					tnud_Value.Location = new Point(
						0,
						IncreaseButton.Location.Y + IncreaseButton.Height + spacing);
					DecreaseButton.Location = new Point(
						0,
						tnud_Value.Location.Y + tnud_Value.Height + spacing);
				}
				else //Vertical, value NOT visible
				{
					IncreaseButton.Height = Convert.ToInt32(insideComponentsTotalHeight * 0.48);
					DecreaseButton.Height = Convert.ToInt32(insideComponentsTotalHeight * 0.48);

					IncreaseButton.Location = new Point(
						0,
						0);
					DecreaseButton.Location = new Point(
						0,
						this.Size.Height - DecreaseButton.Height);
				}
			}
			else //Horizontal, value visible
			{
				IncreaseButton.Height = this.Size.Height;
				tnud_Value.Height = Convert.ToInt32(this.Size.Height * 0.80);
				DecreaseButton.Height = this.Size.Height;
				int insideComponentsTotalWidth = this.Size.Width;
				if (ValueVisible)
				{
					IncreaseButton.Width = Convert.ToInt32(insideComponentsTotalWidth * 0.32);
					tnud_Value.Width = Convert.ToInt32(insideComponentsTotalWidth * 0.32);
					DecreaseButton.Width = Convert.ToInt32(insideComponentsTotalWidth * 0.32);

					int spacing = Convert.ToInt32((insideComponentsTotalWidth
						- IncreaseButton.Width - tnud_Value.Width - DecreaseButton.Width) / 2);

					IncreaseButton.Location = new Point(
						0,
						0);
					tnud_Value.Location = new Point(
						IncreaseButton.Location.X + IncreaseButton.Width + spacing,
						Convert.ToInt32((this.Height - tnud_Value.Height) / 2));
					DecreaseButton.Location = new Point(
						tnud_Value.Location.X + tnud_Value.Width + spacing,
						0);
				}
				else //Horizontal, value NOT visible
				{
					IncreaseButton.Width = Convert.ToInt32(insideComponentsTotalWidth * 0.48);
					DecreaseButton.Width = Convert.ToInt32(insideComponentsTotalWidth * 0.48);

					IncreaseButton.Location = new Point(
						0,
						0);
					DecreaseButton.Location = new Point(
						this.Size.Width - DecreaseButton.Width,
						0);
				}
			}
		}

		/// <summary>
		/// Simulate the MouseUp event of the Increase button.
		/// </summary>
		public void PerformIncreaseMouseUp()
		{
			IncreaseButton.PerformMouseUp();
		}

		/// <summary>
		/// Simulate the MouseUp event of the Decrease button.
		/// </summary>
		public void PerformDecreaseMouseUp()
		{
			DecreaseButton.PerformMouseUp();
		}

		/// <summary>
		/// Simulate the MouseDown event of the Increase button.
		/// </summary>
		public void PerformIncreaseMouseDown()
		{
			IncreaseButton.PerformMouseDown();
		}

		/// <summary>
		/// Simulate the MouseDown event of the Decrease button.
		/// </summary>
		public void PerformDecreaseMouseDown()
		{
			DecreaseButton.PerformMouseDown();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
 			 base.OnPaint(e);
			 ArrangeComponents();
		}
		
	}
}
