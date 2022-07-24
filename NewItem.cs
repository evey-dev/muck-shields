using System;
using UnityEngine;

namespace muck_shields
{
	
	public class NewItem
	{
		public string copyName, name, description;
		public Texture2D texture;
		public Sprite sprite;
		public Mesh mesh;
		public InventoryItem.CraftRequirement[] requirements;
		public int factor;
		public InventoryItem.ItemTag tag = InventoryItem.ItemTag.LeftHanded;
		public InventoryItem gameItem;
		
		public NewItem(string copyName, string name, string description, Texture2D texture, Sprite sprite, Mesh mesh, List<string> requirement_names, List<int> requirement_amounts, int factor)
		{
			this.copyName = copyName;
			this.name = name;
			this.description = description;
			this.texture = texture;
			this.sprite = sprite;
			this.mesh = mesh;
			this.factor = factor;
			
			List<InventoryItem.CraftRequirement> list = new List<InventoryItem.CraftRequirement>();
			foreach (InventoryItem inventoryItem in ItemManager.Instance.allItems.Values)
			{
				int num = requirement_names.IndexOf(inventoryItem.name);
				if (num > -1)
				{
					list.Add(new InventoryItem.CraftRequirement
					{
						item = inventoryItem,
						amount = requirement_amounts[num]
					});
				}
			}
			this.requirements = list.ToArray();
		}
	}
}
