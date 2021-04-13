using HarmonyLib;
using System;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;





namespace LoveCoupleMod
{
    [HarmonyPatch(typeof(GameOptionsMenu))]
    public static class GameOptionMenuPatch
    {
        public static ToggleOption bothLoversDie;
        public static GameOptionsMenu instance;

        //Lobby options menu get opened
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void Postfix1(GameOptionsMenu __instance)
        {

            instance = __instance;
            CustomPlayerMenuPatch.AddOptions();

        }
        //Not a good solution, but works. Set both options to the bottom.
        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        public static void Postfix2(GameOptionsMenu __instance)
        {

            OptionBehaviour option = __instance.MCAHCPOHNFI[__instance.MCAHCPOHNFI.Count - 2];

            if (bothLoversDie != null)
            {
                bothLoversDie.transform.position = option.transform.position - new Vector3(0, 0.5f, 0);
            }

        }
    }
    //Change the toggle behaviour of our custom options.
    [HarmonyPatch]
    public static class ToggleButtonPatch
    {
        [HarmonyPatch(typeof(ToggleOption), "Toggle")]
        public static bool Prefix(ToggleOption __instance)
        {

            if (__instance.TitleText.text == "Both Lovers Die")
            {
                CustomGameOptions.BothLoversDie = !CustomGameOptions.BothLoversDie;
                PlayerControl.LocalPlayer.RpcSyncSettings(PlayerControl.GameOptions);

                __instance.LCDAKOCANPH = CustomGameOptions.BothLoversDie;
                __instance.CheckMark.enabled = CustomGameOptions.BothLoversDie;
                return false;

            }
            return true;

        }

    }
   

    [HarmonyPatch(typeof(CustomPlayerMenu))]
    public class CustomPlayerMenuPatch
    {
        public static void deleteOptions(bool destroy)
        {
            if (GameOptionMenuPatch.bothLoversDie != null )
            {
                GameOptionMenuPatch.bothLoversDie.gameObject.SetActive(false);
                if (destroy)
                {
                    GameObject.Destroy(GameOptionMenuPatch.bothLoversDie);
                    GameOptionMenuPatch.bothLoversDie = null;
                }
            }

        }
        public static void AddOptions()
        {
            if (GameOptionMenuPatch.bothLoversDie == null )
            {
                //we simply copy existing options...
                var toggleopt = GameObject.FindObjectsOfType<ToggleOption>().ToList().First();
                GameOptionMenuPatch.bothLoversDie = GameObject.Instantiate(toggleopt);


                OptionBehaviour[] options = new OptionBehaviour[GameOptionMenuPatch.instance.MCAHCPOHNFI.Count + 1];
                GameOptionMenuPatch.instance.MCAHCPOHNFI.ToArray().CopyTo(options, 0);
                options[options.Length - 1] = GameOptionMenuPatch.bothLoversDie;
                GameOptionMenuPatch.instance.MCAHCPOHNFI = new Il2CppReferenceArray<OptionBehaviour>(options);

            }
            else
            {

                GameOptionMenuPatch.bothLoversDie.gameObject.SetActive(true);
            }

            GameOptionMenuPatch.bothLoversDie.TitleText.text = "Both Lovers Die";
            GameOptionMenuPatch.bothLoversDie.LCDAKOCANPH = CustomGameOptions.BothLoversDie;
            GameOptionMenuPatch.bothLoversDie.CheckMark.enabled = CustomGameOptions.BothLoversDie;

        }

        [HarmonyPostfix]
        [HarmonyPatch("Close")]
        public static void Postfix1(CustomPlayerMenu __instance, bool DOGNCNIKKIP)
        {
            deleteOptions(true);
        }

        //if we switch tabs our custom buttons get deleted
        [HarmonyPrefix]
        [HarmonyPatch("OpenTab")]
        public static void Prefix1(GameObject NCDEAICDCNC)
        {

            if (NCDEAICDCNC.name == "GameGroup" && GameOptionMenuPatch.instance != null)
            {
                AddOptions();
            }
            else
            {
                deleteOptions(false);
            }

        }

    }
}

