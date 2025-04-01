using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public Camera fpsCamera;
    public Camera topDownCamera;
    public float switchBackTime = 5.0f;

    private void OnEnable()
    {
        EventManager.StartListening("SwitchToTopDown", SwitchToTopDown);
        EventManager.StartListening("SwitchBackToFPS", SwitchBackToFPS);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SwitchToTopDown", SwitchToTopDown);
        EventManager.StopListening("SwitchBackToFPS", SwitchBackToFPS);
    }

    private void SwitchToTopDown()
    {
        if (fpsCamera != null)
            fpsCamera.gameObject.SetActive(false);

        if (topDownCamera != null)
            topDownCamera.gameObject.SetActive(true);

        // Start the coroutine to switch back to FPS view after a delay
        StartCoroutine(SwitchBackCoroutine());
    }

    private IEnumerator SwitchBackCoroutine()
    {
        yield return new WaitForSeconds(switchBackTime);
        EventManager.TriggerEvent("SwitchBackToFPS");
    }

    private void SwitchBackToFPS()
    {
        if (fpsCamera != null)
            fpsCamera.gameObject.SetActive(true);

        if (topDownCamera != null)
            topDownCamera.gameObject.SetActive(false);
    }
}
