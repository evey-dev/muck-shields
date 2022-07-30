using System.Reflection;
using UnityEngine;
using HarmonyLib;

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

		public void Awake()
		{
			LocalClient.packetHandlers.Add(69, new LocalClient.PacketHandler(OnlinePatch.ShieldInHandCH));
			
		}

		// [HarmonyPatch(typeof(ServerSend), "SendTCPDataToAll", new Type[] {typeof(int), typeof(Packet)})]
		// [HarmonyPrefix]
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
            int num = packet.ReadInt();
			int objectID = packet.ReadInt(true);
			ShieldImplementPatch.UpdateShield(key, objectID);
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
			if (Server.clients[fromClient].player == null)
			{
				return;
			}
			int objectID = packet.ReadInt(true);
			ShieldInHandSS(fromClient, objectID);
		}
	}
}