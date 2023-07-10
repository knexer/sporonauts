using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public event System.Action<Collider2D> OnTriggerEnter2DEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter2DEvent?.Invoke(other);
    }
}
