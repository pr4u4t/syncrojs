using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Controls.GroupPanel
{
  public delegate void MyListBoxSelectedIndexChangedHandler(object sender, string args);

  public partial class MyListBox : UserControl
  {
    [Description("Event raised when MyListBox Selected Index Changed."), Category("Fosbersoft")]
    public event MyListBoxSelectedIndexChangedHandler OnMyListBoxSelectedIndexChangedHandler;

    private List<Triple<string, string>> myLista = new List<Triple<string, string>>();

    public override Color BackColor
    {
      get { return base.BackColor; }
      set 
      {
        base.BackColor = value;
        this.flowLayoutPanel.BackColor = value; 
      }
    }

    public MyListBox()
    {
      InitializeComponent();
    }

    public string getSelectedItem()
    {
      string res = "";
      foreach (Control c in this.flowLayoutPanel.Controls)
      {
        if (c is Label)
        {
          if (((Label)c).BackColor != Color.White)
          {
            int number = SyncroC.Utility.SyncroConverter.convertToInt(((Label)c).Name.Replace("lbl_", ""), -1);

            if ((number < 0) || (number > myLista.Count))
              continue;

            res = myLista[number].First;
            break;
          }
        }
      }
      return res;
    }

    public void fillData(List<Triple<string, string>> theLista)
    {
      this.SuspendLayout();

      if (theLista.Count != 0)
        theLista.Sort(delegate(Triple<string, string> x, Triple<string, string> y) { return x.Orario.CompareTo(y.Orario); });

      myLista.Clear();
      myLista.AddRange(theLista);

      int scroll = 0;
      //if (((ScrollableControl)this.flowLayoutPanel).VerticalScroll.Visible)
      //  scroll = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

      if((theLista.Count * 26) >  this.Height)
        scroll = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

      foreach (Control c in this.flowLayoutPanel.Controls)
      {
        if (c is Label)
        {
          int number = SyncroC.Utility.SyncroConverter.convertToInt(((Label)c).Name.Replace("lbl_", ""), -1);

          if (number >= myLista.Count)
            ((Label)c).Visible = false;
          
          ((Label)c).Size = new System.Drawing.Size(this.flowLayoutPanel.Width - scroll, 25);
          ((Label)c).Text = "";
          ((Label)c).BackColor = Color.White;
        }
      }

      for (int i = 0; i < myLista.Count; i++)
      {
        this.getControl(true, "lbl_" + i.ToString()).Text = myLista[i].First;
        //ABELLO_FI22/12/2010_1 Aggiunto il trace degli Stop su Postgres.
        if (myLista[i].First.Contains("Stop"))
        {
          this.getControl(true, "lbl_" + i.ToString()).BackColor = Color.Red;
        }
      }

      this.ResumeLayout(false);
    }

    private void labelN_Click(object sender, EventArgs e)
    {
      bool resetClick = false;
      if ((((Label)sender).BackColor != Color.White) && (((Label)sender).BackColor != Color.Red))
      {
        resetClick = true;
        if (this.OnMyListBoxSelectedIndexChangedHandler != null)
          OnMyListBoxSelectedIndexChangedHandler(this, "");
      }

      this.SuspendLayout();

      int scroll = 0;
      if (((ScrollableControl)this.flowLayoutPanel).VerticalScroll.Visible)
        scroll = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

      int selectedNumber = 0;
      foreach (Control c in this.flowLayoutPanel.Controls)
      {
        if (c is Label)
        {
          int number = SyncroC.Utility.SyncroConverter.convertToInt(((Label)c).Name.Replace("lbl_", ""),-1);

          if ((number < 0) || (number > myLista.Count))
            continue;

          if (((Label)c).Visible == false)
            continue;
          
          if ((c == sender) && (!resetClick))
          {
            string dtext = myLista[number].First + "\n" + myLista[number].Second;
            SizeF f2 = ((Label)c).CreateGraphics().MeasureString(dtext, ((Label)c).Font, this.flowLayoutPanel.Width);

            ((Label)c).Size = new System.Drawing.Size(this.flowLayoutPanel.Width - scroll, (int)f2.Height + 9);
            ((Label)c).Text = dtext;
            if (((Label)c).Text.Contains("Stop"))
              ((Label)c).BackColor = Color.Tomato;
            else
              ((Label)c).BackColor = Color.LightSteelBlue;

            selectedNumber = number;
          }
          else
          {
            ((Label)c).Size = new System.Drawing.Size(this.flowLayoutPanel.Width - scroll, 25);
            ((Label)c).Text = myLista[number].First;
            if (((Label)c).Text.Contains("Stop"))
              ((Label)c).BackColor = Color.Red;
            else
              ((Label)c).BackColor = Color.White;
          }
        }
      }

      this.flowLayoutPanel.ScrollControlIntoView((Label)sender);

      if ((this.OnMyListBoxSelectedIndexChangedHandler != null) && (!resetClick))
        OnMyListBoxSelectedIndexChangedHandler(this, myLista[selectedNumber].First);

      this.ResumeLayout(false);
    }

    private Label getControl(bool createIfNotExist, string name)
    {
      Label res = null;

      foreach (Control c in this.flowLayoutPanel.Controls)
      {
        if (c is Label)
        {
          if (c.Name == name)
            res = (Label)c;
        }
      }

      int scroll = 0;
      if (((ScrollableControl)this.flowLayoutPanel).VerticalScroll.Visible)
        scroll = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

      if (res == null)
      {
        // 
        // lbl_N
        // 
        Label lbl_N = new Label();
        lbl_N.AutoEllipsis = true;
        lbl_N.BackColor = Color.White;
        lbl_N.Cursor = System.Windows.Forms.Cursors.Hand;
        lbl_N.Font = this.Font;
        lbl_N.Location = new System.Drawing.Point(0, 0);
        lbl_N.Margin = new System.Windows.Forms.Padding(0,1,0,0);
        lbl_N.Name = name;
        lbl_N.Size = new System.Drawing.Size(this.flowLayoutPanel.Width - scroll, 25);
        lbl_N.TabIndex = 0;
        lbl_N.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lbl_N.Click += new System.EventHandler(this.labelN_Click);

        this.flowLayoutPanel.Controls.Add(lbl_N);

        res = lbl_N;
      }

      res.Visible = true;

      return res;
    }
  }

  public class Triple<T, U>
  {
    public Triple()
    {
    }

    public Triple(DateTime dateTime, T first, U second)
    {
      this.First = first;
      this.Second = second;
      this.Orario = dateTime;
    }

    public T First { get; set; }
    public U Second { get; set; }
    public DateTime Orario { get; set; }
  };
}
