using System;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using Logger = BepInEx.Logging.Logger;

namespace MaskedAdvanced.Patches;

[HarmonyPatch(typeof(RoundManager))]
public class MaskedSpawn
{
    [HarmonyPatch("BeginEnemySpawning")]
    [HarmonyPrefix]
    static void UpdateBeginEnemySpawning(ref SelectableLevel ___currentLevel)
    {
        ManualLogSource logger = Logger.CreateLogSource(PluginInfo.Meta.Guid);

        try
        {
            logger.LogInfo("Starting BeginEnemySpawning");

            SpawnableEnemyWithRarity maskedEnemy = MaskedAdvanceMain.MaskedEnemy;
            
            maskedEnemy.enemyType.PowerLevel = 1;
            
            logger.LogInfo($"Masked Enemy: {maskedEnemy}");
            logger.LogInfo($"Masked Rarity: {maskedEnemy.rarity}");
            logger.LogInfo($"Masked Rarity: {maskedEnemy.enemyType.probabilityCurve}");
            //maskedEnemy.enemyType.probabilityCurve = flowerman.enemyType.probabilityCurve;

            maskedEnemy.rarity = MaskedAdvanceMain.SpawnRate > 1000000
                ? 1000000
                : MaskedAdvanceMain.SpawnRate;

            maskedEnemy.enemyType.MaxCount = 50;
            maskedEnemy.enemyType.isOutsideEnemy = true;
            
            ___currentLevel.enemySpawnChanceThroughoutDay = new AnimationCurve((Keyframe[])(object)new Keyframe[2]
            {
                new(0f, 500f),
                new(0.5f, 500f)
            });
            ___currentLevel.daytimeEnemySpawnChanceThroughDay = new AnimationCurve((Keyframe[])(object)new Keyframe[2]
            {
                new(0f, 500f),
                new(0.5f, 500f)
            });
            ___currentLevel.outsideEnemySpawnChanceThroughDay = new AnimationCurve((Keyframe[])(object)new Keyframe[2]
            {
                new(0f, 50f),
                new(21f, 50f)
            });
            
            ___currentLevel.OutsideEnemies.Add(maskedEnemy);
            ___currentLevel.DaytimeEnemies.Add(maskedEnemy);
            ___currentLevel.maxEnemyPowerCount += maskedEnemy.enemyType.MaxCount;
            ___currentLevel.maxDaytimeEnemyPowerCount += maskedEnemy.enemyType.MaxCount;
            ___currentLevel.maxOutsideEnemyPowerCount += maskedEnemy.enemyType.MaxCount;
            ___currentLevel.Enemies.Add(maskedEnemy);
        }
        catch (Exception e)
        {
            logger.LogInfo($"Error in UpdateBeginEnemySpawning: {e}");
            throw;
        }
    }
}
