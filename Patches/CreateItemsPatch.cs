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
		public static GameObject leftHandCell;
		[HarmonyPatch(typeof(InventoryUI), "Awake")]
		[HarmonyPostfix]
		private static void AddComponentToCell() {
			leftHandCell = GameObject.Find("InventoryNew").GetComponent<InventoryUI>().allCells[29].gameObject;
			leftHandCell.AddComponent<UpdateShield>();
		}

		[HarmonyPatch(typeof(ItemManager), "InitAllItems")]
		[HarmonyPostfix]
		private static void InitItemsPostfix()
		{
			initShields();
			if (ItemManager.Instance.allItems.Count >= 1)
			{
				foreach (NewItem newItem in Shields.newItems)
				{
					InventoryItem inventoryItem = ScriptableObject.CreateInstance<InventoryItem>();
					foreach (InventoryItem inventoryItem2 in ItemManager.Instance.allItems.Values)
					{
						if (inventoryItem2.name == newItem.copyName)
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
			Shields.newItems.Add(new NewItem("Steel Chestplate", "Steel Shield", "it protecc", Shields.steel, Shields.steel_sprite, Shields.shield, new List<string>() { "Iron bar", "Birch Wood" }, new List<int>() { 5, 5 }, 1));
			Shields.newItems.Add(new NewItem("Mithril Chestplate", "Mithril Shield", "it protecc", Shields.mithril, Shields.mithril_sprite, Shields.shield, new List<string>() { "Mithril bar", "Fir Wood" }, new List<int>() { 5, 5 }, 1));
			Shields.newItems.Add(new NewItem("Adamantite Chestplate", "Adamantite Shield", "it protecc", Shields.adamantite, Shields.adamantite_sprite, Shields.shield, new List<string>() { "Adamantite bar", "Oak Wood" }, new List<int>() { 5, 5 }, 1));
			Shields.newItems.Add(new NewItem("Obamium Chestplate", "Obamium Shield", "get down mr president!", Shields.obamium, Shields.obamium_sprite, Shields.shield, new List<string>() { "Obamium bar", "Dark Oak Wood" }, new List<int>() { 5, 5 }, 1));
			Shields.newItems.Add(new NewItem("Obamium Chestplate", "Night Shield", "Dark as the night sky", Shields.night, Shields.night_sprite, Shields.shield, new List<string>() { "Black Shard", "Dark Oak Wood" }, new List<int>() { 1, 5 }, 2));
		}

		
		[HarmonyPatch(typeof(CraftingUI), "Awake")]
		[HarmonyPostfix]
		private static void CraftingPostfix(CraftingUI __instance)
		{
			if (!__instance.handCrafts && __instance.tabParent.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text.Equals("Other"))
			{
				foreach (NewItem newItem in Shields.newItems)
				{
					InventoryItem[] items = __instance.tabs[2].items;
					InventoryItem[] array = new InventoryItem[items.Length + 1];
					items.CopyTo(array, 0);
					array[items.Length] = newItem.gameItem;
					__instance.tabs[2].items = array;
				}
			}
		}
	}
}
