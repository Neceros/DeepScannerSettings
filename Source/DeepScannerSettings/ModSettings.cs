using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace DeepScannerSettings
{
  public class DSSSettings : ModSettings
  {
    public static List<ThingDef> DeepOreDefs = new List<ThingDef>();
    public static List<float> Commonality = new List<float>();
    public static List<int> MinedAmountPerChunk = new List<int>();
    public static List<IntRange> VeinSizeRange = new List<IntRange>();

    public override void ExposeData()
    {
      base.ExposeData();
      Scribe_Collections.Look(ref DeepOreDefs, "DeepOreDefs", LookMode.Def);
      Scribe_Collections.Look(ref Commonality, "Commonality");
      Scribe_Collections.Look(ref MinedAmountPerChunk, "MinedAmountPerChunk");
      Scribe_Collections.Look(ref VeinSizeRange, "VeinSizeRange");
    }
  }

  public class DSSMod : Mod
  {
    public bool resetDefaults;
    public Vector2 scrollPosition;

    DSSSettings settings;
    public DSSMod(ModContentPack con) : base(con)
    {
      this.settings = GetSettings<DSSSettings>();
    }

    public override void DoSettingsWindowContents(Rect canvas)
    {
      Listing_Standard lister = new Listing_Standard();

      float numOfItems = DSSSettings.DeepOreDefs.Count() * 220f;
      float height = canvas.y + numOfItems;
      Rect viewRect = new Rect(0f, 0f, canvas.width - 260f, height);

      lister.Begin(canvas);
      lister.BeginScrollView(new Rect(120f, 0f, canvas.width - 240f, canvas.height), ref scrollPosition, ref viewRect);
      lister.Gap();

      for (int i = 0; i < DSSSettings.DeepOreDefs.Count(); ++i)
      {
        lister.Settings_Header(DSSSettings.DeepOreDefs[i].label, Color.clear, GameFont.Medium, TextAnchor.MiddleLeft);
        lister.GapLine();

        float c = DSSSettings.Commonality[i];
        int m = DSSSettings.MinedAmountPerChunk[i];
        int r1 = DSSSettings.VeinSizeRange[i].min;
        int r2 = DSSSettings.VeinSizeRange[i].max;

        lister.Settings_Numericbox("Commonality".Translate(), ref c, 420, 12f);
        lister.Settings_IntegerBox("MinedAmountPerChunk".Translate(), ref m, 420, 12);
        lister.Settings_IntegerBox("VeinSizeRangeMin".Translate(), ref r1, 420, 12f);
        lister.Settings_IntegerBox("VeinSizeRangeMax".Translate(), ref r2, 420, 12f);

        DSSSettings.Commonality[i] = c;
        DSSSettings.MinedAmountPerChunk[i] = m;
        DSSSettings.VeinSizeRange[i] = new IntRange(r1, r2);

        lister.Gap(32f);
      }

      lister.End();
      lister.EndScrollView(ref viewRect);

      base.DoSettingsWindowContents(canvas);
    }

    public override void WriteSettings()
    {
      UpdateChanges();

      base.WriteSettings();
    }

    public override string SettingsCategory()
    {
      return "MenuTitle".Translate();
    }

    public static void UpdateChanges()
    {
      // *My own references, ignore*
      // DefDatabase<HediffDef>.GetNamed("SmokeleafHigh").stages[0].capMods[0].offset = LLLModSettings.amountPenaltyConsciousness;
      // HediffDef.Named("SmokeleafHigh").stages.Where((HediffStage stage) => stage.capMods.Any((PawnCapacityModifier mod) => mod.capacity == PawnCapacityDefOf.Consciousness)).First().capMods.Where((PawnCapacityModifier mod) => mod.capacity == PawnCapacityDefOf.Consciousness).First().offset = RSModSettings.amountCramped;
      // ThingDef.Named("NEC_ReinforcedWall").statBases.Where((StatModifier statBase) => statBase.stat == StatDefOf.MaxHitPoints).First().value = RWModSettings.WallHitPoints;
      // SRWSettings.ReinforcedWall.statBases.Where((StatModifier statBase) => statBase.stat == StatDefOf.MaxHitPoints).First().value = SRWSettings.WallHitPoints;
      for (int i = 0; i < DSSSettings.DeepOreDefs.Count(); ++i)
      {
        ThingDef oreDef = DSSSettings.DeepOreDefs[i];
        oreDef.deepCommonality = DSSSettings.Commonality[i];
        oreDef.deepCountPerPortion = DSSSettings.MinedAmountPerChunk[i];
        oreDef.deepLumpSizeRange = DSSSettings.VeinSizeRange[i];
      }
    }

    private static float RoundToNearestHundredth(float val)
    {
      return (float)Math.Round(val * 100, MidpointRounding.AwayFromZero) / 100;
    }
  }
}
