using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {
    Fuel,
    Wood
}

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    [SerializeField] private float mass;

    public ResourceType GetResourceType() {
        return type;
    }

    public float GetMass() {
        return mass;
    }

    private void Awake() {
        GetComponent<Rigidbody2D>().mass = mass;
    }

    public void OnAddedToInventory(Inventory inventory) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        DestroyImmediate(rb);
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void OnRemovedFromInventory(Inventory inventory) {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.mass = mass;
        GetComponent<Collider2D>().isTrigger = false;
    }
}
