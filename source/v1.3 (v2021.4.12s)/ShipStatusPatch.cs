using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using System.Reflection;

using InnerPlayerControl = PlayerControl;

using OxygenSystem = GIICFCLBGOD;
using ReactorSystem = NHIJNMDDACI;
using SystemType = BCPJLGGNHBC;
using Hazel;

namespace LoveCoupleMod
{
	[HarmonyPatch(typeof(ShipStatus))]
	public class ShipStatusPatch
	{


		[HarmonyPostfix]
		[HarmonyPatch(typeof(ShipStatus),"IsGameOverDueToDeath")]
		public static void Postfix(ShipStatus __instance, ref bool __result)
		{
				__result = false;
		}

		
		[HarmonyPatch(typeof(ShipStatus), "KMPKPPGPNIH")]
		public static bool Prefix(ShipStatus __instance)
		{

		

			if (__instance.CheckTaskCompletion() ) return true;
			if (__instance.Systems.ContainsKey(SystemType.Reactor))
			{
				if (__instance.GetIl2CppType() == AirshipStatus.Il2CppType)
				{
					HeliSabotageSystem Reactor = __instance.Systems[SystemType.Reactor].Cast<HeliSabotageSystem>();
					if (Reactor.LEJABJDEHPE & Reactor.GPBBPGOINOF <= 0) return true;
				}
				else
				{
					ReactorSystem Reactor = __instance.Systems[SystemType.Reactor].Cast<ReactorSystem>();
					if (Reactor.LEJABJDEHPE & Reactor.GPBBPGOINOF <= 0) return true;
				}

			}
			if (__instance.Systems.ContainsKey(SystemType.LifeSupp))
			{
				OxygenSystem oxygen = __instance.Systems[SystemType.LifeSupp].Cast<OxygenSystem>();
				if (oxygen.LEJABJDEHPE & oxygen.GPBBPGOINOF <= 0) return true;
			
			}
			if (__instance.Systems.ContainsKey(SystemType.Laboratory))
			{
				ReactorSystem Reactor = __instance.Systems[SystemType.Laboratory].Cast<ReactorSystem>();
				if (Reactor.LEJABJDEHPE & Reactor.GPBBPGOINOF <= 0) return true;
				
			}

			EndReason reason = Love.CheckWin();
			if (reason == EndReason.IgnoreEndReason) return true;
			if (reason == EndReason.NoWin) return false;
			

			if (reason == EndReason.LoveWin)
			{
				MessageWriter writer = AmongUsClient.Instance.StartRpc(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.LoveWin, Hazel.SendOption.Reliable);
				writer.Write((byte)reason);
				writer.EndMessage();

				PlayerControlPatch.LoveCoupleWins();
				ShipStatus.EABBNOODFGL(AMGMAKBHCMN.ImpostorByVote, true);
				return false;

			}
			return true;
		}
		

	}
	/*
	[HarmonyPatch] // make sure Harmony inspects the class
	class MyPatches
	{
		static IEnumerable<MethodBase> TargetMethods()
		{
			return AccessTools.GetDeclaredMethods(typeof(AmongUsClient))
				.Where(method => method.ReturnType == typeof(void) && method.GetParameters().Length==2 && !method.IsConstructor)
				.Cast<MethodBase>();
		}

		// prefix all methods in someAssembly with a non-void return type and beginning with "Player"
		static void Postfix(MethodBase __originalMethod)
		{
			LoveCoupleMod.log.LogInfo(__originalMethod.Name);
			// use __originalMethod to decide what to do
		}
	}
	*/



}
