using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace DeepScannerSettings
{
  public class DeepScanEntry : IExposable
  {
    public ThingDef defName;
    public float commonality;
    public float numberMined;
    public IEnumerable<int> veinSizeRange;

    public DeepScanEntry() { }

    public DeepScanEntry(ThingDef defName, float commonality, float numberMined, IEnumerable<int> veinSizeRange)
    {
      this.defName = defName;
      this.commonality = commonality;
      this.numberMined = numberMined;
      this.veinSizeRange = veinSizeRange;
    }

    public void ExposeData()
    {
      Scribe_Defs.Look(ref defName, "defName");
      Scribe_Values.Look(ref commonality, "commonality");
      Scribe_Values.Look(ref numberMined, "numberMined");
      Scribe_Values.Look(ref veinSizeRange, "veinSizeRange");
    }
  }
}
