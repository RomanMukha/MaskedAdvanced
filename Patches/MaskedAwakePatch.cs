using HarmonyLib;

namespace MaskedAdvanced.Patches;

[HarmonyPatch(typeof(MaskedPlayerEnemy))]
internal class MaskedAwakePatch
{
    public static MaskedBehaviour MaskedBehaviourInstance;
    
    [HarmonyPrefix]
    [HarmonyPatch("Awake")]
    public static void BeforeAwake(EnemyAI __instance)
    {
        MaskedBehaviourInstance = __instance.gameObject.AddComponent<MaskedBehaviour>();
    }
}