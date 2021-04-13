using HarmonyLib;
using Il2CppSystem.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using InnerPlayerControl = PlayerControl;

using VoteArea = PlayerVoteArea;
namespace LoveCoupleMod
{
    [HarmonyPatch]
    class HudManagerPatch
    {
   

        public static HudManager HUD;

        public static void LoadSprites()
        {

            Heart.heart = ModUtils.createSprite("Assets/heart.asset", 250, 250, 100);
            Heart.heartsmall = ModUtils.createSprite("Assets/heart.asset", 250, 250, 700, new Vector2(-8f, 0.4f));

        }

        
        [HarmonyPatch(typeof(HudManager), "Update")]


        public static void Postfix(HudManager __instance)
        {
            HUD = __instance;



   

            CustomGameObjectManager.Update();
            PlayerController.Update();
            var localplayer = PlayerController.getLocalPlayer();
            if (localplayer != null)
            {
               
                if (localplayer.hasComponent("Love"))
                {
                    if (!HUD.Chat.isActiveAndEnabled) HUD.Chat.SetVisible(true);

                    Love love = (Love)localplayer.GetComponentByName("Love");
                    if (love.lover.isAlive())
                    {
                        HUD.TaskText.text = "<color=#FF80D5FF>Primary objective:</color>\n"
                       + "<color=#FFFFFFFF>Stay alive with your love </color><color=#FF80D5FF>" + love.lover.playerdata.name + "\n</color>and win together"
                        + "\n\nSecondary objective: \n" + HUD.FGIFCILIPEH.ToString();
                    }
                }

            }
               


      
        }
    }

   

}

