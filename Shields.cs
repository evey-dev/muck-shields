using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace muck_shields
{
	
	[BepInPlugin("me.evey-dev.muck_shields", "Muck Shields", "1.0.1")]
	public class Shields : BaseUnityPlugin
	{
		public static List<NewItem> newItems = new List<NewItem>();
		
		public static Texture2D steel;
		public static Texture2D mithril;
		public static Texture2D adamantite;
		public static Texture2D obamium;
		public static Texture2D night;
		
		public static Sprite steel_sprite;
		public static Sprite mithril_sprite;
		public static Sprite adamantite_sprite;
		public static Sprite obamium_sprite;
		public static Sprite night_sprite;
		
		public static Mesh shield;
		
		private void Awake()
		{
			base.Logger.LogInfo("Started");
			Stream embeddedResourceStream = Shields.GetEmbeddedResourceStream("muck_shields.resources");
			AssetBundle assetBundle = AssetBundle.LoadFromStream(embeddedResourceStream);
			bool flag = assetBundle == null;
			if (flag)
			{
				Debug.Log("Failed to load AssetBundle ui!");
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
			
			Harmony.CreateAndPatchAll(typeof(CreateItemsPatch), null);

			Shields.newItems.Add(new NewItem("Steel Helmet", "Steel Shield", "it protecc", Shields.steel, Shields.steel_sprite, Shields.shield, new List<string>() {"Iron bar", "Wood"}, new List<int>() {5, 5}, 2));
			Shields.newItems.Add(new NewItem("Mithril Helmet", "Mithril Shield", "it protecc", Shields.mithril, Shields.mithril_sprite, Shields.shield, new List<string>() {"Mithril bar", "Birch Wood"}, new List<int>() {5, 5}, 2));
			Shields.newItems.Add(new NewItem("Adamantite Helmet", "Adamantite Shield", "it protecc", Shields.adamantite, Shields.adamantite_sprite, Shields.shield, new List<string>() {"Adamantite bar", "Fir Wood"}, new List<int>() {5, 5}, 2));
			Shields.newItems.Add(new NewItem("Obamium Helmet", "Obamium Shield", "get down mr president!", Shields.obamium, Shields.obamium_sprite, Shields.shield, new List<string>() {"Obamium bar", "Dark Oak Wood"}, new List<int>() {5, 5}, 2));
			Shields.newItems.Add(new NewItem("Obamium Helmet", "Night Shield", "Dark as the night sky", Shields.night, Shields.night_sprite, Shields.shield, new List<string>() {"Black Shard", "Dark Oak Wood"}, new List<int>() {1, 5}, 2));;
		}

		
		public static Stream GetEmbeddedResourceStream(string resourceName)
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
		}
	}
}
