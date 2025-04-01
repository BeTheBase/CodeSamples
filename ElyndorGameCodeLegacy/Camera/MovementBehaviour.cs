using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveObject(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveObject(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveObject(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveObject(Vector3.back);
        }
    }

    private void MoveObject(Vector3 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
