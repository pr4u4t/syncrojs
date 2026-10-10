using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace SyncroC.DataBase.LineConfig
{

	[TypeConverter(typeof(Converters))]
	public class Regulation : LineBase
	{
		double deadZone;
    [PropertyOrder(10), Category("Regulation parameters"), Description("Tolleranza sul valore del WrapArm, GlueGap, etc. raggiunta durante il movimento richiesto da Syncro. Il valore impostato deve essere maggiore di 0 e si accettano anche valori decimali")]
		public double DeadZone
		{
			get { return this.deadZone; }
			set { this.deadZone = value; }
		}

		int minVal;
    [PropertyOrder(30), Category("Regulation parameters"), Description("Valore  minimo a cui può essere portata il valore del WrapArm, GlueGap, etc.")]
		public int MinVal
		{
			get { return this.minVal; }
			set { this.minVal = value; }
		}

		int maxVal;
		[PropertyOrder(40), Category("Regulation parameters"), Description("Valore  massimo a cui può essere portata il valore del WrapArm, GlueGap, etc.")]
		public int MaxVal
		{
			get { return this.maxVal; }
			set { this.maxVal = value; }
		}

		double timeOutValue_UTs;
    //RFAZZI_FI22/10/2010_1. Cambiato nome alla variabile rimuovendo l'unità di misura
    //[PropertyOrder(50), Category("Regulation parameters"), Description("Used during the regulation period. If in the last (TimeOutValue_UTs) the control doesn't move than at last (timeOutRange_UTs) the function exit.")]
    [PropertyOrder(50), Category("Regulation parameters"), Description("Usato durante la fase di regolazione. Se nell'ultimo (TimeOutValue_UTs) il valore del WrapArm, GlueGap, etc.. non si muove di almeno (timeOutRange) la funzione termina.")]
    public double TimeOutValue_UTs
		{
			get { return this.timeOutValue_UTs; }
			set { this.timeOutValue_UTs = value; }
		}

    //RFAZZI_FI22/10/2010_1. Cambiato nome alla variabile rimuovendo l'unità di misura
    //double timeOutRange_UTs;
    //[PropertyOrder(60), Category("Regulation parameters"), Description("Used during the regulation period. If in the last (TimeOutValue_UTs) the control doesn't move than at last (timeOutRange_UTs) the function exit.")]
    //public double TimeOutRange_UTs
    //{
    //  get { return this.timeOutRange_UTs; }
    //  set { this.timeOutRange_UTs = value; }
    //}
    double timeOutRange;
    [PropertyOrder(60), Category("Regulation parameters"), Description("Usato durante la fase di regolazione. Se nell'ultimo (TimeOutValue_UTs) il valore del WrapArm, GlueGap, etc.. non si muove di almeno (timeOutRange) la funzione termina.")]
    public double TimeOutRange
    {
      get { return this.timeOutRange; }
      set { this.timeOutRange = value; }
    }


		public Regulation(string regulationName)
			: base()
		{

			this.sectionName = regulationName;
			this.maxVal = 100;
			this.minVal = 0;
      //RFAZZI_FI22/10/2010_1. Cambiato nome alla variabile rimuovendo l'unità di misura
      this.timeOutRange = 1;
			this.timeOutValue_UTs = 1;
			this.deadZone = 2;
		}
	}

	[TypeConverter(typeof(Converters))]
	public class ScaleConfig : LineBase
	{

		double lowInputValue;
    [PropertyOrder(10), Category("1. Scale Config"), Description("Valore analogico in ingresso quando il WrapArm, GlueGap, etc.. è alla minima posizione. Il valore del WrapArm, GlueGap, etc.. verrà calcolata in funzione di questo valore.")]
		public double LowInputValue
		{
			get { return lowInputValue; }
			set { lowInputValue = value; }
		}

		double highInputValue;
    [PropertyOrder(20), Category("1. Scale Config"), Description("Valore analogico in ingresso quando il WrapArm, GlueGap, etc.. è alla massima posizione. Il valore del WrapArm, GlueGap, etc.. verrà calcolata in funzione di questo valore.")]
		public double HighInputValue
		{
			get { return highInputValue; }
			set { highInputValue = value; }
		}

		public ScaleConfig(string scaleConfigName)
			: base()
		{

			this.sectionName = scaleConfigName;
			this.highInputValue = 10;
			this.lowInputValue = 0;

		}
	}

  //RFAZZI_FI07/07/2011_1. Aggiunta gestione di un numero di sezioni del Vapore per ogni singolo PreHeater. 
  [TypeConverter(typeof(Converters))]
  public class SectionDimension : LineBase
  {

    int[] dimension;
    [PropertyOrder(10), Category("1. Section Dimension"), Description("Dimensione della sezione")]
    public int[] Dimension
    {
      get { return dimension; }
      set { dimension = value; }
    }

    public SectionDimension(string sectionDimensionsName)
      : base()
    {

      this.sectionName = sectionDimensionsName;
      this.dimension = new int[10] ;
			for (int i = 0; i < 10; i++)
				this.dimension[i] = 0;    
    }
  }
}
