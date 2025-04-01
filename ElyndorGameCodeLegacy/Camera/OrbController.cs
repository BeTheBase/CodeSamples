using UnityEngine;

public class OrbController : MonoBehaviour, IOrbActions
{
    public Transform targetPosition; // The target position the orb should move to
    public float moveSpeed = 5.0f; // Speed at which the orb moves
    private bool isMoving = false;
    private bool passageCreated = false;

    private void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition.position = position;
        isMoving = true;
    }

    public void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f)
        {
            isMoving = false;
            WaitForPassage();
        }
    }

    public void WaitForPassage()
    {
        // Stop movement and wait for passage creation
        isMoving = false;
        passageCreated = false;
    }

    public void PassageCreated()
    {
        // Call this method when the passage is created
        passageCreated = true;
        if (!isMoving && passageCreated)
        {
            isMoving = true;
        }
    }
}
