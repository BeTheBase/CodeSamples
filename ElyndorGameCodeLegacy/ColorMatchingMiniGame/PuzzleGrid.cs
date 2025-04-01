using System.Collections;
using UnityEngine;

namespace Puzzles.Games
{
    public class PuzzleGrid : MonoBehaviour
    {
        public GameObject blockPrefab;
        public int gridSize = 8;
        public float canvasSize = 600f; // Size of the canvas
        private GameObject[,] grid;

        void Start()
        {
            StartCoroutine(SpawnBlocks());
        }

        IEnumerator SpawnBlocks()
        {
            float blockSize = canvasSize / gridSize;

            grid = new GameObject[gridSize, gridSize];
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    float xPos = x * blockSize - (canvasSize / 2) + (blockSize / 2);
                    float yPos = y * blockSize - (canvasSize / 2) + (blockSize / 2);

                    Vector3 position = new Vector3(xPos, yPos, 0);
                    GameObject newBlock = Instantiate(blockPrefab, transform); // Ensure the PuzzleGrid object is the parent
                    RectTransform rectTransform = newBlock.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = position;
                    rectTransform.sizeDelta = new Vector2(blockSize, blockSize);

                    Block blockScript = newBlock.GetComponent<Block>();
                    if (Random.Range(0f, 1f) < 0.2f) // 20% chance
                    {
                        blockScript.SetColor(Color.red);
                    }

                    grid[x, y] = newBlock;

                    // Yield to the next frame
                    yield return null;
                }
            }
        }
    }
}
