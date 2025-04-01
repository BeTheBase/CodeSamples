using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.TriggerEvent("SwitchToTopDown");
        }
    }
}
