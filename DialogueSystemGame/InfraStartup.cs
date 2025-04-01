using UnityEngine;

public class InfraStartup : MonoBehaviour
{
    private void Awake()
    {
        // Voer alle DI-registraties uit.
        ServiceCollection.ConfigureServices();

        // Eventueel andere initialisaties, zoals het opstarten van je DialogueManager, etc.
        Debug.Log("DI Container is geconfigureerd en InfraStartup is voltooid.");
    }
}
