using HarmonyLib;

using InnerPlayerControl = PlayerControl;

namespace LoveCoupleMod
{
    [HarmonyPatch]
    public static class ChatControllerPatch
    {
        public static bool isImpostor;

        [HarmonyPatch(typeof(ChatController), "AddChat")]
        public static bool Prefix(PlayerControl LLKPJLNBAHL, string NKGAHDGIADB)
        {
            var localplayer = PlayerController.getLocalPlayer();
            if (localplayer != null)
            {
                if (localplayer.hasComponent("Love") || !localplayer.isAlive() || MeetingHUDPatch.MeetingHUDopen)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            return true;
        }


    }
}