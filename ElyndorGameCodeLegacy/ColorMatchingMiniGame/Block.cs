using UnityEngine;
using System.Linq;

public class Block : MonoBehaviour
{
    private Color[] possibleColors;
    private Color[] solidColors;
    private int currentColorIndex = 0;
    private Renderer blockRenderer;

    void Start()
    {
        blockRenderer = GetComponent<Renderer>();

        // Get the ColorManager instance and retrieve the color arrays
        ColorManager colorManager = FindObjectOfType<ColorManager>();
        if (colorManager != null)
        {
            possibleColors = colorManager.PossibleColors;
            solidColors = colorManager.SolidColors;
        }
        else
        {
            Debug.LogError("ColorManager not found in the scene.");
        }

        SetColor(possibleColors[currentColorIndex]);
    }

    void OnMouseDown()
    {
        if (!solidColors.Any(sc => sc.Equals(blockRenderer.material.color)))
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % possibleColors.Length;
        SetColor(possibleColors[currentColorIndex]);
    }

    public void SetColor(Color color)
    {
        blockRenderer.material.color = color;
    }
}
