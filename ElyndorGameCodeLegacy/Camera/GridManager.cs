using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Camera topDownCamera;
    public LayerMask clickableLayer;
    private GameObject selectedObject;
    public GameObject arrowCanvasPrefab;
    private GameObject arrowCanvasInstance;

    private void OnEnable()
    {
        EventManager.StartListening("SwitchToTopDown", EnableMouseInput);
        EventManager.StartListening("SwitchBackToFPS", DisableMouseInput);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SwitchToTopDown", EnableMouseInput);
        EventManager.StopListening("SwitchBackToFPS", DisableMouseInput);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastToGrid();
        }

        if (selectedObject != null && Input.GetMouseButton(1)) // Right mouse button to move the object
        {
            MoveSelectedObject();
        }
    }

    private void EnableMouseInput()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableMouseInput()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void RaycastToGrid()
    {
        Ray ray = topDownCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
        {
            Debug.Log("Hit: " + hit.collider.name); // Debug line to check what we hit
            if (hit.collider.CompareTag("MovableObject"))
            {
                SelectObject(hit.collider.gameObject);
            }
        }
        else
        {
            Debug.Log("No hit");
        }
    }

    private void SelectObject(GameObject obj)

    {
        selectedObject = obj;

        // Add MovementBehaviour if not already added
        if (selectedObject.GetComponent<MovementBehaviour>() == null)
        {
            selectedObject.AddComponent<MovementBehaviour>();
        }

        // Instantiate or re-use arrow canvas prefab
        if (arrowCanvasPrefab != null)
        {
            if (arrowCanvasInstance == null)
            {
                arrowCanvasInstance = Instantiate(arrowCanvasPrefab, selectedObject.transform.position, arrowCanvasPrefab.transform.rotation);
            }
            else
            {
                arrowCanvasInstance.SetActive(true);
            }
        }
    }

    private void MoveSelectedObject()
    {
        // Convert mouse position to world position
        Ray ray = topDownCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); // Assuming movement on X/Z plane
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 targetPosition = ray.GetPoint(distance);
        }
    }
}
