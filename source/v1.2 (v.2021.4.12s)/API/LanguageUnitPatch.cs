using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnhollowerBaseLib;
using LanguageUnit = LCCPIGHHLOH;
namespace LoveCoupleMod
{


    public enum CustomStringNames {

        CustomServerName = 3000
    }
    [HarmonyPatch]
    public class LanguageUnitPatch
    {


        [HarmonyPatch(typeof(LanguageUnit), "KCCAEECMGNJ")]
        public static bool Prefix(StringNames BPHOFEKLDLF, string BIHKOFGIABL, Il2CppReferenceArray<Il2CppSystem.Object> FHLKFONKJLH, ref string __result)
        {

            if (BPHOFEKLDLF == (StringNames)CustomStringNames.CustomServerName)
            {
                __result = LoveCoupleMod.Name.Value;
                return false;
            }         
            return true;
        }
  
    }
}
