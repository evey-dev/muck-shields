using System;
using UnityEngine;

namespace muck-shields
{
	
	public class NewItem
	{
		public string copyName;
		public string name;
		public string description;
		public Texture2D texture;
		public Sprite sprite;
		public Mesh mesh;
		public InventoryItem.CraftRequirement[] requirements;
		public int factor;
		public ItemTag tag = ItemTag.LeftHanded;
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
						amount = newItem.requirements[num].amount
					});
				}
			}
			this.requirements = list.ToArray();
		}
	}
}
