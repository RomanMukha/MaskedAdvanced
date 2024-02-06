using BepInEx.Logging;
using HarmonyLib;

namespace MaskedAdvanced.Patches;

[HarmonyPatch]
internal class FindMaskedPrefab
{
    [HarmonyPatch(typeof(Terminal), "Start")]
    [HarmonyPrefix]
    static void SaveMaskedPrefab(ref SelectableLevel[] ___moonsCatalogueList)
    {
        ManualLogSource logger = Logger.CreateLogSource(PluginInfo.Meta.Guid);

        foreach (var moon in ___moonsCatalogueList)
        {
            foreach (var enemy in moon.Enemies)
            {
                if (enemy.enemyType.enemyName != "Masked") continue;
                    
                logger.LogInfo("Found masked enemy prefab, saving it");
                MaskedAdvanceMain.MaskedEnemy = enemy;
            }
        }
    }
}