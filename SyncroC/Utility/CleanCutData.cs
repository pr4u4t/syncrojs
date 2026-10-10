#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion
using SyncroC.DataBase.LineConfig;

namespace SyncroC.Utility
{
  public class SlitCounters
  {
    public int totalRunMeters;
    public int totalGrindings;
    public int metersSinceLastGrind;
    public int metersSinceLastLube;
		//RFAZZI_FI27/08/08_1. Added value MetersSinceLastSlitRegolation on graphics
		public int grindMetersSinceLastRegulation;
		//RFAZZI_FI04/11/08_1. APM TW4084. Added new counters "TotalGrindMeters" and "grindMetersSinceLastRegulation" in the machine protocol.
		public int totalGrindMetres;


    public SlitCounters()
    {
      this.totalRunMeters = 0;
      this.totalGrindings = 0;
      this.metersSinceLastGrind = 0;
      this.metersSinceLastLube = 0;
			//RFAZZI_FI27/08/08_1. Added value MetersSinceLastSlitRegolation on graphics
			this.grindMetersSinceLastRegulation = 0;
			//RFAZZI_FI04/11/08_1. APM TW4084. Added new counters "TotalGrindMeters" and "grindMetersSinceLastRegulation" in the machine protocol.
			this.totalGrindMetres = 0;
    }
  }

  public class CleanCutData
  {
    public SlitCounters[][] slitCounters;
    public int grindToolMeters;
    public int grindDuration;
    public int lubeDuration;
    public int grindInterval;
    public int lubeInterval;
    public int trimGrindingMultiplier;

    public CleanCutData()
    {
      this.slitCounters = new SlitCounters[SlitterScorerHeader.NUM_SLIT_SHAFTS][];
      for (int i = 0; i < SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
      {
        this.slitCounters[i] = new SlitCounters[SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
        for (int j = 0; j < SlitterScorerHeader.MAX_SLITS_PER_SHAFT; j++)
          this.slitCounters[i][j] = new SlitCounters();
      }
      this.grindToolMeters = 0;
      this.grindDuration = 0;
      this.lubeDuration = 0;
      this.grindInterval = 0;
      this.lubeInterval = 0;
      this.trimGrindingMultiplier = 0;
    }
  }
}
