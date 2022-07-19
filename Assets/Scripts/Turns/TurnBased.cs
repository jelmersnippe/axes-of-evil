using System;
using UnityEngine;

[RequireComponent(typeof(Brain))]
public class TurnBased : MonoBehaviour
{
    public static event Action<TurnBased> OnTurnComplete;
    [SerializeField] private Brain brain;
    [SerializeField] private TurnBasedRuntimeSet activeEntities;

    private void OnEnable()
    {
        activeEntities.Add(this);
        TurnSystem.OnTurnGrant += SetActiveTurn;
        Brain.OnFinishedActing += CompleteTurn;
        brain.enabled = false;
    }

    private void OnDisable()
    {
        activeEntities.Remove(this);
        TurnSystem.OnTurnGrant -= SetActiveTurn;
        Brain.OnFinishedActing -= CompleteTurn;
    }

    private void SetActiveTurn(TurnBased entity)
    {
        if (entity == this)
        {
            brain.enabled = true;
        }
    }

    public virtual void CompleteTurn(Brain brain)
    {
        if (brain.gameObject.GetInstanceID() == gameObject.GetInstanceID())
        {
            brain.enabled = false;
            OnTurnComplete?.Invoke(this);
        }
    }
}
