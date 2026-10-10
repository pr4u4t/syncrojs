using System;
using System.Collections.Generic;
using System.Text;

namespace SyncroC.Utility
{
	class LocalValidationLimits
	{
		//Frm_Operator
		public static readonly int DryEndRSHCutsRequired_WarningMaxLimit = 10;
		public static readonly int DryEndRSHCutsRequired_WarningMinLimit = 1;

		public static readonly int WetEndRSHCutsRequired_WarningMaxLimit = 10;
		public static readonly int WetEndRSHCutsRequired_WarningMinLimit = 1;

		public static readonly int spliceRemovalRSHCutsRequired_WarningMaxLimit = 10;
		public static readonly int spliceRemovalRSHCutsRequired_WarningMinLimit = 2;

		public static readonly int operatorMaxProductionSpeed_WarningMinLimit_UVmm_m = 60000;

		public static readonly int slowSpeed_WarningMinLimit_UVmm_m = 30000;

		public static readonly int OperatorMaxAOCSpeed_WarningMinLimit_UVmm_m = 40000;

		public static readonly int OperatorSlowAOCSpeed_WarningMinLimit_UVmm_m = 20000;

    //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
    public static readonly int OperatorOrderLengthForMaxProductionSpeed_maxLength_m = 400;

		//Frm_Quality
		public static readonly int ProductionSpeed_WarningMinLimit_UVm_m = 60;

		public static readonly int AOCSpeed_WarningMinLimit_UVm_m = 40;

	}
}
