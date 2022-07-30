using System.Reflection;
using UnityEngine;
using HarmonyLib;
using System;

/*
Step 1: When equip shield, send a packet with the shield ID to the server (ClientSend)
Step 2: Calls serversend method (ServerHandle)
Step 3: sends to all clients except the original (ServerSend) 
Step 4: update GameManager.players[key].onlinePlayer (ClientHandle)
Step 5: Calls OnlinePlayer's UpdateWeapon
*/
namespace MuckShields
{
	public class OnlinePatch
	{

        [HarmonyPatch(typeof(LocalClient), "InitializeClientData")]
		[HarmonyPostfix]
		public static void AddShieldReceiverCH()
		{
			LocalClient.packetHandlers.Add(69, new LocalClient.PacketHandler(OnlinePatch.ShieldInHandCH));
		}

		[HarmonyPatch(typeof(Server), "InitializeServerPackets")]
		[HarmonyPostfix]
		public static void AddShieldReceiverSH()
		{
			Server.PacketHandlers.Add(69, new Server.PacketHandler(OnlinePatch.ShieldInHandSH));
		}

		[HarmonyPatch(typeof(GameManager), "SpawnPlayer")]
		[HarmonyPostfix]
        public static void AddShieldToPlayer(int id, string username, Color color, Vector3 position, float orientationY)
        {
			GameObject onlineShield = new GameObject();
            onlineShield.name = "OnlineShield";
			onlineShield.AddComponent<MeshFilter>();
			onlineShield.AddComponent<MeshRenderer>();
			onlineShield.transform.SetParent(GameManager.players[id].onlinePlayer.gameObject.transform.Find("newPlayer/Armature/Hips/Torso 1/Shoulder.L/Hand.L/Hand.L_end"));
			onlineShield.transform.localPosition = new Vector3(0, 0, 0);
			onlineShield.transform.localEulerAngles = new Vector3(80, 275, 170);
			onlineShield.transform.localScale = new Vector3(2, 2, 2);
			onlineShield.layer = LayerMask.NameToLayer("Player");
        }

		public static void ShieldInHandCS(int itemID)
		{
			try
			{
				using (Packet packet = new Packet(69))
				{
					packet.Write(itemID);
					typeof(ClientSend).GetMethod("SendTCPData", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { packet });
				}
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
		}

		public static void ShieldInHandCH(Packet packet)
		{
            int num = packet.ReadInt(); // fromClient
			int objectID = packet.ReadInt(true);
			ShieldImplementPatch.UpdateShield(num, objectID);
            
		}

		public static void ShieldInHandSS(int fromClient, int objectID)
		{
			using (Packet packet = new Packet(69))
			{
				packet.Write(fromClient);
				packet.Write(objectID);
				typeof(ServerSend).GetMethod("SendTCPDataToAll", (BindingFlags.NonPublic | BindingFlags.Static), null, new Type[] { typeof(int), typeof(int) }, null).Invoke(null, new object[] { fromClient, packet });
			}
		}

		public static void ShieldInHandSH(int fromClient, Packet packet)
		{
			if (Server.clients[fromClient].player == null) // should be redundant check
			{
				return;
			}
			int objectID = packet.ReadInt(true);
			ShieldInHandSS(fromClient, objectID);
		}
	}
}