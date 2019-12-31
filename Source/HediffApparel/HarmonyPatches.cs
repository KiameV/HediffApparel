using Harmony;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace HediffApparel
{
    [StaticConstructorOnStartup]
    partial class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = HarmonyInstance.Create("com.hediffapparel.rimworld.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Message(
                "HediffApparel Harmony Patches:" + Environment.NewLine +
                "  Postfix:" + Environment.NewLine +
                "    Pawn_ApparelTracker.Notify_ApparelAdded" + Environment.NewLine +
                "    Pawn_ApparelTracker.Notify_ApparelRemoved");
        }
    }

    [HarmonyPatch(typeof(Pawn_ApparelTracker), "Notify_ApparelAdded")]
    static class Patch_Pawn_ApparelTracker_Notify_ApparelAdded
    {
        public static void Postfix(Apparel apparel)
        {
            apparel.BroadcastCompSignal(CompHediffApparel.AddHediffsToPawnSignal);
        }
    }

    [HarmonyPatch(typeof(Pawn_ApparelTracker), "Notify_ApparelRemoved")]
    static class Patch_Pawn_ApparelTracker_Notify_ApparelRemoved
    {
        public static void Postfix(Apparel apparel)
        {
            apparel.BroadcastCompSignal(CompHediffApparel.RemoveHediffsFromPawnSignal);
        }
    }
}
