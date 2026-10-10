using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Debug
{
  public partial class Frm_DebugTwin2 : Frm_DebugGroup
  {
    private string[] bitDescriptionsDM206, bitDescriptionsDM207, bitDescriptionsDM208, bitDescriptionsDM209, bitDescriptionsDM210, bitDescriptionsDM211;
    
    public Frm_DebugTwin2()
    {
      InitializeComponent();
      this.bitDescriptionsDM206 = new string[]
       {"Emergenza da tesch", "Fault Azion. clt 1", "Fault Azion. clt 2", "Fault Azion. crd 1",
        "Fault Azion. crd 2", "Fault Azion. crd aux 1", "Fault Azion. crd aux 2", "Errore Triax 1",
        "Errore Triax 2", "Errore Triax 3", "Errore Triax 4", "Preallarme aria bassa",
        "ِAllarme aria", "Magnet. soffi cordoni", "Magnet. soffi coltelli", "Magnet. condiz. quadro"
       };
      this.dm206.Config("DM206", this.bitDescriptionsDM206, true, null);
      this.dm206.Debug = true;

      this.bitDescriptionsDM207 = new string[]
       {"Carrello clt sup fuori", "Carrello clt inf fuori", "Carrello crd sup fuori", "Carrello crd inf fuori",
        "Carrello crd aux sup fuori", "Carrello crd aux inf fuori", "", "",
        "", "", "", "",
        "", "", "", ""
       };
      this.dm207.Config("DM207", this.bitDescriptionsDM207, true, null);
      this.dm207.Debug = true;

      this.bitDescriptionsDM208 = new string[]
       {"Asse clt 1 lav controc. aperti", "Asse clt 2 lav controc. aperti", "Ev colt 1 chiuse", "Ev colt 2 chiuse",
        "", "", "", "",
        "", "", "", "",
        "", "", "", ""
       };
      this.dm208.Config("DM208", this.bitDescriptionsDM208, true, null);
      this.dm208.Debug = true;

      this.bitDescriptionsDM209 = new string[]
       {"Posiz. coltelli in corso", "Posiz. cordoni in corso", "Posiz. cord aux in corso", "Asse clt1 riposo",
        "Asse clt1 movimento", "Asse clt1 lavoro", "Asse crd1 riposo", "Asse crd1 movimento",
        "Asse crd1 lavoro", "Asse aux1 riposo", "Asse aux1 movimento", "Asse aux1 lavoro",
        "", "", "", ""
       };
      this.dm209.Config("DM209", this.bitDescriptionsDM209, true, null);
      this.dm209.Debug = true;

      this.bitDescriptionsDM210 = new string[]
       {"Triax 3 ok", "Triax 3 reset in corso", "Triax 3 resettato", "Asse clt2 riposo",
        "Asse clt2 movimento", "Asse clt2 lavoro", "Asse crd2 riposo", "Asse crd2 movimento",
        "Asse crd2 lavoro", "Asse aux2 riposo", "Asse aux2 movimento", "Asse aux2 lavoro",
        "", "", "", ""
       };
      this.dm210.Config("DM210", this.bitDescriptionsDM210, true, null);
      this.dm210.Debug = true;

      this.bitDescriptionsDM211 = new string[]
       {"Corr su clt1", "Corr su clt2", "Corr su crd1", "Corr su crd2",
        "Corr su aux1", "Corr su aux2", "", "",
        "Succ su clt1", "Succ su clt2", "Succ su crd1", "Succ su crd2",
        "Succ su aux1", "Succ su aux2", "", ""
       };
      this.dm211.Config("DM211", this.bitDescriptionsDM211, true, null);
      this.dm211.Debug = true;
    }

    private void _RefreshStatus(string s)
    {
      string[] stData;
      stData = s.Split(Command.PARAMETER_SEPARATOR);
      if (stData.Length != 14)
        return;
      int cnt = 0;
      this.lab_CurrentShaft.Text = stData[cnt++];
      this.lab_OrderChange.Text = stData[cnt++];
      this.lab_ScoreAngle1.Text = stData[cnt++];
      this.lab_ScoreGap1.Text = stData[cnt++];
      this.lab_ScoreAngle2.Text = stData[cnt++];
      this.lab_ScoreGap2.Text = stData[cnt++];
      this.lab_ScoreAngle3.Text = stData[cnt++];
      this.lab_ScoreGap3.Text = stData[cnt++];
      this.dm206.SetValue(this.intParse(stData[cnt++]));
      this.dm207.SetValue(this.intParse(stData[cnt++]));
      this.dm208.SetValue(this.intParse(stData[cnt++]));
      this.dm209.SetValue(this.intParse(stData[cnt++]));
      this.dm210.SetValue(this.intParse(stData[cnt++]));
      this.dm211.SetValue(this.intParse(stData[cnt++]));
    }
  }
}
