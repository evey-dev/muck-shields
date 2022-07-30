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
		private static void UpdateShield(int key, int objectID) {
			GameManager.players[key].onlinePlayer;

			inventoryShield.AddComponent<MeshFilter>();
			inventoryShield.AddComponent<MeshRenderer>();
			inventoryShield.transform.SetParent(GameObject.Find("OnlinePlayer(Clone)/newPlayer/Armature/Hips/Torso 1/Shoulder.L/Hand.L/Hand.L_end/").transform);
			inventoryShield.transform.localPosition = new Vector3(0, 0, 0);
			inventoryShield.transform.localEulerAngles = new Vector3(80, 275, 170);
			inventoryShield.transform.localScale = new Vector3(2, 2, 2);
			inventoryShield.layer = LayerMask.NameToLayer("Player");
		}
	}
}