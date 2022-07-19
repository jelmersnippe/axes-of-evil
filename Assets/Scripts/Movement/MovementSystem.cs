using System;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public static event Action<int, Vector2Int, bool> OnMovementRequestValidated;

    public static Tile[,] map;

    private void OnEnable()
    {
        Movement.OnRequestMovement += ValidateMovement;
    }

    private void OnDisable()
    {
        Movement.OnRequestMovement -= ValidateMovement;
    }

    private void ValidateMovement(Movement requestingUnit, Vector2Int targetSquare)
    {
        // TODO: Validate units movement capabilities -> range, flying etc.
        bool inBoundsX = targetSquare.x >= 0 && targetSquare.x < map.GetLength(0);
        bool inBoundsY = targetSquare.y >= 0 && targetSquare.y < map.GetLength(1);
        bool valid = inBoundsX && inBoundsY && map[targetSquare.x, targetSquare.y].walkable;

        Debug.Log(requestingUnit.name + " on tile " + requestingUnit.transform.position + "requested movement to " + targetSquare + ". Valid: " + valid);

        OnMovementRequestValidated?.Invoke(requestingUnit.gameObject.GetInstanceID(), targetSquare, valid);
    }
}
