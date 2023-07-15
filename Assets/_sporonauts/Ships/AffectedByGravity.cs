using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedByGravity : MonoBehaviour
{
    private void Awake() {
    }

    private void FixedUpdate() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (!rb) {
            return;
        }
        
        foreach (Planet planet in Planet.planets) {
            rb.AddForce(planet.CalculateGravity(transform.position) * rb.mass);
        }
    }
}
