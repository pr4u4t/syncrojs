using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for SingleFacer.
	/// </summary>
	public class GlueUnit : SyncroC.DataBase.LineConfig.LineBase
	{
    bool preHeaterAndGlueGapManualIfRemote;
    [Category("0. Generic"), PropertyOrder(10), Description("NON FOSBER PreHeaters and GlueGap if has no management LOCAL/REMOTE for preheater/GlueGap. Preheaters/GlueGap then be put in MANUAL if they are REMOTE.")]
    public bool PreHeaterAndGlueGapManualIfRemote
    {
      get { return preHeaterAndGlueGapManualIfRemote; }
      set { preHeaterAndGlueGapManualIfRemote = value; }
    }

		// *************************************  GLUE UNIT WRAP ARMS   ************************************
		WrapArm preheater21;
		[Category("2. GlueUnit WRAP-ARMS"), PropertyOrder(10), Description("Preheater21")]
		public WrapArm Preheater21
		{
			get { return preheater21; }
			set { preheater21 = value; }
		}

		WrapArm preheater4;
		[Category("2. GlueUnit WRAP-ARMS"), PropertyOrder(20), Description("Preheater4")]
		public WrapArm Preheater4
		{
			get { return preheater4; }
			set { preheater4 = value; }
		}

		WrapArm preheater5;
		[Category("2. GlueUnit WRAP-ARMS"), PropertyOrder(30), Description("Preheater5")]
		public WrapArm Preheater5
		{
			get { return preheater5; }
			set { preheater5 = value; }
		}

		WrapArm preheater6;
		[Category("2. GlueUnit WRAP-ARMS"), PropertyOrder(40), Description("Preheater6")]
		public WrapArm Preheater6
		{
			get { return preheater6; }
			set { preheater6 = value; }
		}

		WrapArm preheater51;
		[Category("2. GlueUnit WRAP-ARMS"), PropertyOrder(50), Description("Preheater51")]
		public WrapArm Preheater51
		{
			get { return preheater51; }
			set { preheater51 = value; }
		}

		WrapArm preheater40;
		[Category("2. GlueUnit WRAP-ARMS"), PropertyOrder(60), Description("Preheater40")]
		public WrapArm Preheater40
		{
			get { return preheater40; }
			set { preheater40 = value; }
		}

		WrapArm preheater50;
		[Category("2. GlueUnit WRAP-ARMS"), PropertyOrder(70), Description("Preheater50")]
		public WrapArm Preheater50
		{
			get { return preheater50; }
			set { preheater50 = value; }
		}

		WrapArm preheater60;
		[Category("2. GlueUnit WRAP-ARMS"), PropertyOrder(80), Description("Preheater60")]
		public WrapArm Preheater60
		{
			get { return preheater60; }
			set { preheater60 = value; }
		}

		// *************************************  GLUE UNIT GLUE GAPS   ************************************
		GlueGap gluegapG1;
		[Category("1. GlueUnit GLUE-GAPS"), PropertyOrder(10), Description("GluegapG1")]
		public GlueGap GluegapG1
		{
			get { return gluegapG1; }
			set { gluegapG1 = value; }
		}

		GlueGap gluegapG2;
		[Category("1. GlueUnit GLUE-GAPS"), PropertyOrder(20), Description("GluegapG2")]
		public GlueGap GluegapG2
		{
			get { return gluegapG2; }
			set { gluegapG2 = value; }
		}

		GlueGap gluegapG3;
		[Category("1. GlueUnit GLUE-GAPS"), PropertyOrder(30), Description("GluegapG3")]
		public GlueGap GluegapG3
		{
			get { return gluegapG3; }
			set { gluegapG3 = value; }
		}

		GlueGap gluegapG4;
		[Category("1. GlueUnit GLUE-GAPS"), PropertyOrder(40), Description("GluegapG4")]
		public GlueGap GluegapG4
		{
			get { return gluegapG4; }
			set { gluegapG4 = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public GlueUnit()
		{
			sectionName="GLUEUNIT";

      this.preHeaterAndGlueGapManualIfRemote = false;

			this.preheater21 = new WrapArm(21);
			this.preheater4 = new WrapArm(4);
			this.preheater5 = new WrapArm(5);
			this.preheater6 = new WrapArm(6);
			this.preheater51 = new WrapArm(51);
			this.preheater40 = new WrapArm(40);
			this.preheater50 = new WrapArm(50);
			this.preheater60 = new WrapArm(60);

			this.gluegapG1 = new GlueGap(1);
			this.gluegapG2 = new GlueGap(2);
			this.gluegapG3 = new GlueGap(3);
			this.gluegapG4 = new GlueGap(4);
		}
	}
}