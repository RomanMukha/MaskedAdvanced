using Unity.Netcode;

namespace MaskedAdvanced.Patches;

public class MaskedBehaviour : NetworkBehaviour
{
    public EnemyAI __instance;

    public void Start()
    {
        __instance = GetComponent<MaskedPlayerEnemy>();
        
        Plugin.LogSource.LogMessage("Masked Behaviour started");
        Plugin.LogSource.LogMessage(__instance);
    }

    public void Update()
    {
        Plugin.LogSource.LogMessage("Masked Behaviour updated");
    }
}