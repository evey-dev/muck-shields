using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace MuckShields
{
	[BepInPlugin(ID, MODNAME, VERSION)]
	// [BepInProcess("Muck.exe")]
	public class Shields : BaseUnityPlugin
	{
		public const string
		MODNAME = "Muck Shields",
		// AUTHOR = "Your Mother",
		ID = "evey-dev.MuckShields",
		VERSION = "1.0";

		public Harmony harmony;
		public Assembly assembly;
		public ManualLogSource log;

		public static List<NewItem> newItems = new List<NewItem>();

		public static Texture2D steel, mithril, adamantite, obamium, night;
		public static Sprite steel_sprite, mithril_sprite, adamantite_sprite, obamium_sprite, night_sprite;
		public static Mesh shield;

		private void Awake()
		{
            Debug.Log($"Plugin Muck Shields is loaded!");
            harmony = new Harmony(ID);
            assembly = Assembly.GetExecutingAssembly();
			harmony.PatchAll(assembly);
			init();
		}

		private void init()
		{
			Debug.Log("\n\n\n\n\n\n\n\n\nSHIELDS.CS REACHED\n\n\n\n\n\n\n\n\n");
			Stream embeddedResourceStream = Shields.GetEmbeddedResourceStream("muck_shields.resources");
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

			Harmony.CreateAndPatchAll(typeof(CreateItemsPatch), null);
			Harmony.CreateAndPatchAll(typeof(ShieldImplementPatch), null);

			Shields.newItems.Add(new NewItem("Steel Chestplate", "Steel Shield", "it protecc", Shields.steel, Shields.steel_sprite, Shields.shield, new List<string>() { "Iron bar", "Wood" }, new List<int>() { 5, 5 }, 1));
			Shields.newItems.Add(new NewItem("Mithril Chestplate", "Mithril Shield", "it protecc", Shields.mithril, Shields.mithril_sprite, Shields.shield, new List<string>() { "Mithril bar", "Birch Wood" }, new List<int>() { 5, 5 }, 2));
			Shields.newItems.Add(new NewItem("Adamantite Chestplate", "Adamantite Shield", "it protecc", Shields.adamantite, Shields.adamantite_sprite, Shields.shield, new List<string>() { "Adamantite bar", "Fir Wood" }, new List<int>() { 5, 5 }, 2));
			Shields.newItems.Add(new NewItem("Obamium Chestplate", "Obamium Shield", "get down mr president!", Shields.obamium, Shields.obamium_sprite, Shields.shield, new List<string>() { "Obamium bar", "Dark Oak Wood" }, new List<int>() { 5, 5 }, 2));
			Shields.newItems.Add(new NewItem("Obamium Chestplate", "Night Shield", "Dark as the night sky", Shields.night, Shields.night_sprite, Shields.shield, new List<string>() { "Black Shard", "Dark Oak Wood" }, new List<int>() { 1, 5 }, 2));
		}


		public static Stream GetEmbeddedResourceStream(string resourceName)
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
		}
	}
}
