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
			Harmony.CreateAndPatchAll(typeof(CreateItemsPatch), null);
			Harmony.CreateAndPatchAll(typeof(ShieldImplementPatch), null);
		}
	}
}
