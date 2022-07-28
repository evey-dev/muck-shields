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
            harmony = new Harmony(ID);
            assembly = Assembly.GetExecutingAssembly();
			loadAssets();
			harmony.PatchAll(assembly);
			Harmony.CreateAndPatchAll(typeof(CreateItemsPatch), null);
			Harmony.CreateAndPatchAll(typeof(ShieldImplementPatch), null);
		}

		private void loadAssets() {
			Stream embeddedResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MuckShields.Resources.resources");
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
		}
	}
}
