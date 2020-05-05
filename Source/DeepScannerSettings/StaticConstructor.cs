using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace DeepScannerSettings
{
  [StaticConstructorOnStartup]
  public static class StartUp
  {
    static StartUp()
    {
      // Loads right before main menu
      SetOriginalValues();

      DSSMod.UpdateChanges();
    }

    public static void SetOriginalValues()
    {
      List<ThingDef> oreList = DefDatabase<ThingDef>.AllDefs.Where(o => o.deepCommonality > 0).ToList();
      Log.Warning(oreList.Count().ToString());

      foreach (ThingDef item in oreList)
      {
        Log.Warning(item.defName + " is a deep ore.");
      }
    }
  }
}
