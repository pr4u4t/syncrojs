#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

using SyncroC.DataBase.LineConfig;
#endregion


namespace SyncroC.Utility
{

	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.

  class SetupSituation
  {
    public bool filled;
    public int[][] slitPositions;
    public int[] plungedSlitMask;
    public int[] sepSlitMask;
    public int[] dummySlitMask;
    //CM7 18-09-2005
    public int shaftMask;
    public bool wideToNarrow;
    public bool narrowToWide;
    public bool cleanSlits;
    public int setupId;

    public SetupSituation()
    {
      this.filled = false;
      this.slitPositions = new int[SlitterScorerHeader.NUM_SLIT_SHAFTS][];
      for (int i = 0; i < SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
        this.slitPositions[i] = new int[SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
      this.plungedSlitMask = new int[SlitterScorerHeader.NUM_SLIT_SHAFTS];
      this.sepSlitMask = new int[SlitterScorerHeader.NUM_SLIT_SHAFTS];
      this.dummySlitMask = new int[SlitterScorerHeader.NUM_SLIT_SHAFTS];
      //CM7 18-09-2005
      this.shaftMask = 0;
      this.wideToNarrow = false;
      this.narrowToWide = false;
      this.cleanSlits = false;
      this.setupId = 0;
    }

    public bool Update(string s)
    {
      int cnt = 0;
      bool zeroPos=true;
      string[] data = s.Split(Command.PARAMETER_SEPARATOR);
      //CM7 18-09-2005
      if (data.Length != SlitterScorerHeader.NUM_SLIT_SHAFTS * (SlitterScorerHeader.MAX_SLITS_PER_SHAFT + 3) + 5)
        return false;
      for (int i = 0; i < SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
      {
        this.plungedSlitMask[i] = int.Parse(data[cnt++]);
        this.sepSlitMask[i] = int.Parse(data[cnt++]);
        this.dummySlitMask[i] = int.Parse(data[cnt++]);
        for (int slt = 0; slt < SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
        {
          this.slitPositions[i][slt] = int.Parse(data[cnt++]);
          zeroPos=zeroPos && (this.slitPositions[i][slt]==0);
        }
      }
      this.shaftMask = int.Parse(data[cnt++]);
      this.narrowToWide = bool.Parse(data[cnt++]);
      this.wideToNarrow = bool.Parse(data[cnt++]);
      this.cleanSlits = bool.Parse(data[cnt++]);
      this.setupId = int.Parse(data[cnt]);
      this.filled = !zeroPos;
      return true;
    }
  }
}
