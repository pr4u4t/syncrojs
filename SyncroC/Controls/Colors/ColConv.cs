using System.Drawing;
using System;

namespace SyncroC.Controls.Colors
{
  /// <summary>
  /// Colors of graphic components of Syncro client.
  /// </summary>
  public enum SyncroColor
  {
    /// <summary>Color.Black</summary>
    Black,
    /// <summary>Color.White</summary>
    White,
    /// <summary>178, 205, 203</summary>
    LightGreen,
    /// <summary>0, 192, 0</summary>
    Green,
    /// <summary>0, 90, 82</summary>
    DarkGreen,
    /// <summary>Color.LightGray</summary>
    LightGray,
    /// <summary>Color to be used to show Alarms</summary>
    AlarmRed,
    /// <summary>Color to be used to show AOC Early Warning</summary>
    AOCEarlyWarningColor,
    /// <summary>Color to be used to show Overrunning</summary>
    OverRunColor,
    /// <summary>Color to be used to show AOC in progress</summary>
    AOCInProgressColor,
    /// <summary>Color to be used to show not standard downtime reasons</summary>
    NoStandardDownTimeReasonColor,
    /// <summary>193; 192; 224</summary>
    SingleFacerColor,
    /// <summary>51; 51; 139</summary>
    DoubleBackerColor,
    /// <summary>209; 127; 129</summary>
    PhtAndGlueUnitColor,
    ///<summary>128; 128; 255</summary>
    PRCUserValueColor,
    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
    ///<summary>255, 128, 0</summary>
    WarningMessageYellowColor,
  }

  /// <summary>
  /// Provides functionality to convert a SyncroColor to a Color and viceversa.
  /// </summary>
  public class ColConv
  {
    /// <summary>Corresponding color for the SyncroColor.Black.</summary>
    private static Color Black = Color.Black;
    /// <summary>Corresponding color for the SyncroColor.White.</summary>
    //private static Color White = Color.White;
    private static Color White = Color.WhiteSmoke;
    /// <summary>Corresponding color for the SyncroColor.LightGreen.</summary>
    private static Color LightGreen = Color.FromArgb(178, 205, 203);
    /// <summary>Corresponding color for the SyncroColor.Green.</summary>
    private static Color Green = Color.FromArgb(102, 179, 102);
    /// <summary>Corresponding color for the SyncroColor.DarkGreen.</summary>
    private static Color DarkGreen = Color.FromArgb(0, 90, 82);
    /// <summary>Corresponding color for the SyncroColor.DarkGreen.</summary>
    private static Color LightGray = Color.LightGray;
    /// <summary>Corresponding color for the SyncroColor.Transparent.</summary>
    private static Color AlarmRed = Color.FromArgb(204, 2, 28);
    /// <summary>Corresponding color for the SyncroColor.AOCEarlyWarningColor.</summary>
    private static Color AOCEarlyWarningColor = Color.Yellow;
    /// <summary>Corresponding color for the SyncroColor.OverRunColor.</summary>
    private static Color OverRunColor = Color.Yellow;
    /// <summary>Corresponding color for the SyncroColor.AOCInProgressColor.</summary>
    private static Color AOCInProgressColor = Color.LightBlue;
    /// <summary>Corresponding color for the SyncroColor.NoStandardDownTimeReasonColor.</summary>
    private static Color NoStandardDownTimeReasonColor = Color.DarkBlue;
    /// <summary>Corresponding color for the SyncroColor.SingleFacerColor.</summary>
    private static Color SingleFacerColor = Color.FromArgb(193, 192, 224);
    /// <summary>Corresponding color for the SyncroColor.DoubleBackerColor.</summary>
    private static Color DoubleBackerColor = Color.FromArgb(51, 51, 139);
    /// <summary>Corresponding color for the SyncroColor.DoubleBackerColor.</summary>
    private static Color PhtAndGlueUnitColor = Color.FromArgb(209, 127, 129);
    /// <summary>Corresponding color for the SyncroColor.PRCSetPointColor.</summary>
    private static Color PRCSetPointColor = Color.Lime;

    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
    /// <summary>Corresponding color for the SyncroColor.WarningOrangeColor</summary>
    private static Color WarningMessageYellowColor = Color.FromArgb(255, 216, 0);

    /// <summary>
    /// Convert a SyncroColor to a Color.
    /// </summary>
    /// <param name="color">the SyncroColor to convert.</param>
    /// <returns>the converted Color</returns>
    public static Color Convert(SyncroColor color)
    {
      switch (color)
      {
        case SyncroColor.Black:
          return Black;
        case SyncroColor.White:
          return White;
        case SyncroColor.LightGreen:
          return LightGreen;
        case SyncroColor.Green:
          return Green;
        case SyncroColor.DarkGreen:
          return DarkGreen;
        case SyncroColor.LightGray:
          return LightGray;
        case SyncroColor.AlarmRed:
          return AlarmRed;
        case SyncroColor.AOCEarlyWarningColor:
          return AOCEarlyWarningColor;
        case SyncroColor.OverRunColor:
          return OverRunColor;
        case SyncroColor.AOCInProgressColor:
          return AOCInProgressColor;
        case SyncroColor.NoStandardDownTimeReasonColor:
          return NoStandardDownTimeReasonColor;
        case SyncroColor.SingleFacerColor:
          return SingleFacerColor;
        case SyncroColor.DoubleBackerColor:
          return DoubleBackerColor;
        case SyncroColor.PhtAndGlueUnitColor:
          return PhtAndGlueUnitColor;
        case SyncroColor.PRCUserValueColor:
          return PRCSetPointColor;
        //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
        case SyncroColor.WarningMessageYellowColor:
          return WarningMessageYellowColor;
        default:
          return White;
      }
    }

    /// <summary>
    /// Convert a Color to a SyncroColor
    /// </summary>
    /// <param name="color">the Color to convert</param>
    /// <returns>the converted SyncroColor</returns>
    public static SyncroColor Convert(Color color)
    {
      if (color.Equals(Black))
        return SyncroColor.Black;
      else if (color.Equals(White))
        return SyncroColor.White;
      else if (color.Equals(LightGreen))
        return SyncroColor.LightGreen;
      else if (color.Equals(Green))
        return SyncroColor.Green;
      else if (color.Equals(DarkGreen))
        return SyncroColor.DarkGreen;
      else if (color.Equals(LightGray))
        return SyncroColor.LightGray;
      else if (color.Equals(AlarmRed))
        return SyncroColor.AlarmRed;
      else if (color.Equals(AOCEarlyWarningColor))
        return SyncroColor.AOCEarlyWarningColor;
      else if (color.Equals(OverRunColor))
        return SyncroColor.OverRunColor;
      else if (color.Equals(AOCInProgressColor))
        return SyncroColor.AOCInProgressColor;
      else if (color.Equals(NoStandardDownTimeReasonColor))
        return SyncroColor.NoStandardDownTimeReasonColor;
      else if (color.Equals(SingleFacerColor))
        return SyncroColor.SingleFacerColor;
      else if (color.Equals(DoubleBackerColor))
        return SyncroColor.DoubleBackerColor;
      else if (color.Equals(PhtAndGlueUnitColor))
        return SyncroColor.PhtAndGlueUnitColor;
      else if (color.Equals(PRCSetPointColor))
        return SyncroColor.PRCUserValueColor;
      //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
      else if (color.Equals(WarningMessageYellowColor))
        return SyncroColor.WarningMessageYellowColor;
      return SyncroColor.White;
    }

    public static Color Lighten(SyncroColor c)
    {
      return Lighten(Convert(c));
    }

    public static Color Lighten(Color c)
    {
      const double PERC_REF = 1.0 / 3.0;
      const double n = 1.25;
      double percR, percG, percB;
      int max = Math.Max(Math.Max(c.R, c.G), c.B);
      int tot = c.R + c.G + c.B;
      percR = (double)c.R / (double)tot;
      percG = (double)c.G / (double)tot;
      percB = (double)c.B / (double)tot;
      tot += (int)(0.9 * ((3 * n * 255 * tot) / (tot + 3 * (n - 1) * max) - tot)); ;
      percR = percR + (PERC_REF - percR) / n;
      percG = percG + (PERC_REF - percG) / n;
      percB = percB + (PERC_REF - percB) / n;
      Color moreLight = Color.FromArgb((byte)(tot * percR), (byte)(tot * percG), (byte)(tot * percB));
      return moreLight;
    }
  }
}

