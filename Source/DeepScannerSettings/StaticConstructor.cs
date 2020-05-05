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
      
    }
  }
}
