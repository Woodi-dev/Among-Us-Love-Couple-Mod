using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LoveCoupleMod
{
	[HarmonyPatch]
	public static class IntroCutScenePatch
	{
		[HarmonyPatch(typeof(IntroCutscene.EMGDLDOHGCK), "MoveNext")]
		public static void Postfix(IntroCutscene.EMGDLDOHGCK __instance)
		{
			var localplayer = PlayerController.LocalPlayer;
			if (localplayer == null) return;
			if (localplayer.hasComponent("Love"))
			{
				Love love = (Love)localplayer.GetComponentByName("Love");
				__instance.__4__this.Title.text = "Lover";
                if (PlayerController.getLocalPlayer().isImpostor())
                {
					__instance.__4__this.Title.text = "Loving Impostor";
					__instance.__4__this.Title.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);

				}
				__instance.__4__this.Title.color = Love.color;
				__instance.__4__this.ImpostorText.gameObject.SetActive(true);
				__instance.__4__this.ImpostorText.color = new Color(1, 1, 1, 1);
				__instance.__4__this.ImpostorText.text = "You are in <color=#FF66CCFF>Love</color> with <color=#FF66CCFF>" + love.lover.playerdata.name+"</color>";
				__instance.__4__this.BackgroundBar.material.color = Love.color;

			}
           

		}


	}
}
