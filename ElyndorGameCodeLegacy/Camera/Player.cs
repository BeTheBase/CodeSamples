using UnityEngine;

public class Player : MonoBehaviour
{
    public MonoBehaviour[] fpsControllerComponents; // List of FPS controller components to disable/enable

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SwitchableObject"))
        {
            EventManager.TriggerEvent("SwitchToTopDown");
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening("SwitchToTopDown", DisableFPSController);
        EventManager.StartListening("SwitchBackToFPS", EnableFPSController);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SwitchToTopDown", DisableFPSController);
        EventManager.StopListening("SwitchBackToFPS", EnableFPSController);
    }

    private void DisableFPSController()
    {
        foreach (var component in fpsControllerComponents)
        {
            component.enabled = false;
        }

        // Enable the mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void EnableFPSController()
    {
        foreach (var component in fpsControllerComponents)
        {
            component.enabled = true;
        }

        // Disable the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
