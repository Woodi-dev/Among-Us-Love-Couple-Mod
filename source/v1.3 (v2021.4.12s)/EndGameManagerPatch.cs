using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using InnerPlayerControl = PlayerControl;


namespace LoveCoupleMod
{
    [HarmonyPatch]
    class EndGameManagerPatch
    {
        public static bool LoveCoupleWins = false;
        [HarmonyPatch(typeof(EndGameManager), "Start")]
        public static void Postfix(EndGameManager __instance)
        {
            CustomGameObjectManager.deleteAll();
            HudManagerPatch.LoadSprites();
            if (LoveCoupleWins)
            {
                PoolablePlayer[] players = GameObject.FindObjectsOfType<PoolablePlayer>();
                if (players[0] != null) {
                    players[0].gameObject.transform.position -= new Vector3(1.5f, 0, 0);
                    players[0].SetFlipX(true);
                    players[0].NameText.text = "<color=#FF80D5FF>" + players[0].NameText.text+"</color>";
                }
                if (players[1] != null)
                {
                    players[1].SetFlipX(false);
                    players[1].gameObject.transform.position = players[0].gameObject.transform.position + new Vector3(1.2f, 0, 0);
                    players[1].gameObject.transform.localScale *= 0.92f;
                    players[1].HatSlot.transform.position += new Vector3(0.1f, 0, 0);
                    players[1].NameText.text = "<color=#FF80D5FF>" + players[1].NameText.text + "</color>";

                }
                __instance.BackgroundBar.material.color = Love.color;
                var wintext = GameObject.Instantiate(players[0].NameText);
                wintext.text = "<color=#FF80D5FF>Love couple wins</color > ";
                wintext.transform.position += new Vector3(0, 1.4f, 0);
                wintext.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                wintext.color = new Color(1, 1, 1, 1);
                Heart heart = new Heart();
                heart.go.transform.position = players[0].transform.position + new Vector3(0.35f, 0.55f, 0);
                heart.go.transform.localScale = new Vector3(0.2f, 0.2f, 1);
                CustomGameObjectManager.AddObject(heart);
            }

         
        }
        [HarmonyPatch(typeof(EndGameManager), "Start")]
        public static void Prefix(EndGameManager __instance)
        {
       

            if (LoveCoupleWins) {
                foreach (var winner in MFEGHOFFKKA.BPDANAHEJDD)
                {

                    winner.IAGJEKLJCCI = false;
                }
            }
        }


    }

}
