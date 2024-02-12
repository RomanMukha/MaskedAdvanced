using System;
using Unity.Netcode;
using UnityEngine;

namespace MaskedAdvanced.Patches;

public class MaskedBehaviour : NetworkBehaviour
{
    public EnemyAI __instance;
    public GrabbableObject flashlightObj;
    public GameObject somePrefab;
    public GameObject itemHolder;
    public bool holdingFlashlight;

    public void Start()
    {
        __instance = GetComponent<MaskedPlayerEnemy>();
        flashlightObj = GetComponent<FlashlightItem>();
        
        itemHolder = new GameObject("ItemHolder");
        itemHolder.transform.parent = this.__instance.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0);
        itemHolder.transform.localPosition = new Vector3(-1f / 500f, 0.036f, -0.042f);
        itemHolder.transform.localRotation = Quaternion.Euler(-3.616f, -2.302f, 0.145f);

        Plugin.LogSource.LogMessage("Masked Behaviour started");
        Plugin.LogSource.LogMessage(__instance);

        if (holdingFlashlight) return;

        var someObject = Instantiate(somePrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity, RoundManager.Instance.spawnedScrapContainer);
        someObject.GetComponent<NetworkObject>().Spawn();
        
        var flashlight = someObject.GetComponent<FlashlightItem>();
        if (flashlight == null)
        {
            Plugin.LogSource.LogMessage("Flashlight did not contain FlashlightItem component.");
            return;
        }
        
        flashlight.SetScrapValue(1);
        flashlight.parentObject = itemHolder.transform;
        flashlight.isHeldByEnemy = true;
        flashlight.grabbableToEnemies = false;
        flashlight.grabbable = false;
        flashlight.GrabItemFromEnemy(__instance);
        
        // foreach (GrabbableObject flashlight in FlashlightList.Instance.flashlightList)
        // {
        //     flashlightObj = flashlight;
        //     flashlightObj.parentObject = itemHolder.transform;
        //     flashlightObj.hasHitGround = false;
        //     flashlightObj.isHeld = true;
        //     flashlightObj.isHeldByEnemy = true;
        //     flashlightObj.grabbable = false;
        //     holdingFlashlight = true;
        //     flashlightObj.GrabItemFromEnemy(__instance);
        // }
    }
}