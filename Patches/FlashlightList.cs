using System.Collections.Generic;
using UnityEngine;

namespace MaskedAdvanced.Patches;

public class FlashlightList : MonoBehaviour
{
    public List<GrabbableObject> flashlightList = new List<GrabbableObject>();
    
    public static FlashlightList Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }
}