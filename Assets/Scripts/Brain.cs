using System;
using UnityEngine;

[RequireComponent(typeof(TurnBased))]
public abstract class Brain : MonoBehaviour
{
    public static event Action<Brain> OnFinishedActing;

    protected virtual void FinishedActing()
    {
        OnFinishedActing?.Invoke(this);
    }
}
