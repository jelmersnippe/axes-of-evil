using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static event Action<Movement, Vector2Int> OnRequestMovement;
    private int id;

    private void OnEnable()
    {
        id = gameObject.GetInstanceID();
        MovementSystem.OnMovementRequestValidated += HandleValidatedMovementRequest;
    }

    private void OnDisable()
    {
        MovementSystem.OnMovementRequestValidated -= HandleValidatedMovementRequest;
    }

    public void RequestMovement(Vector2Int targetSquare)
    {
        OnRequestMovement?.Invoke(this, targetSquare);
    }

    private void HandleValidatedMovementRequest(int instanceId, Vector2Int targetSquare, bool valid)
    {
        if (!valid || instanceId != id)
        {
            return;
        }

        Move(targetSquare);
    }

    private void Move(Vector2Int targetSquare)
    {
        Debug.Log("Moving " + name + " from " + transform.position + " to " + targetSquare);
        transform.position = (Vector3Int)targetSquare;
    }
}
