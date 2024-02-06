using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using MaskedAdvanced.Patches;

namespace MaskedAdvanced
{
    [BepInPlugin(PluginInfo.Meta.Guid, PluginInfo.Meta.Name, PluginInfo.Meta.Version)]
    public class MaskedAdvanceMain : BaseUnityPlugin
    {
        private readonly Harmony HarmonyInstance = new(PluginInfo.Meta.Guid);
        internal static MaskedAdvanceMain ThisInstance;
        internal static ManualLogSource LogSource;
        
        private ConfigEntry<int> SpawnRateConfig;
        public static int SpawnRate;
        
        public static SpawnableEnemyWithRarity MaskedEnemy;

        private void Awake()
        {
            if (ThisInstance == null)
            {
                ThisInstance = this;
            }

            SpawnRateConfig = Config.Bind("Main", "SpawnRate", 15, "Spawn rate for Masked Enemy. Default value is 15");
            SpawnRate = SpawnRateConfig.Value;
            
            LogSource = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.Meta.Guid);
            LogSource.LogInfo($"{PluginInfo.Meta.Name} mod is loading");
            
            HarmonyInstance.PatchAll(typeof(MaskedAdvanceMain));
            HarmonyInstance.PatchAll(typeof(FindMaskedPrefab));
            HarmonyInstance.PatchAll(typeof(MaskedSpawn));
        }
    }
}
