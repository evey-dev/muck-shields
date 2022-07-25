// InventoryUI.allcells[29] <- left hand slot
// cell.currentItem == null
// (float)Server.clients[pnum].player.totalArmor
using HarmonyLib;
using UnityEngine;

namespace MuckShields
{
	internal class ShieldImplementPatch
	{
		public static int pnum;
		public static int armor;
		[HarmonyPatch(typeof(ServerHandle), "PlayerHit")]
		[HarmonyPrefix]
		private static void ShieldMouseCheck(int fromClient, Packet packet)
		{
			InventoryUI inventoryUI = GameObject.Find("InventoryNew").GetComponent<InventoryUI>();
			if (inventoryUI.allCells[29].currentItem != null)
			{
				armor = inventoryUI.allCells[29].currentItem.armor;
				Packet copy = new Packet(packet.ToArray());
				copy.ReadInt();
				pnum = copy.ReadInt();
				if (Input.GetMouseButton(1))
				{
					Server.clients[pnum].player.totalArmor += armor;
				}
			}
			armor = 0;
		}
		[HarmonyPatch(typeof(ServerHandle), "PlayerHit")]
		[HarmonyPostfix]
		private static void ShieldArmorRemove(int fromClient, Packet packet)
		{
			if (Input.GetMouseButton(1))
			{
				Server.clients[pnum].player.totalArmor -= armor;
			}
		}
	}
}