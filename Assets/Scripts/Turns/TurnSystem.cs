using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static event Action<TurnBased> OnTurnGrant;
    [SerializeField] private TurnBasedRuntimeSet activeEntities;
    private TurnBased currentlyActiveEntity;
    private Queue<TurnBased> turnQueue = new Queue<TurnBased>();

    private void OnEnable()
    {
        TurnBased.OnTurnComplete += CompleteTurn;
        MapGenerator.OnEntitiesSpawned += InitializeTurnQueue;
    }

    private void OnDisable()
    {
        TurnBased.OnTurnComplete -= CompleteTurn;
        MapGenerator.OnEntitiesSpawned -= InitializeTurnQueue;
    }

    private void CompleteTurn(TurnBased entity)
    {
        if (entity != currentlyActiveEntity)
        {
            Debug.LogWarning(entity.name + " emited a OnTurnComplete event without it being their turn!");
            return;
        }
        Debug.Log(entity.name + " completed it's turn");
        GrantNextTurn();
    }

    private void GrantNextTurn()
    {
        if (turnQueue.Count <= 0)
        {
            InitializeTurnQueue();
            return;
        }

        currentlyActiveEntity = turnQueue.Dequeue();
        Debug.Log("Granting turn to " + currentlyActiveEntity.name);
        OnTurnGrant?.Invoke(currentlyActiveEntity);
    }

    private void InitializeTurnQueue()
    {
        turnQueue.Clear();
        if (activeEntities.items.Count <= 0)
        {
            Debug.LogWarning("No more active entities to enqueue in the TurnSystem!");
            return;
        }

        foreach (TurnBased entity in activeEntities.items)
        {
            turnQueue.Enqueue(entity);
        }
        GrantNextTurn();
    }
}
