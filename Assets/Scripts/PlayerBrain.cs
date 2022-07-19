using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerBrain : Brain
{
    [SerializeField] private Movement movement;

    private void OnEnable()
    {
        MovementSystem.OnMovementRequestValidated += HandleValidatedMovementRequest;
    }

    private void OnDisable()
    {
        MovementSystem.OnMovementRequestValidated -= HandleValidatedMovementRequest;
    }

    private void Update()
    {
        Vector3 movementRequest = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            movementRequest = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            movementRequest = Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            movementRequest = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            movementRequest = Vector3.left;
        }

        if (movementRequest == Vector3.zero)
        {
            return;
        }

        Vector3 requestPosition = transform.position + movementRequest;
        Debug.Log("Player RequestMovement");
        movement.RequestMovement(new Vector2Int(Mathf.RoundToInt(requestPosition.x), Mathf.RoundToInt(requestPosition.y)));
    }

    private void HandleValidatedMovementRequest(int id, Vector2Int targetSquare, bool valid)
    {
        if (id != gameObject.GetInstanceID())
        {
            return;
        }
        if (valid)
        {
            FinishedActing();
            return;
        }
    }
}
