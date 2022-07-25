using System;
using System.Collections.Generic;
using HarmonyLib;
using TMPro;
using UnityEngine;
using System.Reflection;

namespace MuckShields
{
	public class CreateItemsPatch
	{
		
		[HarmonyPatch(typeof(ItemManager), "InitAllItems")]
		[HarmonyPostfix]
		private static void Postfix()
		{
			Debug.Log("\n\n\n\n\n\n\nHELLO\n\n\n\n\n\n\n\n");
			initShields();
			bool flag = ItemManager.Instance.allItems.Count < 1;
			if (!flag)
			{
				Debug.Log("Adding Shields");
				foreach (NewItem newItem in Shields.newItems)
				{
					InventoryItem inventoryItem = ScriptableObject.CreateInstance<InventoryItem>();
					foreach (InventoryItem inventoryItem2 in ItemManager.Instance.allItems.Values)
					{
						bool flag2 = inventoryItem2.name == newItem.copyName;
						if (flag2)
						{
							inventoryItem.Copy(inventoryItem2, 1);
							break;
						}
					}
					inventoryItem.name = newItem.name;
					inventoryItem.description = newItem.description;
					inventoryItem.id = ItemManager.Instance.allItems.Count;
					inventoryItem.armor *= newItem.factor;
					inventoryItem.material = new Material(inventoryItem.material);
					inventoryItem.material.mainTexture = newItem.texture;
					inventoryItem.material.color = new Color(1f, 1f, 1f, 1f);
					inventoryItem.sprite = newItem.sprite;
					inventoryItem.mesh = newItem.mesh;
					inventoryItem.tag = newItem.tag;
					inventoryItem.requirements = newItem.requirements;
					ItemManager.Instance.allItems.Add(inventoryItem.id, inventoryItem);
					Debug.Log("Added: " + inventoryItem.name);
					newItem.gameItem = inventoryItem;
				}
			}
		}

		private static void initShields()
		{
			Stream embeddedResourceStream = GetEmbeddedResourceStream("MuckShields.Resources.resources");
			AssetBundle assetBundle = AssetBundle.LoadFromStream(embeddedResourceStream);
			if (assetBundle == null)
			{
				throw new Exception("Failed to load AssetBundle ui!");
			}
			Shields.steel = assetBundle.LoadAsset<Texture2D>("Shield_Texture_Color_Steel");
			Shields.mithril = assetBundle.LoadAsset<Texture2D>("Shield_Texture_Color_Mithril");
			Shields.adamantite = assetBundle.LoadAsset<Texture2D>("Shield_Texture_Color_Adamantite");
			Shields.obamium = assetBundle.LoadAsset<Texture2D>("Shield_Texture_Color_Obamium");
			Shields.night = assetBundle.LoadAsset<Texture2D>("Shield_Texture_Color_Night");
			Shields.steel_sprite = assetBundle.LoadAsset<Sprite>("Shield_Sprite_Color_Steel");
			Shields.mithril_sprite = assetBundle.LoadAsset<Sprite>("Shield_Sprite_Color_Mithril");
			Shields.adamantite_sprite = assetBundle.LoadAsset<Sprite>("Shield_Sprite_Color_Adamantite");
			Shields.obamium_sprite = assetBundle.LoadAsset<Sprite>("Shield_Sprite_Color_Obamium");
			Shields.night_sprite = assetBundle.LoadAsset<Sprite>("Shield_Sprite_Color_Night");
			Shields.shield = assetBundle.LoadAsset<Mesh>("Shield_Mesh");
			Debug.Log("lmao");
			Debug.Log(Shields.steel);
			Debug.Log(Shields.steel_sprite);
			Debug.Log(Shields.shield);
			Debug.Log("lmao2");

			NewItem e = new NewItem("Steel Chestplate", "Steel Shield", "it protecc", Shields.steel, Shields.steel_sprite, Shields.shield, new List<string>() { "Iron bar", "Wood" }, new List<int>() { 5, 5 }, 1);
			Debug.Log("lmaoe");
			Debug.Log(e);
			Debug.Log("lmaoee");
			
			Shields.newItems.Add(e);
			Debug.Log("lmaoeee");

			Shields.newItems.Add(new NewItem("Mithril Chestplate", "Mithril Shield", "it protecc", Shields.mithril, Shields.mithril_sprite, Shields.shield, new List<string>() { "Mithril bar", "Birch Wood" }, new List<int>() { 5, 5 }, 2));
			Shields.newItems.Add(new NewItem("Adamantite Chestplate", "Adamantite Shield", "it protecc", Shields.adamantite, Shields.adamantite_sprite, Shields.shield, new List<string>() { "Adamantite bar", "Fir Wood" }, new List<int>() { 5, 5 }, 2));
			Shields.newItems.Add(new NewItem("Obamium Chestplate", "Obamium Shield", "get down mr president!", Shields.obamium, Shields.obamium_sprite, Shields.shield, new List<string>() { "Obamium bar", "Dark Oak Wood" }, new List<int>() { 5, 5 }, 2));
			Shields.newItems.Add(new NewItem("Obamium Chestplate", "Night Shield", "Dark as the night sky", Shields.night, Shields.night_sprite, Shields.shield, new List<string>() { "Black Shard", "Dark Oak Wood" }, new List<int>() { 1, 5 }, 2));
		}


		public static Stream GetEmbeddedResourceStream(string resourceName)
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
		}

		
		[HarmonyPatch(typeof(CraftingUI), "Awake")]
		[HarmonyPostfix]
		private static void CraftingPostfix(CraftingUI __instance)
		{
			bool flag = __instance.tabParent.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text.Equals("Armor");
			if (flag)
			{
				Debug.Log("Anvil");
				foreach (NewItem newItem in Shields.newItems)
				{
					InventoryItem[] items = __instance.tabs[2].items;
					InventoryItem[] array = new InventoryItem[items.Length + 1];
					items.CopyTo(array, 0);
					array[items.Length] = newItem.gameItem;
					__instance.tabs[2].items = array;
				}
			}
			else
			{
				Debug.Log("Not anvil, start crying");
			}
		}
	}
}
