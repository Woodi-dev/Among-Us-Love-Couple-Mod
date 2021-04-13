using HarmonyLib;


namespace LoveCoupleMod
{
    [HarmonyPatch(typeof(VersionShower), "Start")]
    public static class VersionShowerPatch
    {
        
        public static void Postfix(VersionShower __instance)
        {
            __instance.text.verticalAlignment = TMPro.VerticalAlignmentOptions.Top;
            __instance.text.fontSize = 2;
            __instance.text.text += "\nloaded Love Couple Mod v1.3 by <color=#FFD11AFF>Woodi</color>";

        }
    }

}
