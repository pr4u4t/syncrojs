#region Using directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

#endregion

namespace SyncroC.Utility
{
  public class Paddle
  {
    public int number;
    public int Height, Width, Left, Top;
    public int configWidth;
    public int leftPosition;
    public Color levelColor;

    public Paddle(int number)
    {
      this.number = number;
    }

    public void Position(int leftPosition, Color levelColor)
    {
      this.leftPosition = leftPosition;
      this.levelColor = levelColor;
    }

    public void Config(int configWidth)
    {
      this.configWidth = configWidth;
    }
  }
}
