using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace LifetimeCruiserWarranty
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "33mamaster.LifetimeCruiserWarranty";
        private const string modName = "Lifetime Cruiser Warranty";
        private const string modVersion = "1.1.0";

        public static ConfigFile config;
        internal static new ManualLogSource Logger;

        private readonly Harmony harmony = new Harmony(modGUID);

        private static Plugin Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Logger = base.Logger;
            Logger.LogInfo($"Mod {modName} is loaded!");

            config = Config;
            LifetimeCruiserWarranty.Config.Load();

            harmony.PatchAll();
        }
    }
}
