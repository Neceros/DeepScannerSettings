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

      for (int i = 0; i < oreList.Count(); ++i)
      {
        DSSSettings.DeepOreDefs.Add(oreList[i]);
        DSSSettings.Commonality.Add(oreList[i].deepCommonality);
        DSSSettings.MinedAmountPerChunk.Add(oreList[i].deepCountPerPortion);
        DSSSettings.VeinSizeRange.Add(oreList[i].deepLumpSizeRange);
      }
    }
  }
}
