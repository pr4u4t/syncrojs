#region Using directives

using System.Collections;

using SyncroC.DataBase.LineConfig;

#endregion

namespace SyncroC.Utility
{
  class Score
  {
    public int number;
    public int Height, Width, Left, Top;
    public SyncroC.DataBase.LineConfig.ScoreType config;
    public char parity;
    //agrassi_FI04-02-09
    //public int position, upRelPosition, leftPosition, scoreGap, shaftGap;
    public int position;
    //public int measureIndex;
    //public PositType posType;
    public Hashtable colorTable;

    public Score(int number)
    {
      this.number = number;
    }

    //agrassi_FI04-02-09
    //public void Position(int position, int upRelPosition, int leftPosition, int scoreGap, int shaftGap,
    //                      PositType posType, int measureIndex)
    public void Position(int position)
    {
      this.position = position;
      //this.upRelPosition = upRelPosition;
      //this.posType = posType;
      //this.measureIndex = measureIndex;
      //this.leftPosition = leftPosition;
      //this.scoreGap = scoreGap;
      //this.shaftGap = shaftGap;
    }

    public void Config(SyncroC.DataBase.LineConfig.ScoreType st, char parity, Hashtable tbl)
    {
      this.config = st;
      this.parity = parity;
      this.colorTable = tbl;
    }
  }
}
