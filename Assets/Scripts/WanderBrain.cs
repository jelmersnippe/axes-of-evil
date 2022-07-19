using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class WanderBrain : Brain
{
    [SerializeField] private Movement movement;
    private List<Vector2Int> options;
    private bool isAttemptingMovement = false;

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
        if (isAttemptingMovement)
        {
            return;
        }
        Vector2Int currentPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        options = new List<Vector2Int>() {
            currentPosition+Vector2Int.up,
            currentPosition+Vector2Int.down,
            currentPosition+Vector2Int.left,
            currentPosition+Vector2Int.right
            };
        AttemptMovement(options);
    }

    private void AttemptMovement(List<Vector2Int> options)
    {
        if (options.Count <= 0)
        {
            FinishedActing();
            return;
        }
        isAttemptingMovement = true;
        Vector2Int targetSquare = options[Random.Range(0, options.Count)];
        movement.RequestMovement(targetSquare);
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

        options.Remove(targetSquare);
        AttemptMovement(options);
    }

    protected override void FinishedActing()
    {
        base.FinishedActing();
        isAttemptingMovement = false;
    }
}
