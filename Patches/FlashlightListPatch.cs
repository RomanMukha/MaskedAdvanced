using HarmonyLib;

namespace MaskedAdvanced.Patches;

[HarmonyPatch(typeof (GrabbableObject))]
internal class FlashlightListPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    private static void AfterStart(GrabbableObject __instance)
    {
        // __instance.gameObject.AddComponent<CheckItemCollision>();
        if (!(__instance is FlashlightItem)) return;
        
        FlashlightList.Instance.flashlightList.Add(__instance);
    }

    [HarmonyPostfix]
    [HarmonyPatch("DestroyObjectInHand")]
    private static void DestroyObjectInHand_Postfix(GrabbableObject __instance)
    {
        if (!(__instance is FlashlightItem)) return;
        
        FlashlightList.Instance.flashlightList.Remove(__instance);
    }
}
