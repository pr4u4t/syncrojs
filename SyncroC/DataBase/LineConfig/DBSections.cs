using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class DbHotSection : SyncroC.DataBase.LineConfig.LineBase
	{
		bool present;
		[PropertyOrder(10), Category("DBHotSection"), Description("True if this DB Hot Section are in the system.")]
		public bool Present
		{
			get { return present; }
			set { present = value; }
		}


		Regulation dbHotSectionRegulation;
		[Category("2. Regulation Config"), PropertyOrder(10), Description("DB Hot Section Regulation Config")]
		public Regulation DBHotSectionRegulation
		{
			get { return dbHotSectionRegulation; }
			set { dbHotSectionRegulation = value; }
		}

		ScaleConfig dbHotSectionScaleConfig;
		[Category("3. Scale Config"), PropertyOrder(10), Description("Db Hot Section Scaling Config")]
		public ScaleConfig DBHotSectionScaleConfig
		{
			get { return dbHotSectionScaleConfig; }
			set { dbHotSectionScaleConfig = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public DbHotSection(int dbHotSectionNumber)
			: base()
		{
			this.sectionName = "DbHotSection_" + dbHotSectionNumber.ToString();

			this.present = false;
			this.dbHotSectionRegulation = new Regulation("DbHotSectionRegulation_" + dbHotSectionNumber.ToString());
			this.dbHotSectionScaleConfig = new ScaleConfig("DbHotSectionScaleConfig_"+dbHotSectionNumber.ToString());
		}
	}


	[TypeConverter(typeof(Converters))]
	public class DbColdSection : SyncroC.DataBase.LineConfig.LineBase
	{
		bool present;
		[PropertyOrder(10), Category("DBColdSection"), Description("True if this DB Cold Section are in the system.")]
		public bool Present
		{
			get { return present; }
			set { present = value; }
		}


		Regulation dbColdSectionRegulation;
		[Category("Regulation Config"), PropertyOrder(10), Description("DB Cold Section Regulation Config")]
		public Regulation DBColdSectionRegulation
		{
			get { return dbColdSectionRegulation; }
			set { dbColdSectionRegulation = value; }
		}

		ScaleConfig dbColdSectionScaleConfig;
		[Category("Scale Config"), PropertyOrder(10), Description("Db Cold Section Scaling Config")]
		public ScaleConfig DBColdSectionScaleConfig
		{
			get { return dbColdSectionScaleConfig; }
			set { dbColdSectionScaleConfig = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public DbColdSection(int dbColdSectionNumber)
			: base()
		{
			this.sectionName = "DbColdSection_" + dbColdSectionNumber.ToString();

			this.present = false;
			this.dbColdSectionRegulation = new Regulation("DbColdSectionRegulation_" + dbColdSectionNumber.ToString());
			this.dbColdSectionScaleConfig = new ScaleConfig("DbColdSectionScaleConfig_" + dbColdSectionNumber.ToString());
		}
	}

	[TypeConverter(typeof(Converters))]
	public class DbSteamGroup : SyncroC.DataBase.LineConfig.LineBase
	{
		bool present;
		[PropertyOrder(10), Category("DBColdSection"), Description("True if this DB Cold Section are in the system.")]
		public bool Present
		{
			get { return present; }
			set { present = value; }
		}


		Regulation dbSteamGroupRegulation;
		[Category("Regulation Config"), PropertyOrder(10), Description("DB Steam Group Regulation Config")]
		public Regulation DBSteamGroupRegulation
		{
			get { return dbSteamGroupRegulation; }
			set { dbSteamGroupRegulation = value; }
		}

		ScaleConfig dbSteamGroupScaleConfig;
		[Category("Scale Config"), PropertyOrder(10), Description("Db Steam Group Scaling Config")]
		public ScaleConfig DBSteamGroupScaleConfig
		{
			get { return dbSteamGroupScaleConfig; }
			set { dbSteamGroupScaleConfig = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public DbSteamGroup(int dbSteamGroupNumber)
			: base()
		{
			this.sectionName = "DbSteamGroup_" + dbSteamGroupNumber.ToString();

			this.present = false;
			this.dbSteamGroupRegulation = new Regulation("DbSteamGroupRegulation_" + dbSteamGroupNumber.ToString());
			this.dbSteamGroupScaleConfig = new ScaleConfig("DbSteamGroupScaleConfig_" + dbSteamGroupNumber.ToString());
		}
	}

}
