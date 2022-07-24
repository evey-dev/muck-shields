using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace muck_shields
{
    [BepInPlugin(ID, MODNAME, VERSION)]

    public class Main : BaseUnityPlugin 
    {

        public const string
        MODNAME = "Muck Shields",
        // AUTHOR = "Your Mother",
        ID = "me.evey-dev.Muck Shields",
        VERSION = "1.0";
        public Harmony harmony;
        public Assembly assembly;
        public ManualLogSource log;

        public Main () {
            log = Logger;
            harmony = new Harmony(ID);
            assembly = Assembly.GetExecutingAssembly();
        }

        public void Start() {
            harmony.PatchAll(assembly);
        }

    }
}