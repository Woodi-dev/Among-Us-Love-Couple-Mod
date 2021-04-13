using HarmonyLib;
using Hazel;
using InnerNet;
using System;
using System.Collections.Generic;
using System.Text;




using InnerPlayerControl = PlayerControl;
namespace LoveCoupleMod
{
    [HarmonyPatch(typeof(AmongUsClient))]
    class AmongUsClientPatch
    {


        [HarmonyPostfix]
        [HarmonyPatch("LIOEBBDDACD")]
        public static void Postfix1(AmongUsClient __instance, ClientData MLPPNIKICPC, GABADEGMIHF AMGCOJNIHLM)
        {
            MLPPNIKICPC.Character.FIMGDJOCIGD.IAGJEKLJCCI = true;

            if (PlayerController.players != null)
            {
                if (AmongUsClient.Instance.ClientId == AmongUsClient.Instance.HostId)
                {
                    EndReason reason = Love.CheckWin();

                    if (reason == EndReason.LoveWin)
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.LoveWin, Hazel.SendOption.Reliable, -1);
                        writer.Write((byte)reason);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);

                        PlayerControlPatch.LoveCoupleWins();
                        ShipStatus.EABBNOODFGL(AMGMAKBHCMN.ImpostorByVote, false);

                    }
                }
            }

        }
        [HarmonyPrefix]
        [HarmonyPatch("DDIEDPFFHOG")]

        public static bool Prefix1(AmongUsClient __instance, AMGMAKBHCMN NEPMFBMGGLF, bool FBEKDLNKNLL)
        {
            if (FBEKDLNKNLL)
            {
                LoveCoupleMod.log.LogInfo("Love couple wins");
                PlayerControlPatch.LoveCoupleWins();
                FBEKDLNKNLL = false;
            }
            return true;


        }

    }
}
