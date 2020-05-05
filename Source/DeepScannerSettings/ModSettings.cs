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
    public static List<ThingDef> DeepScanDefs;

    public override void ExposeData()
    {
      base.ExposeData();
      Scribe_Collections.Look(ref DeepScanDefs, "DeepScanDefs");
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

      // lister.ColumnWidth = canvas.width - 80f;
      float height = canvas.y + 800f; // set height here
      Rect viewRect = new Rect(0f, 0f, canvas.width - 600f, height);


      lister.Begin(canvas);
      lister.BeginScrollView(new Rect(120f, 0f, canvas.width - 240f, canvas.height), ref scrollPosition, ref viewRect);
      lister.Settings_Header("YieldsHeader".Translate(), Color.clear, GameFont.Medium, TextAnchor.MiddleLeft);
      lister.GapLine();
      lister.Gap(12f);



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
      
    }

    private static float RoundToNearestHundredth(float val)
    {
      return (float)Math.Round(val * 100, MidpointRounding.AwayFromZero) / 100;
    }
  }
}
