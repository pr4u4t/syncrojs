using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase;
using SyncroC.Utility;
using SyncroC.Network;

using SyncroC.Forms.PopUp; //PMASSARO_FI25/05/10 - Aggiunta conferma su comando "giunta ora"

namespace SyncroC.Forms.Pages.Production.Home
{
	public class Frm_HomeWet : SyncroC.Forms.Pages.Production.Home.Frm_HomeSubGroup
	{
		private SyncroC.Controls.Title ttl_Syncronization;
		
		private SyncroC.Controls.StateButton SyncIdle;
		private SyncroC.Controls.StateButton SyncOnMetres;
		private SyncroC.Controls.StateButton SyncNow;
		private SyncroC.Controls.StateButton SyncOnSplicer1;
		private SyncroC.Controls.StateButton SyncOnSplicer2;
		private SyncroC.Controls.StateButton SyncOnSplicer3;
		private SyncroC.Controls.StateButton SyncOnSplicer4;
		private SyncroC.Controls.StateButton SyncOnSplicer5;
		private System.ComponentModel.IContainer components = null;
		private SyncroC.Controls.Title ttl_Splicer1;
		private SyncroC.Controls.CheckBox chk_S1Ready;
		private SyncroC.Controls.CheckBox chk_S2Ready;
		private SyncroC.Controls.Title ttl_Splicer2;
		private SyncroC.Controls.CheckBox chk_S3Ready;
		private SyncroC.Controls.Title ttl_Splicer3;
		private SyncroC.Controls.CheckBox chk_S4Ready;
		private SyncroC.Controls.Title ttl_Splicer4;
		private SyncroC.Controls.Title ttl_Splicer5;
		private SyncroC.Controls.TitledLabel ttl_CurrentS1;
		private SyncroC.Controls.TitledLabel ttl_CurrentS2;
		private SyncroC.Controls.TitledLabel ttl_CurrentS3;
		private SyncroC.Controls.TitledLabel ttl_CurrentS4;
		private SyncroC.Controls.TitledLabel ttl_CurrentS5;
		private SyncroC.Controls.TitledLabel ttl_NextS5;
		private SyncroC.Controls.TitledLabel ttl_NextS4;
		private SyncroC.Controls.TitledLabel ttl_NextS3;
		private SyncroC.Controls.TitledLabel ttl_NextS2;
		private SyncroC.Controls.TitledLabel ttl_NextS1;
		private SyncroC.Controls.StateButton ToggleAutoManS5;
		private SyncroC.Controls.StateButton ToggleAutoManS4;
		private SyncroC.Controls.StateButton ToggleAutoManS3;
		private SyncroC.Controls.StateButton ToggleAutoManS2;
		private SyncroC.Controls.StateButton ToggleAutoManS1;

		CommunicationManager cm;
		private SyncroC.Controls.TitledNumericUpDown tnud_WetEndMetersProduced;
		private SyncroC.Controls.TitledMaskTextBox tmt_WetEndStartTime;
		private SyncroC.Controls.Title ttl_WetEnd;
		private SyncroC.Controls.StateButton stb_SpliceConfirmedS1;
		private SyncroC.Controls.StateButton stb_SpliceConfirmedS2;
		private SyncroC.Controls.StateButton stb_SpliceConfirmedS3;
		private SyncroC.Controls.StateButton stb_SpliceConfirmedS4;
		private SyncroC.Controls.StateButton stb_SpliceConfirmedS5;
		private SyncroC.Controls.TitledNumericUpDown tnud_WetEndMetersRequired;
		private SyncroC.Controls.TitledLabel ttl_StopTime;
		private Label lbl_ShiftID;
		private SyncroC.Controls.TitledMaskTextBox tmt_ShiftStartTime;
		private SyncroC.Controls.Title title2;
		private SyncroC.Controls.TitledNumericUpDown tnud_RemainingProductionS5;
		private SyncroC.Controls.TitledNumericUpDown tnud_RemainingProductionS4;
		private SyncroC.Controls.TitledNumericUpDown tnud_RemainingProductionS3;
		private SyncroC.Controls.TitledNumericUpDown tnud_RemainingProductionS2;
		private SyncroC.Controls.TitledNumericUpDown tnud_RemainingProductionS1;
		private SyncroC.Controls.TitledLabel tlb_WetEndNote;
		private SyncroC.Controls.TitledNumericUpDown tlb_ShiftMetresProduced;
    private SyncroC.Controls.StateButton ToggleAutoManS7;
    private SyncroC.Controls.StateButton ToggleAutoManS6;
    private SyncroC.Controls.CheckBox chk_S6Ready;
    private SyncroC.Controls.CheckBox chk_S7Ready;
    private SyncroC.Controls.StateButton stb_SpliceConfirmedS6;
    private SyncroC.Controls.StateButton stb_SpliceConfirmedS7;
    private SyncroC.Controls.Title ttl_Splicer6;
    private SyncroC.Controls.Title ttl_Splicer7;
    private SyncroC.Controls.TitledLabel ttl_CurrentS7;
    private SyncroC.Controls.TitledLabel ttl_CurrentS6;
    private SyncroC.Controls.TitledLabel ttl_NextS7;
    private SyncroC.Controls.TitledLabel ttl_NextS6;
    private SyncroC.Controls.StateButton SyncOnSplicer7;
    private SyncroC.Controls.StateButton SyncOnSplicer6;
    private SyncroC.Controls.TitledNumericUpDown tnud_RemainingProductionS7;
    private SyncroC.Controls.TitledNumericUpDown tnud_RemainingProductionS6;
		private SyncroC.Controls.CheckBox chk_S5Ready;

		public Frm_HomeWet()
		{
			InitializeComponent();
			cm = CommunicationManager.GetCommunicationManager();

			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
			{
				this.SyncIdle.IsButton = false;
				this.SyncNow.IsButton = false;
				this.SyncOnMetres.IsButton = false;
				this.SyncOnSplicer1.IsButton = false;
				this.SyncOnSplicer2.IsButton = false;
				this.SyncOnSplicer3.IsButton = false;
				this.SyncOnSplicer4.IsButton = false;
				this.SyncOnSplicer5.IsButton = false;

				this.stb_SpliceConfirmedS1.IsButton = false;
				this.stb_SpliceConfirmedS2.IsButton = false;
				this.stb_SpliceConfirmedS3.IsButton = false;
				this.stb_SpliceConfirmedS4.IsButton = false;
				this.stb_SpliceConfirmedS5.IsButton = false;

				this.ToggleAutoManS1.IsButton = false;
				this.ToggleAutoManS2.IsButton = false;
				this.ToggleAutoManS3.IsButton = false;
				this.ToggleAutoManS4.IsButton = false;
				this.ToggleAutoManS5.IsButton = false;

        //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
        this.SyncOnSplicer6.IsButton = false;
        this.SyncOnSplicer7.IsButton = false;
        this.stb_SpliceConfirmedS6.IsButton = false;
        this.stb_SpliceConfirmedS7.IsButton = false;
        this.ToggleAutoManS6.IsButton = false;
        this.ToggleAutoManS7.IsButton = false;
      }


      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      if (!DBManager.getDBManager().lineConfig.SingleFacer3)
      {
        this.chk_S6Ready.Enabled = this.chk_S7Ready.Enabled = false;
        this.ToggleAutoManS6.Enabled = this.ToggleAutoManS7.Enabled = false;
        this.stb_SpliceConfirmedS6.Enabled = this.stb_SpliceConfirmedS7.Enabled = false;

        this.chk_S6Ready.Visible = this.chk_S7Ready.Visible = false;

        this.ToggleAutoManS6.Visible = this.ToggleAutoManS7.Visible = false;

        this.stb_SpliceConfirmedS6.Visible = this.stb_SpliceConfirmedS7.Visible = false;

        this.ttl_CurrentS6.Visible = this.ttl_CurrentS7.Visible = false;

        this.ttl_NextS6.Visible = this.ttl_NextS7.Visible = false;

        this.ttl_Splicer6.Visible = this.ttl_Splicer7.Visible = false;

        this.SyncOnSplicer6.Visible = this.SyncOnSplicer7.Visible = false;

        this.tnud_RemainingProductionS6.Visible = this.tnud_RemainingProductionS7.Visible = false;

        //Ridisegno la pagina come prima che prevedeva solo due zone
        this.ttl_Splicer1.Width = this.ttl_Splicer2.Width = this.ttl_Splicer3.Width = this.ttl_Splicer4.Width = this.ttl_Splicer5.Width = 141;
        this.chk_S1Ready.Width = this.chk_S2Ready.Width = this.chk_S3Ready.Width = this.chk_S4Ready.Width = this.chk_S5Ready.Width = 134;
        this.ToggleAutoManS1.Width = this.ToggleAutoManS2.Width = this.ToggleAutoManS3.Width = this.ToggleAutoManS4.Width = this.ToggleAutoManS5.Width = 134;
        this.stb_SpliceConfirmedS1.Width = this.stb_SpliceConfirmedS2.Width = this.stb_SpliceConfirmedS3.Width = this.stb_SpliceConfirmedS4.Width = this.stb_SpliceConfirmedS5.Width = 134;
        this.ttl_CurrentS1.Width = this.ttl_CurrentS2.Width = this.ttl_CurrentS3.Width = this.ttl_CurrentS4.Width = this.ttl_CurrentS5.Width = 134;
        this.ttl_NextS1.Width = this.ttl_NextS2.Width = this.ttl_NextS3.Width = this.ttl_NextS4.Width = this.ttl_NextS5.Width = 134;
        this.SyncOnSplicer1.Width = this.SyncOnSplicer2.Width = this.SyncOnSplicer3.Width = this.SyncOnSplicer4.Width = this.SyncOnSplicer5.Width = 120;
        this.tnud_RemainingProductionS1.Width = this.tnud_RemainingProductionS2.Width = this.tnud_RemainingProductionS3.Width = this.tnud_RemainingProductionS4.Width = this.tnud_RemainingProductionS5.Width = 115;

        this.ttl_Splicer1.Location = new Point(1060, this.ttl_Splicer1.Location.Y);
        this.ttl_Splicer2.Location = new Point(ttl_Splicer1.Location.X - 155, this.ttl_Splicer1.Location.Y);
        this.ttl_Splicer3.Location = new Point(ttl_Splicer2.Location.X - 155, this.ttl_Splicer1.Location.Y);
        this.ttl_Splicer4.Location = new Point(ttl_Splicer3.Location.X - 155, this.ttl_Splicer1.Location.Y);
        this.ttl_Splicer5.Location = new Point(ttl_Splicer4.Location.X - 155, this.ttl_Splicer1.Location.Y);

        this.chk_S1Ready.Location = new Point(1067, this.chk_S1Ready.Location.Y);
        this.chk_S2Ready.Location = new Point(this.chk_S1Ready.Location.X - 155, this.chk_S1Ready.Location.Y);
        this.chk_S3Ready.Location = new Point(this.chk_S2Ready.Location.X - 155, this.chk_S1Ready.Location.Y);
        this.chk_S4Ready.Location = new Point(this.chk_S3Ready.Location.X - 155, this.chk_S1Ready.Location.Y);
        this.chk_S5Ready.Location = new Point(this.chk_S4Ready.Location.X - 155, this.chk_S1Ready.Location.Y);

        this.ToggleAutoManS1.Location = new Point(1067, this.ToggleAutoManS1.Location.Y);
        this.ToggleAutoManS2.Location = new Point(this.ToggleAutoManS1.Location.X - 155, this.ToggleAutoManS1.Location.Y);
        this.ToggleAutoManS3.Location = new Point(this.ToggleAutoManS2.Location.X - 155, this.ToggleAutoManS1.Location.Y);
        this.ToggleAutoManS4.Location = new Point(this.ToggleAutoManS3.Location.X - 155, this.ToggleAutoManS1.Location.Y);
        this.ToggleAutoManS5.Location = new Point(this.ToggleAutoManS4.Location.X - 155, this.ToggleAutoManS1.Location.Y);

        this.stb_SpliceConfirmedS1.Location = new Point(1067, this.stb_SpliceConfirmedS1.Location.Y);
        this.stb_SpliceConfirmedS2.Location = new Point(this.stb_SpliceConfirmedS1.Location.X - 155, this.stb_SpliceConfirmedS1.Location.Y);
        this.stb_SpliceConfirmedS3.Location = new Point(this.stb_SpliceConfirmedS2.Location.X - 155, this.stb_SpliceConfirmedS1.Location.Y);
        this.stb_SpliceConfirmedS4.Location = new Point(this.stb_SpliceConfirmedS3.Location.X - 155, this.stb_SpliceConfirmedS1.Location.Y);
        this.stb_SpliceConfirmedS5.Location = new Point(this.stb_SpliceConfirmedS4.Location.X - 155, this.stb_SpliceConfirmedS1.Location.Y);

        this.ttl_CurrentS1.Location = new Point(1067, this.ttl_CurrentS1.Location.Y);
        this.ttl_CurrentS2.Location = new Point(this.ttl_CurrentS1.Location.X - 155, this.ttl_CurrentS1.Location.Y);
        this.ttl_CurrentS3.Location = new Point(this.ttl_CurrentS2.Location.X - 155, this.ttl_CurrentS1.Location.Y);
        this.ttl_CurrentS4.Location = new Point(this.ttl_CurrentS3.Location.X - 155, this.ttl_CurrentS1.Location.Y);
        this.ttl_CurrentS5.Location = new Point(this.ttl_CurrentS4.Location.X - 155, this.ttl_CurrentS1.Location.Y);

        this.ttl_NextS1.Location = new Point(1067, this.ttl_NextS1.Location.Y);
        this.ttl_NextS2.Location = new Point(this.ttl_NextS1.Location.X - 155, this.ttl_NextS1.Location.Y);
        this.ttl_NextS3.Location = new Point(this.ttl_NextS2.Location.X - 155, this.ttl_NextS1.Location.Y);
        this.ttl_NextS4.Location = new Point(this.ttl_NextS3.Location.X - 155, this.ttl_NextS1.Location.Y);
        this.ttl_NextS5.Location = new Point(this.ttl_NextS4.Location.X - 155, this.ttl_NextS1.Location.Y);

        this.SyncOnSplicer1.Location = new Point(1081, this.SyncOnSplicer1.Location.Y);
        this.SyncOnSplicer2.Location = new Point(this.SyncOnSplicer1.Location.X - 155, this.SyncOnSplicer1.Location.Y);
        this.SyncOnSplicer3.Location = new Point(this.SyncOnSplicer2.Location.X - 155, this.SyncOnSplicer1.Location.Y);
        this.SyncOnSplicer4.Location = new Point(this.SyncOnSplicer3.Location.X - 155, this.SyncOnSplicer1.Location.Y);
        this.SyncOnSplicer5.Location = new Point(this.SyncOnSplicer4.Location.X - 155, this.SyncOnSplicer1.Location.Y);

        this.tnud_RemainingProductionS1.Location = new Point(1086, this.tnud_RemainingProductionS1.Location.Y);
        this.tnud_RemainingProductionS2.Location = new Point(this.tnud_RemainingProductionS1.Location.X - 155, this.tnud_RemainingProductionS1.Location.Y);
        this.tnud_RemainingProductionS3.Location = new Point(this.tnud_RemainingProductionS2.Location.X - 155, this.tnud_RemainingProductionS1.Location.Y);
        this.tnud_RemainingProductionS4.Location = new Point(this.tnud_RemainingProductionS3.Location.X - 155, this.tnud_RemainingProductionS1.Location.Y);
        this.tnud_RemainingProductionS5.Location = new Point(this.tnud_RemainingProductionS4.Location.X - 155, this.tnud_RemainingProductionS1.Location.Y);
      }
    }

    public override void _Show()
    {
      base._Show();

      //ABELLO_FI11/02/15_2 Gestita un pò meglio la configurazione con un solo SingleFacer
      if (!DBManager.getDBManager().lineConfig.SingleFacer1)
      {
        this.chk_S2Ready.Enabled = false;
        this.chk_S3Ready.Enabled = false;
        this.ToggleAutoManS2.Enabled = false;
        this.ToggleAutoManS3.Enabled = false;
        this.stb_SpliceConfirmedS2.Enabled = false;
        this.stb_SpliceConfirmedS3.Enabled = false;
      }

      if (!DBManager.getDBManager().lineConfig.SingleFacer2)
      {
        this.chk_S4Ready.Enabled = false;
        this.chk_S5Ready.Enabled = false;
        this.ToggleAutoManS4.Enabled = false;
        this.ToggleAutoManS5.Enabled = false;
        this.stb_SpliceConfirmedS4.Enabled = false;
        this.stb_SpliceConfirmedS5.Enabled = false;
      }

    }

		public override void CorrugatorDirectionLeft()
		{
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      int leftLimit = this.ttl_Splicer5.Location.X;
      if (DBManager.getDBManager().lineConfig.SingleFacer3)
        leftLimit = this.ttl_Splicer7.Location.X;

			int rightLimit = this.ttl_Splicer1.Location.X + this.ttl_Splicer1.Width;
			HorizontalLocationMirror(this.ttl_Splicer1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_Splicer2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_Splicer3, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_Splicer4, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_Splicer5, leftLimit, rightLimit);
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      HorizontalLocationMirror(this.ttl_Splicer6, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ttl_Splicer7, leftLimit, rightLimit);

			leftLimit = this.chk_S5Ready.Location.X;
      if (DBManager.getDBManager().lineConfig.SingleFacer3)
        leftLimit = this.chk_S7Ready.Location.X;
      rightLimit = this.chk_S1Ready.Location.X + this.chk_S1Ready.Width;

			HorizontalLocationMirror(this.chk_S1Ready, leftLimit, rightLimit);
			HorizontalLocationMirror(this.chk_S2Ready, leftLimit, rightLimit);
			HorizontalLocationMirror(this.chk_S3Ready, leftLimit, rightLimit);
			HorizontalLocationMirror(this.chk_S4Ready, leftLimit, rightLimit);
			HorizontalLocationMirror(this.chk_S5Ready, leftLimit, rightLimit);
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      HorizontalLocationMirror(this.chk_S6Ready, leftLimit, rightLimit);
      HorizontalLocationMirror(this.chk_S7Ready, leftLimit, rightLimit);

			HorizontalLocationMirror(this.ToggleAutoManS1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ToggleAutoManS2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ToggleAutoManS3, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ToggleAutoManS4, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ToggleAutoManS5, leftLimit, rightLimit);
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      HorizontalLocationMirror(this.ToggleAutoManS6, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ToggleAutoManS7, leftLimit, rightLimit);

			HorizontalLocationMirror(this.stb_SpliceConfirmedS1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.stb_SpliceConfirmedS2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.stb_SpliceConfirmedS3, leftLimit, rightLimit);
			HorizontalLocationMirror(this.stb_SpliceConfirmedS4, leftLimit, rightLimit);
			HorizontalLocationMirror(this.stb_SpliceConfirmedS5, leftLimit, rightLimit);
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      HorizontalLocationMirror(this.stb_SpliceConfirmedS6, leftLimit, rightLimit);
      HorizontalLocationMirror(this.stb_SpliceConfirmedS7, leftLimit, rightLimit);

			HorizontalLocationMirror(this.ttl_CurrentS1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_CurrentS2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_CurrentS3, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_CurrentS4, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_CurrentS5, leftLimit, rightLimit);
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      HorizontalLocationMirror(this.ttl_CurrentS6, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ttl_CurrentS7, leftLimit, rightLimit);

			HorizontalLocationMirror(this.ttl_NextS1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_NextS2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_NextS3, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_NextS4, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_NextS5, leftLimit, rightLimit);
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      HorizontalLocationMirror(this.ttl_NextS6, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ttl_NextS7, leftLimit, rightLimit);

			HorizontalLocationMirror(this.tnud_RemainingProductionS1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.tnud_RemainingProductionS2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.tnud_RemainingProductionS3, leftLimit, rightLimit);
			HorizontalLocationMirror(this.tnud_RemainingProductionS4, leftLimit, rightLimit);
			HorizontalLocationMirror(this.tnud_RemainingProductionS5, leftLimit, rightLimit);
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      HorizontalLocationMirror(this.tnud_RemainingProductionS6, leftLimit, rightLimit);
      HorizontalLocationMirror(this.tnud_RemainingProductionS7, leftLimit, rightLimit);

			leftLimit = this.SyncOnSplicer5.Location.X;
      if (DBManager.getDBManager().lineConfig.SingleFacer3)
        leftLimit = this.SyncOnSplicer7.Location.X;

			rightLimit = this.SyncOnSplicer1.Location.X + this.SyncOnSplicer1.Width;
			HorizontalLocationMirror(this.SyncOnSplicer1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.SyncOnSplicer2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.SyncOnSplicer3, leftLimit, rightLimit);
			HorizontalLocationMirror(this.SyncOnSplicer4, leftLimit, rightLimit);
			HorizontalLocationMirror(this.SyncOnSplicer5, leftLimit, rightLimit);
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      HorizontalLocationMirror(this.SyncOnSplicer6, leftLimit, rightLimit);
      HorizontalLocationMirror(this.SyncOnSplicer7, leftLimit, rightLimit);

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		protected override void F11()
		{
		}


		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_HomeWet));
      this.SyncIdle = new SyncroC.Controls.StateButton();
      this.ttl_Syncronization = new SyncroC.Controls.Title();
      this.SyncOnMetres = new SyncroC.Controls.StateButton();
      this.SyncNow = new SyncroC.Controls.StateButton();
      this.SyncOnSplicer1 = new SyncroC.Controls.StateButton();
      this.SyncOnSplicer2 = new SyncroC.Controls.StateButton();
      this.SyncOnSplicer3 = new SyncroC.Controls.StateButton();
      this.SyncOnSplicer4 = new SyncroC.Controls.StateButton();
      this.SyncOnSplicer5 = new SyncroC.Controls.StateButton();
      this.ttl_Splicer1 = new SyncroC.Controls.Title();
      this.chk_S1Ready = new SyncroC.Controls.CheckBox();
      this.chk_S2Ready = new SyncroC.Controls.CheckBox();
      this.ttl_Splicer2 = new SyncroC.Controls.Title();
      this.chk_S3Ready = new SyncroC.Controls.CheckBox();
      this.ttl_Splicer3 = new SyncroC.Controls.Title();
      this.chk_S4Ready = new SyncroC.Controls.CheckBox();
      this.ttl_Splicer4 = new SyncroC.Controls.Title();
      this.chk_S5Ready = new SyncroC.Controls.CheckBox();
      this.ttl_Splicer5 = new SyncroC.Controls.Title();
      this.ttl_CurrentS1 = new SyncroC.Controls.TitledLabel();
      this.ttl_CurrentS2 = new SyncroC.Controls.TitledLabel();
      this.ttl_CurrentS3 = new SyncroC.Controls.TitledLabel();
      this.ttl_CurrentS4 = new SyncroC.Controls.TitledLabel();
      this.ttl_CurrentS5 = new SyncroC.Controls.TitledLabel();
      this.ttl_NextS5 = new SyncroC.Controls.TitledLabel();
      this.ttl_NextS4 = new SyncroC.Controls.TitledLabel();
      this.ttl_NextS3 = new SyncroC.Controls.TitledLabel();
      this.ttl_NextS2 = new SyncroC.Controls.TitledLabel();
      this.ttl_NextS1 = new SyncroC.Controls.TitledLabel();
      this.ToggleAutoManS5 = new SyncroC.Controls.StateButton();
      this.ToggleAutoManS4 = new SyncroC.Controls.StateButton();
      this.ToggleAutoManS3 = new SyncroC.Controls.StateButton();
      this.ToggleAutoManS2 = new SyncroC.Controls.StateButton();
      this.ToggleAutoManS1 = new SyncroC.Controls.StateButton();
      this.tnud_WetEndMetersProduced = new SyncroC.Controls.TitledNumericUpDown();
      this.tmt_WetEndStartTime = new SyncroC.Controls.TitledMaskTextBox();
      this.ttl_WetEnd = new SyncroC.Controls.Title();
      this.stb_SpliceConfirmedS1 = new SyncroC.Controls.StateButton();
      this.stb_SpliceConfirmedS2 = new SyncroC.Controls.StateButton();
      this.stb_SpliceConfirmedS3 = new SyncroC.Controls.StateButton();
      this.stb_SpliceConfirmedS4 = new SyncroC.Controls.StateButton();
      this.stb_SpliceConfirmedS5 = new SyncroC.Controls.StateButton();
      this.tnud_WetEndMetersRequired = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_StopTime = new SyncroC.Controls.TitledLabel();
      this.lbl_ShiftID = new System.Windows.Forms.Label();
      this.tmt_ShiftStartTime = new SyncroC.Controls.TitledMaskTextBox();
      this.title2 = new SyncroC.Controls.Title();
      this.tnud_RemainingProductionS5 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_RemainingProductionS4 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_RemainingProductionS3 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_RemainingProductionS2 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_RemainingProductionS1 = new SyncroC.Controls.TitledNumericUpDown();
      this.tlb_WetEndNote = new SyncroC.Controls.TitledLabel();
      this.tlb_ShiftMetresProduced = new SyncroC.Controls.TitledNumericUpDown();
      this.ToggleAutoManS7 = new SyncroC.Controls.StateButton();
      this.ToggleAutoManS6 = new SyncroC.Controls.StateButton();
      this.chk_S6Ready = new SyncroC.Controls.CheckBox();
      this.chk_S7Ready = new SyncroC.Controls.CheckBox();
      this.stb_SpliceConfirmedS6 = new SyncroC.Controls.StateButton();
      this.stb_SpliceConfirmedS7 = new SyncroC.Controls.StateButton();
      this.ttl_Splicer6 = new SyncroC.Controls.Title();
      this.ttl_Splicer7 = new SyncroC.Controls.Title();
      this.ttl_CurrentS7 = new SyncroC.Controls.TitledLabel();
      this.ttl_CurrentS6 = new SyncroC.Controls.TitledLabel();
      this.ttl_NextS7 = new SyncroC.Controls.TitledLabel();
      this.ttl_NextS6 = new SyncroC.Controls.TitledLabel();
      this.SyncOnSplicer7 = new SyncroC.Controls.StateButton();
      this.SyncOnSplicer6 = new SyncroC.Controls.StateButton();
      this.tnud_RemainingProductionS7 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_RemainingProductionS6 = new SyncroC.Controls.TitledNumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // iml_navigationImages
      // 
      this.iml_navigationImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_navigationImages.ImageStream")));
      this.iml_navigationImages.Images.SetKeyName(0, "");
      this.iml_navigationImages.Images.SetKeyName(1, "");
      this.iml_navigationImages.Images.SetKeyName(2, "");
      this.iml_navigationImages.Images.SetKeyName(3, "");
      // 
      // btn_Group1
      // 
      this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
      this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
      // 
      // btn_Group2
      // 
      this.btn_Group2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedActiveImage")));
      this.btn_Group2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedImage")));
      // 
      // btn_Group3
      // 
      this.btn_Group3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedActiveImage")));
      this.btn_Group3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedImage")));
      // 
      // btn_Group5
      // 
      this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
      this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // SyncIdle
      // 
      this.SyncIdle.Active = false;
      this.SyncIdle.Alarmed = false;
      this.SyncIdle.Auto = false;
      this.SyncIdle.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SyncIdle.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SyncIdle.FillBackColorWithImage = false;
      this.SyncIdle.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SyncIdle.ForeColor = System.Drawing.Color.Black;
      this.SyncIdle.FramePresent = true;
      this.SyncIdle.HighLightEnable = false;
      this.SyncIdle.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SyncIdle.ImagePassword = null;
      this.SyncIdle.Inemergency = false;
      this.SyncIdle.InLocal = false;
      this.SyncIdle.IsButton = true;
      this.SyncIdle.IsForceToInvisible = false;
      this.SyncIdle.Location = new System.Drawing.Point(42, 645);
      this.SyncIdle.Name = "SyncIdle";
      this.SyncIdle.OnLine = false;
      this.SyncIdle.ShowImage = false;
      this.SyncIdle.ShowText = true;
      this.SyncIdle.Size = new System.Drawing.Size(105, 70);
      this.SyncIdle.Switching = false;
      this.SyncIdle.TabIndex = 40;
      this.SyncIdle.Text = "Sync Idle";
      this.SyncIdle.TranslateText = true;
      this.SyncIdle.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SyncIdle.UserDefinedActiveImage")));
      this.SyncIdle.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SyncIdle.UserDefinedImage")));
      this.SyncIdle.Warned = false;
      this.SyncIdle.Click += new System.EventHandler(this.SyncIdle_Click);
      // 
      // ttl_Syncronization
      // 
      this.ttl_Syncronization.BackColor = System.Drawing.Color.Black;
      this.ttl_Syncronization.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Syncronization.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Syncronization.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Syncronization.Location = new System.Drawing.Point(30, 620);
      this.ttl_Syncronization.Name = "ttl_Syncronization";
      this.ttl_Syncronization.Size = new System.Drawing.Size(1171, 92);
      this.ttl_Syncronization.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Syncronization.TabIndex = 200;
      this.ttl_Syncronization.Text = "Splice Syncronization";
      this.ttl_Syncronization.TranslateText = true;
      // 
      // SyncOnMetres
      // 
      this.SyncOnMetres.Active = false;
      this.SyncOnMetres.Alarmed = false;
      this.SyncOnMetres.Auto = false;
      this.SyncOnMetres.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SyncOnMetres.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SyncOnMetres.FillBackColorWithImage = false;
      this.SyncOnMetres.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SyncOnMetres.ForeColor = System.Drawing.Color.Black;
      this.SyncOnMetres.FramePresent = true;
      this.SyncOnMetres.HighLightEnable = false;
      this.SyncOnMetres.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SyncOnMetres.ImagePassword = null;
      this.SyncOnMetres.Inemergency = false;
      this.SyncOnMetres.InLocal = false;
      this.SyncOnMetres.IsButton = true;
      this.SyncOnMetres.IsForceToInvisible = false;
      this.SyncOnMetres.Location = new System.Drawing.Point(179, 645);
      this.SyncOnMetres.Name = "SyncOnMetres";
      this.SyncOnMetres.OnLine = false;
      this.SyncOnMetres.ShowImage = false;
      this.SyncOnMetres.ShowText = true;
      this.SyncOnMetres.Size = new System.Drawing.Size(105, 70);
      this.SyncOnMetres.Switching = false;
      this.SyncOnMetres.TabIndex = 41;
      this.SyncOnMetres.Text = "Sync on Meters";
      this.SyncOnMetres.TranslateText = true;
      this.SyncOnMetres.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SyncOnMetres.UserDefinedActiveImage")));
      this.SyncOnMetres.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SyncOnMetres.UserDefinedImage")));
      this.SyncOnMetres.Warned = false;
      this.SyncOnMetres.Click += new System.EventHandler(this.SyncOnMetres_Click);
      // 
      // SyncNow
      // 
      this.SyncNow.Active = false;
      this.SyncNow.Alarmed = false;
      this.SyncNow.Auto = false;
      this.SyncNow.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SyncNow.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SyncNow.FillBackColorWithImage = false;
      this.SyncNow.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SyncNow.ForeColor = System.Drawing.Color.Black;
      this.SyncNow.FramePresent = true;
      this.SyncNow.HighLightEnable = false;
      this.SyncNow.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SyncNow.ImagePassword = null;
      this.SyncNow.Inemergency = false;
      this.SyncNow.InLocal = false;
      this.SyncNow.IsButton = true;
      this.SyncNow.IsForceToInvisible = false;
      this.SyncNow.Location = new System.Drawing.Point(316, 645);
      this.SyncNow.Name = "SyncNow";
      this.SyncNow.OnLine = false;
      this.SyncNow.ShowImage = false;
      this.SyncNow.ShowText = true;
      this.SyncNow.Size = new System.Drawing.Size(105, 70);
      this.SyncNow.Switching = false;
      this.SyncNow.TabIndex = 42;
      this.SyncNow.Text = "Sync Now";
      this.SyncNow.TranslateText = true;
      this.SyncNow.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SyncNow.UserDefinedActiveImage")));
      this.SyncNow.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SyncNow.UserDefinedImage")));
      this.SyncNow.Warned = false;
      this.SyncNow.Click += new System.EventHandler(this.SyncNow_Click);
      // 
      // SyncOnSplicer1
      // 
      this.SyncOnSplicer1.Active = false;
      this.SyncOnSplicer1.Alarmed = false;
      this.SyncOnSplicer1.Auto = false;
      this.SyncOnSplicer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SyncOnSplicer1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SyncOnSplicer1.FillBackColorWithImage = false;
      this.SyncOnSplicer1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SyncOnSplicer1.ForeColor = System.Drawing.Color.Black;
      this.SyncOnSplicer1.FramePresent = true;
      this.SyncOnSplicer1.HighLightEnable = false;
      this.SyncOnSplicer1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SyncOnSplicer1.ImagePassword = null;
      this.SyncOnSplicer1.Inemergency = false;
      this.SyncOnSplicer1.InLocal = false;
      this.SyncOnSplicer1.IsButton = true;
      this.SyncOnSplicer1.IsForceToInvisible = false;
      this.SyncOnSplicer1.Location = new System.Drawing.Point(1101, 645);
      this.SyncOnSplicer1.Name = "SyncOnSplicer1";
      this.SyncOnSplicer1.OnLine = false;
      this.SyncOnSplicer1.ShowImage = false;
      this.SyncOnSplicer1.ShowText = true;
      this.SyncOnSplicer1.Size = new System.Drawing.Size(100, 70);
      this.SyncOnSplicer1.Switching = false;
      this.SyncOnSplicer1.TabIndex = 43;
      this.SyncOnSplicer1.Text = "Sync on Splicer 1";
      this.SyncOnSplicer1.TranslateText = true;
      this.SyncOnSplicer1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer1.UserDefinedActiveImage")));
      this.SyncOnSplicer1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer1.UserDefinedImage")));
      this.SyncOnSplicer1.Warned = false;
      this.SyncOnSplicer1.Click += new System.EventHandler(this.SyncOnSplicer1_Click);
      // 
      // SyncOnSplicer2
      // 
      this.SyncOnSplicer2.Active = false;
      this.SyncOnSplicer2.Alarmed = false;
      this.SyncOnSplicer2.Auto = false;
      this.SyncOnSplicer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SyncOnSplicer2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SyncOnSplicer2.FillBackColorWithImage = false;
      this.SyncOnSplicer2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SyncOnSplicer2.ForeColor = System.Drawing.Color.Black;
      this.SyncOnSplicer2.FramePresent = true;
      this.SyncOnSplicer2.HighLightEnable = false;
      this.SyncOnSplicer2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SyncOnSplicer2.ImagePassword = null;
      this.SyncOnSplicer2.Inemergency = false;
      this.SyncOnSplicer2.InLocal = false;
      this.SyncOnSplicer2.IsButton = true;
      this.SyncOnSplicer2.IsForceToInvisible = false;
      this.SyncOnSplicer2.Location = new System.Drawing.Point(991, 645);
      this.SyncOnSplicer2.Name = "SyncOnSplicer2";
      this.SyncOnSplicer2.OnLine = false;
      this.SyncOnSplicer2.ShowImage = false;
      this.SyncOnSplicer2.ShowText = true;
      this.SyncOnSplicer2.Size = new System.Drawing.Size(100, 70);
      this.SyncOnSplicer2.Switching = false;
      this.SyncOnSplicer2.TabIndex = 44;
      this.SyncOnSplicer2.Text = "Sync on Splicer 2";
      this.SyncOnSplicer2.TranslateText = true;
      this.SyncOnSplicer2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer2.UserDefinedActiveImage")));
      this.SyncOnSplicer2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer2.UserDefinedImage")));
      this.SyncOnSplicer2.Warned = false;
      this.SyncOnSplicer2.Click += new System.EventHandler(this.SyncOnSplicer2_Click);
      // 
      // SyncOnSplicer3
      // 
      this.SyncOnSplicer3.Active = false;
      this.SyncOnSplicer3.Alarmed = false;
      this.SyncOnSplicer3.Auto = false;
      this.SyncOnSplicer3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SyncOnSplicer3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SyncOnSplicer3.FillBackColorWithImage = false;
      this.SyncOnSplicer3.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SyncOnSplicer3.ForeColor = System.Drawing.Color.Black;
      this.SyncOnSplicer3.FramePresent = true;
      this.SyncOnSplicer3.HighLightEnable = false;
      this.SyncOnSplicer3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SyncOnSplicer3.ImagePassword = null;
      this.SyncOnSplicer3.Inemergency = false;
      this.SyncOnSplicer3.InLocal = false;
      this.SyncOnSplicer3.IsButton = true;
      this.SyncOnSplicer3.IsForceToInvisible = false;
      this.SyncOnSplicer3.Location = new System.Drawing.Point(881, 645);
      this.SyncOnSplicer3.Name = "SyncOnSplicer3";
      this.SyncOnSplicer3.OnLine = false;
      this.SyncOnSplicer3.ShowImage = false;
      this.SyncOnSplicer3.ShowText = true;
      this.SyncOnSplicer3.Size = new System.Drawing.Size(100, 70);
      this.SyncOnSplicer3.Switching = false;
      this.SyncOnSplicer3.TabIndex = 45;
      this.SyncOnSplicer3.Text = "Sync on Splicer 3";
      this.SyncOnSplicer3.TranslateText = true;
      this.SyncOnSplicer3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer3.UserDefinedActiveImage")));
      this.SyncOnSplicer3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer3.UserDefinedImage")));
      this.SyncOnSplicer3.Warned = false;
      this.SyncOnSplicer3.Click += new System.EventHandler(this.SyncOnSplicer3_Click);
      // 
      // SyncOnSplicer4
      // 
      this.SyncOnSplicer4.Active = false;
      this.SyncOnSplicer4.Alarmed = false;
      this.SyncOnSplicer4.Auto = false;
      this.SyncOnSplicer4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SyncOnSplicer4.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SyncOnSplicer4.FillBackColorWithImage = false;
      this.SyncOnSplicer4.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SyncOnSplicer4.ForeColor = System.Drawing.Color.Black;
      this.SyncOnSplicer4.FramePresent = true;
      this.SyncOnSplicer4.HighLightEnable = false;
      this.SyncOnSplicer4.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SyncOnSplicer4.ImagePassword = null;
      this.SyncOnSplicer4.Inemergency = false;
      this.SyncOnSplicer4.InLocal = false;
      this.SyncOnSplicer4.IsButton = true;
      this.SyncOnSplicer4.IsForceToInvisible = false;
      this.SyncOnSplicer4.Location = new System.Drawing.Point(771, 645);
      this.SyncOnSplicer4.Name = "SyncOnSplicer4";
      this.SyncOnSplicer4.OnLine = false;
      this.SyncOnSplicer4.ShowImage = false;
      this.SyncOnSplicer4.ShowText = true;
      this.SyncOnSplicer4.Size = new System.Drawing.Size(100, 70);
      this.SyncOnSplicer4.Switching = false;
      this.SyncOnSplicer4.TabIndex = 46;
      this.SyncOnSplicer4.Text = "Sync on Splicer 4";
      this.SyncOnSplicer4.TranslateText = true;
      this.SyncOnSplicer4.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer4.UserDefinedActiveImage")));
      this.SyncOnSplicer4.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer4.UserDefinedImage")));
      this.SyncOnSplicer4.Warned = false;
      this.SyncOnSplicer4.Click += new System.EventHandler(this.SyncOnSplicer4_Click);
      // 
      // SyncOnSplicer5
      // 
      this.SyncOnSplicer5.Active = false;
      this.SyncOnSplicer5.Alarmed = false;
      this.SyncOnSplicer5.Auto = false;
      this.SyncOnSplicer5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SyncOnSplicer5.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SyncOnSplicer5.FillBackColorWithImage = false;
      this.SyncOnSplicer5.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SyncOnSplicer5.ForeColor = System.Drawing.Color.Black;
      this.SyncOnSplicer5.FramePresent = true;
      this.SyncOnSplicer5.HighLightEnable = false;
      this.SyncOnSplicer5.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SyncOnSplicer5.ImagePassword = null;
      this.SyncOnSplicer5.Inemergency = false;
      this.SyncOnSplicer5.InLocal = false;
      this.SyncOnSplicer5.IsButton = true;
      this.SyncOnSplicer5.IsForceToInvisible = false;
      this.SyncOnSplicer5.Location = new System.Drawing.Point(661, 645);
      this.SyncOnSplicer5.Name = "SyncOnSplicer5";
      this.SyncOnSplicer5.OnLine = false;
      this.SyncOnSplicer5.ShowImage = false;
      this.SyncOnSplicer5.ShowText = true;
      this.SyncOnSplicer5.Size = new System.Drawing.Size(100, 70);
      this.SyncOnSplicer5.Switching = false;
      this.SyncOnSplicer5.TabIndex = 47;
      this.SyncOnSplicer5.Text = "Sync on Splicer 5";
      this.SyncOnSplicer5.TranslateText = true;
      this.SyncOnSplicer5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer5.UserDefinedActiveImage")));
      this.SyncOnSplicer5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer5.UserDefinedImage")));
      this.SyncOnSplicer5.Warned = false;
      this.SyncOnSplicer5.Click += new System.EventHandler(this.SyncOnSplicer5_Click);
      // 
      // ttl_Splicer1
      // 
      this.ttl_Splicer1.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer1.Location = new System.Drawing.Point(1101, 193);
      this.ttl_Splicer1.Name = "ttl_Splicer1";
      this.ttl_Splicer1.Size = new System.Drawing.Size(100, 416);
      this.ttl_Splicer1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer1.TabIndex = 208;
      this.ttl_Splicer1.Text = "Splicer 1";
      this.ttl_Splicer1.TranslateText = false;
      // 
      // chk_S1Ready
      // 
      this.chk_S1Ready.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1Ready.Checked = false;
      this.chk_S1Ready.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S1Ready.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S1Ready.ImagePassword = null;
      this.chk_S1Ready.Location = new System.Drawing.Point(1108, 216);
      this.chk_S1Ready.Name = "chk_S1Ready";
      this.chk_S1Ready.ReadOnly = true;
      this.chk_S1Ready.Size = new System.Drawing.Size(93, 54);
      this.chk_S1Ready.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_S1Ready.TabIndex = 11;
      this.chk_S1Ready.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1Ready.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_S1Ready.TranslateText = false;
      // 
      // chk_S2Ready
      // 
      this.chk_S2Ready.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2Ready.Checked = false;
      this.chk_S2Ready.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S2Ready.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S2Ready.ImagePassword = null;
      this.chk_S2Ready.Location = new System.Drawing.Point(998, 216);
      this.chk_S2Ready.Name = "chk_S2Ready";
      this.chk_S2Ready.ReadOnly = true;
      this.chk_S2Ready.Size = new System.Drawing.Size(93, 54);
      this.chk_S2Ready.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_S2Ready.TabIndex = 16;
      this.chk_S2Ready.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2Ready.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_S2Ready.TranslateText = false;
      // 
      // ttl_Splicer2
      // 
      this.ttl_Splicer2.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer2.Location = new System.Drawing.Point(991, 193);
      this.ttl_Splicer2.Name = "ttl_Splicer2";
      this.ttl_Splicer2.Size = new System.Drawing.Size(100, 416);
      this.ttl_Splicer2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer2.TabIndex = 210;
      this.ttl_Splicer2.Text = "Splicer 2";
      this.ttl_Splicer2.TranslateText = false;
      // 
      // chk_S3Ready
      // 
      this.chk_S3Ready.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S3Ready.Checked = false;
      this.chk_S3Ready.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S3Ready.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S3Ready.ImagePassword = null;
      this.chk_S3Ready.Location = new System.Drawing.Point(888, 216);
      this.chk_S3Ready.Name = "chk_S3Ready";
      this.chk_S3Ready.ReadOnly = true;
      this.chk_S3Ready.Size = new System.Drawing.Size(93, 54);
      this.chk_S3Ready.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_S3Ready.TabIndex = 21;
      this.chk_S3Ready.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S3Ready.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_S3Ready.TranslateText = false;
      // 
      // ttl_Splicer3
      // 
      this.ttl_Splicer3.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer3.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer3.Location = new System.Drawing.Point(881, 193);
      this.ttl_Splicer3.Name = "ttl_Splicer3";
      this.ttl_Splicer3.Size = new System.Drawing.Size(100, 416);
      this.ttl_Splicer3.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer3.TabIndex = 212;
      this.ttl_Splicer3.Text = "Splicer 3";
      this.ttl_Splicer3.TranslateText = false;
      // 
      // chk_S4Ready
      // 
      this.chk_S4Ready.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S4Ready.Checked = false;
      this.chk_S4Ready.Enabled = false;
      this.chk_S4Ready.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S4Ready.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S4Ready.ImagePassword = null;
      this.chk_S4Ready.Location = new System.Drawing.Point(778, 216);
      this.chk_S4Ready.Name = "chk_S4Ready";
      this.chk_S4Ready.ReadOnly = true;
      this.chk_S4Ready.Size = new System.Drawing.Size(93, 54);
      this.chk_S4Ready.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_S4Ready.TabIndex = 26;
      this.chk_S4Ready.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S4Ready.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_S4Ready.TranslateText = false;
      // 
      // ttl_Splicer4
      // 
      this.ttl_Splicer4.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer4.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer4.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer4.Location = new System.Drawing.Point(771, 193);
      this.ttl_Splicer4.Name = "ttl_Splicer4";
      this.ttl_Splicer4.Size = new System.Drawing.Size(100, 416);
      this.ttl_Splicer4.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer4.TabIndex = 214;
      this.ttl_Splicer4.Text = "Splicer 4";
      this.ttl_Splicer4.TranslateText = false;
      // 
      // chk_S5Ready
      // 
      this.chk_S5Ready.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S5Ready.Checked = false;
      this.chk_S5Ready.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S5Ready.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S5Ready.ImagePassword = null;
      this.chk_S5Ready.Location = new System.Drawing.Point(668, 216);
      this.chk_S5Ready.Name = "chk_S5Ready";
      this.chk_S5Ready.ReadOnly = true;
      this.chk_S5Ready.Size = new System.Drawing.Size(93, 54);
      this.chk_S5Ready.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_S5Ready.TabIndex = 31;
      this.chk_S5Ready.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S5Ready.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_S5Ready.TranslateText = false;
      // 
      // ttl_Splicer5
      // 
      this.ttl_Splicer5.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer5.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer5.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer5.Location = new System.Drawing.Point(661, 193);
      this.ttl_Splicer5.Name = "ttl_Splicer5";
      this.ttl_Splicer5.Size = new System.Drawing.Size(100, 416);
      this.ttl_Splicer5.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer5.TabIndex = 216;
      this.ttl_Splicer5.Text = "Splicer 5";
      this.ttl_Splicer5.TranslateText = false;
      // 
      // ttl_CurrentS1
      // 
      this.ttl_CurrentS1.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentS1.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS1.ImagePassword = null;
      this.ttl_CurrentS1.Location = new System.Drawing.Point(1108, 430);
      this.ttl_CurrentS1.Name = "ttl_CurrentS1";
      this.ttl_CurrentS1.Size = new System.Drawing.Size(93, 86);
      this.ttl_CurrentS1.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_CurrentS1.TabIndex = 218;
      this.ttl_CurrentS1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS1.TranslateText = false;
      this.ttl_CurrentS1.Value = "S1 current paper";
      this.ttl_CurrentS1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS1.ValueMultiline = true;
      // 
      // ttl_CurrentS2
      // 
      this.ttl_CurrentS2.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentS2.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS2.ImagePassword = null;
      this.ttl_CurrentS2.Location = new System.Drawing.Point(998, 430);
      this.ttl_CurrentS2.Name = "ttl_CurrentS2";
      this.ttl_CurrentS2.Size = new System.Drawing.Size(93, 86);
      this.ttl_CurrentS2.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_CurrentS2.TabIndex = 219;
      this.ttl_CurrentS2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS2.TranslateText = false;
      this.ttl_CurrentS2.Value = "S2 current paper";
      this.ttl_CurrentS2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS2.ValueMultiline = true;
      // 
      // ttl_CurrentS3
      // 
      this.ttl_CurrentS3.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentS3.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS3.ImagePassword = null;
      this.ttl_CurrentS3.Location = new System.Drawing.Point(888, 430);
      this.ttl_CurrentS3.Name = "ttl_CurrentS3";
      this.ttl_CurrentS3.Size = new System.Drawing.Size(93, 86);
      this.ttl_CurrentS3.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_CurrentS3.TabIndex = 220;
      this.ttl_CurrentS3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS3.TranslateText = false;
      this.ttl_CurrentS3.Value = "S3 current paper";
      this.ttl_CurrentS3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS3.ValueMultiline = true;
      // 
      // ttl_CurrentS4
      // 
      this.ttl_CurrentS4.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentS4.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS4.ImagePassword = null;
      this.ttl_CurrentS4.Location = new System.Drawing.Point(778, 430);
      this.ttl_CurrentS4.Name = "ttl_CurrentS4";
      this.ttl_CurrentS4.Size = new System.Drawing.Size(93, 86);
      this.ttl_CurrentS4.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_CurrentS4.TabIndex = 221;
      this.ttl_CurrentS4.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS4.TranslateText = false;
      this.ttl_CurrentS4.Value = "S4 current paper";
      this.ttl_CurrentS4.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS4.ValueMultiline = true;
      // 
      // ttl_CurrentS5
      // 
      this.ttl_CurrentS5.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentS5.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS5.ImagePassword = null;
      this.ttl_CurrentS5.Location = new System.Drawing.Point(668, 430);
      this.ttl_CurrentS5.Name = "ttl_CurrentS5";
      this.ttl_CurrentS5.Size = new System.Drawing.Size(93, 86);
      this.ttl_CurrentS5.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_CurrentS5.TabIndex = 222;
      this.ttl_CurrentS5.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS5.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS5.TranslateText = false;
      this.ttl_CurrentS5.Value = "S5 current paper";
      this.ttl_CurrentS5.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS5.ValueMultiline = true;
      // 
      // ttl_NextS5
      // 
      this.ttl_NextS5.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextS5.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS5.ImagePassword = null;
      this.ttl_NextS5.Location = new System.Drawing.Point(668, 523);
      this.ttl_NextS5.Name = "ttl_NextS5";
      this.ttl_NextS5.Size = new System.Drawing.Size(93, 86);
      this.ttl_NextS5.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_NextS5.TabIndex = 227;
      this.ttl_NextS5.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS5.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS5.TranslateText = false;
      this.ttl_NextS5.Value = "S5 next paper";
      this.ttl_NextS5.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS5.ValueMultiline = true;
      // 
      // ttl_NextS4
      // 
      this.ttl_NextS4.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextS4.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS4.ImagePassword = null;
      this.ttl_NextS4.Location = new System.Drawing.Point(778, 523);
      this.ttl_NextS4.Name = "ttl_NextS4";
      this.ttl_NextS4.Size = new System.Drawing.Size(93, 86);
      this.ttl_NextS4.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_NextS4.TabIndex = 226;
      this.ttl_NextS4.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS4.TranslateText = false;
      this.ttl_NextS4.Value = "S4 next paper";
      this.ttl_NextS4.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS4.ValueMultiline = true;
      // 
      // ttl_NextS3
      // 
      this.ttl_NextS3.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextS3.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS3.ImagePassword = null;
      this.ttl_NextS3.Location = new System.Drawing.Point(888, 523);
      this.ttl_NextS3.Name = "ttl_NextS3";
      this.ttl_NextS3.Size = new System.Drawing.Size(93, 86);
      this.ttl_NextS3.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_NextS3.TabIndex = 225;
      this.ttl_NextS3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS3.TranslateText = false;
      this.ttl_NextS3.Value = "S3 next paper";
      this.ttl_NextS3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS3.ValueMultiline = true;
      // 
      // ttl_NextS2
      // 
      this.ttl_NextS2.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextS2.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS2.ImagePassword = null;
      this.ttl_NextS2.Location = new System.Drawing.Point(998, 523);
      this.ttl_NextS2.Name = "ttl_NextS2";
      this.ttl_NextS2.Size = new System.Drawing.Size(93, 86);
      this.ttl_NextS2.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_NextS2.TabIndex = 224;
      this.ttl_NextS2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS2.TranslateText = false;
      this.ttl_NextS2.Value = "S2 next paper";
      this.ttl_NextS2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS2.ValueMultiline = true;
      // 
      // ttl_NextS1
      // 
      this.ttl_NextS1.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextS1.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS1.ImagePassword = null;
      this.ttl_NextS1.Location = new System.Drawing.Point(1108, 523);
      this.ttl_NextS1.Name = "ttl_NextS1";
      this.ttl_NextS1.Size = new System.Drawing.Size(93, 86);
      this.ttl_NextS1.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_NextS1.TabIndex = 223;
      this.ttl_NextS1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS1.TranslateText = false;
      this.ttl_NextS1.Value = "S1 next paper";
      this.ttl_NextS1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS1.ValueMultiline = true;
      // 
      // ToggleAutoManS5
      // 
      this.ToggleAutoManS5.Active = false;
      this.ToggleAutoManS5.Alarmed = false;
      this.ToggleAutoManS5.Auto = false;
      this.ToggleAutoManS5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManS5.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManS5.FillBackColorWithImage = false;
      this.ToggleAutoManS5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManS5.FramePresent = true;
      this.ToggleAutoManS5.HighLightEnable = false;
      this.ToggleAutoManS5.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManS5.ImagePassword = null;
      this.ToggleAutoManS5.Inemergency = false;
      this.ToggleAutoManS5.InLocal = false;
      this.ToggleAutoManS5.IsButton = true;
      this.ToggleAutoManS5.IsForceToInvisible = false;
      this.ToggleAutoManS5.Location = new System.Drawing.Point(668, 281);
      this.ToggleAutoManS5.Name = "ToggleAutoManS5";
      this.ToggleAutoManS5.OnLine = false;
      this.ToggleAutoManS5.ShowImage = true;
      this.ToggleAutoManS5.ShowText = false;
      this.ToggleAutoManS5.Size = new System.Drawing.Size(93, 70);
      this.ToggleAutoManS5.Switching = false;
      this.ToggleAutoManS5.TabIndex = 30;
      this.ToggleAutoManS5.TranslateText = false;
      this.ToggleAutoManS5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS5.UserDefinedActiveImage")));
      this.ToggleAutoManS5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS5.UserDefinedImage")));
      this.ToggleAutoManS5.Warned = false;
      this.ToggleAutoManS5.Click += new System.EventHandler(this.ToggleAutoManSX_Click);
      // 
      // ToggleAutoManS4
      // 
      this.ToggleAutoManS4.Active = false;
      this.ToggleAutoManS4.Alarmed = false;
      this.ToggleAutoManS4.Auto = false;
      this.ToggleAutoManS4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManS4.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManS4.FillBackColorWithImage = false;
      this.ToggleAutoManS4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManS4.FramePresent = true;
      this.ToggleAutoManS4.HighLightEnable = false;
      this.ToggleAutoManS4.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManS4.ImagePassword = null;
      this.ToggleAutoManS4.Inemergency = false;
      this.ToggleAutoManS4.InLocal = false;
      this.ToggleAutoManS4.IsButton = true;
      this.ToggleAutoManS4.IsForceToInvisible = false;
      this.ToggleAutoManS4.Location = new System.Drawing.Point(778, 281);
      this.ToggleAutoManS4.Name = "ToggleAutoManS4";
      this.ToggleAutoManS4.OnLine = false;
      this.ToggleAutoManS4.ShowImage = true;
      this.ToggleAutoManS4.ShowText = false;
      this.ToggleAutoManS4.Size = new System.Drawing.Size(93, 70);
      this.ToggleAutoManS4.Switching = false;
      this.ToggleAutoManS4.TabIndex = 25;
      this.ToggleAutoManS4.TranslateText = false;
      this.ToggleAutoManS4.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS4.UserDefinedActiveImage")));
      this.ToggleAutoManS4.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS4.UserDefinedImage")));
      this.ToggleAutoManS4.Warned = false;
      this.ToggleAutoManS4.Click += new System.EventHandler(this.ToggleAutoManSX_Click);
      // 
      // ToggleAutoManS3
      // 
      this.ToggleAutoManS3.Active = false;
      this.ToggleAutoManS3.Alarmed = false;
      this.ToggleAutoManS3.Auto = false;
      this.ToggleAutoManS3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManS3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManS3.FillBackColorWithImage = false;
      this.ToggleAutoManS3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManS3.FramePresent = true;
      this.ToggleAutoManS3.HighLightEnable = false;
      this.ToggleAutoManS3.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManS3.ImagePassword = null;
      this.ToggleAutoManS3.Inemergency = false;
      this.ToggleAutoManS3.InLocal = false;
      this.ToggleAutoManS3.IsButton = true;
      this.ToggleAutoManS3.IsForceToInvisible = false;
      this.ToggleAutoManS3.Location = new System.Drawing.Point(888, 281);
      this.ToggleAutoManS3.Name = "ToggleAutoManS3";
      this.ToggleAutoManS3.OnLine = false;
      this.ToggleAutoManS3.ShowImage = true;
      this.ToggleAutoManS3.ShowText = false;
      this.ToggleAutoManS3.Size = new System.Drawing.Size(93, 70);
      this.ToggleAutoManS3.Switching = false;
      this.ToggleAutoManS3.TabIndex = 20;
      this.ToggleAutoManS3.TranslateText = false;
      this.ToggleAutoManS3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS3.UserDefinedActiveImage")));
      this.ToggleAutoManS3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS3.UserDefinedImage")));
      this.ToggleAutoManS3.Warned = false;
      this.ToggleAutoManS3.Click += new System.EventHandler(this.ToggleAutoManSX_Click);
      // 
      // ToggleAutoManS2
      // 
      this.ToggleAutoManS2.Active = false;
      this.ToggleAutoManS2.Alarmed = false;
      this.ToggleAutoManS2.Auto = false;
      this.ToggleAutoManS2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManS2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManS2.FillBackColorWithImage = false;
      this.ToggleAutoManS2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManS2.FramePresent = true;
      this.ToggleAutoManS2.HighLightEnable = false;
      this.ToggleAutoManS2.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManS2.ImagePassword = null;
      this.ToggleAutoManS2.Inemergency = false;
      this.ToggleAutoManS2.InLocal = false;
      this.ToggleAutoManS2.IsButton = true;
      this.ToggleAutoManS2.IsForceToInvisible = false;
      this.ToggleAutoManS2.Location = new System.Drawing.Point(998, 281);
      this.ToggleAutoManS2.Name = "ToggleAutoManS2";
      this.ToggleAutoManS2.OnLine = false;
      this.ToggleAutoManS2.ShowImage = true;
      this.ToggleAutoManS2.ShowText = false;
      this.ToggleAutoManS2.Size = new System.Drawing.Size(93, 70);
      this.ToggleAutoManS2.Switching = false;
      this.ToggleAutoManS2.TabIndex = 15;
      this.ToggleAutoManS2.TranslateText = false;
      this.ToggleAutoManS2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS2.UserDefinedActiveImage")));
      this.ToggleAutoManS2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS2.UserDefinedImage")));
      this.ToggleAutoManS2.Warned = false;
      this.ToggleAutoManS2.Click += new System.EventHandler(this.ToggleAutoManSX_Click);
      // 
      // ToggleAutoManS1
      // 
      this.ToggleAutoManS1.Active = false;
      this.ToggleAutoManS1.Alarmed = false;
      this.ToggleAutoManS1.Auto = false;
      this.ToggleAutoManS1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManS1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManS1.FillBackColorWithImage = false;
      this.ToggleAutoManS1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManS1.FramePresent = true;
      this.ToggleAutoManS1.HighLightEnable = false;
      this.ToggleAutoManS1.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManS1.ImagePassword = null;
      this.ToggleAutoManS1.Inemergency = false;
      this.ToggleAutoManS1.InLocal = false;
      this.ToggleAutoManS1.IsButton = true;
      this.ToggleAutoManS1.IsForceToInvisible = false;
      this.ToggleAutoManS1.Location = new System.Drawing.Point(1108, 281);
      this.ToggleAutoManS1.Name = "ToggleAutoManS1";
      this.ToggleAutoManS1.OnLine = false;
      this.ToggleAutoManS1.ShowImage = true;
      this.ToggleAutoManS1.ShowText = false;
      this.ToggleAutoManS1.Size = new System.Drawing.Size(93, 70);
      this.ToggleAutoManS1.Switching = false;
      this.ToggleAutoManS1.TabIndex = 10;
      this.ToggleAutoManS1.TranslateText = false;
      this.ToggleAutoManS1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS1.UserDefinedActiveImage")));
      this.ToggleAutoManS1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS1.UserDefinedImage")));
      this.ToggleAutoManS1.Warned = false;
      this.ToggleAutoManS1.Click += new System.EventHandler(this.ToggleAutoManSX_Click);
      // 
      // tnud_WetEndMetersProduced
      // 
      this.tnud_WetEndMetersProduced.DecimalPlaces = 0;
      this.tnud_WetEndMetersProduced.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WetEndMetersProduced.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_WetEndMetersProduced.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndMetersProduced.ImagePassword = null;
      this.tnud_WetEndMetersProduced.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WetEndMetersProduced.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_WetEndMetersProduced.Location = new System.Drawing.Point(37, 457);
      this.tnud_WetEndMetersProduced.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_WetEndMetersProduced.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WetEndMetersProduced.Name = "tnud_WetEndMetersProduced";
      this.tnud_WetEndMetersProduced.ReadOnly = true;
      this.tnud_WetEndMetersProduced.Size = new System.Drawing.Size(190, 64);
      this.tnud_WetEndMetersProduced.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_WetEndMetersProduced.TabIndex = 237;
      this.tnud_WetEndMetersProduced.TabStop = false;
      this.tnud_WetEndMetersProduced.Text = "Produced";
      this.tnud_WetEndMetersProduced.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndMetersProduced.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndMetersProduced.TranslateText = true;
      this.tnud_WetEndMetersProduced.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_WetEndMetersProduced.Value = "3542";
      this.tnud_WetEndMetersProduced.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndMetersProduced.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tmt_WetEndStartTime
      // 
      this.tmt_WetEndStartTime.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_WetEndStartTime.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tmt_WetEndStartTime.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_WetEndStartTime.ImagePassword = null;
      this.tmt_WetEndStartTime.Location = new System.Drawing.Point(37, 389);
      this.tmt_WetEndStartTime.Name = "tmt_WetEndStartTime";
      this.tmt_WetEndStartTime.PasswordChar = '\0';
      this.tmt_WetEndStartTime.ReadOnly = true;
      this.tmt_WetEndStartTime.Size = new System.Drawing.Size(388, 64);
      this.tmt_WetEndStartTime.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_WetEndStartTime.TabIndex = 236;
      this.tmt_WetEndStartTime.TabStop = false;
      this.tmt_WetEndStartTime.Text = "Quality Start Time";
      this.tmt_WetEndStartTime.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.tmt_WetEndStartTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_WetEndStartTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_WetEndStartTime.TranslateText = true;
      this.tmt_WetEndStartTime.Value = " ";
      this.tmt_WetEndStartTime.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.tmt_WetEndStartTime.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_WetEndStartTime.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.tmt_WetEndStartTime.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_WetEndStartTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttl_WetEnd
      // 
      this.ttl_WetEnd.BackColor = System.Drawing.Color.Black;
      this.ttl_WetEnd.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_WetEnd.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_WetEnd.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_WetEnd.Location = new System.Drawing.Point(30, 367);
      this.ttl_WetEnd.Name = "ttl_WetEnd";
      this.ttl_WetEnd.Size = new System.Drawing.Size(397, 242);
      this.ttl_WetEnd.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_WetEnd.TabIndex = 234;
      this.ttl_WetEnd.TabStop = false;
      this.ttl_WetEnd.Text = "WET END";
      this.ttl_WetEnd.TranslateText = false;
      // 
      // stb_SpliceConfirmedS1
      // 
      this.stb_SpliceConfirmedS1.Active = false;
      this.stb_SpliceConfirmedS1.Alarmed = false;
      this.stb_SpliceConfirmedS1.Auto = false;
      this.stb_SpliceConfirmedS1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_SpliceConfirmedS1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SpliceConfirmedS1.FillBackColorWithImage = false;
      this.stb_SpliceConfirmedS1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stb_SpliceConfirmedS1.ForeColor = System.Drawing.Color.Black;
      this.stb_SpliceConfirmedS1.FramePresent = true;
      this.stb_SpliceConfirmedS1.HighLightEnable = false;
      this.stb_SpliceConfirmedS1.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_SpliceConfirmedS1.ImagePassword = null;
      this.stb_SpliceConfirmedS1.Inemergency = false;
      this.stb_SpliceConfirmedS1.InLocal = false;
      this.stb_SpliceConfirmedS1.IsButton = true;
      this.stb_SpliceConfirmedS1.IsForceToInvisible = false;
      this.stb_SpliceConfirmedS1.Location = new System.Drawing.Point(1108, 357);
      this.stb_SpliceConfirmedS1.Name = "stb_SpliceConfirmedS1";
      this.stb_SpliceConfirmedS1.OnLine = false;
      this.stb_SpliceConfirmedS1.ShowImage = false;
      this.stb_SpliceConfirmedS1.ShowText = true;
      this.stb_SpliceConfirmedS1.Size = new System.Drawing.Size(93, 70);
      this.stb_SpliceConfirmedS1.Switching = false;
      this.stb_SpliceConfirmedS1.TabIndex = 238;
      this.stb_SpliceConfirmedS1.Text = "No Sync";
      this.stb_SpliceConfirmedS1.TranslateText = false;
      this.stb_SpliceConfirmedS1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS1.UserDefinedActiveImage")));
      this.stb_SpliceConfirmedS1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS1.UserDefinedImage")));
      this.stb_SpliceConfirmedS1.Warned = false;
      this.stb_SpliceConfirmedS1.Click += new System.EventHandler(this.stb_SpliceConfirmedSX_Click);
      // 
      // stb_SpliceConfirmedS2
      // 
      this.stb_SpliceConfirmedS2.Active = false;
      this.stb_SpliceConfirmedS2.Alarmed = false;
      this.stb_SpliceConfirmedS2.Auto = false;
      this.stb_SpliceConfirmedS2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_SpliceConfirmedS2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SpliceConfirmedS2.FillBackColorWithImage = false;
      this.stb_SpliceConfirmedS2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stb_SpliceConfirmedS2.ForeColor = System.Drawing.Color.Black;
      this.stb_SpliceConfirmedS2.FramePresent = true;
      this.stb_SpliceConfirmedS2.HighLightEnable = false;
      this.stb_SpliceConfirmedS2.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_SpliceConfirmedS2.ImagePassword = null;
      this.stb_SpliceConfirmedS2.Inemergency = false;
      this.stb_SpliceConfirmedS2.InLocal = false;
      this.stb_SpliceConfirmedS2.IsButton = true;
      this.stb_SpliceConfirmedS2.IsForceToInvisible = false;
      this.stb_SpliceConfirmedS2.Location = new System.Drawing.Point(998, 357);
      this.stb_SpliceConfirmedS2.Name = "stb_SpliceConfirmedS2";
      this.stb_SpliceConfirmedS2.OnLine = false;
      this.stb_SpliceConfirmedS2.ShowImage = false;
      this.stb_SpliceConfirmedS2.ShowText = true;
      this.stb_SpliceConfirmedS2.Size = new System.Drawing.Size(93, 70);
      this.stb_SpliceConfirmedS2.Switching = false;
      this.stb_SpliceConfirmedS2.TabIndex = 239;
      this.stb_SpliceConfirmedS2.Text = "No Sync";
      this.stb_SpliceConfirmedS2.TranslateText = false;
      this.stb_SpliceConfirmedS2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS2.UserDefinedActiveImage")));
      this.stb_SpliceConfirmedS2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS2.UserDefinedImage")));
      this.stb_SpliceConfirmedS2.Warned = false;
      this.stb_SpliceConfirmedS2.Click += new System.EventHandler(this.stb_SpliceConfirmedSX_Click);
      // 
      // stb_SpliceConfirmedS3
      // 
      this.stb_SpliceConfirmedS3.Active = false;
      this.stb_SpliceConfirmedS3.Alarmed = false;
      this.stb_SpliceConfirmedS3.Auto = false;
      this.stb_SpliceConfirmedS3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_SpliceConfirmedS3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SpliceConfirmedS3.FillBackColorWithImage = false;
      this.stb_SpliceConfirmedS3.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stb_SpliceConfirmedS3.ForeColor = System.Drawing.Color.Black;
      this.stb_SpliceConfirmedS3.FramePresent = true;
      this.stb_SpliceConfirmedS3.HighLightEnable = false;
      this.stb_SpliceConfirmedS3.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_SpliceConfirmedS3.ImagePassword = null;
      this.stb_SpliceConfirmedS3.Inemergency = false;
      this.stb_SpliceConfirmedS3.InLocal = false;
      this.stb_SpliceConfirmedS3.IsButton = true;
      this.stb_SpliceConfirmedS3.IsForceToInvisible = false;
      this.stb_SpliceConfirmedS3.Location = new System.Drawing.Point(888, 357);
      this.stb_SpliceConfirmedS3.Name = "stb_SpliceConfirmedS3";
      this.stb_SpliceConfirmedS3.OnLine = false;
      this.stb_SpliceConfirmedS3.ShowImage = false;
      this.stb_SpliceConfirmedS3.ShowText = true;
      this.stb_SpliceConfirmedS3.Size = new System.Drawing.Size(93, 70);
      this.stb_SpliceConfirmedS3.Switching = false;
      this.stb_SpliceConfirmedS3.TabIndex = 240;
      this.stb_SpliceConfirmedS3.Text = "No Sync";
      this.stb_SpliceConfirmedS3.TranslateText = false;
      this.stb_SpliceConfirmedS3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS3.UserDefinedActiveImage")));
      this.stb_SpliceConfirmedS3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS3.UserDefinedImage")));
      this.stb_SpliceConfirmedS3.Warned = false;
      this.stb_SpliceConfirmedS3.Click += new System.EventHandler(this.stb_SpliceConfirmedSX_Click);
      // 
      // stb_SpliceConfirmedS4
      // 
      this.stb_SpliceConfirmedS4.Active = false;
      this.stb_SpliceConfirmedS4.Alarmed = false;
      this.stb_SpliceConfirmedS4.Auto = false;
      this.stb_SpliceConfirmedS4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_SpliceConfirmedS4.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SpliceConfirmedS4.FillBackColorWithImage = false;
      this.stb_SpliceConfirmedS4.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stb_SpliceConfirmedS4.ForeColor = System.Drawing.Color.Black;
      this.stb_SpliceConfirmedS4.FramePresent = true;
      this.stb_SpliceConfirmedS4.HighLightEnable = false;
      this.stb_SpliceConfirmedS4.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_SpliceConfirmedS4.ImagePassword = null;
      this.stb_SpliceConfirmedS4.Inemergency = false;
      this.stb_SpliceConfirmedS4.InLocal = false;
      this.stb_SpliceConfirmedS4.IsButton = true;
      this.stb_SpliceConfirmedS4.IsForceToInvisible = false;
      this.stb_SpliceConfirmedS4.Location = new System.Drawing.Point(778, 357);
      this.stb_SpliceConfirmedS4.Name = "stb_SpliceConfirmedS4";
      this.stb_SpliceConfirmedS4.OnLine = false;
      this.stb_SpliceConfirmedS4.ShowImage = false;
      this.stb_SpliceConfirmedS4.ShowText = true;
      this.stb_SpliceConfirmedS4.Size = new System.Drawing.Size(93, 70);
      this.stb_SpliceConfirmedS4.Switching = false;
      this.stb_SpliceConfirmedS4.TabIndex = 241;
      this.stb_SpliceConfirmedS4.Text = "No Sync";
      this.stb_SpliceConfirmedS4.TranslateText = false;
      this.stb_SpliceConfirmedS4.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS4.UserDefinedActiveImage")));
      this.stb_SpliceConfirmedS4.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS4.UserDefinedImage")));
      this.stb_SpliceConfirmedS4.Warned = false;
      this.stb_SpliceConfirmedS4.Click += new System.EventHandler(this.stb_SpliceConfirmedSX_Click);
      // 
      // stb_SpliceConfirmedS5
      // 
      this.stb_SpliceConfirmedS5.Active = false;
      this.stb_SpliceConfirmedS5.Alarmed = false;
      this.stb_SpliceConfirmedS5.Auto = false;
      this.stb_SpliceConfirmedS5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_SpliceConfirmedS5.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SpliceConfirmedS5.FillBackColorWithImage = false;
      this.stb_SpliceConfirmedS5.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stb_SpliceConfirmedS5.ForeColor = System.Drawing.Color.Black;
      this.stb_SpliceConfirmedS5.FramePresent = true;
      this.stb_SpliceConfirmedS5.HighLightEnable = false;
      this.stb_SpliceConfirmedS5.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_SpliceConfirmedS5.ImagePassword = null;
      this.stb_SpliceConfirmedS5.Inemergency = false;
      this.stb_SpliceConfirmedS5.InLocal = false;
      this.stb_SpliceConfirmedS5.IsButton = true;
      this.stb_SpliceConfirmedS5.IsForceToInvisible = false;
      this.stb_SpliceConfirmedS5.Location = new System.Drawing.Point(668, 357);
      this.stb_SpliceConfirmedS5.Name = "stb_SpliceConfirmedS5";
      this.stb_SpliceConfirmedS5.OnLine = false;
      this.stb_SpliceConfirmedS5.ShowImage = false;
      this.stb_SpliceConfirmedS5.ShowText = true;
      this.stb_SpliceConfirmedS5.Size = new System.Drawing.Size(93, 70);
      this.stb_SpliceConfirmedS5.Switching = false;
      this.stb_SpliceConfirmedS5.TabIndex = 242;
      this.stb_SpliceConfirmedS5.Text = "No Sync";
      this.stb_SpliceConfirmedS5.TranslateText = false;
      this.stb_SpliceConfirmedS5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS5.UserDefinedActiveImage")));
      this.stb_SpliceConfirmedS5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS5.UserDefinedImage")));
      this.stb_SpliceConfirmedS5.Warned = false;
      this.stb_SpliceConfirmedS5.Click += new System.EventHandler(this.stb_SpliceConfirmedSX_Click);
      // 
      // tnud_WetEndMetersRequired
      // 
      this.tnud_WetEndMetersRequired.DecimalPlaces = 0;
      this.tnud_WetEndMetersRequired.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WetEndMetersRequired.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_WetEndMetersRequired.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndMetersRequired.ImagePassword = null;
      this.tnud_WetEndMetersRequired.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WetEndMetersRequired.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_WetEndMetersRequired.Location = new System.Drawing.Point(237, 459);
      this.tnud_WetEndMetersRequired.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_WetEndMetersRequired.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WetEndMetersRequired.Name = "tnud_WetEndMetersRequired";
      this.tnud_WetEndMetersRequired.ReadOnly = true;
      this.tnud_WetEndMetersRequired.Size = new System.Drawing.Size(190, 64);
      this.tnud_WetEndMetersRequired.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_WetEndMetersRequired.TabIndex = 243;
      this.tnud_WetEndMetersRequired.TabStop = false;
      this.tnud_WetEndMetersRequired.Text = "Requested";
      this.tnud_WetEndMetersRequired.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndMetersRequired.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WetEndMetersRequired.TranslateText = true;
      this.tnud_WetEndMetersRequired.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_WetEndMetersRequired.Value = "587600";
      this.tnud_WetEndMetersRequired.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WetEndMetersRequired.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttl_StopTime
      // 
      this.ttl_StopTime.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_StopTime.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_StopTime.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_StopTime.ImagePassword = null;
      this.ttl_StopTime.Location = new System.Drawing.Point(247, 287);
      this.ttl_StopTime.Name = "ttl_StopTime";
      this.ttl_StopTime.Size = new System.Drawing.Size(180, 64);
      this.ttl_StopTime.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_StopTime.TabIndex = 248;
      this.ttl_StopTime.TabStop = false;
      this.ttl_StopTime.Text = "Stop time";
      this.ttl_StopTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_StopTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_StopTime.TranslateText = true;
      this.ttl_StopTime.Value = "00:00:00";
      this.ttl_StopTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_StopTime.ValueMultiline = true;
      // 
      // lbl_ShiftID
      // 
      this.lbl_ShiftID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
      this.lbl_ShiftID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.lbl_ShiftID.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Bold);
      this.lbl_ShiftID.ForeColor = System.Drawing.Color.Yellow;
      this.lbl_ShiftID.Location = new System.Drawing.Point(160, 193);
      this.lbl_ShiftID.Name = "lbl_ShiftID";
      this.lbl_ShiftID.Size = new System.Drawing.Size(52, 19);
      this.lbl_ShiftID.TabIndex = 247;
      this.lbl_ShiftID.Text = "A";
      this.lbl_ShiftID.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // tmt_ShiftStartTime
      // 
      this.tmt_ShiftStartTime.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_ShiftStartTime.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tmt_ShiftStartTime.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_ShiftStartTime.ImagePassword = null;
      this.tmt_ShiftStartTime.Location = new System.Drawing.Point(37, 217);
      this.tmt_ShiftStartTime.Name = "tmt_ShiftStartTime";
      this.tmt_ShiftStartTime.PasswordChar = '\0';
      this.tmt_ShiftStartTime.ReadOnly = true;
      this.tmt_ShiftStartTime.Size = new System.Drawing.Size(391, 64);
      this.tmt_ShiftStartTime.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_ShiftStartTime.TabIndex = 246;
      this.tmt_ShiftStartTime.TabStop = false;
      this.tmt_ShiftStartTime.Text = "Shift Start Time";
      this.tmt_ShiftStartTime.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.tmt_ShiftStartTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_ShiftStartTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_ShiftStartTime.TranslateText = true;
      this.tmt_ShiftStartTime.Value = " ";
      this.tmt_ShiftStartTime.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.tmt_ShiftStartTime.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_ShiftStartTime.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.tmt_ShiftStartTime.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_ShiftStartTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // title2
      // 
      this.title2.BackColor = System.Drawing.Color.Black;
      this.title2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title2.Location = new System.Drawing.Point(30, 193);
      this.title2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
      this.title2.Name = "title2";
      this.title2.Size = new System.Drawing.Size(397, 159);
      this.title2.Style = SyncroC.Controls.TitleStyles.Small;
      this.title2.TabIndex = 245;
      this.title2.TabStop = false;
      this.title2.Text = "SHIFT";
      this.title2.TranslateText = true;
      // 
      // tnud_RemainingProductionS5
      // 
      this.tnud_RemainingProductionS5.DecimalPlaces = 0;
      this.tnud_RemainingProductionS5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_RemainingProductionS5.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_RemainingProductionS5.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS5.ImagePassword = null;
      this.tnud_RemainingProductionS5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_RemainingProductionS5.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_RemainingProductionS5.Location = new System.Drawing.Point(666, 711);
      this.tnud_RemainingProductionS5.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_RemainingProductionS5.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_RemainingProductionS5.Name = "tnud_RemainingProductionS5";
      this.tnud_RemainingProductionS5.ReadOnly = true;
      this.tnud_RemainingProductionS5.Size = new System.Drawing.Size(95, 20);
      this.tnud_RemainingProductionS5.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_RemainingProductionS5.TabIndex = 249;
      this.tnud_RemainingProductionS5.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS5.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_RemainingProductionS5.TranslateText = false;
      this.tnud_RemainingProductionS5.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_RemainingProductionS5.Value = "1234567000";
      this.tnud_RemainingProductionS5.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS5.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_RemainingProductionS4
      // 
      this.tnud_RemainingProductionS4.DecimalPlaces = 0;
      this.tnud_RemainingProductionS4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_RemainingProductionS4.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_RemainingProductionS4.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS4.ImagePassword = null;
      this.tnud_RemainingProductionS4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_RemainingProductionS4.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_RemainingProductionS4.Location = new System.Drawing.Point(776, 711);
      this.tnud_RemainingProductionS4.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_RemainingProductionS4.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_RemainingProductionS4.Name = "tnud_RemainingProductionS4";
      this.tnud_RemainingProductionS4.ReadOnly = true;
      this.tnud_RemainingProductionS4.Size = new System.Drawing.Size(95, 20);
      this.tnud_RemainingProductionS4.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_RemainingProductionS4.TabIndex = 250;
      this.tnud_RemainingProductionS4.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_RemainingProductionS4.TranslateText = false;
      this.tnud_RemainingProductionS4.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_RemainingProductionS4.Value = "1234567000";
      this.tnud_RemainingProductionS4.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS4.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_RemainingProductionS3
      // 
      this.tnud_RemainingProductionS3.DecimalPlaces = 0;
      this.tnud_RemainingProductionS3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_RemainingProductionS3.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_RemainingProductionS3.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS3.ImagePassword = null;
      this.tnud_RemainingProductionS3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_RemainingProductionS3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_RemainingProductionS3.Location = new System.Drawing.Point(886, 711);
      this.tnud_RemainingProductionS3.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_RemainingProductionS3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_RemainingProductionS3.Name = "tnud_RemainingProductionS3";
      this.tnud_RemainingProductionS3.ReadOnly = true;
      this.tnud_RemainingProductionS3.Size = new System.Drawing.Size(95, 20);
      this.tnud_RemainingProductionS3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_RemainingProductionS3.TabIndex = 251;
      this.tnud_RemainingProductionS3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_RemainingProductionS3.TranslateText = false;
      this.tnud_RemainingProductionS3.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_RemainingProductionS3.Value = "1234567000";
      this.tnud_RemainingProductionS3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_RemainingProductionS2
      // 
      this.tnud_RemainingProductionS2.DecimalPlaces = 0;
      this.tnud_RemainingProductionS2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_RemainingProductionS2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_RemainingProductionS2.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS2.ImagePassword = null;
      this.tnud_RemainingProductionS2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_RemainingProductionS2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_RemainingProductionS2.Location = new System.Drawing.Point(996, 711);
      this.tnud_RemainingProductionS2.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_RemainingProductionS2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_RemainingProductionS2.Name = "tnud_RemainingProductionS2";
      this.tnud_RemainingProductionS2.ReadOnly = true;
      this.tnud_RemainingProductionS2.Size = new System.Drawing.Size(95, 20);
      this.tnud_RemainingProductionS2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_RemainingProductionS2.TabIndex = 252;
      this.tnud_RemainingProductionS2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_RemainingProductionS2.TranslateText = false;
      this.tnud_RemainingProductionS2.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_RemainingProductionS2.Value = "1234567000";
      this.tnud_RemainingProductionS2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_RemainingProductionS1
      // 
      this.tnud_RemainingProductionS1.DecimalPlaces = 0;
      this.tnud_RemainingProductionS1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_RemainingProductionS1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_RemainingProductionS1.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS1.ImagePassword = null;
      this.tnud_RemainingProductionS1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_RemainingProductionS1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_RemainingProductionS1.Location = new System.Drawing.Point(1106, 711);
      this.tnud_RemainingProductionS1.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_RemainingProductionS1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_RemainingProductionS1.Name = "tnud_RemainingProductionS1";
      this.tnud_RemainingProductionS1.ReadOnly = true;
      this.tnud_RemainingProductionS1.Size = new System.Drawing.Size(95, 20);
      this.tnud_RemainingProductionS1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_RemainingProductionS1.TabIndex = 253;
      this.tnud_RemainingProductionS1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_RemainingProductionS1.TranslateText = false;
      this.tnud_RemainingProductionS1.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_RemainingProductionS1.Value = "1234567000";
      this.tnud_RemainingProductionS1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tlb_WetEndNote
      // 
      this.tlb_WetEndNote.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_WetEndNote.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_WetEndNote.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_WetEndNote.ImagePassword = null;
      this.tlb_WetEndNote.Location = new System.Drawing.Point(37, 529);
      this.tlb_WetEndNote.Name = "tlb_WetEndNote";
      this.tlb_WetEndNote.Size = new System.Drawing.Size(388, 75);
      this.tlb_WetEndNote.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_WetEndNote.TabIndex = 254;
      this.tlb_WetEndNote.TabStop = false;
      this.tlb_WetEndNote.Text = "Note";
      this.tlb_WetEndNote.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_WetEndNote.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_WetEndNote.TranslateText = true;
      this.tlb_WetEndNote.Value = "Ciao a tutti queste sono note per produrre una buona qualità";
      this.tlb_WetEndNote.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_WetEndNote.ValueMultiline = true;
      // 
      // tlb_ShiftMetresProduced
      // 
      this.tlb_ShiftMetresProduced.DecimalPlaces = 0;
      this.tlb_ShiftMetresProduced.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_ShiftMetresProduced.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tlb_ShiftMetresProduced.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ShiftMetresProduced.ImagePassword = null;
      this.tlb_ShiftMetresProduced.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tlb_ShiftMetresProduced.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_ShiftMetresProduced.Location = new System.Drawing.Point(37, 288);
      this.tlb_ShiftMetresProduced.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tlb_ShiftMetresProduced.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tlb_ShiftMetresProduced.Name = "tlb_ShiftMetresProduced";
      this.tlb_ShiftMetresProduced.ReadOnly = true;
      this.tlb_ShiftMetresProduced.Size = new System.Drawing.Size(204, 64);
      this.tlb_ShiftMetresProduced.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tlb_ShiftMetresProduced.TabIndex = 255;
      this.tlb_ShiftMetresProduced.TabStop = false;
      this.tlb_ShiftMetresProduced.Text = "Produced";
      this.tlb_ShiftMetresProduced.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ShiftMetresProduced.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ShiftMetresProduced.TranslateText = true;
      this.tlb_ShiftMetresProduced.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_ShiftMetresProduced.Value = "3542";
      this.tlb_ShiftMetresProduced.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ShiftMetresProduced.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ToggleAutoManS7
      // 
      this.ToggleAutoManS7.Active = false;
      this.ToggleAutoManS7.Alarmed = false;
      this.ToggleAutoManS7.Auto = false;
      this.ToggleAutoManS7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManS7.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManS7.FillBackColorWithImage = false;
      this.ToggleAutoManS7.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManS7.FramePresent = true;
      this.ToggleAutoManS7.HighLightEnable = false;
      this.ToggleAutoManS7.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManS7.ImagePassword = null;
      this.ToggleAutoManS7.Inemergency = false;
      this.ToggleAutoManS7.InLocal = false;
      this.ToggleAutoManS7.IsButton = true;
      this.ToggleAutoManS7.IsForceToInvisible = false;
      this.ToggleAutoManS7.Location = new System.Drawing.Point(448, 281);
      this.ToggleAutoManS7.Name = "ToggleAutoManS7";
      this.ToggleAutoManS7.OnLine = false;
      this.ToggleAutoManS7.ShowImage = true;
      this.ToggleAutoManS7.ShowText = false;
      this.ToggleAutoManS7.Size = new System.Drawing.Size(93, 70);
      this.ToggleAutoManS7.Switching = false;
      this.ToggleAutoManS7.TabIndex = 256;
      this.ToggleAutoManS7.TranslateText = false;
      this.ToggleAutoManS7.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS7.UserDefinedActiveImage")));
      this.ToggleAutoManS7.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS7.UserDefinedImage")));
      this.ToggleAutoManS7.Warned = false;
      this.ToggleAutoManS7.Click += new System.EventHandler(this.ToggleAutoManSX_Click);
      // 
      // ToggleAutoManS6
      // 
      this.ToggleAutoManS6.Active = false;
      this.ToggleAutoManS6.Alarmed = false;
      this.ToggleAutoManS6.Auto = false;
      this.ToggleAutoManS6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoManS6.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoManS6.FillBackColorWithImage = false;
      this.ToggleAutoManS6.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoManS6.FramePresent = true;
      this.ToggleAutoManS6.HighLightEnable = false;
      this.ToggleAutoManS6.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoManS6.ImagePassword = null;
      this.ToggleAutoManS6.Inemergency = false;
      this.ToggleAutoManS6.InLocal = false;
      this.ToggleAutoManS6.IsButton = true;
      this.ToggleAutoManS6.IsForceToInvisible = false;
      this.ToggleAutoManS6.Location = new System.Drawing.Point(558, 281);
      this.ToggleAutoManS6.Name = "ToggleAutoManS6";
      this.ToggleAutoManS6.OnLine = false;
      this.ToggleAutoManS6.ShowImage = true;
      this.ToggleAutoManS6.ShowText = false;
      this.ToggleAutoManS6.Size = new System.Drawing.Size(93, 70);
      this.ToggleAutoManS6.Switching = false;
      this.ToggleAutoManS6.TabIndex = 257;
      this.ToggleAutoManS6.TranslateText = false;
      this.ToggleAutoManS6.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS6.UserDefinedActiveImage")));
      this.ToggleAutoManS6.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoManS6.UserDefinedImage")));
      this.ToggleAutoManS6.Warned = false;
      this.ToggleAutoManS6.Click += new System.EventHandler(this.ToggleAutoManSX_Click);
      // 
      // chk_S6Ready
      // 
      this.chk_S6Ready.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S6Ready.Checked = false;
      this.chk_S6Ready.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S6Ready.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S6Ready.ImagePassword = null;
      this.chk_S6Ready.Location = new System.Drawing.Point(558, 216);
      this.chk_S6Ready.Name = "chk_S6Ready";
      this.chk_S6Ready.ReadOnly = true;
      this.chk_S6Ready.Size = new System.Drawing.Size(93, 54);
      this.chk_S6Ready.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_S6Ready.TabIndex = 258;
      this.chk_S6Ready.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S6Ready.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_S6Ready.TranslateText = false;
      // 
      // chk_S7Ready
      // 
      this.chk_S7Ready.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S7Ready.Checked = false;
      this.chk_S7Ready.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S7Ready.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S7Ready.ImagePassword = null;
      this.chk_S7Ready.Location = new System.Drawing.Point(448, 216);
      this.chk_S7Ready.Name = "chk_S7Ready";
      this.chk_S7Ready.ReadOnly = true;
      this.chk_S7Ready.Size = new System.Drawing.Size(93, 54);
      this.chk_S7Ready.Style = SyncroC.Controls.CheckBoxStyles.NoTitle;
      this.chk_S7Ready.TabIndex = 259;
      this.chk_S7Ready.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S7Ready.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_S7Ready.TranslateText = false;
      // 
      // stb_SpliceConfirmedS6
      // 
      this.stb_SpliceConfirmedS6.Active = false;
      this.stb_SpliceConfirmedS6.Alarmed = false;
      this.stb_SpliceConfirmedS6.Auto = false;
      this.stb_SpliceConfirmedS6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_SpliceConfirmedS6.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SpliceConfirmedS6.FillBackColorWithImage = false;
      this.stb_SpliceConfirmedS6.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stb_SpliceConfirmedS6.ForeColor = System.Drawing.Color.Black;
      this.stb_SpliceConfirmedS6.FramePresent = true;
      this.stb_SpliceConfirmedS6.HighLightEnable = false;
      this.stb_SpliceConfirmedS6.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_SpliceConfirmedS6.ImagePassword = null;
      this.stb_SpliceConfirmedS6.Inemergency = false;
      this.stb_SpliceConfirmedS6.InLocal = false;
      this.stb_SpliceConfirmedS6.IsButton = true;
      this.stb_SpliceConfirmedS6.IsForceToInvisible = false;
      this.stb_SpliceConfirmedS6.Location = new System.Drawing.Point(558, 357);
      this.stb_SpliceConfirmedS6.Name = "stb_SpliceConfirmedS6";
      this.stb_SpliceConfirmedS6.OnLine = false;
      this.stb_SpliceConfirmedS6.ShowImage = false;
      this.stb_SpliceConfirmedS6.ShowText = true;
      this.stb_SpliceConfirmedS6.Size = new System.Drawing.Size(93, 70);
      this.stb_SpliceConfirmedS6.Switching = false;
      this.stb_SpliceConfirmedS6.TabIndex = 260;
      this.stb_SpliceConfirmedS6.Text = "No Sync";
      this.stb_SpliceConfirmedS6.TranslateText = false;
      this.stb_SpliceConfirmedS6.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS6.UserDefinedActiveImage")));
      this.stb_SpliceConfirmedS6.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS6.UserDefinedImage")));
      this.stb_SpliceConfirmedS6.Warned = false;
      this.stb_SpliceConfirmedS6.Click += new System.EventHandler(this.stb_SpliceConfirmedSX_Click);
      // 
      // stb_SpliceConfirmedS7
      // 
      this.stb_SpliceConfirmedS7.Active = false;
      this.stb_SpliceConfirmedS7.Alarmed = false;
      this.stb_SpliceConfirmedS7.Auto = false;
      this.stb_SpliceConfirmedS7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_SpliceConfirmedS7.Cursor = System.Windows.Forms.Cursors.Hand;
      this.stb_SpliceConfirmedS7.FillBackColorWithImage = false;
      this.stb_SpliceConfirmedS7.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stb_SpliceConfirmedS7.ForeColor = System.Drawing.Color.Black;
      this.stb_SpliceConfirmedS7.FramePresent = true;
      this.stb_SpliceConfirmedS7.HighLightEnable = false;
      this.stb_SpliceConfirmedS7.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_SpliceConfirmedS7.ImagePassword = null;
      this.stb_SpliceConfirmedS7.Inemergency = false;
      this.stb_SpliceConfirmedS7.InLocal = false;
      this.stb_SpliceConfirmedS7.IsButton = true;
      this.stb_SpliceConfirmedS7.IsForceToInvisible = false;
      this.stb_SpliceConfirmedS7.Location = new System.Drawing.Point(448, 357);
      this.stb_SpliceConfirmedS7.Name = "stb_SpliceConfirmedS7";
      this.stb_SpliceConfirmedS7.OnLine = false;
      this.stb_SpliceConfirmedS7.ShowImage = false;
      this.stb_SpliceConfirmedS7.ShowText = true;
      this.stb_SpliceConfirmedS7.Size = new System.Drawing.Size(93, 70);
      this.stb_SpliceConfirmedS7.Switching = false;
      this.stb_SpliceConfirmedS7.TabIndex = 261;
      this.stb_SpliceConfirmedS7.Text = "No Sync";
      this.stb_SpliceConfirmedS7.TranslateText = false;
      this.stb_SpliceConfirmedS7.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS7.UserDefinedActiveImage")));
      this.stb_SpliceConfirmedS7.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_SpliceConfirmedS7.UserDefinedImage")));
      this.stb_SpliceConfirmedS7.Warned = false;
      this.stb_SpliceConfirmedS7.Click += new System.EventHandler(this.stb_SpliceConfirmedSX_Click);
      // 
      // ttl_Splicer6
      // 
      this.ttl_Splicer6.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer6.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer6.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer6.Location = new System.Drawing.Point(551, 193);
      this.ttl_Splicer6.Name = "ttl_Splicer6";
      this.ttl_Splicer6.Size = new System.Drawing.Size(100, 416);
      this.ttl_Splicer6.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer6.TabIndex = 262;
      this.ttl_Splicer6.Text = "Splicer 6";
      this.ttl_Splicer6.TranslateText = false;
      // 
      // ttl_Splicer7
      // 
      this.ttl_Splicer7.BackColor = System.Drawing.Color.Black;
      this.ttl_Splicer7.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Splicer7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Splicer7.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Splicer7.Location = new System.Drawing.Point(441, 193);
      this.ttl_Splicer7.Name = "ttl_Splicer7";
      this.ttl_Splicer7.Size = new System.Drawing.Size(100, 416);
      this.ttl_Splicer7.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Splicer7.TabIndex = 263;
      this.ttl_Splicer7.Text = "Splicer 7";
      this.ttl_Splicer7.TranslateText = false;
      // 
      // ttl_CurrentS7
      // 
      this.ttl_CurrentS7.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentS7.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS7.ImagePassword = null;
      this.ttl_CurrentS7.Location = new System.Drawing.Point(448, 430);
      this.ttl_CurrentS7.Name = "ttl_CurrentS7";
      this.ttl_CurrentS7.Size = new System.Drawing.Size(93, 86);
      this.ttl_CurrentS7.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_CurrentS7.TabIndex = 264;
      this.ttl_CurrentS7.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS7.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS7.TranslateText = false;
      this.ttl_CurrentS7.Value = "S7 current paper";
      this.ttl_CurrentS7.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS7.ValueMultiline = true;
      // 
      // ttl_CurrentS6
      // 
      this.ttl_CurrentS6.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentS6.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS6.ImagePassword = null;
      this.ttl_CurrentS6.Location = new System.Drawing.Point(558, 430);
      this.ttl_CurrentS6.Name = "ttl_CurrentS6";
      this.ttl_CurrentS6.Size = new System.Drawing.Size(93, 86);
      this.ttl_CurrentS6.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_CurrentS6.TabIndex = 265;
      this.ttl_CurrentS6.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS6.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS6.TranslateText = false;
      this.ttl_CurrentS6.Value = "S6 current paper";
      this.ttl_CurrentS6.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentS6.ValueMultiline = true;
      // 
      // ttl_NextS7
      // 
      this.ttl_NextS7.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextS7.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS7.ImagePassword = null;
      this.ttl_NextS7.Location = new System.Drawing.Point(448, 523);
      this.ttl_NextS7.Name = "ttl_NextS7";
      this.ttl_NextS7.Size = new System.Drawing.Size(93, 86);
      this.ttl_NextS7.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_NextS7.TabIndex = 266;
      this.ttl_NextS7.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS7.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS7.TranslateText = false;
      this.ttl_NextS7.Value = "S7 next paper";
      this.ttl_NextS7.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS7.ValueMultiline = true;
      // 
      // ttl_NextS6
      // 
      this.ttl_NextS6.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextS6.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS6.ImagePassword = null;
      this.ttl_NextS6.Location = new System.Drawing.Point(558, 523);
      this.ttl_NextS6.Name = "ttl_NextS6";
      this.ttl_NextS6.Size = new System.Drawing.Size(93, 86);
      this.ttl_NextS6.Style = SyncroC.Controls.TitledLabelStyles.NoTitle;
      this.ttl_NextS6.TabIndex = 267;
      this.ttl_NextS6.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS6.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS6.TranslateText = false;
      this.ttl_NextS6.Value = "S6 next paper";
      this.ttl_NextS6.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextS6.ValueMultiline = true;
      // 
      // SyncOnSplicer7
      // 
      this.SyncOnSplicer7.Active = false;
      this.SyncOnSplicer7.Alarmed = false;
      this.SyncOnSplicer7.Auto = false;
      this.SyncOnSplicer7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SyncOnSplicer7.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SyncOnSplicer7.FillBackColorWithImage = false;
      this.SyncOnSplicer7.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SyncOnSplicer7.ForeColor = System.Drawing.Color.Black;
      this.SyncOnSplicer7.FramePresent = true;
      this.SyncOnSplicer7.HighLightEnable = false;
      this.SyncOnSplicer7.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SyncOnSplicer7.ImagePassword = null;
      this.SyncOnSplicer7.Inemergency = false;
      this.SyncOnSplicer7.InLocal = false;
      this.SyncOnSplicer7.IsButton = true;
      this.SyncOnSplicer7.IsForceToInvisible = false;
      this.SyncOnSplicer7.Location = new System.Drawing.Point(441, 645);
      this.SyncOnSplicer7.Name = "SyncOnSplicer7";
      this.SyncOnSplicer7.OnLine = false;
      this.SyncOnSplicer7.ShowImage = false;
      this.SyncOnSplicer7.ShowText = true;
      this.SyncOnSplicer7.Size = new System.Drawing.Size(100, 70);
      this.SyncOnSplicer7.Switching = false;
      this.SyncOnSplicer7.TabIndex = 268;
      this.SyncOnSplicer7.Text = "Sync on Splicer 7";
      this.SyncOnSplicer7.TranslateText = true;
      this.SyncOnSplicer7.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer7.UserDefinedActiveImage")));
      this.SyncOnSplicer7.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer7.UserDefinedImage")));
      this.SyncOnSplicer7.Warned = false;
      this.SyncOnSplicer7.Click += new System.EventHandler(this.SyncOnSplicer7_Click);
      // 
      // SyncOnSplicer6
      // 
      this.SyncOnSplicer6.Active = false;
      this.SyncOnSplicer6.Alarmed = false;
      this.SyncOnSplicer6.Auto = false;
      this.SyncOnSplicer6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SyncOnSplicer6.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SyncOnSplicer6.FillBackColorWithImage = false;
      this.SyncOnSplicer6.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SyncOnSplicer6.ForeColor = System.Drawing.Color.Black;
      this.SyncOnSplicer6.FramePresent = true;
      this.SyncOnSplicer6.HighLightEnable = false;
      this.SyncOnSplicer6.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SyncOnSplicer6.ImagePassword = null;
      this.SyncOnSplicer6.Inemergency = false;
      this.SyncOnSplicer6.InLocal = false;
      this.SyncOnSplicer6.IsButton = true;
      this.SyncOnSplicer6.IsForceToInvisible = false;
      this.SyncOnSplicer6.Location = new System.Drawing.Point(551, 645);
      this.SyncOnSplicer6.Name = "SyncOnSplicer6";
      this.SyncOnSplicer6.OnLine = false;
      this.SyncOnSplicer6.ShowImage = false;
      this.SyncOnSplicer6.ShowText = true;
      this.SyncOnSplicer6.Size = new System.Drawing.Size(100, 70);
      this.SyncOnSplicer6.Switching = false;
      this.SyncOnSplicer6.TabIndex = 269;
      this.SyncOnSplicer6.Text = "Sync on Splicer 6";
      this.SyncOnSplicer6.TranslateText = true;
      this.SyncOnSplicer6.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer6.UserDefinedActiveImage")));
      this.SyncOnSplicer6.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SyncOnSplicer6.UserDefinedImage")));
      this.SyncOnSplicer6.Warned = false;
      this.SyncOnSplicer6.Click += new System.EventHandler(this.SyncOnSplicer6_Click);
      // 
      // tnud_RemainingProductionS7
      // 
      this.tnud_RemainingProductionS7.DecimalPlaces = 0;
      this.tnud_RemainingProductionS7.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_RemainingProductionS7.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_RemainingProductionS7.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS7.ImagePassword = null;
      this.tnud_RemainingProductionS7.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_RemainingProductionS7.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_RemainingProductionS7.Location = new System.Drawing.Point(446, 711);
      this.tnud_RemainingProductionS7.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_RemainingProductionS7.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_RemainingProductionS7.Name = "tnud_RemainingProductionS7";
      this.tnud_RemainingProductionS7.ReadOnly = true;
      this.tnud_RemainingProductionS7.Size = new System.Drawing.Size(95, 20);
      this.tnud_RemainingProductionS7.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_RemainingProductionS7.TabIndex = 270;
      this.tnud_RemainingProductionS7.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS7.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_RemainingProductionS7.TranslateText = false;
      this.tnud_RemainingProductionS7.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_RemainingProductionS7.Value = "1234567000";
      this.tnud_RemainingProductionS7.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS7.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_RemainingProductionS6
      // 
      this.tnud_RemainingProductionS6.DecimalPlaces = 0;
      this.tnud_RemainingProductionS6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_RemainingProductionS6.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tnud_RemainingProductionS6.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS6.ImagePassword = null;
      this.tnud_RemainingProductionS6.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_RemainingProductionS6.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_RemainingProductionS6.Location = new System.Drawing.Point(556, 711);
      this.tnud_RemainingProductionS6.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_RemainingProductionS6.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_RemainingProductionS6.Name = "tnud_RemainingProductionS6";
      this.tnud_RemainingProductionS6.ReadOnly = true;
      this.tnud_RemainingProductionS6.Size = new System.Drawing.Size(95, 20);
      this.tnud_RemainingProductionS6.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_RemainingProductionS6.TabIndex = 271;
      this.tnud_RemainingProductionS6.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS6.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_RemainingProductionS6.TranslateText = false;
      this.tnud_RemainingProductionS6.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_RemainingProductionS6.Value = "1234567000";
      this.tnud_RemainingProductionS6.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_RemainingProductionS6.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // Frm_HomeWet
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tnud_RemainingProductionS6);
      this.Controls.Add(this.tnud_RemainingProductionS7);
      this.Controls.Add(this.SyncOnSplicer6);
      this.Controls.Add(this.SyncOnSplicer7);
      this.Controls.Add(this.ttl_NextS6);
      this.Controls.Add(this.ttl_NextS7);
      this.Controls.Add(this.ttl_CurrentS6);
      this.Controls.Add(this.ttl_CurrentS7);
      this.Controls.Add(this.ttl_Splicer7);
      this.Controls.Add(this.ttl_Splicer6);
      this.Controls.Add(this.stb_SpliceConfirmedS7);
      this.Controls.Add(this.stb_SpliceConfirmedS6);
      this.Controls.Add(this.chk_S7Ready);
      this.Controls.Add(this.chk_S6Ready);
      this.Controls.Add(this.ToggleAutoManS6);
      this.Controls.Add(this.ToggleAutoManS7);
      this.Controls.Add(this.tlb_ShiftMetresProduced);
      this.Controls.Add(this.tlb_WetEndNote);
      this.Controls.Add(this.SyncOnSplicer5);
      this.Controls.Add(this.tnud_RemainingProductionS5);
      this.Controls.Add(this.ttl_StopTime);
      this.Controls.Add(this.lbl_ShiftID);
      this.Controls.Add(this.tmt_ShiftStartTime);
      this.Controls.Add(this.title2);
      this.Controls.Add(this.tnud_WetEndMetersRequired);
      this.Controls.Add(this.stb_SpliceConfirmedS1);
      this.Controls.Add(this.stb_SpliceConfirmedS2);
      this.Controls.Add(this.stb_SpliceConfirmedS3);
      this.Controls.Add(this.stb_SpliceConfirmedS4);
      this.Controls.Add(this.stb_SpliceConfirmedS5);
      this.Controls.Add(this.tnud_WetEndMetersProduced);
      this.Controls.Add(this.tmt_WetEndStartTime);
      this.Controls.Add(this.ttl_WetEnd);
      this.Controls.Add(this.ToggleAutoManS1);
      this.Controls.Add(this.ToggleAutoManS2);
      this.Controls.Add(this.ToggleAutoManS3);
      this.Controls.Add(this.ToggleAutoManS4);
      this.Controls.Add(this.chk_S5Ready);
      this.Controls.Add(this.ttl_Splicer5);
      this.Controls.Add(this.chk_S4Ready);
      this.Controls.Add(this.ttl_Splicer4);
      this.Controls.Add(this.chk_S3Ready);
      this.Controls.Add(this.ttl_Splicer3);
      this.Controls.Add(this.chk_S2Ready);
      this.Controls.Add(this.ttl_Splicer2);
      this.Controls.Add(this.chk_S1Ready);
      this.Controls.Add(this.ttl_Splicer1);
      this.Controls.Add(this.SyncOnSplicer4);
      this.Controls.Add(this.SyncOnSplicer3);
      this.Controls.Add(this.SyncOnSplicer2);
      this.Controls.Add(this.SyncOnSplicer1);
      this.Controls.Add(this.SyncNow);
      this.Controls.Add(this.SyncOnMetres);
      this.Controls.Add(this.ttl_Syncronization);
      this.Controls.Add(this.SyncIdle);
      this.Controls.Add(this.ToggleAutoManS5);
      this.Controls.Add(this.ttl_CurrentS5);
      this.Controls.Add(this.ttl_NextS5);
      this.Controls.Add(this.ttl_NextS4);
      this.Controls.Add(this.ttl_NextS1);
      this.Controls.Add(this.ttl_NextS2);
      this.Controls.Add(this.ttl_NextS3);
      this.Controls.Add(this.ttl_CurrentS4);
      this.Controls.Add(this.ttl_CurrentS1);
      this.Controls.Add(this.ttl_CurrentS2);
      this.Controls.Add(this.ttl_CurrentS3);
      this.Controls.Add(this.tnud_RemainingProductionS4);
      this.Controls.Add(this.tnud_RemainingProductionS3);
      this.Controls.Add(this.tnud_RemainingProductionS2);
      this.Controls.Add(this.tnud_RemainingProductionS1);
      this.Name = "Frm_HomeWet";
      this.Text = "Frm_HomeWet";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.tnud_RemainingProductionS1, 0);
      this.Controls.SetChildIndex(this.tnud_RemainingProductionS2, 0);
      this.Controls.SetChildIndex(this.tnud_RemainingProductionS3, 0);
      this.Controls.SetChildIndex(this.tnud_RemainingProductionS4, 0);
      this.Controls.SetChildIndex(this.ttl_CurrentS3, 0);
      this.Controls.SetChildIndex(this.ttl_CurrentS2, 0);
      this.Controls.SetChildIndex(this.ttl_CurrentS1, 0);
      this.Controls.SetChildIndex(this.ttl_CurrentS4, 0);
      this.Controls.SetChildIndex(this.ttl_NextS3, 0);
      this.Controls.SetChildIndex(this.ttl_NextS2, 0);
      this.Controls.SetChildIndex(this.ttl_NextS1, 0);
      this.Controls.SetChildIndex(this.ttl_NextS4, 0);
      this.Controls.SetChildIndex(this.ttl_NextS5, 0);
      this.Controls.SetChildIndex(this.ttl_CurrentS5, 0);
      this.Controls.SetChildIndex(this.ToggleAutoManS5, 0);
      this.Controls.SetChildIndex(this.SyncIdle, 0);
      this.Controls.SetChildIndex(this.ttl_Syncronization, 0);
      this.Controls.SetChildIndex(this.SyncOnMetres, 0);
      this.Controls.SetChildIndex(this.SyncNow, 0);
      this.Controls.SetChildIndex(this.SyncOnSplicer1, 0);
      this.Controls.SetChildIndex(this.SyncOnSplicer2, 0);
      this.Controls.SetChildIndex(this.SyncOnSplicer3, 0);
      this.Controls.SetChildIndex(this.SyncOnSplicer4, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer1, 0);
      this.Controls.SetChildIndex(this.chk_S1Ready, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer2, 0);
      this.Controls.SetChildIndex(this.chk_S2Ready, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer3, 0);
      this.Controls.SetChildIndex(this.chk_S3Ready, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer4, 0);
      this.Controls.SetChildIndex(this.chk_S4Ready, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer5, 0);
      this.Controls.SetChildIndex(this.chk_S5Ready, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.lbl_Time, 0);
      this.Controls.SetChildIndex(this.lbl_Status, 0);
      this.Controls.SetChildIndex(this.tab_Tab1, 0);
      this.Controls.SetChildIndex(this.tab_Tab2, 0);
      this.Controls.SetChildIndex(this.tab_Tab3, 0);
      this.Controls.SetChildIndex(this.tab_Tab4, 0);
      this.Controls.SetChildIndex(this.tab_Tab5, 0);
      this.Controls.SetChildIndex(this.tab_Tab6, 0);
      this.Controls.SetChildIndex(this.tab_Tab7, 0);
      this.Controls.SetChildIndex(this.tab_Tab8, 0);
      this.Controls.SetChildIndex(this.tab_TabA, 0);
      this.Controls.SetChildIndex(this.tab_TabD, 0);
      this.Controls.SetChildIndex(this.tab_TabC, 0);
      this.Controls.SetChildIndex(this.tab_TabB, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.ToggleAutoManS4, 0);
      this.Controls.SetChildIndex(this.ToggleAutoManS3, 0);
      this.Controls.SetChildIndex(this.ToggleAutoManS2, 0);
      this.Controls.SetChildIndex(this.ToggleAutoManS1, 0);
      this.Controls.SetChildIndex(this.ttl_WetEnd, 0);
      this.Controls.SetChildIndex(this.tmt_WetEndStartTime, 0);
      this.Controls.SetChildIndex(this.tnud_WetEndMetersProduced, 0);
      this.Controls.SetChildIndex(this.stb_SpliceConfirmedS5, 0);
      this.Controls.SetChildIndex(this.stb_SpliceConfirmedS4, 0);
      this.Controls.SetChildIndex(this.stb_SpliceConfirmedS3, 0);
      this.Controls.SetChildIndex(this.stb_SpliceConfirmedS2, 0);
      this.Controls.SetChildIndex(this.stb_SpliceConfirmedS1, 0);
      this.Controls.SetChildIndex(this.tnud_WetEndMetersRequired, 0);
      this.Controls.SetChildIndex(this.title2, 0);
      this.Controls.SetChildIndex(this.tmt_ShiftStartTime, 0);
      this.Controls.SetChildIndex(this.lbl_ShiftID, 0);
      this.Controls.SetChildIndex(this.ttl_StopTime, 0);
      this.Controls.SetChildIndex(this.tnud_RemainingProductionS5, 0);
      this.Controls.SetChildIndex(this.SyncOnSplicer5, 0);
      this.Controls.SetChildIndex(this.tlb_WetEndNote, 0);
      this.Controls.SetChildIndex(this.tlb_ShiftMetresProduced, 0);
      this.Controls.SetChildIndex(this.ToggleAutoManS7, 0);
      this.Controls.SetChildIndex(this.ToggleAutoManS6, 0);
      this.Controls.SetChildIndex(this.chk_S6Ready, 0);
      this.Controls.SetChildIndex(this.chk_S7Ready, 0);
      this.Controls.SetChildIndex(this.stb_SpliceConfirmedS6, 0);
      this.Controls.SetChildIndex(this.stb_SpliceConfirmedS7, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer6, 0);
      this.Controls.SetChildIndex(this.ttl_Splicer7, 0);
      this.Controls.SetChildIndex(this.ttl_CurrentS7, 0);
      this.Controls.SetChildIndex(this.ttl_CurrentS6, 0);
      this.Controls.SetChildIndex(this.ttl_NextS7, 0);
      this.Controls.SetChildIndex(this.ttl_NextS6, 0);
      this.Controls.SetChildIndex(this.SyncOnSplicer7, 0);
      this.Controls.SetChildIndex(this.SyncOnSplicer6, 0);
      this.Controls.SetChildIndex(this.tnud_RemainingProductionS7, 0);
      this.Controls.SetChildIndex(this.tnud_RemainingProductionS6, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		private void SyncIdle_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.WEC + ".CancelSync()");//instead of select the SYNCMODE_IDLE we send a CancelSync command
		}

		private void SyncOnMetres_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.WEC + ".SyncMode(8)");//8 = SYNCMODE_METRES 
		}

		private void SyncNow_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

      //PMASSARO_FI25/05/10 - Aggiunta conferma su comando "giunta ora"
      /*
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.WEC + ".SyncMode(10)");//10 = SYNCMODE_NOW
       */
      bool ok = false;
      PopUp.Frm_Confirm conf = new Frm_Confirm();
      string txtKey = "§SYCC_199";
      string text = DBManager.getDBManager().message.getTranslatedMessage(txtKey);
      DialogResult res = conf.ShowDialog(this, text);
      ok = (res == DialogResult.Yes) || (res == DialogResult.OK);
      if (ok)
      {
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
          Command.WEC + ".SyncMode(10)");//10 = SYNCMODE_NOW
      }
		}

		private void SyncOnSplicer1_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.WEC + ".SyncMode(1)");//1 = SYNCMODE_SPLICER1 
		}

		private void SyncOnSplicer2_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.WEC + ".SyncMode(2)");//2 = SYNCMODE_SPLICER2
		}

		private void SyncOnSplicer3_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.WEC + ".SyncMode(3)");//3 = SYNCMODE_SPLICER3
		}

		private void SyncOnSplicer4_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.WEC + ".SyncMode(4)");//4 = SYNCMODE_SPLICER4
		}

		private void SyncOnSplicer5_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.WEC + ".SyncMode(5)");//5 = SYNCMODE_SPLICER5
		}

    //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
    private void SyncOnSplicer6_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
        return;

      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        Command.WEC + ".SyncMode(6)");//6 = SYNCMODE_SPLICER6
    }

    private void SyncOnSplicer7_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
        return;

      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        Command.WEC + ".SyncMode(7)");//7 = SYNCMODE_SPLICER7
    }

    public void SyncButtonsEnabled(bool enabled)
    {
      /*
       * enabled = (! wec syncModeChange)
       * SyncOnSplicerX must be (! wec syncModeChange) && SplicerX in auto
       */
      //ZETA 01
      SyncIdle.Enabled = true;
      SyncOnMetres.Enabled = enabled;
      SyncNow.Enabled = enabled;
      SyncOnSplicer1.Enabled = enabled && chk_S1Ready.Visible && ((!ttl_CurrentS1.Value.Trim().Equals(string.Empty) && ttl_NextS1.ValueBackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS1.Value.Trim().Equals(string.Empty) && ttl_NextS1.Value.Trim().Equals(string.Empty)));
      SyncOnSplicer2.Enabled = enabled && chk_S2Ready.Visible && ((!ttl_CurrentS2.Value.Trim().Equals(string.Empty) && ttl_NextS2.ValueBackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS2.Value.Trim().Equals(string.Empty) && ttl_NextS2.Value.Trim().Equals(string.Empty)));
      SyncOnSplicer3.Enabled = enabled && chk_S3Ready.Visible && ((!ttl_CurrentS3.Value.Trim().Equals(string.Empty) && ttl_NextS3.ValueBackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS3.Value.Trim().Equals(string.Empty) && ttl_NextS3.Value.Trim().Equals(string.Empty)));
      SyncOnSplicer4.Enabled = enabled && chk_S4Ready.Visible && ((!ttl_CurrentS4.Value.Trim().Equals(string.Empty) && ttl_NextS4.ValueBackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS4.Value.Trim().Equals(string.Empty) && ttl_NextS4.Value.Trim().Equals(string.Empty)));
      SyncOnSplicer5.Enabled = enabled && chk_S5Ready.Visible && ((!ttl_CurrentS5.Value.Trim().Equals(string.Empty) && ttl_NextS5.ValueBackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS5.Value.Trim().Equals(string.Empty) && ttl_NextS5.Value.Trim().Equals(string.Empty)));
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      SyncOnSplicer6.Enabled = enabled && chk_S4Ready.Visible && ((!ttl_CurrentS6.Value.Trim().Equals(string.Empty) && ttl_NextS6.ValueBackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS6.Value.Trim().Equals(string.Empty) && ttl_NextS6.Value.Trim().Equals(string.Empty)));
      SyncOnSplicer7.Enabled = enabled && chk_S5Ready.Visible && ((!ttl_CurrentS7.Value.Trim().Equals(string.Empty) && ttl_NextS7.ValueBackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS7.Value.Trim().Equals(string.Empty) && ttl_NextS7.Value.Trim().Equals(string.Empty)));

    }

    //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
    public void SyncButtonsActive(bool SyncIdleActive, bool SyncOnMetresActive, bool SyncNowActive, bool SyncOnSplicer1Active,
      bool SyncOnSplicer2Active, bool SyncOnSplicer3Active, bool SyncOnSplicer4Active, bool SyncOnSplicer5Active, bool SyncOnSplicer6Active, bool SyncOnSplicer7Active)
		{
			SyncIdle.Active = SyncIdleActive;
			SyncOnMetres.Active = SyncOnMetresActive;
			SyncNow.Active = SyncNowActive;
      SyncOnSplicer1.Active = SyncOnSplicer1Active;
      SyncOnSplicer2.Active = SyncOnSplicer2Active;
      SyncOnSplicer3.Active = SyncOnSplicer3Active;
      SyncOnSplicer4.Active = SyncOnSplicer4Active;
      SyncOnSplicer5.Active = SyncOnSplicer5Active;
      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      SyncOnSplicer6.Active = SyncOnSplicer6Active;
      SyncOnSplicer7.Active = SyncOnSplicer7Active;
    }

		private void ToggleAutoManSX_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			string senderName = ((Control)sender).Name;
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					Command.SPLICER + senderName[senderName.Length-1] + "." + Command.TOGGLE_AUTO_MAN + "()");
		}

		private void stb_SpliceConfirmedSX_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
				return;

			string senderName = ((Control)sender).Name;
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					Command.SPLICER + senderName[senderName.Length - 1] + "." + Command.TOGGLE_CONFIRM_SPLICE + "()");
		}

		//CM69 aggiunta produzione restante in bobina + bugFix per il cambio altezza(sostituire tutto il file)
		private void _setPaperTypeForSplicer(int splNumber, string paperTypeCurrent, int paperWeigthFactorCurrent, int paperWidthCurrent, int remainingProductionCurrent_UDmm, int productionUntilSplice_UDmm, string paperTypeNext, int paperWeigthFactorNext, int paperWidthNext)
		{
			SyncroC.Controls.TitledLabel CurrentS = null;
			SyncroC.Controls.TitledLabel NextS = null;
			SyncroC.Controls.TitledNumericUpDown remainingProductionS = null;

      //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
      switch (splNumber)
			{
				case 1:
					CurrentS = this.ttl_CurrentS1;
					NextS = this.ttl_NextS1;
					remainingProductionS = this.tnud_RemainingProductionS1;
					break;
				case 2:
					CurrentS = this.ttl_CurrentS2;
					NextS = this.ttl_NextS2;
					remainingProductionS = this.tnud_RemainingProductionS2;
					break;
				case 3:
					CurrentS = this.ttl_CurrentS3;
					NextS = this.ttl_NextS3;
					remainingProductionS = this.tnud_RemainingProductionS3;
					break;
				case 4:
					CurrentS = this.ttl_CurrentS4;
					NextS = this.ttl_NextS4;
					remainingProductionS = this.tnud_RemainingProductionS4;
					break;
				case 5:
					CurrentS = this.ttl_CurrentS5;
					NextS = this.ttl_NextS5;
					remainingProductionS = this.tnud_RemainingProductionS5;
					break;
        case 6:
          CurrentS = this.ttl_CurrentS6;
          NextS = this.ttl_NextS6;
          remainingProductionS = this.tnud_RemainingProductionS6;
          break;
        case 7:
          CurrentS = this.ttl_CurrentS7;
          NextS = this.ttl_NextS7;
          remainingProductionS = this.tnud_RemainingProductionS7;
          break;
        default:
					break;
			}

			//CM69 aggiunta la quantità di carta in bobina sulla home wet
			string paperTypeStr = paperTypeCurrent;
			if(paperWeigthFactorCurrent != 0)
				paperTypeStr = paperTypeStr + " (" + paperWeigthFactorCurrent.ToString() + ")";
			if(CurrentS != null)
				CurrentS.Value = paperTypeStr;

			paperTypeStr = paperTypeNext;
			if (paperWeigthFactorNext != 0)
				paperTypeStr = paperTypeStr + " (" + paperWeigthFactorNext.ToString() + ")";
			if (NextS != null)
			{
				NextS.Value = paperTypeStr;

				if ((paperTypeCurrent != paperTypeNext) || (paperWeigthFactorCurrent != paperWeigthFactorNext) || (paperWidthCurrent != paperWidthNext))
					NextS.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
				else
					NextS.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			}

			if (remainingProductionCurrent_UDmm == productionUntilSplice_UDmm)
			{
				remainingProductionS.Visible = false;
				remainingProductionS.Value = "";
			}
			else
			{
				if (remainingProductionS != null)
				{
					if (CurrentS.Value != "")
						remainingProductionS.Value = remainingProductionCurrent_UDmm.ToString();
					else
						remainingProductionS.Value = "";
					remainingProductionS.Visible = true;
				}
			}
		}

		public void _SetShiftStopTime(int seconds)
		{
			ttl_StopTime.Value = SyncroConverter.ConvertToFormattedTime(seconds);
		}

    public void _SetIsStopShift(bool isStopShift)
    {
      this.title2.BackColor = isStopShift ? Color.Red : Color.Black;
    }

	}
}

