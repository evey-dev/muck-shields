using BepInEx;

namespace muck_shields
{
    [BepInPlugin("me.evey-dev.muck_shields", "Muck Shields", "1.0")]

    public class MainClass : BaseUnityPlugin 
    {
        public static MainClass instance;
        public Harmony harmony;
        public ManualLogSource log;

        private void Awake() {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
            
            log = Logger;
            harmony = new Harmony("me.evey-dev.muck_shields");
        }

    }
}