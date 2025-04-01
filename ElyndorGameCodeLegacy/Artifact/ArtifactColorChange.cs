using System.Collections;
using UnityEngine;

public class ArtifactColorChange : MonoBehaviour
{
    // Public variable to set the color change interval, editable in the Inspector
    public float colorChangeInterval = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to change the color periodically
        StartCoroutine(ChangeColorPeriodically());
    }

    // Coroutine to change the color every 'colorChangeInterval' seconds
    IEnumerator ChangeColorPeriodically()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(colorChangeInterval);

            // Generate a random color
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // Get the Renderer component of the object
            Renderer renderer = GetComponent<Renderer>();

            // Check if the Renderer component exists
            if (renderer != null)
            {
                // Clone the material to create a unique instance for this object
                Material clonedMaterial = Instantiate(renderer.material);

                // Apply the random color to the cloned material
                clonedMaterial.color = randomColor;

                // Assign the cloned material to the renderer
                renderer.material = clonedMaterial;
            }
            else
            {
                // Log a message if no Renderer component is found
                Debug.LogWarning("No Renderer component found on this object.");
            }
        }
    }
}
