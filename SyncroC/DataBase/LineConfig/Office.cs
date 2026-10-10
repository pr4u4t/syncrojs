using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for OFFICE
	/// </summary>
	/// 


  [TypeConverter(typeof(Converters))]
  public class OFFICE : SyncroC.DataBase.LineConfig.LineBase
	{

		bool disableUpDateQualityRecord;
    //agrassi_FI10-11-2010_1
    bool putFluteIntoQualityCode;
    //PMASSARO_FI21/10/10 - Gestione Scrittura Dati Su Files
    bool enableOnFileRecording = false;
    int filesMaxNumber = 70000;
		public OFFICE(): base()
		{
			this.sectionName = "OFFICE";
			this.disableUpDateQualityRecord = false;

		}

		[PropertyOrder(10), Category("OFFICE"), Description("se 'TRUE' disabilita la sovrascrittura dei dati relativi alle qualità da produrre se già presente nel dbase di Syncro quando ricevuto dagli uffici.")]
		public bool DisableUpDateQualityRecord
		{
			get { return this.disableUpDateQualityRecord; }
			set { this.disableUpDateQualityRecord = value; }
		}

    //agrassi_FI10-11-2010_1
    [PropertyOrder(10), Category("OFFICE"), Description("If True, the flute code is put at the beginning of the quality code")]
    public bool PutFluteIntoQualityCode
    {
      get { return this.putFluteIntoQualityCode; }
      set { this.putFluteIntoQualityCode = value; }
    }

    //PMASSARO_FI21/10/10 - Gestione Scrittura Dati Su Files
    [PropertyOrder(20), Category("OFFICE"), Description("Enable the On-File recording for: Past Order, Shift, Downtime and Discharge Data")]
    public bool EnableOnFileRecording
    {
      get { return this.enableOnFileRecording; }
      set { this.enableOnFileRecording = value; }
    }

    //PMASSARO_FI21/10/10 - Gestione Scrittura Dati Su Files 
    [PropertyOrder(30), Category("OFFICE"), Description("If files quantity are over than this, Syncro will stop temporanely the recording")]
    public int FilesMaxNumber
    {
      get { return this.filesMaxNumber; }
      set { this.filesMaxNumber = value; }
    }
	}
}