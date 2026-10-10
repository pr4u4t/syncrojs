#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#endregion

namespace SyncroC.Utility
{
	/// <summary>
	/// Encapsulate all the measure overrides.
	/// </summary>
	public class ScoreMeasure
	{
    public int measure;
    public PositType positType;
		public char scoreTypeLetter;
		public int gap;
		public int offset;
		public ScorerShaft scorerShaft;

		public ScoreMeasure(int measure, PositType positType, char scoreTypeLetter,
			int gap, int offset, ScorerShaft scorerShaft)
		{
			this.measure = measure;
			this.positType = positType;
			this.scoreTypeLetter = scoreTypeLetter;
			this.gap = gap;
			this.offset = offset;
			this.scorerShaft = scorerShaft;
		}

    public ScoreMeasure()
    {
      this.measure = 0;
      this.positType = PositType.NoPositType;
      this.scoreTypeLetter = ' ';
      this.gap = 0;
      this.offset = 0;
      this.scorerShaft = ScorerShaft.Auto;
    }

    public void SetValues(int measure, string posType, string letter, string gap, string offset, ScorerShaft scorerShaft)
    {
      this.measure = measure;
      if (posType!="")
        this.positType = (PositType)Enum.Parse(typeof(PositType), posType, true);
      if (letter!="")
        this.scoreTypeLetter = letter.ToCharArray()[0];
      if (gap!="")
        this.gap = int.Parse(gap);
      if (offset!="")
        this.offset = int.Parse(offset);
      this.scorerShaft = scorerShaft;
    }
    /// <summary>
    /// Parse a "measures" string.
		/// </summary>
		/// <param name="measures"></param>
		/// <returns></returns>
		public static ArrayList ParseMeasures(string measures, PositType defaultPositType, char defaultScoreLetter,
			int defaultGap, int defaultOffset, ScorerShaft defaultScorerShaft)
		{
			measures = measures.Trim();
			while (measures.IndexOf("  ") != -1)
			{
				measures=measures.Replace("  ", " ");
			}
			ArrayList measuresArray;
			if (measures.Equals(string.Empty))
			{
				measuresArray = new ArrayList(0);
			}
			else
			{
				String[] stringsArray = measures.Split(new Char[] { ' ' });
				measuresArray = new ArrayList(stringsArray.Length);
        for (int i = 0; i < stringsArray.Length; i++)
        {
          measuresArray.Add(ParseMeasure(stringsArray[i], defaultPositType, defaultScoreLetter,
            defaultGap, defaultOffset, defaultScorerShaft));
        }
      }
			return measuresArray;
		}

		/// <summary>
		/// Parse a single measure of a "measures" string
		/// </summary>
		/// <param name="measure"></param>
		/// <returns></returns>
    //agrassi_FI30-05-08
    private static ScoreMeasure ParseMeasure(string measure, PositType defaultPositType, char defaultScoreLetter,
			int defaultGap, int defaultOffset, ScorerShaft defaultScorerShaft)
		{
			int value;
			PositType positType;
			char scoreLetter;
			int gap;
			int offset;
			ScorerShaft scorerShaft;

			string valueString = string.Empty;
			string positTypeString = string.Empty;
			string scoreLetterString = string.Empty;
			string gapString = string.Empty;
			string offsetString = string.Empty;
			string scorerShaftString = string.Empty;

			int state = 0;
			int index = 0;
			while (index < measure.Length && state != 7)
			{
				switch (state)
				{
					case 0:
						if (Char.IsDigit(measure[index]))
						{
							valueString += measure[index].ToString();
              state = 0;
						}
						else
						{
							if (Char.IsUpper(measure[index]))
							{
								scoreLetterString = measure[index].ToString();
								state = 0;
							}
							else if (measure[index].Equals('*'))
							{
								state = 1;
							}
							else if (Char.IsLower(measure[index]))
							{
								positTypeString = measure[index].ToString();
								state = 0;
							}
							else if (measure[index].Equals('-'))
							{
								state = 3;
							}
							else if (measure[index].Equals('+'))
							{
								state = 2;
							}
							else
							{
								Logger.LogError("ScoreMeasure", "Not expected char encountered during score measure parsing. (case 0)");
							}
						}
						break;
          case 1:
            scorerShaftString = measure[index].ToString();
            state = 0;
            break;
          case 2:
            gapString += measure[index].ToString();
            if ((index + 1 < measure.Length) && (Char.IsDigit(measure[index+1])))
              state = 2;
            else
              state = 0;
            break;
          case 3:
            offsetString += measure[index].ToString();
            if ((index + 1 < measure.Length) && (Char.IsDigit(measure[index+1])))
              state = 3;
            else
              state = 0;
            break;
        }
				index++;
			}

			if (valueString == null || valueString.Equals(string.Empty))
			{
				Logger.LogError("ScoreMeasure", "ParseMeasure - invalid measure \"" + measure + "\".");
				value = 0;
			}
			else
			{
				try
				{
					value = Convert.ToInt32(valueString);
				}
				catch (Exception e)
				{
					Logger.LogError("ScoreMeasure", "ParseMeasure - invalid measure \"" + measure + "\".", e);
					value = 0;
				}
			}
			if (positTypeString == null || positTypeString.Equals(string.Empty))
			{
				positType = defaultPositType;
			}
			else
			{
				try
				{
					positType = ScoreMeasure.ConvertPositTypeToEnum(positTypeString);
				}
				catch (Exception e)
				{
					Logger.LogError("ScoreMeasure", "ParseMeasure - invalid type in measure \"" + measure + "\".", e);
					positType = PositType.NoPositType;
				}
			}
			if (scoreLetterString == null || scoreLetterString.Equals(string.Empty))
			{
				scoreLetter = defaultScoreLetter;
			}
			else
			{
				scoreLetter = scoreLetterString[0];
			}
			if (offsetString == null || offsetString.Equals(string.Empty))
			{
				offset = defaultOffset;
			}
			else
			{
				try
				{
					offset = Convert.ToInt32(offsetString);
				}
				catch (Exception e)
				{
					Logger.LogError("ScoreMeasure", "ParseMeasure - invalid offset in measure \"" + measure + "\".", e);
					offset = defaultGap;
				}
			}
			if (gapString == null || gapString.Equals(string.Empty))
			{
				gap = defaultGap;
			}
			else
			{
				try
				{
					gap = Convert.ToInt32(gapString);
				}
				catch (Exception e)
				{
					Logger.LogError("ScoreMeasure", "ParseMeasure - invalid gap in measure \"" + measure + "\".", e);
					gap = defaultGap;
				}
			}
			if (scorerShaftString == null || scorerShaftString.Equals(string.Empty))
			{
				scorerShaft = defaultScorerShaft;
			}
			else
			{
				try
				{
					scorerShaft = (ScorerShaft)Enum.Parse(typeof(ScorerShaft), scorerShaftString);
				}
				catch (Exception e)
				{
					Logger.LogError("ScoreMeasure", "ParseMeasure - invalid scorer shaft in measure \"" + measure + "\".", e);
					scorerShaft = defaultScorerShaft;
				}
			}
			return new ScoreMeasure(value, positType, scoreLetter, gap, offset, scorerShaft);
		}

//    private static ScoreMeasure[] UpdateMeasures(ScoreMeasure[]

    public static PositType ConvertPositTypeToEnum(string pts)
    {
      PositType p = PositType.NoPositType;
      if (pts.Equals("m"))
        p = PositType.MaleFemaleP;
      else if (pts.Equals("p"))
        p = PositType.PointToPoint;
      else if (pts.Equals("r"))
        p = PositType.ReverseOffset;
      else if (pts.Equals("n"))
        p = PositType.NormalOffset;
      else if (pts.Equals("v"))
        p = PositType.NormalReverse;
      else if (pts.Equals("w"))
        p = PositType.TeletwinP;
      else if (pts.Equals("t"))
        p = PositType.TearTape;
      return p;
    }

    public static String ConvertPositTypeToString(PositType p)
    {
      string pts = string.Empty;
      if (p == PositType.MaleFemaleP)
        pts="m";
      if (p == PositType.PointToPoint)
        pts = "p";
      if (p == PositType.ReverseOffset)
        pts = "r";
      if (p == PositType.NormalOffset)
        pts = "n";
      if (p == PositType.NormalReverse)
        pts = "v";
      if (p == PositType.TeletwinP)
        pts = "w";
      if (p == PositType.TearTape)
        pts = "t";
      return pts;
    }

    public static string ComposeScoreMeasures(ArrayList m, ScoreMeasure def)
    {
      string ms = string.Empty;
      if ((m.Count>0) &&(((ScoreMeasure)m[m.Count - 1]).measure == 0))
        m.RemoveAt(m.Count - 1);
      for (int i = 0; i < m.Count; i++)
      {
        ScoreMeasure sm = (ScoreMeasure)m[i];
        if (sm.measure > 0)
        {
          ms += sm.measure.ToString();
          if (i < m.Count - 1)
          {
            if (sm.scoreTypeLetter!=def.scoreTypeLetter)
              if (char.IsLetter(sm.scoreTypeLetter))
                ms += sm.scoreTypeLetter.ToString();
            if (sm.positType!=def.positType)
              if (sm.positType != PositType.NoPositType)
                ms += ScoreMeasure.ConvertPositTypeToString(sm.positType);
            if (sm.offset!=def.offset)
              if ((sm.offset != 0) && (sm.positType == PositType.TeletwinP))
                ms += "-" + sm.offset.ToString();
            if (sm.gap!=def.gap)
              if (sm.gap != 0)
                ms += "+" + sm.gap.ToString();
            if (sm.scorerShaft!=def.scorerShaft)
              if (sm.scorerShaft != ScorerShaft.Auto)
                ms += "*" + Enum.Format(typeof(ScorerShaft), sm.scorerShaft, "d");
            ms += " ";
          }
        }
      }
      return (ms==""?" ":ms);
    }
  }
}
