using System;
using System.Collections.Generic;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace muck-shields
{
	internal class CreateItemsPatch
	{
		
		[HarmonyPatch(typeof(ItemManager), "InitAllItems")]
		[HarmonyPostfix]
		private static void Postfix()
		{
			bool flag = ItemManager.Instance.allItems.Count < 1;
			if (!flag)
			{
				Debug.Log("Adding Shields");
				foreach (NewItem newItem in ObamiumStuff.newItems)
				{
					InventoryItem inventoryItem = ScriptableObject.CreateInstance<InventoryItem>();
					foreach (InventoryItem inventoryItem2 in ItemManager.Instance.allItems.Values)
					{
						bool flag2 = inventoryItem2.name == newItem.originalName;
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
					inventoryItem.requirements = newItem.requirements
					ItemManager.Instance.allItems.Add(inventoryItem.id, inventoryItem);
					Debug.Log("Added: " + inventoryItem.name);
					newItem.gameItem = inventoryItem;
				}
			}
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
