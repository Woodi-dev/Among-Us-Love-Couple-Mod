using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InnerPlayerControl = PlayerControl;
using VoteArea = PlayerVoteArea;
using UnityEngine;

namespace LoveCoupleMod
{
    [HarmonyPatch(typeof(MeetingHud))]
    public static class MeetingHUDPatch
    {
        public static bool MeetingHUDopen;
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void Postfix1(MeetingHud __instance)
        {
            if (PlayerController.getLocalPlayer().hasComponent("Love"))
            {

                Love love = (Love)PlayerController.getLocalPlayer().GetComponentByName("Love");

                foreach (VoteArea votearea in __instance.GBKFCOAKLAH)
                {
                    if (votearea.NameText.text == love.parent.playerdata.name |
                        votearea.NameText.text == love.lover.playerdata.name)
                    {
                       SpriteRenderer rend = votearea.gameObject.AddComponent<SpriteRenderer>();
                       
                       rend.sprite = Heart.heartsmall;
                        
                    



                    }

                }

            }
            MeetingHUDopen = true;
        }
        [HarmonyPostfix]
        [HarmonyPatch("Close")]
        public static void Postfix2(MeetingHud __instance)
        {

            MeetingHUDopen = false;


        }
    
    }
}
