using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConceptArtSlideshowManager : MonoBehaviour
{
    public Image conceptArtDisplay; // UI Image where the concept art will be shown
    public ConceptArtData conceptArtData; // Reference to the ScriptableObject holding concept art images
    public float ConceptWaitTime = 4f;
    private int currentImageIndex = 0; // Track the current image index

    private void Start()
    {
        InitSlideshow();
    }

    // Initializes the first image in the slideshow
    public void InitSlideshow()
    {
        if (conceptArtData != null && conceptArtData.conceptArtImages.Count > 0)
        {
            conceptArtDisplay.sprite = conceptArtData.conceptArtImages[0];
        }
        else
        {
            Debug.LogError("No concept art images available!");
        }
    }

    // Updates the slideshow every 'waitTime' seconds
    public void StartSlideshow()
    {
        if (conceptArtData != null && conceptArtData.conceptArtImages.Count > 0)
        {
            StartCoroutine(WaitUpdateSlideshow(ConceptWaitTime));
        }
    }

    // Coroutine to wait and update the concept art image
    private IEnumerator WaitUpdateSlideshow(float waitTime)
    {
        while (currentImageIndex < conceptArtData.conceptArtImages.Count)
        {
            yield return new WaitForSeconds(waitTime);

            // Ensure the index is within bounds
            if (currentImageIndex < conceptArtData.conceptArtImages.Count)
            {
                conceptArtDisplay.sprite = conceptArtData.conceptArtImages[currentImageIndex];
                currentImageIndex++; // Move to the next image
            }
        }
    }

    // Resets the slideshow to the beginning
    public void ResetSlideshow()
    {
        currentImageIndex = 0;
        InitSlideshow();
    }
}
