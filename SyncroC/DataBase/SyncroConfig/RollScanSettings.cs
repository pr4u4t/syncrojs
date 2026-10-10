using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.DataBase.SyncroConfig
{

	public class RollScanSettings : TaskSettings
	{
		bool enable;
		[Category("1. SYNCRO TASK GENERIC SETTINGS"), Description("Enable task")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		BaseCommsSettings roll_scan_1_A_Comms;
		[Category("2. ROLL_SCAN Settings"), Description("ROLL SCAN Comms Settings")]
		public BaseCommsSettings Roll_scan_1_A_Comms
		{
			get { return roll_scan_1_A_Comms; }
			set { roll_scan_1_A_Comms = value; }
		}
		BaseCommsSettings roll_scan_1_B_Comms;
		[Category("2. ROLL_SCAN Settings"), Description("ROLL SCAN Comms Settings")]
		public BaseCommsSettings Roll_scan_1_B_Comms
		{
			get { return roll_scan_1_B_Comms; }
			set { roll_scan_1_B_Comms = value; }
		}

		BaseCommsSettings roll_scan_2_A_Comms;
		[Category("2. ROLL_SCAN Settings"), Description("ROLL SCAN Comms Settings")]
		public BaseCommsSettings Roll_scan_2_A_Comms
		{
			get { return roll_scan_2_A_Comms; }
			set { roll_scan_2_A_Comms = value; }
		}
		BaseCommsSettings roll_scan_2_B_Comms;
		[Category("2. ROLL_SCAN Settings"), Description("ROLL SCAN Comms Settings")]
		public BaseCommsSettings Roll_scan_2_B_Comms
		{
			get { return roll_scan_2_B_Comms; }
			set { roll_scan_2_B_Comms = value; }
		}

		BaseCommsSettings roll_scan_3_A_Comms;
		[Category("2. ROLL_SCAN Settings"), Description("ROLL SCAN Comms Settings")]
		public BaseCommsSettings Roll_scan_3_A_Comms
		{
			get { return roll_scan_3_A_Comms; }
			set { roll_scan_3_A_Comms = value; }
		}
		BaseCommsSettings roll_scan_3_B_Comms;
		[Category("2. ROLL_SCAN Settings"), Description("ROLL SCAN Comms Settings")]
		public BaseCommsSettings Roll_scan_3_B_Comms
		{
			get { return roll_scan_3_B_Comms; }
			set { roll_scan_3_B_Comms = value; }
		}

		BaseCommsSettings roll_scan_4_A_Comms;
		[Category("2. ROLL_SCAN Settings"), Description("ROLL SCAN Comms Settings")]
		public BaseCommsSettings Roll_scan_4_A_Comms
		{
			get { return roll_scan_4_A_Comms; }
			set { roll_scan_4_A_Comms = value; }
		}
		BaseCommsSettings roll_scan_4_B_Comms;
		[Category("2. ROLL_SCAN Settings"), Description("ROLL SCAN Comms Settings")]
		public BaseCommsSettings Roll_scan_4_B_Comms
		{
			get { return roll_scan_4_B_Comms; }
			set { roll_scan_4_B_Comms = value; }
		}

		BaseCommsSettings roll_scan_5_A_Comms;
		[Category("2. ROLL_SCAN Settings"), Description("ROLL SCAN Comms Settings")]
		public BaseCommsSettings Roll_scan_5_A_Comms
		{
			get { return roll_scan_5_A_Comms; }
			set { roll_scan_5_A_Comms = value; }
		}
		BaseCommsSettings roll_scan_5_B_Comms;
		[Category("2. ROLL_SCAN Settings"), Description("ROLL SCAN Comms Settings")]
		public BaseCommsSettings Roll_scan_5_B_Comms
		{
			get { return roll_scan_5_B_Comms; }
			set { roll_scan_5_B_Comms = value; }
		}

		public RollScanSettings()
			: base()
    {
			this.sectionName = "S_ROLL_SCAN" + this.sectionName;
			this.enable = false;
			this.roll_scan_1_A_Comms = new BaseCommsSettings("S_ROLL_SCAN_1_A_Comms");
			this.roll_scan_1_B_Comms = new BaseCommsSettings("S_ROLL_SCAN_1_B_Comms");
			this.roll_scan_2_A_Comms = new BaseCommsSettings("S_ROLL_SCAN_2_A_Comms");
			this.roll_scan_2_B_Comms = new BaseCommsSettings("S_ROLL_SCAN_2_B_Comms");
			this.roll_scan_3_A_Comms = new BaseCommsSettings("S_ROLL_SCAN_3_A_Comms");
			this.roll_scan_3_B_Comms = new BaseCommsSettings("S_ROLL_SCAN_3_B_Comms");
			this.roll_scan_4_A_Comms = new BaseCommsSettings("S_ROLL_SCAN_4_A_Comms");
			this.roll_scan_4_B_Comms = new BaseCommsSettings("S_ROLL_SCAN_4_B_Comms");
			this.roll_scan_5_A_Comms = new BaseCommsSettings("S_ROLL_SCAN_5_A_Comms");
			this.roll_scan_5_B_Comms = new BaseCommsSettings("S_ROLL_SCAN_5_B_Comms");
		}
	}
}
