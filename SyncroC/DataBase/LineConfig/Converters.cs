using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Drawing.Design;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	public class Converters : ExpandableObjectConverter
	{
		public const string spaces="";

		public override bool GetPropertiesSupported(ITypeDescriptorContext context) 
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			// This override returns a list of properties in order
			PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(value, attributes);
			ArrayList orderedProperties = new ArrayList();
			foreach (PropertyDescriptor pd in pdc)
			{
				Attribute attribute = pd.Attributes[typeof(PropertyOrderAttribute)];
				if (attribute != null)
				{
					// If the attribute is found, then create an pair object to hold it
					PropertyOrderAttribute poa = (PropertyOrderAttribute)attribute;
					orderedProperties.Add(new PropertyOrderPair(pd.Name,poa.Order));
				}
				else
				{
					// If no order attribute is specifed then given it an order of 10000
					orderedProperties.Add(new PropertyOrderPair(pd.Name,10000));
				}
			}
			// Perform the actual order using the value PropertyOrderPair classes
			// implementation of IComparable to sort
			orderedProperties.Sort();
			// Build a string list of the ordered names
			ArrayList propertyNames = new ArrayList();
			foreach (PropertyOrderPair pop in orderedProperties)
				propertyNames.Add(pop.Name);
			// Pass in the ordered list for the PropertyDescriptorCollection to sort by
			return pdc.Sort((string[])propertyNames.ToArray(typeof(string)));
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object val, Type destinationType)
		{
			if (destinationType==typeof(System.String) && val.GetType().IsSubclassOf(typeof(ConfigBase)))
				return	Converters.spaces;
			return base.ConvertTo(context, culture, val, destinationType);
		}

	}

	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyOrderAttribute : Attribute
	{
		// Simple attribute to allow the order of a property to be specified
		private int _order;
		public PropertyOrderAttribute(int order)
		{
			_order = order;
		}

		public int Order
		{
      get { return _order; }
		}
	}

	public class PropertyOrderPair : IComparable
	{
		private int _order;
		private string _name;
		public string Name
		{
			get	{ return _name;	}
		}

		public PropertyOrderPair(string name, int order)
		{
			_order = order;
			_name = name;
		}

		public int CompareTo(object obj)
		{
			// Sort the pair objects by ordering by order value
			// Equal values get the same rank
			int otherOrder = ((PropertyOrderPair)obj)._order;
			if (otherOrder == _order)
			{
				// If order not specified, sort by name
				string otherName = ((PropertyOrderPair)obj)._name;
				return string.Compare(_name,otherName);
			}
			else if (otherOrder > _order)
			{
				return -1;
			}
			return 1;
		}
	}

  public class ScorerProfileConverter : StringConverter
  {
    private string[] profileNames=null;
    public ScorerProfileConverter():base()
    { 
      ScorerProfileType[] profiles=DBManager.getDBManager().scorerProfile.Profiles;
      profileNames=new string[profiles.Length];
      for (int i = 0; i < profiles.Length; i++)
        profileNames[i] = profiles[i].ScoreId;
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.profileNames);
    }
  }

  public class SSNameConverter : StringConverter
  {
    private string[] SSNames = null;
    public SSNameConverter(): base()
    {
			//RFAZZI_FI03/09/2008_1.Added Compact2.
      //this.SSNames = new string[] { "TwinE", "Twin400", "Compact2" };
      //agrassi_FI25-05-2011_1
      this.SSNames = new string[] { "Twin400", "TwinE", "Twin2", "Compact2" };
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.SSNames);
    }
  }

  public class PrinterNamesConverter : StringConverter
  {
    private string[] printerNames = null;
    public PrinterNamesConverter(): base()
    {
      if (Settings.getSettings().configuration.getOnServer())
      {
        //agrassi_FI14-05-2010_1
        try
        {
          this.printerNames = new string[PrinterSettings.InstalledPrinters.Count];
          PrinterSettings.InstalledPrinters.CopyTo(this.printerNames, 0);
        }
        catch (System.Exception e)
        {
          Logger.LogError(this, e);
          this.printerNames = new string[] { "<error detecting printer>" };
        }
      }
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return Settings.getSettings().configuration.getOnServer();
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.printerNames);
    }
  }

  public class LanguageConverter : StringConverter
  {
    private string[] languages = null;
    public LanguageConverter(): base()
    {
      //PMASSARO_FI08/05/09 - Added Norwegian language
      /*
       this.languages = new string[] { "English", "Italian", "German", "Spanish", "Dutch", "Czech", "French", "Portuguese", 
                                      "Polish", "Finnish", "Swedish", "Chinese", "Turkish", "Russian"};
      */
      //agrassi_FI19-08-2010_2
      //this.languages = new string[] { "English", "Italian", "German", "Spanish", "Dutch", "Czech", "French", "Portuguese", 
      //                                "Polish", "Finnish", "Swedish", "Chinese", "Turkish", "Russian", "Norwegian"};
      this.languages = new string[] { "English", "Italian", "German", "Spanish", "Dutch", "Czech", "French", "Portuguese", 
                                      "Polish", "Finnish", "Swedish", "Chinese", "Turkish", "Russian", "Norwegian", "Arabic"};
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.languages);
    }
  }

  public class SSWidthConverter : Int32Converter
  {
    private int[] SSWidths = null;
    public SSWidthConverter(): base()
    {
			//RFAZZI_FI29/02/2008_2.Created new Twin400 PLC Protocol Version for WideBody and TopSlitter
			this.SSWidths = new int[] { 34500, 35000, 37500 };
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.SSWidths);
    }
  }

  public class FosberArrayConverter : ArrayConverter
  {
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType.Equals(typeof(string)))
        return null;
      return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      return "";
    }
  }

  public class ArrayTypeEditor : System.Drawing.Design.UITypeEditor
  {
    public override bool GetPaintValueSupported(ITypeDescriptorContext context)
    {
      return false;
    }
  }
}
