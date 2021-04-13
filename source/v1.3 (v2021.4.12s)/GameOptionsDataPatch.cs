using HarmonyLib;
using GameOptionsData = CEIOGGEDKAN;
namespace LoveCoupleMod
{
    [HarmonyPatch(typeof(GameOptionsData))]
    public class GameOptionsDataPatch
    {


        //Update lobby options text
        [HarmonyPostfix]
        [HarmonyPatch("ONCLFHFDADB")]
        public static void Postfix1(GameOptionsData __instance, ref string __result, int JFGKGCCMCNK)
        {
            if (CustomGameOptions.BothLoversDie)
               __result +=  "Both Lovers Die: On" + "\n";
            else
               __result+="Both Lovers Die: Off" + "\n";
            __result += "><color=#FF80D5FF> Love Couple Mod </color><";

        }

       
    }
    
    
}
