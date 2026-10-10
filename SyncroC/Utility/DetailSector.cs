#region Using directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

#endregion

namespace SyncroC.Utility
{
  public class Sector
  {
    public int number;
    public int Height, Width, Left, Top;
    public int configWidth;
    public int leftPosition;
    public int centerDist;
    public bool cutting;

    public Sector(int number)
    {
      this.number = number;
    }

    public void Position(int leftPosition, bool cutting)
    {
      this.leftPosition = leftPosition;
      this.cutting = cutting;
    }

    public void Config(int configWidth)
    {
      this.configWidth = configWidth;
    }
  }
}
