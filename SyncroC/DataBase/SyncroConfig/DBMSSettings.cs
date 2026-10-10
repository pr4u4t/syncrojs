using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class DBMSSettings : TaskSettings
	{
    bool updateThicknessOnRemoveRoll;
    [Category("2. DBMS TASK SETTINGS"), Description("TRUE if update paper Thickness On Remove Roll.")]
    public bool UpdateThicknessOnRemoveRoll
    {
      get { return updateThicknessOnRemoveRoll; }
      set { updateThicknessOnRemoveRoll = value; }
    }

		bool overrideOldPaper;
		[Category("2. DBMS TASK SETTINGS"), Description("Determine whether old papers in database should be overridden when database dimension has reached the maximum limit.")]
		public bool OverrideOldPaper
		{
			get { return overrideOldPaper; }
			set { overrideOldPaper = value; }
		}

		bool overrideOldQuality;
		[Category("2. DBMS TASK SETTINGS"), Description("Determine whether old qualities in database should be overridden when database dimension has reached the maximum limit.")]
		public bool OverrideOldQuality
		{
			get { return overrideOldQuality; }
			set { overrideOldQuality = value; }
		}

    bool overrideOldPattern;
    [Category("2. DBMS TASK SETTINGS"), Description("Determine whether old patterns in database should be overridden when database dimension has reached the maximum limit.")]
    public bool OverrideOldPattern
    {
      get { return overrideOldPattern; }
      set { overrideOldPattern = value; }
    }

		private bool programNumberSplitsRuns;									// Used by DBMS when generating wet end schedule
		[Category("2. DBMS TASK SETTINGS"), Description("Used by DBMS when generating wet end schedule")]
		public bool ProgramNumberSplitsRuns
		{
			get { return programNumberSplitsRuns; }
			set { programNumberSplitsRuns = value; }
		}

		bool autoShiftChangedEnabled;
		[Category("3. AUTO SHIFT CHANGE"), Description("Enable auto shift change.")]
		public bool AutoShiftChangedEnabled
		{
			get { return autoShiftChangedEnabled; }
			set { autoShiftChangedEnabled = value; }
		}

		int numberShiftsPerDay;
		[Category("3. AUTO SHIFT CHANGE"), Description("Determine the number of shifts per day.")]
		public int NumberShiftsPerDay
		{
			get { return numberShiftsPerDay; }
			set { numberShiftsPerDay = value; }
		}

		//shift 1
		string shiftID_1;
		[Category("4. AUTO SHIFT CHANGE 1"), Description("Shift 1's ID.")]
		public string ShiftID_1
		{
			get { return shiftID_1; }
			set { shiftID_1 = value; }
		}

		int startTime_UTh_1;
		[Category("4. AUTO SHIFT CHANGE 1"), Description("Start time (hour) for shift 1.")]
		public int StartTime_UTh_1
		{
			get { return startTime_UTh_1; }
			set { startTime_UTh_1 = value; }
		}

		int startTime_UTm_1;
		[Category("4. AUTO SHIFT CHANGE 1"), Description("Start time (minute) for shift 1.")]
		public int StartTime_UTm_1
		{
			get { return startTime_UTm_1; }
			set { startTime_UTm_1 = value; }
		}

		int stopTime_UTh_1;
		[Category("4. AUTO SHIFT CHANGE 1"), Description("Stop time (hour) for shift 1.")]
		public int StopTime_UTh_1
		{
			get { return stopTime_UTh_1; }
			set { stopTime_UTh_1 = value; }
		}

		int stopTime_UTm_1;
		[Category("4. AUTO SHIFT CHANGE 1"), Description("Stop time (minute) for shift 1.")]
		public int StopTime_UTm_1
		{
			get { return stopTime_UTm_1; }
			set { stopTime_UTm_1 = value; }
		}

		//shift 2
		string shiftID_2;
		[Category("5. AUTO SHIFT CHANGE 2"), Description("Shift 2's ID.")]
		public string ShiftID_2
		{
			get { return shiftID_2; }
			set { shiftID_2 = value; }
		}

		int startTime_UTh_2;
		[Category("5. AUTO SHIFT CHANGE 2"), Description("Start time (hour) for shift 2.")]
		public int StartTime_UTh_2
		{
			get { return startTime_UTh_2; }
			set { startTime_UTh_2 = value; }
		}

		int startTime_UTm_2;
		[Category("5. AUTO SHIFT CHANGE 2"), Description("Start time (minute) for shift 2.")]
		public int StartTime_UTm_2
		{
			get { return startTime_UTm_2; }
			set { startTime_UTm_2 = value; }
		}

		int stopTime_UTh_2;
		[Category("5. AUTO SHIFT CHANGE 2"), Description("Stop time (hour) for shift 2.")]
		public int StopTime_UTh_2
		{
			get { return stopTime_UTh_2; }
			set { stopTime_UTh_2 = value; }
		}

		int stopTime_UTm_2;
		[Category("5. AUTO SHIFT CHANGE 2"), Description("Stop time (minute) for shift 2.")]
		public int StopTime_UTm_2
		{
			get { return stopTime_UTm_2; }
			set { stopTime_UTm_2 = value; }
		}

		//shift 3
		string shiftID_3;
		[Category("6. AUTO SHIFT CHANGE 3"), Description("Shift 3's ID.")]
		public string ShiftID_3
		{
			get { return shiftID_3; }
			set { shiftID_3 = value; }
		}

		int startTime_UTh_3;
		[Category("6. AUTO SHIFT CHANGE 3"), Description("Start time (hour) for shift 3.")]
		public int StartTime_UTh_3
		{
			get { return startTime_UTh_3; }
			set { startTime_UTh_3 = value; }
		}

		int startTime_UTm_3;
		[Category("6. AUTO SHIFT CHANGE 3"), Description("Start time (minute) for shift 3.")]
		public int StartTime_UTm_3
		{
			get { return startTime_UTm_3; }
			set { startTime_UTm_3 = value; }
		}

		int stopTime_UTh_3;
		[Category("6. AUTO SHIFT CHANGE 3"), Description("Stop time (hour) for shift 3.")]
		public int StopTime_UTh_3
		{
			get { return stopTime_UTh_3; }
			set { stopTime_UTh_3 = value; }
		}

		int stopTime_UTm_3;
		[Category("6. AUTO SHIFT CHANGE 3"), Description("Stop time (minute) for shift 3.")]
		public int StopTime_UTm_3
		{
			get { return stopTime_UTm_3; }
			set { stopTime_UTm_3 = value; }
		}

		//shift 4
		string shiftID_4;
		[Category("7. AUTO SHIFT CHANGE 4"), Description("Shift 4's ID.")]
		public string ShiftID_4
		{
			get { return shiftID_4; }
			set { shiftID_4 = value; }
		}

		int startTime_UTh_4;
		[Category("7. AUTO SHIFT CHANGE 4"), Description("Start time (hour) for shift 4.")]
		public int StartTime_UTh_4
		{
			get { return startTime_UTh_4; }
			set { startTime_UTh_4 = value; }
		}

		int startTime_UTm_4;
		[Category("7. AUTO SHIFT CHANGE 4"), Description("Start time (minute) for shift 4.")]
		public int StartTime_UTm_4
		{
			get { return startTime_UTm_4; }
			set { startTime_UTm_4 = value; }
		}

		int stopTime_UTh_4;
		[Category("7. AUTO SHIFT CHANGE 4"), Description("Stop time (hour) for shift 4.")]
		public int StopTime_UTh_4
		{
			get { return stopTime_UTh_4; }
			set { stopTime_UTh_4 = value; }
		}

		int stopTime_UTm_4;
		[Category("7. AUTO SHIFT CHANGE 4"), Description("Stop time (minute) for shift 4.")]
		public int StopTime_UTm_4
		{
			get { return stopTime_UTm_4; }
			set { stopTime_UTm_4 = value; }
		}

		public DBMSSettings(): base()
    {
			this.sectionName = "S_DBMS" + this.sectionName;

      this.updateThicknessOnRemoveRoll = false;
      this.overrideOldPattern = true;
			this.overrideOldPaper = true;
			this.overrideOldQuality = false;
			this.programNumberSplitsRuns = false;
			this.autoShiftChangedEnabled = false;
			this.numberShiftsPerDay = 0;

			shiftID_1 = string.Empty;
			startTime_UTh_1 = 0;
			startTime_UTm_1 = 0;
			stopTime_UTh_1 = 0;
			stopTime_UTm_1 = 0;

			shiftID_2 = string.Empty;
			startTime_UTh_2 = 0;
			startTime_UTm_2 = 0;
			stopTime_UTh_2 = 0;
			stopTime_UTm_2 = 0;

			shiftID_3 = string.Empty;
			startTime_UTh_3 = 0;
			startTime_UTm_3 = 0;
			stopTime_UTh_3 = 0;
			stopTime_UTm_3 = 0;

			shiftID_4 = string.Empty;
			startTime_UTh_4 = 0;
			startTime_UTm_4 = 0;
			stopTime_UTh_4 = 0;
			stopTime_UTm_4 = 0;
    }
	}
}
