#region Using directives
using SyncroC.DataBase.LineConfig;
#endregion

namespace SyncroC.Utility
{
  class Slit
  {
    public int number;
    //public int Height, Width, Left, Top, LeftNarrow;
    public int Height, Width, Left, Top;
    public SlitType config;
    //public int position, plcPosition, narrowPosition;
    public int position, plcPosition;
    //agrassi_FI04-02-09
    //public bool plunged, dummy, sep, broken, wideToNarrow;
    public bool plunged, sep, broken;
    public bool selected, notOK;

    public Slit(int number)
    {
      this.number = number;
    }

    //agrassi_FI04-02-09
    //public void Position(int position, int plcPosition, bool plunged, bool dummy, bool sep,
    //                            bool broken, int leftBoardEdge, int rightBoardEdge, bool wideToNarrow, 
    //                            int narrowPosition, bool notOK)
    public void Position(int position, int plcPosition, bool plunged, bool sep, bool broken, int leftBoardEdge, int rightBoardEdge, bool notOK)
    {
      this.position = position;
      this.plcPosition = plcPosition;
      this.plunged = plunged;
      //agrassi_FI04-02-09
      //this.dummy = dummy;
      this.sep = sep;
      this.broken = broken;
      //this.narrowPosition = narrowPosition;
      //this.wideToNarrow = wideToNarrow;
      this.notOK = notOK;
    }

    public void Config(SlitType st)
    {
      this.config = st;
    }
  }
}
