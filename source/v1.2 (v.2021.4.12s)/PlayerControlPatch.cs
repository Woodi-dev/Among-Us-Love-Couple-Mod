using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;

using InnerPlayerControl = PlayerControl;
using GameOptions = CEIOGGEDKAN;



namespace LoveCoupleMod
{
	enum CustomRPC
	{

		SetCouple = 50,
		SyncCustomSettings = 51,
		LoveSuicide = 52,
		LoveWin=53

	}
	

	[HarmonyPatch]
    class PlayerControlPatch
    {
		public static List<Player> getCrewMates(Il2CppReferenceArray<GameData.LGBOMGHJELL> infection)
		{

			List<Player> Crewmates = new List<Player>();
			foreach (Player player in PlayerController.players)
			{

				bool isInfected = false;
				foreach (var infected in infection)
				{

					if (player.playerdata.PlayerId == infected.GJPBCGFPMOD.PlayerId)
					{
						isInfected = true;

						break;
					}

				}
				if (!isInfected)
				{
					Crewmates.Add(player);
				}


			}
			return Crewmates;

		}
		[HarmonyPatch(typeof(InnerPlayerControl),"HandleRpc")]
		public static void Postfix(byte ONIABIILFGF, MessageReader JIGFBHFFNFI)
        {
            var reader = JIGFBHFFNFI;
			
            switch (ONIABIILFGF)
			{

				case (byte)CustomRPC.SyncCustomSettings:
                    {
						CustomGameOptions.BothLoversDie = reader.ReadBoolean();
						break;
                    }
				case (byte)CustomRPC.SetCouple:
                    {
						byte lover1id = reader.ReadByte();
						byte lover2id = reader.ReadByte();
						byte r = reader.ReadByte();
						HudManagerPatch.LoadSprites();
						PlayerController.InitPlayers();
						CustomGameObjectManager.deleteAll();
						EndGameManagerPatch.LoveCoupleWins = false;

						Player lover1 = PlayerController.getPlayerById(lover1id);
						Player lover2 = PlayerController.getPlayerById(lover2id);

						lover1.components.Add(new Love(lover1,lover2));
						lover2.components.Add(new Love(lover2, lover1));
                        if (r == 0)
                        {
							((Love)lover2.GetComponentByName("Love")).spawnedAsImpostor = true;
                        }
						var localplayer = PlayerController.getLocalPlayer();
						if (lover1 == localplayer | lover2 == localplayer)
						{
							CustomGameObjectManager.AddObject(new Heart(lover1.playerdata.nameText));
							CustomGameObjectManager.AddObject(new Heart(lover2.playerdata.nameText));
						}
						break;

					}
				case (byte)CustomRPC.LoveWin:
					{
						LoveCoupleWins();
						break;
					}
				case (byte)CustomRPC.LoveSuicide:
					{
						byte target = reader.ReadByte();
						Player player = PlayerController.getPlayerById(target);
						player.playerdata.MurderPlayer(player.playerdata);
						break;
					}




			}
        }

		[HarmonyPatch(typeof(InnerPlayerControl), "RpcSetInfected")]
		public static void Postfix(Il2CppReferenceArray<GameData.LGBOMGHJELL> BHNEINNHPIJ)
        {
			PlayerController.InitPlayers();

			CustomGameObjectManager.deleteAll();
	
			EndGameManagerPatch.LoveCoupleWins = false;
			List<Player> crewmates = getCrewMates(BHNEINNHPIJ);
			int lover1idx = (byte)new System.Random().Next(0, crewmates.Count);


	

			int lover2idx = (byte)new System.Random().Next(0, crewmates.Count);
			if (lover1idx == lover2idx)
				lover2idx = (lover2idx + 1) % crewmates.Count;
       


			Player[] impostors = PlayerController.getImpostors();
			byte r = (byte)new System.Random().Next(0, 3);
			
			if (r == 0)
            {

				lover2idx=(byte)new System.Random().Next(0, impostors.Length);
		

			}

			Player lover1 = crewmates[lover1idx];
			Player lover2 = crewmates[lover2idx];
			if (r == 0)
			{
				lover2 = impostors[lover2idx];

			}



			MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetCouple, Hazel.SendOption.Reliable);

			writer.Write(lover1.playerdata.PlayerId);

			writer.Write(lover2.playerdata.PlayerId);
			writer.Write(r);
			writer.EndMessage();
			lover1.components.Add(new Love(lover1, lover2));
			lover2.components.Add(new Love(lover2, lover1));
			HudManagerPatch.LoadSprites();
			var localplayer = PlayerController.getLocalPlayer();
			if (lover1 == localplayer | lover2 == localplayer)
			{
				CustomGameObjectManager.AddObject(new Heart(lover1.playerdata.nameText));
				CustomGameObjectManager.AddObject(new Heart(lover2.playerdata.nameText));
			}

		}

		[HarmonyPatch(typeof(InnerPlayerControl), "RpcSyncSettings")]
		public static void Postfix(CEIOGGEDKAN DJGAEEMDIDF)
		{
			if (PlayerControl.AllPlayerControls.Count > 1)
			{
				MessageWriter writer = AmongUsClient.Instance.StartRpc(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SyncCustomSettings, Hazel.SendOption.Reliable);
				writer.Write(CustomGameOptions.BothLoversDie);

				writer.EndMessage();
			}
		}
		[HarmonyPatch(typeof(InnerPlayerControl), "Die")]
		public static bool Prefix(InnerPlayerControl __instance, EGHDCAKGMKI AMGCOJNIHLM)
		{



			if (AmongUsClient.Instance.ClientId == AmongUsClient.Instance.HostId)
				{
				__instance.FIMGDJOCIGD.IAGJEKLJCCI = true;

				Player player = PlayerController.getPlayerById(__instance.PlayerId);
				
                if (player.hasComponent("Love") && CustomGameOptions.BothLoversDie)
                {
					Love love = (Love)player.GetComponentByName("Love");
					MessageWriter writer = AmongUsClient.Instance.StartRpc(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.LoveSuicide, Hazel.SendOption.Reliable);
					writer.Write((byte)love.lover.playerdata.PlayerId);
					writer.EndMessage();
					love.lover.playerdata.MurderPlayer(love.lover.playerdata);
                }

			}
         
	
			return true;

		}
		public static void LoveCoupleWins()
		{
			Player[] lovers = Love.getLovers();
			foreach (Player player in PlayerController.getImpostors())
			{
				player.setImpostor(false);

			}
			foreach (Player player in lovers)
			{
				player.setImpostor(true);
			}

			EndGameManagerPatch.LoveCoupleWins = true;
		}

		[HarmonyPatch(typeof(PlayerControl), "MurderPlayer")]
		public static bool Prefix(PlayerControl __instance, PlayerControl DGDGDKCCKHJ)
		{
			Player player = PlayerController.getPlayerById(__instance.PlayerId);
			if (player.hasComponent("Love"))
			{			
				player.setImpostor(true);
			}
			return true;
		}

		[HarmonyPatch(typeof(PlayerControl), "MurderPlayer")]
		public static void Postfix(PlayerControl __instance, PlayerControl DGDGDKCCKHJ)
		{
			Player player = PlayerController.getPlayerById(__instance.PlayerId);
			Love love = (Love)player.GetComponentByName("Love");
			if (love!=null)
			{
				if(love.spawnedAsImpostor==false)
				player.setImpostor(false);


			}
		


		}

	}

}
